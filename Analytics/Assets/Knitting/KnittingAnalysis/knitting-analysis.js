(function () {

    'use strict';


    const data =
        window.knittingDashboardData || {};


    // =========================================================
    // POWER BI-LIKE PALETTE
    // =========================================================

    const chartColors = {
        blue: '#118DFF',
        darkBlue: '#12239E',
        orange: '#E66C37',
        purple: '#6B007B',
        pink: '#E044A7',
        green: '#1AAB40',
        red: '#D64550',
        teal: '#197278',
        yellow: '#D9B300',
        grey: '#8A8886'
    };


    // =========================================================
    // FAULT COLOURS
    //
    // Same fault = same colour throughout Knitting Quality.
    // =========================================================

    const defectColors = {

        brokenNeedle:
            chartColors.blue,

        droppedStitches:
            chartColors.orange,

        holes:
            chartColors.grey,

        oilMarks:
            chartColors.yellow,

        pressOff:
            chartColors.green,

        slubMarks:
            chartColors.purple,

        thick:
            chartColors.pink,

        thin:
            chartColors.teal
    };


    // =========================================================
    // PRODUCT / QUALITY COLOURS
    //
    // A product receives a deterministic colour.
    // The same product therefore remains the same colour.
    // =========================================================

    const qualityPalette = [
        '#118DFF', // bright blue
        '#1AAB40', // green
        '#E66C37', // orange
        '#744EC2', // purple
        '#D64550', // red
        '#00A6A6', // turquoise
        '#D9B300', // yellow/gold
        '#E044A7', // pink
        '#12239E', // dark blue
        '#197278', // dark teal
        '#FF8C00', // amber
        '#7A9A01', // olive green
        '#C239B3', // magenta
        '#0078D4', // medium blue
        '#8764B8', // lavender
        '#8A5A44', // brown
        '#00B7C3', // cyan
        '#A4262C'  // dark red
    ];

    const qualityColorCache = {};

    let nextQualityColorIndex = 0;


    function getQualityColor(product) {

        const key =
            product || 'Unknown';


        if (qualityColorCache[key]) {

            return qualityColorCache[key];
        }


        qualityColorCache[key] =
            qualityPalette[
            nextQualityColorIndex
            % qualityPalette.length
            ];


        nextQualityColorIndex++;


        return qualityColorCache[key];
    }



    // =========================================================
    // GENERAL HELPERS
    // =========================================================

    function number(value, decimals) {

        const n =
            Number(value || 0);


        return n.toLocaleString(
            undefined,
            {
                minimumFractionDigits:
                    decimals || 0,

                maximumFractionDigits:
                    decimals || 0
            });
    }


    function percentage(value) {

        return number(value, 2) + '%';
    }


    function escapeHtml(value) {

        return String(value == null ? '' : value)

            .replace(/&/g, '&amp;')

            .replace(/</g, '&lt;')

            .replace(/>/g, '&gt;')

            .replace(/"/g, '&quot;')

            .replace(/'/g, '&#039;');
    }


    function sum(rows, property) {

        return rows.reduce(
            function (total, row) {

                return total
                    + Number(
                        row[property] || 0);
            },
            0);
    }


    function groupRows(
        rows,
        keyProperty) {

        const groups = {};


        rows.forEach(
            function (row) {

                const key =
                    row[keyProperty]
                    || 'Unknown';


                if (!groups[key]) {

                    groups[key] = [];
                }


                groups[key].push(row);
            });


        return groups;
    }


    function setText(
        id,
        value) {

        const element =
            document.getElementById(id);


        if (element) {

            element.textContent =
                value;
        }
    }



    // =========================================================
    // TAB HANDLING
    // =========================================================

    function initialiseTabs() {

        const buttons =
            document.querySelectorAll(
                '.tab-button');


        buttons.forEach(
            function (button) {

                button.addEventListener(
                    'click',
                    function () {

                        const targetId =
                            button.getAttribute(
                                'data-tab');


                        document
                            .querySelectorAll(
                                '.tab-button')
                            .forEach(
                                function (b) {

                                    b.classList
                                        .remove(
                                            'active');
                                });


                        document
                            .querySelectorAll(
                                '.tab-page')
                            .forEach(
                                function (page) {

                                    page.classList
                                        .remove(
                                            'active');
                                });


                        button.classList
                            .add(
                                'active');


                        const target =
                            document
                                .getElementById(
                                    targetId);


                        if (target) {

                            target.classList
                                .add(
                                    'active');
                        }


                        /*
                         * Chart.js sometimes needs a resize after
                         * being displayed from a hidden tab.
                         */
                        setTimeout(
                            function () {

                                window.dispatchEvent(
                                    new Event(
                                        'resize'));
                            },
                            50);
                    });
            });
    }



    // =========================================================
    // DISK VARIANCE
    // =========================================================

    function getDiskVarianceColor(
        variance) {

        const absolute =
            Math.abs(
                Number(variance || 0));


        if (absolute > 3.5) {

            return chartColors.red;
        }


        if (absolute <= 1) {

            return chartColors.green;
        }


        return chartColors.blue;
    }


    function getVarianceClass(
        variance) {

        const absolute =
            Math.abs(
                Number(variance || 0));


        if (absolute > 3.5) {

            return 'variance-bad';
        }


        if (absolute <= 1) {

            return 'variance-good';
        }


        return 'variance-normal';
    }


    function renderDiskVariance() {

        const rows =
            data.diskVariance || [];


        const totalPieces =
            sum(
                rows,
                'pieceCount');


        const goodCount =
            rows.filter(
                function (row) {

                    return Math.abs(
                        Number(
                            row.diskVariancePercentage
                            || 0))
                        <= 1;
                })
                .length;


        const badCount =
            rows.filter(
                function (row) {

                    return Math.abs(
                        Number(
                            row.diskVariancePercentage
                            || 0))
                        > 3.5;
                })
                .length;


        setText(
            'diskQualityCount',
            number(rows.length, 0));


        setText(
            'diskPieceCount',
            number(totalPieces, 0));


        setText(
            'diskGoodCount',
            number(goodCount, 0));


        setText(
            'diskBadCount',
            number(badCount, 0));


        // -----------------------------------------------------
        // TABLE
        // -----------------------------------------------------

        const tableBody =
            document.getElementById(
                'diskVarianceTableBody');


        if (tableBody) {

            tableBody.innerHTML =
                rows.map(
                    function (row) {

                        const cssClass =
                            getVarianceClass(
                                row.diskVariancePercentage);


                        return `
                            <tr>
                                <td>
                                    ${escapeHtml(
                            row.greigeQuality)}
                                </td>

                                <td class="number">
                                    ${number(
                                row.pieceCount,
                                0)}
                                </td>

                                <td class="number">
                                    ${number(
                                    row.averageStandardDisk,
                                    2)}
                                </td>

                                <td class="number">
                                    ${number(
                                        row.averageActualDisk,
                                        2)}
                                </td>

                                <td class="number ${cssClass}">
                                    ${percentage(
                                            row.diskVariancePercentage)}
                                </td>
                            </tr>
                        `;
                    })
                    .join('');
        }


        // -----------------------------------------------------
        // CHART
        // -----------------------------------------------------

        const canvas =
            document.getElementById(
                'diskVarianceChart');


        if (!canvas) {

            return;
        }


        const sortedRows =
            rows.slice()
                .sort(
                    function (a, b) {

                        return Number(
                            a.diskVariancePercentage)
                            -
                            Number(
                                b.diskVariancePercentage);
                    });


        new Chart(
            canvas,
            {
                type: 'bar',

                data: {

                    labels:
                        sortedRows.map(
                            function (row) {

                                return row.greigeQuality;
                            }),

                    datasets: [
                        {
                            label:
                                'Disk Variance %',

                            data:
                                sortedRows.map(
                                    function (row) {

                                        return row
                                            .diskVariancePercentage;
                                    }),

                            backgroundColor:
                                sortedRows.map(
                                    function (row) {

                                        return getDiskVarianceColor(
                                            row.diskVariancePercentage);
                                    }),

                            borderWidth: 0,

                            borderRadius: 2
                        }
                    ]
                },

                options: {

                    indexAxis: 'y',

                    responsive: true,

                    maintainAspectRatio: false,

                    plugins: {

                        legend: {
                            display: false
                        },

                        tooltip: {

                            callbacks: {

                                label:
                                    function (context) {

                                        return 'Variance: '
                                            + percentage(
                                                context.raw);
                                    }
                            }
                        }
                    },

                    scales: {

                        x: {

                            ticks: {

                                callback:
                                    function (value) {

                                        return value + '%';
                                    }
                            },

                            grid: {
                                color: '#edebe9'
                            }
                        },

                        y: {

                            grid: {
                                display: false
                            },

                            ticks: {
                                autoSkip: false
                            }
                        }
                    }
                }
            });
    }



    // =========================================================
    // KNITTING QUALITY
    // =========================================================

    const faultDefinitions = [

        {
            property:
                'brokenNeedle',

            label:
                'Broken Needle',

            color:
                defectColors.brokenNeedle
        },

        {
            property:
                'droppedStitches',

            label:
                'Dropped Stitches',

            color:
                defectColors.droppedStitches
        },

        {
            property:
                'holes',

            label:
                'Holes',

            color:
                defectColors.holes
        },

        {
            property:
                'oilMarks',

            label:
                'Oil Marks',

            color:
                defectColors.oilMarks
        },

        {
            property:
                'pressOff',

            label:
                'Press Off',

            color:
                defectColors.pressOff
        },

        {
            property:
                'slubMarks',

            label:
                'Slub Marks',

            color:
                defectColors.slubMarks
        },

        {
            property:
                'thick',

            label:
                'Thick',

            color:
                defectColors.thick
        },

        {
            property:
                'thin',

            label:
                'Thin',

            color:
                defectColors.thin
        }
    ];


    function renderKnittingQuality() {

        const rows =
            data.knittingQuality || [];


        const totalPieces =
            sum(
                rows,
                'totalPieces');


        const aGrade =
            sum(
                rows,
                'aGrade');


        const bGrade =
            sum(
                rows,
                'bGrade');


        const cGrade =
            sum(
                rows,
                'cGrade');


        const totalFaults =
            sum(
                rows,
                'totalFaults');


        setText(
            'qualityTotalPieces',
            number(
                totalPieces,
                0));


        setText(
            'qualityAGrade',
            number(
                aGrade,
                0));


        setText(
            'qualityBGrade',
            number(
                bGrade,
                0));


        setText(
            'qualityTotalFaults',
            number(
                totalFaults,
                0));


        renderGradeDistribution(
            aGrade,
            bGrade,
            cGrade);


        renderFaultSummary(rows);


        renderFaultsByGroup(
            rows,
            'machine',
            'faultsByMachineChart');


        renderFaultsByGroup(
            rows,
            'greigeProduct',
            'faultsByQualityChart');


        renderQualityTable(rows);
    }


    function renderGradeDistribution(
        aGrade,
        bGrade,
        cGrade) {

        const canvas =
            document.getElementById(
                'gradeDistributionChart');


        if (!canvas) {

            return;
        }


        new Chart(
            canvas,
            {
                type: 'doughnut',

                data: {

                    labels: [
                        'A Grade',
                        'B Grade',
                        'C Grade'
                    ],

                    datasets: [
                        {
                            data: [
                                aGrade,
                                bGrade,
                                cGrade
                            ],

                            backgroundColor: [
                                chartColors.green,
                                chartColors.orange,
                                chartColors.red
                            ],

                            borderColor:
                                '#ffffff',

                            borderWidth: 2
                        }
                    ]
                },

                options: {

                    responsive: true,

                    maintainAspectRatio: false,

                    plugins: {

                        legend: {

                            position:
                                'bottom'
                        }
                    }
                }
            });
    }


    function renderFaultSummary(rows) {

        const canvas =
            document.getElementById(
                'faultSummaryChart');


        if (!canvas) {

            return;
        }


        new Chart(
            canvas,
            {
                type: 'bar',

                data: {

                    labels:
                        faultDefinitions.map(
                            function (fault) {

                                return fault.label;
                            }),

                    datasets: [
                        {
                            label:
                                'Faults',

                            data:
                                faultDefinitions.map(
                                    function (fault) {

                                        return sum(
                                            rows,
                                            fault.property);
                                    }),

                            backgroundColor:
                                faultDefinitions.map(
                                    function (fault) {

                                        return fault.color;
                                    }),

                            borderWidth: 0,

                            borderRadius: 2
                        }
                    ]
                },

                options: {

                    responsive: true,

                    maintainAspectRatio: false,

                    plugins: {

                        legend: {
                            display: false
                        }
                    },

                    scales: {

                        x: {

                            grid: {
                                display: false
                            }
                        },

                        y: {

                            beginAtZero: true,

                            grid: {
                                color: '#edebe9'
                            }
                        }
                    }
                }
            });
    }


    function renderFaultsByGroup(
        rows,
        groupProperty,
        canvasId) {

        const canvas =
            document.getElementById(
                canvasId);


        if (!canvas) {

            return;
        }


        const groups =
            groupRows(
                rows,
                groupProperty);


        const labels =
            Object.keys(groups)
                .sort();


        const datasets =
            faultDefinitions.map(
                function (fault) {

                    return {

                        label:
                            fault.label,

                        data:
                            labels.map(
                                function (label) {

                                    return sum(
                                        groups[label],
                                        fault.property);
                                }),

                        backgroundColor:
                            fault.color,

                        borderWidth: 0,

                        stack:
                            'faults'
                    };
                });


        new Chart(
            canvas,
            {
                type: 'bar',

                data: {
                    labels:
                        labels,

                    datasets:
                        datasets
                },

                options: {

                    indexAxis: 'y',

                    responsive: true,

                    maintainAspectRatio: false,

                    plugins: {

                        legend: {

                            position:
                                'bottom',

                            labels: {
                                boxWidth: 12
                            }
                        }
                    },

                    scales: {

                        x: {

                            stacked: true,

                            beginAtZero: true,

                            grid: {
                                color: '#edebe9'
                            }
                        },

                        y: {

                            stacked: true,

                            grid: {
                                display: false
                            },

                            ticks: {
                                autoSkip: false
                            }
                        }
                    }
                }
            });
    }


    function renderQualityTable(rows) {

        const tableBody =
            document.getElementById(
                'qualityTableBody');


        if (!tableBody) {

            return;
        }


        tableBody.innerHTML =
            rows.map(
                function (row) {

                    return `
                        <tr>

                            <td>
                                ${escapeHtml(
                        row.greigeProduct)}
                            </td>

                            <td>
                                ${escapeHtml(
                            row.machine)}
                            </td>

                            <td class="number">
                                ${number(
                                row.totalPieces,
                                0)}
                            </td>

                            <td class="number">
                                ${number(
                                    row.aGrade,
                                    0)}
                            </td>

                            <td class="number">
                                ${number(
                                        row.bGrade,
                                        0)}
                            </td>

                            <td class="number">
                                ${number(
                                            row.cGrade,
                                            0)}
                            </td>

                            <td class="number">
                                ${number(
                                                row.brokenNeedle,
                                                0)}
                            </td>

                            <td class="number">
                                ${number(
                                                    row.droppedStitches,
                                                    0)}
                            </td>

                            <td class="number">
                                ${number(
                                                        row.holes,
                                                        0)}
                            </td>

                            <td class="number">
                                ${number(
                                                            row.oilMarks,
                                                            0)}
                            </td>

                            <td class="number">
                                ${number(
                                                                row.pressOff,
                                                                0)}
                            </td>

                            <td class="number">
                                ${number(
                                                                    row.slubMarks,
                                                                    0)}
                            </td>

                            <td class="number">
                                ${number(
                                                                        row.thick,
                                                                        0)}
                            </td>

                            <td class="number">
                                ${number(
                                                                            row.thin,
                                                                            0)}
                            </td>

                            <td class="number">
                                ${number(
                                                                                row.totalFaults,
                                                                                0)}
                            </td>

                            <td class="number">
                                ${number(
                                                                                    row.averageFaultsPerPiece,
                                                                                    2)}
                            </td>

                        </tr>
                    `;
                })
                .join('');
    }



    // =========================================================
    // PROCESS LOSS
    // =========================================================

    function renderProcessLoss() {

        const orderRows =
            data.processLossOrders || [];


        const machineRows =
            data.processLossByMachine || [];


        const totalYarnConsumed =
            sum(
                orderRows,
                'yarnConsumed');


        const totalKnitted =
            sum(
                orderRows,
                'totalKnitted');


        let overallLoss = 0;


        if (totalYarnConsumed !== 0) {

            overallLoss =
                (
                    (totalKnitted
                        / totalYarnConsumed)
                    * 100
                )
                - 100;
        }


        setText(
            'processOrderCount',
            number(
                orderRows.length,
                0));


        setText(
            'processYarnConsumed',
            number(
                totalYarnConsumed,
                2));


        setText(
            'processTotalKnitted',
            number(
                totalKnitted,
                2));


        setText(
            'processOverallLoss',
            percentage(
                overallLoss));


        renderProcessLossChart(
            machineRows);


        renderProcessMachineTable(
            machineRows);


        renderProcessOrderTable(
            orderRows);
    }


    function renderProcessLossChart(rows) {

        const canvas =
            document.getElementById(
                'processLossChart');


        if (!canvas) {

            return;
        }


        const sortedRows =
            rows.slice()
                .sort(
                    function (a, b) {

                        const machineCompare =
                            String(a.machine)
                                .localeCompare(
                                    String(b.machine));


                        if (machineCompare !== 0) {

                            return machineCompare;
                        }


                        return String(a.product)
                            .localeCompare(
                                String(b.product));
                    });


        new Chart(
            canvas,
            {
                type: 'bar',

                data: {

                    labels:
                        sortedRows.map(
                            function (row) {

                                return row.machine;
                            }),

                    datasets: [
                        {
                            label:
                                'Process Loss %',

                            data:
                                sortedRows.map(
                                    function (row) {

                                        return row.processLoss;
                                    }),

                            backgroundColor:
                                sortedRows.map(
                                    function (row) {

                                        return getQualityColor(
                                            row.product);
                                    }),

                            borderWidth: 0,

                            borderRadius: 2
                        }
                    ]
                },

                options: {

                    responsive: true,

                    maintainAspectRatio: false,

                    plugins: {

                        legend: {
                            display: false
                        },

                        tooltip: {

                            callbacks: {

                                title:
                                    function (items) {

                                        if (!items
                                            || items.length === 0) {

                                            return '';
                                        }


                                        const row =
                                            sortedRows[
                                            items[0]
                                                .dataIndex];


                                        return row.machine;
                                    },

                                afterTitle:
                                    function (items) {

                                        if (!items
                                            || items.length === 0) {

                                            return '';
                                        }


                                        const row =
                                            sortedRows[
                                            items[0]
                                                .dataIndex];


                                        return row.product;
                                    },

                                label:
                                    function (context) {

                                        return 'Process Loss: '
                                            + percentage(
                                                context.raw);
                                    }
                            }
                        }
                    },

                    scales: {

                        x: {

                            grid: {
                                display: false
                            }
                        },

                        y: {

                            ticks: {

                                callback:
                                    function (value) {

                                        return value + '%';
                                    }
                            },

                            grid: {
                                color: '#edebe9'
                            }
                        }
                    }
                }
            });
    }


    function renderProcessMachineTable(rows) {

        const tableBody =
            document.getElementById(
                'processMachineTableBody');


        if (!tableBody) {

            return;
        }


        tableBody.innerHTML =
            rows.map(
                function (row) {

                    const qualityColor =
                        getQualityColor(
                            row.product);


                    return `
                        <tr>

                            <td>
                                ${escapeHtml(
                        row.machine)}
                            </td>

                            <td>
                                <span
                                    class="quality-swatch"
                                    style="background:${qualityColor}">
                                </span>

                                ${escapeHtml(
                            row.product)}
                            </td>

                            <td class="number">
                                ${number(
                                row.orderQty,
                                2)}
                            </td>

                            <td class="number">
                                ${number(
                                    row.yarnConsumed,
                                    2)}
                            </td>

                            <td class="number">
                                ${number(
                                        row.totalKnitted,
                                        2)}
                            </td>

                            <td class="number">
                                ${percentage(
                                            row.processLoss)}
                            </td>

                        </tr>
                    `;
                })
                .join('');
    }


    function renderProcessOrderTable(rows) {

        const tableBody =
            document.getElementById(
                'processOrderTableBody');


        if (!tableBody) {

            return;
        }


        tableBody.innerHTML =
            rows.map(
                function (row) {

                    const qualityColor =
                        getQualityColor(
                            row.product);


                    return `
                        <tr>

                            <td>
                                ${escapeHtml(
                        row.productionDate)}
                            </td>

                            <td>
                                ${escapeHtml(
                            row.orderNumber)}
                            </td>

                            <td>
                                ${escapeHtml(
                                row.machine)}
                            </td>

                            <td>
                                <span
                                    class="quality-swatch"
                                    style="background:${qualityColor}">
                                </span>

                                ${escapeHtml(
                                    row.product)}
                            </td>

                            <td>
                                ${escapeHtml(
                                        row.yarnType)}
                            </td>

                            <td class="number">
                                ${number(
                                            row.yarnTex,
                                            2)}
                            </td>

                            <td class="number">
                                ${number(
                                                row.yarnTwist,
                                                2)}
                            </td>

                            <td class="number">
                                ${number(
                                                    row.orderQty,
                                                    2)}
                            </td>

                            <td class="number">
                                ${number(
                                                        row.yarnConsumed,
                                                        2)}
                            </td>

                            <td class="number">
                                ${number(
                                                            row.totalKnitted,
                                                            2)}
                            </td>

                            <td class="number">
                                ${percentage(
                                                                row.processLoss)}
                            </td>

                        </tr>
                    `;
                })
                .join('');
    }



    // =========================================================
    // INITIALISE
    // =========================================================

    function initialise() {

        setText(
            'dashboardPeriod',
            (data.fromDate || '')
            + ' - '
            + (data.toDate || ''));


        initialiseTabs();


        renderDiskVariance();

        renderKnittingQuality();

        renderProcessLoss();
    }


    initialise();

})();
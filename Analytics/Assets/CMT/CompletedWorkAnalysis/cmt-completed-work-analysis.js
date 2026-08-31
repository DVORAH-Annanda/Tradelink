(function () {

    const data = window.cmtDashboardData;

    // ---------------------------------------------------------
    // Dashboard tab navigation
    // ---------------------------------------------------------

    const tabButtons =
        document.querySelectorAll(
            '.dashboard-tab-button');

    const tabPages =
        document.querySelectorAll(
            '.dashboard-tab-page');


    tabButtons.forEach(function (button) {

        button.addEventListener(
            'click',
            function () {

                const targetId =
                    button.getAttribute(
                        'data-tab');


                tabButtons.forEach(
                    function (item) {

                        item.classList
                            .remove('active');
                    });


                tabPages.forEach(
                    function (page) {

                        page.classList
                            .remove('active');
                    });


                button.classList
                    .add('active');


                const targetPage =
                    document.getElementById(
                        targetId);


                if (targetPage) {

                    targetPage.classList
                        .add('active');
                }
            });
    });

    if (!data) {
        return;
    }


    function formatNumber(value) {
        return Number(value || 0).toLocaleString();
    }


    function escapeHtml(value) {

        const div = document.createElement('div');

        div.textContent =
            value == null ? '' : value;

        return div.innerHTML;
    }


    // ---------------------------------------------------------
    // Header / summary cards
    // ---------------------------------------------------------

    document.getElementById('period')
        .textContent =
        data.fromDate + ' - ' + data.toDate;


    document.getElementById('totalUnits')
        .textContent =
        formatNumber(data.summary.totalUnits);


    document.getElementById('aGrade')
        .textContent =
        formatNumber(data.summary.aGrade);


    document.getElementById('bGrade')
        .textContent =
        formatNumber(data.summary.bGrade);


    document.getElementById('bGradePercentage')
        .textContent =
        Number(data.summary.bGradePercentage)
            .toFixed(2) + '%';



    // ---------------------------------------------------------
    // B Grade % by Style
    // ---------------------------------------------------------

    const styleRows =
        data.bGradeByStyle || [];


    const chartContainer =
        document.getElementById('styleChartContainer');


    if (styleRows.length > 0) {

        chartContainer.style.height =
            Math.max(
                420,
                styleRows.length * 32
            ) + 'px';


        new Chart(
            document.getElementById(
                'bGradeByStyleChart'),

            {
                type: 'bar',

                data: {

                    labels:
                        styleRows.map(
                            row => row.style),

                    datasets: [
                        {
                            label: 'B Grade %',

                            data:
                                styleRows.map(
                                    row =>
                                        row.bGradePercentage),

                            borderWidth: 1
                        }
                    ]
                },

                options: {

                    indexAxis: 'y',

                    responsive: true,

                    maintainAspectRatio: false,

                    scales: {

                        x: {

                            beginAtZero: true,

                            title: {
                                display: true,
                                text: 'B Grade %'
                            },

                            ticks: {

                                callback:
                                    function (value) {
                                        return value + '%';
                                    }
                            }
                        },

                        y: {

                            ticks: {
                                autoSkip: false
                            }
                        }
                    },

                    plugins: {

                        legend: {
                            display: false
                        },

                        tooltip: {

                            callbacks: {

                                label:
                                    function (context) {

                                        return 'B Grade: ' +
                                            context.parsed.x
                                                .toFixed(2) +
                                            '%';
                                    },

                                afterLabel:
                                    function (context) {

                                        const row =
                                            styleRows[
                                            context.dataIndex];

                                        return [
                                            'Total Units: ' +
                                            formatNumber(
                                                row.totalUnits),

                                            'A Grade: ' +
                                            formatNumber(
                                                row.aGrade),

                                            'B Grade: ' +
                                            formatNumber(
                                                row.bGrade)
                                        ];
                                    }
                            }
                        }
                    }
                }
            });

    }
    else {

        chartContainer.innerHTML =
            '<div class="empty-message">' +
            'No style data was found for this period.' +
            '</div>';
    }



    // ---------------------------------------------------------
    // Style detail table
    // ---------------------------------------------------------

    const tableBody =
        document.getElementById(
            'styleTableBody');


    let totalUnits = 0;
    let totalAGrade = 0;
    let totalBGrade = 0;

    let maxPercentage = 0;


    styleRows.forEach(function (row) {

        if (row.bGradePercentage >
            maxPercentage) {

            maxPercentage =
                row.bGradePercentage;
        }
    });


    if (maxPercentage <= 0) {
        maxPercentage = 1;
    }


    styleRows.forEach(function (row) {

        totalUnits +=
            Number(row.totalUnits || 0);

        totalAGrade +=
            Number(row.aGrade || 0);

        totalBGrade +=
            Number(row.bGrade || 0);


        const percentage =
            Number(
                row.bGradePercentage || 0);


        const barWidth =
            Math.min(
                100,
                Math.max(
                    0,
                    (percentage /
                        maxPercentage) *
                    100));


        const tr =
            document.createElement('tr');


        tr.innerHTML =

            '<td class="style-name">' +
            escapeHtml(row.style) +
            '</td>' +

            '<td class="number">' +
            formatNumber(
                row.totalUnits) +
            '</td>' +

            '<td class="number">' +
            formatNumber(
                row.aGrade) +
            '</td>' +

            '<td class="number">' +
            formatNumber(
                row.bGrade) +
            '</td>' +

            '<td class="number">' +

            '<div class="percent-wrapper">' +

            '<div class="percent-bar-track">' +

            '<div class="percent-bar" ' +
            'style="width:' +
            barWidth.toFixed(1) +
            '%">' +
            '</div>' +

            '</div>' +

            '<span class="percent-text">' +
            percentage.toFixed(2) +
            '%' +
            '</span>' +

            '</div>' +

            '</td>';


        tableBody.appendChild(tr);
    });


    const totalGrades =
        totalAGrade + totalBGrade;


    const totalPercentage =
        totalGrades === 0
            ? 0
            : (totalBGrade /
                totalGrades) * 100;


    document.getElementById(
        'styleTotalUnits')
        .textContent =
        formatNumber(totalUnits);


    document.getElementById(
        'styleTotalAGrade')
        .textContent =
        formatNumber(totalAGrade);


    document.getElementById(
        'styleTotalBGrade')
        .textContent =
        formatNumber(totalBGrade);


    document.getElementById(
        'styleTotalPercentage')
        .textContent =
        totalPercentage.toFixed(2) +
        '%';

    // ---------------------------------------------------------
    // MNFF & Ospec by Style
    // ---------------------------------------------------------

    const mnffRows =
        data.mnffAndOspec || [];


    const mnffTableBody =
        document.getElementById(
            'mnffOspecTableBody');


    let mnffTotalUnits = 0;
    let mnffTotal = 0;
    let ospecTotal = 0;

    let maxMnff = 0;
    let maxOspec = 0;


    mnffRows.forEach(function (row) {

        if (Number(row.mnff) > maxMnff) {

            maxMnff =
                Number(row.mnff);
        }

        if (Number(row.ospec) > maxOspec) {

            maxOspec =
                Number(row.ospec);
        }
    });


    if (maxMnff <= 0) {
        maxMnff = 1;
    }

    if (maxOspec <= 0) {
        maxOspec = 1;
    }


    mnffRows.forEach(function (row) {

        const totalUnits =
            Number(row.totalUnits || 0);

        const mnff =
            Number(row.mnff || 0);

        const ospec =
            Number(row.ospec || 0);


        mnffTotalUnits +=
            totalUnits;

        mnffTotal +=
            mnff;

        ospecTotal +=
            ospec;


        const mnffBarWidth =
            Math.min(
                100,
                (mnff / maxMnff) * 100);


        const ospecBarWidth =
            Math.min(
                100,
                (ospec / maxOspec) * 100);


        const tr =
            document.createElement('tr');


        tr.innerHTML =

            '<td class="style-name">' +
            escapeHtml(row.style) +
            '</td>' +


            '<td class="number">' +
            formatNumber(totalUnits) +
            '</td>' +


            '<td class="number">' +

            '<div class="defect-wrapper">' +

            '<div class="defect-bar-track">' +

            '<div class="defect-bar" ' +
            'style="width:' +
            mnffBarWidth.toFixed(1) +
            '%">' +
            '</div>' +

            '</div>' +

            '<span class="defect-value">' +
            formatNumber(mnff) +
            '</span>' +

            '</div>' +

            '</td>' +


            '<td class="number">' +

            '<div class="defect-wrapper">' +

            '<div class="defect-bar-track">' +

            '<div class="defect-bar ospec" ' +
            'style="width:' +
            ospecBarWidth.toFixed(1) +
            '%">' +
            '</div>' +

            '</div>' +

            '<span class="defect-value">' +
            formatNumber(ospec) +
            '</span>' +

            '</div>' +

            '</td>';


        mnffTableBody.appendChild(tr);
    });


    document.getElementById(
        'mnffTotalUnits')
        .textContent =
        formatNumber(
            mnffTotalUnits);


    document.getElementById(
        'mnffTotal')
        .textContent =
        formatNumber(
            mnffTotal);


    document.getElementById(
        'ospecTotal')
        .textContent =
        formatNumber(
            ospecTotal);

   
    // ---------------------------------------------------------
    // B Grade Holes by Knitting Machine
    // ---------------------------------------------------------

    const holesMachineRows =
        data.holesByMachine || [];


    // ---------------------------------------------------------
    // B Grade Holes - Detail Table
    // ---------------------------------------------------------

    const holesMachineTableBody =
        document.getElementById(
            'holesMachineTableBody');


    let holesMachineTotalUnits = 0;
    let holesMachineTotalHoles = 0;

    let maxHolePercentage = 0;


    holesMachineRows.forEach(
        function (row) {

            if (Number(row.holesPercentage) >
                maxHolePercentage) {

                maxHolePercentage =
                    Number(
                        row.holesPercentage);
            }
        });


    if (maxHolePercentage <= 0) {
        maxHolePercentage = 1;
    }


    holesMachineRows.forEach(
        function (row) {

            const totalUnits =
                Number(
                    row.totalUnits || 0);

            const holes =
                Number(
                    row.holes || 0);

            const percentage =
                Number(
                    row.holesPercentage || 0);


            holesMachineTotalUnits +=
                totalUnits;

            holesMachineTotalHoles +=
                holes;


            const barWidth =
                Math.min(
                    100,
                    Math.max(
                        0,
                        (percentage /
                            maxHolePercentage)
                        * 100));


            const tr =
                document.createElement('tr');


            tr.innerHTML =

                '<td class="style-name">' +
                escapeHtml(row.machine) +
                '</td>' +

                '<td class="number">' +
                formatNumber(totalUnits) +
                '</td>' +

                '<td class="number">' +
                formatNumber(holes) +
                '</td>' +

                '<td class="number">' +

                '<div class="percent-wrapper">' +

                '<div class="percent-bar-track">' +

                '<div class="percent-bar" ' +
                'style="width:' +
                barWidth.toFixed(1) +
                '%">' +
                '</div>' +

                '</div>' +

                '<span class="percent-text">' +
                percentage.toFixed(2) +
                '%' +
                '</span>' +

                '</div>' +

                '</td>';


            holesMachineTableBody
                .appendChild(tr);
        });


    const holesMachineTotalPercentage =
        holesMachineTotalUnits === 0
            ? 0
            : (
                holesMachineTotalHoles /
                holesMachineTotalUnits
            ) * 100;


    document.getElementById(
        'holesMachineTotalUnits')
        .textContent =
        formatNumber(
            holesMachineTotalUnits);


    document.getElementById(
        'holesMachineTotalHoles')
        .textContent =
        formatNumber(
            holesMachineTotalHoles);


    document.getElementById(
        'holesMachineTotalPercentage')
        .textContent =
        holesMachineTotalPercentage
            .toFixed(2) + '%';


    // ---------------------------------------------------------
    // B Grade Holes - Chart
    // ---------------------------------------------------------

    const holesMachineChartContainer =
        document.getElementById(
            'holesMachineChartContainer');


    if (holesMachineRows.length > 0) {

        new Chart(
            document.getElementById(
                'holesMachineChart'),

            {
                type: 'bar',

                data: {

                    labels:
                        holesMachineRows.map(
                            row => row.machine),

                    datasets: [
                        {
                            label:
                                'B Grade Holes %',

                            data:
                                holesMachineRows.map(
                                    row =>
                                        row.holesPercentage),

                            borderWidth: 1
                        }
                    ]
                },

                options: {

                    responsive: true,

                    maintainAspectRatio: false,

                    scales: {

                        y: {

                            beginAtZero: true,

                            title: {
                                display: true,
                                text: 'B Grade Holes %'
                            },

                            ticks: {

                                callback:
                                    function (value) {

                                        return value + '%';
                                    }
                            }
                        },

                        x: {

                            title: {
                                display: true,
                                text: 'Knitting Machine'
                            }
                        }
                    },

                    plugins: {

                        legend: {
                            display: false
                        },

                        tooltip: {

                            callbacks: {

                                label:
                                    function (context) {

                                        return (
                                            'B Grade Holes: ' +
                                            context.parsed.y
                                                .toFixed(2) +
                                            '%'
                                        );
                                    },

                                afterLabel:
                                    function (context) {

                                        const row =
                                            holesMachineRows[
                                            context.dataIndex];

                                        return [
                                            'Total Units: ' +
                                            formatNumber(
                                                row.totalUnits),

                                            'Holes: ' +
                                            formatNumber(
                                                row.holes)
                                        ];
                                    }
                            }
                        }
                    }
                }
            });

    }
    else {

        holesMachineChartContainer
            .innerHTML =

            '<div class="empty-message">' +
            'No knitting machine data was found for this period.' +
            '</div>';
    }

    // ---------------------------------------------------------
    // B Grades - Spinning
    // ---------------------------------------------------------

    const spinningRows =
        data.spinningByYarnType || [];


    // ---------------------------------------------------------
    // Spinning Detail Table
    // ---------------------------------------------------------

    const spinningTableBody =
        document.getElementById(
            'spinningTableBody');


    let spinningTotalUnits = 0;
    let spinningTotalBarre = 0;
    let spinningTotalFflaw = 0;
    let spinningTotalContam = 0;

    let maxSpinningPercentage = 0;


    spinningRows.forEach(
        function (row) {

            const percentage =
                Number(
                    row.spinningBGradePercentage || 0);

            if (percentage >
                maxSpinningPercentage) {

                maxSpinningPercentage =
                    percentage;
            }
        });


    if (maxSpinningPercentage <= 0) {
        maxSpinningPercentage = 1;
    }


    spinningRows.forEach(
        function (row) {

            const totalUnits =
                Number(row.totalUnits || 0);

            const barre =
                Number(row.barreLines || 0);

            const fflaw =
                Number(row.fflaw || 0);

            const contam =
                Number(row.contam || 0);

            const percentage =
                Number(
                    row.spinningBGradePercentage || 0);


            spinningTotalUnits +=
                totalUnits;

            spinningTotalBarre +=
                barre;

            spinningTotalFflaw +=
                fflaw;

            spinningTotalContam +=
                contam;


            const barWidth =
                Math.min(
                    100,
                    Math.max(
                        0,
                        (percentage /
                            maxSpinningPercentage)
                        * 100));


            const tr =
                document.createElement('tr');


            tr.innerHTML =

                '<td class="style-name">' +
                escapeHtml(row.yarnType) +
                '</td>' +

                '<td class="number">' +
                formatNumber(totalUnits) +
                '</td>' +

                '<td class="number">' +
                formatNumber(barre) +
                '</td>' +

                '<td class="number">' +
                formatNumber(fflaw) +
                '</td>' +

                '<td class="number">' +
                formatNumber(contam) +
                '</td>' +

                '<td class="number">' +

                '<div class="percent-wrapper">' +

                '<div class="percent-bar-track">' +

                '<div class="percent-bar" ' +
                'style="width:' +
                barWidth.toFixed(1) +
                '%">' +
                '</div>' +

                '</div>' +

                '<span class="percent-text">' +

                percentage.toFixed(2) +
                '%' +

                '</span>' +

                '</div>' +

                '</td>';


            spinningTableBody
                .appendChild(tr);
        });


    const spinningTotalDefects =
        spinningTotalBarre +
        spinningTotalFflaw +
        spinningTotalContam;


    const spinningTotalPercentage =
        spinningTotalUnits === 0
            ? 0
            : (
                spinningTotalDefects /
                spinningTotalUnits
            ) * 100;


    document.getElementById(
        'spinningTotalUnits')
        .textContent =
        formatNumber(
            spinningTotalUnits);


    document.getElementById(
        'spinningTotalBarre')
        .textContent =
        formatNumber(
            spinningTotalBarre);


    document.getElementById(
        'spinningTotalFflaw')
        .textContent =
        formatNumber(
            spinningTotalFflaw);


    document.getElementById(
        'spinningTotalContam')
        .textContent =
        formatNumber(
            spinningTotalContam);


    document.getElementById(
        'spinningTotalPercentage')
        .textContent =
        spinningTotalPercentage
            .toFixed(2) + '%';

    // ---------------------------------------------------------
    // Spinning Defect Count Chart
    // ---------------------------------------------------------

    const spinningDefectChartContainer =
        document.getElementById(
            'spinningDefectChartContainer');


    if (spinningRows.length > 0) {

        new Chart(
            document.getElementById(
                'spinningDefectChart'),

            {
                type: 'bar',

                data: {

                    labels:
                        spinningRows.map(
                            row =>
                                row.yarnType),

                    datasets: [

                        {
                            label:
                                'Barre Lines',

                            data:
                                spinningRows.map(
                                    row =>
                                        row.barreLines),

                            borderWidth: 1
                        },

                        {
                            label:
                                'Fflaw',

                            data:
                                spinningRows.map(
                                    row =>
                                        row.fflaw),

                            borderWidth: 1
                        },

                        {
                            label:
                                'Contam',

                            data:
                                spinningRows.map(
                                    row =>
                                        row.contam),

                            borderWidth: 1
                        }
                    ]
                },

                options: {

                    indexAxis: 'y',

                    responsive: true,

                    maintainAspectRatio: false,

                    scales: {

                        x: {
                            beginAtZero: true,

                            title: {
                                display: true,
                                text: 'Defect Count'
                            }
                        },

                        y: {
                            ticks: {
                                autoSkip: false
                            }
                        }
                    },

                    plugins: {

                        legend: {
                            display: true,
                            position: 'top'
                        },

                        tooltip: {

                            callbacks: {

                                afterBody:
                                    function (items) {

                                        if (!items ||
                                            items.length === 0) {
                                            return '';
                                        }

                                        const row =
                                            spinningRows[
                                            items[0].dataIndex];

                                        return (
                                            'Total Units: ' +
                                            formatNumber(
                                                row.totalUnits)
                                        );
                                    }
                            }
                        }
                    }
                }
            });

    }
    else {

        spinningDefectChartContainer
            .innerHTML =

            '<div class="empty-message">' +
            'No spinning data was found for this period.' +
            '</div>';
    }

    // ---------------------------------------------------------
    // Spinning 100% Defect Mix Chart
    // ---------------------------------------------------------

    const spinningMixChartContainer =
        document.getElementById(
            'spinningMixChartContainer');


    const spinningMixRows =
        spinningRows.map(
            function (row) {

                const barre =
                    Number(
                        row.barreLines || 0);

                const fflaw =
                    Number(
                        row.fflaw || 0);

                const contam =
                    Number(
                        row.contam || 0);

                const total =
                    barre +
                    fflaw +
                    contam;


                return {

                    yarnType:
                        row.yarnType,

                    barrePercentage:
                        total === 0
                            ? 0
                            : (barre /
                                total) * 100,

                    fflawPercentage:
                        total === 0
                            ? 0
                            : (fflaw /
                                total) * 100,

                    contamPercentage:
                        total === 0
                            ? 0
                            : (contam /
                                total) * 100
                };
            });


    if (spinningMixRows.length > 0) {

        new Chart(
            document.getElementById(
                'spinningMixChart'),

            {
                type: 'bar',

                data: {

                    labels:
                        spinningMixRows.map(
                            row =>
                                row.yarnType),

                    datasets: [

                        {
                            label:
                                'Barre Lines',

                            data:
                                spinningMixRows.map(
                                    row =>
                                        row.barrePercentage),

                            stack:
                                'spinning'
                        },

                        {
                            label:
                                'Fflaw',

                            data:
                                spinningMixRows.map(
                                    row =>
                                        row.fflawPercentage),

                            stack:
                                'spinning'
                        },

                        {
                            label:
                                'Contam',

                            data:
                                spinningMixRows.map(
                                    row =>
                                        row.contamPercentage),

                            stack:
                                'spinning'
                        }
                    ]
                },

                options: {

                    responsive: true,

                    maintainAspectRatio: false,

                    scales: {

                        x: {
                            stacked: true
                        },

                        y: {

                            stacked: true,

                            beginAtZero: true,

                            min: 0,
                            max: 100,

                            title: {
                                display: true,
                                text: 'Defect Mix %'
                            },

                            ticks: {

                                callback:
                                    function (value) {

                                        return value + '%';
                                    }
                            }
                        }
                    },

                    plugins: {

                        legend: {
                            display: true,
                            position: 'top'
                        },

                        tooltip: {

                            callbacks: {

                                label:
                                    function (context) {

                                        return (
                                            context.dataset.label +
                                            ': ' +
                                            context.parsed.y
                                                .toFixed(1) +
                                            '%'
                                        );
                                    }
                            }
                        }
                    }
                }
            });

    }
    else {

        spinningMixChartContainer
            .innerHTML =

            '<div class="empty-message">' +
            'No spinning defect mix data was found.' +
            '</div>';
    }

    // ---------------------------------------------------------
    // B Grades - Knitting
    // ---------------------------------------------------------

    const knittingRows =
        data.knittingByMachine || [];


    // ---------------------------------------------------------
    // Knitting Detail Table
    // ---------------------------------------------------------

    const knittingTableBody =
        document.getElementById(
            'knittingTableBody');


    let knittingTotalUnits = 0;
    let knittingTotalTwisting = 0;
    let knittingTotalHoles = 0;
    let knittingTotalOilMarks = 0;
    let knittingTotalNeedleLines = 0;

    let maxKnittingPercentage = 0;


    knittingRows.forEach(
        function (row) {

            const percentage =
                Number(
                    row.knittingBGradePercentage || 0);

            if (percentage >
                maxKnittingPercentage) {

                maxKnittingPercentage =
                    percentage;
            }
        });


    if (maxKnittingPercentage <= 0) {
        maxKnittingPercentage = 1;
    }


    knittingRows.forEach(
        function (row) {

            const totalUnits =
                Number(row.totalUnits || 0);

            const twisting =
                Number(row.twisting || 0);

            const holes =
                Number(row.holes || 0);

            const oilMarks =
                Number(row.oilMarks || 0);

            const needleLines =
                Number(row.needleLines || 0);

            const percentage =
                Number(
                    row.knittingBGradePercentage || 0);


            knittingTotalUnits +=
                totalUnits;

            knittingTotalTwisting +=
                twisting;

            knittingTotalHoles +=
                holes;

            knittingTotalOilMarks +=
                oilMarks;

            knittingTotalNeedleLines +=
                needleLines;


            const barWidth =
                Math.min(
                    100,
                    Math.max(
                        0,
                        (percentage /
                            maxKnittingPercentage)
                        * 100));


            const tr =
                document.createElement('tr');


            tr.innerHTML =

                '<td class="style-name">' +
                escapeHtml(row.machine) +
                '</td>' +

                '<td class="number">' +
                formatNumber(totalUnits) +
                '</td>' +

                '<td class="number">' +
                formatNumber(twisting) +
                '</td>' +

                '<td class="number">' +
                formatNumber(holes) +
                '</td>' +

                '<td class="number">' +
                formatNumber(oilMarks) +
                '</td>' +

                '<td class="number">' +
                formatNumber(needleLines) +
                '</td>' +

                '<td class="number">' +

                '<div class="percent-wrapper">' +

                '<div class="percent-bar-track">' +

                '<div class="percent-bar" ' +
                'style="width:' +
                barWidth.toFixed(1) +
                '%">' +
                '</div>' +

                '</div>' +

                '<span class="percent-text">' +
                percentage.toFixed(2) +
                '%' +
                '</span>' +

                '</div>' +

                '</td>';


            knittingTableBody
                .appendChild(tr);
        });

    const knittingTotalDefects =
        knittingTotalTwisting +
        knittingTotalHoles +
        knittingTotalOilMarks +
        knittingTotalNeedleLines;


    const knittingTotalPercentage =
        knittingTotalUnits === 0
            ? 0
            : (
                knittingTotalDefects /
                knittingTotalUnits
            ) * 100;


    document.getElementById(
        'knittingTotalUnits')
        .textContent =
        formatNumber(
            knittingTotalUnits);


    document.getElementById(
        'knittingTotalTwisting')
        .textContent =
        formatNumber(
            knittingTotalTwisting);


    document.getElementById(
        'knittingTotalHoles')
        .textContent =
        formatNumber(
            knittingTotalHoles);


    document.getElementById(
        'knittingTotalOilMarks')
        .textContent =
        formatNumber(
            knittingTotalOilMarks);


    document.getElementById(
        'knittingTotalNeedleLines')
        .textContent =
        formatNumber(
            knittingTotalNeedleLines);


    document.getElementById(
        'knittingTotalPercentage')
        .textContent =
        knittingTotalPercentage
            .toFixed(2) + '%';

    // ---------------------------------------------------------
    // Knitting Defect Count Chart
    // ---------------------------------------------------------

    const knittingDefectChartContainer =
        document.getElementById(
            'knittingDefectChartContainer');


    if (knittingRows.length > 0) {

        new Chart(
            document.getElementById(
                'knittingDefectChart'),

            {
                type: 'bar',

                data: {

                    labels:
                        knittingRows.map(
                            row =>
                                row.machine),

                    datasets: [

                        {
                            label:
                                'Twisting',

                            data:
                                knittingRows.map(
                                    row =>
                                        row.twisting),

                            borderWidth: 1
                        },

                        {
                            label:
                                'Holes',

                            data:
                                knittingRows.map(
                                    row =>
                                        row.holes),

                            borderWidth: 1
                        },

                        {
                            label:
                                'Oil Marks',

                            data:
                                knittingRows.map(
                                    row =>
                                        row.oilMarks),

                            borderWidth: 1
                        },

                        {
                            label:
                                'Needle Lines',

                            data:
                                knittingRows.map(
                                    row =>
                                        row.needleLines),

                            borderWidth: 1
                        }
                    ]
                },


                options: {

                    indexAxis: 'y',

                    responsive: true,

                    maintainAspectRatio: false,


                    scales: {

                        x: {

                            beginAtZero: true,

                            title: {
                                display: true,
                                text: 'Defect Count'
                            }
                        },

                        y: {

                            ticks: {
                                autoSkip: false
                            }
                        }
                    },


                    plugins: {

                        legend: {
                            display: true,
                            position: 'top'
                        },

                        tooltip: {

                            callbacks: {

                                afterBody:
                                    function (items) {

                                        if (!items ||
                                            items.length === 0) {
                                            return '';
                                        }

                                        const row =
                                            knittingRows[
                                            items[0].dataIndex];

                                        return (
                                            'Total Units: ' +
                                            formatNumber(
                                                row.totalUnits)
                                        );
                                    }
                            }
                        }
                    }
                }
            });

    }
    else {

        knittingDefectChartContainer
            .innerHTML =

            '<div class="empty-message">' +
            'No knitting data was found for this period.' +
            '</div>';
    }

    // ---------------------------------------------------------
    // Knitting Defect Mix
    // ---------------------------------------------------------

    const knittingMixChartContainer =
        document.getElementById(
            'knittingMixChartContainer');


    const knittingMixRows =
        knittingRows.map(
            function (row) {

                const twisting =
                    Number(row.twisting || 0);

                const holes =
                    Number(row.holes || 0);

                const oilMarks =
                    Number(row.oilMarks || 0);

                const needleLines =
                    Number(row.needleLines || 0);


                const total =
                    twisting +
                    holes +
                    oilMarks +
                    needleLines;


                return {

                    machine:
                        row.machine,

                    twistingPercentage:
                        total === 0
                            ? 0
                            : (twisting /
                                total) * 100,

                    holesPercentage:
                        total === 0
                            ? 0
                            : (holes /
                                total) * 100,

                    oilMarksPercentage:
                        total === 0
                            ? 0
                            : (oilMarks /
                                total) * 100,

                    needleLinesPercentage:
                        total === 0
                            ? 0
                            : (needleLines /
                                total) * 100
                };
            });


    if (knittingMixRows.length > 0) {

        new Chart(
            document.getElementById(
                'knittingMixChart'),

            {
                type: 'bar',

                data: {

                    labels:
                        knittingMixRows.map(
                            row =>
                                row.machine),

                    datasets: [

                        {
                            label: 'Twisting',

                            data:
                                knittingMixRows.map(
                                    row =>
                                        row.twistingPercentage),

                            stack: 'knitting'
                        },

                        {
                            label: 'Holes',

                            data:
                                knittingMixRows.map(
                                    row =>
                                        row.holesPercentage),

                            stack: 'knitting'
                        },

                        {
                            label: 'Oil Marks',

                            data:
                                knittingMixRows.map(
                                    row =>
                                        row.oilMarksPercentage),

                            stack: 'knitting'
                        },

                        {
                            label: 'Needle Lines',

                            data:
                                knittingMixRows.map(
                                    row =>
                                        row.needleLinesPercentage),

                            stack: 'knitting'
                        }
                    ]
                },


                options: {

                    indexAxis: 'y',

                    responsive: true,

                    maintainAspectRatio: false,


                    scales: {

                        x: {

                            stacked: true,

                            min: 0,
                            max: 100,

                            title: {
                                display: true,
                                text: 'Defect Mix %'
                            },

                            ticks: {

                                callback:
                                    function (value) {

                                        return value + '%';
                                    }
                            }
                        },

                        y: {

                            stacked: true,

                            ticks: {
                                autoSkip: false
                            }
                        }
                    },


                    plugins: {

                        legend: {
                            display: true,
                            position: 'top'
                        },

                        tooltip: {

                            callbacks: {

                                label:
                                    function (context) {

                                        return (
                                            context.dataset.label +
                                            ': ' +
                                            context.parsed.x
                                                .toFixed(1) +
                                            '%'
                                        );
                                    }
                            }
                        }
                    }
                }
            });

    }
    else {

        knittingMixChartContainer
            .innerHTML =

            '<div class="empty-message">' +
            'No knitting defect mix data was found.' +
            '</div>';
    }

    // ---------------------------------------------------------
    // B Grades - Dyeing
    // ---------------------------------------------------------

    const dyeingRows =
        data.dyeingByStyleQuality || [];

    const dyeingStyleMap = {};


    dyeingRows.forEach(function (row) {

        const style =
            row.style || '(Unknown Style)';


        if (!dyeingStyleMap[style]) {

            dyeingStyleMap[style] = {

                style: style,

                totalUnits: 0,

                stains: 0,

                ospec: 0,

                shading: 0
            };
        }


        dyeingStyleMap[style].totalUnits +=
            Number(row.totalUnits || 0);

        dyeingStyleMap[style].stains +=
            Number(row.stains || 0);

        dyeingStyleMap[style].ospec +=
            Number(row.ospec || 0);

        dyeingStyleMap[style].shading +=
            Number(row.shading || 0);
    });


    const dyeingStyleRows =
        Object.keys(dyeingStyleMap)
            .map(function (key) {
                return dyeingStyleMap[key];
            });

    const dyeingTableBody =
        document.getElementById(
            'dyeingTableBody');


    let dyeingTotalUnits = 0;
    let dyeingTotalStains = 0;
    let dyeingTotalOspec = 0;
    let dyeingTotalShading = 0;

    let maxDyeingPercentage = 0;


    dyeingRows.forEach(function (row) {

        const percentage =
            Number(
                row.dyeingBGradePercentage || 0);

        if (percentage >
            maxDyeingPercentage) {

            maxDyeingPercentage =
                percentage;
        }
    });


    if (maxDyeingPercentage <= 0) {
        maxDyeingPercentage = 1;
    }


    dyeingRows.forEach(function (row) {

        const totalUnits =
            Number(row.totalUnits || 0);

        const stains =
            Number(row.stains || 0);

        const ospec =
            Number(row.ospec || 0);

        const shading =
            Number(row.shading || 0);

        const percentage =
            Number(
                row.dyeingBGradePercentage || 0);


        dyeingTotalUnits +=
            totalUnits;

        dyeingTotalStains +=
            stains;

        dyeingTotalOspec +=
            ospec;

        dyeingTotalShading +=
            shading;


        const barWidth =
            Math.min(
                100,
                Math.max(
                    0,
                    (percentage /
                        maxDyeingPercentage)
                    * 100));


        const tr =
            document.createElement('tr');


        tr.innerHTML =

        '<td class="style-name">' +
        escapeHtml(row.style) +
        '</td>' +

        '<td>' +
        escapeHtml(row.greigeQuality || '') +
        '</td>' +

            '<td class="number">' +
            formatNumber(totalUnits) +
            '</td>' +

            '<td class="number">' +
            formatNumber(stains) +
            '</td>' +

            '<td class="number">' +
            formatNumber(ospec) +
            '</td>' +

            '<td class="number">' +
            formatNumber(shading) +
            '</td>' +

            '<td class="number">' +

            '<div class="percent-wrapper">' +

            '<div class="percent-bar-track">' +

            '<div class="percent-bar" ' +
            'style="width:' +
            barWidth.toFixed(1) +
            '%">' +
            '</div>' +

            '</div>' +

            '<span class="percent-text">' +
            percentage.toFixed(2) +
            '%' +
            '</span>' +

            '</div>' +

            '</td>';


        dyeingTableBody
            .appendChild(tr);
    });


    const dyeingTotalDefects =
        dyeingTotalStains +
        dyeingTotalOspec +
        dyeingTotalShading;


    const dyeingTotalPercentage =
        dyeingTotalUnits === 0
            ? 0
            : (
                dyeingTotalDefects /
                dyeingTotalUnits
            ) * 100;


    document.getElementById(
        'dyeingTotalUnits')
        .textContent =
        formatNumber(dyeingTotalUnits);

    document.getElementById(
        'dyeingTotalStains')
        .textContent =
        formatNumber(dyeingTotalStains);

    document.getElementById(
        'dyeingTotalOspec')
        .textContent =
        formatNumber(dyeingTotalOspec);

    document.getElementById(
        'dyeingTotalShading')
        .textContent =
        formatNumber(dyeingTotalShading);

    document.getElementById(
        'dyeingTotalPercentage')
        .textContent =
        dyeingTotalPercentage
            .toFixed(2) + '%';

    const dyeingDefectChartContainer =
        document.getElementById(
            'dyeingDefectChartContainer');


    if (dyeingRows.length > 0) {

        new Chart(
            document.getElementById(
                'dyeingDefectChart'),

            {
                type: 'bar',

                data: {

                    labels:
                        dyeingStyleRows.map(
                            row => row.style),

                    datasets: [
                        {
                            label: 'Stains',

                            data:
                                dyeingStyleRows.map(
                                    row => row.stains)
                        },

                        {
                            label: 'Ospec',

                            data:
                                dyeingStyleRows.map(
                                    row => row.ospec)
                        },

                        {
                            label: 'Shading',

                            data:
                                dyeingStyleRows.map(
                                    row => row.shading)
                        }
                    ]
                },

                options: {

                    indexAxis: 'y',

                    responsive: true,

                    maintainAspectRatio: false,

                    scales: {

                        x: {
                            beginAtZero: true,

                            title: {
                                display: true,
                                text: 'Defect Count'
                            }
                        },

                        y: {
                            ticks: {
                                autoSkip: false
                            }
                        }
                    },

                    plugins: {

                        legend: {
                            display: true,
                            position: 'top'
                        }
                    }
                }
            });

    }
    else {

        dyeingDefectChartContainer
            .innerHTML =

            '<div class="empty-message">' +
            'No dyeing data was found for this period.' +
            '</div>';
    }

    const dyeingMixRows =
        dyeingStyleRows.map(function (row) {

            const stains =
                Number(row.stains || 0);

            const ospec =
                Number(row.ospec || 0);

            const shading =
                Number(row.shading || 0);

            const total =
                stains +
                ospec +
                shading;


            return {

                style:
                    row.style,

                stainsPercentage:
                    total === 0
                        ? 0
                        : stains / total * 100,

                ospecPercentage:
                    total === 0
                        ? 0
                        : ospec / total * 100,

                shadingPercentage:
                    total === 0
                        ? 0
                        : shading / total * 100
            };
        });


    new Chart(
        document.getElementById(
            'dyeingMixChart'),

        {
            type: 'bar',

            data: {

                labels:
                    dyeingMixRows.map(
                        row =>
                            row.style),

                datasets: [

                    {
                        label: 'Stains',

                        data:
                            dyeingMixRows.map(
                                row =>
                                    row.stainsPercentage),

                        stack: 'dyeing'
                    },

                    {
                        label: 'Ospec',

                        data:
                            dyeingMixRows.map(
                                row =>
                                    row.ospecPercentage),

                        stack: 'dyeing'
                    },

                    {
                        label: 'Shading',

                        data:
                            dyeingMixRows.map(
                                row =>
                                    row.shadingPercentage),

                        stack: 'dyeing'
                    }
                ]
            },

            options: {

                indexAxis: 'y',

                responsive: true,

                maintainAspectRatio: false,

                scales: {

                    x: {
                        stacked: true,
                        min: 0,
                        max: 100,

                        ticks: {
                            callback:
                                value =>
                                    value + '%'
                        }
                    },

                    y: {
                        stacked: true
                    }
                }
            }
        });

    // ---------------------------------------------------------
    // B Grades - Cutting
    // ---------------------------------------------------------

    const cuttingRows =
        data.cuttingByStyle || [];


    const cuttingTableBody =
        document.getElementById(
            'cuttingTableBody');


    let cuttingTotalUnits = 0;
    let cuttingTotalMNFF = 0;
    let cuttingTotalPoorCutting = 0;

    let maxCuttingPercentage = 0;


    cuttingRows.forEach(function (row) {

        const percentage =
            Number(
                row.cuttingBGradePercentage || 0);

        if (percentage >
            maxCuttingPercentage) {

            maxCuttingPercentage =
                percentage;
        }
    });


    if (maxCuttingPercentage <= 0) {
        maxCuttingPercentage = 1;
    }


    cuttingRows.forEach(function (row) {

        const totalUnits =
            Number(row.totalUnits || 0);

        const mnff =
            Number(row.mnff || 0);

        const poorCutting =
            Number(row.poorCutting || 0);

        const percentage =
            Number(
                row.cuttingBGradePercentage || 0);


        cuttingTotalUnits +=
            totalUnits;

        cuttingTotalMNFF +=
            mnff;

        cuttingTotalPoorCutting +=
            poorCutting;


        const barWidth =
            Math.min(
                100,
                Math.max(
                    0,
                    percentage /
                    maxCuttingPercentage *
                    100));


        const tr =
            document.createElement('tr');


        tr.innerHTML =

            '<td class="style-name">' +
            escapeHtml(row.style) +
            '</td>' +

            '<td class="number">' +
            formatNumber(totalUnits) +
            '</td>' +

            '<td class="number">' +
            formatNumber(mnff) +
            '</td>' +

            '<td class="number">' +
            formatNumber(poorCutting) +
            '</td>' +

            '<td class="number">' +

            '<div class="percent-wrapper">' +

            '<div class="percent-bar-track">' +

            '<div class="percent-bar" ' +
            'style="width:' +
            barWidth.toFixed(1) +
            '%">' +
            '</div>' +

            '</div>' +

            '<span class="percent-text">' +
            percentage.toFixed(2) +
            '%' +
            '</span>' +

            '</div>' +

            '</td>';


        cuttingTableBody
            .appendChild(tr);
    });


    const cuttingTotalDefects =
        cuttingTotalMNFF +
        cuttingTotalPoorCutting;


    const cuttingTotalPercentage =
        cuttingTotalUnits === 0
            ? 0
            : (
                cuttingTotalDefects /
                cuttingTotalUnits
            ) * 100;


    document.getElementById(
        'cuttingTotalUnits')
        .textContent =
        formatNumber(cuttingTotalUnits);

    document.getElementById(
        'cuttingTotalMNFF')
        .textContent =
        formatNumber(cuttingTotalMNFF);

    document.getElementById(
        'cuttingTotalPoorCutting')
        .textContent =
        formatNumber(
            cuttingTotalPoorCutting);

    document.getElementById(
        'cuttingTotalPercentage')
        .textContent =
        cuttingTotalPercentage
            .toFixed(2) + '%';

    new Chart(
        document.getElementById(
            'cuttingDefectChart'),

        {
            type: 'bar',

            data: {

                labels:
                    cuttingRows.map(
                        row => row.style),

                datasets: [

                    {
                        label: 'MNFF',

                        data:
                            cuttingRows.map(
                                row => row.mnff)
                    },

                    {
                        label: 'Poor Cutting',

                        data:
                            cuttingRows.map(
                                row =>
                                    row.poorCutting)
                    }
                ]
            },

            options: {

                indexAxis: 'y',

                responsive: true,

                maintainAspectRatio: false,

                scales: {

                    x: {
                        beginAtZero: true
                    },

                    y: {
                        ticks: {
                            autoSkip: false
                        }
                    }
                }
            }
        });

    const cuttingMixRows =
        cuttingRows.map(function (row) {

            const mnff =
                Number(row.mnff || 0);

            const poorCutting =
                Number(
                    row.poorCutting || 0);

            const total =
                mnff + poorCutting;


            return {

                style:
                    row.style,

                mnffPercentage:
                    total === 0
                        ? 0
                        : mnff / total * 100,

                poorCuttingPercentage:
                    total === 0
                        ? 0
                        : poorCutting /
                        total * 100
            };
        });


    new Chart(
        document.getElementById(
            'cuttingMixChart'),

        {
            type: 'bar',

            data: {

                labels:
                    cuttingMixRows.map(
                        row => row.style),

                datasets: [

                    {
                        label: 'MNFF',

                        data:
                            cuttingMixRows.map(
                                row =>
                                    row.mnffPercentage),

                        stack: 'cutting'
                    },

                    {
                        label:
                            'Poor Cutting',

                        data:
                            cuttingMixRows.map(
                                row =>
                                    row.poorCuttingPercentage),

                        stack: 'cutting'
                    }
                ]
            },

            options: {

                indexAxis: 'y',

                responsive: true,

                maintainAspectRatio: false,

                scales: {

                    x: {
                        stacked: true,
                        min: 0,
                        max: 100,

                        ticks: {
                            callback:
                                value =>
                                    value + '%'
                        }
                    },

                    y: {
                        stacked: true
                    }
                }
            }
        });
})();
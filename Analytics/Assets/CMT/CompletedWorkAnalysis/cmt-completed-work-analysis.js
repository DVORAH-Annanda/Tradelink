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

})();
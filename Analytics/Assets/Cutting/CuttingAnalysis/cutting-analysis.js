(function () {
    'use strict';

    const data = window.cuttingDashboardData || {};

    const colors = {
        blue: '#118DFF',
        darkBlue: '#12239E',
        orange: '#E66C37',
        purple: '#744EC2',
        green: '#1AAB40',
        red: '#D64550',
        teal: '#00A6A6',
        grey: '#8A8886',
        yellow: '#D9B300',
        pink: '#E044A7'
    };

    const qualityPalette = [
        '#118DFF', '#1AAB40', '#E66C37', '#744EC2', '#D64550',
        '#00A6A6', '#D9B300', '#E044A7', '#12239E', '#197278',
        '#FF8C00', '#7A9A01', '#C239B3', '#0078D4', '#8764B8',
        '#8A5A44', '#00B7C3', '#A4262C'
    ];

    // Same palette used by Knitting Analysis -> Process Loss.
    // The cache is prepared once from a stable quality list so the same
    // quality keeps the same colour even when individual charts are sorted
    // differently (for example by kg vs percentage).
    const qualityColorCache = {};

    function prepareQualityColors(rows) {
        const qualities = [];
        const seen = {};

        (rows || []).forEach(function (row) {
            const quality = String((row && row.quality) || 'Unknown');
            if (!seen[quality]) {
                seen[quality] = true;
                qualities.push(quality);
            }
        });

        qualities.sort(function (a, b) {
            return a.localeCompare(b);
        });

        qualities.forEach(function (quality, index) {
            if (!qualityColorCache[quality]) {
                qualityColorCache[quality] = qualityPalette[index % qualityPalette.length];
            }
        });
    }

    function getQualityColor(quality) {
        const key = String(quality || 'Unknown');

        if (!qualityColorCache[key]) {
            // Fallback for a quality that was not in the original list.
            // A small deterministic hash keeps the result stable.
            let hash = 0;
            for (let i = 0; i < key.length; i++) {
                hash = ((hash << 5) - hash) + key.charCodeAt(i);
                hash |= 0;
            }
            qualityColorCache[key] = qualityPalette[Math.abs(hash) % qualityPalette.length];
        }

        return qualityColorCache[key];
    }

    function number(value, decimals) {
        const n = Number(value || 0);
        const d = decimals || 0;
        return n.toLocaleString(undefined, {
            minimumFractionDigits: d,
            maximumFractionDigits: d
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
        return rows.reduce(function (total, row) {
            return total + Number(row[property] || 0);
        }, 0);
    }

    function setText(id, value) {
        const el = document.getElementById(id);
        if (el) el.textContent = value;
    }

    function setVarianceText(id, value, formatter) {
        const el = document.getElementById(id);
        if (!el) return;

        el.textContent = formatter(value);
        el.classList.remove('positive', 'negative');

        if (Number(value) > 0) el.classList.add('positive');
        if (Number(value) < 0) el.classList.add('negative');
    }

    function varianceClass(value) {
        const n = Number(value || 0);
        if (n > 0) return 'variance-positive';
        if (n < 0) return 'variance-negative';
        return 'variance-zero';
    }

    function signedNumber(value, decimals) {
        const n = Number(value || 0);
        if (n > 0) return '+' + number(n, decimals);
        return number(n, decimals);
    }

    function signedPercentage(value) {
        const n = Number(value || 0);
        if (n > 0) return '+' + percentage(n);
        return percentage(n);
    }

    function chartHeight(rowCount, minimum, perRow) {
        return Math.max(minimum, rowCount * perRow + 60);
    }

    function setWrapperHeight(id, height) {
        const el = document.getElementById(id);
        if (el) el.style.height = height + 'px';
    }

    function initialiseTabs() {
        document.querySelectorAll('.tab-button').forEach(function (button) {
            button.addEventListener('click', function () {
                const targetId = button.getAttribute('data-tab');

                document.querySelectorAll('.tab-button').forEach(function (b) {
                    b.classList.remove('active');
                });

                document.querySelectorAll('.tab-page').forEach(function (page) {
                    page.classList.remove('active');
                });

                button.classList.add('active');

                const target = document.getElementById(targetId);
                if (target) target.classList.add('active');

                setTimeout(function () {
                    window.dispatchEvent(new Event('resize'));
                }, 50);
            });
        });
    }

    function commonHorizontalOptions() {
        return {
            responsive: true,
            maintainAspectRatio: false,
            indexAxis: 'y',
            plugins: {
                legend: { display: false },
                tooltip: { enabled: true }
            },
            scales: {
                x: {
                    beginAtZero: true,
                    grid: { color: '#edebe9' },
                    ticks: { color: '#605e5c' }
                },
                y: {
                    grid: { display: false },
                    ticks: {
                        color: '#323130',
                        autoSkip: false,
                        font: { size: 10 }
                    }
                }
            }
        };
    }

    function renderProduction() {
        const qualityRows = data.productionByQuality || [];
        const dayRows = data.productionByDay || [];
        const machineRows = data.productionByMachine || [];

        const expected = sum(qualityRows, 'expectedQty');
        const actual = sum(qualityRows, 'actualQty');
        const varianceQty = actual - expected;
        const variancePct = expected === 0 ? 0 : (varianceQty / expected) * 100;
        const expectedVsActualPct = expected === 0 ? 0 : (actual / expected) * 100;

        setText('productionExpected', number(expected, 0));
        setText('productionActual', number(actual, 0));
        setVarianceText('productionVarianceQty', varianceQty, function (v) { return signedNumber(v, 0); });
        setVarianceText('productionVariancePct', variancePct, signedPercentage);

        renderProductionDayChart(dayRows);
        renderMachineChart(machineRows);
        renderExpectedActualChart(qualityRows);
        renderVarianceChart(qualityRows);
        renderProductionTable(qualityRows, expected, actual, varianceQty, expectedVsActualPct, variancePct);
    }

    function renderProductionDayChart(rows) {
        const canvas = document.getElementById('productionDayChart');
        if (!canvas || !rows.length) return;

        new Chart(canvas, {
            type: 'line',
            data: {
                labels: rows.map(function (r) { return r.productionDate; }),
                datasets: [{
                    label: 'Actual Cut',
                    data: rows.map(function (r) { return r.actualQty; }),
                    borderColor: colors.blue,
                    backgroundColor: colors.blue,
                    tension: 0.15,
                    borderWidth: 2,
                    pointRadius: 4,
                    pointHoverRadius: 5
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: { legend: { display: false } },
                scales: {
                    y: { beginAtZero: true, grid: { color: '#edebe9' } },
                    x: { grid: { display: false }, ticks: { maxRotation: 45, minRotation: 0 } }
                }
            }
        });
    }

    function renderMachineChart(rows) {
        const canvas = document.getElementById('productionMachineChart');
        if (!canvas || !rows.length) return;

        setWrapperHeight('machineChartWrapper', chartHeight(rows.length, 360, 34));
        const options = commonHorizontalOptions();
        options.plugins.tooltip.callbacks = {
            label: function (context) { return 'Actual Cut: ' + number(context.raw, 0); }
        };

        new Chart(canvas, {
            type: 'bar',
            data: {
                labels: rows.map(function (r) { return r.machine; }),
                datasets: [{
                    data: rows.map(function (r) { return r.actualQty; }),
                    backgroundColor: rows.map(function (_, i) { return qualityPalette[i % qualityPalette.length]; }),
                    borderWidth: 0,
                    barPercentage: 0.78,
                    categoryPercentage: 0.88
                }]
            },
            options: options
        });
    }

    function renderExpectedActualChart(rows) {
        const canvas = document.getElementById('expectedActualChart');
        if (!canvas || !rows.length) return;

        const sorted = rows.slice().sort(function (a, b) {
            return Number(b.actualQty || 0) - Number(a.actualQty || 0);
        });

        setWrapperHeight('expectedActualChartWrapper', chartHeight(sorted.length, 420, 38));

        new Chart(canvas, {
            type: 'bar',
            data: {
                labels: sorted.map(function (r) { return r.quality; }),
                datasets: [
                    {
                        label: 'Expected Qty',
                        data: sorted.map(function (r) { return r.expectedQty; }),
                        backgroundColor: colors.grey,
                        borderWidth: 0
                    },
                    {
                        label: 'Actual Cut',
                        data: sorted.map(function (r) { return r.actualQty; }),
                        backgroundColor: colors.blue,
                        borderWidth: 0
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                indexAxis: 'y',
                plugins: {
                    legend: { display: true, position: 'top' }
                },
                scales: {
                    x: { beginAtZero: true, grid: { color: '#edebe9' } },
                    y: { grid: { display: false }, ticks: { autoSkip: false, font: { size: 10 } } }
                }
            }
        });
    }

    function renderVarianceChart(rows) {
        const canvas = document.getElementById('varianceChart');
        if (!canvas || !rows.length) return;

        const sorted = rows.slice().sort(function (a, b) {
            return Number(b.variancePercentage || 0) - Number(a.variancePercentage || 0);
        });

        setWrapperHeight('varianceChartWrapper', chartHeight(sorted.length, 420, 38));
        const options = commonHorizontalOptions();
        options.scales.x.beginAtZero = false;
        options.plugins.tooltip.callbacks = {
            label: function (context) { return 'Variance: ' + signedPercentage(context.raw); }
        };

        new Chart(canvas, {
            type: 'bar',
            data: {
                labels: sorted.map(function (r) { return r.quality; }),
                datasets: [{
                    data: sorted.map(function (r) { return r.variancePercentage; }),
                    backgroundColor: sorted.map(function (r) {
                        return Number(r.variancePercentage || 0) < 0 ? colors.red : colors.green;
                    }),
                    borderWidth: 0,
                    barPercentage: 0.78,
                    categoryPercentage: 0.88
                }]
            },
            options: options
        });
    }

    function renderProductionTable(rows, expected, actual, varianceQty, expectedVsActualPct, variancePct) {
        const body = document.getElementById('productionTableBody');
        const foot = document.getElementById('productionTableFoot');
        if (!body || !foot) return;

        if (!rows.length) {
            body.innerHTML = '<tr><td colspan="6" class="empty-message">No production data for the selected period.</td></tr>';
            foot.innerHTML = '';
            return;
        }

        body.innerHTML = rows.map(function (r) {
            return '<tr>' +
                '<td>' + escapeHtml(r.quality) + '</td>' +
                '<td class="number">' + number(r.expectedQty, 0) + '</td>' +
                '<td class="number">' + number(r.actualQty, 0) + '</td>' +
                '<td class="number ' + varianceClass(r.varianceQty) + '">' + signedNumber(r.varianceQty, 0) + '</td>' +
                '<td class="number">' + percentage(r.expectedVsActualPercentage) + '</td>' +
                '<td class="number ' + varianceClass(r.variancePercentage) + '">' + signedPercentage(r.variancePercentage) + '</td>' +
                '</tr>';
        }).join('');

        foot.innerHTML = '<tr>' +
            '<td>Grand Total</td>' +
            '<td class="number">' + number(expected, 0) + '</td>' +
            '<td class="number">' + number(actual, 0) + '</td>' +
            '<td class="number ' + varianceClass(varianceQty) + '">' + signedNumber(varianceQty, 0) + '</td>' +
            '<td class="number">' + percentage(expectedVsActualPct) + '</td>' +
            '<td class="number ' + varianceClass(variancePct) + '">' + signedPercentage(variancePct) + '</td>' +
            '</tr>';
    }

    function renderWaste() {
        const rows = data.wasteByQuality || [];

        prepareQualityColors(rows);

        const fabricWeight = sum(rows, 'fabricWeight');
        const cuttingWaste = sum(rows, 'recordedCuttingWaste');
        const panelWaste = sum(rows, 'recordedPanelWaste');
        const totalWaste = sum(rows, 'totalWaste');
        const cuttingPct = fabricWeight === 0 ? 0 : (cuttingWaste / fabricWeight) * 100;
        const panelPct = fabricWeight === 0 ? 0 : (panelWaste / fabricWeight) * 100;
        const totalPct = fabricWeight === 0 ? 0 : (totalWaste / fabricWeight) * 100;

        setText('wasteFabricWeight', number(fabricWeight, 2));
        setText('wasteCutting', number(cuttingWaste, 2));
        setText('wastePanel', number(panelWaste, 2));
        setText('wasteTotal', number(totalWaste, 2));
        setText('wasteTotalPct', percentage(totalPct));

        renderWasteKgChart(rows);
        renderWastePctChart(rows);
        renderWasteTable(rows, fabricWeight, cuttingWaste, cuttingPct, panelWaste, panelPct, totalWaste, totalPct);
    }

    function renderWasteKgChart(rows) {
        const canvas = document.getElementById('wasteKgChart');
        if (!canvas || !rows.length) return;

        const sorted = rows.slice().sort(function (a, b) {
            return Number(b.totalWaste || 0) - Number(a.totalWaste || 0);
        });

        setWrapperHeight('wasteKgChartWrapper', chartHeight(sorted.length, 420, 38));
        const options = commonHorizontalOptions();
        options.plugins.tooltip.callbacks = {
            label: function (context) { return 'Total Waste: ' + number(context.raw, 2); }
        };

        new Chart(canvas, {
            type: 'bar',
            data: {
                labels: sorted.map(function (r) { return r.quality; }),
                datasets: [{
                    data: sorted.map(function (r) { return r.totalWaste; }),
                    backgroundColor: sorted.map(function (r) { return getQualityColor(r.quality); }),
                    borderWidth: 0,
                    barPercentage: 0.78,
                    categoryPercentage: 0.88
                }]
            },
            options: options
        });
    }

    function renderWastePctChart(rows) {
        const canvas = document.getElementById('wastePctChart');
        if (!canvas || !rows.length) return;

        const sorted = rows.slice().sort(function (a, b) {
            return Number(b.totalWastePercentage || 0) - Number(a.totalWastePercentage || 0);
        });

        setWrapperHeight('wastePctChartWrapper', chartHeight(sorted.length, 420, 38));
        const options = commonHorizontalOptions();
        options.plugins.tooltip.callbacks = {
            label: function (context) { return 'Total Waste: ' + percentage(context.raw); }
        };
        options.scales.x.ticks.callback = function (value) { return value + '%'; };

        new Chart(canvas, {
            type: 'bar',
            data: {
                labels: sorted.map(function (r) { return r.quality; }),
                datasets: [{
                    data: sorted.map(function (r) { return r.totalWastePercentage; }),
                    backgroundColor: sorted.map(function (r) { return getQualityColor(r.quality); }),
                    borderWidth: 0,
                    barPercentage: 0.78,
                    categoryPercentage: 0.88
                }]
            },
            options: options
        });
    }

    function renderWasteTable(rows, fabricWeight, cuttingWaste, cuttingPct, panelWaste, panelPct, totalWaste, totalPct) {
        const body = document.getElementById('wasteTableBody');
        const foot = document.getElementById('wasteTableFoot');
        if (!body || !foot) return;

        if (!rows.length) {
            body.innerHTML = '<tr><td colspan="8" class="empty-message">No cutting waste data for the selected period.</td></tr>';
            foot.innerHTML = '';
            return;
        }

        body.innerHTML = rows.map(function (r) {
            return '<tr>' +
                '<td>' + escapeHtml(r.quality) + '</td>' +
                '<td class="number">' + number(r.fabricWeight, 2) + '</td>' +
                '<td class="number">' + number(r.recordedCuttingWaste, 2) + '</td>' +
                '<td class="number">' + percentage(r.cuttingWastePercentage) + '</td>' +
                '<td class="number">' + number(r.recordedPanelWaste, 2) + '</td>' +
                '<td class="number">' + percentage(r.panelWastePercentage) + '</td>' +
                '<td class="number">' + number(r.totalWaste, 2) + '</td>' +
                '<td class="number">' + percentage(r.totalWastePercentage) + '</td>' +
                '</tr>';
        }).join('');

        foot.innerHTML = '<tr>' +
            '<td>Grand Total</td>' +
            '<td class="number">' + number(fabricWeight, 2) + '</td>' +
            '<td class="number">' + number(cuttingWaste, 2) + '</td>' +
            '<td class="number">' + percentage(cuttingPct) + '</td>' +
            '<td class="number">' + number(panelWaste, 2) + '</td>' +
            '<td class="number">' + percentage(panelPct) + '</td>' +
            '<td class="number">' + number(totalWaste, 2) + '</td>' +
            '<td class="number">' + percentage(totalPct) + '</td>' +
            '</tr>';
    }

    function initialise() {
        setText('dashboardPeriod', (data.fromDate || '') + ' - ' + (data.toDate || ''));
        initialiseTabs();
        renderProduction();
        renderWaste();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initialise);
    } else {
        initialise();
    }
})();

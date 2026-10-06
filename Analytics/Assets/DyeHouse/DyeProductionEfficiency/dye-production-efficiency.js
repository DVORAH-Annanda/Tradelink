(function () {
    'use strict';

    const data = window.dyeDashboardData || {};
    const blue = '#118DFF', red = '#C00000';

    function n(v, d) {
        return Number(v || 0).toLocaleString(undefined, {
            minimumFractionDigits: d,
            maximumFractionDigits: d
        });
    }

    function pct(v) {
        return n(v, 2) + '%';
    }

    function esc(v) {
        return String(v == null ? '' : v)
            .replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;')
            .replace(/"/g, '&quot;');
    }

    document.getElementById('period').textContent = (data.fromDate || '') + ' - ' + (data.toDate || '');

    document.querySelectorAll('.tab-button').forEach(function (b) {
        b.addEventListener('click', function () {
            document.querySelectorAll('.tab-button').forEach(x => x.classList.remove('active'));
            document.querySelectorAll('.tab-page').forEach(x => x.classList.remove('active'));
            b.classList.add('active');
            document.getElementById(b.dataset.tab).classList.add('active');
            setTimeout(() => window.dispatchEvent(new Event('resize')), 20);
        });
    });

    const p = (data.processLoss || [])
        .slice()
        .sort((a, b) => a.processLossPercentage - b.processLossPercentage);

    document.getElementById('processBody').innerHTML = p.map(r =>
        '<tr>' +
        '<td>' + esc(r.colour) + '</td>' +
        '<td class="number">' + n(r.grossWeight, 2) + '</td>' +
        '<td class="number">' + n(r.nettWeight, 2) + '</td>' +
        '<td class="number">' + pct(r.processLossPercentage) + '</td>' +
        '</tr>'
    ).join('');

    let gross = p.reduce((s, r) => s + Number(r.grossWeight || 0), 0),
        nett = p.reduce((s, r) => s + Number(r.nettWeight || 0), 0),
        loss = gross ? ((nett / gross) - 1) * 100 : 0;

    document.getElementById('processFoot').innerHTML =
        '<tr>' +
        '<td>Total</td>' +
        '<td class="number">' + n(gross, 2) + '</td>' +
        '<td class="number">' + n(nett, 2) + '</td>' +
        '<td class="number">' + pct(loss) + '</td>' +
        '</tr>';

    const chartHeight = Math.max(450, p.length * 24);
    document.getElementById('processChart').parentElement.style.height =
        chartHeight + 'px';

    new Chart(document.getElementById('processChart'), {
        type: 'bar',
        data: {
            labels: p.map(r => r.colour),
            datasets: [{
                label: 'Process Loss %',
                data: p.map(r => r.processLossPercentage),
                backgroundColor: p.map(r => window.AnalyticsColourPalette ? window.AnalyticsColourPalette.getDyeColour(r.colour) : blue)
            }]
        },
        options: {
            indexAxis: 'y',
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false }
            },
            scales: {
                x: {
                    ticks: { callback: v => v + '%' }
                },
                y: {
                    ticks: {
                        autoSkip: false
                    }
                }
            }
        }
    });

    const d = (data.diskVariance || [])
        .slice()
        .sort((a, b) => a.diskVariancePercentage - b.diskVariancePercentage);

    document.getElementById('diskBody').innerHTML = d.map(r =>
        '<tr>' +
        '<td>' + esc(r.greigeQuality) + '</td>' +
        '<td class="number">' + n(r.pieceCount, 0) + '</td>' +
        '<td class="number">' + n(r.averageStandardDisk, 2) + '</td>' +
        '<td class="number">' + n(r.averageActualDisk, 2) + '</td>' +
        '<td class="number ' + (r.diskVariancePercentage > 1 ? 'bad' : '') + '">' + pct(r.diskVariancePercentage) + '</td>' +
        '</tr>'
    ).join('');


    document.getElementById('diskChart').parentElement.style.height =
        chartHeight + 'px';

    new Chart(document.getElementById('diskChart'), {
        type: 'bar',
        data: {
            labels: d.map(r => r.greigeQuality),
            datasets: [{
                label: 'Disk Variance %',
                data: d.map(r => r.diskVariancePercentage),
                backgroundColor: d.map(r => r.diskVariancePercentage > 1 ? red : blue)
            }]
        },
        options: {
            indexAxis: 'y',
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false }
            },
            scales: {
                x: {
                    ticks: { callback: v => v + '%' }
                },
                y: {
                    ticks: {
                        autoSkip: false
                    }
                }
            }
        }
    });
})();

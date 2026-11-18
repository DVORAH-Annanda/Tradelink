(function () {
    'use strict';

    const data = window.salesDashboardData || {};
    const summary = data.summary || {};
    const blue = '#118DFF', grey = '#A19F9D', green = '#107C10', red = '#C00000';

    function n(v) { return Number(v || 0).toLocaleString('en-US', { maximumFractionDigits: 0, useGrouping: false }); }
    function pct(v) { const x = Number(v || 0); return (x > 0 ? '+' : '') + x.toFixed(2) + '%'; }
    function esc(v) { return String(v == null ? '' : v).replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/"/g,'&quot;'); }
    function movementClass(v) { const x = Number(v || 0); return x > 0 ? 'positive' : x < 0 ? 'negative' : 'neutral'; }
    function setText(id, text, cls) { const el = document.getElementById(id); if (!el) return; el.textContent = text; if (cls) el.classList.add(cls); }
    function chartHeight(count, minHeight, perRow) { return Math.max(minHeight, count * perRow); }

    setText('period', (data.fromDate || '') + ' - ' + (data.toDate || ''));
    setText('previousPeriod', 'Compared with ' + (data.previousFromDate || '') + ' - ' + (data.previousToDate || ''));
    setText('grossSales', n(summary.grossSales));
    setText('returns', n(summary.returns));
    setText('netSales', n(summary.netSales));
    setText('returnRate', Number(summary.returnRate || 0).toFixed(2) + '%');
    setText('growth', pct(summary.netGrowthPercentage), movementClass(summary.netGrowthPercentage));
    setText('grossPrev', 'Previous: ' + n(summary.previousGrossSales));
    setText('returnsPrev', 'Previous: ' + n(summary.previousReturns));
    setText('netPrev', 'Previous: ' + n(summary.previousNetSales));
    setText('returnRatePrev', 'Previous: ' + Number(summary.previousReturnRate || 0).toFixed(2) + '%');
    setText('prevNetHead', data.previousYear + ' Net');
    setText('currNetHead', data.currentYear + ' Net');

    document.querySelectorAll('.tab-button').forEach(function (button) {
        button.addEventListener('click', function () {
            document.querySelectorAll('.tab-button').forEach(function (b) { b.classList.remove('active'); });
            document.querySelectorAll('.tab-page').forEach(function (p) { p.classList.remove('active'); });
            button.classList.add('active');
            document.getElementById(button.dataset.tab).classList.add('active');
            setTimeout(function () { window.dispatchEvent(new Event('resize')); }, 20);
        });
    });

    const monthly = data.monthly || [];
    document.getElementById('monthlyBody').innerHTML = monthly.map(function (r) {
        const diff = Number(r.currentNetSales || 0) - Number(r.previousNetSales || 0);
        return '<tr><td>' + esc(r.month) + '</td>' +
            '<td class="number">' + n(r.previousNetSales) + '</td>' +
            '<td class="number">' + n(r.currentNetSales) + '</td>' +
            '<td class="number ' + movementClass(diff) + '">' + (diff > 0 ? '+' : '') + n(diff) + '</td>' +
            '<td class="number movement ' + movementClass(r.netGrowthPercentage) + '">' + pct(r.netGrowthPercentage) + '</td>' +
            '<td class="number">' + n(r.currentReturns) + '</td></tr>';
    }).join('');

    new Chart(document.getElementById('monthlyChart'), {
        type: 'bar',
        data: {
            labels: monthly.map(function (r) { return r.month; }),
            datasets: [
                { label: String(data.previousYear), data: monthly.map(function (r) { return r.previousNetSales; }), backgroundColor: grey },
                { label: String(data.currentYear), data: monthly.map(function (r) { return r.currentNetSales; }), backgroundColor: blue }
            ]
        },
        options: { responsive:true, maintainAspectRatio:false, plugins:{ legend:{ position:'top' } }, scales:{ y:{ beginAtZero:true, grid:{ color:'#edebe9' } }, x:{ grid:{ display:false } } } }
    });

    function renderPerformanceTable(bodyId, rows, detailed, limit) {
        const body = document.getElementById(bodyId);
        const list = (rows || []).slice(0, limit || 30);
        if (!list.length) { body.innerHTML = '<tr><td colspan="6">No data for the selected period.</td></tr>'; return; }
        body.innerHTML = list.map(function (r) {
            if (detailed) {
                return '<tr><td>' + esc(r.name) + '</td><td class="number">' + n(r.previousNetSales) + '</td><td class="number">' + n(r.currentGrossSales) + '</td><td class="number">' + n(r.currentReturns) + '</td><td class="number">' + n(r.currentNetSales) + '</td><td class="number movement ' + movementClass(r.growthPercentage) + '">' + pct(r.growthPercentage) + '</td></tr>';
            }
            return '<tr><td>' + esc(r.name) + '</td><td class="number">' + n(r.previousNetSales) + '</td><td class="number">' + n(r.currentNetSales) + '</td><td class="number movement ' + movementClass(r.growthPercentage) + '">' + pct(r.growthPercentage) + '</td></tr>';
        }).join('');
    }

    renderPerformanceTable('customerBody', data.customers, true, 50);
    renderPerformanceTable('styleBody', data.styles, false, 30);
    renderPerformanceTable('colourBody', data.colours, false, 30);

    function renderHorizontalChart(canvasId, wrapId, rows, colourFn) {
        const top = (rows || []).filter(function (r) { return Number(r.currentNetSales || 0) !== 0; }).slice(0, 15);
        const wrap = document.getElementById(wrapId);
        if (wrap) wrap.style.height = chartHeight(top.length, 430, 34) + 'px';
        if (!top.length) return;
        new Chart(document.getElementById(canvasId), {
            type:'bar',
            data:{ labels:top.map(function(r){ return r.name; }), datasets:[{ data:top.map(function(r){ return r.currentNetSales; }), backgroundColor:top.map(function(r,i){ return colourFn ? colourFn(r, i) : blue; }), borderWidth:0 }] },
            options:{ indexAxis:'y', responsive:true, maintainAspectRatio:false, plugins:{ legend:{ display:false }, tooltip:{ callbacks:{ label:function(ctx){ return 'Net sales: ' + n(ctx.raw); } } } }, scales:{ x:{ beginAtZero:true, grid:{ color:'#edebe9' } }, y:{ grid:{ display:false }, ticks:{ autoSkip:false, font:{ size:11 } } } } }
        });
    }

    renderHorizontalChart('customerChart','customerChartWrap',data.customers);
    renderHorizontalChart('styleChart','styleChartWrap',data.styles);
    renderHorizontalChart('colourChart','colourChartWrap',data.colours,function(r){
        return window.AnalyticsColourPalette ? window.AnalyticsColourPalette.getDyeColour(r.name) : blue;
    });

    const q = data.quality || {};
    setText('soldRows', n(q.soldRows));
    setText('zeroQty', n(q.zeroOrNegativeQtyRows), Number(q.zeroOrNegativeQtyRows || 0) > 0 ? 'negative' : 'positive');
    setText('missingMaster', n(q.missingMasterDataRows), Number(q.missingMasterDataRows || 0) > 0 ? 'negative' : 'positive');
    setText('missingDate', n(q.missingSoldDateRows), Number(q.missingSoldDateRows || 0) > 0 ? 'negative' : 'positive');
    setText('latestSale', q.latestSoldDate || '');
    setText('latestReturn', q.latestReturnDate || '');

    // Completed financial years: selection is client-side, no extra SQL calls.
    const financialYears = (data.financialYears || []).slice().sort(function(a,b){ return a.endYear-b.endYear; });
    const fyPicker = document.getElementById('fyYearPicker');
    const fySelected = new Set(financialYears.slice(-5).map(function(r){ return Number(r.endYear); }));
    let financialChart = null;
    function fyLabel(endYear) { return 'FY ' + (endYear - 1) + '/' + String(endYear).slice(-2); }
    function fyNum(v) { return Number(v || 0).toLocaleString('en-ZA').replace(/\u00a0/g,' '); }
    function drawFinancialYears() {
        const selected = financialYears.filter(function(r){ return fySelected.has(Number(r.endYear)); });
        const count = fySelected.size;
        setText('fySelectedCount', count + ' of 10 financial years selected');
        fyPicker.innerHTML = financialYears.slice().reverse().map(function(r){
            const year = Number(r.endYear), checked = fySelected.has(year);
            const disabled = count >= 10 && !checked;
            return '<label class="fy-year-option' + (disabled ? ' disabled' : '') + '">' +
                '<input type="checkbox" data-year="' + year + '" ' + (checked ? 'checked ' : '') +
                (disabled ? 'disabled ' : '') + '>' + esc(fyLabel(year)) + '</label>';
        }).join('');
        fyPicker.querySelectorAll('input[type=checkbox]').forEach(function(input){
            input.addEventListener('change', function(){
                const year = Number(input.dataset.year);
                if (input.checked && fySelected.size < 10) fySelected.add(year);
                else if (!input.checked) fySelected.delete(year);
                drawFinancialYears();
            });
        });
        const tbody = document.getElementById('fyTableBody');
        let previous = null;
        tbody.innerHTML = selected.length ? selected.map(function(r){
            const net = Number(r.netSales || 0);
            const difference = previous ? net - Number(previous.netSales || 0) : null;
            const percent = difference === null || Number(previous.netSales || 0) === 0 ? null : difference / Math.abs(Number(previous.netSales)) * 100;
            previous = r;
            return '<tr><td>' + esc(fyLabel(r.endYear)) + '</td><td>01 Jul ' + (r.endYear - 1) + ' – 30 Jun ' + r.endYear + '</td>' +
                '<td class="number">' + fyNum(net) + '</td>' +
                '<td class="number ' + (difference === null ? '' : movementClass(difference)) + '">' + (difference === null ? '—' : ((difference > 0 ? '+' : '') + fyNum(difference))) + '</td>' +
                '<td class="number ' + (percent === null ? '' : movementClass(percent)) + '">' + (percent === null ? '—' : pct(percent)) + '</td></tr>';
        }).join('') : '<tr><td colspan="5">Select at least one financial year to display results.</td></tr>';
        const latest = selected.length ? selected[selected.length-1] : null;
        //setText('fySummary', selected.length ? 'Selected: ' + selected.length + ' financial year(s)  |  Most recent selected: ' + fyLabel(latest.endYear) + '  |  Net units: ' + fyNum(latest.netSales) : 'No years selected');
        if (financialChart) { financialChart.destroy(); financialChart = null; }
        if (selected.length && typeof Chart !== 'undefined') {
            financialChart = new Chart(document.getElementById('fyChart'), {
                type:'bar',
                data:{labels:selected.map(function(r){return fyLabel(r.endYear);}),datasets:[{label:'Net units sold',data:selected.map(function(r){return r.netSales;}),backgroundColor:blue}]},
                options:{responsive:true,maintainAspectRatio:false,plugins:{legend:{display:false},tooltip:{callbacks:{label:function(ctx){return 'Net units: '+fyNum(ctx.raw);}}}},scales:{y:{beginAtZero:true,ticks:{callback:function(v){return fyNum(v);}}},x:{grid:{display:false}}}}
            });
        }
    }
    drawFinancialYears();
})();

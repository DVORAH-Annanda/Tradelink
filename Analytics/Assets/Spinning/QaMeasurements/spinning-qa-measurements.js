(function () {
  'use strict';
  var data = window.spinningQaDashboardData || {};
  var kind = data.kind === 'rsb' ? 'rsb' : 'card';
  var isRsb = kind === 'rsb';
  var all = Array.isArray(data.rows) ? data.rows : [];
  var filtered = all.slice();
  var charts = [];
  var page = 0, pageSize = 100;
  var el = function (id) { return document.getElementById(id); };
  var finite = function (v) { return v !== null && v !== undefined && v !== '' && isFinite(Number(v)); };
  var number = function (v) { return finite(v) ? Number(v) : null; };
  var fmt = function (v, digits) { return finite(v) ? Number(v).toLocaleString('en-US', { minimumFractionDigits: digits || 0, maximumFractionDigits: digits || 0, useGrouping: false }) : '—'; };
  var esc = function (v) { return String(v == null ? '' : v).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&#39;'); };
  var cell = function (v, dp, extra) { return '<td class="num ' + (extra || '') + '">' + fmt(v, dp) + '</td>'; };
  var empty = function (columns) { return '<tr><td colspan="' + columns + '" class="empty">No measurements match the selected filters.</td></tr>'; };
  var mean = function (rows, field) { var a = rows.map(function (r) { return number(r[field]); }).filter(function (v) { return v !== null; }); return a.length ? a.reduce(function (s, v) { return s + v; }, 0) / a.length : null; };
  var extent = function (rows, field, min) { var a = rows.map(function (r) { return number(r[field]); }).filter(function (v) { return v !== null; }); return a.length ? (min ? Math.min.apply(null, a) : Math.max.apply(null, a)) : null; };
  var machine = function (r) { return r.machine || 'Unknown machine'; };
  var dateText = function (d) { var s = String(d || '').split('-'); return s.length === 3 ? s[2] + '/' + s[1] + '/' + s[0] : String(d || ''); };
  var shiftLabel = function (v) { return Number(v) === 1 ? 'Day shift' : Number(v) === 2 ? 'Night shift' : 'Unknown shift'; };
  var group = function (rows, keyfn) { var o = {}; rows.forEach(function (r) { var k = keyfn(r); (o[k] = o[k] || []).push(r); }); return o; };
  var ordered = function (rows) { return rows.slice().sort(function (a, b) { return String(b.date).localeCompare(String(a.date)) || Number(b.id) - Number(a.id); }); };
  var baseLabels = [isRsb ? 'RSB readings' : 'Card tests', isRsb ? 'Average measurement' : 'Average daily value', isRsb ? 'Outside tolerance' : 'Average CV %', 'Machines tested'];

  function setText(id, value) { el(id).textContent = value; }
  function setVisible(id, visible) { el(id).style.display = visible ? '' : 'none'; }
  function limits() {
    var low = Number(el('lowerInput').value), high = Number(el('upperInput').value);
    var ok = el('lowerInput').value.trim() !== '' && el('upperInput').value.trim() !== '' && isFinite(low) && isFinite(high) && low < high;
    return { low: low, high: high, valid: ok };
  }
  function outside(r, l) { var n = number(r.measurement); return n !== null && (n < l.low || n > l.high); }
  function validMeasurement(r) { return finite(r.measurement); }
  function calcOutsideRate(rows, l) { var a = rows.filter(validMeasurement); return a.length ? a.filter(function (r) { return outside(r, l); }).length * 100 / a.length : null; }
  function machineGroups(rows) {
    var by = group(rows, machine);
    return Object.keys(by).sort().map(function (name) { return { name: name, rows: by[name] }; });
  }
  function dailyGroups(rows) {
    var by = group(rows, function (r) { return r.date; });
    return Object.keys(by).sort().map(function (d) { return { date: d, label: dateText(d), rows: by[d] }; });
  }
  function setKpi(index, title, value, note, problem) {
    setText('kpiTitle' + index, title);
    setText('kpiValue' + index, value);
    setText('kpiNote' + index, note || '');
    el('kpiValue' + index).className = problem ? 'bad' : '';
  }

  setText('title', isRsb ? 'RSB Measurements Analysis' : 'Card Measurements Analysis');
  setText('period', (data.fromDate || '') + ' – ' + (data.toDate || ''));
  setText('intro', isRsb ? 'Draw-frame measurements by machine, canister and shift' : 'Daily card averages and CV % trends by machine');
  setVisible('shiftControl', isRsb);
  setVisible('statusControl', isRsb);
  setVisible('lowerControl', isRsb);
  setVisible('upperControl', isRsb);
  setVisible('toleranceWarning', isRsb);
  setVisible('shiftSection', isRsb);
  el('lowerInput').value = String(data.initialLowerTolerance || 4410);
  el('upperInput').value = String(data.initialUpperTolerance || 4590);
  Array.from(new Set(all.map(function (r) { return r.machine || ''; }).filter(Boolean))).sort().forEach(function (name) {
    var option = document.createElement('option'); option.value = name; option.textContent = name; el('machineFilter').appendChild(option);
  });
  if (typeof Chart !== 'undefined') {
    Chart.defaults.locale = 'en-US';
    Chart.defaults.plugins.tooltip.callbacks.label = function (context) {
      var label = context.dataset.label || '';
      var val = context.parsed.y;
      return (label ? label + ': ' : '') + fmt(val, Number.isInteger(val) ? 0 : 2);
    };
  }

  function renderMachineSummary(l) {
    var groups = machineGroups(filtered);
    setText('machineTitle', isRsb ? 'Machine performance against selected limits' : 'Card averages by machine');
    setText('machineSubtitle', isRsb ? 'Outside % uses only recorded measurements; selected tolerance is inclusive.' : 'Compare average CV % and daily values across machines; no quality pass/fail limit has been set.');
    el('machineHeader').innerHTML = isRsb
      ? '<tr><th>Machine</th><th class="num">Readings</th><th class="num">Avg measurement</th><th class="num">Minimum</th><th class="num">Maximum</th><th class="num">Outside</th><th class="num">Outside %</th></tr>'
      : '<tr><th>Machine</th><th class="num">Tests</th><th class="num">Avg daily value</th><th class="num">Min daily value</th><th class="num">Max daily value</th><th class="num">Avg CV %</th><th class="num">CV % range</th></tr>';
    el('machineBody').innerHTML = groups.length ? groups.map(function (g) {
      var r = g.rows;
      if (isRsb) {
        return '<tr><td>' + esc(g.name) + '</td>' + cell(r.length, 0) + cell(mean(r, 'measurement'), 1) + cell(extent(r, 'measurement', true), 0) + cell(extent(r, 'measurement', false), 0) + cell(r.filter(function (x) { return outside(x, l); }).length, 0) + cell(calcOutsideRate(r, l), 1) + '</tr>';
      }
      return '<tr><td>' + esc(g.name) + '</td>' + cell(r.length, 0) + cell(mean(r, 'dailyAverage'), 2) + cell(extent(r, 'dailyAverage', true), 2) + cell(extent(r, 'dailyAverage', false), 2) + cell(mean(r, 'cv'), 2) + '<td class="num">' + fmt(extent(r, 'cv', true), 2) + ' – ' + fmt(extent(r, 'cv', false), 2) + '</td></tr>';
    }).join('') : empty(7);
    return groups;
  }

  function renderKpis(l) {
    var count = filtered.length;
    var machines = new Set(filtered.filter(function (r) { return r.hasMachine; }).map(function (r) { return machine(r); })).size;
    setKpi(1, baseLabels[0], fmt(count, 0), 'Selected period and filters');
    setKpi(2, baseLabels[1], fmt(mean(filtered, isRsb ? 'measurement' : 'dailyAverage'), 2), isRsb ? 'Captured RSB results' : 'As captured by the Spinning team');
    if (isRsb) {
      var nonNull = filtered.filter(validMeasurement).length;
      var failed = filtered.filter(function (r) { return outside(r, l); }).length;
      setKpi(3, baseLabels[2], fmt(calcOutsideRate(filtered, l), 1) + (nonNull ? '%' : ''), fmt(failed, 0) + ' outside / ' + fmt(nonNull, 0) + ' tested', failed > 0);
    } else {
      setKpi(3, baseLabels[2], fmt(mean(filtered, 'cv'), 2), 'A lower CV% generally means more even material');
    }
    setKpi(4, baseLabels[3], fmt(machines, 0), 'Machines with a matching master record');
  }

  function renderShiftSummary(l) {
    if (!isRsb) return;
    var shifts = [ { key: '1', text: 'Day shift' }, { key: '2', text: 'Night shift' }, { key: 'other', text: 'Unknown shift' } ];
    el('shiftBody').innerHTML = shifts.map(function (s) {
      var rows = filtered.filter(function (r) { return s.key === 'other' ? Number(r.testNo) !== 1 && Number(r.testNo) !== 2 : String(r.testNo) === s.key; });
      var nonNull = rows.filter(validMeasurement).length;
      var badCount = rows.filter(function (r) { return outside(r, l); }).length;
      return '<tr><td>' + s.text + '</td>' + cell(rows.length, 0) + cell(mean(rows, 'measurement'), 1) + cell(extent(rows, 'measurement', true), 0) + cell(extent(rows, 'measurement', false), 0) + cell(badCount, 0) + cell(nonNull ? 100 * badCount / nonNull : null, 1) + '</tr>';
    }).join('');
  }

  function renderCharts(groups, l) {
    charts.forEach(function (c) { c.destroy(); }); charts = [];
    if (typeof Chart === 'undefined') return;
    var dates = dailyGroups(filtered);
    var axes = { y: { beginAtZero: !isRsb, ticks: { callback: function (v) { return fmt(v, 0); } } }, x: { ticks: { autoSkip: true, maxTicksLimit: 14, maxRotation: 45 } } };
    var options = function (legend) { return { responsive: true, maintainAspectRatio: false, plugins: { legend: { display: !!legend, position: 'top' } }, scales: axes }; };
    if (isRsb) {
      setText('chartTitle1', 'Out-of-tolerance rate by machine');
      setText('chartNote1', 'Share of recorded RSB results outside the selected limits, by machine.');
      setText('chartTitle2', 'Daily RSB measurement trend');
      setText('chartNote2', 'Daily average with your editable lower and upper limits. Daily averages may hide individual outliers.');
      charts.push(new Chart(el('chart1'), { type: 'bar', data: { labels: groups.map(function (g) { return g.name; }), datasets: [{ label: 'Outside %', data: groups.map(function (g) { return calcOutsideRate(g.rows, l); }), backgroundColor: '#cf7783' }] }, options: options(false) }));
      charts.push(new Chart(el('chart2'), { type: 'line', data: { labels: dates.map(function (g) { return g.label; }), datasets: [
        { label: 'Daily average', data: dates.map(function (g) { return mean(g.rows, 'measurement'); }), borderColor: '#1679ca', pointRadius: 3, tension: .2 },
        { label: 'Lower limit', data: dates.map(function () { return l.low; }), borderColor: '#cc793e', borderDash: [6, 5], pointRadius: 0, borderWidth: 1.5 },
        { label: 'Upper limit', data: dates.map(function () { return l.high; }), borderColor: '#cc793e', borderDash: [6, 5], pointRadius: 0, borderWidth: 1.5 }
      ] }, options: options(true) }));
    } else {
      setText('chartTitle1', 'Average CV % by carding machine');
      setText('chartNote1', 'A lower CV % usually indicates more consistent material; compare similar testing conditions.');
      setText('chartTitle2', 'Card measurements over time');
      setText('chartNote2', 'Daily averages of recorded CV % and daily card values; the two series have separate scales.');
      charts.push(new Chart(el('chart1'), { type: 'bar', data: { labels: groups.map(function (g) { return g.name; }), datasets: [{ label: 'Average CV %', data: groups.map(function (g) { return mean(g.rows, 'cv'); }), backgroundColor: '#3d8acc' }] }, options: options(false) }));
      charts.push(new Chart(el('chart2'), { type: 'line', data: { labels: dates.map(function (g) { return g.label; }), datasets: [
        { label: 'Daily avg CV %', data: dates.map(function (g) { return mean(g.rows, 'cv'); }), borderColor: '#196fb7', yAxisID: 'cv', tension: .2, pointRadius: 3 },
        { label: 'Daily avg value', data: dates.map(function (g) { return mean(g.rows, 'dailyAverage'); }), borderColor: '#df9446', yAxisID: 'dailyAverage', tension: .2, pointRadius: 3 }
      ] }, options: { responsive: true, maintainAspectRatio: false, plugins: { legend: { position: 'top' } }, scales: {
        x: { ticks: { autoSkip: true, maxTicksLimit: 14, maxRotation: 45 } },
        cv: { type: 'linear', position: 'left', title: { display: true, text: 'CV %' }, ticks: { callback: function (v) { return fmt(v, 1); } } },
        dailyAverage: { type: 'linear', position: 'right', title: { display: true, text: 'Daily avg' }, grid: { drawOnChartArea: false }, ticks: { callback: function (v) { return fmt(v, 1); } } }
      } } }));
    }
  }

  function renderDetails(l) {
    setText('detailTitle', isRsb ? 'RSB readings — canister detail' : 'Card quality — individual tests');
    setText('detailSubtitle', isRsb ? 'Red rows are outside the currently selected tolerance. Limits are inclusive.' : 'Daily average and CV% are directly recorded in the original Spinning application.');
    el('detailHeader').innerHTML = isRsb
      ? '<tr><th>Date</th><th>Machine</th><th>Shift</th><th class="num">Canister</th><th class="num">Measurement</th><th>Result</th></tr>'
      : '<tr><th>Date</th><th>Machine</th><th>Test</th><th class="num">Daily average</th><th class="num">CV %</th></tr>';
    var rows = ordered(filtered), totalPages = Math.max(1, Math.ceil(rows.length / pageSize));
    page = Math.min(Math.max(0, page), totalPages - 1);
    var part = rows.slice(page * pageSize, (page + 1) * pageSize);
    el('detailBody').innerHTML = part.length ? part.map(function (r) {
      if (isRsb) {
        var state = !validMeasurement(r) ? '<span class="pill">Missing value</span>' : outside(r, l) ? '<span class="pill bad">Outside</span>' : '<span class="pill good">Within</span>';
        return '<tr><td>' + dateText(r.date) + '</td><td title="' + esc(r.machineDescription) + '">' + esc(machine(r)) + '</td><td>' + shiftLabel(r.testNo) + '</td>' + cell(r.canister, 0) + cell(r.measurement, 0, outside(r, l) ? 'bad' : '') + '<td>' + state + '</td></tr>';
      }
      return '<tr><td>' + dateText(r.date) + '</td><td title="' + esc(r.machineDescription) + '">' + esc(machine(r)) + '</td><td>' + esc(r.testNo == null ? '—' : 'Avg ' + r.testNo) + '</td>' + cell(r.dailyAverage, 2) + cell(r.cv, 2) + '</tr>';
    }).join('') : empty(isRsb ? 6 : 5);
    el('prevPage').disabled = page === 0;
    el('nextPage').disabled = page === totalPages - 1;
    setText('pageStatus', 'Page ' + fmt(page + 1, 0) + ' of ' + fmt(totalPages, 0) + ' · ' + fmt(rows.length, 0) + ' rows');
  }

  function renderChecks() {
    var checks = [
      ['Missing machine', filtered.filter(function (r) { return !r.hasMachine; }).length, 'No matching machine definition for the stored machine ID.'],
      ['Missing ' + (isRsb ? 'RSB measurement' : 'card average or CV %'), filtered.filter(function (r) { return isRsb ? !finite(r.measurement) : !finite(r.dailyAverage) || !finite(r.cv); }).length, 'One or more report values are not populated.']
    ];
    if (isRsb) {
      checks.push(['Missing canister number', filtered.filter(function (r) { return !finite(r.canister); }).length, 'Canister ID not recorded.']);
      checks.push(['Unexpected shift code', filtered.filter(function (r) { return Number(r.testNo) !== 1 && Number(r.testNo) !== 2; }).length, 'Expected shift 1 (day) or 2 (night).']);
    }
    el('checksBody').innerHTML = checks.map(function (x) { return '<tr><td>' + esc(x[0]) + '</td>' + cell(x[1], 0, x[1] ? 'bad' : '') + '<td>' + esc(x[2]) + '</td></tr>'; }).join('');
  }

  function update() {
    var l = limits();
    if (isRsb && !l.valid) {
      setText('filterStatus', 'Please enter valid limits: lower must be less than upper.');
      el('filterStatus').className = 'hint bad';
      return;
    }
    el('filterStatus').className = 'hint';
    var selectedMachine = el('machineFilter').value;
    var selectedShift = el('shiftFilter').value;
    var selectedStatus = el('statusFilter').value;
    filtered = all.filter(function (r) {
      if (selectedMachine && r.machine !== selectedMachine) return false;
      if (!isRsb) return true;
      if (selectedShift === 'other' && (Number(r.testNo) === 1 || Number(r.testNo) === 2)) return false;
      if (selectedShift && selectedShift !== 'other' && String(r.testNo) !== selectedShift) return false;
      if (selectedStatus === 'missing') return !validMeasurement(r);
      if (selectedStatus === 'outside') return outside(r, l);
      if (selectedStatus === 'inside') return validMeasurement(r) && !outside(r, l);
      return true;
    });
    setText('filterStatus', fmt(filtered.length, 0) + ' of ' + fmt(all.length, 0) + ' records. Machine and filter selections apply to the whole dashboard.');
    page = 0;
    renderKpis(l);
    var groups = renderMachineSummary(l);
    renderShiftSummary(l);
    renderCharts(groups, l);
    renderDetails(l);
    renderChecks();
  }

  function csvEscape(v) { return '"' + String(v == null ? '' : v).replace(/"/g, '""') + '"'; }
  function exportCsv() {
    var lines = [isRsb ? ['Date', 'Machine', 'Shift', 'Canister', 'Measurement', 'Status', 'Lower Limit', 'Upper Limit'] : ['Date', 'Machine', 'Test', 'Daily Average', 'CV %']];
    var l = limits();
    ordered(filtered).forEach(function (r) {
      lines.push(isRsb
        ? [r.date, r.machine, shiftLabel(r.testNo), r.canister, r.measurement, validMeasurement(r) ? outside(r, l) ? 'Outside' : 'Within' : 'Missing', l.low, l.high]
        : [r.date, r.machine, r.testNo, r.dailyAverage, r.cv]);
    });
    var contents = '\uFEFF' + lines.map(function (line) { return line.map(csvEscape).join(';'); }).join('\r\n');
    var url = URL.createObjectURL(new Blob([contents], { type: 'text/csv;charset=utf-8;' }));
    var link = document.createElement('a'); link.href = url;
    link.download = 'Spinning-' + (isRsb ? 'RSB' : 'Card') + '-' + new Date().toISOString().slice(0, 10) + '.csv';
    document.body.appendChild(link); link.click(); link.remove();
    setTimeout(function () { URL.revokeObjectURL(url); }, 2500);
  }

  el('machineFilter').addEventListener('change', update);
  el('shiftFilter').addEventListener('change', update);
  el('statusFilter').addEventListener('change', update);
  el('lowerInput').addEventListener('change', update);
  el('upperInput').addEventListener('change', update);
  el('clearFilters').addEventListener('click', function () {
    el('machineFilter').value = '';
    el('shiftFilter').value = '';
    el('statusFilter').value = '';
    el('lowerInput').value = String(data.initialLowerTolerance || 4410);
    el('upperInput').value = String(data.initialUpperTolerance || 4590);
    update();
  });
  el('prevPage').addEventListener('click', function () { page--; renderDetails(limits()); });
  el('nextPage').addEventListener('click', function () { page++; renderDetails(limits()); });
  el('btnCsv').addEventListener('click', exportCsv);
  el('btnPrint').addEventListener('click', function () { window.print(); });
  renderChecks();
  update();
})();

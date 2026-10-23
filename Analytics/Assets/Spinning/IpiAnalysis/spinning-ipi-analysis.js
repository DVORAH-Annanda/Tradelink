(function () {
    'use strict';
    var data = window.spinningIpiDashboardData || {};
    var all = Array.isArray(data.rows) ? data.rows : [];
    var filtered = all;
    var matrixLimit = 45;
    var page = 0;
    var pageSize = 150;
    var charts = [];
    var el = function(id) { return document.getElementById(id); };
    var finite = function(v) { return v !== null && v !== undefined && v !== '' && isFinite(Number(v)); };
    var num = function(v) { return finite(v) ? Number(v) : null; };
    var fmt = function(v, dp) { return finite(v) ? Number(v).toLocaleString('en-ZA', {minimumFractionDigits:dp||0,maximumFractionDigits:dp||0}) : '—'; };
    var esc = function(v) { return String(v == null ? '' : v).replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/"/g,'&quot;').replace(/'/g,'&#39;'); };
    var mean = function(rows,key) { var a=rows.map(function(r){return num(r[key]);}).filter(function(v){return v!==null;});return a.length ? a.reduce(function(s,v){return s+v;},0)/a.length : null; };
    var minVal = function(rows,key) {var a=rows.map(function(r){return num(r[key]);}).filter(function(v){return v!==null;});return a.length?Math.min.apply(null,a):null;};
    var maxVal = function(rows,key) {var a=rows.map(function(r){return num(r[key]);}).filter(function(v){return v!==null;});return a.length?Math.max.apply(null,a):null;};
    var hasMismatch = function(r) { return [r.thin,r.thick,r.nep,r.ipi].every(finite) && Math.abs(Number(r.ipi)-Number(r.thin)-Number(r.thick)-Number(r.nep)) > 0.01; };
    var texKey = function(r) { return finite(r.nominalTex) ? Number(r.nominalTex).toFixed(2) : 'Unknown'; };
    var texLabel = function(k) { return k === 'Unknown' ? 'Unknown tex' : fmt(Number(k), 1) + ' tex'; };
    var sortedTexKeys = function(groups) {return Object.keys(groups).sort(function(a,b){if(a==='Unknown')return 1;if(b==='Unknown')return -1;return Number(a)-Number(b);});};
    var groupsByTex = function(rows) {var out={};rows.forEach(function(r){var k=texKey(r);(out[k]=out[k]||[]).push(r);});return out;};
    function dateText(v) { if(!v)return ''; var a=String(v).split('-');return a.length===3?a[2]+'/'+a[1]+'/'+a[0]:v; }
    function cell(v,dp,cl) { return '<td class="num '+(cl||'')+'">'+fmt(v,dp)+'</td>'; }
    function emptyTable(colspan) {return '<tr><td colspan="'+colspan+'" class="empty">No quality tests for these filters.</td></tr>';}

    el('period').textContent = (data.fromDate||'')+' – '+(data.toDate||'');
    var machineNames = Array.from(new Set(all.map(function(r){return r.machine||'';}).filter(Boolean))).sort();
    machineNames.forEach(function(name){var o=document.createElement('option');o.value=name;o.textContent=name;el('machineFilter').appendChild(o);});
    var texGroups = groupsByTex(all);
    sortedTexKeys(texGroups).forEach(function(k){var o=document.createElement('option');o.value=k;o.textContent=texLabel(k);el('texFilter').appendChild(o);});

    function summariseByTex(rows) {
        var g=groupsByTex(rows);
        return sortedTexKeys(g).map(function(k){return {key:k, label:texLabel(k), items:g[k], count:g[k].length, measured:mean(g[k],'tex'), cv:mean(g[k],'cv'), thin:mean(g[k],'thin'), thick:mean(g[k],'thick'), nep:mean(g[k],'nep'), ipi:mean(g[k],'ipi'), min:minVal(g[k],'ipi'), max:maxVal(g[k],'ipi')};});
    }
    function renderKpis() {
        el('kpiTests').textContent=fmt(filtered.length,0);
        el('kpiIpi').textContent=fmt(mean(filtered,'ipi'),1);
        el('kpiCv').textContent=fmt(mean(filtered,'cv'),2);
        var mismatch=filtered.filter(hasMismatch).length;
        el('kpiMismatch').textContent=fmt(mismatch,0);
        el('kpiMismatch').className=mismatch?'problem':'';
        el('filteredCount').textContent=fmt(filtered.length,0)+' of '+fmt(all.length,0)+' tests';
    }
    function renderMatrix() {
        var groups=groupsByTex(filtered), keys=sortedTexKeys(groups);
        if(!keys.length){el('matrixContainer').innerHTML='<div class="empty">No test results match the selected filters.</div>';el('matrixStatus').textContent='';return;}
        keys.forEach(function(k){groups[k].sort(function(a,b){return b.date.localeCompare(a.date)||b.id-a.id;});});
        var maxRows=Math.max.apply(null,keys.map(function(k){return Math.min(groups[k].length,matrixLimit);}));
        var head1='<tr>'+keys.map(function(k){return '<th class="group-header" colspan="9">'+esc(texLabel(k))+' &nbsp;·&nbsp; '+fmt(groups[k].length,0)+' tests</th>';}).join('')+'</tr>';
        var head2='<tr>'+keys.map(function(){return '<th>Date</th><th>Merge</th><th>Y O</th><th>Tex</th><th>CV %</th><th>Thin</th><th>Thick</th><th>Nep</th><th>IPI</th>';}).join('')+'</tr>';
        var body='';
        for(var i=0;i<maxRows;i++){
            body+='<tr>'+keys.map(function(k){var r=groups[k][i];if(!r)return '<td colspan="9"></td>';
                return '<td>'+esc(dateText(r.date))+'</td>'+'<td class="merge">'+esc(r.merge||'—')+'</td>'+
                  '<td class="yo num">'+(r.yarnOrder==null?'—':fmt(r.yarnOrder,0))+'</td>'+
                  cell(r.tex,2)+cell(r.cv,2)+cell(r.thin,0)+cell(r.thick,0)+cell(r.nep,0)+
                  '<td class="ipi num '+(hasMismatch(r)?'incorrect':'')+'">'+fmt(r.ipi,0)+'</td>';
            }).join('')+'</tr>';
        }
        body+='<tr class="group-footer">'+keys.map(function(k){var g=groups[k];return '<td>Average</td><td colspan="2">'+fmt(g.length,0)+' tests</td>'+cell(mean(g,'tex'),2)+cell(mean(g,'cv'),2)+cell(mean(g,'thin'),1)+cell(mean(g,'thick'),1)+cell(mean(g,'nep'),1)+cell(mean(g,'ipi'),1);}).join('')+'</tr>';
        el('matrixContainer').innerHTML='<table><thead>'+head1+head2+'</thead><tbody>'+body+'</tbody></table>';
        var hidden=keys.reduce(function(s,k){return s+Math.max(0,groups[k].length-matrixLimit);},0);
        el('matrixStatus').textContent= hidden ? fmt(hidden,0)+' additional rows hidden in matrix for readability. Use “Show more” or CSV to see everything.' : 'Showing all rows for each tex group.';
        el('matrixMore').disabled=hidden===0;
    }
    function renderSummary() {
        var sums=summariseByTex(filtered);
        el('texSummary').innerHTML=sums.length?sums.map(function(s){return '<tr><td>'+esc(s.label)+'</td>'+cell(s.count,0)+cell(s.measured,2)+cell(s.cv,2)+cell(s.thin,1)+cell(s.thick,1)+cell(s.nep,1)+cell(s.ipi,1)+'<td class="num">'+fmt(s.min,0)+' – '+fmt(s.max,0)+'</td></tr>';}).join(''):emptyTable(9);
        var groups={};filtered.forEach(function(r){var key=(r.machine||'Unknown machine')+'\u0001'+texKey(r);(groups[key]=groups[key]||[]).push(r);});
        var machineRows=Object.keys(groups).map(function(k){var rs=groups[k];return {name:rs[0].machine||'Unknown machine', tex:texKey(rs[0]), rs:rs};});
        machineRows.sort(function(a,b){return a.tex===b.tex ? a.name.localeCompare(b.name) : a.tex==='Unknown'?1:b.tex==='Unknown'?-1:Number(a.tex)-Number(b.tex);});
        el('machineSummary').innerHTML=machineRows.length?machineRows.map(function(g){return '<tr><td>'+esc(g.name)+'</td><td>'+esc(texLabel(g.tex))+'</td>'+cell(g.rs.length,0)+cell(mean(g.rs,'cv'),2)+cell(mean(g.rs,'ipi'),1)+cell(minVal(g.rs,'ipi'),0)+cell(maxVal(g.rs,'ipi'),0)+'</tr>';}).join(''):emptyTable(7);
        return sums;
    }
    function renderDetails() {
        var ordered=filtered.slice().sort(function(a,b){return b.date.localeCompare(a.date)||b.id-a.id;});
        var pages=Math.max(1,Math.ceil(ordered.length/pageSize));page=Math.max(0,Math.min(page,pages-1));
        var pageRows=ordered.slice(page*pageSize,(page+1)*pageSize);
        el('detailBody').innerHTML=pageRows.length?pageRows.map(function(r){var m=hasMismatch(r);
            return '<tr><td>'+esc(dateText(r.date))+'</td><td title="'+esc(r.machineDescription||'')+'">'+esc(r.machine||'—')+'</td><td>'+esc(r.merge||'—')+'</td><td class="num">'+(r.yarnOrder==null?'—':fmt(r.yarnOrder,0))+'</td>'+
            cell(r.nominalTex,1)+cell(r.tex,2)+cell(r.cv,2)+cell(r.thin,0)+cell(r.thick,0)+cell(r.nep,0)+'<td class="num '+(m?'problem':'')+'">'+fmt(r.ipi,0)+'</td><td>'+(m?'<span class="problem">IPI mismatch</span>':'')+'</td></tr>';
        }).join(''):emptyTable(12);
        el('prevPage').disabled=page===0;el('nextPage').disabled=page>=pages-1;
        el('pageStatus').textContent='Page '+(page+1)+' of '+pages+' · '+fmt(ordered.length,0)+' rows';
    }
    function renderChecks() {
        var checks=[
            ['IPI does not equal Thin + Thick + Nep',filtered.filter(hasMismatch).length,'Verify the stored total against the three components.'],
            ['Missing yarn order',filtered.filter(function(r){return !r.hasYarnOrder;}).length,'Test has no matching yarn-order master record.'],
            ['Missing yarn type / nominal tex',filtered.filter(function(r){return !r.hasYarnType||!finite(r.nominalTex);}).length,'Cannot group this test by nominal tex.'],
            ['Missing machine',filtered.filter(function(r){return !r.hasMachine;}).length,'Cannot associate this test with a machine.'],
            ['Missing measurement value',filtered.filter(function(r){return ![r.tex,r.cv,r.thin,r.thick,r.nep,r.ipi].every(finite);}).length,'One or more input/result fields were not recorded.'],
            ['Missing merge description',filtered.filter(function(r){return !r.merge;}).length,'No linked cotton merge or merge description.']
        ];
        el('checksBody').innerHTML=checks.map(function(r){return '<tr><td>'+esc(r[0])+'</td><td class="num '+(r[1]?'problem':'')+'">'+fmt(r[1],0)+'</td><td>'+esc(r[2])+'</td></tr>';}).join('');
    }
    function makeCharts(sums) {
        charts.forEach(function(c){c.destroy();}); charts=[];
        if(typeof Chart==='undefined')return;
        var label=sums.map(function(s){return s.label;});
        var opts={responsive:true,maintainAspectRatio:false,plugins:{legend:{display:false}},scales:{y:{beginAtZero:true},x:{ticks:{autoSkip:false,maxRotation:35,minRotation:0}}}};
        charts.push(new Chart(el('chartTex'),{type:'bar',data:{labels:label,datasets:[{label:'Avg IPI',data:sums.map(function(s){return s.ipi;}),backgroundColor:'#118dff'}]},options:opts}));
        charts.push(new Chart(el('chartComponents'),{type:'bar',data:{labels:label,datasets:[
          {label:'Thin',data:sums.map(function(s){return s.thin;}),backgroundColor:'#387eb8'},
          {label:'Thick',data:sums.map(function(s){return s.thick;}),backgroundColor:'#eca744'},
          {label:'Nep',data:sums.map(function(s){return s.nep;}),backgroundColor:'#7ea65a'}]},
          options:{responsive:true,maintainAspectRatio:false,plugins:{legend:{position:'top'}},scales:{x:{stacked:true,ticks:{autoSkip:false}},y:{stacked:true,beginAtZero:true}}}}));
    }
    function update() {
        var machine=el('machineFilter').value, tex=el('texFilter').value, order=el('orderFilter').value.trim().toLowerCase();
        filtered=all.filter(function(r){return (!machine||r.machine===machine)&&(!tex||texKey(r)===tex)&&(!order||String(r.yarnOrder==null?'':r.yarnOrder).toLowerCase().includes(order)||String(r.merge||'').toLowerCase().includes(order));});
        page=0;matrixLimit=45;
        renderKpis();renderMatrix();makeCharts(renderSummary());renderDetails();renderChecks();
    }
    function csvEscape(v) {var t=String(v==null?'':v);return '"'+t.replace(/"/g,'""')+'"';}
    function exportCsv() {
        var rows=[['Date','Machine','Merge','Yarn Order','Nominal Tex','Measured Tex','CV %','Thin','Thick','Nep','Stored IPI','IPI Check']];
        filtered.slice().sort(function(a,b){return b.date.localeCompare(a.date)||b.id-a.id;}).forEach(function(r){rows.push([r.date,r.machine,r.merge,r.yarnOrder,r.nominalTex,r.tex,r.cv,r.thin,r.thick,r.nep,r.ipi,hasMismatch(r)?'Mismatch':'OK']);});
        var content='\uFEFF'+rows.map(function(r){return r.map(csvEscape).join(';');}).join('\r\n');
        var blob=new Blob([content],{type:'text/csv;charset=utf-8;'}),url=URL.createObjectURL(blob);
        var a=document.createElement('a');a.href=url;a.download='Spinning-IPI-'+new Date().toISOString().slice(0,10)+'.csv';document.body.appendChild(a);a.click();a.remove();setTimeout(function(){URL.revokeObjectURL(url);},2000);
    }
    el('machineFilter').addEventListener('change',update);
    el('texFilter').addEventListener('change',update);
    el('orderFilter').addEventListener('input',update);
    el('btnClear').addEventListener('click',function(){el('machineFilter').value='';el('texFilter').value='';el('orderFilter').value='';update();});
    el('matrixMore').addEventListener('click',function(){matrixLimit+=45;renderMatrix();});
    el('prevPage').addEventListener('click',function(){page--;renderDetails();});
    el('nextPage').addEventListener('click',function(){page++;renderDetails();});
    el('btnCsv').addEventListener('click',exportCsv);
    el('btnPrint').addEventListener('click',function(){window.print();});
    update();
})();

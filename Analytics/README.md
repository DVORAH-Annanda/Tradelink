## Included
- Cutting Production HTML analytics tab
  - Expected Qty
  - Actual Cut (TLCUTSHRD_BoxUnits)
  - Variance Qty / Variance %
  - Production by day
  - Production by machine
  - Expected vs Actual by greige quality
  - Red/green variance chart
- Cutting Waste HTML analytics tab
  - Fabric weight
  - Cutting waste
  - Panel waste
  - Total waste / Total waste %
  - Waste charts and quality detail table
- Main application Analytics menu entry: Cutting Analysis

## Important calculation rule
The production view is receipt-level. Expected Qty is cut-sheet-level, so GetProductionByQuality first collapses receipts to one cut sheet before summing Expected Qty. This prevents expected production being double-counted when a cut sheet has multiple receipts.

## Sales Analytics
- New form: `Analytics.Sales.SalesAnalysis.frmSalesAnalysis`
- Default period: last three completed calendar months, compared with the same dates one year earlier.
- KPI cards: Gross Sales, Returns, Net Sales, Return Rate, YoY Net Growth.
- Tables before charts for monthly, customer, style and colour analysis.
- Shared colour-family mapping is used for sales-by-colour charts.
- Operational checks surface zero/negative quantities, missing master-data links, missing sold dates, latest sale date and latest return date.
- SQL source: `Sql\vw_SalesAnalysis.sql` (the database view must exist in TTI2).

### Host application menu/form integration
The Analytics project is a library and the host application's menu code is not included here. Open the new report from the host application with:

```csharp
using Analytics.Sales.SalesAnalysis;

var report = new frmSalesAnalysis();
report.ShowDialog(this);
```

## Spinning IPI Analysis (added 23 October 2026)

The new `Analytics.Spinning.IpiAnalysis.frmSpinningIpiAnalysis` matches the
Excel-style Spinning IPI report. It displays tests grouped horizontally by
nominal tex, data tables before charts, a per-machine/per-tex comparison,
consistency checks, and filtered CSV export.

### Open from the main TTI2 host application

```csharp
using Analytics.Spinning.IpiAnalysis;

using (var report = new frmSpinningIpiAnalysis())
    report.ShowDialog(this);
```

The Analytics project is a .NET Framework 4.8 class library; its host application's
menu/MDI code is not included in this uploaded Analytics archive. Add the menu
entry there, rebuild Analytics, and ensure the updated Analytics DLL is published
with the host app.

### SQL source and mapping (confirmed in uploaded Spinning code)

* `TLSPN_QAMeasurements` with `YarnQA_MeasureNo = 2` is the Spinning Machine Measurements / IPI dataset.
* `YarnQA_Date` => Date; `YarnQA_MachineNo_FK` => machine definition.
* `YarnQA_YarnOrder_FK` => `TLSPN_YarnOrder.YarnO_Pk` => `YarnO_OrderNumber` (Y O).
* `YarnO_MergeContract_FK` => `TLSPN_CottonMerge.TLCTM_Pk` => `TLCTM_Description` (Merge).
* `Yarno_YarnType_FK` => `TLADM_Yarn.YA_Id` => `YA_TexCount` (nominal tex, grouping).
* `YarnQA_08H00` => actual Tex/Count; `YarnQA_10H00` => CV %;
  `YarnQA_12H00` => Thin; `YarnQA_14H00` => Thick;
  `YarnQA_16H00` => Nep; `YarnQA_18H00` => stored IPI.

The legacy `08H00` etc suffixes are field names reused to represent different
measurements; they are **not** time slots in the IPI report. No new database
view or schema change is required.

The last available QA measurement date is used to choose the default 3-month range.
The SQL query uses the date range with an exclusive upper bound to preserve
records having time components. The presentation performs machine, tex and
order/merge filters client-side. Large ranges that exceed WebView2
`NavigateToString`'s size limit fail with a meaningful error, so reduce the
range if needed. All chart values are simple unweighted test-level averages;
no undisclosed quality specification threshold is assumed.

### First production cross-check

Choose a period and a yarn order from the existing Spinning `Quality Results
by Spinning Machine` Crystal report. Verify merge number, yarn order number,
nominal vs tested tex, CV %, Thin, Thick, Nep, and IPI for the **same machine
and date**. Investigate any missing merge/master-data or stored IPI mismatches.

### Constraints

The uploaded archives contain source but no SQL Server instance. The SQL query
and WinForms UI have not been run against your TTI2 database or tested on the
Windows host. The host application needs the menu integration noted above.

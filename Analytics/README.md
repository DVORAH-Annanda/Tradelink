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

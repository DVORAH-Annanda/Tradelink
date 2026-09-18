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

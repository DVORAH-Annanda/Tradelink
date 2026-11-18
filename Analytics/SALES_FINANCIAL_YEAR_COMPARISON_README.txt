Sales Analytics - Financial Year Comparison (18 November 2026)

WHAT CHANGED
- Fifth tab in Sales Analytics: Financial Year Comparison.
- Compare net garment quantities for completed 1 July-30 June financial years.
- Select 0-10 years, including non-adjacent years. Latest five completed years selected by default where available.
- Net = gross sold quantity on sold date minus returned quantity on return date.
- Table shows years, net units, change from preceding selected year and percentage change.
- Chart compares selected years. No new SQL view or migration required.
- Unfinished current financial year is excluded by design.
- Independent of the standard Sales date filters.

FILES CHANGED
Sales/SalesAnalysis/SalesAnalysisRepository.cs
Sales/SalesAnalysis/SalesAnalysisDashboardBuilder.cs
Sales/SalesAnalysis/frmSalesAnalysis.cs
Sales/SalesAnalysis/Models/SalesFinancialYearRow.cs (new)
Assets/Sales/SalesAnalysis/sales-analysis.html
Assets/Sales/SalesAnalysis/sales-analysis.js
Analytics.csproj

NOTES
- Source ZIP: excludes transient bin/obj/.vs files. Open Analytics.csproj in Visual Studio and rebuild.
- Uses the existing dbo.vw_SalesAnalysis view.
- No live SQL Server or Windows/.NET Framework build was available for verification here.
- Financial year comparison reports quantities, not Rand/invoice monetary values.

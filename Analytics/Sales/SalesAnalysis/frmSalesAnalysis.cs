using Analytics.Common;
using Analytics.Sales.SalesAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.Sales.SalesAnalysis
{
    public partial class frmSalesAnalysis : Form
    {
        private readonly HtmlDashboardViewer dashboardViewer;
        private readonly DateTimePicker dtpFromDate;
        private readonly DateTimePicker dtpToDate;
        private readonly Button btnApply;

        public frmSalesAnalysis()
        {
            InitializeComponent();
            Text = "Sales Analytics";
            WindowState = FormWindowState.Maximized;

            var filterPanel = new Panel { Dock = DockStyle.Top, Height = 65, Padding = new Padding(15), BackColor = Color.White };
            var lblFromDate = new Label { Text = "From Date:", AutoSize = true, Location = new Point(15, 23) };
            dtpFromDate = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Width = 120, Location = new Point(85, 18) };
            var lblToDate = new Label { Text = "To Date:", AutoSize = true, Location = new Point(225, 23) };
            dtpToDate = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Width = 120, Location = new Point(285, 18) };
            btnApply = new Button { Text = "Apply", Width = 90, Height = 28, Location = new Point(425, 17) };
            btnApply.Click += Apply_Click;

            var compareLabel = new Label
            {
                Text = "Comparison: same date range last year",
                AutoSize = true,
                ForeColor = Color.DimGray,
                Location = new Point(535, 23)
            };

            filterPanel.Controls.Add(lblFromDate);
            filterPanel.Controls.Add(dtpFromDate);
            filterPanel.Controls.Add(lblToDate);
            filterPanel.Controls.Add(dtpToDate);
            filterPanel.Controls.Add(btnApply);
            filterPanel.Controls.Add(compareLabel);

            dashboardViewer = new HtmlDashboardViewer { Dock = DockStyle.Fill };
            Controls.Add(dashboardViewer);
            Controls.Add(filterPanel);
            Load += SalesAnalysis_Load;
        }

        private async void SalesAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                var repository = new SalesAnalysisRepository();
                DateTime latest = repository.GetLatestSoldDate() ?? DateTime.Today;

                // Default to the last three completed calendar months.
                DateTime lastCompletedMonth = new DateTime(latest.Year, latest.Month, 1).AddDays(-1);
                dtpToDate.Value = lastCompletedMonth;
                dtpFromDate.Value = new DateTime(lastCompletedMonth.Year, lastCompletedMonth.Month, 1).AddMonths(-2);
                await LoadDashboardAsync();
            }
            catch (Exception ex) { await ShowErrorAsync(ex); }
        }

        private async void Apply_Click(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show("The From Date cannot be later than the To Date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await LoadDashboardAsync();
        }

        private async Task LoadDashboardAsync()
        {
            try
            {
                btnApply.Enabled = false;
                btnApply.Text = "Loading...";

                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date;
                var repository = new SalesAnalysisRepository();

                SalesSummary summary = repository.GetSummary(fromDate, toDate);
                List<SalesMonthlyComparisonRow> monthly = repository.GetMonthlyComparison(fromDate, toDate);
                List<SalesPerformanceRow> customers = repository.GetCustomerPerformance(fromDate, toDate);
                List<SalesPerformanceRow> styles = repository.GetStylePerformance(fromDate, toDate);
                List<SalesPerformanceRow> colours = repository.GetColourPerformance(fromDate, toDate);
                SalesDataQualitySummary quality = repository.GetDataQuality(fromDate, toDate);

                string html = new SalesAnalysisDashboardBuilder().Build(summary, monthly, customers, styles, colours, quality, fromDate, toDate);
                await dashboardViewer.ShowHtmlAsync(html);
            }
            catch (Exception ex) { await ShowErrorAsync(ex); }
            finally
            {
                btnApply.Enabled = true;
                btnApply.Text = "Apply";
            }
        }

        private async Task ShowErrorAsync(Exception ex)
        {
            await dashboardViewer.ShowHtmlAsync("<h2>Unable to load Sales Analytics</h2><p>" + WebUtility.HtmlEncode(ex.Message) + "</p>");
        }
    }
}

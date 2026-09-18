using Analytics.Common;
using Analytics.Cutting.CuttingAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.Cutting.CuttingAnalysis
{
    public partial class frmCuttingAnalysis : Form
    {
        private readonly HtmlDashboardViewer dashboardViewer;
        private readonly DateTimePicker dtpFromDate;
        private readonly DateTimePicker dtpToDate;
        private readonly Button btnApply;
        private readonly Label lblFromDate;
        private readonly Label lblToDate;

        public frmCuttingAnalysis()
        {
            InitializeComponent();

            Text = "Cutting Analysis";
            WindowState = FormWindowState.Maximized;

            var filterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                Padding = new Padding(15),
                BackColor = Color.White
            };

            lblFromDate = new Label
            {
                Text = "From Date:",
                AutoSize = true,
                Location = new Point(15, 23)
            };

            dtpFromDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Width = 120,
                Location = new Point(85, 18)
            };

            lblToDate = new Label
            {
                Text = "To Date:",
                AutoSize = true,
                Location = new Point(225, 23)
            };

            dtpToDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy",
                Width = 120,
                Location = new Point(285, 18)
            };

            btnApply = new Button
            {
                Text = "Apply",
                Width = 90,
                Height = 28,
                Location = new Point(425, 17)
            };

            btnApply.Click += Apply_Click;

            filterPanel.Controls.Add(lblFromDate);
            filterPanel.Controls.Add(dtpFromDate);
            filterPanel.Controls.Add(lblToDate);
            filterPanel.Controls.Add(dtpToDate);
            filterPanel.Controls.Add(btnApply);

            dashboardViewer = new HtmlDashboardViewer
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(dashboardViewer);
            Controls.Add(filterPanel);

            Load += frmCuttingAnalysis_Load;
        }

        private async void frmCuttingAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                var repository = new CuttingAnalysisRepository();
                DateTime? latestDate = repository.GetLatestProductionDate();

                if (!latestDate.HasValue)
                {
                    await dashboardViewer.ShowHtmlAsync(
                        "<h2>No Cutting Production data was found.</h2>");
                    return;
                }

                // Same default behaviour as the CMT dashboard:
                // latest 7 calendar days ending on the latest data date.
                dtpToDate.Value = latestDate.Value.Date;
                dtpFromDate.Value = latestDate.Value.Date.AddDays(-6);

                await LoadDashboardAsync();
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex);
            }
        }

        private async void Apply_Click(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show(
                    "The From Date cannot be later than the To Date.",
                    "Invalid Date Range",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
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

                var repository = new CuttingAnalysisRepository();

                List<CuttingProductionQualityRow> productionByQuality =
                    repository.GetProductionByQuality(fromDate, toDate);

                List<CuttingProductionDailyRow> productionByDay =
                    repository.GetProductionByDay(fromDate, toDate);

                List<CuttingProductionMachineRow> productionByMachine =
                    repository.GetProductionByMachine(fromDate, toDate);

                List<CuttingWasteQualityRow> wasteByQuality =
                    repository.GetWasteByQuality(fromDate, toDate);

                var builder = new CuttingAnalysisDashboardBuilder();

                string html = builder.Build(
                    productionByQuality,
                    productionByDay,
                    productionByMachine,
                    wasteByQuality,
                    fromDate,
                    toDate);

                await dashboardViewer.ShowHtmlAsync(html);
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex);
            }
            finally
            {
                btnApply.Enabled = true;
                btnApply.Text = "Apply";
            }
        }

        private async Task ShowErrorAsync(Exception ex)
        {
            string message = WebUtility.HtmlEncode(ex.Message);

            await dashboardViewer.ShowHtmlAsync(
                "<h2>Unable to load Cutting Analysis</h2>" +
                "<p>" + message + "</p>");
        }
    }
}

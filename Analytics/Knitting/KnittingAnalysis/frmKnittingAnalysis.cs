using Analytics.Common;
using Analytics.Knitting.KnittingAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.Knitting.KnittingAnalysis
{
    public partial class frmKnittingAnalysis : Form
    {
        private readonly HtmlDashboardViewer dashboardViewer;

        private readonly DateTimePicker dtpFromDate;
        private readonly DateTimePicker dtpToDate;
        private readonly Button btnApply;

        private readonly Label lblFromDate;
        private readonly Label lblToDate;


        public frmKnittingAnalysis()
        {
            InitializeComponent();

            Text = "Knitting Analysis";
            WindowState = FormWindowState.Maximized;


            // -------------------------------------------------
            // Filter panel
            // -------------------------------------------------
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


            // -------------------------------------------------
            // Dashboard
            // -------------------------------------------------
            dashboardViewer = new HtmlDashboardViewer
            {
                Dock = DockStyle.Fill
            };


            Controls.Add(dashboardViewer);
            Controls.Add(filterPanel);


            Load += KnittingAnalysis_Load;
        }


        private async void KnittingAnalysis_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                SetDefaultPepDateRange();

                await LoadDashboardAsync();
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex);
            }
        }


        // =====================================================
        // DEFAULT PEP PERIOD
        //
        // Previous Tuesday -> this Monday
        //
        // Example:
        // Friday 21 Nov
        // From: Tuesday 11 Nov
        // To:   Monday 17 Nov
        // =====================================================
        private void SetDefaultPepDateRange()
        {
            DateTime today =
                DateTime.Today;


            int daysSinceMonday =
                ((int)today.DayOfWeek + 6) % 7;


            DateTime thisMonday =
                today.AddDays(
                    -daysSinceMonday);


            DateTime previousTuesday =
                thisMonday.AddDays(-6);


            dtpFromDate.Value =
                previousTuesday;


            dtpToDate.Value =
                thisMonday;
        }


        private async void Apply_Click(
            object sender,
            EventArgs e)
        {
            if (dtpFromDate.Value.Date >
                dtpToDate.Value.Date)
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


                DateTime fromDate =
                    dtpFromDate.Value.Date;


                DateTime toDate =
                    dtpToDate.Value.Date;


                var repository =
                    new KnittingAnalysisRepository();


                // =============================================
                // Get all dashboard datasets
                // =============================================

                List<KnittingDiskVarianceRow>
                    diskVariance =
                        repository.GetDiskVariance(
                            fromDate,
                            toDate);


                List<KnittingQualityRow>
                    knittingQuality =
                        repository.GetKnittingQuality(
                            fromDate,
                            toDate);


                List<KnittingProcessLossOrderRow>
                    processLossOrders =
                        repository.GetProcessLossOrders(
                            fromDate,
                            toDate);


                List<KnittingProcessLossMachineRow>
                    processLossByMachine =
                        repository.GetProcessLossByMachine(
                            fromDate,
                            toDate);


                // =============================================
                // Build HTML dashboard
                // =============================================

                var builder =
                    new KnittingAnalysisDashboardBuilder();


                string html =
                    builder.Build(
                        diskVariance,
                        knittingQuality,
                        processLossOrders,
                        processLossByMachine,
                        fromDate,
                        toDate);


                await dashboardViewer
                    .ShowHtmlAsync(html);
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


        private async Task ShowErrorAsync(
            Exception ex)
        {
            string message =
                WebUtility.HtmlEncode(
                    ex.Message);


            await dashboardViewer.ShowHtmlAsync(
                "<h2>Unable to load Knitting Analysis</h2>" +
                "<p>" + message + "</p>");
        }
    }
}
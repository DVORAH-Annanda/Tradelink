using Analytics.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.CMT.CompletedWorkAnalysis
{
    public partial class frmCMTCompletedWorkAnalysis : Form
    {
        private readonly HtmlDashboardViewer dashboardViewer;

        private readonly DateTimePicker dtpFromDate;
        private readonly DateTimePicker dtpToDate;
        private readonly Button btnApply;

        private readonly Label lblFromDate;
        private readonly Label lblToDate;

        public frmCMTCompletedWorkAnalysis()
        {
            InitializeComponent();

            Text = "CMT Completed Work Analysis";
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

            btnApply.Click += btnApply_Click;

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

            Load += frmCMTCompletedWorkAnalysis_Load;
        }

        private async void frmCMTCompletedWorkAnalysis_Load(
            object sender,
            EventArgs e)
        {
            try
            {
                var repository = new CMTCompletedWorkRepository();

                DateTime? latestDate =
                    repository.GetLatestTransactionDate();

                if (!latestDate.HasValue)
                {
                    await dashboardViewer.ShowHtmlAsync(
                        "<h2>No CMT Completed Work data was found.</h2>");

                    return;
                }

                // Default to the latest 7 full days that contain data
                dtpToDate.Value = latestDate.Value.Date;
                dtpFromDate.Value = latestDate.Value.Date.AddDays(-6);

                await LoadDashboardAsync();
            }
            catch (Exception ex)
            {
                await ShowErrorAsync(ex);
            }
        }

        private async void btnApply_Click(
            object sender,
            EventArgs e)
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

                DateTime fromDate =
                    dtpFromDate.Value.Date;

                DateTime toDate =
                    dtpToDate.Value.Date;


                var repository =
                    new CMTCompletedWorkRepository();


                CMTCompletedWorkSummary summary =
                    repository.GetSummary(
                        fromDate,
                        toDate);


                List<CMTBGradeByStyle> bGradeByStyle =
                    repository.GetBGradeByStyle(
                        fromDate,
                        toDate);

                List<CMTMnffOspecByStyle> mnffAndOspec =
    repository.GetMnffAndOspecByStyle(
        fromDate,
        toDate);

                List<CMTBGradeHolesByMachine> holesByMachine =
    repository.GetBGradeHolesByMachine(
        fromDate,
        toDate);

                List<CMTSpinningByYarnType> spinningByYarnType =
    repository.GetSpinningByYarnType(
        fromDate,
        toDate);


                var builder =
                    new CMTCompletedWorkDashboardBuilder();


                string html =
                    builder.Build(
                        summary,
                        bGradeByStyle,
                        mnffAndOspec,
                         holesByMachine,
                         spinningByYarnType,
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

        private async Task ShowErrorAsync(Exception ex)
        {
            string message = WebUtility.HtmlEncode(ex.Message);

            await dashboardViewer.ShowHtmlAsync(
                "<h2>Unable to load CMT Completed Work Analysis</h2>" +
                "<p>" + message + "</p>");
        }
    }
}
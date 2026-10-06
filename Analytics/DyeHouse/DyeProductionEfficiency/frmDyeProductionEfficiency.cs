using Analytics.Common;
using Analytics.DyeHouse.DyeProductionEfficiency.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.DyeHouse.DyeProductionEfficiency
{
    public partial class frmDyeProductionEfficiency : Form
    {
        private readonly HtmlDashboardViewer dashboardViewer;
        private readonly DateTimePicker dtpFromDate;
        private readonly DateTimePicker dtpToDate;
        private readonly Button btnApply;

        public frmDyeProductionEfficiency()
        {
            InitializeComponent();
            Text = "Dye Production Efficiency";
            WindowState = FormWindowState.Maximized;

            var filterPanel = new Panel { Dock = DockStyle.Top, Height = 65, Padding = new Padding(15), BackColor = Color.White };
            var lblFromDate = new Label { Text = "From Date:", AutoSize = true, Location = new Point(15, 23) };
            dtpFromDate = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Width = 120, Location = new Point(85, 18) };
            var lblToDate = new Label { Text = "To Date:", AutoSize = true, Location = new Point(225, 23) };
            dtpToDate = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Width = 120, Location = new Point(285, 18) };
            btnApply = new Button { Text = "Apply", Width = 90, Height = 28, Location = new Point(425, 17) };
            btnApply.Click += Apply_Click;
            filterPanel.Controls.Add(lblFromDate);
            filterPanel.Controls.Add(dtpFromDate);
            filterPanel.Controls.Add(lblToDate);
            filterPanel.Controls.Add(dtpToDate);
            filterPanel.Controls.Add(btnApply);

            dashboardViewer = new HtmlDashboardViewer { Dock = DockStyle.Fill };
            Controls.Add(dashboardViewer);
            Controls.Add(filterPanel);
            Load += DyeProductionEfficiency_Load;
        }

        private async void DyeProductionEfficiency_Load(object sender, EventArgs e)
        {
            try
            {
                var repository = new DyeProductionEfficiencyRepository();
                DateTime latest = repository.GetLatestReportingDate() ?? DateTime.Today;
                dtpToDate.Value = latest.Date;
                dtpFromDate.Value = latest.Date.AddMonths(-5);
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
                var repository = new DyeProductionEfficiencyRepository();
                List<DyeProcessLossRow> processLoss = repository.GetProcessLossByColour(fromDate, toDate);
                List<DyeDiskVarianceRow> diskVariance = repository.GetDiskVarianceByGreigeQuality(fromDate, toDate);
                string html = new DyeProductionEfficiencyDashboardBuilder().Build(processLoss, diskVariance, fromDate, toDate);
                await dashboardViewer.ShowHtmlAsync(html);
            }
            catch (Exception ex) { await ShowErrorAsync(ex); }
            finally { btnApply.Enabled = true; btnApply.Text = "Apply"; }
        }

        private async Task ShowErrorAsync(Exception ex)
        {
            await dashboardViewer.ShowHtmlAsync("<h2>Unable to load Dye Production Efficiency</h2><p>" + WebUtility.HtmlEncode(ex.Message) + "</p>");
        }
    }
}

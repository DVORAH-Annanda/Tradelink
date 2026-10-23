using Analytics.Common;
using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.Spinning.IpiAnalysis
{
    public partial class frmSpinningIpiAnalysis : Form
    {
        private readonly HtmlDashboardViewer dashboardViewer;
        private readonly DateTimePicker dtpFromDate;
        private readonly DateTimePicker dtpToDate;
        private readonly Button btnApply;

        public frmSpinningIpiAnalysis()
        {
            InitializeComponent();
            Text = "Spinning IPI Analysis";
            WindowState = FormWindowState.Maximized;

            var filters = new Panel { Dock = DockStyle.Top, Height = 64, BackColor = Color.White };
            filters.Controls.Add(new Label { Text = "From Date:", AutoSize = true, Location = new Point(16, 23) });
            dtpFromDate = new DateTimePicker { Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy", Width = 120, Location = new Point(87, 18) };
            filters.Controls.Add(dtpFromDate);
            filters.Controls.Add(new Label { Text = "To Date:", AutoSize = true, Location = new Point(227, 23) });
            dtpToDate = new DateTimePicker { Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy", Width = 120, Location = new Point(289, 18) };
            filters.Controls.Add(dtpToDate);
            btnApply = new Button { Text = "Apply", Width = 95, Height = 29,
                Location = new Point(431, 17) };
            btnApply.Click += Apply_Click;
            filters.Controls.Add(btnApply);
            filters.Controls.Add(new Label
            {
                Text = "Filter machine, tex, merge and yarn order inside the report",
                AutoSize = true, ForeColor = Color.DimGray, Location = new Point(550, 23)
            });

            dashboardViewer = new HtmlDashboardViewer { Dock = DockStyle.Fill };
            Controls.Add(dashboardViewer);
            Controls.Add(filters);
            Load += SpinningIpiAnalysis_Load;
        }

        private async void SpinningIpiAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                // Use the most recent actual QA test, not today's date, so that
                // older active factory data still opens to a meaningful report.
                DateTime last = new SpinningIpiRepository().GetLatestMeasurementDate() ?? DateTime.Today;
                dtpToDate.Value = last.Date;
                dtpFromDate.Value = last.Date.AddMonths(-3);
                await LoadDashboardAsync();
            }
            catch (Exception ex) { await ShowErrorAsync(ex); }
        }

        private async void Apply_Click(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show("From Date cannot be later than To Date.", "Invalid date range",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                var fromDate = dtpFromDate.Value.Date;
                var toDate = dtpToDate.Value.Date;
                var rows = new SpinningIpiRepository().GetMeasurements(fromDate, toDate);
                string html = new SpinningIpiDashboardBuilder().Build(rows, fromDate, toDate);
                await dashboardViewer.ShowHtmlAsync(html);
            }
            catch (Exception ex) { await ShowErrorAsync(ex); }
            finally
            {
                btnApply.Enabled = true;
                btnApply.Text = "Apply";
            }
        }

        private async Task ShowErrorAsync(Exception error)
        {
            await dashboardViewer.ShowHtmlAsync("<html><body style='font:16px Segoe UI,Arial;padding:30px'>" +
                "<h2>Unable to load Spinning IPI Analysis</h2><p>" +
                WebUtility.HtmlEncode(error.Message) + "</p></body></html>");
        }
    }
}

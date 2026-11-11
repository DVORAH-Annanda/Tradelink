using Analytics.Common;
using Analytics.Spinning.QaMeasurements;
using System;
using System.Drawing;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analytics.Spinning.IpiAnalysis
{
    // Retains the original form class so existing menu callers still work.
    public partial class frmSpinningIpiAnalysis : Form
    {
        private readonly HtmlDashboardViewer[] viewers = new HtmlDashboardViewer[3];
        private readonly bool[] loaded = new bool[3];
        private readonly bool[] loading = new bool[3];
        private readonly TabControl tabs;
        private readonly DateTimePicker dtpFromDate;
        private readonly DateTimePicker dtpToDate;
        private readonly Button btnApply;
        private bool ready;

        public frmSpinningIpiAnalysis()
        {
            InitializeComponent();
            Text = "Spinning Quality Control";
            WindowState = FormWindowState.Maximized;

            var filters = new Panel { Dock = DockStyle.Top, Height = 58, BackColor = Color.White };
            filters.Controls.Add(new Label { Text = "From Date:", AutoSize = true, Location = new Point(16, 21) });
            dtpFromDate = new DateTimePicker { Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy", Width = 120, Location = new Point(87, 16) };
            filters.Controls.Add(dtpFromDate);
            filters.Controls.Add(new Label { Text = "To Date:", AutoSize = true, Location = new Point(227, 21) });
            dtpToDate = new DateTimePicker { Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy", Width = 120, Location = new Point(289, 16) };
            filters.Controls.Add(dtpToDate);
            btnApply = new Button { Text = "Apply", Width = 95, Height = 29,
                Location = new Point(431, 15) };
            btnApply.Click += Apply_Click;
            filters.Controls.Add(btnApply);

            tabs = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                Padding = new Point(18, 7)
            };
            string[] tabNames = { "Spinning IPI", "Card Measurements", "RSB Measurements" };
            for (int i = 0; i < tabNames.Length; i++)
            {
                var page = new TabPage(tabNames[i]) { BackColor = Color.White, Padding = new Padding(0) };
                viewers[i] = new HtmlDashboardViewer { Dock = DockStyle.Fill };
                page.Controls.Add(viewers[i]);
                tabs.TabPages.Add(page);
            }
            tabs.SelectedIndexChanged += Tabs_SelectedIndexChanged;
            Controls.Add(tabs);
            Controls.Add(filters);
            Load += SpinningQualityAnalysis_Load;
        }

        private async void SpinningQualityAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                // Previous Monday to this Monday (initial default only).
                dtpFromDate.Value = AnalyticsDateDefaults.PreviousMonday();
                dtpToDate.Value = AnalyticsDateDefaults.ThisMonday();
                ready = true;
                await LoadSelectedTabAsync();
            }
            catch (Exception ex) { await ShowErrorAsync(tabs.SelectedIndex, ex); }
        }

        private async void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ready) await LoadSelectedTabAsync();
        }

        private async void Apply_Click(object sender, EventArgs e)
        {
            if (dtpFromDate.Value.Date > dtpToDate.Value.Date)
            {
                MessageBox.Show("From Date cannot be later than To Date.", "Invalid date range",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            for (int i = 0; i < loaded.Length; i++) loaded[i] = false;
            await LoadSelectedTabAsync();
        }

        private async Task LoadSelectedTabAsync()
        {
            int index = tabs.SelectedIndex;
            if (index < 0 || loaded[index] || loading[index]) return;
            loading[index] = true;
            btnApply.Enabled = false;
            btnApply.Text = "Loading...";
            try
            {
                DateTime fromDate = dtpFromDate.Value.Date;
                DateTime toDate = dtpToDate.Value.Date;
                string html;
                if (index == 0)
                {
                    var rows = new SpinningIpiRepository().GetMeasurements(fromDate, toDate);
                    html = new SpinningIpiDashboardBuilder().Build(rows, fromDate, toDate);
                }
                else
                {
                    int measureNo = index == 1 ? 1 : 3;
                    var rows = new SpinningQaRepository().GetMeasurements(measureNo, fromDate, toDate);
                    html = new SpinningQaDashboardBuilder().Build(measureNo, rows, fromDate, toDate);
                }
                await viewers[index].ShowHtmlAsync(html);
                loaded[index] = true;
            }
            catch (Exception ex) { await ShowErrorAsync(index, ex); }
            finally
            {
                loading[index] = false;
                btnApply.Enabled = true;
                btnApply.Text = "Apply";
            }
        }

        private async Task ShowErrorAsync(int tabIndex, Exception error)
        {
            if (tabIndex < 0) return;
            try
            {
                await viewers[tabIndex].ShowHtmlAsync("<html><body style='font:16px Segoe UI,Arial;padding:30px'>" +
                    "<h2>Unable to load Spinning Quality Control</h2><p>" +
                    WebUtility.HtmlEncode(error.Message) + "</p></body></html>");
            }
            catch (Exception) { MessageBox.Show(error.Message, "Spinning Quality Control", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}

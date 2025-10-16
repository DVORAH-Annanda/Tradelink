using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities;

namespace Spinning
{
    public partial class frmCottonDeliveryVarianceReport : Form
    {
        public frmCottonDeliveryVarianceReport()
        {
            InitializeComponent();
            BuildUi();
        }

        private RadioButton rbSingle;
        private RadioButton rbRange;
        private ComboBox cmbDeliveries;  // GRN list
        private DateTimePicker dtFrom;
        private DateTimePicker dtTo;
        private Button btnGenerate;
        private CheckBox chkGroupBySupplier; // for date range mode

        private void BuildUi()
        {
            Text = "Cotton Delivery Weight Variance Report";
            Width = 720; Height = 260; StartPosition = FormStartPosition.CenterParent;

            var lblDeliveries = new Label { Left = 20, Top = 20, Width = 120, Text = "DELIVERIES:" };
            rbSingle = new RadioButton { Left = 140, Top = 18, Text = "Single GRN", Checked = true };
            rbRange = new RadioButton { Left = 240, Top = 18, Text = "Date range" };
            rbSingle.CheckedChanged += ModeChanged;
            rbRange.CheckedChanged += ModeChanged;

            cmbDeliveries = new ComboBox { Left = 140, Top = 45, Width = 520, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblFrom = new Label { Left = 20, Top = 90, Width = 120, Text = "FROM DATE" };
            var lblTo = new Label { Left = 20, Top = 120, Width = 120, Text = "TO DATE" };
            dtFrom = new DateTimePicker { Left = 140, Top = 86, Width = 160, Format = DateTimePickerFormat.Short };
            dtTo = new DateTimePicker { Left = 140, Top = 116, Width = 160, Format = DateTimePickerFormat.Short };
            chkGroupBySupplier = new CheckBox { Left = 320, Top = 88, Width = 320, Text = "Group by supplier (totals per supplier)" };

            btnGenerate = new Button { Left = 560, Top = 160, Width = 100, Text = "Print" };
            btnGenerate.Click += BtnGenerate_Click;

            Controls.Add(lblDeliveries);
            Controls.Add(rbSingle);
            Controls.Add(rbRange);
            Controls.Add(cmbDeliveries);
            Controls.Add(lblFrom);
            Controls.Add(dtFrom);
            Controls.Add(lblTo);
            Controls.Add(dtTo);
            Controls.Add(chkGroupBySupplier);
            Controls.Add(btnGenerate);

            LoadDeliveries();
            ModeChanged(null, EventArgs.Empty);
        }

        private void ModeChanged(object sender, EventArgs e)
        {
            cmbDeliveries.Enabled = rbSingle.Checked;
            dtFrom.Enabled = dtTo.Enabled = chkGroupBySupplier.Enabled = rbRange.Checked;
        }

        private void LoadDeliveries()
        {
            using (var ctx = new TTI2Entities())
            {
                // Pull recent deliveries (last 12 months) — adjust as needed
                var since = DateTime.Today.AddMonths(-12);
                var list = (
                    from t in ctx.TLSPN_CottonTransactions
                    join s in ctx.TLADM_Cotton on t.cotrx_Supplier_FK equals s.Cotton_Pk
                    where t.cotrx_TransDate >= since
                    orderby t.cotrx_TransDate descending
                    select new
                    {
                        t.cotrx_Return_No,            // GRN
                        t.cotrx_LotNo,
                        t.cotrx_TransDate,
                        Supplier = s.Cotton_Description,
                        t.cotrx_GrossWeight
                    }
                ).ToList();

                cmbDeliveries.DataSource = list;
                cmbDeliveries.DisplayMember = "cotrx_Return_No"; // we’ll format display text below
                cmbDeliveries.ValueMember = "cotrx_Return_No";

                // Pretty display
                cmbDeliveries.Format += (s, e) =>
                {
                    dynamic it = e.ListItem;
                    e.Value = $"GRN {it.cotrx_Return_No} | Lot {it.cotrx_LotNo} | {((DateTime)it.cotrx_TransDate):dd/MM/yy} | {it.Supplier} | Supplier Gross {it.cotrx_GrossWeight:N1} kg";
                };
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string html;
                if (rbSingle.Checked)
                {
                    if (cmbDeliveries.SelectedValue == null) { MessageBox.Show("Please select a delivery (GRN)."); return; }
                    int grn = (int)cmbDeliveries.SelectedValue;
                    html = rptCottonDeliveryVariance.BuildSingle(grn);
                }
                else
                {
                    var from = dtFrom.Value.Date;
                    var to = dtTo.Value.Date.AddDays(1).AddTicks(-1); // inclusive
                    html = rptCottonDeliveryVariance.BuildRange(from, to, chkGroupBySupplier.Checked);
                }

                var path = Path.Combine(Path.GetTempPath(), $"CottonVariance_{DateTime.Now:yyyyMMdd_HHmmss}.html");
                File.WriteAllText(path, html, Encoding.UTF8);
                Process.Start(path); // open in default browser for print
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to generate report:\r\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


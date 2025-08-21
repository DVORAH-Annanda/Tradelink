using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Spinning
{
    public partial class frmCottonDeliverySampleBales : Form
    {
        // --- Config ---
        private readonly int _totalBales;
        private readonly DateTime? _deliveryDate;
        private readonly string _lotNumber;
        private const decimal DiffThresholdPct = 7.5m; // 7.5%

        // --- UI ---
        //private DataGridView dgvSampleBales;
        //private Label lblMinRows, lblTotals, lblStatus;
        //private TextBox txtOverrideReason;
        //private Button btnUpdate, btnCancel;

        // Column names (for code safety)
        private const string ColBaleNo = "SampleBaleNo";
        private const string ColSupp = "SupplierWeight";
        private const string ColTts = "TTSWeight";
        private const string ColDiff = "WeightDifference";
        private const string ColDiffPct = "DifferencePct";
        private const string ColOverride = "Override";

        public frmCottonDeliverySampleBales(int totalBales,
                                            DateTime? deliveryDate = null,
                                            string lotNumber = null)
        {
            InitializeComponent(); // Designer is minimal; we lay out controls in code for clarity.
            _totalBales = Math.Max(0, totalBales);
            _deliveryDate = deliveryDate;
            _lotNumber = lotNumber ?? string.Empty;

            Text = "Cotton Delivery – Sample Bales";
            //Width = 900;
            //Height = 600;
            StartPosition = FormStartPosition.CenterParent;

            BuildUi();
            SeedInitialRows();
            RecalcTotalsAndValidate();
        }

        private void BuildUi()
        {
            var panelTop = new Panel 
            {
                Height = 35,
                Width = this.ClientSize.Width - 45,  
                Location = new Point(25, 25)
            };
            var lblHeader = new Label
            {
                AutoSize = true,
                Font = new Font(Font, FontStyle.Bold),
                Text = $"Delivery: {(_deliveryDate?.ToString("yyyy-MM-dd") ?? "n/a")}     Lot: {(_lotNumber ?? "n/a")}     Total bales: {_totalBales}"
            };
            panelTop.Controls.Add(lblHeader);
            Controls.Add(panelTop);

            // Columns
            dgvSampleBales.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Sample Bale No",
                Name = ColBaleNo,
                Width = 165
            });

            dgvSampleBales.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Supplier Weight",
                Name = ColSupp,
                Width = 125,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N1" }
            });

            dgvSampleBales.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "TTS Weight",
                Name = ColTts,
                Width = 125,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N1" }
            });

            dgvSampleBales.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Weight Difference",
                Name = ColDiff,
                ReadOnly = true,
                Width = 125,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N1", ForeColor = Color.DimGray }
            });

            dgvSampleBales.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Weight Difference %",
                Name = ColDiffPct,
                ReadOnly = true,
                Width = 125,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2", ForeColor = Color.DimGray }
            });

            dgvSampleBales.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "Override?",
                Name = ColOverride,
                Width = 65
            });

            dgvSampleBales.CellEndEdit += Dgv_CellEndEdit;
            dgvSampleBales.RowsRemoved += (_, __) => RecalcTotalsAndValidate();
            dgvSampleBales.RowsAdded += (_, __) => RecalcTotalsAndValidate();
            dgvSampleBales.CellValueChanged += (_, e) =>
            {
                if (e.RowIndex >= 0 && (e.ColumnIndex == dgvSampleBales.Columns[ColSupp].Index || e.ColumnIndex == dgvSampleBales.Columns[ColTts].Index))
                    RecalcRow(e.RowIndex, true);
            };
            dgvSampleBales.CurrentCellDirtyStateChanged += (_, __) =>
            {
                if (dgvSampleBales.IsCurrentCellDirty) dgvSampleBales.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            Controls.Add(dgvSampleBales);

            var panelBottom = new Panel { Dock = DockStyle.Bottom, Height = 185, Padding = new Padding(25) };

            lblMinRows = new Label { AutoSize = true, Top = 65, Padding = new Padding(25) };
            lblMinRows.Text = $"Minimum sample size (10% of total): {MinSampleRowsRequired()} row(s).";
            panelBottom.Controls.Add(lblMinRows);

            lblTotals = new Label { AutoSize = true, Top = 85, Padding = new Padding(25) };
            panelBottom.Controls.Add(lblTotals);

            lblStatus = new Label
            {
                AutoSize = true,
                Top = 105,
                Padding = new Padding(25),
                ForeColor = Color.MidnightBlue
            };
            panelBottom.Controls.Add(lblStatus);

            var lblReason = new Label { Text = "Override reason (required if any row overridden):", AutoSize = true, Top = 125 };
            panelBottom.Controls.Add(lblReason);

            txtOverrideReason = new TextBox { Left = lblReason.Right + 8, Top = 135, Width = 500, Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top };
            panelBottom.Controls.Add(txtOverrideReason);

            btnUpdate = new Button { Text = "Update", Width = 100, Anchor = AnchorStyles.Right | AnchorStyles.Top };
            btnUpdate.Left = panelBottom.Width - btnUpdate.Width - 110;
            btnUpdate.Top = 135;
            btnUpdate.Click += BtnUpdate_Click;
            panelBottom.Controls.Add(btnUpdate);

            btnCancel = new Button { Text = "Cancel", Width = 100, Anchor = AnchorStyles.Right | AnchorStyles.Top };
            btnCancel.Left = panelBottom.Width - btnCancel.Width - 5;
            btnCancel.Top = 135;
            btnCancel.Click += (_, __) => DialogResult = DialogResult.Cancel;
            panelBottom.Controls.Add(btnCancel);

            panelBottom.Resize += (_, __) =>
            {
                btnCancel.Left = panelBottom.Width - btnCancel.Width - 5;
                btnUpdate.Left = btnCancel.Left - btnUpdate.Width - 10;
                txtOverrideReason.Width = btnUpdate.Left - txtOverrideReason.Left - 10;
            };

            Controls.Add(panelBottom);
        }

        private int MinSampleRowsRequired()
        {
            // At least 10% of total bales, rounded up; if total unknown/zero, allow at least 1.
            if (_totalBales <= 0) return 1;
            return Math.Max(1, (int)Math.Ceiling(_totalBales * 0.10m));
        }

        private void SeedInitialRows()
        {
            // Seed blank rows equal to min sample size (user may add/remove later)
            int target = MinSampleRowsRequired();
            for (int i = 0; i < target; i++)
                dgvSampleBales.Rows.Add();
        }

        private void Dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == dgvSampleBales.Columns[ColSupp].Index || e.ColumnIndex == dgvSampleBales.Columns[ColTts].Index)
                RecalcRow(e.RowIndex, true);
        }

        private static decimal ParseCellDecimal(object cellValue)
        {
            if (cellValue == null) return 0m;
            var s = Convert.ToString(cellValue)?.Trim();
            if (string.IsNullOrEmpty(s)) return 0m;
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out var val))
                return val;
            return 0m;
        }

        private void RecalcRow(int rowIndex, bool updateTotalsAfter = false)
        {
            if (rowIndex < 0 || rowIndex >= dgvSampleBales.Rows.Count) return;
            var row = dgvSampleBales.Rows[rowIndex];
            if (row.IsNewRow) return;

            decimal supplier = ParseCellDecimal(row.Cells[ColSupp].Value);
            decimal tts = ParseCellDecimal(row.Cells[ColTts].Value);

            decimal diff = tts - supplier;
            decimal pct = (supplier == 0m) ? 0m : (diff / supplier) * 100m;

            row.Cells[ColDiff].Value = diff;
            row.Cells[ColDiffPct].Value = pct;

            // Color row if beyond threshold and not overridden
            bool overridden = Convert.ToBoolean(row.Cells[ColOverride].Value ?? false);
            bool breach = Math.Abs(pct) > DiffThresholdPct;
            row.DefaultCellStyle.BackColor = (breach && !overridden) ? Color.MistyRose : Color.White;

            if (updateTotalsAfter) RecalcTotalsAndValidate();
        }

        private void RecalcTotalsAndValidate()
        {
            decimal totalSupp = 0m, totalTts = 0m;
            int rowCount = 0;
            bool anyBreachWithoutOverride = false;
            bool anyOverride = false;

            foreach (DataGridViewRow r in dgvSampleBales.Rows)
            {
                if (r.IsNewRow) continue;
                rowCount++;

                decimal supplier = ParseCellDecimal(r.Cells[ColSupp].Value);
                decimal tts = ParseCellDecimal(r.Cells[ColTts].Value);
                totalSupp += supplier;
                totalTts += tts;

                decimal diff = tts - supplier;
                decimal pct = (supplier == 0m) ? 0m : (diff / supplier) * 100m;

                bool overridden = Convert.ToBoolean(r.Cells[ColOverride].Value ?? false);
                bool breach = Math.Abs(pct) > DiffThresholdPct;
                if (breach && !overridden) anyBreachWithoutOverride = true;
                if (overridden) anyOverride = true;
            }

            decimal totalDiff = totalTts - totalSupp;
            decimal totalPct = (totalSupp == 0m) ? 0m : (totalDiff / totalSupp) * 100m;

            lblTotals.Text = $"Totals: Supplier: {totalSupp:N1}     TTS: {totalTts:N1}     Weight Difference: {totalDiff:N1}     Weight Difference %: {totalPct:N2}%";

            var issues = new List<string>();
            int minRows = MinSampleRowsRequired();
            if (rowCount < minRows)
                issues.Add($"Sample size too small: {rowCount} rows. Need at least {minRows} (10% of total).");

            if (anyBreachWithoutOverride)
                issues.Add($"One or more rows exceed ±{DiffThresholdPct:N1}% and are not overridden.");

            if (anyOverride && string.IsNullOrWhiteSpace(txtOverrideReason.Text))
                issues.Add("Override reason is required when any row is overridden.");

            if (issues.Count == 0)
            {
                lblStatus.ForeColor = Color.ForestGreen;
                lblStatus.Text = "All checks passed.";
                btnUpdate.Enabled = true;
            }
            else
            {
                lblStatus.ForeColor = Color.DarkRed;
                lblStatus.Text = string.Join("  ", issues);
                btnUpdate.Enabled = false;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Final revalidate
            RecalcTotalsAndValidate();
            if (!btnUpdate.Enabled) return;

            // Confirm if there are overrides
            bool anyOverride = dgvSampleBales.Rows.Cast<DataGridViewRow>()
                .Any(r => !r.IsNewRow && Convert.ToBoolean(r.Cells[ColOverride].Value ?? false));
            if (anyOverride)
            {
                var resp = MessageBox.Show(
                    "Some rows were overridden. Do you want to proceed with Update?",
                    "Confirm Update",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resp != DialogResult.Yes) return;
            }

            // Gather data
            var items = new List<SampleBaleRow>();
            foreach (DataGridViewRow r in dgvSampleBales.Rows)
            {
                if (r.IsNewRow) continue;

                var item = new SampleBaleRow
                {
                    SampleBaleNo = Convert.ToString(r.Cells[ColBaleNo].Value)?.Trim(),
                    SupplierWeight = ParseCellDecimal(r.Cells[ColSupp].Value),
                    TTSWeight = ParseCellDecimal(r.Cells[ColTts].Value),
                    WeightDifference = ParseCellDecimal(r.Cells[ColDiff].Value),
                    DifferencePct = ParseCellDecimal(r.Cells[ColDiffPct].Value),
                    Overridden = Convert.ToBoolean(r.Cells[ColOverride].Value ?? false)
                };
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(item.SampleBaleNo) && item.SupplierWeight == 0m && item.TTSWeight == 0m)
                    continue;

                items.Add(item);
            }

            // TODO: Persist to your DB via EF. Example stub:
            try
            {
                SaveToDatabase(items, txtOverrideReason.Text?.Trim());
                MessageBox.Show("Sample bales saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveToDatabase(List<SampleBaleRow> items, string overrideReason)
        {
            // Wire this to your EF context and target tables.
            // Example (pseudo):
            //
            // using (var ctx = new TTI2Entities())
            // {
            //     var header = new TLSPN_CottonSampleHeader
            //     {
            //         DeliveryDate = _deliveryDate,
            //         LotNumber = _lotNumber,
            //         TotalBales = _totalBales,
            //         OverrideReason = overrideReason,
            //         InsertedOn = DateTime.Now
            //     };
            //     ctx.TLSPN_CottonSampleHeader.Add(header);
            //     ctx.SaveChanges();
            //
            //     foreach (var it in items)
            //     {
            //         var row = new TLSPN_CottonSampleDetail
            //         {
            //             Header_FK = header.Id,
            //             SampleBaleNo = it.SampleBaleNo,
            //             SupplierWeight = it.SupplierWeight,
            //             TTSWeight = it.TTSWeight,
            //             WeightDifference = it.WeightDifference,
            //             DifferencePct = it.DifferencePct,
            //             Overridden = it.Overridden
            //         };
            //         ctx.TLSPN_CottonSampleDetail.Add(row);
            //     }
            //     ctx.SaveChanges();
            // }
        }

        // Keep totals/validation in sync
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            RecalcTotalsAndValidate();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dgvSampleBales.CellValueChanged += (_, __) => RecalcTotalsAndValidate();
            dgvSampleBales.UserDeletedRow += (_, __) => RecalcTotalsAndValidate();
            dgvSampleBales.UserAddedRow += (_, __) => RecalcTotalsAndValidate();
            txtOverrideReason.TextChanged += (_, __) => RecalcTotalsAndValidate();
        }

        private sealed class SampleBaleRow
        {
            public string SampleBaleNo { get; set; }
            public decimal SupplierWeight { get; set; }
            public decimal TTSWeight { get; set; }
            public decimal WeightDifference { get; set; }
            public decimal DifferencePct { get; set; }
            public bool Overridden { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
namespace Cutting
{
    public partial class frmSelRejectedPanel : Form
    {
        bool formloaded;
        int sortOptions;
        int _RepNo;

        public frmSelRejectedPanel(int RepNo)
        {
            InitializeComponent();
            if (RepNo == 1)
                groupBox1.Visible = false;

            _RepNo = RepNo;
        }

        private void frmSelRejectedPanel_Leave(object sender, EventArgs e)
        {

        }

        private void frmSelRejectedPanel_Load(object sender, EventArgs e)
        {
            formloaded = false;

            if (_RepNo == 1)
            {
                sortOptions = 1;

                var reportOptions = new BindingList<KeyValuePair<int, string>>();
                reportOptions.Add(new KeyValuePair<int, string>(1, "CutSheet Number"));
                reportOptions.Add(new KeyValuePair<int, string>(2, "Quality"));
                reportOptions.Add(new KeyValuePair<int, string>(3, "Colour"));
                reportOptions.Add(new KeyValuePair<int, string>(4, "Customer"));
                cmboReportSelection.DataSource = reportOptions;
                cmboReportSelection.ValueMember = "Key";
                cmboReportSelection.DisplayMember = "Value";
                cmboReportSelection.SelectedIndex = -1;
            }
            
            formloaded = true;
        }

        private void cmboReportSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                sortOptions = Convert.ToInt32(oCmbo.SelectedValue);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                CutReportOptions cutOpts = new CutReportOptions();
                cutOpts.C3SortOption = sortOptions;
                cutOpts.RepNo = _RepNo;

                if (_RepNo == 1)
                {
                    frmCutViewRep vRep = new frmCutViewRep(7, cutOpts);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog();
                    if (vRep != null)
                    {
                        vRep.Close();
                        vRep.Dispose();
                    }
                }
                else
                {
                    cutOpts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                    cutOpts.toDate   = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                    cutOpts.toDate   = cutOpts.toDate.AddHours(23);
                    frmCutViewRep vRep = new frmCutViewRep(8, cutOpts);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog();
                    if (vRep != null)
                    {
                        vRep.Close();
                        vRep.Dispose();
                    }
                }
                sortOptions = 1;
            }
        }
    }
}

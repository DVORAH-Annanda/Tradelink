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

namespace DyeHouse
{
    public partial class frmDyeOrdersSel : Form
    {
        public frmDyeOrdersSel()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Util core = new Util();
            DyeReportOptions dro = new DyeReportOptions();

            //------------------------------------------------------------
            // 
            if (rbGarmentsOnly.Checked)
                dro.SelGarments = true;
            else if (rbFabricOnly.Checked)
                dro.SelFabric = true;
            else if (rbBoth.Checked)
                dro.SelBoth = true;

            if (rbAdditionalBoth.Checked)
                dro.DO_Both = true;
            else if (rbOrderOpen.Checked)
                dro.DO_Open = true;
            else if(rbOrderClosed.Checked)
                dro.DO_Closed = true;

            if (cmboReportOptions.SelectedValue != null)
            {
                dro.DO_OptionSelected = int.Parse(cmboReportOptions.SelectedValue.ToString());
                if (dro.DO_OptionSelected == 0)
                    dro.DO_ByOrderNo = true;
            }
            else
            {
                dro.DO_ByOrderNo = true;
            }


            frmDyeViewReport vRep = new frmDyeViewReport(14, dro);
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            vRep.ClientSize = new Size(w, h);
            vRep.ShowDialog(this);
        }

        private void frmDyeOrdersSel_Load(object sender, EventArgs e)
        {
            var reportOptions = new BindingList<KeyValuePair<string, string>>();
            reportOptions.Add(new KeyValuePair<string, string>("0", "Dye Order Number"));
           //reportOptions.Add(new KeyValuePair<string, string>("1", "Date Created"));
            reportOptions.Add(new KeyValuePair<string, string>("2", "Quality"));
            reportOptions.Add(new KeyValuePair<string, string>("3", "Colour"));
            reportOptions.Add(new KeyValuePair<string, string>("4", "Customer"));
            // reportOptions.Add(new KeyValuePair<string, string>("5", "Due Date Sequence"));
            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;
        }
    }
}

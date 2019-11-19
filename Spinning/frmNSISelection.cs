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

namespace Spinning
{
    public partial class frmNSISelection : Form
    {
        bool _Whch;
        public frmNSISelection( bool Whch)
        {
            InitializeComponent();
            SetUp();
            _Whch = Whch;
        }

        void SetUp()
        {
            if (_Whch)
                this.Text = "Non Stock Items Consumption (Theoritical)";
            else
                this.Text = "Capacity Utilisation";

            rbAllMachines.Checked = true;

            using (var context = new TTI2Entities())
            {
                ComboMachines.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_MachineCode.Contains("AC")).ToList();
                ComboMachines.ValueMember = "MD_Pk";
                ComboMachines.DisplayMember = "MD_MachineCode";
                ComboMachines.Visible = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null)
            {
                if (oRad.Checked)
                    ComboMachines.Visible = true;
                else
                    ComboMachines.Visible = false;
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                NSISelection nsiSel = new NSISelection();
                nsiSel.NSI = _Whch;

                if (dtpFromDate.Value != null)
                    nsiSel.fromDate = dtpFromDate.Value;
                else
                    nsiSel.fromDate = DateTime.Now;
                if (dtpToDate.Value != null)
                    nsiSel.toDate = dtpToDate.Value;
                else
                    nsiSel.toDate = DateTime.Now;

                if (rbAllMachines.Checked)
                    nsiSel.allmachines = true;
                else
                {
                    var machine = (TLADM_MachineDefinitions)ComboMachines.SelectedItem;
                    if (machine != null)
                        nsiSel.machineKey = machine.MD_Pk;
                    else
                    {
                        MessageBox.Show("Please select a machine from the drop down list provided");
                        return;
                    }

                }

                nsiSel.toDate = Convert.ToDateTime(nsiSel.toDate.ToShortDateString()).AddHours(23);
                nsiSel.fromDate = Convert.ToDateTime(nsiSel.fromDate.ToShortDateString());

                frmViewReport vRep = new frmViewReport(15, nsiSel);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }

    public class NSISelection
    {
        public NSISelection()
        {
        }

        public bool NSI { get; set; } 
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public bool allmachines { get; set; }
        public int machineKey { get; set; } 
    }
}

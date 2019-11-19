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
    public partial class frmYarnProdSel : Form
    {
        bool QASummary;

        public frmYarnProdSel()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {
            using ( var context = new TTI2Entities())
            {
                cmbSpecificOrder.DataSource = context.TLSPN_YarnOrder.Where(x=>!x.Yarno_Closed).ToList();
                cmbSpecificOrder.ValueMember = "YarnO_Pk";
                cmbSpecificOrder.DisplayMember = "YarnO_OrderNumber";
            }
            rbNo.Checked = true;
            rbSpecificNo.Checked = true;

            cmbSpecificOrder.Visible = false;
        }

        private void rbSpecificYes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if(oRad != null)
            {
                if(oRad.Checked)
                    cmbSpecificOrder.Visible = true;
                else
                    cmbSpecificOrder.Visible = false;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {

                YarnProductionSel ysel = new YarnProductionSel();
                ysel.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                ysel.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                
                if (rbYes.Checked)
                    ysel.IncludeClosed = true;
                
                if (rbSpecificYes.Checked)
                {
                    var OrderDt = (TLSPN_YarnOrder)cmbSpecificOrder.SelectedItem;
                    if (OrderDt == null)
                    {
                        MessageBox.Show("Please select an order number from the drop down list ");
                        return;
                    }
                    
                    ysel.orderKey = OrderDt.YarnO_Pk;
                    ysel.SpecificOrder = true;
                    ysel.IncludeClosed = false;

                }

                if (chkQASummary.Checked)
                    ysel.QASummary = true;

                frmViewReport vRep = new frmViewReport(16, ysel);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }

    public class YarnProductionSel
    {
        public YarnProductionSel()
        {
        }

        public DateTime fromDate { get; set;}
        public DateTime toDate { get; set;}
        public bool IncludeClosed { get; set;}
        public bool SpecificOrder { get; set;} 
        public Int32 orderKey { get; set;}
        public bool QASummary { get; set; } 
    }
}

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

namespace Knitting
{
    public partial class frmGreigeProductionSel : Form
    {
        public frmGreigeProductionSel()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                cmbGreigeProduct.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmbGreigeProduct.DisplayMember = "TLGreige_Description";
                cmbGreigeProduct.ValueMember = "TLGreige_Id";

                cmbKnittingCustomer.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmbKnittingCustomer.DisplayMember = "Cust_Description";
                cmbKnittingCustomer.ValueMember = "Cust_Pk";

                var Dept = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
                    cmbMachines.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    cmbMachines.DisplayMember = "MD_Description";
                    cmbMachines.ValueMember = "MD_Pk";

                    cmbOperator.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id).OrderBy(x => x.MachOp_Description).ToList();
                    cmbOperator.DisplayMember = "MachOP_Description";
                    cmbOperator.ValueMember = "MachOP_Pk";
                }
            }
        }
    }
}

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
    public partial class frmSelResourceEffic : Form
    {
        bool formloaded;

        public frmSelResourceEffic()
        {
            InitializeComponent();
        }

        private void frmSelResourceEffic_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboMachines.DataSource = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    cmboMachines.DisplayMember = "MD_AlternateDesc";
                    cmboMachines.ValueMember = "MD_Pk";
                    cmboMachines.SelectedIndex = -1;

                    cmboFabricType.DataSource = context.TLADM_FabricProduct.ToList();
                    cmboFabricType.ValueMember = "FP_Id";
                    cmboFabricType.DisplayMember = "FP_Description";
                    cmboFabricType.SelectedIndex = -1;
                }
            }

            formloaded = true;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                DyeReportOptions RepOptions = new DyeReportOptions();

                RepOptions.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                RepOptions.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                RepOptions.toDate = RepOptions.toDate.AddHours(23);

                var selected = (TLADM_MachineDefinitions)cmboMachines.SelectedItem;
                if (selected != null)
                {
                    RepOptions.ResEficMachine = selected.MD_Pk;
                }

                if (rbDyeFirstTime.Checked)
                {
                    RepOptions.ResEfficTrans = true; 
                }

                var FTSelected = (TLADM_FabricProduct)cmboFabricType.SelectedItem;
                if (FTSelected != null)
                {
                    RepOptions.ResFabType = FTSelected.FP_Id;
                }

                frmDyeViewReport ViewRep = new frmDyeViewReport(35, RepOptions);
                ViewRep.ShowDialog(this);
                formloaded = false;
                cmboFabricType.SelectedIndex = -1;
                cmboMachines.SelectedIndex = -1;
                formloaded = true;
            }
        }
    }
}

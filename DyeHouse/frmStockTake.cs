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
    public partial class frmStockTake : Form
    {
        bool formloaded;
        DyeReportOptions repOps = null;

        public frmStockTake()
        {
            InitializeComponent();
        }

        private void frmStockTake_Load(object sender, EventArgs e)
        {
            formloaded = false;
            repOps = new DyeReportOptions();

            using ( var context = new TTI2Entities())
            {
                var Deptment = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if(Deptment != null)
                {
                    cmboStore.DataSource = context.TLADM_WhseStore.Where(x=>x.WhStore_DepartmentFK == Deptment.Dep_Id).ToList();
                    cmboStore.ValueMember = "WhStore_Id";
                    cmboStore.DisplayMember = "WhStore_Description";
                    cmboStore.SelectedValue = -1;
                }

                cmboStockType.DataSource = context.TLADM_StockTypes.Where(x=>x.ST_ShortCode.Contains("DC")).ToList();
                cmboStockType.ValueMember = "ST_Id";
                cmboStockType.DisplayMember = "ST_Description";
                cmboStockType.SelectedValue = -1;

                cmboStockTake.DataSource = context.TLADM_StockTakeFreq.ToList();
                cmboStockTake.ValueMember = "STF_Pk";
                cmboStockTake.DisplayMember = "STF_Description";
                cmboStockTake.SelectedValue = -1;

            }
            formloaded = true;

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                if (repOps.stkWhseSelected == false &&
                    repOps.stkTakeCategorySelected == false &&
                    repOps.stkTypeSelected == false)
                {
                    MessageBox.Show("Please select one option from the drop down box");
                    return;


                }

                frmDyeViewReport vRep = new frmDyeViewReport(25, repOps);
                vRep.ShowDialog(this);
                repOps = new DyeReportOptions();
            }
        }

        private void cmboStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                if (oCmbo.Name == "cmboStore")
                {
                    repOps.stkWhseSelected = true;
                    repOps.stkWhseIndex = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboStockType")
                {
                    repOps.stkTypeSelected = true;
                    repOps.stkStockTypeIndex = (int)oCmbo.SelectedValue;
                }
                else if (oCmbo.Name == "cmboStockTake")
                {
                    repOps.stkTakeCategorySelected = true;
                    repOps.stkTakeCategory = (int)oCmbo.SelectedValue;
                }

            }
        }
            
    }
}

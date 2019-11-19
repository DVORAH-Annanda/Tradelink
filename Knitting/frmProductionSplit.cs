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
    public partial class frmProductionSplit : Form
    {
        int _RecordKey;
        Util core;
        TLKNI_GreigeProduction GreigeProd = null;

        bool formloaded;

        public frmProductionSplit( int RecordKey)
        {
            InitializeComponent();
            _RecordKey = RecordKey;

            core = new Util();

            txtReasons.Text = string.Empty;

            txtAdjustedWeight.KeyPress += core.txtWin_KeyPress;
            txtAdjustedWeight.KeyDown += core.txtWin_KeyDownOEM;

            txtAdjustedWeight.Text = "0.0";

            radRNumber.Checked = true;

        }

        private void frmProductionSplit_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                GreigeProd = context.TLKNI_GreigeProduction.Find(_RecordKey);
                if (GreigeProd != null)
                {
                    if (GreigeProd.GreigeP_KnitO_Fk != null)
                    {
                        var KO = context.TLKNI_Order.Find(GreigeProd.GreigeP_KnitO_Fk);
                        if (KO != null)
                        {
                            lblKnitOrder.Text = "KO" + KO.KnitO_OrderNumber.ToString();
                        }

                        txtWeightAvailable.Text = GreigeProd.GreigeP_weightAvail.ToString();
                        label4.Text = context.TLADM_Griege.Find(GreigeProd.GreigeP_Greige_Fk).TLGreige_Description;

                    }
                }
            }
            formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLKNI_Order KO = null;
          
            if (oBtn != null && formloaded)
            {
                if(Decimal.Parse(txtAdjustedWeight.Text) > 0)
                {
                    Decimal Weight = Decimal.Parse(txtAdjustedWeight.Text);
                    if (GreigeProd.GreigeP_weight - Weight >= 0)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var GP = context.TLKNI_GreigeProduction.Find(GreigeProd.GreigeP_Pk);
                            if (GP != null)
                            {
                                if (GP.GreigeP_KnitO_Fk != null)
                                {
                                    KO = context.TLKNI_Order.Find(GP.GreigeP_KnitO_Fk);
                                    if (KO != null)
                                    {

                                        //============================================
                                        // Create an actual Production split record
                                        //=============================================
                                        TLKNI_ProductionSplit ProdSplit = new TLKNI_ProductionSplit();
                                        ProdSplit.TLKNISP_FromStore_FK = (int)GreigeProd.GreigeP_Store_FK;
                                        if (radKNumber.Checked)
                                            ProdSplit.TLKNISP_NewPieceNo = "K" + GreigeProd.GreigeP_PieceNo.ToString();
                                        else
                                            ProdSplit.TLKNISP_NewPieceNo = "R" + GreigeProd.GreigeP_PieceNo.ToString();

                                        ProdSplit.TLKNISP_Date = DateTime.Now;
                                        ProdSplit.TLKNISP_OrigPieceNo = GreigeProd.GreigeP_PieceNo;
                                        ProdSplit.TLKNISP_Notes = txtReasons.Text;
                                        ProdSplit.TLKNISP_Mass = GP.GreigeP_weight - Weight;
                                        ProdSplit.TLKNISP_KnitOrder_FK = KO.KnitO_Pk;
                                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                                        if (Dept != null)
                                        {
                                            var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 2005).FirstOrDefault();
                                            if (TranType != null)
                                            {
                                                ProdSplit.TLKNISP_ToStore_FK = (int)TranType.TrxT_ToWhse_FK;
                                            }
                                        }

                                        context.TLKNI_ProductionSplit.Add(ProdSplit);

                                        //================================================
                                        // Now Adjust the actual GP record with new weight
                                        //=========================================
                                        GP.GreigeP_weight = Weight;
                                        GP.GreigeP_weightAvail = Weight;
                                    }
                                }
                            }
                            try
                            {
                                context.SaveChanges();
                                MessageBox.Show("Data successfully saved to database");
                                this.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cannot split more than what was originally Knitted");
                    }
                }
            }
        }
    }
}

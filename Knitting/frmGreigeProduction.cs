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
    public partial class frmGreigeProduction : Form
    {
        Util core;
        bool formloaded;
        TLKNI_Order _KO;

        public frmGreigeProduction(TLKNI_Order KO)
        {
            InitializeComponent();
            core = new Util();
            txtKnitOrderWeight.KeyDown += core.txtWin_KeyDownOEM;
            txtKnitOrderWeight.KeyPress += core.txtWin_KeyPress;
            txtKOPieces.KeyDown += core.txtWin_KeyDown;
            txtKOPieces.KeyPress += core.txtWin_KeyPress;

            _KO = KO;

            SetUp();
        }


        void SetUp()
        {
            formloaded = false;

            using (var context = new TTI2Entities())
            {
                var Product = context.TLADM_Griege.Find(_KO.KnitO_Product_FK);
                txtGreigeProduct.Text = Product.TLGreige_Description;

                txtKnitOrderWeight.Text = _KO.KnitO_Weight.ToString();
                txtKOPieces.Text = _KO.KnitO_NoOfPieces.ToString();
                var Machine = context.TLADM_MachineDefinitions.Find(_KO.KnitO_Machine_FK);
                txtMachDetails.Text = Machine.MD_MachineCode;

                if (_KO.KnitO_YarnO_FK != null && _KO.KnitO_YarnO_FK != 0)
                {
                  txtYarnOrder.Text = context.TLSPN_YarnOrder.Find(_KO.KnitO_YarnO_FK).YarnO_OrderNumber.ToString();
                }
            }
            
            formloaded = true;
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            string Code = string.Empty;
            int LN = 0;

            if (oBtn != null && formloaded)
            {
               using (var context = new TTI2Entities())
               {
                        var TotalPieces = _KO.KnitO_NoOfPieces;
                        var I = 1;
                        /*
                        var Machine = context.TLADM_MachineDefinitions.Where(x=>x.MD_Pk == _KO.KnitO_Machine_FK).FirstOrDefault();
                        if(Machine != null)
                        {
                            Code = Machine.MD_MachineCode;
                            Code = Code.Substring(1, -1 + Code.Length);
                            LN = Machine.MD_LastNumberUsed;
                        }

                        do
                        {
                            TLKNI_GreigeProduction greigP = new TLKNI_GreigeProduction();
                            greigP.GreigeP_KnitO_Fk = _KO.KnitO_Pk;
                            greigP.GreigeP_Greige_Fk = _KO.KnitO_Product_FK;
                            greigP.GreigeP_PieceNo = Code + (LN++).ToString();
                            greigP.GreigeP_weight = 0.00M;

                            context.TLKNI_GreigeProduction.Add(greigP);
                          
                            I++;
                        } while (I <= TotalPieces);

                        Machine = context.TLADM_MachineDefinitions.Find(Machine.MD_Pk);
                        if(Machine != null)
                            context.UpdateLastNumber(LN, Machine.MD_MachineCode);
                        //LastNumberUsed.col6 = LN;
                        */
                        try
                        {
                            // context.SaveChanges();
                            frmKnitViewRep vRep = new frmKnitViewRep(2, _KO.KnitO_Pk);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
             }
      
      }
    }
}

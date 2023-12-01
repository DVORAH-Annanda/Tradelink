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
using Administration;

namespace TTI2_WF
{
    public partial class frmTLADM_Cotton : Form
    {
        bool formloaded;
        bool nonNumeric;
        bool addRecord; 
        string[][] FldNames;
        bool[] FldEntered;
        Util core;

        public 
            frmTLADM_Cotton()
        {
            InitializeComponent();
            core = new Util();
  
            FldNames = new string[][]
            {   new string[] {"txtCode", "Please enter a valid code", "0" , "10"},
                new string[] {"txtDescription", "Please enter a description", "1", "50"}, 
                new string[] {"txtUnits", "Please enter the number of units", "2", "500" },
                new string[] {"txtStdCost", "Please enter a standard cost amount", "3", "500"},
                new string[] {"txtContact", "Please enter a contact person details", "4", "50"},
                new string[] {"txtRol", "Please enter a ROL amount", "5", "500"}, 
                new string[] {"txtRoq", "Please enter a ROQ amount", "6", "500"},
                new string[] {"txtGrade", "Please enter a valid grade", "7", "12"}};
  
           Setup(true);
          
        }

        void Setup(bool updt)
        {
            formloaded = false;

            using (var context = new TTI2Entities())
            {
                cmbCotton.DataSource = context.TLADM_Cotton.OrderBy(x => x.Cotton_Description).ToList();
                cmbCotton.DisplayMember = "Cotton_Description";
                cmbCotton.ValueMember = "Cotton_Pk";

                if (updt)
                {
                    cmbStockType.DataSource = context.TLADM_StockTypes.OrderBy(x => x.ST_ShortCode).ToList();
                    cmbStockType.DisplayMember = "ST_Description";
                    cmbStockType.ValueMember = "ST_Id";

                    cmbUOM.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                    cmbUOM.ValueMember = "UOM_Pk";
                    cmbUOM.DisplayMember = "UOM_Description";

                    cmbOrigin.DataSource = context.TLADM_CottonOrigin.OrderBy(x => x.CottonOrigin_Description).ToList();
                    cmbOrigin.DisplayMember = "CottonOrigin_Description";
                    cmbOrigin.ValueMember = "CottonOrigin_Pk";

                    cmbStore.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore == false).ToList();
                    cmbStore.DisplayMember = "WhStore_Description";
                    cmbStore.ValueMember = "WhStore_Id";

                    cmbCottonAgent.DataSource = context.TLADM_CottonAgent.OrderBy(x => x.CottonAgent_Description).ToList();
                    cmbCottonAgent.ValueMember = "CottonAgent_Pk";
                    cmbCottonAgent.DisplayMember = "CottonAgent_Description";
                }
           

            }

            addRecord = false;
            formloaded = true;
            
            txtStdCost.Visible = true;
            txtUnits.Visible = true;
            rbShowCostNo.Checked = true;
            rbShowQtyNo.Checked = true;

            rtbNotes.Text = string.Empty;

            txtCode.Text        = string.Empty;
            txtDescription.Text = string.Empty;
            txtGrade.Text       = string.Empty;
            txtRol.Text         = string.Empty;
            txtRoq.Text         = string.Empty;
            txtStdCost.Text     = string.Empty;
            txtUnits.Text       = string.Empty;
            txtContact.Text = string.Empty; 
            cmbCotton.SelectedValue   = 0;
            cmbOrigin.SelectedValue   = 0;
            cmbStockType.SelectedValue = 0;
            cmbStore.SelectedValue = 0;
            cmbUOM.SelectedValue = 0;
            cmbCottonAgent.SelectedValue = 0;

            txtRoq.KeyPress += core.txtWin_KeyPress;
            txtRoq.KeyDown += core.txtWin_KeyDownOEM;
            
            txtRol.KeyDown += core.txtWin_KeyDownOEM;
            txtRol.KeyPress += core.txtWin_KeyPress;

            txtUnits.KeyPress += core.txtWin_KeyPress;
            txtUnits.KeyDown += core.txtWin_KeyDownOEM;

            txtStdCost.KeyDown += core.txtWin_KeyDownOEM;
            txtStdCost.KeyPress += core.txtWin_KeyPress;
            
            FldEntered = core.PopulateArray(FldNames.Length, false);

        }

        private void txtWin_KeyDownOem(object sender, KeyEventArgs e)
        {

        }

        private void txtWin_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtWin_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

     

        private void Cotton_TextChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                  var result = (from u in FldNames
                             where u[0] == oTxt.Name
                             select u).FirstOrDefault();

                  if (result != null)
                  {
                      int nbr = Convert.ToInt32(result[2].ToString());
                      int length = Convert.ToInt32(result[3].ToString());

                      if (oTxt.TextLength > 0 && oTxt.TextLength < length)
                          FldEntered[nbr] = true;
                      else
                      {
                          FldEntered[nbr] = false;
                          if (oTxt.TextLength > length)
                              MessageBox.Show("Value entered exceeds allowable length");
                      }
                  }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool lSuccess = true;
            if (oBtn != null && formloaded)
            {
                var Mess = returnMessage(FldEntered);
                if (!string.IsNullOrEmpty(Mess))
                {
                    MessageBox.Show(Mess);
                    return;
                }
                using (var context = new TTI2Entities())
                {
                    TLADM_Cotton ctn = new TLADM_Cotton();
                    if (!addRecord)
                    {
                        var cotton = (TLADM_Cotton)cmbCotton.SelectedItem;
                        if (cotton != null)
                        {
                            ctn = context.TLADM_Cotton.Find(cotton.Cotton_Pk);
                        }
                    }
                    ctn.Cotton_Code = txtCode.Text;
                    ctn.Cotton_Description = txtDescription.Text;
                    ctn.Cotton_Grade = txtGrade.Text;
                    ctn.Cotton_ROL = Convert.ToDecimal(txtRol.Text);
                    ctn.Cotton_ROQ = Convert.ToDecimal(txtRoq.Text);

                    if (rbShowCostYes.Checked)
                        ctn.Cotton_ShowStdCost = true;
                    else
                        ctn.Cotton_ShowStdCost = false;
                    if (rbShowQtyYes.Checked)
                        ctn.Cotton_ShowQty = true;
                    else
                        ctn.Cotton_ShowQty = false;

                    ctn.Cotton_Contact = txtContact.Text;
                    ctn.Cotton_Units = Convert.ToInt32(txtUnits.Text);
                    ctn.Cotton_StdCost = Math.Round(Convert.ToDecimal(txtStdCost.Text),2);
                    ctn.Cotton_Notes = rtbNotes.Text;
                    //----------------------------------------------------------
                    var uom = (TLADM_UOM)cmbUOM.SelectedItem;
                    if (uom != null)
                    {
                        ctn.Cotton_UOM_Fk = uom.UOM_Pk;
                    }
                    else
                        ctn.Cotton_UOM_Fk = 1;
                
                    //---------------------------------------------------------------------
                    var store = (TLADM_WhseStore)cmbStore.SelectedItem;
                    if (store != null)
                        ctn.Cotton_Store_FK = store.WhStore_Id;
                    else
                        ctn.Cotton_Store_FK = 1;
                
                    //--------------------------------------------------------------------
                    var stock = (TLADM_StockTypes)cmbStockType.SelectedItem;
                    if (stock != null)
                        ctn.Cotton_StockType_FK = stock.ST_Id;
                    else
                        ctn.Cotton_StockType_FK = 1;
                
                    //-----------------------------------------------------------------------
                    var origin = (TLADM_CottonOrigin)cmbOrigin.SelectedItem;
                    if (origin != null)
                        ctn.Cotton_Origin_FK = origin.CottonOrigin_Pk;
                    else
                        ctn.Cotton_Origin_FK = 1;

                    //-----------------------------------------------------------------------
                    var agents = (TLADM_CottonAgent)cmbCottonAgent.SelectedItem; 
                    if (agents != null)
                        ctn.Cotton_Agent_FK = agents.CottonAgent_Pk;
                    else
                        ctn.Cotton_Agent_FK = 1;
                
                    if (addRecord)
                        context.TLADM_Cotton.Add(ctn);

                    try
                    {
                            context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                            MessageBox.Show(ex.Message);
                            lSuccess = false;
                    }
                }
                if (lSuccess)
                {
                    MessageBox.Show("Records saved to database successfully");
                    Setup(false);
                }
            }
        }

        private string returnMessage(bool[] selectedarray)
        {
            int Cnt = 0;
            StringBuilder Mess = new StringBuilder();

            foreach (var ArrayElement in selectedarray)
            {
                if (bool.FalseString == ArrayElement.ToString())
                {
                    var result = (from u in FldNames
                                  where u[2] == Cnt.ToString()
                                  select u).FirstOrDefault();

                    Mess.Append(result[1] + Environment.NewLine);
                }
                
                Cnt += 1;
            }
            return Mess.ToString();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                Setup(false);
                addRecord = true;
               
            }
        }

        private void cmbCotton_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var cotton = (TLADM_Cotton)cmbCotton.SelectedItem;
                if (cotton != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        TLADM_Cotton newcotton = context.TLADM_Cotton.Find(cotton.Cotton_Pk);
                        txtCode.Text = newcotton.Cotton_Code;
                        txtDescription.Text = newcotton.Cotton_Description;
                        txtGrade.Text = newcotton.Cotton_Grade;
                        txtRol.Text = Math.Round(newcotton.Cotton_ROL,2).ToString();
                        txtRoq.Text = Math.Round(newcotton.Cotton_ROQ,2).ToString();
                        txtContact.Text = newcotton.Cotton_Contact;
                         if (newcotton.Cotton_ShowQty)
                        {
                            rbShowQtyYes.Checked = true;
                            txtUnits.Visible = true;
                        }
                        else
                        {
                            rbShowQtyNo.Checked = true;
                            txtUnits.Visible = false;
                        }
                        if (newcotton.Cotton_ShowStdCost)
                        {
                            rbShowCostYes.Checked = true;
                            txtStdCost.Visible = true;
                        }
                        else
                        {
                            rbShowQtyNo.Checked = true;
                            txtStdCost.Visible = false;
                        }

                        txtStdCost.Text = Math.Round(newcotton.Cotton_StdCost, 2).ToString();
                        txtUnits.Text = newcotton.Cotton_Units.ToString();

                        rtbNotes.Text = newcotton.Cotton_Notes;

                        cmbOrigin.SelectedValue = newcotton.Cotton_Origin_FK;
                        cmbStore.SelectedValue = newcotton.Cotton_Store_FK;
                        cmbUOM.SelectedValue = newcotton.Cotton_UOM_Fk;
                        cmbStockType.SelectedValue = newcotton.Cotton_StockType_FK;
                        cmbCottonAgent.SelectedValue = newcotton.Cotton_Agent_FK;

                        FldEntered = core.PopulateArray(FldNames.Length, true);
                    }
                }
            }
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var Record = (TLADM_Cotton)cmbCotton.SelectedItem;
                if (Record != null)
                {
                    frmCottonContracts cotContracts = new frmCottonContracts(Record.Cotton_Pk);
                    cotContracts.ShowDialog(this);
                }
                else
                {
                    MessageBox.Show("Please select a contract from the drop down list provided");
                }
            }
        }

        private void frmTLADM_Cotton_Load(object sender, EventArgs e)
        {

        }
    }
}

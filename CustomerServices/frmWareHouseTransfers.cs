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

namespace CustomerServices
{
    public partial class frmWareHouseTransfers : Form
    {
        bool formloaded;

        CustomerServices.Repository repo;
        CustomerServices.CustomerServicesParameters QueryParms;

        Util core;

        DataGridViewTextBoxColumn  oTxtA = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn  oTxtB = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn  oTxtC = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn  oTxtD = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();
        bool Submit;

        public frmWareHouseTransfers()
        {
            InitializeComponent();

            repo = new Repository();

            //--------------------------------------------------------------------------------------------------------
            this.cmboStyles.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
            this.cmboSizes.CheckStateChanged += new System.EventHandler(this.cmboSizes_CheckStateChanged);
        }

        private void frmWareHouseTransfers_Load(object sender, EventArgs e)
        {
            formloaded = false;
            Submit = true;

            core = new Util();

            using (var context = new TTI2Entities())
            {
                QueryParms = new CustomerServicesParameters();

                cmboFrom.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                cmboFrom.ValueMember = "Dep_Id";
                cmboFrom.DisplayMember = "Dep_Description";
                cmboFrom.SelectedValue = -1;

                cmboTo.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore).ToList();
                cmboTo.ValueMember = "WhStore_Id";
                cmboTo.DisplayMember = "WhStore_Description";
                cmboTo.SelectedValue = -1;

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CustomerServices.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

                var Colours = context.TLADM_Colours.Where(x=>!(bool)x.Col_Discontinued).OrderBy(x=>x.Col_Display).ToList();
                foreach(var Colour in Colours)
                {
                    cmboColours.Items.Add(new CustomerServices.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Sizes = context.TLADM_Sizes.Where(x=>!(bool)x.SI_Discontinued).OrderBy(x=>x.SI_DisplayOrder).ToList();
                foreach(var Size in Sizes)
                {
                    cmboSizes.Items.Add(new CustomerServices.CheckComboBoxItem(Size.SI_id, Size.SI_Description, false));
                }
                
                
                DGVResults.Visible = false;

                oTxtA = new DataGridViewTextBoxColumn();
                oTxtA.Visible = false;
                oTxtA.ValueType = typeof(int);
                oTxtA.ReadOnly = true;
                DGVResults.Columns.Add(oTxtA);

                oChkA = new DataGridViewCheckBoxColumn();
                oChkA.Visible = true;
                oChkA.ValueType = typeof(Boolean);
                oChkA.HeaderText = "Select";
                DGVResults.Columns.Add(oChkA);

                oTxtB = new DataGridViewTextBoxColumn();
                oTxtB.Visible = true;
                oTxtB.HeaderText = "Box Detail";
                oTxtB.ReadOnly = true;
                oTxtB.ValueType = typeof(string);
                DGVResults.Columns.Add(oTxtB);

                oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.Visible = true;
                oTxtC.HeaderText = "Box Qty";
                oTxtC.ReadOnly = true;
                oTxtC.ValueType = typeof(int);
                DGVResults.Columns.Add(oTxtC);

                oTxtD = new DataGridViewTextBoxColumn();
                oTxtD.Visible = true;
                oTxtD.HeaderText = "Box Weight";
                oTxtD.ReadOnly = true;
                oTxtD.ValueType = typeof(decimal);
                DGVResults.Columns.Add(oTxtD);

                oTxtE = new DataGridViewTextBoxColumn();
                oTxtE.Visible = true;
                oTxtE.HeaderText = "Customer";
                oTxtE.ReadOnly = true;
                oTxtE.ValueType = typeof(string);
                DGVResults.Columns.Add(oTxtE);

                oTxtF = new DataGridViewTextBoxColumn();
                oTxtF.Visible = true;
                oTxtF.HeaderText = "Style";
                oTxtF.ReadOnly = true;
                oTxtF.ValueType = typeof(string);
                DGVResults.Columns.Add(oTxtF);

                oTxtG = new DataGridViewTextBoxColumn();
                oTxtG.Visible = true;
                oTxtG.HeaderText = "Colour";
                oTxtG.ReadOnly = true;
                oTxtG.ValueType = typeof(string);
                DGVResults.Columns.Add(oTxtG);

                oTxtH = new DataGridViewTextBoxColumn();
                oTxtH.Visible = true;
                oTxtH.HeaderText = "Size";
                oTxtH.ReadOnly = true;
                oTxtH.ValueType = typeof(string);
                DGVResults.Columns.Add(oTxtH);

                DGVResults.AutoGenerateColumns = false;
                DGVResults.AllowUserToAddRows = false;
                
                formloaded = true;

            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Styles.Add(repo.LoadStyle(item._Pk));

                }
                else
                {
                    var value = QueryParms.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        QueryParms.Styles.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Colours.Add(repo.LoadColour(item._Pk));

                }
                else
                {
                    var value = QueryParms.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        QueryParms.Colours.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboSizes_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CustomerServices.CheckComboBoxItem && formloaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Sizes.Add(repo.LoadSize(item._Pk));

                }
                else
                {
                    var value = QueryParms.Sizes.Find(it => it.SI_id == item._Pk);
                    if (value != null)
                        QueryParms.Sizes.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            Repository repo = new Repository();
            TLCSV_BoxSelected boxSelected;

            if (oBtn != null && Submit)
            {
                var Depts = (TLADM_Departments)cmboFrom.SelectedItem;
                if (Depts == null)
                {
                    MessageBox.Show("Please select a CMT from from the drop down box");
                    return;
                }

                QueryParms.Depts.Add(repo.LoadDepart(Depts.Dep_Id));
               
               
                var BoxesAvailable = repo.Query(QueryParms);

                if (BoxesAvailable.Count() == 0)
                {
                    MessageBox.Show("There are no Boxes available");
                    return;
                }
                

                DGVResults.Rows.Clear();

                using (var context = new TTI2Entities())
                {
                    foreach (var Box in BoxesAvailable)
                    {
                        var index = DGVResults.Rows.Add();
                        DGVResults.Rows[index].Cells[0].Value = Box.TLCMTWC_Pk;
                        DGVResults.Rows[index].Cells[1].Value = true;          
                        DGVResults.Rows[index].Cells[2].Value = Box.TLCMTWC_BoxNumber;
                        DGVResults.Rows[index].Cells[3].Value = Box.TLCMTWC_Qty;
                        DGVResults.Rows[index].Cells[4].Value = Box.TLCMTWC_Weight;
                        var Styles = context.TLADM_Styles.Find(Box.TLCMTWC_Style_FK);
                        if (Styles != null)
                        {
                            DGVResults.Rows[index].Cells[5].Value = context.TLADM_CustomerFile.Find(Styles.Sty_Customer_Fk).Cust_Description;
                            DGVResults.Rows[index].Cells[6].Value = Styles.Sty_Description;
                        }
                        DGVResults.Rows[index].Cells[7].Value = context.TLADM_Colours.Find(Box.TLCMTWC_Colour_FK).Col_Display;
                        DGVResults.Rows[index].Cells[8].Value = context.TLADM_Sizes.Find(Box.TLCMTWC_Size_FK).SI_Description;
                    }
                }

               
                DGVResults.Visible = true;
                                       
                label4.Visible = false;
                label5.Visible = false;
                label6.Visible = false;

                Submit = !Submit;
                btnSubmit.Text = "Process";
                groupBox1.Text = "Results";

            }
            else
            {
            
                //-------------------------------------------------------------------
                // 1st Has a to destination been selected 
                // If No return
                //------------------------------------------------------------------------
                var Dept = (TLADM_Departments)cmboFrom.SelectedItem;
                if (Dept == null)
                {
                    MessageBox.Show("Error encountered" + Environment.NewLine + "Process aborted");
                    return;
                }

                var Whse = (TLADM_WhseStore)cmboTo.SelectedItem;
                if (Whse == null)
                {
                    MessageBox.Show("Please select a TO Destination from the drop down box provided");
                    return;
                }

                var RecCount = DGVResults.Rows.Cast<DataGridViewRow>().Where(x=>(bool)x.Cells[1].Value == true).Count();

                if (RecCount == 0)
                {
                    MessageBox.Show("Please select a least one record to process");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    //------------------------------------------------
                    // 1st Things first create a header record 
                    //-------------------------------------------------------------------------

                    var LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                   
                    boxSelected = new TLCSV_BoxSelected();

                    boxSelected.TLCSV_TransDate = dateTimePicker1.Value;
                    boxSelected.TLCSV_From_FK = Dept.Dep_Id;
                    boxSelected.TLCSV_To_FK = Whse.WhStore_Id;
                    if (LNU != null)
                    {
                        boxSelected.TLCSV_TransNumber = LNU.col6;
                        LNU.col6 += 1;
                    }

                    boxSelected.TLCSV_PLDetails = "CP" + boxSelected.TLCSV_TransNumber.ToString().PadLeft(5, '0');
                    try
                    {
                        context.TLCSV_BoxSelected.Add(boxSelected);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessages = new StringBuilder();
                        do
                        {
                            exceptionMessages.Append(ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);
                        MessageBox.Show(exceptionMessages.ToString());
                        return;
                    }

                    foreach (DataGridViewRow row in DGVResults.Rows)
                    {
                        if ((bool)row.Cells[1].Value == false)
                            continue;

                        TLCMT_CompletedWork comWork = new TLCMT_CompletedWork();
                        var index = (int)row.Cells[0].Value;
                        comWork = context.TLCMT_CompletedWork.Find(index);
                        if (comWork != null)
                        {
                            comWork.TLCMTWC_PickList_FK = boxSelected.TLCSV_Pk;
                            comWork.TLCMTWC_Picked = true;
                            comWork.TLCMTWC_ToWhse_FK = Whse.WhStore_Id;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                        {
                            MessageBox.Show("Data successfully saved to the database");
                        }
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessages = new StringBuilder();
                        do
                        {
                            exceptionMessages.Append(ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);
                        MessageBox.Show(exceptionMessages.ToString());
                        return;
                    }
                    //----------------------------------------------------------------------
                    frmCSViewRep vRep = new frmCSViewRep(1, boxSelected.TLCSV_Pk);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    //-----------------------------------------------------------------------

                    try
                    {
                        DialogResult res = MessageBox.Show(" Do you wish to send a confirmation email ", "Confirmation email", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            //-------------------------------------------------------------------
                            // We need to build up a data table of  
                            // boxes( stock ) transfered 
                            //-------------------------------------------------------------------
                            DataTable dt = new DataTable();
                            dt.Columns.Add("BoxNumber", typeof(String));
                            dt.Columns["BoxNumber"].DefaultValue = string.Empty;
                            dt.Columns.Add("Style", typeof(String));
                            dt.Columns["Style"].DefaultValue = string.Empty;
                            dt.Columns.Add("Colour", typeof(String));
                            dt.Columns["Colour"].DefaultValue = string.Empty;
                            dt.Columns.Add("Size", typeof(String));
                            dt.Columns["Size"].DefaultValue = string.Empty;
                            dt.Columns.Add("Grade", typeof(String));
                            dt.Columns["Grade"].DefaultValue = string.Empty;
                            dt.Columns.Add("Qty", typeof(int));
                            dt.Columns["Qty"].DefaultValue = 0;
                            dt.Columns.Add("Weight", typeof(decimal));
                            dt.Columns["Weight"].DefaultValue = 0.0M;

                            var BoxesTransfered = context.TLCMT_CompletedWork.Where(x=>x.TLCMTWC_PickList_FK == boxSelected.TLCSV_Pk).ToList();
                            foreach(var Box in BoxesTransfered)
                            {
                                 DataRow dr = dt.NewRow();
                                 dr[0] = Box.TLCMTWC_BoxNumber;
                                 dr[1] = context.TLADM_Styles.Find(Box.TLCMTWC_Style_FK).Sty_Description;
                                 dr[2] = context.TLADM_Colours.Find(Box.TLCMTWC_Colour_FK).Col_Description;
                                 dr[3] = context.TLADM_Sizes.Find(Box.TLCMTWC_Size_FK).SI_Description;
                                 dr[4] = Box.TLCMTWC_Grade;
                                 dr[5] = Box.TLCMTWC_Qty;
                                 dr[6] = Box.TLCMTWC_Weight;
                                 dt.Rows.Add(dr);
                            }
                            
                            StringBuilder EMailAdd = new StringBuilder();
                            frmSeleEMailAddress  EMailAddress = new frmSeleEMailAddress();
                            EMailAddress.ShowDialog(this);
                            var Email = EMailAddress.EMailSelected;
                            if (Email.ToString().Length > 0)
                            {
                                try
                                {
                                    string ToWhse = context.TLADM_WhseStore.Find(boxSelected.TLCSV_To_FK).WhStore_Description;

                                    core.SendEmailtoContacts(Email.ToString(), dt, 2, dateTimePicker1.Value, ToWhse, boxSelected.TLCSV_PLDetails);
                                    MessageBox.Show("Email successfully despatched");
                                }
                                catch (Exception ex)
                                {
                                    var exceptionMessages = new StringBuilder();
                                    do
                                    {
                                        exceptionMessages.Append(ex.Message);
                                        ex = ex.InnerException;
                                    }
                                    while (ex != null);
                                    MessageBox.Show(exceptionMessages.ToString());
                                }
                            }
                            else
                            {
                                MessageBox.Show("No Email address's selected or Email address's do not exist");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessages = new StringBuilder();
                        do
                        {
                            exceptionMessages.Append(ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);
                        MessageBox.Show(exceptionMessages.ToString());
                    }
                   
                }

              
                             
                DGVResults.Visible = false;
              
                groupBox1.Text = "Selection";

                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;

                Submit = !Submit;
                btnSubmit.Text = "Submit";

                cmboStyles.Items.Clear();
                cmboColours.Items.Clear();
                cmboSizes.Items.Clear();

                frmWareHouseTransfers_Load(this, null);
            }
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && formloaded)
            {
                TLADM_Styles StyleSelected = (TLADM_Styles)oCmbo.SelectedItem;
                if (StyleSelected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        if (StyleSelected.Sty_PFD)
                        {
                            cmboTo.DataSource = null;
                            cmboTo.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_RFD).ToList();
                            cmboTo.ValueMember = "WhStore_Id";
                            cmboTo.DisplayMember = "WhStore_Description";
                            cmboTo.SelectedValue = -1;
                        }
                    }
                }
            }
        }
    }
}

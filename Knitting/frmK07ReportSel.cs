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
    public partial class frmK07ReportSel : Form
    {
        string[][] MandatoryFields;
        bool[] MandFieldsSelected;
        Util core;
        bool formloaded;
        bool _GreigeProd;

        KnitQueryParameters QueryParms;
        KnitRepository repo;

        public frmK07ReportSel(bool GreigeProd)
        {
            // Note for the file if set to true then this performs as per original specifications
            // otherwise it must do the the dsk weight variance calculations and Reporting 
            InitializeComponent();

            _GreigeProd = GreigeProd;
            
            this.cmbGreigeProduct.CheckStateChanged += new System.EventHandler(this.cmboGreigeProduct_CheckStateChanged);
            this.cmbKnittingCustomer.CheckStateChanged += new EventHandler(this.cmboKnittingCustomer_CheckStateChanged);
            this.cmbYarnTypes.CheckStateChanged += new EventHandler(this.cmboYarnTypes_CheckStateChanged);
            this.cmbMachines.CheckStateChanged += new System.EventHandler(this.cmboMachines_CheckStateChanged);
            this.cmbOperator.CheckStateChanged += new EventHandler(this.cmboMachineOperators_CheckStateChanged);
            
            QueryParms = new KnitQueryParameters();
            repo = new Knitting.KnitRepository();
            core = new Util();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            formloaded = false;

            chkQASummary.Checked = false;

            if(_GreigeProd)
            {
                this.Text = "Greige Production Reporting";
            }
            else
            {
                this.Text = "Dsk Variance Reporting";
                groupBox1.Visible = false;
                chkQASummary.Visible = false;
                label5.Text = "Variance Band";

                txtGrade.Text = "0.00";
                txtGrade.KeyDown += core.txtWin_KeyDownOEM;
                txtGrade.KeyPress += core.txtWin_KeyPress;

            }

            MandatoryFields = new string[][]
                {   new string[] {"cmbKnittingCustomer", "0"},
                    new string[] {"cmbGreigeProduct", "1"},
                    new string[] {"cmbMachines", "2"},
                    new string[] {"cmbOperator", "3"},
                    new string[] {"txtGrade", "4"}
                };

            using (var context = new TTI2Entities())
            {
                var Yarns = context.TLADM_Yarn.Where(x => !(bool)x.YA_Discontinued).OrderBy(x => x.YA_Description).ToList();
                foreach (var Yar in Yarns)
                {
                    cmbYarnTypes.Items.Add(new Knitting.CheckComboBoxItem(Yar.YA_Id, Yar.YA_Description, false));
                }

                var Greiges = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                foreach (var Greige in Greiges)
                {
                    cmbGreigeProduct.Items.Add(new Knitting.CheckComboBoxItem(Greige.TLGreige_Id, Greige.TLGreige_Description, false));
                }

                var Customers = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                foreach (var Customer in Customers)
                {
                    cmbKnittingCustomer.Items.Add(new Knitting.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                }
                              
                txtGrade.Text = string.Empty;

                var Dept = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
                    var Machines  = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).OrderBy(x=>x.MD_MachineCode).ToList();
                    foreach (var Machine in Machines)
                    {
                        cmbMachines.Items.Add(new Knitting.CheckComboBoxItem(Machine.MD_Pk, Machine.MD_AlternateDesc, false));
                    }
                   
                    var Operators = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).OrderBy(x => x.MachOp_Description).ToList();
                    foreach (var Operator in Operators)
                    {
                        cmbOperator.Items.Add(new Knitting.CheckComboBoxItem(Operator.MachOp_Pk, Operator.MachOp_Description, false));
                    }
                    
                }

                MandFieldsSelected = core.PopulateArray(MandatoryFields.Length, false);
                rbGPByMachine.Checked = true;
                formloaded = true;
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboYarnTypes_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Knitting.CheckComboBoxItem)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.YarnTypes.Add(repo.LoadYarn(item._Pk));
                }
                else
                {
                    var value = QueryParms.YarnTypes.Find(it => it.YA_Id == item._Pk);
                    if (value != null)
                        QueryParms.YarnTypes.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboGreigeProduct_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Knitting.CheckComboBoxItem)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Greiges.Add(repo.LoadGriege(item._Pk));
                }
                else
                {
                    var value = QueryParms.Greiges.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        QueryParms.Greiges.Remove(value);
                }
            }
        }


        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboKnittingCustomer_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Knitting.CheckComboBoxItem)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Customers.Add(repo.LoadCustomer(item._Pk));
                }
                else
                {
                    var value = QueryParms.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Customers.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboMachines_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Knitting.CheckComboBoxItem)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Machines.Add(repo.LoadMachine(item._Pk));

                }
                else
                {
                    var value = QueryParms.Machines.Find(it => it.MD_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Machines.Remove(value);

                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboMachineOperators_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Knitting.CheckComboBoxItem)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Operators.Add(repo.LoadOperator(item._Pk));

                }
                else
                {
                    var value = QueryParms.Operators.Find(it => it.MachOp_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Operators.Remove(value);

                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {

                YarnReportOptions YarnOpts = new YarnReportOptions();
                if (chkQASummary.Checked)
                    YarnOpts.QASummary = true;

                QueryParms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                QueryParms.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                QueryParms.ToDate = QueryParms.ToDate.AddHours(23.59);

                QueryParms.Grade = txtGrade.Text;
               
                    int index = -1;
                    foreach (var element in MandFieldsSelected)
                    {
                        ++index;
                        if (element)
                        {
                            if (index == 0)
                            {
                                YarnOpts.K7CustSel = true;
                                //YarnOpts.K7KnittingCustomer_FK = ((TLADM_CustomerFile)cmbKnittingCustomer.SelectedItem).Cust_Pk;
                            }
                            else if (index == 1)
                            {
                                YarnOpts.K7ProdSel = true;
                                //YarnOpts.K7GreigeProduct_FK = ((TLADM_Griege)cmbGreigeProduct.SelectedItem).TLGreige_Id;
                            }
                            else if (index == 2)
                            {
                                YarnOpts.K7MachineSel = true;
                                //YarnOpts.K7Machine_FK = ((TLADM_MachineDefinitions)cmbMachines.SelectedItem).MD_Pk;
                            }
                            else if (index == 3)
                            {
                                YarnOpts.K7OperatorSel = true;
                                //YarnOpts.K7Operator_FK = ((TLADM_MachineOperators)cmbOperator.SelectedItem).MachOp_Pk;
                            }
                            else if (index == 4)
                            {
                                YarnOpts.K7GradeSel = true;
                                YarnOpts.K7Grade = txtGrade.Text;
                            }
                        }
                    }

                    if (rbGPByMachine.Checked)
                    {
                        YarnOpts.K7rbQA1 = true;
                    }
                    else if (rbGPByTotal.Checked)
                    {
                        YarnOpts.K7rbQA2 = true;
                    }
                    else if (rbGPOperator.Checked)
                    {
                        YarnOpts.K7rbQA3 = true;
                    }
                    else if (rbTotalsByMachineByDay.Checked)
                    {
                        YarnOpts.K7ByMachineByDay = true;
                    }

                if (_GreigeProd)
                {
                    QueryParms.DiskVarianceReport = false;
                    frmKnitViewRep vRep = null;
                    
                    if (!rbTotalsByMachineByDay.Checked)
                    {
                        vRep  = new frmKnitViewRep(23, QueryParms, YarnOpts);
                    }
                    else
                    {
                        vRep = new frmKnitViewRep(35, QueryParms, YarnOpts);
                    }
                    
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                else
                {
                    if(txtGrade.Text.Length != 0 && decimal.Parse(txtGrade.Text) != 0)
                    {
                        QueryParms.UpperDskVariance = Decimal.Parse(txtGrade.Text);
                        QueryParms.LowerDiskVariance = -1 * QueryParms.UpperDskVariance;

                    }

                    QueryParms.DiskVarianceReport = true;
                    frmKnitViewRep vRep = new frmKnitViewRep(34, QueryParms, YarnOpts);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    if (vRep != null)
                    {
                        vRep.Close();
                        vRep.Dispose();
                    }
                }
               
               
            }
            
            MandFieldsSelected = core.PopulateArray(MandatoryFields.Length, false);
            Form_Load(this, null);
                  
             

        }

        private void cmbKnittingCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int a = Convert.ToInt32(result[1]);
                    MandFieldsSelected[a] = true;
                }
                if (oCmbo != null && !oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }

        private void cmbGreigeProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int a = Convert.ToInt32(result[1]);
                    MandFieldsSelected[a] = true;
                }

                if (oCmbo != null && !oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }

        private void cmbMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int a = Convert.ToInt32(result[1]);
                    MandFieldsSelected[a] = true;
                }

                if (oCmbo != null && !oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }

        private void cmbOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                   int a = Convert.ToInt32(result[1]);
                    MandFieldsSelected[a] = true;
                }

                if (oCmbo != null && !oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }

        private void txtGrade_TextChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int a = Convert.ToInt32(result[1]);
                    MandFieldsSelected[a] = true;
                }
            }
        }
    }
}

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
    public partial class frmK08ReportSel : Form
    {
        string[][] MandatoryFields;
        bool[] MandFieldsSelected;
        Util core;
        bool formloaded;

        KnitQueryParameters QueryParms;
        KnitRepository repo;

        public frmK08ReportSel()
        {
            InitializeComponent();

            this.cmbGreigeProduct.CheckStateChanged += new System.EventHandler(this.cmboGreigeProduct_CheckStateChanged);
            this.cmbKnittingCustomer.CheckStateChanged += new EventHandler(this.cmboKnittingCustomer_CheckStateChanged);

            this.cmbKnittingMachine.CheckStateChanged += new System.EventHandler(this.cmboMachines_CheckStateChanged);
            this.cmbOperator.CheckStateChanged += new EventHandler(this.cmboMachineOperators_CheckStateChanged);

            QueryParms = new KnitQueryParameters();
            repo = new Knitting.KnitRepository();
            core = new Util();

        }

        private void FormLoad(object sender, EventArgs e)
        {
            formloaded = false;

            using (var context = new TTI2Entities())
            {
                rbQA1.Checked = true;

                chkQASummary.Checked = false;

                MandatoryFields = new string[][]
                {   new string[] {"cmbKnittingCustomer", "0"},
                    new string[] {"cmbGreigeProduct", "1"},
                    new string[] {"cmbKnittingMachine", "2"},
                    new string[] {"cmbOperator", "3"}
                };

                rbQA5.Enabled = false;
                rbQA6.Enabled = false;
                rbQA7.Enabled = true;
                rbQA8.Enabled = true;

                MandFieldsSelected = core.PopulateArray(MandatoryFields.Length, false);

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
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


                    var Machines = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    foreach (var Machine in Machines)
                    {
                            cmbKnittingMachine.Items.Add(new Knitting.CheckComboBoxItem(Machine.MD_Pk, Machine.MD_AlternateDesc, false));
                    }

                    var Operators = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued ).OrderBy(x => x.MachOp_Description).ToList();
                    foreach (var Operator in Operators)
                    {
                            cmbOperator.Items.Add(new Knitting.CheckComboBoxItem(Operator.MachOp_Pk, Operator.MachOp_Description, false));
                    }
               }

                MandFieldsSelected = core.PopulateArray(MandatoryFields.Length, false);
                formloaded = true;
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

                //--------------------------------------------------------------------------
                // Do the selection first 
                //-----------------------------------------------------------------------
                
                QueryParms.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                QueryParms.ToDate   = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                YarnOpts.toDate = QueryParms.ToDate.AddHours(23.59);
                int index = -1;
                foreach (var element in MandFieldsSelected)
                {
                    ++index;
                    if (element)
                    {
                        if (index == 0)
                        {
                            YarnOpts.K8CustSel = true;
                            //YarnOpts.K8KnittingCustomer_FK = ((TLADM_CustomerFile)cmbKnittingCustomer.SelectedItem).Cust_Pk;
                        }
                        else if (index == 1)
                        {
                            YarnOpts.K8QualSel = true;
                            //YarnOpts.K8GreigeProduct_FK = ((TLADM_Griege)cmbGreigeProduct.SelectedItem).TLGreige_Id;
                        }
                        else if (index == 2)
                        {
                            YarnOpts.K8MachineSel = true;
                            //YarnOpts.K8KnittingMachine_FK = ((TLADM_MachineDefinitions)cmbKnittingMachine.SelectedItem).MD_Pk;
                        }
                        else if (index == 3)
                        {
                            YarnOpts.K8OperatorSel = true;
                            //YarnOpts.K8Operator_Fk = ((TLADM_MachineOperators)cmbOperator.SelectedItem).MachOp_Pk;
                        }
                    }
                }

                //-------------------------------------------------------------------
                // Do The Sort next 
                //-----------------------------------------------------------------------

                if (rbQA1.Checked)
                    YarnOpts.K8rbQA1 = true;
                else if (rbQA2.Checked)
                    YarnOpts.K8rbQA2 = true;
                else if (rbQA3.Checked)
                    YarnOpts.K8rbQA3 = true;
                else if (rbQA4.Checked)
                    YarnOpts.K8rbQA4 = true;
                else if (rbQA7.Checked)
                    YarnOpts.K8rbQA7 = true;
                else if (rbQA8.Checked)
                    YarnOpts.K8rbQA8 = true;

                if (rbExcludeCommission.Checked)
                {
                    YarnOpts.ExcludeCommission = true;
                }

               
                frmKnitViewRep vRep = new frmKnitViewRep(22, QueryParms, YarnOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                   
                }
                MandFieldsSelected = core.PopulateArray(MandatoryFields.Length, false);
            }
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

        private void cmbKnittingMachine_SelectedIndexChanged(object sender, EventArgs e)
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

        private void frmK08ReportSel_FormClosing(object sender, FormClosingEventArgs e)
        {
                
            
        }
    }
}

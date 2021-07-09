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
    public partial class frmDyedBasicQual : Form
    {
        bool FormLoaded;
        protected readonly TTI2Entities _context;
        DyeHouse.DyeRepository repo;
        DyeHouse.DyeQueryParameters QueryParms;

        bool _Mode;
       Util core;

        BindingList<KeyValuePair<int, string>> reportOptions;

        public frmDyedBasicQual(bool Mode)
        {
            InitializeComponent();
            _Mode = Mode;

            repo = new DyeRepository();

            _context = new TTI2Entities();

            core = new Util();

            reportOptions = new BindingList<KeyValuePair<int, string>>();
            if (Mode)
            {
                label9.Visible = false;
                cmboCustomers.Visible = false;

                label10.Visible = false;
                cmboDyeMachine.Visible = false;
                label11.Visible = false;
                cmboDyeOperator.Visible = false;

                reportOptions.Add(new KeyValuePair<int, string>(0, "Batch Number "));
                reportOptions.Add(new KeyValuePair<int, string>(1, "Dye Machines"));
                reportOptions.Add(new KeyValuePair<int, string>(2, "Dye Operator"));
                reportOptions.Add(new KeyValuePair<int, string>(3, "Process loss magnitude"));
                reportOptions.Add(new KeyValuePair<int, string>(4, "Width difference to fabric final"));
            }
            else
            {
                reportOptions.Add(new KeyValuePair<int, string>(0, "Batch Number "));
                reportOptions.Add(new KeyValuePair<int, string>(1, "Quality"));
                reportOptions.Add(new KeyValuePair<int, string>(2, "Dye Machine"));
                reportOptions.Add(new KeyValuePair<int, string>(3, "Operator"));
                reportOptions.Add(new KeyValuePair<int, string>(4, "Process loss magnitude"));
                reportOptions.Add(new KeyValuePair<int, string>(5, "Width difference to fabric final"));
                reportOptions.Add(new KeyValuePair<int, string>(6, "Weight difference Magnitude"));
                reportOptions.Add(new KeyValuePair<int, string>(7, "Customer"));
            }

            cmboReportOptions.DataSource = reportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;

            if(_Mode)
            {
                this.Text = "Dyed Fabric basic Quality information - Fabric width movement ";
            }
            else
            {
                this.Text = "Dyed Fabric basic Quality information - Fabric weight movement ";
            }
            txtProcessLoss.KeyDown += core.txtWin_KeyDownOEM;
            txtProcessLoss.KeyPress += core.txtWin_KeyPress;
            
            txtWidthDiff.KeyDown += core.txtWin_KeyDownOEM;
            txtWidthDiff.KeyPress += core.txtWin_KeyPress;

            this.cmboCustomers.CheckStateChanged += new System.EventHandler(this.cmboCustomers_CheckStateChanged);

            this.cmboGreige.CheckStateChanged += new System.EventHandler(this.cmboGreige_CheckStateChanged);
            this.cmboColours.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
        }

        private void frmDyedBasicQual_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            txtWidthDiff.Text = "0.0";
            txtProcessLoss.Text = "0.0";

            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;

            QueryParms = new DyeQueryParameters();
            
            var Qualities = _context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
            foreach (var Quality in Qualities)
            {
                cmboGreige.Items.Add(new DyeHouse.CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description, false));
            }

            var Colours = _context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
            foreach (var Colour in Colours)
            {
                cmboColours.Items.Add(new DyeHouse.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
            }


            if(!_Mode)
            {
                var Customers = _context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();

                foreach(var Customer in Customers)
                {
                    cmboCustomers.Items.Add(new DyeHouse.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                }

                var Machines = _context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == 12 && !x.MD_Compactor && !x.MD_Drier && !x.MD_Hydro).OrderBy(x => x.MD_Description).ToList();
                foreach(var Machine in Machines)
                {
                    cmboDyeMachine.Items.Add(new DyeHouse.CheckComboBoxItem(Machine.MD_Pk, Machine.MD_Description, false));
                }

                var Operators = _context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == 12).OrderBy(x => x.MachOp_Description).ToList();
                foreach(var Operator in Operators)
                {
                    cmboDyeOperator.Items.Add(new DyeHouse.CheckComboBoxItem(Operator.MachOp_Pk, Operator.MachOp_Description, false));
                }
            }
            FormLoaded = true;
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboGreige_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Qualities.Add(repo.LoadQuality(item._Pk));

                }
                else
                {
                    var value = QueryParms.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        QueryParms.Qualities.Remove(value);

                }
            }
        }

        private void cmboCustomers_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Customers.Add(repo.LoadCustomer(item._Pk));

                }
                else
                {
                    var value = QueryParms.Customers.Find(it => it.Cust_Pk  == item._Pk);
                    if (value != null)
                        QueryParms.Customers.Remove(value);

                }
            }
        }
        private void cmboMachine_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Machines.Add(repo.LoadMachines(item._Pk));

                }
                else
                {
                    var value = QueryParms.Machines.Find(it => it.MD_Pk  == item._Pk);
                    if (value != null)
                        QueryParms.Machines.Remove(value);

                }
            }
        }

        private void cmboOperator_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Operators.Add(repo.LoadOperators(item._Pk));

                }
                else
                {
                    var value = QueryParms.Operators.Find(it => it.MachOp_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Operators.Remove(value);

                }
            }
        }
        private void cmboColours_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            frmDyeViewReport vRep = null;

            if(oBtn != null && FormLoaded)
            {
                var FDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                var TDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                
                TDate = TDate.AddHours(23);

                QueryParms.FromDate = FDate;
                QueryParms.ToDate = TDate;

                QueryParms.ProcessLoss = Convert.ToDecimal(txtProcessLoss.Text);
                QueryParms.WidthMagnitude = Convert.ToDecimal(txtWidthDiff.Text);

                if (cmboReportOptions.SelectedValue != null)
                {
                    QueryParms.DO_OptionSelected = Int32.Parse(cmboReportOptions.SelectedValue.ToString());
                }
                else
                {
                    QueryParms.DO_OptionSelected = 0;
                }

                if (_Mode)
                {
                    vRep = new frmDyeViewReport(48, QueryParms);
                }
                else
                {
                    vRep = new frmDyeViewReport(49, QueryParms);
                }
                
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                        vRep.Dispose();
                }


                cmboColours.Items.Clear();
                cmboCustomers.Items.Clear();
                cmboDyeMachine.Items.Clear();
                cmboDyeOperator.Items.Clear();
                cmboGreige.Items.Clear();
                cmboReportOptions.SelectedValue = -1;
                
                frmDyedBasicQual_Load(this, null);
            }
        }

        private void frmDyedBasicQual_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(FormLoaded && !e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}

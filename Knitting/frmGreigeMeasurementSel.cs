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
    public partial class frmGreigeMeasurementSel : Form
    {
        bool FormLoaded;
        Knitting.KnitRepository repo;
        Knitting.KnitQueryParameters Parms;
        Util core;
        String _Depts;

        public frmGreigeMeasurementSel(String Depts)
        {
            InitializeComponent();

            repo = new KnitRepository();

            core = new Util();

            _Depts = Depts;

            this.cmboQuality.CheckStateChanged += new System.EventHandler(this.cmboGreigeProduct_CheckStateChanged);
            this.cmboOperators.CheckStateChanged += new EventHandler(this.cmboMachineOperators_CheckStateChanged);
            this.cmboMachines.CheckStateChanged += new System.EventHandler(this.cmboMachines_CheckStateChanged);
        }

        private void frmGreigeMeasurementSel_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            Parms = new KnitQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode == _Depts).FirstOrDefault();
                if (Depts != null)
                {
                    var Machines = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Depts.Dep_Id).OrderBy(x => x.MD_Description).ToList();
                    foreach (var Machine in Machines)
                    {
                        cmboMachines.Items.Add(new Knitting.CheckComboBoxItem(Machine.MD_Pk, Machine.MD_Description, false));
                    }


                    var Operators = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Depts.Dep_Id && !x.MachOp_Discontinued).OrderBy(x => x.MachOp_Description).ToList();
                    foreach (var Operator in Operators)
                    {
                        cmboOperators.Items.Add(new Knitting.CheckComboBoxItem(Operator.MachOp_Pk, Operator.MachOp_Description, false));
                    }

                    var Qualities = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                    foreach (var Quality in Qualities)
                    {
                        cmboQuality.Items.Add(new Knitting.CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description, false));
                    }
                }

            }


            FormLoaded = true;
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
                    Parms.Greiges.Add(repo.LoadGriege(item._Pk));
                }
                else
                {
                    var value = Parms.Greiges.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        Parms.Greiges.Remove(value);
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
                    Parms.Machines.Add(repo.LoadMachine(item._Pk));

                }
                else
                {
                    var value = Parms.Machines.Find(it => it.MD_Pk == item._Pk);
                    if (value != null)
                        Parms.Machines.Remove(value);

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
                    Parms.Operators.Add(repo.LoadOperator(item._Pk));

                }
                else
                {
                    var value = Parms.Operators.Find(it => it.MachOp_Pk == item._Pk);
                    if (value != null)
                        Parms.Operators.Remove(value);

                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                YarnReportOptions opts = new YarnReportOptions();
                
                opts.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                opts.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                opts.toDate = opts.toDate.AddHours(23);

                if (rbQuality.Checked)
                    opts.GKM_Quality = true;
                else if (rbMachine.Checked)
                    opts.GKM_Machines = true;
                else
                    opts.GKM_Operators = true;


               
                frmKnitViewRep vRep = new frmKnitViewRep(33, Parms, opts);
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
    }
}

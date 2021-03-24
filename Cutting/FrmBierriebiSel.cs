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
namespace Cutting
{
    public partial class FrmBierriebiSel : Form
    {
        bool FormLoaded;
        CuttingRepository repo;
        CuttingQueryParameters parms;

        public FrmBierriebiSel()
        {
            InitializeComponent();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.comboCutSheets.CheckStateChanged += new System.EventHandler(this.comboCutSheet_CheckStateChanged);
            this.comboGreige.CheckStateChanged += new System.EventHandler(this.comboGreige_CheckStateChanged);
            this.comboOperator.CheckStateChanged += new System.EventHandler(this.comboOperators_CheckStateChanged);
            this.comboMachines.CheckStateChanged += new System.EventHandler(this.comboMachines_CheckStateChanged);

            repo = new CuttingRepository();

            var reportOptions = new BindingList<KeyValuePair<int, string>>();
            reportOptions.Add(new KeyValuePair<int, string>(1, "Quality"));
            reportOptions.Add(new KeyValuePair<int, string>(2, "Colour"));
            reportOptions.Add(new KeyValuePair<int, string>(3, "Operator"));

            comboReportOptions.DataSource = reportOptions;
            comboReportOptions.ValueMember = "Key";
            comboReportOptions.DisplayMember = "Value";
            comboReportOptions.SelectedIndex = -1;

        }

        private void FrmBierriebiSel_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            parms = new CuttingQueryParameters();
            using (var context = new TTI2Entities())
            {
                var Query = from T1 in context.TLCUT_QCBerrie
                            join T2 in context.TLCUT_CutSheetReceipt on T1.TLQCFB_CutSheetReceipt_FK equals T2.TLCUTSHR_Pk
                            join T3 in context.TLCUT_CutSheet on T2.TLCUTSHR_CutSheet_FK equals T3.TLCutSH_Pk
                            where (T1.TLQCFB_Measure1 != 0 || 
                                  T1.TLQCFB_Measure10 != 0 || 
                                  T1.TLQCFB_Measure11 != 0 ||
                                  T1.TLQCFB_Measure2 != 0  ||
                                  T1.TLQCFB_Measure3 != 0 || 
                                  T1.TLQCFB_Measure4 != 0 || 
                                  T1.TLQCFB_Measure5 != 0 || 
                                  T1.TLQCFB_Measure6 != 0 ||
                                  T1.TLQCFB_Measure7 != 0 || 
                                  T1.TLQCFB_Measure8 != 0 ||
                                  T1.TLQCFB_Measure9 != 0) && !T3.TLCutSH_Closed
                            select new { T2.TLCUTSHR_Pk, T3.TLCutSH_No };

                var QueryGroup = Query.OrderBy(x=>x.TLCutSH_No).GroupBy(x => x.TLCUTSHR_Pk);

                foreach (var Receipt in QueryGroup)
                {
                    comboCutSheets.Items.Add(new Cutting.CheckComboBoxItem(Receipt.FirstOrDefault().TLCUTSHR_Pk, Receipt.FirstOrDefault().TLCutSH_No, false));
                }
              
                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();
                if (Dept != null)
                {
                    var Operators = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Inspector && !x.MachOp_Discontinued).ToList();
                    foreach (var Operator in Operators)
                    {
                        this.comboOperator.Items.Add(new Cutting.CheckComboBoxItem(Operator.MachOp_Pk, Operator.MachOp_Description, false));
                    }

                    var Machines = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    foreach (var Machine in Machines)
                    {
                        this.comboMachines.Items.Add(new Cutting.CheckComboBoxItem(Machine.MD_Pk, Machine.MD_AlternateDesc, false));
                    }
                }
                

                var Qualities = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                foreach (var Quality in Qualities)
                {
                    this.comboGreige.Items.Add(new Cutting.CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description, false));
                }
            }
            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboCutSheet_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && FormLoaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.CutSheetReceipts.Add(repo.LoadCutSheetReceipt(item._Pk));
                }
                else
                {
                    var value = parms.CutSheetReceipts.Find(it => it.TLCUTSHR_Pk == item._Pk);
                    if (value != null)
                        parms.CutSheetReceipts.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboMachines_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && FormLoaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Machines.Add(repo.LoadMachine(item._Pk));
                }
                else
                {
                    var value = parms.Machines.Find(it => it.MD_Pk == item._Pk);
                    if (value != null)
                        parms.Machines.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboGreige_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && FormLoaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Qualities.Add(repo.LoadQuality(item._Pk));
                }
                else
                {
                    var value = parms.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        parms.Qualities.Remove(value);
                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboOperators_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Cutting.CheckComboBoxItem && FormLoaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    parms.Operators.Add(repo.LoadOperators(item._Pk));
                }
                else
                {
                    var value = parms.Operators.Find(it => it.MachOp_Pk == item._Pk);
                    if (value != null)
                        parms.Operators.Remove(value);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                if (comboReportOptions.SelectedValue != null)
                {
                    parms.RepSortOption = Int32.Parse(comboReportOptions.SelectedValue.ToString());
                }
                else
                    parms.RepSortOption = 1;

                frmCutViewRep vRep = new frmCutViewRep(10, parms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }

                comboCutSheets.Items.Clear();
                comboGreige.Items.Clear();
                comboMachines.Items.Clear();
                comboOperator.Items.Clear();

                FrmBierriebiSel_Load(this, null);


            }
        }
    }
}

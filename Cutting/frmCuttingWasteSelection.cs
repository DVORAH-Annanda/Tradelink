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
    public partial class frmCuttingWasteSelection : Form
    {
        protected readonly TTI2Entities _context;
        Util core;
        Cutting.CuttingRepository repo;
        Cutting.CuttingQueryParameters QParms;
        bool FormLoaded;
        BindingList<KeyValuePair<int, string>> ReportOptions;
        
        public frmCuttingWasteSelection()
        {
            InitializeComponent();
            _context = new TTI2Entities();
            core = new Util();
            repo = new CuttingRepository();
            ReportOptions = new BindingList<KeyValuePair<int, string>>();

            this.comboCutSheet.CheckStateChanged += new System.EventHandler(this.comboCutSheet_CheckStateChanged);
            this.comboQuality.CheckStateChanged += new EventHandler(this.comboQuality_CheckStateChanged);
            this.comboMachines.CheckStateChanged += new System.EventHandler(this.comboMachines_CheckStateChanged);
  
        }

        private void frmCuttingWasteSelection_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            ReportOptions = new BindingList<KeyValuePair<int, string>>();

            QParms = new Cutting.CuttingQueryParameters();

            ReportOptions.Add(new KeyValuePair<int, string>(1, "Dye Batch"));
            ReportOptions.Add(new KeyValuePair<int, string>(2, "Machine"));
            ReportOptions.Add(new KeyValuePair<int, string>(3, "Quality"));
          

            cmboReportOptions.DataSource = ReportOptions;
            cmboReportOptions.ValueMember = "Key";
            cmboReportOptions.DisplayMember = "Value";
            cmboReportOptions.SelectedIndex = -1;


            var Dept = _context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();
            if (Dept != null)
            {
                var Machines = _context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).OrderBy(x=>x.MD_Description).ToList();
                foreach (var Machine in Machines)
                {
                    this.comboMachines.Items.Add(new Cutting.CheckComboBoxItem(Machine.MD_Pk, Machine.MD_Description, false));
                 
                }

                var Qualities = _context.TLADM_Griege.Where(x => !(bool)x.TLGriege_Discontinued).OrderBy(x => x.TLGreige_Description).ToList();
                foreach(var Quality in Qualities)
                {
                    this.comboQuality.Items.Add(new Cutting.CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description, false));
                }

                DateTime SixPrevious = DateTime.Now.AddDays(-180);
                var CutSheets = _context.TLCUT_CutSheet.Where(x=>(bool)x.TLCutSH_WIPComplete && x.TLCUTSH_Completed_Date >= SixPrevious).OrderBy(x=>x.TLCutSH_No);
                foreach (var CutSheet in CutSheets)
                {
                    this.comboCutSheet.Items.Add(new Cutting.CheckComboBoxItem(CutSheet.TLCutSH_Pk, CutSheet.TLCutSH_No, false));
                }
            }

            QParms = new CuttingQueryParameters();

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
                    QParms.CutSheets.Add(repo.LoadCutSheet(item._Pk));

                }
                else
                {
                    var value = QParms.CutSheets.Find(it => it.TLCutSH_Pk == item._Pk);
                    if (value != null)
                        QParms.CutSheets.Remove(value);

                }
            }
        }

        private void comboQuality_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Cutting.CheckComboBoxItem && FormLoaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QParms.Qualities.Add(repo.LoadQuality(item._Pk));

                }
                else
                {
                    var value = QParms.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        QParms.Qualities.Remove(value);

                }
            }
        }

        private void comboMachines_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Cutting.CheckComboBoxItem && FormLoaded)
            {
                Cutting.CheckComboBoxItem item = (Cutting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QParms.Machines.Add(repo.LoadMachine(item._Pk));

                }
                else
                {
                    var value = QParms.Machines.Find(it => it.MD_Pk == item._Pk);
                    if (value != null)
                        QParms.Machines.Remove(value);

                }
            }
        }
        private void frmCuttingWasteSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                QParms.FromDate = dtpFromDate.Value;
                QParms.ToDate = dtpToDate.Value;
                if (cmboReportOptions.SelectedValue != null)
                {
                    QParms.RepSortOption = (int)cmboReportOptions.SelectedValue;
                    if (QParms.RepSortOption == 0)
                    {
                        QParms.RepSortOption = 1;
                    }
                }
                else
                {
                    QParms.RepSortOption = 1;
                }

                try
                {
                    var vRep = new Cutting.frmCutViewRep(22, QParms);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                finally
                {
                    this.comboCutSheet.Items.Clear();
                    this.comboMachines.Items.Clear();
                    this.comboQuality.Items.Clear();
                    frmCuttingWasteSelection_Load(this, null);
                }
            }
        }
    }
}

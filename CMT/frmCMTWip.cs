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

namespace CMT
{
    public partial class frmCMTWip : Form
    {
        bool FormLoaded;
        CMTRepository repo;
        CMTQueryParameters QueryParms;

        public frmCMTWip()
        {
            InitializeComponent();
            repo = new CMTRepository();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboCMT.CheckStateChanged += new EventHandler(this.cmboDepts_CheckStateChanged);
            this.cmboLine.CheckStateChanged += new System.EventHandler(this.cmboLine_CheckStateChanged);
        }

        private void frmCMTWip_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var Depts = context.TLADM_Departments.Where(x=>x.Dep_IsCMT).OrderBy(x => x.Dep_Description).ToList();
                foreach (var Dept in Depts)
                {
                    cmboCMT.Items.Add(new CMT.CheckComboBoxItem(Dept.Dep_Id, Dept.Dep_Description, false));
                }

                var Lines = context.TLCMT_FactConfig.OrderBy(x => x.TLCMTCFG_Description).ToList();
                foreach (var Line in Lines)
                {
                    cmboLine.Items.Add(new CMT.CheckComboBoxItem(Line.TLCMTCFG_Pk, Line.TLCMTCFG_Description, false));
                }
            }

            QueryParms = new CMTQueryParameters();
            
            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboDepts_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && FormLoaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Depts.Add(repo.LoadDepartment(item._Pk));

                }
                else
                {
                    var value = QueryParms.Depts.Find(it => it.Dep_Id == item._Pk);
                    if (value != null)
                        QueryParms.Depts.Remove(value);

                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboLine_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem && FormLoaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.Lines.Add(repo.LoadLine(item._Pk));

                }
                else
                {
                    var value = QueryParms.Lines.Find(it => it.TLCMTCFG_Pk == item._Pk);
                    if (value != null)
                        QueryParms.Lines.Remove(value);

                }
            }
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                CMTReportOptions repOptions = new CMTReportOptions();

                repOptions.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                repOptions.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                repOptions.toDate = repOptions.toDate.AddHours(23);

                try
                {
                    frmCMTViewRep vRep = new frmCMTViewRep(15, QueryParms, repOptions);
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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    cmboLine.Items.Clear();
                    cmboCMT.Items.Clear();
                    frmCMTWip_Load(this, null);

                }
            }
        }

        private void cmboCMT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void cmboLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}

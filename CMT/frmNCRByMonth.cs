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
    public partial class frmNCRByMonth : Form
    {
        CMTQueryParameters QueryParms;
        CMTRepository repo;
        bool FormLoaded;

        public frmNCRByMonth()
        {
            InitializeComponent();

            repo = new CMTRepository();
            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmboLines.CheckStateChanged += new System.EventHandler(this.cmboLines_CheckStateChanged);
            this.cmboStyles.CheckStateChanged += new EventHandler(this.cmboStyle_CheckStateChanged);
        }

        private void frmNCRByMonth_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new CMTQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    cmboStyles.Items.Add(new CMT.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }


            }
            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboLines_CheckStateChanged(object sender, EventArgs e)
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
        private void cmboStyle_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is CMT.CheckComboBoxItem &&  FormLoaded)
            {
                CMT.CheckComboBoxItem item = (CMT.CheckComboBoxItem)sender;
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                var FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                var ToDate   = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                ToDate = ToDate.AddHours(23);

                QueryParms.FromDate = FromDate;
                QueryParms.ToDate = ToDate;

                QueryParms.Year = FromDate.Year;
                
                frmCMTViewRep vRep = new frmCMTViewRep(28, QueryParms, false);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog();

                cmboStyles.Items.Clear();
                frmNCRByMonth_Load(this, null);
            }
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}

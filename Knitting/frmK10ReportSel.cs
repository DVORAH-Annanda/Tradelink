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
using System.Reflection;

namespace Knitting
{
    public partial class frmK10ReportSel : Form
    {
        string[][] MandatoryFields;
        bool[] MandSelected;
        Util core = null;
        bool formloaded;
        bool _Version;
        bool BoughtInFabric;

        KnitQueryParameters QueryParms;
        KnitRepository repo;

        public frmK10ReportSel(bool Version)
        {
            //Note for the file 
            // Version = true then produce the detailed report
            // Version = false then produce the summary  report 
            //===============================================================
            InitializeComponent();

            repo = new KnitRepository();

            this.cmboProduct.CheckStateChanged      += new System.EventHandler(this.cmboGreigeProduct_CheckStateChanged);
            this.cmboStore.CheckStateChanged        += new EventHandler(this.cmboStore_CheckStateChanged);
            this.cmboGreigeQuality.CheckStateChanged += new EventHandler(this.cmboGreigeQuality_CheckStateChanged);

      core = new Util();

            rbBIFSummarised.Checked = true;

            BoughtInFabric = false;

            if (Version)
                this.Text = "Greige Stock on hand - full detail";
            else
                this.Text = "Greige Stock on hand - Summary detail";


            MandatoryFields = new string[][]
                {   new string[] {"cmboStore",  "0"},
                    new string[] {"txtGrade", "1"},
                    new string[] {"cmboProduct", "2"},
                    new string[] {"cmboProductGroup", "3"},
                    new string[] {"cmboStockTake", "4"}
                };
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

        private void cmboGreigeQuality_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is Knitting.CheckComboBoxItem)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.ProductQualities.Add(repo.LoadGreigeQuality(item._Pk));
                }
                else
                {
                    var value = QueryParms.ProductQualities.Find(it => it.GQ_Pk == item._Pk);
                    if (value != null)
                        QueryParms.ProductQualities.Remove(value);
                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboStore_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Knitting.CheckComboBoxItem)
            {
                Knitting.CheckComboBoxItem item = (Knitting.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.WhseStores.Add(repo.LoadStore(item._Pk));
                }
                else
                {
                    var value = QueryParms.WhseStores.Find(it => it.WhStore_Id == item._Pk);
                    if (value != null)
                        QueryParms.WhseStores.Remove(value);

                }
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                QueryParms = new KnitQueryParameters();

                var Greiges = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                foreach (var Greige in Greiges)
                {
                    cmboProduct.Items.Add(new Knitting.CheckComboBoxItem(Greige.TLGreige_Id, Greige.TLGreige_Description, false));
                }

                var GreigeQualities = context.TLADM_GreigeQuality.OrderBy(x => x.GQ_Description).ToList();
                foreach(var Quality in GreigeQualities)
                {
                    cmboGreigeQuality.Items.Add(new Knitting.CheckComboBoxItem(Quality.GQ_Pk, Quality.GQ_Description, false));

                }

                chkBoughtInFabric.Checked = false;

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
                    var Whses = context.TLADM_WhseStore.Where(x => !x.WhStore_WhseOrStore && x.WhStore_DepartmentFK == Dept.Dep_Id).OrderBy(x => x.WhStore_Description).ToList();
                    foreach (var Whse in Whses)
                    {
                        cmboStore.Items.Add(new Knitting.CheckComboBoxItem(Whse.WhStore_Id, Whse.WhStore_Description, false));
                    }
                }

                
            }

            cbGradeA.Checked = true;
            cbGradeB.Checked = false;
            cbGradeC.Checked = false;
            cbIncludeGradweAWithWarnings.Checked = false;
            chkNonStandardGrades.Checked = false;

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            formloaded = true;

        }

        private void cmboGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                Object tst = oCmbo.SelectedItem;

                foreach (PropertyInfo prop in tst.GetType().GetProperties())
                {
                    if (prop.Name == "Value")
                    {
                        QueryParms.Grade = Convert.ToString(prop.GetValue(tst));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                YarnReportOptions YarnOpts = new YarnReportOptions();
                var index = -1;

                YarnOpts.BIFSummarised = rbBIFSummarised.Checked;

                QueryParms.BoughtInFabric = chkBoughtInFabric.Checked;
               
                foreach (var element in MandSelected)
                {
                    ++index;
                    if (element)
                    {
                        if (index == 0)
                        {
                            YarnOpts.K10Store = true;
                        }
                        else if (index == 1)
                        {
                            YarnOpts.K10Grade = true;
                        }
                        else if (index == 2)
                        {
                            YarnOpts.GreigeProduct = true;
                        }
                        else if (index == 3)
                        {
                            YarnOpts.K10ProductQ = true;
                        }
                        else if (index == 4)
                        {
                            YarnOpts.K10STF = true;
                        }
                    }
                }

                QueryParms.BoxChecked[0] = (bool)cbGradeA.Checked;
                QueryParms.BoxChecked[1] = (bool)cbGradeB.Checked;
                QueryParms.BoxChecked[2] = (bool)cbGradeC.Checked;
                QueryParms.BoxChecked[3] = (bool)cbIncludeGradweAWithWarnings.Checked;

                QueryParms.NonStandardGrades = (bool)chkNonStandardGrades.Checked;
                
                if (!rbBIFSummarised.Checked)
                {
                    frmKnitViewRep vRep = new frmKnitViewRep(24, QueryParms, YarnOpts);
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
                else
                {
                    frmKnitViewRep vRep = new frmKnitViewRep(30, QueryParms, YarnOpts);
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

                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
                
                cmboProduct.Items.Clear();
                cmboStore.Items.Clear();
                cmboGreigeQuality.Items.Clear();
                chkBoughtInFabric.Checked = false;
                rbBIFSummarised.Checked = true;
                
                Form_Load(this, null);
                
            }
        }

        private void cmboStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int mnbr = Convert.ToInt32(result[1].ToString());
                    MandSelected[mnbr] = true;
                }
                if (oCmbo != null && !oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }

        private void cmboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int mnbr = Convert.ToInt32(result[1].ToString());
                    MandSelected[mnbr] = true;
                }
                if (oCmbo != null && !oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }

        private void cmboProductGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int mnbr = Convert.ToInt32(result[1].ToString());
                    MandSelected[mnbr] = true;
                }

                if (oCmbo != null && !oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }

        private void cmboStockTake_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int mnbr = Convert.ToInt32(result[1].ToString());
                    MandSelected[mnbr] = true;
                }

                if (oCmbo != null && !oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }

        private void TextChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                if (oTxt.Text.ToUpper().Contains("A") ||
                    oTxt.Text.ToUpper().Contains("B") ||
                    oTxt.Text.ToUpper().Contains("C"))
                {
                    oTxt.Text = oTxt.Text.ToUpper();

                    var result = (from u in MandatoryFields
                                  where u[0] == oTxt.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int mnbr = Convert.ToInt32(result[1].ToString());
                        MandSelected[mnbr] = true;
                    }
                }
                else
                {
                    MessageBox.Show("This Field must contain either A, B or C") ;
                }
            }
        }

        private void cmboGreigeQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && !oCmbo.DroppedDown)
               oCmbo.DroppedDown = true;
        }
    }
}

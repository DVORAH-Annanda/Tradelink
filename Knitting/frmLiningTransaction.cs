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
    public partial class frmLiningTransaction : Form
    {
        bool FormLoaded;
        private DataTable dt;

        Knitting.KnitQueryParameters QueryParms;
        Knitting.KnitRepository repo;

        public frmLiningTransaction()
        {
            InitializeComponent();
            this.cmboGreige.CheckStateChanged += new System.EventHandler(this.cmboGreige_CheckStateChanged);

            repo = new KnitRepository();
            dt = new DataTable();

            DataColumn column = new DataColumn();

            //------------------------------------------------------
            // Create column 1. // This is Index Position 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "CompWork_Pk";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 2. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Select";
            column.Caption = "Select Record";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 2. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "PieceNumber";
            column.Caption = "Piece Number";
            column.ReadOnly = true;
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 3. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Quality";
            column.Caption = "Quality";
            column.ReadOnly = true;
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 4. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Grade";
            column.Caption = "Grade";
            dt.Columns.Add(column);

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            int idx = -1;

            foreach (DataColumn col in dt.Columns)
            {
                if (++idx == 0)
                {
                    dataGridView1.Columns[idx].Visible = false;
                }
                else
                {
                    dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
                    dataGridView1.Columns[col.ColumnName].Width = 120;
                }
            }

        }

        private void LiningTransaction_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new KnitQueryParameters();

            using (var context = new TTI2Entities())
            {
                cmboLiningQuality.DataSource = context.TLADM_Griege.Where(x => x.TLGreige_IsLining && !(bool)x.TLGriege_Discontinued).ToList();
                cmboLiningQuality.DisplayMember = "TLGreige_Description";
                cmboLiningQuality.ValueMember = "TLGreige_Id";
                cmboLiningQuality.SelectedValue = -1;
                
                var Grieges = context.TLADM_Griege.Where(x => !(bool)x.TLGriege_Discontinued && !x.TLGreige_IsLining).OrderBy(x=>x.TLGreige_Description).ToList();
                foreach(var Greige in Grieges)
                {
                    cmboGreige.Items.Add(new Knitting.CheckComboBoxItem(Greige.TLGreige_Id, Greige.TLGreige_Description, false));
                }
            }
            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboGreige_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Knitting.CheckComboBoxItem && FormLoaded)
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
                    {
                        QueryParms.Greiges.Remove(value);
                    }
                }
            }
                
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                if(QueryParms.Greiges.Count == 0)
                {
                    MessageBox.Show("Please select at least one record to process");
                    return;
                }

                dt.Rows.Clear();
                using (var context = new TTI2Entities())
                {
                    foreach (var Qual in QueryParms.Greiges)
                    {
                        var Records = (from Qt in context.TLKNI_GreigeProduction
                                       where Qt.GreigeP_Greige_Fk == Qual.TLGreige_Id && !Qt.GreigeP_Dye && Qt.GreigeP_Inspected
                                       select Qt).ToList();
                        if(Records.Count == 0)
                        {
                            var Descrip = context.TLADM_Griege.Find(Qual.TLGreige_Id).TLGreige_Description;

                            MessageBox.Show("No production data found for " + Descrip);
                            continue;
                        }
                        
                        foreach(var Rec in Records)
                        {
                            DataRow NewRow = dt.NewRow();
                            NewRow[0] = Rec.GreigeP_Pk;
                            NewRow[1] = false;
                            NewRow[2] = Rec.GreigeP_PieceNo;
                            NewRow[3] = context.TLADM_Griege.Find(Rec.GreigeP_Greige_Fk).TLGreige_Description;
                            NewRow[4] = Rec.GreigeP_Grade;
                            
                            dt.Rows.Add(NewRow);
                        }

                    }
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                var ChangeTo = (TLADM_Griege)cmboLiningQuality.SelectedItem;
                if(ChangeTo == null)
                {
                    MessageBox.Show("Please select a current lining");
                    return;
                }
                               
                using (var context = new TTI2Entities())
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!dr.Field<bool>(1))
                            continue;

                        var Pk = dr.Field<int>(0);

                        var GriegP = context.TLKNI_GreigeProduction.Find(Pk);
                        if(GriegP != null)
                        {
                            GriegP.GreigeP_Greige_Fk = ChangeTo.TLGreige_Id; 
                        }
                    }

                    if (dt.Rows.Count != 0)
                    {
                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to database");
                            LiningTransaction_Load(this, null);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                }
            }
        }

        private void cmboGreige_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
    }
}

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
    public partial class frmTLDYEReceipe : Form
    {
        Util core;
        bool formloaded;

        bool lNew;
        string[][] MandatoryFields;
        string[][] MandatoryRows;

        bool[] MandSelected;
        bool[] MandRows;

        DyeQueryParameters QueryParms;

        DyeRepository repo;
 
        List<DATA> fieldEntered = new List<DATA>();

        DataGridViewTextBoxColumn selecta;  // 0  index of the main record 
        DataGridViewComboBoxColumn oCmboA;  // 1  ConsumablesDC 
        DataGridViewCheckBoxColumn oChkA;    //2  Grams Per Litre
        DataGridViewTextBoxColumn selectb;  // 3  Ratios
        DataGridViewTextBoxColumn selectc;  // 4  Liquid Ratios

        public frmTLDYEReceipe()
        {
            InitializeComponent();

            core = new Util();

            MandatoryFields = new string[][]
                { 
                    new string[] {"txtProductCode", "Please enter a PROG number", "0"}
                
                };

            MandatoryRows = new string[][]
                {   new string[] {"1", "Please select a consumable", "0"},
                    new string[] {"2", "Please Tick a Grams", "1"},
                    new string[] {"3", "Please enter a MEL/FLC amount", "2"},
                    new string[] {"4", "Please enter a LIQ Ratio", "3"}
                };

            selecta = new DataGridViewTextBoxColumn();   // record index
            selecta.Visible = false;
            selecta.ValueType = typeof(System.Int32);

            oCmboA = new DataGridViewComboBoxColumn();   // Consumables foreign Key
            oCmboA.HeaderText = "Consumables";
            oCmboA.Width = 150;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "g/l";
            oChkA.ValueType = typeof(Boolean);

            selectb = new DataGridViewTextBoxColumn();  //  Mel / FC value
            selectb.HeaderText = "Chemical Ratios to Program Load";
            selectb.ValueType = typeof(decimal);

            selectc = new DataGridViewTextBoxColumn();  //  Liquid Ratio's
            selectc.HeaderText = "Liquid Ratios";
            selectc.ValueType = typeof(int);
            selectc.Width = 150;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns.Add(oCmboA);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns.Add(selectc);

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            this.cmboGreigeQuality.CheckStateChanged += new System.EventHandler(this.cmboQuality_CheckStateChanged);
            
            repo = new DyeRepository();

            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            txtProductCode.Text = string.Empty;

            QueryParms = new DyeQueryParameters();

            rbStandard.Checked = true;

            lNew = true;

            using (var context = new TTI2Entities())
            {
                cmboColours.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                cmboColours.DisplayMember = "Col_Display";
                cmboColours.ValueMember = "Col_Id";
                cmboColours.SelectedValue = -1;

                cmboProductCodes.DataSource = context.TLDYE_RecipeDefinition.Where(x=>x.TLDYE_StandardReceipe).OrderBy(x => x.TLDYE_DefineCode).ToList();
                cmboProductCodes.DisplayMember = "TLDYE_DefineDescription";
                cmboProductCodes.ValueMember = "TLDYE_DefinePK";
                cmboProductCodes.SelectedValue = -1;

                var GreigeQualitys = context.TLADM_GreigeQuality.OrderBy(x => x.GQ_Description).ToList();
                foreach (var Quality in GreigeQualitys)
                {
                    cmboGreigeQuality.Items.Add(new DyeHouse.CheckComboBoxItem(Quality.GQ_Pk, Quality.GQ_Description, false));
                }
                
                oCmboA.DataSource = context.TLADM_ConsumablesDC.OrderBy(x => x.ConsDC_Description).ToList();
                oCmboA.DisplayMember = "ConsDC_Description";
                oCmboA.ValueMember = "ConsDC_Pk";
            }

            txtProgramLoad.KeyPress += core.txtWin_KeyPress;
            txtProgramLoad.KeyDown += core.txtWin_KeyDownJI;
            txtProgramLoad.Text = "0";

            txtProgramVolume.KeyPress += core.txtWin_KeyPress;
            txtProgramVolume.KeyDown += core.txtWin_KeyDownJI;
            txtProgramVolume.Text = "0";
           
            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            formloaded = true; 
        }


        private void cmboQuality_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.FabricQualities.Add(repo.LoadFabricQuality(item._Pk));
                }
                else
                {
                    var value = QueryParms.FabricQualities.Find(it => it.GQ_Pk == item._Pk);
                    if (value != null)
                        QueryParms.FabricQualities.Remove(value);
                }
            }
        }

        private struct DATA
        {
            public int rownumber;
            public bool[] fieldComplete;

            public DATA(int rownumber, bool[] fieldComplete)
            {
                this.rownumber = rownumber;
                this.fieldComplete = fieldComplete;
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && oDgv.CurrentCell.ColumnIndex == 3)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
            else if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && oDgv.CurrentCell.ColumnIndex == 4)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
            else if (combo != null)
            {
                combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }


        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var cell = dataGridView1.CurrentCell;

            if (cb != null)
            {
                var ActiveRow = dataGridView1.CurrentRow.Index;
                var tst = fieldEntered.Find(x => x.rownumber == ActiveRow);
                if (tst.fieldComplete == null)
                {
                    MandRows = core.PopulateArray(MandatoryRows.Length, false);
                    fieldEntered.Add(new DATA(ActiveRow, MandRows));
                    
                }
            }

        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                if (e.RowIndex > 0)
                {
                    // oDgv.Rows[e.RowIndex].Cells[3].Value = 5;
                }
            }
        }

        private void txt(object sender, EventArgs e)
        {
            TextBox oTxtBx = sender as TextBox;
            if (oTxtBx != null && formloaded)
            {

                var result = (from u in MandatoryFields
                              where u[0] == oTxtBx.Name
                              select u).FirstOrDefault();

                int nbr = Convert.ToInt32(result[2].ToString());
                if (oTxtBx.TextLength > 0)
                    MandSelected[nbr] = true;
                else
                {
                    MandSelected[nbr] = false;
                }
            }
        }

        private void cmboColorList_SelectedValueChanged(object sender, EventArgs e)
        {
             ComboBox oCmbo = sender as ComboBox;
             if (oCmbo != null && formloaded)
             {
                 var result = (from u in MandatoryFields
                               where u[0] == oCmbo.Name
                               select u).FirstOrDefault();
                 if(result != null)
                 {
                     int nbr = Convert.ToInt32(result[2].ToString());
                     MandSelected[nbr] = true;
                 }
             }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLADM_Colours SelectedColour = null;

            bool lAdd = false;

            if (lNew)
               lAdd = true;

            if (rbStandard.Checked)
            {
                SelectedColour = (TLADM_Colours)cmboColours.SelectedItem;
                if (SelectedColour == null)
                {
                    MessageBox.Show("Please select a colour from the drop down box provided");
                    return;
                }

                if (QueryParms.FabricQualities.Count == 0)
                {
                    MessageBox.Show("Please add a Quality");
                    return;
                }
            }

            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandSelected, false, MandatoryFields);

                if (!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    if (rbStandard.Checked)
                    {

                        var tst = fieldEntered.Find(x => x.rownumber == dr.Index);
                        if (tst.fieldComplete == null)
                            continue;

                        tst.fieldComplete[1] = true;

                        var cnt = tst.fieldComplete.Where(x => x == false).Count();
                        if (cnt == MandatoryRows.Length)
                            continue;

                        cnt = tst.fieldComplete.Where(x => x == true).Count();
                    }

                   
                }

                // 0  index of the main record 
                // 1  ConsumablesDC 
                // 2  Grams Per Litre
                // 3  Ratios
                // 4  Liquid Ratios
                using (var context = new TTI2Entities())
                {
                    TLDYE_RecipeDefinition rd = new TLDYE_RecipeDefinition();
                    if (!lAdd)
                    {
                        var selected = (TLDYE_RecipeDefinition)cmboProductCodes.SelectedItem;
                        if(selected != null)
                            rd = context.TLDYE_RecipeDefinition.Find(selected.TLDYE_DefinePk);
                    }

                    rd.TLDYE_DefineCode = txtProductCode.Text;
                    
                    if (rbStandard.Checked)
                        rd.TLDYE_DefineDescription = txtProductCode.Text + " " + SelectedColour.Col_Display;
                    else
                        rd.TLDYE_DefineDescription = txtProductCode.Text;

                    if (rbStandard.Checked)
                    {
                        rd.TLDYE_ProgramLoad = Convert.ToInt32(txtProgramLoad.Text);
                        rd.TLDYE_LiquidLoad = Convert.ToInt32(txtProgramVolume.Text);
                        rd.TLDYE_ColorChart_FK = SelectedColour.Col_Id;
                        rd.TLDYE_StandardReceipe = true;
                    }
                    else
                    {
                        rd.TLDYE_LiquidLoad = 0;
                        rd.TLDYE_ProgramLoad = 0;
                        rd.TLDYE_ColorChart_FK = null;
                        rd.TLDYE_StandardReceipe = false;
                    }
                    if (lAdd)
                        context.TLDYE_RecipeDefinition.Add(rd);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                   {
	            	    foreach (var validationErrors in dbEx.EntityValidationErrors)
		                {
		                      foreach (var validationError in validationErrors.ValidationErrors)
		                      {
		                         MessageBox.Show("Property: " + validationError.PropertyName + " Error " + validationError.ErrorMessage);
                              }
                              return;
                        }
		            }
		            catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
            
                        return;
                    }

                    // 0  index of the main record 
                    // 1  ConsumablesDC 
                    // 2  Grams Per Litre
                    // 3  Ratios
                    // 4  Liquid Ratios
                    foreach (DataGridViewRow dr in dataGridView1.Rows)
                    {
                        if (dr.Cells[1].Value == null)
                            continue;

                        var tst = fieldEntered.Find(x => x.rownumber == dr.Index);
                        if (tst.fieldComplete == null)
                            continue;

                        tst.fieldComplete[1] = true;

                        var cnt = tst.fieldComplete.Where(x => x == false).Count();
                        if (cnt == MandatoryRows.Length)
                            continue;

                        lAdd = false;

                        if (dr.Cells[0].Value == null)
                            lAdd = true;

                        cnt = tst.fieldComplete.Where(x => x == true).Count();
                        if (cnt == MandatoryRows.Length)
                        {
                            TLDYE_DefinitionDetails defdet = new TLDYE_DefinitionDetails();
                            if (!lAdd)
                            {
                                var index = Convert.ToInt32(dr.Cells[0].Value.ToString());
                                defdet = context.TLDYE_DefinitionDetails.Find(index);
                            }

                            defdet.TLDYED_Cosumables_FK = (int)dr.Cells[1].Value;
                            if (dr.Cells[2].Value != null)
                            {
                                if ((bool)dr.Cells[2].Value == true)
                                    defdet.TLDYED_LiqCalc = true;
                                else
                                    defdet.TLDYED_LiqCalc = false;
                            }
                            else
                                defdet.TLDYED_LiqCalc = false;

                            defdet.TLDYED_MELFC = (decimal)dr.Cells[3].Value;
                            defdet.TLDYED_LiqRatio = (int)dr.Cells[4].Value;
                            defdet.TLDYED_Receipe_FK = rd.TLDYE_DefinePk;                            

                            if (lAdd)
                                context.TLDYE_DefinitionDetails.Add(defdet);
                        }
                    }
                    //---------------------------------------------------------
                    //
                    //----------------------------------------------------------------
                    if (rbStandard.Checked)
                    {
                        //------------------------------------------------------------------------------------
                        // First we must ensure that any previous records that may exist are deleted and that we start with a clean slate
                        //---------------------------------------------------------------------------
                        context.TLDYE_ReceipeGreigeQual.RemoveRange(context.TLDYE_ReceipeGreigeQual.Where(x => x.TLGQ_ReceipeDef_FK == rd.TLDYE_DefinePk));
                        //------------------------------------------------------
                        // new development 
                        //----------------------------------------------------------
                        foreach (var Qual in QueryParms.FabricQualities)
                        {
                            TLDYE_ReceipeGreigeQual repQual = new TLDYE_ReceipeGreigeQual();
                            repQual.TLGQ_GreigeQuality_FK = Qual.GQ_Pk;
                            repQual.TLGQ_ReceipeDef_FK = rd.TLDYE_DefinePk;

                            context.TLDYE_ReceipeGreigeQual.Add(repQual);
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved to database successfully");


                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.Message);
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                    finally
                    {
                        this.cmboGreigeQuality.Items.Clear();
                        dataGridView1.Rows.Clear();
                        SetUp();
                    }
                }

               
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;
             if (e.ColumnIndex == 1 ||
                 e.ColumnIndex == 3 ||
                 e.ColumnIndex == 4)
             {
                 var record = fieldEntered.Find(x => x.rownumber == e.RowIndex);
                 if (record.fieldComplete != null)
                 {
                     if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                     {
                         var result = (from u in MandatoryRows
                                       where u[0] == e.ColumnIndex.ToString()
                                       select u).FirstOrDefault();

                         if (result != null)
                         {
                             MessageBox.Show(result[1]);
                         }
                         e.Cancel = true;
                     }
                     else
                     {
                         var index = fieldEntered.IndexOf(record);

                         var result = (from u in MandatoryRows
                                           where u[0] == e.ColumnIndex.ToString()
                                           select u).FirstOrDefault();

                         if (result != null)
                         {
                                 int a = Convert.ToInt32(result[2]);
                                 record.fieldComplete[a] = true;
                         }

                         fieldEntered[index] = record;
                         
                     }
                 }
             }
        }

        private void cmboProductCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLDYE_RecipeDefinition)cmboProductCodes.SelectedItem;
                if (selected != null)
                {
                    dataGridView1.Rows.Clear();
                    txtProductCode.Text = selected.TLDYE_DefineCode;
                    //cmboGreigeQuality.SelectedValue = selected.TLDYE_DefineGreigeQual_Fk;
                    if(selected.TLDYE_ColorChart_FK != null)
                        cmboColours.SelectedValue = selected.TLDYE_ColorChart_FK;
                    txtProgramLoad.Text = selected.TLDYE_ProgramLoad.ToString();
                    txtProgramVolume.Text = selected.TLDYE_LiquidLoad.ToString();
                    lNew = false;

                    using (var context = new TTI2Entities())
                    {
                        var ExistingData = context.TLDYE_DefinitionDetails.Where(x => x.TLDYED_Receipe_FK == selected.TLDYE_DefinePk).ToList();
                        foreach (var row in ExistingData)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.TLDYED_PK;
                            dataGridView1.Rows[index].Cells[1].Value = row.TLDYED_Cosumables_FK;
                            dataGridView1.Rows[index].Cells[2].Value = row.TLDYED_LiqCalc;
                            dataGridView1.Rows[index].Cells[3].Value = row.TLDYED_MELFC;
                            dataGridView1.Rows[index].Cells[4].Value = row.TLDYED_LiqRatio;

                            MandRows = core.PopulateArray(MandatoryRows.Length, true);
                            fieldEntered.Add(new DATA(index, MandRows));
                           
                         }
 
                        this.cmboGreigeQuality.Items.Clear();
                        QueryParms = new DyeQueryParameters();

                        var GreigeQualities = context.TLADM_GreigeQuality.OrderBy(x => x.GQ_Description).ToList();
                        foreach (var Quality in GreigeQualities)
                        {
                            var Existing = context.TLDYE_ReceipeGreigeQual.Where(x => x.TLGQ_GreigeQuality_FK == Quality.GQ_Pk && x.TLGQ_ReceipeDef_FK == selected.TLDYE_DefinePk).FirstOrDefault();
                            if (Existing != null)
                            {
                                cmboGreigeQuality.Items.Add(new DyeHouse.CheckComboBoxItem(Quality.GQ_Pk, Quality.GQ_Description, true));
                                QueryParms.FabricQualities.Add(repo.LoadFabricQuality(Quality.GQ_Pk));
                            }
                            else
                            {
                                cmboGreigeQuality.Items.Add(new DyeHouse.CheckComboBoxItem(Quality.GQ_Pk, Quality.GQ_Description, false));
                            }
                        } 
                        
                      }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                dataGridView1.Rows.Clear();
                SetUp();
                lNew = true;
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;

            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded && e.Button.ToString() == "Right")
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        var index = this.dataGridView1.SelectedRows[0].Index;

                        DataGridViewRow cr = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            int RecNo = (int)cr.Cells[0].Value;
                            var locRec = context.TLDYE_DefinitionDetails.Find(RecNo);
                            if (locRec != null)
                            {
                                try
                                {
                                    context.TLDYE_DefinitionDetails.Remove(locRec);
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }

                        }
                        fieldEntered.RemoveAt(index);
                        oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);

                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
        
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
        }

        private void dataGridView1_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void btnColour_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLDYE_RecipeDefinition rd;

            if (oBtn != null && formloaded)
            {
                if (txtProductCode.Text.Length <= 0)
                {
                    MessageBox.Show("Please enter a code prior to selecting this option");
                    return;
                }

                var ComboQual = (TLADM_GreigeQuality)cmboGreigeQuality.SelectedItem;
                if (ComboQual == null)
                {
                    MessageBox.Show("Please select a Greige quality group prior to selecting this option");
                    return;
                }

                if (txtProgramLoad.Text.Length <= 0)
                {
                    MessageBox.Show("Please enter a program load prior to selecting this option");
                    return;
                }

                if (txtProgramVolume.Text.Length <= 0)
                {
                    MessageBox.Show("please enter a program volume prior to selecting this option");
                    return;
                }

                if (lNew)
                {
                    using (var context = new TTI2Entities())
                    {
                        rd = new TLDYE_RecipeDefinition();
                        rd.TLDYE_DefineCode = txtProductCode.Text;
                        rd.TLDYE_DefineDescription = txtProductCode.Text + " " + ComboQual.GQ_Description;
                        rd.TLDYE_ProgramLoad = Convert.ToInt32(txtProgramLoad.Text);
                        rd.TLDYE_LiquidLoad = Convert.ToInt32(txtProgramVolume.Text);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.InnerException.ToString());
                        }
                    }
                }
                else
                {
                    rd = (TLDYE_RecipeDefinition)cmboProductCodes.SelectedItem; 
                }

                using (frmDyeColourDefinition YOAssigned = new frmDyeColourDefinition(rd.TLDYE_DefinePk))
                {
                    DialogResult dr = YOAssigned.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
        }

        private void rbRemedy_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded)
            {
                formloaded = false;

                txtProductCode.Text = string.Empty;

                txtProgramLoad.Text = "0";
                txtProgramLoad.Enabled = false;
                txtProgramVolume.Text = "0";
                txtProgramVolume.Enabled = false;

                cmboColours.SelectedValue = -1;
                cmboColours.Enabled = false;
                cmboGreigeQuality.Enabled = false;

                using (var context = new TTI2Entities())
                {
                    cmboProductCodes.DataSource = null;
                    cmboProductCodes.DataSource = context.TLDYE_RecipeDefinition.Where(x=>!x.TLDYE_StandardReceipe).OrderBy(x =>x.TLDYE_DefineDescription) .ToList();
                    cmboProductCodes.DisplayMember = "TLDYE_DefineDescription";
                    cmboProductCodes.ValueMember = "TLDYE_DefinePK";
                    cmboProductCodes.SelectedValue = -1;
                }

                dataGridView1.Rows.Clear();

                formloaded = true;
            }
        }

        private void rbStandard_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && formloaded)
            {
               
                formloaded = false;

                txtProductCode.Text = string.Empty;
                txtProgramLoad.Text = "0";
                txtProgramLoad.Enabled = true;
                txtProgramVolume.Text = "0";
                txtProgramVolume.Enabled = true;

                cmboColours.SelectedValue = -1;
                cmboColours.Enabled = true;
                cmboGreigeQuality.Enabled = true;

                using (var context = new TTI2Entities())
                {
                    cmboProductCodes.DataSource = null;
                    cmboProductCodes.DataSource = context.TLDYE_RecipeDefinition.Where(x => x.TLDYE_StandardReceipe).OrderBy(x => x.TLDYE_DefineDescription).ToList();
                    cmboProductCodes.DisplayMember = "TLDYE_DefineDescription";
                    cmboProductCodes.ValueMember = "TLDYE_DefinePK";
                    cmboProductCodes.SelectedValue = -1;

                }

                dataGridView1.Rows.Clear();
                formloaded = true;

            }
        }
    }
}

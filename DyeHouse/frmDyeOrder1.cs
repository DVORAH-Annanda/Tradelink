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
    public partial class frmDyeOrder1 : Form
    {
        Util core;
        bool formloaded;
        decimal FabricYield;
        TLADM_ProductRating SelectedBody;
        TLADM_ProductRating SelectedTrim;

        DataTable dt = null;

        //These are to be stored in datagridView1 
        //--------------------------------------------------------------------------------------
        DataGridViewTextBoxColumn selecta;  // 0 index of the main record 
        DataGridViewComboBoxColumn oCmboA;  // 1 Sizes 
       // DataGridViewComboBoxColumn oCmboB;  // 2 label
        DataGridViewTextBoxColumn selectc;  // 2 rating
        DataGridViewTextBoxColumn selectd;  // 3 yield
        DataGridViewTextBoxColumn selecte;  // 4 No of units required
        DataGridViewTextBoxColumn selectf;  // 5 No of units of lost production Dye
        DataGridViewTextBoxColumn selectg;  // 6 No of units of lost production cut
        DataGridViewTextBoxColumn selecth;  // 7 No of units of lost production CMT
        DataGridViewTextBoxColumn selectj;  // 8 Total Kgs of Fabric needed

        //These are to be stored in datagridView2
        //--------------------------------------------------------------------------------------
        DataGridViewTextBoxColumn selectza;  // 0 index of the main record 
        DataGridViewComboBoxColumn oCmboZA;  // 1 Trims 
        DataGridViewTextBoxColumn selectzc;  // 2 rating
        DataGridViewTextBoxColumn selectzd;  // 3 yield
        DataGridViewTextBoxColumn selectze;  // 4 No of units required
        DataGridViewTextBoxColumn selectzf;  // 5 No of units of lost production Dye
        DataGridViewTextBoxColumn selectzg;  // 6 No of units of lost production cut
        DataGridViewTextBoxColumn selectzh;  // 7 No of units of lost production CMT
        DataGridViewTextBoxColumn selectzj;  // 8 Total Kgs of Fabric needed

        bool[] MandSelected;
        string[][] MandatoryFields;

        string[][] MandatoryRows;
        bool[] MandRows;

        IList<TLADM_Colours> prodColours;
        IList<TLADM_ProductRating> prodRatingBody;
        IList<TLADM_ProductRating> prodRatingTrims;
        
        IList<TLADM_Sizes> Listsizes = null;
        IList<TLADM_Trims> Trimsizes = null;

        //---------------------------------------------------------
        // This is a floating variable to store the index key pertaining to the label that
        // the user has to select 
        int Garment_Label_FK;

        List<LINEDATA> fieldEntered = new List<LINEDATA>();

        public frmDyeOrder1()
        {
            InitializeComponent();
            core = new Util();
            MandatoryFields = new string[][]
            {   new string[] {"cmboCustomer", "Please select a customer number", "0"},
                new string[] {"txtCustomerOrder", "Please enter a customer order number", "1"},
                new string[] {"cmboStyles", "Please select a style", "2"},
                new string[] {"cmboFabric", "Please select a fabric", "3"},
                new string[] {"cmboColour", "Please select a colour", "4"},
                new string[] {"cmboLabels", "Please select a label", "5"},
                new string[] {"txtDyeReq", "Please enter a Dye week number", "6"},
                new string[] {"txtCutReq", "Please enter a Cut week number", "7"},
                new string[] {"txtCmtReq", "Please enter a CMT week number", "8"}
            };

            MandatoryRows = new string[][]
                {   new string[] {"1", "Please select a Size / Marker", "0"},
                    new string[] {"4", "Please enter the number of units required", "1"}
                };
        

            //-----------------------------------------------------------------------------
            // DataGridView1
            //----------------------------------------------------------------------------------------
            selecta = new DataGridViewTextBoxColumn();
            selecta.HeaderText = "Index";
            selecta.Visible = false;
            selecta.ValueType = typeof(Int32);

            DataTable dd = new DataTable();

            oCmboA = new DataGridViewComboBoxColumn();
            oCmboA.HeaderText = "Sizes / Markers";
            oCmboA.ValueMember = "Pr_Id";
            oCmboA.DisplayMember = "Pr_Display";

            /*
            oCmboB = new  DataGridViewComboBoxColumn(); 
            oCmboB.HeaderText = "Labels";
            oCmboB.ValueMember = "Lbl_Id";
            oCmboB.DisplayMember = "Lbl_Description";
            */

            selectc = new DataGridViewTextBoxColumn();
            selectc.HeaderText = "Rating";
            selectc.ValueType = typeof(decimal);

            selectd = new DataGridViewTextBoxColumn();
            selectd.HeaderText = "Yield";
            selectd.ValueType = typeof(decimal);
            selectd.ReadOnly = true;

            selecte = new DataGridViewTextBoxColumn();
            selecte.HeaderText = "No of Units";
            selecte.ValueType = typeof(Decimal);

            selectf = new DataGridViewTextBoxColumn();
            selectf.HeaderText = "Prod Loss DYE";
            selectf.ReadOnly = true;
            selectf.ValueType = typeof(Decimal);

            selectg = new DataGridViewTextBoxColumn();
            selectg.HeaderText = "Prod Loss Cut";
            selectg.ReadOnly = true;
            selectg.ValueType = typeof(Decimal);

            selecth = new DataGridViewTextBoxColumn();
            selecth.HeaderText = "Prod Loss CMT";
            selecth.ReadOnly = true;
            selecth.ValueType = typeof(Decimal);

            selectj = new DataGridViewTextBoxColumn();
            selectj.HeaderText = "Total Kgs";
            selectj.ValueType = typeof(Decimal);
            selectj.ReadOnly = true;

            //-----------------------------------------------------------------------------
            // DataGridView2
            //----------------------------------------------------------------------------------------
            selectza = new DataGridViewTextBoxColumn();
            selectza.HeaderText = "Index";
            selectza.Visible = false;
            selectza.ValueType = typeof(Int32);

            oCmboZA = new DataGridViewComboBoxColumn();
            oCmboZA.HeaderText = "Trims";
            oCmboZA.ValueMember = "Pr_Id";
            oCmboZA.DisplayMember = "Pr_Display";
            oCmboZA.Width = 175; 

            selectzc = new DataGridViewTextBoxColumn();
            selectzc.HeaderText = "Rating";
            selectzc.ValueType = typeof(decimal);

            selectzd = new DataGridViewTextBoxColumn();
            selectzd.HeaderText = "Yield";
            selectzd.ValueType = typeof(decimal);
            selectzd.ReadOnly = true;

            selectze = new DataGridViewTextBoxColumn();
            selectze.HeaderText = "No of Units";
            selectze.ValueType = typeof(decimal);

            selectzf = new DataGridViewTextBoxColumn();
            selectzf.HeaderText = "Prod Loss DYE";
            selectzf.ReadOnly = true;
            selectzf.ValueType = typeof(Int32);

            selectzg = new DataGridViewTextBoxColumn();
            selectzg.HeaderText = "Prod Loss Cut";
            selectzg.ReadOnly = true;
            selectzg.ValueType = typeof(Int32);

            selectzh = new DataGridViewTextBoxColumn();
            selectzh.HeaderText = "Prod Loss CMT";
            selectzh.ReadOnly = true;
            selectzh.ValueType = typeof(Int32);

            selectzj = new DataGridViewTextBoxColumn();
            selectzj.HeaderText = "Total Kgs";
            selectzj.ValueType = typeof(Decimal);
            selectzj.ReadOnly = true;

          

            //---------------------------------------------------------------
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns.Add(oCmboA);
            //dataGridView1.Columns.Add(oCmboB);
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns.Add(selecte);
            dataGridView1.Columns.Add(selectf);
            dataGridView1.Columns.Add(selectg);
            dataGridView1.Columns.Add(selecth);
            dataGridView1.Columns.Add(selectj);

          
            //-------------------------------------------------------------------------
            dataGridView2.Columns.Add(selectza);
            dataGridView2.Columns.Add(oCmboZA);
            dataGridView2.Columns.Add(selectzc);
            dataGridView2.Columns.Add(selectzd);
            dataGridView2.Columns.Add(selectze);
            dataGridView2.Columns.Add(selectzf);
            dataGridView2.Columns.Add(selectzg);
            dataGridView2.Columns.Add(selectzh);
            dataGridView2.Columns.Add(selectzj);
          
            //----------------------------------------------------------------------------

            txtCMTPLoss.KeyPress += core.txtWin_KeyPress;
            txtCMTPLoss.KeyDown += core.txtWin_KeyDownOEM;

            txtCutPLoss.KeyPress += core.txtWin_KeyPress;
            txtCutPLoss.KeyDown += core.txtWin_KeyDownOEM;

            txtDyePLoss.KeyPress += core.txtWin_KeyPress;
            txtDyePLoss.KeyDown += core.txtWin_KeyDownOEM;

            txtDyeReq.KeyPress += core.txtWin_KeyPress;
            txtDyeReq.KeyDown += core.txtWin_KeyDown;

            txtCutReq.KeyPress += core.txtWin_KeyPress;
            txtCutReq.KeyDown += core.txtWin_KeyDown;

            txtCmtReq.KeyPress += core.txtWin_KeyPress;
            txtCmtReq.KeyDown += core.txtWin_KeyDown;

            radUnits2Kg.Checked = true;
            //---------------------------------------------------------------------------

            dataGridView1.AllowUserToAddRows = false;
            SetUp();
           
        }

        void SetUp()
        {
            formloaded = false;

            SelectedBody = null;
            SelectedTrim = null;

            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add();

            dataGridView2.Rows.Clear();
            if(dataGridView3.Rows.Count > 0)
            {
                dataGridView3.DataSource = null;
                dataGridView3.Rows.Clear();
            }

            using (var context = new TTI2Entities())
            {
                Listsizes = context.TLADM_Sizes.ToList();
                Trimsizes = context.TLADM_Trims.ToList();

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Dept != null)
                {
                    var Existing = context.TLADM_ProductionLoss.ToList();
                    if (Existing != null)
                    {
                        foreach(var row in Existing)
                        {
                            if (row.TLProdLoss_Dept_Fk == 12)
                                txtDyePLoss.Text = row.TLProdLoss_Percent.ToString();
                            if (row.TLProdLoss_Dept_Fk == 13)
                                txtCutPLoss.Text = row.TLProdLoss_Percent.ToString();
                            if(row.TLProdLoss_Dept_Fk == 16)
                                txtCMTPLoss.Text = row.TLProdLoss_Percent.ToString();
                        }
                        
                    }
                }

                var LastNum = context.TLADM_LastNumberUsed.Find(3);
                if (LastNum != null)
                {
                    txtDyeOrderNo.Text = "DO" + LastNum.col1.ToString().PadLeft(6, '0');
                }

                cmboDyeOrders.DataSource = context.TLDYE_DyeOrder.Where(x=>!x.TLDYO_Closed).OrderBy(x => x.TLDYO_DyeOrderNum).ToList();
                cmboDyeOrders.DisplayMember = "TLDYO_DyeOrderNum";
                cmboDyeOrders.ValueMember = "TLDYO_Pk";

                cmboCustomer.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomer.ValueMember = "Cust_Pk";
                cmboCustomer.DisplayMember = "Cust_Description";
                cmboCustomer.SelectedValue = 0;

                cmboFabric.DataSource = context.TLADM_Griege.Where(x=>!(bool)x.TLGriege_Discontinued).OrderBy(x => x.TLGreige_Description).ToList();
                cmboFabric.ValueMember = "TLGreige_Id";
                cmboFabric.DisplayMember = "TLGreige_Description";
                cmboFabric.SelectedValue = 0;


                cmboColour.ValueMember = "Col_Id";
                cmboColour.DisplayMember = "Col_Display";
                cmboColour.SelectedValue = 0;

                cmboDyeOrders.SelectedValue = 0;
                cmboStyles.SelectedValue = 0;

                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
                MandRows = core.PopulateArray(MandatoryRows.Length, false);

                txtTotalKgs.Text = "0.00";
                FabricYield = 0;

                txtCmtReq.Text = "0";
                txtCutReq.Text = "0";
                txtDyeReq.Text = "0";
                txtCustomerOrder.Text = string.Empty;

            }

            formloaded = true;
        }

        private void cmboFabric_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_Griege)cmboFabric.SelectedItem;
                if (selected != null)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;
                    }

                    using (var context = new TTI2Entities())
                    {
                        var FabWeight = context.TLADM_FabricWeight.Find(selected.TLGreige_FabricWeight_FK);
                        var FabWidth = context.TLADM_FabWidth.Find(selected.TLGreige_FabricWidth_FK);
                        FabricYield = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);
                        var FabQual = context.TLADM_GreigeQuality.Where(x => x.GQ_Pk == selected.TLGreige_Quality_FK).FirstOrDefault();
                    }
                }

                if (formloaded && dataGridView1.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var CurrentRow = row;

                        if (CurrentRow.Cells[3].Value != null)
                        {
                            decimal FY = (decimal)CurrentRow.Cells[3].Value;

                            if (FY != FabricYield)
                            {
                                dataGridView1.Rows[CurrentRow.Index].Cells[3].Value = Math.Round(FabricYield,4);
                                dataGridView1.CurrentCell = dataGridView1[4, CurrentRow.Index];
                                dataGridView1.CurrentCell = dataGridView1[5, CurrentRow.Index];
                                dataGridView1.CurrentCell = dataGridView1[4, CurrentRow.Index];
                            }
                        }
                    }
                }
            }
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            decimal Greige_FK = 0.00M;

            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_Styles)cmboStyles.SelectedItem;
                if(selected != null)
                {

                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;
                    }

                    dataGridView1.Rows.Clear();
                    oCmboA.DataSource = null;
                    oCmboA.Items.Clear();
                    dataGridView1.Rows.Add();

                    dataGridView2.Rows.Clear();
                    oCmboZA.Items.Clear();

                    prodRatingBody = new List<TLADM_ProductRating>();
                    prodRatingTrims = new List<TLADM_ProductRating>();
                    prodColours = new List<TLADM_Colours>();
                    
                    var custSelectedValue = cmboCustomer.SelectedValue;

                    using (var context = new TTI2Entities())
                    {
                        var StyleColours = context.TLADM_StyleColour.Where(x => x.STYCOL_Style_FK == selected.Sty_Id).ToList();
                        foreach(var StyleColour in StyleColours)
                        {
                            var ColDetail = context.TLADM_Colours.Find(StyleColour.STYCOL_Colour_FK);
                            if (ColDetail != null)
                            {
                                prodColours.Add(ColDetail);
                            }
                        }

                        cmboColour.DataSource = prodColours;
                        MandSelected[4] = false;
                        prodRatingBody = context.TLADM_ProductRating.Where(x => !x.Pr_Discontinued && x.Pr_Style_FK == selected.Sty_Id && x.Pr_Customer_FK == (int)custSelectedValue && x.Pr_BodyorRibbing == 1).ToList();
                        foreach (var row in prodRatingBody)
                        {
                            /*
                            StringBuilder description = new StringBuilder();
                            List<int> xx = core.ExtrapNumber(row.Pr_PowerN, context.TLADM_Sizes.Count());
                            xx.Sort();

                            foreach (var rw in xx)
                            {
                                foreach (var dd in Listsizes)
                                {
                                    if (dd.SI_PowerN == rw)
                                    {
                                        if (description.Length == 0)
                                            description.Append(dd.SI_Description);
                                        else
                                            description.Append(", " + dd.SI_Description);
                                    }
                                }
                            }
                            */
                            row.Pr_Display = row.Pr_Display;
                            try
                            {
                                oCmboA.Items.Add(row);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }

                        int CSV = (int)custSelectedValue;
                       
                        prodRatingTrims = context.TLADM_ProductRating.Where(x => !x.Pr_Discontinued && x.Pr_Style_FK == selected.Sty_Id && x.Pr_Customer_FK == (int)custSelectedValue && x.Pr_BodyorRibbing == 0).ToList();
                       
                        foreach (var prod in prodRatingTrims)
                        {
                            TLADM_ProductRating pr = new TLADM_ProductRating();
                            //===================================================
                            // Note 1 : Because the greige used to manufacture the trims can be different
                            // to that of the body, I use the row.Pr_Size_Power Number to store the greige Primary key
                            // used to knit the trims. This is called later  
                            //=======================================================
                            pr = prod;
                            
                            /* var Trims = context.TLADM_Trims.Find(pr.Pr_Trim_FK);
                            if (Trims != null)
                            {
                                pr.Pr_Display = Trims.TR_Description;
                                pr.Pr_Size_Power = (int)Trims.TR_Greige_FK;
                            }*/

                            oCmboZA.Items.Add(pr);
                        }

                        if (selected.Sty_Labels_FK > 0)
                        {
                            IList<TLADM_Labels> Labels = new List<TLADM_Labels>();
                            Labels.Add(context.TLADM_Labels.Find(selected.Sty_Labels_FK));
                            if (Labels.Count != 0)
                            {
                                cmboLabels.DataSource = null;
                                cmboLabels.DataSource = Labels;
                                cmboLabels.ValueMember = "Lbl_Id";
                                cmboLabels.DisplayMember = "Lbl_Description";
                                cmboLabels.SelectedValue = -1;
                            }
                        }
                    }
                }
            }
        }

        private void cmboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null & formloaded)
            {
                var selected = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                if (selected != null)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;
                    }
                    
                    // dataGridView1.Rows.Clear();
                    oCmboA.Items.Clear();

                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        try
                        {
                            cmboStyles.DataSource = context.TLADM_Styles.Where(x => x.Sty_Label_FK == selected.Cust_Pk).OrderBy(x => x.Sty_Description).ToList();
                            cmboStyles.DisplayMember = "Sty_Description";
                            cmboStyles.ValueMember = "Sty_Id";
                        }
                        catch (Exception ex)
                        {
                            var exceptionMessages = new StringBuilder();
                            do
                            {
                                exceptionMessages.Append(ex.Message);
                                ex = ex.InnerException;
                            }
                            while (ex != null);

                            MessageBox.Show(exceptionMessages.ToString());
                        }
                       

                        formloaded = true;
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            TextBox txtBox = e.Control as TextBox;
            ComboBox combo = e.Control as ComboBox;
            
            DataGridViewCell Cell = oDgv.CurrentCell;
            
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell &&
            (oDgv.CurrentCell.ColumnIndex == 4))
            {
                if (radUnits2Kg.Checked)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
            }
            else if (combo != null)
            {
                if (Cell.ColumnIndex == 1)
                {
                    combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                    combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                }
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var cell = dataGridView1.CurrentCell;
            if (cb != null && cell.ColumnIndex != 2)
            {
               dataGridView1.Rows[cell.RowIndex].Cells[3].Value = Math.Round(FabricYield, 4);
               dataGridView1.Rows[cell.RowIndex].Cells[4].Value = 0;
               dataGridView1.Rows[cell.RowIndex].Cells[5].Value = 0;
               dataGridView1.Rows[cell.RowIndex].Cells[6].Value = 0;
               dataGridView1.Rows[cell.RowIndex].Cells[7].Value = 0;
               dataGridView1.Rows[cell.RowIndex].Cells[8].Value = 0;
               var selected = (TLADM_ProductRating)cb.SelectedItem;
               if (selected != null)
               {
                   SelectedBody = selected;

                   dataGridView1.Rows[cell.RowIndex].Cells[2].Value = Math.Round(selected.Pr_numeric_Rating, 4);
               }
            }
            if (cell.ColumnIndex == 1)
            {
                var ActiveRow = dataGridView1.CurrentRow.Index;
                var tst = fieldEntered.Find(x => x.rownumber == ActiveRow);
                if (tst.fieldComplete == null)
                {
                    MandRows = core.PopulateArray(MandatoryRows.Length, false);
                    fieldEntered.Add(new LINEDATA(ActiveRow, MandRows));
                }
            }
            else if (cell.ColumnIndex == 2)
            {
                var selected = (TLADM_Labels)cb.SelectedItem;
                if (selected != null)
                {
                    Garment_Label_FK = selected.Lbl_Id;
                }
            }
        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            DataGridViewCell Cell = oDgv.CurrentCell;
            Decimal Loss = 0.00M;

            if (oDgv != null)
            {
                if (e.ColumnIndex == 4 && !String.IsNullOrEmpty(Cell.EditedFormattedValue.ToString()))
                {
                    int Col = 5;
               
                    var val = Convert.ToDecimal(Cell.EditedFormattedValue.ToString());

                    int i = 0;
                    do
                    {
                        if (i == 0)
                            {
                                if (radUnits2Kg.Checked)
                                {
                                    Loss = Convert.ToDecimal(txtDyePLoss.Text);
                                    val = core.ProdLoss(val, Loss);
                                }
                                else
                                {
                                    Loss = Convert.ToDecimal(txtDyePLoss.Text);
                                    val = Math.Round(core.ProdNLoss(val, Loss), 4);
                                }
                            }
                            else if (i == 1)
                            {
                                if (radUnits2Kg.Checked)
                                {
                                    Loss = Convert.ToDecimal(txtCutPLoss.Text);
                                    val = core.ProdLoss(val, Loss);
                                }
                                else
                                {
                                    Loss = Convert.ToDecimal(txtCutPLoss.Text);
                                    val = Math.Round(core.ProdNLoss(val, Loss), 4);
                                }
                            }
                            else if (i == 2)
                            {
                                if (radUnits2Kg.Checked)
                                {
                                    Loss = Convert.ToDecimal(txtCMTPLoss.Text);
                                    val = core.ProdLoss(val, Loss);
                                }
                                else
                                {
                                    Loss = Convert.ToDecimal(txtCMTPLoss.Text);
                                    val = Math.Round(core.ProdNLoss(val, Loss), 4);
                                }
                        }
                        
                        oDgv.Rows[e.RowIndex].Cells[Col].Value = val;

                        
                        if(radUnits2Kg.Checked )
                            oDgv.Rows[e.RowIndex].Cells[Col].Value = (int)val;

                        ++Col;
                        i++;

                    } while (i < 3);

                    //--------------------------------------------------------
                    // do the final calculation here
                    //-------------------------------------------------------------
                    try
                    {
                        if (radUnits2Kg.Checked)
                        {
                            var Rating = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[2].Value.ToString()); // ?? 0.00M;
                            var Yield = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[3].Value.ToString()); // ?? 0.00M;
                            var units = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[7].Value.ToString());
                            oDgv.Rows[e.RowIndex].Cells[8].Value = Math.Round(Rating / Yield * units, 4);
                        }
                        else
                        {
                            var Rating = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                            var Yield = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[3].Value.ToString()); // ?? 0.00M;
                            var units = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[7].Value.ToString());
                            oDgv.Rows[e.RowIndex].Cells[8].Value = Math.Round(Yield / Rating * units, 4);
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                   
                }
            }
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            TextBox txtBox = e.Control as TextBox;
            ComboBox combo = e.Control as ComboBox;

            DataGridViewCell Cell = oDgv.CurrentCell;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell &&
                oDgv.CurrentCell.ColumnIndex == 4)
            {
                if (radUnits2Kg.Checked)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
            }
            else if (combo != null)
            {
                if (Cell.ColumnIndex == 1)
                {
                    combo.SelectedIndexChanged -= new EventHandler(ComboBox1_SelectedIndexChanged);
                    combo.SelectedIndexChanged += new EventHandler(ComboBox1_SelectedIndexChanged);
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var cell = dataGridView2.CurrentCell;
            decimal FY = 0.00M;
            if (cb != null)
            {
               var selected = (TLADM_ProductRating)cb.SelectedItem;
               if (selected != null)
               {
                   SelectedTrim = selected;

                   dataGridView2.Rows[cell.RowIndex].Cells[2].Value = selected.Pr_numeric_Rating;
                   using (var context = new TTI2Entities())
                   {
                       var Trims = context.TLADM_Trims.Find(selected.Pr_Trim_FK);
                       if (Trims != null)
                       {
                            if (!(bool)Trims.Tr_Body)
                           {
                               var Greige = context.TLADM_Griege.Find(Trims.TR_Greige_FK);
                               if (Greige != null)
                               {
                                   if (!Trims.TR_IsBinding)
                                   {
                                       var FabWeight = context.TLADM_FabricWeight.Find(Greige.TLGreige_FabricWeight_FK);
                                       var FabWidth = context.TLADM_FabWidth.Find(Greige.TLGreige_FabricWidth_FK);
                                       FY = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);
                                       dataGridView2.Rows[cell.RowIndex].Cells[3].Value = Math.Round(FY, 4);
                                   }
                                   else
                                   {
                                       // Bindings take the same fabric quality as the body which is stored
                                       // in data
                                       var GreigeBody = (TLADM_Griege)cmboFabric.SelectedItem;
                                       if (GreigeBody != null)
                                       {
                                           var FabWeight = context.TLADM_FabricWeight.Find(GreigeBody.TLGreige_FabricWeight_FK);
                                           var FabWidth = context.TLADM_FabWidth.Find(GreigeBody.TLGreige_FabricWidth_FK);
                                           FY = core.FabricYield(FabWeight.FWW_Calculation_Value, FabWidth.FW_Calculation_Value);
                                           // dataGridView2.Rows[cell.RowIndex].Cells[2].Value = (decimal)dataGridView1.Rows[0].Cells[2].Value;
                                           dataGridView2.Rows[cell.RowIndex].Cells[3].Value = Math.Round(FY, 4);
                                       }
                                   }
                               }
                           }
                           else
                           {
                               dataGridView2.Rows[cell.RowIndex].Cells[2].Value =  selected.Pr_numeric_Rating;
                               dataGridView2.Rows[cell.RowIndex].Cells[3].Value = Math.Round(FabricYield, 4);
                           }
                       }
                   }
                   dataGridView2.Rows[cell.RowIndex].Cells[4].Value = 0;
                   dataGridView2.Rows[cell.RowIndex].Cells[5].Value = 0;
                   dataGridView2.Rows[cell.RowIndex].Cells[6].Value = 0;
                   dataGridView2.Rows[cell.RowIndex].Cells[7].Value = 0;
                   dataGridView2.Rows[cell.RowIndex].Cells[8].Value = 0;
               }
            }
        }

        private void dataGridView2_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            DataGridViewCell Cell = oDgv.CurrentCell;
            Decimal Loss = 0.00M;
            Decimal val = 0.00M;

            if (oDgv != null)
            {
                if (e.ColumnIndex == 2)
                {
                    if (SelectedBody.Pr_MultiMarker && SelectedTrim.Pr_BodyorRibbing == 0 && SelectedTrim.Pr_Size_FK > 0)
                    {
                        if (!Trimsizes.Where(x => x.TR_Id == SelectedTrim.Pr_Trim_FK).FirstOrDefault().TR_IsBinding)
                        {
                            var SingleRow = (from Rows in dataGridView3.Rows.Cast<DataGridViewRow>()
                                             where (int)Rows.Cells[0].Value == SelectedTrim.Pr_Size_FK
                                             select Rows).FirstOrDefault();


                            if (SingleRow != null)
                            {
                                int a;
                                oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = (int)SingleRow.Cells[3].Value;
                            }

                        }
                    }
                }
                else if (e.ColumnIndex == 4)
                {
                    int Col = 5;
                    val = Convert.ToDecimal(Cell.EditedFormattedValue.ToString());
                    int i = 0;
                    do
                    {
                            if (i == 0)
                            {
                                if (radUnits2Kg.Checked)
                                {
                                    Loss = Convert.ToDecimal(txtDyePLoss.Text);
                                    val = core.ProdLoss(val, Loss);
                                }
                                else
                                {
                                    Loss = Convert.ToDecimal(txtDyePLoss.Text);
                                    val = Math.Round(core.ProdNLoss(val, Loss), 4);
                                }
                            }
                            else if (i == 1)
                            {
                                if (radUnits2Kg.Checked)
                                {
                                    Loss = Convert.ToDecimal(txtCutPLoss.Text);
                                    val = core.ProdLoss(val, Loss);
                                }
                                else
                                {
                                    Loss = Convert.ToDecimal(txtCutPLoss.Text);
                                    val = Math.Round(core.ProdNLoss(val, Loss), 4);
                                }
                            }
                            else if (i == 2)
                            {
                                if (radUnits2Kg.Checked)
                                {
                                    Loss = Convert.ToDecimal(txtCMTPLoss.Text);
                                    val = core.ProdLoss(val, Loss);
                                }
                                else
                                {
                                    Loss = Convert.ToDecimal(txtCMTPLoss.Text);
                                    val = Math.Round(core.ProdNLoss(val, Loss), 4);
                                }
                            }

                            if(!SelectedBody.Pr_MultiMarker)
                                oDgv.Rows[e.RowIndex].Cells[Col].Value = val;
                            else
                                if(!Trimsizes.Where(x=>x.TR_Id == SelectedTrim.Pr_Trim_FK).FirstOrDefault().TR_IsBinding)
                                    oDgv.Rows[e.RowIndex].Cells[Col].Value = Convert.ToInt32(oDgv.Rows[e.RowIndex].Cells[4].Value.ToString());
                                else
                                    oDgv.Rows[e.RowIndex].Cells[Col].Value = (int)val;

                            ++Col;
                            i++;

                    } while (i < 3);

                    //--------------------------------------------------------
                    // do the final calculation here
                    //-------------------------------------------------------------
                    try
                    {
                        if (radUnits2Kg.Checked)
                        {
                            var Yield = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[3].Value.ToString());
                            var units = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[7].Value.ToString());
                            var Rating = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                            oDgv.Rows[e.RowIndex].Cells[8].Value = Math.Round(Rating / Yield * units, 4);
                        }
                        else
                        {
                            var Rating = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[2].Value.ToString());
                            var Yield = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[3].Value.ToString());
                            var units = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[7].Value.ToString());
                            oDgv.Rows[e.RowIndex].Cells[8].Value = Math.Round(Yield / Rating * units, 4);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                 }
            }
        }

        private void radUnits2Kg_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    selecte.HeaderText = "No of Units";
                    selectj.HeaderText = "Total Kgs";
                    selectze.HeaderText = "No of Units";
                    selectzj.HeaderText = "Total Kgs";
                }
                else
                {
                    selecte.HeaderText = "Kg Amount";
                    selectj.HeaderText = "Total Units";
                    selectze.HeaderText = "Kg Amount";
                    selectzj.HeaderText = "Total Units";
                }
            }
        }
        
        private void txtDyeReq_TextChanged(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool Process = false;
            bool Add = true;
            if (oBtn != null && formloaded)
            {
                var ErrM = core.returnMessage(MandSelected, false, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrM))
                {
                    MessageBox.Show(ErrM);
                    return;
                }

                foreach (DataGridViewRow dr in dataGridView1.Rows)
                {
                    var tst = fieldEntered.Find(x => x.rownumber == dr.Index);
                    if (tst.fieldComplete == null)
                        continue;

                    var cnt = tst.fieldComplete.Where(x => x == true).Count();
                    if (cnt != MandatoryRows.Length)
                    {
                        MessageBox.Show("Line " + (1 + dr.Index).ToString() + " Has not been completed correctly");
                        return;
                    }

                    Process = true;
                }

                if (Process)
                {
                    var DO = (TLDYE_DyeOrder)cmboDyeOrders.SelectedItem;

                    TLDYE_DyeOrder DyeOrder = new TLDYE_DyeOrder();
                    using (var context = new TTI2Entities())
                    {
                       
                        if (DO != null)
                        {
                            DyeOrder = context.TLDYE_DyeOrder.Find(DO.TLDYO_Pk);
                            Add = false;
                        }

                        if (Add)
                        {
                            var LNU = context.TLADM_LastNumberUsed.Find(3);
                            if (LNU != null)
                                LNU.col1 += 1;
                        }
                        else
                        {
                            if (DyeOrder.TLDYO_Colour_FK != (int)cmboColour.SelectedValue)
                            {
                                var DyeBatches = context.TLDYE_DyeBatch.Where(x => x.DYEB_DyeOrder_FK == DO.TLDYO_Pk).ToList();
                                foreach (var DyeBatch in DyeBatches)
                                {
                                    DyeBatch.DYEB_Colour_FK = (int)cmboColour.SelectedValue;
                                }
                            }
                        }

                        DyeOrder.TLDYO_DyeOrderNum = txtDyeOrderNo.Text;
                        DyeOrder.TLDYO_OrderNum = txtCustomerOrder.Text;
                        DyeOrder.TLDYO_OrderDate = dtpDyeOrderDate.Value;
                        DyeOrder.TLDYO_CMTPLoss = Convert.ToDecimal(txtCMTPLoss.Text);
                        DyeOrder.TLDYO_CMTReqWeek = Convert.ToInt32(txtCmtReq.Text);
                        DyeOrder.TLDYO_Colour_FK = (int)cmboColour.SelectedValue;
                        DyeOrder.TLDYO_Customer_FK = (int)cmboCustomer.SelectedValue;
                        DyeOrder.TLDYO_CutPLoss = Convert.ToDecimal(txtCutPLoss.Text);
                        DyeOrder.TLDYO_CutReqWeek = Convert.ToInt32(txtCutReq.Text);
                        DyeOrder.TLDYO_DyePLoss = Convert.ToDecimal(txtDyePLoss.Text);
                        DyeOrder.TLDYO_DyeReqWeek = Convert.ToInt32(txtDyeReq.Text);
                        DyeOrder.TLDYO_GarmOrFab = true;
                        DyeOrder.TLDYO_Greige_FK = (int)cmboFabric.SelectedValue;
                        DyeOrder.TLDYO_Notes = txtNotes.Text;
                        DyeOrder.TLDYO_Style_FK = (int)cmboStyles.SelectedValue;
                        DyeOrder.TLDYO_Label_FK = (int)cmboLabels.SelectedValue;

                        
                        if (Add)
                        {

                            context.TLDYE_DyeOrder.Add(DyeOrder);
                            try
                            {
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                var exceptionMessages = new StringBuilder();
                                do
                                {
                                    exceptionMessages.Append(ex.Message);
                                    ex = ex.InnerException;
                                }
                                while (ex != null);
                                MessageBox.Show(exceptionMessages.ToString());

                            }
                        }
                        //=========================================================
                        // This code handles the top window contents
                        // the main body . While it is a loop it will only loop once 
                        // as there is normally only one body record in this window
                        //============================================================
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLDYE_DyeOrderDetails DyeOrderDet = new TLDYE_DyeOrderDetails();
                            if (row.Cells[0].Value == null)
                                DyeOrderDet.TLDYOD_DyeOrder_Fk = DyeOrder.TLDYO_Pk;
                            else
                            {
                                DyeOrderDet = context.TLDYE_DyeOrderDetails.Find((int)row.Cells[0].Value);
                                Add = false;
                            }

                            DyeOrderDet.TLDYOD_BodyOrTrim = true;
                            DyeOrderDet.TLDYOD_Greige_FK = (int)cmboFabric.SelectedValue;
                            DyeOrderDet.TLDYOD_MarkerRating_FK = (int)row.Cells[1].Value;
                            DyeOrderDet.TLDYOD_Labels_FK = 0; // (int)row.Cells[2].Value;
                            DyeOrderDet.TLDYOD_Rating = (decimal)row.Cells[2].Value;
                            DyeOrderDet.TLDYOD_Yield = (decimal)row.Cells[3].Value;
                            DyeOrderDet.TLDYOD_OriginalUnit = (int)Convert.ToDecimal(row.Cells[4].Value.ToString());
                            DyeOrderDet.TLDYOD_Units = (int)Convert.ToDecimal(row.Cells[7].Value.ToString());
                            DyeOrderDet.TLDYOD_Kgs = (decimal)row.Cells[8].Value;
                            if(Add)
                                context.TLDYE_DyeOrderDetails.Add(DyeOrderDet);
                        }

                        //=========================================================
                        // This code handles the bottom window contents
                        // the Trims . There can be x Records  in this window
                        // 
                        //============================================================
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;
                            
                            Add = true;
                            TLDYE_DyeOrderDetails DyeOrderDet = new TLDYE_DyeOrderDetails();
                            if (row.Cells[0].Value == null)
                                DyeOrderDet.TLDYOD_DyeOrder_Fk = DyeOrder.TLDYO_Pk;
                            else
                            {
                                DyeOrderDet = context.TLDYE_DyeOrderDetails.Find((int)row.Cells[0].Value);
                                Add = false;
                            }

                            DyeOrderDet.TLDYOD_BodyOrTrim = false;
                            DyeOrderDet.TLDYOD_MarkerRating_FK = (int)row.Cells[1].Value;

                            var MarkerRating_Pk = (int)row.Cells[1].Value;
                            TLADM_ProductRating ProdRating = context.TLADM_ProductRating.Find(MarkerRating_Pk);
                            if (ProdRating != null)
                            {
                                var Trim = context.TLADM_Trims.Find(ProdRating.Pr_Trim_FK);
                                if (Trim != null)
                                {
                                    DyeOrderDet.TLDYOD_Trims_FK = (int)Trim.TR_Id;

                                    if (!(bool)Trim.Tr_Body)
                                        DyeOrderDet.TLDYOD_Greige_FK = (int)Trim.TR_Greige_FK;
                                    else
                                        DyeOrderDet.TLDYOD_Greige_FK = (int)cmboFabric.SelectedValue;

                                
                                    if (Trim.TR_IsBinding)
                                        DyeOrderDet.TLDYOD_Greige_FK = (int)cmboFabric.SelectedValue;
                                }
                            }
                            
                            DyeOrderDet.TLDYOD_Rating = (decimal)row.Cells[2].Value;
                            DyeOrderDet.TLDYOD_Yield = (decimal)row.Cells[3].Value;
                            DyeOrderDet.TLDYOD_OriginalUnit = (int)Convert.ToDecimal(row.Cells[4].Value.ToString());
                            DyeOrderDet.TLDYOD_Units = (int)Convert.ToDecimal(row.Cells[7].Value.ToString());
                            DyeOrderDet.TLDYOD_Kgs = Convert.ToDecimal(row.Cells[8].Value.ToString());
                            if (Add)
                                context.TLDYE_DyeOrderDetails.Add(DyeOrderDet);
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to database");
                            if (Add)
                            {
                                frmDyeViewReport vRep = new frmDyeViewReport(3, DyeOrder.TLDYO_Pk);
                                int h = Screen.PrimaryScreen.WorkingArea.Height;
                                int w = Screen.PrimaryScreen.WorkingArea.Width;
                                vRep.ClientSize = new Size(w, h);
                                vRep.ShowDialog(this);
                                
                            }
                            dataGridView3.DataSource = null;
                            SetUp();
                        }
                        catch (Exception ex)
                        {
                            var exceptionMessages = new StringBuilder();
                            do
                            {
                                exceptionMessages.Append(ex.Message);
                                ex = ex.InnerException;
                            }
                            while (ex != null);
                            MessageBox.Show(exceptionMessages.ToString());
                        }

                    }
                }
            }
        }

        private void cmboColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var loc = e.Location;
            var Cell = oDgv.CurrentCell;
           
        }

        private void txtDyeReq_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
          
            TextBox oTxtBx = sender as TextBox;

            var result = (from u in MandatoryFields
                          where u[0] == oTxtBx.Name
                          select u).FirstOrDefault();

            int nbr = Convert.ToInt32(result[2].ToString());
            if (!MandSelected[nbr])
            {
                if (e.KeyCode == Keys.Tab)
                {
                    e.IsInputKey = true;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Tab)
                {
                    e.IsInputKey = false;
                }
            }
     
        }

        private void txtCutReq_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TextBox oTxtBx = sender as TextBox;

            var result = (from u in MandatoryFields
                          where u[0] == oTxtBx.Name
                          select u).FirstOrDefault();

            int nbr = Convert.ToInt32(result[2].ToString());
            if (!MandSelected[nbr])
            {
                if (e.KeyCode == Keys.Tab)
                {
                    e.IsInputKey = true;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Tab)
                {
                    e.IsInputKey = false;
                }
            }
        }

        private void txtCmtReq_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            TextBox oTxtBx = sender as TextBox;

            var result = (from u in MandatoryFields
                          where u[0] == oTxtBx.Name
                          select u).FirstOrDefault();

            int nbr = Convert.ToInt32(result[2].ToString());
            if (!MandSelected[nbr])
            {
                if (e.KeyCode == Keys.Tab)
                {
                    e.IsInputKey = true;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Tab)
                {
                    e.IsInputKey = false;
                }
            }
        }

        private struct LINEDATA
        {
            public int rownumber;
            public bool[] fieldComplete;

            public LINEDATA(int rownumber, bool[] fieldComplete)
            {
                this.rownumber = rownumber;
                this.fieldComplete = fieldComplete;
            }
        }
       

        private struct DATA
        {
            public int SizeNo;
            public string SizeDescription;
            public decimal MarkerRatio;
            public int Qty;

            public DATA(int _SizeNo, string _SizeDescription, decimal _MarkerRatio, int _Qty)
            {
                this.SizeNo = _SizeNo;
                this.SizeDescription = _SizeDescription;
                this.MarkerRatio = _MarkerRatio;
                this.Qty = _Qty;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (oDgv != null && e.ColumnIndex == 7)
            {
                decimal Tot = 0;
                var Cell = oDgv.Rows[e.RowIndex].Cells[7].Value;

                if (core.IsValueDidgit(Cell.ToString()))
                {
                    List<DATA> fieldEntered = new List<DATA>();

                   dataGridView3.DataSource = null;
                   dataGridView3.Rows.Clear();

                   using (var context = new TTI2Entities())
                   {
                        dt = new DataTable();
                        dt.Columns.Add("Index", typeof(int));
                        dt.Columns.Add("Description", typeof(string));
                        dt.Columns.Add("Ratio", typeof(decimal));
                        dt.Columns.Add("Quantity", typeof(int));

                        dt.Columns[0].ReadOnly = true;
                        dt.Columns[1].ReadOnly = true;
                        dt.Columns[2].ReadOnly = true;
                        dt.Columns[3].ReadOnly = true;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            var Pk = Convert.ToInt32(row.Cells[1].Value.ToString());
                            var Qty = Convert.ToDecimal(row.Cells[7].Value.ToString());

                            if (Qty == 0)
                                Qty = Convert.ToInt32(Cell.ToString());

                            var Existing = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == Pk).ToList();
                            if (Existing.Count() > 0)
                            {
                                 var Total = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == Pk).Sum(x => x.Prd_MarkerRatio);
                                 foreach (var ExistRow in Existing)
                                 {
                                     if (ExistRow.Prd_MarkerRatio == 0)
                                         continue;
                                     
                                     decimal Percentage = ExistRow.Prd_MarkerRatio / Total;

                                     var record = fieldEntered.Find(x => x.SizeNo == ExistRow.Prd_SizePN);
                                     var index =  fieldEntered.IndexOf(record);
                                     if (index == -1)
                                     {
                                         var size = context.TLADM_Sizes.Find(ExistRow.Prd_SizePN);
                                         if (size != null)
                                         {
                                             var temp = Percentage * Qty;
                                             decimal xdec = temp - Math.Truncate(temp);
                                             if (xdec >= 0.50M)
                                             {
                                                 fieldEntered.Add(new DATA(ExistRow.Prd_SizePN, size.SI_Description, ExistRow.Prd_MarkerRatio, (int)(Math.Ceiling(Percentage * Qty))));
                                             }
                                             else
                                             {
                                                 fieldEntered.Add(new DATA(ExistRow.Prd_SizePN, size.SI_Description, ExistRow.Prd_MarkerRatio, (int)(Math.Floor(Percentage * Qty))));
                                             }
                                         }
                                     }
                                     else
                                     {
                                         record.Qty += (int)(Math.Ceiling(Percentage * Qty));
                                         fieldEntered[index] = record;
                                     }
                                }
                            }

                            Tot += Convert.ToDecimal(row.Cells[8].Value);
                        }

                        foreach (var row in fieldEntered)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = row.SizeNo;
                            dr[1] = row.SizeDescription;
                            dr[2] = row.MarkerRatio;
                            dr[3] = row.Qty;
                            dt.Rows.Add(dr);
                        }

                        dt.DefaultView.Sort = "Index";
                        dataGridView3.DataSource = dt;
                        dataGridView3.Columns[0].Visible = false;
                   }
              
                }
            }
            else if (oDgv != null && e.ColumnIndex == 8)
            {
                decimal Tot = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Tot += Convert.ToDecimal(row.Cells[8].Value);
                }

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    Tot += Convert.ToDecimal(row.Cells[8].Value);
                }
                txtTotalKgs.Text = Tot.ToString();

            }
            else if (oDgv != null && e.ColumnIndex == 4)
            {
                int Col = 5;
                DataGridViewCell Cell = oDgv.CurrentCell;
                Decimal Loss = 0.00M;

                if (e.RowIndex != -1)
                {
                    var val = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    int i = 0;
                    do
                    {
                        
                            if (i == 0)
                            {
                                if (radUnits2Kg.Checked)
                                {
                                    Loss = Convert.ToDecimal(txtDyePLoss.Text);
                                    val = core.ProdLoss(val, Loss);
                                }
                                else
                                {
                                    Loss = Convert.ToDecimal(txtDyePLoss.Text);
                                    val = Math.Round(core.ProdNLoss(val, Loss), 1);
                                }
                            }
                            else if (i == 1)
                            {
                                if (radUnits2Kg.Checked)
                                {
                                    Loss = Convert.ToDecimal(txtCutPLoss.Text);
                                    val = core.ProdLoss(val, Loss);
                                }
                                else
                                {
                                    Loss = Convert.ToDecimal(txtCutPLoss.Text);
                                    val = Math.Round(core.ProdNLoss(val, Loss), 1);
                                }
                            }
                            else if (i == 2)
                            {
                                if (radUnits2Kg.Checked)
                                {
                                    Loss = Convert.ToDecimal(txtCMTPLoss.Text);
                                    val = core.ProdLoss(val, Loss);
                                }
                                else
                                {
                                    Loss = Convert.ToDecimal(txtCMTPLoss.Text);
                                    val = Math.Round(core.ProdNLoss(val, Loss), 1);
                                }
                            }
                        

                        oDgv.Rows[e.RowIndex].Cells[Col].Value = Math.Round(val,0);


                        if (radUnits2Kg.Checked)
                            oDgv.Rows[e.RowIndex].Cells[Col].Value = (int)val;

                        ++Col;
                        i++;

                    } while (i < 3);
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (!formloaded)
                return;

            if ((e.ColumnIndex == 1 ||
                e.ColumnIndex == 2 ||
                e.ColumnIndex == 4) && radUnits2Kg.Checked)
            {
                var record = fieldEntered.Find(x => x.rownumber == e.RowIndex);
                if (record.fieldComplete != null)
                {
                    if (e.ColumnIndex != 4)
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
                    else
                    {
                        if (String.IsNullOrEmpty(e.FormattedValue.ToString()) || Convert.ToInt32(e.FormattedValue.ToString()) == 0)
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
        }

        private void cmboDyeOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            int Greige_FK = 0;
            List<int> xx = null;
           
            if (oCmbo != null && formloaded)
            {
                var selected = (TLDYE_DyeOrder)cmboDyeOrders.SelectedItem;
                if (selected != null)
                {
                    formloaded = false;
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    if (dataGridView3.Rows.Count > 0)
                    {
                        dataGridView3.DataSource = null;
                        dataGridView3.Rows.Clear();
                    }
                   
                    oCmboA.DataSource = null;
                    oCmboZA.DataSource = null;

                    oCmboA.Items.Clear();
                    oCmboZA.Items.Clear();
                   
                    formloaded = true;

                    prodRatingBody = new List<TLADM_ProductRating>();
                    prodRatingTrims = new List<TLADM_ProductRating>();
                    prodColours = new List<TLADM_Colours>();

                    txtCmtReq.Text = selected.TLDYO_CMTReqWeek.ToString();
                    txtCustomerOrder.Text = selected.TLDYO_OrderNum;
                    txtCutReq.Text = selected.TLDYO_CutReqWeek.ToString();
                    txtDyeOrderNo.Text = selected.TLDYO_DyeOrderNum;
                    dtpDyeOrderDate.Value = selected.TLDYO_OrderDate;
                    txtDyeReq.Text = selected.TLDYO_DyeReqWeek.ToString();


                    formloaded = true;
                    cmboCustomer.SelectedValue = (int)selected.TLDYO_Customer_FK;

                    formloaded = false;
                    cmboFabric.SelectedValue = (int)selected.TLDYO_Greige_FK;
                    cmboLabels.SelectedValue = (int)selected.TLDYO_Label_FK;
                    cmboStyles.SelectedValue = (int)selected.TLDYO_Style_FK;
                    formloaded = true;

                    using (var context = new TTI2Entities())
                    {
                        IList<TLADM_Labels> Labels = new List<TLADM_Labels>();
 
                        Labels.Add(context.TLADM_Labels.Find((int)selected.TLDYO_Label_FK));
                        if (Labels.Count != 0)
                        {
                            cmboLabels.DataSource = null;
                            cmboLabels.DataSource = Labels;
                            cmboLabels.ValueMember = "Lbl_Id";
                            cmboLabels.DisplayMember = "Lbl_Description";
                            cmboLabels.SelectedValue = -1;
                        }
                        var StyleColours = context.TLADM_StyleColour.Where(x => x.STYCOL_Style_FK == selected.TLDYO_Style_FK).ToList();
                        foreach (var StyleColour in StyleColours)
                        {
                            var ColDetail = context.TLADM_Colours.Find(StyleColour.STYCOL_Colour_FK);
                            if (ColDetail != null)
                            {
                                prodColours.Add(ColDetail);
                            }
                        }

                        cmboColour.DataSource = prodColours;
                        cmboColour.SelectedValue = (int)selected.TLDYO_Colour_FK;

                        MandSelected = core.PopulateArray(MandSelected.Length, true);
                        
                        prodRatingBody = context.TLADM_ProductRating.Where(x => x.Pr_Style_FK == selected.TLDYO_Style_FK && x.Pr_Customer_FK == selected.TLDYO_Customer_FK && x.Pr_BodyorRibbing == 1).ToList();
                        foreach (var row in prodRatingBody)
                        {
                            StringBuilder description = new StringBuilder();
                            xx = core.ExtrapNumber(row.Pr_PowerN, context.TLADM_Sizes.Count());
                            xx.Sort();
                     
                            foreach (var rw in xx)
                            {
                                foreach (var dd in Listsizes)
                                {
                                    if (dd.SI_PowerN == rw)
                                    {
                                        if (description.Length == 0)
                                            description.Append(dd.SI_Description);
                                        else
                                            description.Append(", " + dd.SI_Description);
                                    }
                                }
                            }

                            row.Pr_Display = description.ToString();
                            oCmboA.Items.Add(row);
                        }
                        
                        var DODetails = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == selected.TLDYO_Pk && x.TLDYOD_BodyOrTrim).ToList();
                        foreach (var row in DODetails)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.TLDYOD_Pk;
                            //===========================================
                            //DJL 
                            //==================================
                            var Existing = prodRatingBody.FirstOrDefault(x => x.Pr_Id == row.TLDYOD_MarkerRating_FK);
                            if (Existing != null)
                            {
                                dataGridView1.Rows[index].Cells[1].Value = row.TLDYOD_MarkerRating_FK;
                                dataGridView1.Rows[index].Cells[2].Value = row.TLDYOD_Rating;
                                dataGridView1.Rows[index].Cells[3].Value = row.TLDYOD_Yield;
                                dataGridView1.Rows[index].Cells[4].Value = row.TLDYOD_OriginalUnit;
                                dataGridView1.Rows[index].Cells[8].Value = row.TLDYOD_Kgs;
                                MandRows = core.PopulateArray(MandatoryRows.Length, true);
                            }
                            else
                            {
                                dataGridView1.Rows[index].Cells[1].Value = prodRatingBody.FirstOrDefault().Pr_Id;
                                dataGridView1.Rows[index].Cells[2].Value = prodRatingBody.FirstOrDefault().Pr_numeric_Rating;
                                dataGridView1.Rows[index].Cells[3].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[4].Value = 0.00M;
                                dataGridView1.Rows[index].Cells[8].Value = 0.00M;
                                MandRows = core.PopulateArray(MandatoryRows.Length, false);
                            }
                            //===============================================================
                            fieldEntered.Add(new LINEDATA(index, MandRows));
                        }
                       
                        prodRatingTrims = context.TLADM_ProductRating.Where(x => x.Pr_Style_FK == selected.TLDYO_Style_FK && x.Pr_Customer_FK == selected.TLDYO_Customer_FK && x.Pr_BodyorRibbing == 0).OrderBy(x=>x.Pr_Display).ToList();
                        foreach (var row in prodRatingTrims)
                        {
                            TLADM_ProductRating pr = new TLADM_ProductRating();

                            pr = row;

                            var Trims = context.TLADM_Trims.Find(row.Pr_Trim_FK);
                            if (Trims != null)
                            {
                                // Note 1 : Because the greige used to manufacture the trims can be different
                                // to that of the body, I use the row.Pr_Size_Poer Number to store the greige Primary key
                                // used to knit the trims. This is later called  
                                pr.Pr_Size_Power = (int)Trims.TR_Greige_FK;
                                pr.Pr_Display = Trims.TR_Description;

                                try
                                {
                                    oCmboZA.Items.Add(pr);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                       

                        DODetails = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == selected.TLDYO_Pk && !x.TLDYOD_BodyOrTrim).ToList();
                        foreach (var row in DODetails)
                        {
                            var index = dataGridView2.Rows.Add();
                            dataGridView2.Rows[index].Cells[0].Value = row.TLDYOD_Pk;
                            //==============================================================
                            // because of a change in the way we store the Trims key data 
                            var StyTrim = context.TLADM_StyleTrim.Where(x => x.StyTrim_Styles_Fk == selected.TLDYO_Style_FK && x.StyTrim_Trim_Fk == row.TLDYOD_Trims_FK && x.StyTrim_ProdRating_FK != 0).FirstOrDefault();
                            if(StyTrim != null)
                                dataGridView2.Rows[index].Cells[1].Value = StyTrim.StyTrim_ProdRating_FK;

                            dataGridView2.Rows[index].Cells[2].Value = row.TLDYOD_Rating;
                            dataGridView2.Rows[index].Cells[3].Value = row.TLDYOD_Yield;
                            dataGridView2.Rows[index].Cells[4].Value = row.TLDYOD_OriginalUnit;
                            dataGridView2.Rows[index].Cells[8].Value = row.TLDYOD_Kgs;
                        }
                                   
                     }
                }
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (oDgv != null && e.ColumnIndex == 4)
            {
                int Col = 5;
                DataGridViewCell Cell = oDgv.CurrentCell;
                Decimal Loss = 0.00M;
                if (e.RowIndex != -1)
                {
                    var val = Convert.ToDecimal(oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    int i = 0;
                    do
                    {
                        if (i == 0)
                        {
                            if (radUnits2Kg.Checked)
                            {
                                Loss = Convert.ToDecimal(txtDyePLoss.Text);
                                val = core.ProdLoss(val, Loss);
                            }
                            else
                            {
                                Loss = Convert.ToDecimal(txtDyePLoss.Text);
                                val = Math.Round(core.ProdNLoss(val, Loss), 4);
                            }
                        }
                        else if (i == 1)
                        {
                            if (radUnits2Kg.Checked)
                            {
                                Loss = Convert.ToDecimal(txtCutPLoss.Text);
                                val = core.ProdLoss(val, Loss);
                            }
                            else
                            {
                                Loss = Convert.ToDecimal(txtCutPLoss.Text);
                                val = Math.Round(core.ProdNLoss(val, Loss), 4);
                            }
                        }
                        else if (i == 2)
                        {
                            if (radUnits2Kg.Checked)
                            {
                                Loss = Convert.ToDecimal(txtCMTPLoss.Text);
                                val = core.ProdLoss(val, Loss);
                            }
                            else
                            {
                                Loss = Convert.ToDecimal(txtCMTPLoss.Text);
                                val = Math.Round(core.ProdNLoss(val, Loss), 4);
                            }
                        }

                        oDgv.Rows[e.RowIndex].Cells[Col].Value = val;


                        if (radUnits2Kg.Checked)
                            oDgv.Rows[e.RowIndex].Cells[Col].Value = (int)val;

                        ++Col;
                        i++;

                    } while (i < 3);
                }
            }
            else if (oDgv != null && e.ColumnIndex == 8)
            {
                decimal Tot = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    Tot += Convert.ToDecimal(row.Cells[8].Value);
                }

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    Tot += Convert.ToDecimal(row.Cells[8].Value);
                }
                txtTotalKgs.Text = Tot.ToString();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                var selected = (TLDYE_DyeOrder)cmboDyeOrders.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a valid Dye Order");
                    return;
                }

                DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    using (var context = new TTI2Entities())
                    {
                        try
                        {
                            context.TLDYE_DyeOrderDetails.RemoveRange(context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == selected.TLDYO_Pk));
                            var locRec = context.TLDYE_DyeOrder.Find(selected.TLDYO_Pk);
                            if (locRec != null)
                            {
                                context.TLDYE_DyeOrder.Remove(locRec);
                                context.SaveChanges();
                                SetUp();
                            }
                        }
                        catch (Exception ex)
                        {
                            var exceptionMessages = new StringBuilder();
                            do
                            {
                                exceptionMessages.Append(ex.Message);
                                ex = ex.InnerException;
                            }
                            while (ex != null);
                            MessageBox.Show(exceptionMessages.ToString());
                        }

                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var DyeOrder = (TLDYE_DyeOrder)cmboDyeOrders.SelectedItem;
                if (DyeOrder != null)
                {
                    DialogResult res = MessageBox.Show("Please confirm this action", "Confirmation required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Record = context.TLDYE_DyeOrder.Find(DyeOrder.TLDYO_Pk);
                            if (Record != null)
                            {
                                Record.TLDYO_Closed = true;

                                try
                                {
                                    context.SaveChanges();
                                    MessageBox.Show("Record successfully updated");
                                }
                                catch (Exception ex)
                                {
                                    var exceptionMessages = new StringBuilder();
                                    do
                                    {
                                        exceptionMessages.Append(ex.Message);
                                        ex = ex.InnerException;
                                    }
                                    while (ex != null);
                                    MessageBox.Show(exceptionMessages.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }

        private void txtDyeReq_Leave(object sender, EventArgs e)
        {
            TextBox oTxtBx = sender as TextBox;
            if (oTxtBx != null && formloaded)
            {
              
                if (oTxtBx.Name == "txtDyeReq" ||
                    oTxtBx.Name == "txtCutReq" ||
                    oTxtBx.Name == "txtCmtReq")
                {
                    if(oTxtBx.Text.Length > 0)
                    {
                        var TxtVal = Convert.ToInt32(oTxtBx.Text);
                        var CurrentWeek = core.GetIso8601WeekOfYear(dtpDyeOrderDate.Value);

                        if (TxtVal < 1 || TxtVal > 52)
                        {
                            MessageBox.Show("Please enter a value between 1 and 52", "Current Week is " + CurrentWeek.ToString());
                            oTxtBx.Text = "0";
                            oTxtBx.Focus();
                            return;
                        }

                        if (dtpDyeOrderDate.Value.Month < 12)
                        {
                            if (TxtVal <= CurrentWeek)
                            {
                                MessageBox.Show("Please enter a week value greater than this week", "Current Week is " + CurrentWeek.ToString());
                                oTxtBx.Text = "0";
                                oTxtBx.Focus();
                                return;
                            }
                        }
                        else
                        {
                            //we are in december and the rules change 
                            //----------------------------------------------------
                            if (TxtVal > CurrentWeek)
                                return;

                            if (TxtVal > 4)
                            {
                                MessageBox.Show("Please enter a week value no greater than the last week of January", "Current Week is " + CurrentWeek.ToString());
                                oTxtBx.Text = "0";
                                oTxtBx.Focus();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void radMeters2Kg_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
          
            if (oRad != null && formloaded)
            {
                    if (oRad.Checked)
                    {
                        selecte.HeaderText = "Meters";
                        selectj.HeaderText = "Total Kgs";
                        selectze.HeaderText = "Meters";
                        selectzj.HeaderText = "Total Kgs";
                    }
                    else
                    {
                        selecte.HeaderText = "Kg Amount";
                        selectj.HeaderText = "Meters";
                        selectze.HeaderText = "Kg Amount";
                        selectzj.HeaderText = "Meters";
                    }
                }
       }
      

        private void cmboLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }
    }
}

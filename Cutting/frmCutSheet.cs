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

namespace Cutting
{
    public partial class frmCutSheet : Form
    {
        bool formloaded;
        object[] ColumnHeadings;
        int BatchKey;

        Util core;

        DataGridViewTextBoxColumn oTxtZA;  // Size index 
        DataGridViewTextBoxColumn oTxtZB;  // Size 
        DataGridViewTextBoxColumn oTxtZC;  // Ratio 
        DataGridViewTextBoxColumn oTxtZD;  // Garments 
        DataGridViewTextBoxColumn oTxtZE;  // Trims 
        DataGridViewTextBoxColumn oTxtZF;  // Binding 
        DataGridViewTextBoxColumn oTxtZG;  // The size power Number 
        DataGridViewTextBoxColumn oTxtZH;  // The Estimated Weight after binding taken off

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key (DYE Batch)          0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Primary Key (Greige Production)  1 
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // Check box                        2
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Piece                            3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Nett                             4
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // Quality                          5
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();   // Tex                              6
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();   // Yarn Supplier                    7 
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();   // K/Order                          9
        DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn();   // Colour;                         10
        DataGridViewTextBoxColumn oTxtK = new DataGridViewTextBoxColumn();   // Grade                           11

        DataGridViewTextBoxColumn oTxtL = new DataGridViewTextBoxColumn();   // Piece Remarks     3             12
        DataGridViewTextBoxColumn oTxtR = new DataGridViewTextBoxColumn();   // No 1              4             13
        DataGridViewTextBoxColumn oTxt1 = new DataGridViewTextBoxColumn();   // No 2              5             14
        DataGridViewTextBoxColumn oTxt2 = new DataGridViewTextBoxColumn();   // No 3              6             15
        DataGridViewTextBoxColumn oTxt3 = new DataGridViewTextBoxColumn();   // No 4              7             16
        DataGridViewTextBoxColumn oTxt4 = new DataGridViewTextBoxColumn();   // No 5              8             17
        DataGridViewTextBoxColumn oTxt5 = new DataGridViewTextBoxColumn();   // No 6              9             18
        DataGridViewTextBoxColumn oTxt6 = new DataGridViewTextBoxColumn();   // No 7             10             19
        DataGridViewTextBoxColumn oTxt7 = new DataGridViewTextBoxColumn();   // No 8             11             20  
        DataGridViewTextBoxColumn oTxt8 = new DataGridViewTextBoxColumn();   // No 9             12             21 
 
        IList<TLADM_ProductRating> prodRatingBody;
        IList<TLADM_ProductRating> prodRatingTrims;

        IList<TLADM_Sizes> Listsizes = null;
        IList<TLADM_Trims> Trimsizes = null;

        TLDYE_DyeBatch DyeBatchSelected = null;

        private Dictionary<int, int> store = new Dictionary<int, int>();
        const int initialValue = -1;

        bool PrevCSSelected;

        BindingList<KeyValuePair<int, decimal>> CurrentRatios;

               
        public frmCutSheet()
        {
            InitializeComponent();

            core = new Util();

            ColumnHeadings = new Object[21];

            ColumnHeadings[0] = oTxtA;  //  Primary Key (DYE Batch Detail)
            ColumnHeadings[1] = oTxtB;  //  Indicator for 
            ColumnHeadings[2] = oChkA;  //  Select Option
            ColumnHeadings[3] = oTxtC;  //  Piece No
            ColumnHeadings[4] = oTxtD;  //  Nett
            ColumnHeadings[5] = oTxtE;  //  Quality
            ColumnHeadings[6] = oTxtF;  //  Text
            ColumnHeadings[7] = oTxtG;  //  Yarn Supplier
            ColumnHeadings[8] = oTxtH;  //  K/Order
            ColumnHeadings[9] = oTxtJ;  //  Colour
            ColumnHeadings[10] = oTxtK; //  Grade
            ColumnHeadings[11] = oTxtL; //  Remarks
            ColumnHeadings[12] = oTxtR; //  measur1 
            ColumnHeadings[13] = oTxt1; //  measur2
            ColumnHeadings[14] = oTxt2; //  measur3
            ColumnHeadings[15] = oTxt3; //  measur4
            ColumnHeadings[16] = oTxt4; //  measur5
            ColumnHeadings[17] = oTxt5; //  measur6
            ColumnHeadings[18] = oTxt6; //  measur7
            ColumnHeadings[19] = oTxt7; //  measur8
            ColumnHeadings[20] = oTxt8; //  measur9
        }

        private void frmCutSheet_Load(object sender, EventArgs e)
        {
            formloaded = false;
            
            PrevCSSelected = false;

            ChkAccepted.Checked = false;
       

            oTxtZA = new DataGridViewTextBoxColumn();     // 0  
            oTxtZA.ValueType = typeof(int);
            oTxtZA.Visible = false;
            oTxtZA.ReadOnly = true;

            oTxtZB = new DataGridViewTextBoxColumn();     // 1
            oTxtZB.HeaderText = "Size";
            oTxtZB.ValueType = typeof(string);
            oTxtZB.ReadOnly = true;

            oTxtZC = new DataGridViewTextBoxColumn();     // 2
            oTxtZC.HeaderText = "Ratio";
            oTxtZC.ValueType = typeof(string);
           // oTxtZC.ReadOnly = true;

            oTxtZD = new DataGridViewTextBoxColumn();     // 3
            oTxtZD.HeaderText = "Garments";
            oTxtZD.ValueType = typeof(int);
            oTxtZD.ReadOnly = true;

            oTxtZE = new DataGridViewTextBoxColumn();      // 4
            oTxtZE.HeaderText = "Binding(Kg)";
            oTxtZE.ValueType = typeof(Decimal);
            oTxtZE.ReadOnly = true;

            oTxtZF = new DataGridViewTextBoxColumn();      // 5
            oTxtZF.HeaderText = "Trim(Kg)";
            oTxtZF.ValueType = typeof(Decimal);
            oTxtZF.ReadOnly = true;

            oTxtZG = new DataGridViewTextBoxColumn();     //  6
            oTxtZG.HeaderText = "Power";
            oTxtZG.Visible = false;
            oTxtZG.ValueType = typeof(int);
            oTxtZG.ReadOnly = true;

            oTxtZH = new DataGridViewTextBoxColumn();     //  7
            oTxtZH.HeaderText = "Estimated Nett Kg";
            oTxtZH.Visible = false;
            oTxtZH.ValueType = typeof(decimal);
            oTxtZH.ReadOnly = true;

            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.AllowUserToOrderColumns = false;
            this.dataGridView2.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView2_EditingControlShowing);
            
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AllowUserToOrderColumns = false;

            dataGridView2.Columns.Add(oTxtZA);
            dataGridView2.Columns.Add(oTxtZB);
            dataGridView2.Columns.Add(oTxtZC);
            dataGridView2.Columns.Add(oTxtZD);
            dataGridView2.Columns.Add(oTxtZE);
            dataGridView2.Columns.Add(oTxtZF);
            dataGridView2.Columns.Add(oTxtZG);
            dataGridView2.Columns.Add(oTxtZH);

            txtNoGarments.Text = "0";
            txtNoBinding.Text = "0";
            txtNoTrims.Text = "0";
            txtQtyKg.Text = "0.00";
            txtTrimKg.Text = "0.00";
            txtDyeBatchNumber.Text = string.Empty;

            txtBodyRating.KeyPress += core.txtWin_KeyPress;
            txtBodyRating.KeyDown += core.txtWin_KeyDownOEM;

            txtTrimRating.KeyDown += core.txtWin_KeyDownOEM;
            txtTrimRating.KeyPress += core.txtWin_KeyPress;

          
            using (var context = new TTI2Entities())
            {
                Listsizes = context.TLADM_Sizes.Where(x=>!(bool)x.SI_Discontinued).ToList();
                Trimsizes = context.TLADM_Trims.ToList();

                var LNU = context.TLADM_LastNumberUsed.Find(4);
                if (LNU != null)
                {
                    txtLastNumber.Text = "CS" + LNU.col1.ToString().PadLeft(5, '0');
                }

                var h2 = (DataGridViewTextBoxColumn)ColumnHeadings[0];
                h2.HeaderText = "Primary Key (DYE Batch)";
                h2.Visible = false;
                h2.ValueType = typeof(int);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[1];
                h2.HeaderText = "Primary Key (Greige Production)";
                h2.Name = "xxx";
                h2.Visible = false;
                h2.ValueType = typeof(int);
                dataGridView1.Columns.Add(h2);

                var chkh2 = (DataGridViewCheckBoxColumn)ColumnHeadings[2];
                chkh2.HeaderText = "Select";
                dataGridView1.Columns.Add(chkh2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[3];
                h2.HeaderText = "Piece";
                h2.ValueType = typeof(int);
                h2.ReadOnly = true;
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[4];
                h2.HeaderText = "Nett";
                // h2.ReadOnly = true;
                h2.ValueType = typeof(decimal);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[5];
                h2.HeaderText = "Quality";
                h2.ReadOnly = true;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[6];
                h2.HeaderText = "Tex";
                h2.ReadOnly = true;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[7];
                h2.HeaderText = "Yarn Supplier";
                h2.ReadOnly = true;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[8];
                h2.HeaderText = "K/Order";
                h2.ReadOnly = true;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[9];
                h2.HeaderText = "Colour";
                h2.ReadOnly = true;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[10];
                h2.HeaderText = "Grade";
                h2.ReadOnly = true;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                h2 = (DataGridViewTextBoxColumn)ColumnHeadings[11];
                h2.HeaderText = "Remarks";
                h2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                h2.ValueType = typeof(string);
                dataGridView1.Columns.Add(h2);

                cmboCutSheet.DataSource = context.TLCUT_CutSheet.Where(x=>!x.TLCutSH_WIPComplete &&  !x.TLCutSH_Closed ).OrderBy(x => x.TLCutSH_No).ToList();
                cmboCutSheet.ValueMember = "TLCutSH_Pk";
                cmboCutSheet.DisplayMember = "TLCutSH_No";
                cmboCutSheet.SelectedValue = -1;

                cmboColour.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                cmboColour.ValueMember = "Col_Id";
                cmboColour.DisplayMember = "Col_Display";
                cmboColour.SelectedValue = -1;

                cmboDepartment.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCut).OrderBy(x => x.Dep_Description).ToList();
                cmboDepartment.ValueMember = "Dep_Id";
                cmboDepartment.DisplayMember = "Dep_Description";
                cmboDepartment.SelectedValue = -1;

                groupBox2.Enabled = true;

                var Depts = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Depts != null)
                {
                    var Reasons = context.TLADM_QualityDefinition.Where(x => x.QD_ReportingDept_FK == Depts.Dep_Id).OrderBy(x => x.QD_ShortCode).ToList();

                    foreach (var Reason in Reasons)
                    {
                        foreach (var elementH in ColumnHeadings)
                        {
                            Type t = elementH.GetType();
                            if (t.Equals(typeof(DataGridViewCheckBoxColumn)))
                                continue;

                            var ch = (DataGridViewTextBoxColumn)elementH;
                            if (String.IsNullOrEmpty(ch.HeaderText))
                            {
                                StringBuilder sb = new StringBuilder();
                                sb.Append(Reason.QD_ShortCode);
                                // sb.Append(Environment.NewLine);
                                // sb.Append(Reason.QD_Description);
                                ch.HeaderText = sb.ToString();
                                ch.ValueType = typeof(int);
                                ch.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dataGridView1.Columns.Add(ch);
                                break;
                            }
                        }
                    }

                    h2 = (DataGridViewTextBoxColumn)ColumnHeadings[20];
                    h2.HeaderText = "Value of the Marker Rating";
                    h2.Visible = false;
                    h2.ValueType = typeof(int);
                    dataGridView1.Columns.Add(h2);

                }
            }

          

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            formloaded = true;
        }

        void SetUp()
        {
            formloaded = false;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            txtNoGarments.Text  = "0";
            txtNoBinding.Text   = "0";
            txtNoTrims.Text     = "0";
            txtQtyKg.Text       = "0.00";
            txtTrimKg.Text      = "0.00";

            ChkAccepted.Checked = false;

            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(4);
                if (LNU != null)
                {
                    txtLastNumber.Text = "CS" + LNU.col1.ToString().PadLeft(5, '0');
                }
            }

            cmboCutSheet.SelectedValue = -1;
            formloaded = true;
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 4)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }
        void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 2)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void btnDyeBatch_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
                    
            if (sender is Button && formloaded)
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();

                txtNoGarments.Text = "0";
                txtNoTrims.Text = "0";
                txtNoBinding.Text = "0";
                txtQtyKg.Text = "0.00";
                txtTrimKg.Text = "0.00";
                cmboRating.Text = string.Empty;
                cmboRatingTrims.Text = string.Empty;

                try
                {
                    frmCSSelectDyeBatch csBatch = new frmCSSelectDyeBatch();
                    csBatch.ShowDialog(this);

                    int index = csBatch.BatchSelected;
                    if (index != 0)
                    {
                        GetDyeBatch(index, false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void GetDyeBatch(int _Pk, bool Flag)
        {
            BatchKey = _Pk;
            IList<TLDYE_DyeBatchDetails> DbDetails = null;

            using (var context = new TTI2Entities())
            {

                var DB = context.TLDYE_DyeBatch.Find(_Pk);
                if (DB != null)
                {
                    DyeBatchSelected = DB;

                    var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                    if (DO != null)
                    {
                        dtpOrderDate.Value = DO.TLDYO_OrderDate;
                        DateTime dt = core.FirstDateOfWeek(DO.TLDYO_OrderDate.Year, DO.TLDYO_CutReqWeek);
                        dt = dt.AddDays(5);
                        dtpRequiredDate.Value = dt;


                        cmboRating.Items.Clear();
                        cmboRating.DataSource = null;
                        cmboRating.ValueMember = "Pr_Id";
                        cmboRating.DisplayMember = "Pr_Display";

                        cmboRatingTrims.Items.Clear();
                        cmboRatingTrims.DataSource = null;
                        cmboRatingTrims.ValueMember = "Pr_Id";
                        cmboRatingTrims.DisplayMember = "Pr_Display";
                       

                        cmboLabels.DataSource = null;
                        cmboLabels.DataSource = context.TLADM_Labels.Where(x => x.Lbl_Customer_FK == DO.TLDYO_Customer_FK).ToList();
                        cmboLabels.DisplayMember = "Lbl_Description";
                        cmboLabels.ValueMember = "Lbl_Id";
                        cmboLabels.SelectedValue = DO.TLDYO_Label_FK;

                        formloaded = false;
                        cmboStyles.DataSource = null;
                        cmboStyles.DataSource = context.TLADM_Styles.Where(x => x.Sty_Label_FK == DO.TLDYO_Customer_FK).ToList();
                        cmboStyles.DisplayMember = "Sty_Description";
                        cmboStyles.ValueMember = "Sty_Id";
                        cmboStyles.SelectedValue = DO.TLDYO_Style_FK;
                        formloaded = true;
                       
                        txtCustomer.Text = context.TLADM_CustomerFile.Find(DO.TLDYO_Customer_FK).Cust_Description;
                        txtCustomerOrder.Text = DO.TLDYO_OrderNum;
                        txtDyeBatchNumber.Text = DB.DYEB_BatchNo;

                        /*var OrderDetails = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DO.TLDYO_Pk).ToList();
                        var cnt = 0;
                        foreach (var row in OrderDetails)
                        {
                            if (row.TLDYOD_BodyOrTrim)
                            {
                                if (cnt == 0)
                                {
                                    txtQualBody.Text = context.TLADM_Griege.Find(row.TLDYOD_Greige_FK).TLGreige_Description;
                                    var PRating = context.TLADM_ProductRating.Find(row.TLDYOD_MarkerRating_FK);
                                    if(PRating != null)
                                        txtBodyRating.Text = PRating.Pr_numeric_Rating.ToString();
                                    
                                    prodRatingBody = context.TLADM_ProductRating.Where(x => x.Pr_Customer_FK == DO.TLDYO_Customer_FK && x.Pr_Style_FK == DO.TLDYO_Style_FK && x.Pr_BodyorRibbing == 1).ToList();
                                    foreach (var rowx in prodRatingBody)
                                    {
                                        StringBuilder description = new StringBuilder();
                                        List<int> xx = core.ExtrapNumber(rowx.Pr_PowerN, context.TLADM_Sizes.Count());
                                        // List<int> xx = core.ExtrapNumber(rowx.Pr_PowerN, context.TLADM_Sizes.Where(x=>(bool)!x.SI_Discontinued).Count());
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

                                        rowx.Pr_Display = description.ToString();
                                        formloaded = false;
                                        cmboRating.Items.Add(rowx);
                                       
                                        formloaded = true;
                                    }
                                 
                                }
                                cmboRating.SelectedValue = row.TLDYOD_MarkerRating_FK;
                            }
                            else
                            {
                                if (cnt > 0)
                                {
                                    if (cnt == 1)
                                    {
                                        txtQualTrim1.Text = context.TLADM_Griege.Find(row.TLDYOD_Greige_FK).TLGreige_Description;
                                    }
                                    else
                                    {
                                        txtQualTrim2.Text = context.TLADM_Griege.Find(row.TLDYOD_Greige_FK).TLGreige_Description;
                                    }

                                    txtTrimRating.Text = row.TLDYOD_Rating.ToString();
                                    prodRatingTrims = context.TLADM_ProductRating.Where(x => x.Pr_Id == row.TLDYOD_MarkerRating_FK).ToList();
                                    foreach (var rowx in prodRatingTrims)
                                    {
                                        StringBuilder description = new StringBuilder();
                                        List<int> xx = core.ExtrapNumber(rowx.Pr_PowerN, context.TLADM_Trims.Count());
                                        xx.Sort();

                                        foreach (var rw in xx)
                                        {
                                            foreach (var dd in Trimsizes)
                                            {
                                                if (dd.TR_powerN == rw)
                                                {
                                                    if (description.Length == 0)
                                                        description.Append(dd.TR_Description);
                                                    else
                                                        description.Append(", " + dd.TR_Description);
                                                }


                                            }
                                        }

                                        rowx.Pr_Display = description.ToString();
                                        cmboRatingTrims.Items.Add(rowx);
                                    }

                                    cmboRatingTrims.SelectedValue = row.TLDYOD_MarkerRating_FK; 
                                }

                            }
                            ++cnt;
                        }
                        */

                    }

                    // txtColour.Text = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Display;

                    cmboColour.SelectedValue = DB.DYEB_Colour_FK;

                    //---------------------------------------------------------------------------------
                    // Dye Batch Details
                    //-----------------------------------------------------------
                    if(Flag)
                      DbDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).OrderByDescending(x=>x.DYEBD_BodyTrim).ToList();
                    else
                      DbDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk  && x.DYEBO_QAApproved && !x.DYEBO_CutSheet && !x.DYEBO_Sold && !x.DYEBO_WriteOff).OrderByDescending(x=>x.DYEBD_BodyTrim).ToList();
                    
                    bool first = true;
                    foreach (var dbDetail in DbDetails)
                    {
                        if (first && dbDetail.DYEBD_BodyTrim)
                        {
                            formloaded = false;

                             CurrentRatios = core.ReturnRatios((int)dbDetail.DYEBO_ProductRating_FK);

                            var Sizes = context.TLADM_Sizes.Where(x=>(bool)!x.SI_Discontinued).OrderBy(x=>x.SI_DisplayOrder).ToList();
                            foreach (var Size in Sizes)
                            {
                                var SizeIndex = dataGridView2.Rows.Add();
                                dataGridView2.Rows[SizeIndex].Cells[0].Value = Size.SI_id;
                                dataGridView2.Rows[SizeIndex].Cells[1].Value = Size.SI_Description;
                                dataGridView2.Rows[SizeIndex].Cells[2].Value = String.Empty;
                              
                                foreach (var row in CurrentRatios)
                                {
                                    if (row.Key == Size.SI_id)
                                    {
                                        dataGridView2.Rows[SizeIndex].Cells[2].Value = row.Value.ToString();
                                        dataGridView2.Rows[SizeIndex].Cells[3].Value = 0; // row.Value.ToString();
                                        dataGridView2.Rows[SizeIndex].Cells[4].Value = 0; // row.Value.ToString();
                                        dataGridView2.Rows[SizeIndex].Cells[5].Value = 0; // row.Value.ToString();
                                        dataGridView2.Rows[SizeIndex].Cells[6].Value = Size.SI_PowerN; // row.Value.ToString();
                                        dataGridView2.Rows[SizeIndex].Cells[7].Value = 0.00M; // row.Value.ToString();
                                    }
                                }
                            }
                            
                            formloaded = true;
                            first = !first;
                        }
                        // Knitting Production
                        var GP = context.TLKNI_GreigeProduction.Find(dbDetail.DYEBD_GreigeProduction_FK);
                        if (GP != null)
                        {
                            var DetIndex = dataGridView1.Rows.Add();
                            dataGridView1.Rows[DetIndex].Cells[0].Value = dbDetail.DYEBD_Pk;
                            dataGridView1.Rows[DetIndex].Cells[2].Value = false;
                            dataGridView1.Rows[DetIndex].Cells[3].Value = GP.GreigeP_PieceNo;
                            dataGridView1.Rows[DetIndex].Cells[4].Value = dbDetail.DYEBO_Nett;
                            dataGridView1.Rows[DetIndex].Cells[5].Value = context.TLADM_Griege.Find(dbDetail.DYEBD_QualityKey).TLGreige_Description;

                            // Knit Order
                            var KO = context.TLKNI_Order.Find(GP.GreigeP_KnitO_Fk);
                            if(KO != null)
                            {
                                // Yarn Order
                                var YO = context.TLADM_Yarn.Find(KO.KnitO_YarnO_FK);
                                if (YO != null)
                                {
                                    dataGridView1.Rows[DetIndex].Cells[6].Value = YO.YA_Description;
                                    dataGridView1.Rows[DetIndex].Cells[7].Value = context.TLADM_Suppliers.Find(YO.YA_Supplier_FK).Sup_Description;
                                }
                                dataGridView1.Rows[DetIndex].Cells[8].Value = "KO" + KO.KnitO_OrderNumber.ToString().PadLeft(6, '0');


                            }

                            dataGridView1.Rows[DetIndex].Cells[9].Value = "Greige";
                            dataGridView1.Rows[DetIndex].Cells[10].Value = GP.GreigeP_Grade;
                            dataGridView1.Rows[DetIndex].Cells[11].Value = GP.GreigeP_Remarks;
                            dataGridView1.Rows[DetIndex].Cells[12].Value = GP.GreigeP_Meas1;
                            dataGridView1.Rows[DetIndex].Cells[13].Value = GP.GreigeP_Meas2;
                            dataGridView1.Rows[DetIndex].Cells[14].Value = GP.GreigeP_Meas3;
                            dataGridView1.Rows[DetIndex].Cells[15].Value = GP.GreigeP_Meas4;
                            dataGridView1.Rows[DetIndex].Cells[16].Value = GP.GreigeP_Meas5;
                            dataGridView1.Rows[DetIndex].Cells[17].Value = GP.GreigeP_Meas6;
                            dataGridView1.Rows[DetIndex].Cells[18].Value = GP.GreigeP_Meas7;
                            dataGridView1.Rows[DetIndex].Cells[19].Value = GP.GreigeP_Meas8;
                            dataGridView1.Rows[DetIndex].Cells[20].Value = dbDetail.DYEBO_ProductRating_FK;
                        }
                    }
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;
           
            if (oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 using (var context = new TTI2Entities())
                 {
                     var CurrentRow = oDgv.CurrentRow;
                     var CurrentCell = oDgv.CurrentCell;
                     if (CurrentRow != null)
                     {
                         var index = (int)CurrentRow.Cells[0].Value;
                         TLDYE_DyeBatchDetails dbd = context.TLDYE_DyeBatchDetails.Find(index);
                         if (dbd != null)
                         {
                             //Is a body or is it a Trim Record
                             //===================================================
                             if (dbd.DYEBD_BodyTrim)
                             {
                                 //--------------------------------------------------------------
                                 // The aim of this event is to calculate the number of expected units
                                 // based on the nett weight of the piece of fabric selected
                                 // less the weight of the calculated binding needed (if applicable)
                                 //------------------------------------------------------------------
                                 var Yield = core.FabricYield(dbd.DYEBO_DiskWeight, dbd.DYEBO_Width);
                                 
                                 if (Yield == 0)
                                 {
                                     MessageBox.Show("Yield Factor is incorrect", "Error Message Fabric Weight " + dbd.DYEBO_DiskWeight.ToString() + " Fabric Width " + dbd.DYEBO_Width.ToString());
                                     return;
                                 }

                                 var NettWeight = dbd.DYEBO_Nett;
                                 var BindWeight = 0.00M;
                                 var TrimWeight = 0.00M;

                                 //---------------------------------------------------------
                                 // We now have to establish whether the style relating to the dye batch
                                 // has a Binding and if so what is the rating
                                 //----------------------------------------------------------------

                                 var StyleFK = (from DO in context.TLDYE_DyeOrder
                                              join DB in context.TLDYE_DyeBatch on DO.TLDYO_Pk equals DB.DYEB_DyeOrder_FK
                                              join DBD in context.TLDYE_DyeBatchDetails on DB.DYEB_Pk equals DBD.DYEBD_DyeBatch_FK
                                              where DBD.DYEBD_Pk == dbd.DYEBD_Pk
                                              select DO).FirstOrDefault().TLDYO_Style_FK;
                                 
                                 if (StyleFK == 0)
                                 {
                                     MessageBox.Show("Unable to establish a style factor", "Error Message Fabric Weight " + dbd.DYEBO_DiskWeight.ToString() + " Fabric Width " + dbd.DYEBO_Width.ToString());
                                     return;
                                 }

                                 var ISBinding = (from stytrim in context.TLADM_StyleTrim
                                                  join trim in context.TLADM_Trims on stytrim.StyTrim_Trim_Fk equals trim.TR_Id
                                                  join prodrating in context.TLADM_ProductRating on stytrim.StyTrim_ProdRating_FK equals prodrating.Pr_Id
                                                  where stytrim.StyTrim_Styles_Fk == StyleFK && trim.TR_IsBinding
                                                  select prodrating).FirstOrDefault();

                                 if (ISBinding != null)
                                 {
                                     BindWeight = NettWeight * ISBinding.Pr_numeric_Rating;
                                     NettWeight -= BindWeight;
                                 }

                                 var ISTrim = (from stytrim in context.TLADM_StyleTrim
                                                  join trim in context.TLADM_Trims on stytrim.StyTrim_Trim_Fk equals trim.TR_Id
                                                  join prodrating in context.TLADM_ProductRating on stytrim.StyTrim_ProdRating_FK equals prodrating.Pr_Id
                                                  where stytrim.StyTrim_Styles_Fk == StyleFK && !trim.TR_IsBinding
                                                  select prodrating).FirstOrDefault();
                                 
                                 if (ISTrim != null)
                                 {
                                     TrimWeight = dbd.DYEBO_Nett * ISTrim.Pr_numeric_Rating;
                                 }

                               // select trims.TR_Description , pr.Pr_numeric_Rating 
                               // from TLADM_StyleTrim strim
                               // inner join. TLADM_Trims trims
                               // on trims.TR_Id = strim.StyTrim_Trim_Fk 
                               // inner join TLADM_ProductRating pr
                               // on pr.Pr_Id = strim.StyTrim_ProdRating_FK 
                               // where trims.TR_IsBinding = 1 and strim.StyTrim_Styles_Fk = 34
                               //-------------------------------------------------
                                 var ProdFK = dbd.DYEBO_ProductRating_FK;
 
                                 var Rating = context.TLADM_ProductRating.Find(ProdFK).Pr_numeric_Rating;
                                 var TotalQtyKg = Convert.ToDecimal(txtQtyKg.Text);
                                 var NoOfGarments = Convert.ToDecimal(txtNoGarments.Text);

                                 if (e.ColumnIndex != 9999)
                                 {
                                     if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                                     {
                                         dbd.DYEBO_CutSheet = true;

                                         CurrentRow.Cells[4].ReadOnly = true;
                                         NoOfGarments += Math.Round(Yield / Rating * NettWeight);
                                         txtQtyKg.Text = (Math.Round(TotalQtyKg + NettWeight, 1)).ToString();
                                         txtNoGarments.Text = NoOfGarments.ToString();
                                     }
                                     else
                                     {
                                         dbd.DYEBO_CutSheet = false;

                                         CurrentRow.Cells[4].ReadOnly = false;
                                         NoOfGarments -= Math.Round(Yield / Rating * NettWeight /*dbd.DYEBO_Nett*/, 0);
                                         txtQtyKg.Text = (TotalQtyKg - dbd.DYEBO_Nett).ToString();
                                         txtNoGarments.Text = NoOfGarments.ToString();
                                     }
                                 }
                                 else
                                 {
                                     CurrentRow.Cells[4].ReadOnly = true;
                                     NoOfGarments += Math.Round(Yield / Rating * NettWeight);
                                     txtQtyKg.Text = (Math.Round(TotalQtyKg + NettWeight, 1)).ToString();
                                     txtNoGarments.Text = NoOfGarments.ToString();
                                 }
                                 
                                 var Factor = Math.Round(Yield / Rating * NettWeight, 0);
                                 var tst = core.CalculateRatios(ProdFK, (int)Factor);
                                 foreach (DataGridViewRow dr in dataGridView2.Rows)
                                 {
                                     foreach (var row in tst)
                                     {
                                         if ((int)dr.Cells[0].Value == row.Key)
                                         {
                                             decimal EstKg = 0.00M;
                                             decimal BindKg = 0.00M;
                                             decimal TrimKg = 0.00M;

                                             if (row.Value != 0 && Yield != 0)
                                             {
                                                 EstKg = Math.Round(NettWeight * (row.Value / Factor), 2);
                                                 BindKg = Math.Round(BindWeight * (row.Value / Factor), 2);
                                                 TrimKg = Math.Round(TrimWeight * (row.Value / Factor), 2);
                                             }
                                             
                                             if (dr.Cells[3].Value == null)
                                             {
                                                 dr.Cells[3].Value = row.Value;
                                                 dr.Cells[4].Value = BindKg;
                                                 txtNoBinding.Text = (Convert.ToDecimal(txtNoBinding.Text.ToString()) + BindKg).ToString();
                                                 dr.Cells[5].Value = TrimKg;
                                                 txtNoTrims.Text = (Convert.ToDecimal(txtNoTrims.Text.ToString()) + TrimKg).ToString();
                                                 dr.Cells[7].Value = EstKg;
                                             }
                                             else
                                             {
                                                 if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                                                 {
                                                    dr.Cells[3].Value = int.Parse(dr.Cells[3].Value.ToString()) + row.Value;
                                                    dr.Cells[4].Value = Decimal.Parse(dr.Cells[4].Value.ToString()) + BindKg;
                                                    txtNoBinding.Text = (Convert.ToDecimal(txtNoBinding.Text.ToString()) + BindKg).ToString();
                                                    dr.Cells[5].Value = Decimal.Parse(dr.Cells[5].Value.ToString()) + TrimKg;
                                                    txtNoTrims.Text = (Convert.ToDecimal(txtNoTrims.Text.ToString()) + TrimKg).ToString();
                                                    if (dr.Cells[7].Value == null)
                                                        dr.Cells[7].Value = 0.00M;
                                                    dr.Cells[7].Value = Decimal.Parse(dr.Cells[7].Value.ToString()) + EstKg;
                                                  }
                                                  else
                                                  {
                                                     dr.Cells[3].Value = int.Parse(dr.Cells[3].Value.ToString()) - row.Value;
                                                     dr.Cells[4].Value = Decimal.Parse(dr.Cells[4].Value.ToString()) - BindKg;
                                                     txtNoBinding.Text = (Convert.ToDecimal(txtNoBinding.Text.ToString()) - BindKg).ToString();
                                                     dr.Cells[5].Value = Decimal.Parse(dr.Cells[5].Value.ToString()) - TrimKg;
                                                     txtNoTrims.Text = (Convert.ToDecimal(txtNoTrims.Text.ToString()) - TrimKg).ToString();
                                                     if (dr.Cells[7].Value == null)
                                                         dr.Cells[7].Value = 0.00M;

                                                     dr.Cells[7].Value = Decimal.Parse(dr.Cells[7].Value.ToString()) - EstKg;
                                                  }
                                                 
                                             }
                                         }
                                     }
                                 }
                             }
                             else
                             {
                               //--------------------------------------------------------------
                               // It is a Trim
                               //------------------------------------------------------------------
                                 if (e.ColumnIndex != 9999)
                                 {
                                     if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                                     {
                                         var CurrentTotal = decimal.Parse(txtTrimKg.Text.ToString());
                                         txtTrimKg.Text = (Math.Round(CurrentTotal + dbd.DYEBO_Nett, 1)).ToString();
                                     }
                                     else
                                     {
                                         var CurrentTotal = decimal.Parse(txtTrimKg.Text.ToString());
                                         txtTrimKg.Text = (Math.Round(CurrentTotal - dbd.DYEBO_Nett, 1)).ToString();
                                     }
                                 }
                                 else
                                 {
                                     var CurrentTotal = decimal.Parse(txtTrimKg.Text.ToString());
                                     txtTrimKg.Text = (Math.Round(CurrentTotal + dbd.DYEBO_Nett, 1)).ToString();
                                 }
                             }
                        }
                     }
                 }
             }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            TLCUT_CutSheet selected = null;
           
            if (oBtn != null && formloaded)
            {
                if (BatchKey == 0)
                {
                    MessageBox.Show("Please select a Dye Batch");
                    return;
                }

                var Total = Convert.ToInt32(txtNoGarments.Text); 
                            
                if (Total == 0)
                {
                    MessageBox.Show("Please select a least one piece");
                    return;
                }

                var Department = (TLADM_Departments)cmboDepartment.SelectedItem;
                if (Department == null)
                {
                    MessageBox.Show("Please select a Cutting Department");
                    return;
                }

                
                using (var context = new TTI2Entities())
                {
                    var DOrder = (from DyeOrder in context.TLDYE_DyeOrder
                                  join DyeBatch in context.TLDYE_DyeBatch on DyeOrder.TLDYO_Pk equals DyeBatch.DYEB_DyeOrder_FK
                                  where DyeBatch.DYEB_Pk == BatchKey
                                  select DyeOrder).FirstOrDefault();

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("CUT")).FirstOrDefault();
                    
                    TLCUT_CutSheet cutSheet = new TLCUT_CutSheet();
                    if (PrevCSSelected)
                    {
                        selected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                        if (selected != null)
                        {
                            cutSheet = context.TLCUT_CutSheet.Find(selected.TLCutSH_Pk);
                            if (cutSheet == null)
                                cutSheet = new TLCUT_CutSheet();
                        }
                    }
                    
                    cutSheet.TLCutSH_Date = DateTime.Now;
                    cutSheet.TLCutSH_No = txtLastNumber.Text;
                    cutSheet.TLCutSH_DyeBatch_FK = BatchKey;
                    cutSheet.TLCutSH_Notes = rtbNotes.Text;
                    cutSheet.TLCutSH_Size_PN = 0;
                 
                    cutSheet.TLCutSH_Department_FK = Department.Dep_Id;
                    cutSheet.TLCUTSH_RequiredDate = dtpRequiredDate.Value;

                    //--------------------------------------------------
                    var DB = context.TLDYE_DyeBatch.Find(BatchKey);
                    if (DB != null)
                    {
                        cutSheet.TLCutSH_Quality_FK = DB.DYEB_Greige_FK;
                        cutSheet.TLCutSH_Customer_FK = DB.DYEB_Customer_FK;
                        cutSheet.TLCutSH_Colour_FK = DB.DYEB_Colour_FK;
                        cutSheet.TLCutSH_Styles_FK = DOrder.TLDYO_Style_FK;
                    }

                    if (ChkAccepted.Checked)
                        cutSheet.TLCutSH_Accepted = true;

                    //This takes care of the Size variable;
                    //-------------------------------------------------------
                    bool lFirst = true;
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            if (lFirst)
                            {
                                lFirst = !lFirst;
                                cutSheet.TLCutSH_Size_FK = (int)row.Cells[0].Value;
                            }
                            cutSheet.TLCutSH_Size_PN += (int)row.Cells[6].Value;
                        }
                    }

                    if (!PrevCSSelected)
                    {
                        context.TLCUT_CutSheet.Add(cutSheet);

                        var LNU = context.TLADM_LastNumberUsed.Find(4);
                        if (LNU != null)
                            LNU.col1 += 1;
                    }
                    else
                    {
                        var CutSheetSelected = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                        if (CutSheetSelected != null)
                        {
                            //------------------------------------------------------------------------------------
                            // First we must ensure that all records are removed from 
                            // the expectedUnits table for this particular Cut Sheet (if Applicables)
                            //---------------------------------------------------------------------------
                            context.TLCUT_ExpectedUnits.RemoveRange(context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CutSheetSelected.TLCutSH_Pk));
                        }
                    }
                    try
                    {
                        context.SaveChanges();
                                               
                        foreach (DataGridViewRow Rowx in dataGridView1.Rows)
                        {
                            if ((bool)Rowx.Cells[2].Value == false)
                                continue;

                            TLCUT_CutSheetDetail cutSheetDetail = new TLCUT_CutSheetDetail();
                            bool Add = true;
                            var Index = (int)Rowx.Cells[0].Value;
                            cutSheetDetail = context.TLCUT_CutSheetDetail.FirstOrDefault(x=>x.TLCutSHD_DyeBatchDet_FK == Index);
                   
                            if (cutSheetDetail == null)
                                 cutSheetDetail = new TLCUT_CutSheetDetail();
                            else
                                 Add = false;
                            
                            cutSheetDetail.TLCutSHD_CutSheet_FK = cutSheet.TLCutSH_Pk;
                            cutSheetDetail.TLCutSHD_DyeBatchDet_FK = (int)Rowx.Cells[0].Value;
                            cutSheetDetail.TLCUTSHD_NettWeight = (decimal)Rowx.Cells[4].Value;

                            if (Dept != null)
                            {
                                var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 100).FirstOrDefault();
                                if (TranType != null)
                                {
                                    cutSheetDetail.TLCutSHD_CurrentStore_FK = (int)TranType.TrxT_ToWhse_FK;
                                    cutSheetDetail.TLCutSHD_Transaction_Type = TranType.TrxT_Pk;
                                }
                            }

                            var DBD = context.TLDYE_DyeBatchDetails.Find((int)Rowx.Cells[0].Value);
                            if (DBD != null)
                            {
                                DBD.DYEBO_CutSheet = true;
                            }

                            if (Add)
                               context.TLCUT_CutSheetDetail.Add(cutSheetDetail);
                        }

                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            bool Add = true;
                            if (!String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                            {
                                TLCUT_ExpectedUnits eUnits = new TLCUT_ExpectedUnits();
                                if (PrevCSSelected && selected != null)
                                {
                                    var sz = (int)row.Cells[0].Value;
                                    eUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == selected.TLCutSH_Pk && x.TLCUTE_Size_FK == sz).FirstOrDefault();
                                    if (eUnits == null)
                                        eUnits = new TLCUT_ExpectedUnits();
                                    else
                                        Add = false;
                                }
                                eUnits.TLCUTE_CutSheet_FK = cutSheet.TLCutSH_Pk;
                                eUnits.TLCUTE_Size_FK = (int)row.Cells[0].Value;
                                eUnits.TLCUTE_MarkerRatio = Convert.ToDecimal(row.Cells[2].Value.ToString());
                                eUnits.TLCUTE_NoofGarments = (Int32)row.Cells[3].Value;
                                eUnits.TLCUTE_NoOfBinding = decimal.Parse(row.Cells[4].Value.ToString());
                                eUnits.TLCUTE_NoOfTrims = decimal.Parse(row.Cells[5].Value.ToString());
                                eUnits.TLCUTE_EstNettWeight = decimal.Parse(row.Cells[7].Value.ToString());
                                if(Add)
                                   context.TLCUT_ExpectedUnits.Add(eUnits);
                            }
                        }

                        context.SaveChanges();

                        if (ChkAccepted.Checked)
                        {
                            frmCutViewRep vRep = new frmCutViewRep(1, cutSheet.TLCutSH_Pk);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);

                            vRep = new frmCutViewRep(2, cutSheet.TLCutSH_Pk);
                            h = Screen.PrimaryScreen.WorkingArea.Height;
                            w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                        }

                        MessageBox.Show("Data saved successfully to database");
                        SetUp();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException en)
                    {
                        foreach (var eve in en.EntityValidationErrors)
                        {
                             MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                             foreach (var ve in eve.ValidationErrors)
                             {
                                MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                             }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                        
            }
        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded)
            {
                if (e.ColumnIndex == 2)
                {
                    DataTable dt = core.Cal(oDgv);

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            if(Convert.ToInt32(dr[0].ToString()) == (int)row.Cells[0].Value)
                            {
                                if(!String.IsNullOrEmpty(dr[3].ToString()))
                                    row.Cells[3].Value = Convert.ToDecimal(dr[3].ToString());

                                if (!String.IsNullOrEmpty(dr[4].ToString()))
                                    row.Cells[4].Value = Convert.ToDecimal(dr[4].ToString());
                                
                                if (!String.IsNullOrEmpty(dr[5].ToString()))
                                    row.Cells[5].Value = Convert.ToDecimal(dr[5].ToString());

                                break;
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded)
            {
                if (e.ColumnIndex == 4)
                {
                    string val = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue as string;
                    if (string.IsNullOrEmpty(val))
                    {
                        e.Cancel = true;
                    }
                    
                }
            }
        }

        private void cmboCutSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLCUT_CutSheet)oCmbo.SelectedItem;
                if (selected != null)
                {
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();

                    cmboDepartment.SelectedValue = selected.TLCutSH_Department_FK;

                    txtNoBinding.Text = "0";
                    txtNoGarments.Text = "0";
                    txtNoTrims.Text = "0";

                    GetDyeBatch(selected.TLCutSH_DyeBatch_FK, true);

                    using (var context = new TTI2Entities())
                    {
                        foreach (DataGridViewRow Row in dataGridView1.Rows)
                        {
                            int Pk = (int)Row.Cells[0].Value; 
                            var CSheetDetail = context.TLCUT_CutSheetDetail.Where(x=>x.TLCutSHD_CutSheet_FK == selected.TLCutSH_Pk && x.TLCutSHD_DyeBatchDet_FK == Pk).FirstOrDefault();
                            if (CSheetDetail != null)
                            {
                                dataGridView1.Rows[Row.Index].Cells[2].Value = true;

                                DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(9999, Pk);
                                dataGridView1_CellContentClick(dataGridView1, ee);
                            }
                        }
                    }
                    
                    dataGridView1.Refresh();
                    dataGridView2.Refresh();
                    txtLastNumber.Text = selected.TLCutSH_No;
                    PrevCSSelected = true;
               }
            }
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmboRating_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmboRatingTrims_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkBIF_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null && formloaded)
            {
                if (oChk.Checked)
                {

                }
            }
        }

       
   }
}

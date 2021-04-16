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

        DataTable UpperTable = null;
        DataTable LowerTable = null;

        Util core;
        UserDetails UDet = null ;

      
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
        BindingSource BindingSrc = null;        

        IList<TLADM_Sizes> Listsizes = null;
        IList<TLADM_Trims> Trimsizes = null;

        TLDYE_DyeBatch DyeBatchSelected = null;

        private Dictionary<int, int> store = new Dictionary<int, int>();
        const int initialValue = -1;

        bool PrevCSSelected;

        BindingList<KeyValuePair<int, decimal>> CurrentRatios;

               
        public frmCutSheet(UserDetails ud)
        {
            InitializeComponent();

            core = new Util();


            UDet = ud;
            
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

            groupBox4.Visible = false;
            groupBox5.Visible = false;

          
           
        }


        private void frmCutSheet_Load(object sender, EventArgs e)
        {
            formloaded = false;
            
            PrevCSSelected = false;

            ChkAccepted.Checked = false;


            groupBox5.Visible = false;
            cmboDownSize.Visible = false;
            chkDownSize.Visible = false;
            label22.Visible = false;

            CreateDataStuctures DStructures = new CreateDataStuctures();

            UpperTable = DStructures.CreateDataTAB1();

            LowerTable = DStructures.CreateDataTAB2();

            DataGridViewTextBoxColumn gc = new DataGridViewTextBoxColumn();     // 0  
            gc.ValueType = typeof(int);
            gc.DataPropertyName = LowerTable.Columns[0].ColumnName;
            gc.Visible = false;
            gc.ReadOnly = true;
            this.dataGridView2.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();     // 1
            gc.HeaderText = "Size";
            gc.ValueType = typeof(string);
            gc.DataPropertyName = LowerTable.Columns[1].ColumnName;
            gc.ReadOnly = true;
            this.dataGridView2.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();     // 2
            gc.HeaderText = "Ratio";
            gc.ValueType = typeof(decimal);
            gc.DataPropertyName = LowerTable.Columns[2].ColumnName;
            this.dataGridView2.Columns.Add(gc);
            // oTxtZC.ReadOnly = true;

            gc = new DataGridViewTextBoxColumn();     // 3
            gc.HeaderText = "Garments";
            gc.ValueType = typeof(int);
            gc.DataPropertyName = LowerTable.Columns[3].ColumnName;
            gc.ReadOnly = true;
            this.dataGridView2.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();      // 4
            gc.HeaderText = "Binding(Kg)";
            gc.ValueType = typeof(Decimal);
            gc.DataPropertyName = LowerTable.Columns[4].ColumnName;
            gc.ReadOnly = true;
            this.dataGridView2.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();      // 5
            gc.HeaderText = "Trims (Kg)";
            gc.ValueType = typeof(Decimal);
            gc.DataPropertyName = LowerTable.Columns[5].ColumnName;
            gc.ReadOnly = true;
            this.dataGridView2.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();     //  6
            gc.HeaderText = "Power";
            gc.Visible = false;
            gc.ValueType = typeof(int);
            gc.DataPropertyName = LowerTable.Columns[6].ColumnName;
            gc.ReadOnly = true;
            this.dataGridView2.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();     //  7
            gc.HeaderText = "Estimated Nett Kg";
            gc.Visible = false;
            gc.ValueType = typeof(decimal);
            gc.DataPropertyName = LowerTable.Columns[7].ColumnName;
            gc.ReadOnly = true;
            this.dataGridView2.Columns.Add(gc);

            gc = new DataGridViewTextBoxColumn();     //  8
            gc.HeaderText = "Active";
            gc.Visible = false;
            gc.ValueType = typeof(Boolean);
            gc.DataPropertyName = LowerTable.Columns[8].ColumnName;
            gc.ReadOnly = true;
            this.dataGridView2.Columns.Add(gc);

            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.AllowUserToOrderColumns = false;
            this.dataGridView2.AutoGenerateColumns = false;

            this.BindingSrc = new BindingSource();
            this.BindingSrc.DataSource = LowerTable;
            this.dataGridView2.DataSource = BindingSrc;

            var idx = -1;

            foreach (DataColumn col in LowerTable.Columns)
            {
                if (++idx == 0 || idx > 5)
                    dataGridView2.Columns[idx].Visible = false;
                else
                    dataGridView2.Columns[idx].HeaderText = col.Caption;

            }

            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AllowUserToOrderColumns = false;

            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.AllowUserToOrderColumns = false;
            this.dataGridView2.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView2_EditingControlShowing);
                  
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
                txtLastNumber.Text = "Pending";

                /*var LNU = context.TLADM_LastNumberUsed.Find(4);
                if (LNU != null)
                {
                    txtLastNumber.Text = "CS" + LNU.col1.ToString().PadLeft(5, '0');
                }*/

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

        void SetUp(bool Onload)
        {
            formloaded = false;
          

            foreach (DataRow row in LowerTable.Rows)
            {
                row.SetField("Ratio", 0.00M);
                row.SetField("Garments", 0);
                row.SetField("Binding", 0.00M);
                row.SetField("Trims", 0.00M);
               // row.SetField("Size_Pn", 0);
                row.SetField("Estimated_Weight", 0.00M);
                row.SetField("Active", false);
            }

            txtNoGarments.Text = "0";
            txtNoBinding.Text = "0.00";
            txtNoTrims.Text = "0.00";
            txtQtyKg.Text = "0.00";
            txtTrimKg.Text = "0.00";

            if (Onload)
            {
                label22.Visible = false;
                chkDownSize.Checked = false;
                chkDownSize.Visible = false;
                groupBox5.Visible   = false;


                dataGridView1.Rows.Clear();

                ChkAccepted.Checked = false;


                txtLastNumber.Text = "Pending";
                /*using (var context = new TTI2Entities())
                 {
                    var LNU = context.TLADM_LastNumberUsed.Find(4);
                    if (LNU != null)
                    {
                        txtLastNumber.Text = "CS" + LNU.col1.ToString().PadLeft(5, '0');
                    }
                 }*/


                cmboCutSheet.SelectedValue = -1;
            }
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
                    
                    foreach (var dbDetail in DbDetails)
                    {
                        // We may have situation where a dybatch is spli
                        if (dbDetail.DYEBO_CutSheet)
                        {
                            var tst = (from CSD in context.TLCUT_CutSheetDetail
                                       join DBD in context.TLDYE_DyeBatchDetails
                                       on CSD.TLCutSHD_DyeBatchDet_FK equals DBD.DYEBD_Pk
                                       where CSD.TLCutSHD_DyeBatchDet_FK == dbDetail.DYEBD_Pk 
                                       select CSD).FirstOrDefault();
                            if (tst == null)
                            {
                                continue;
                            }
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
                            dataGridView1.Rows[DetIndex].Cells[11].Value = GP.GreigeP_Remarks + " " + dbDetail.DYEBO_Notes;
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
            var index = 0;
            if (oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 using (var context = new TTI2Entities())
                 {
                     var CurrentRow = oDgv.CurrentRow;
                     var CurrentCell = oDgv.CurrentCell;
                     if (CurrentRow != null)
                     {
                         if (e.ColumnIndex != 9999)
                         {
                            index = (int)CurrentRow.Cells[0].Value;
                         }
                         else
                         {
                            index = e.RowIndex; 
                         }
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
                                 
                                 if (Yield <= 0)
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
                                    if(NettWeight <= 0)
                                    {
                                        MessageBox.Show("Warning Product Rating table may not have been set up correctly", "Error - Binding Weight", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return; 
                                    }
                                 }

                                 var ISTrim = (from stytrim in context.TLADM_StyleTrim
                                                  join trim in context.TLADM_Trims on stytrim.StyTrim_Trim_Fk equals trim.TR_Id
                                                  join prodrating in context.TLADM_ProductRating on stytrim.StyTrim_ProdRating_FK equals prodrating.Pr_Id
                                                  where stytrim.StyTrim_Styles_Fk == StyleFK && !trim.TR_IsBinding
                                                  select prodrating).FirstOrDefault();
                                 
                                 if (ISTrim != null)
                                 {
                                    TrimWeight = dbd.DYEBO_Nett * ISTrim.Pr_numeric_Rating;
                                    if(TrimWeight <= 0)
                                    {
                                        MessageBox.Show("Warning Product Rating table may not have been set up correctly", "Error - Trim Weight  ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
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
                                var Rating = 0.0M;
                                var RatingDetails = context.TLADM_ProductRating.Find(ProdFK);
                                if(RatingDetails == null)
                                {
                                    MessageBox.Show("No Rating Details found for Style selected");
                                    return;

                                }

                                Rating = RatingDetails.Pr_numeric_Rating;
                                Decimal TotalQtyKg = 0.00M;
                                Decimal.TryParse(txtQtyKg.Text, out TotalQtyKg);
                                // var TotalQtyKg = Decimal.Parse(txtQtyKg.Text);
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
                                     NoOfGarments +=  Math.Round(Yield / Rating * NettWeight);
                                     txtQtyKg.Text = (Math.Round(TotalQtyKg + NettWeight, 1)).ToString();
                                     txtNoGarments.Text = NoOfGarments.ToString();
                                 }
                                 
                                 var Factor = Math.Round(Yield / Rating * NettWeight, 0);
                                 var tst = core.CalculateRatios(ProdFK, (int)Factor);

                                decimal EstKg = 0.00M;
                                decimal BindKg = 0.00M;
                                decimal TrimKg = 0.00M;

                                foreach(var row in tst)
                                {
                                    var TableRow = LowerTable.Rows.Find(row.Key);

                                    if(TableRow != null)
                                    {
                                        EstKg = Math.Round(NettWeight * (row.Value / Factor), 2);
                                        BindKg = Math.Round(BindWeight * (row.Value / Factor), 2);
                                        TrimKg = Math.Round(TrimWeight * (row.Value / Factor), 2);

                                        
                                        if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                                        {
                                            TableRow[2] = RatingDetails.Pr_Ratio;
                                            TableRow[3] = TableRow.Field<Int32>(3) + row.Value;
                                            TableRow[4] = TableRow.Field<Decimal>(4) + BindKg;
                                            TableRow[5] = TableRow.Field<Decimal>(5) + TrimKg;
                                            TableRow[6] = TableRow.Field<Int32>(6);
                                            TableRow[7] = TableRow.Field<decimal>(7) + EstKg;
                                            TableRow[8] = true;
                                        }
                                        else
                                        {
                                            TableRow[3] = TableRow.Field<Int32>(3) - row.Value;
                                            TableRow[4] = TableRow.Field<Decimal>(4) - BindKg;
                                            TableRow[5] = TableRow.Field<Decimal>(5) - TrimKg;
                                            TableRow[7] = TableRow.Field<Decimal>(7) - EstKg;
                                            if(TableRow.Field<Int32>(3) == 0)
                                            {
                                                TableRow[8] = false;
                                            }
                                        }
                                    }
                                }

                                var sum = LowerTable.AsEnumerable()
                                         .Sum(r => r.Field<decimal>(4));
                                txtNoBinding.Text = sum.ToString();

                                sum = LowerTable.AsEnumerable()
                                         .Sum(r => r.Field<decimal>(5));
                                txtNoTrims.Text = sum.ToString();

                                sum = LowerTable.AsEnumerable()
                                         .Sum(r => r.Field<decimal>(7));
                                txtQtyKg.Text = sum.ToString();
                                
                               
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
                                        Decimal CurrentTotal = 0.00M;
                                        Decimal.TryParse(txtTrimKg.Text, out CurrentTotal);
                                        //var CurrentTotal = decimal.Parse(txtTrimKg.Text.ToString());
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
                    foreach (DataRow Row in LowerTable.Rows)
                    {
                        if(!Row.Field<Boolean>(8))
                        {
                            continue;     
                        }

                        if (lFirst)
                        {
                            lFirst = !lFirst;
                            cutSheet.TLCutSH_Size_FK = Row.Field<Int32>(0);
                        }

                        cutSheet.TLCutSH_Size_PN += Row.Field<Int32>(6);
                    
                    }

                    if (!PrevCSSelected)
                    {
                        cutSheet.TLCutSH_No = String.Empty;
                        var LNU = context.TLADM_LastNumberUsed.Find(4);
                        if (LNU != null)
                        {
                            cutSheet.TLCutSH_No = "CS" + LNU.col1.ToString().PadLeft(5, '0');
                            LNU.col1 += 1;
                        }
                        context.TLCUT_CutSheet.Add(cutSheet);

                     
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
                        
                        foreach (DataRow Row in LowerTable.Rows)
                        {
                            var lAdd = true;

                            if (!Row.Field<Boolean>(8))
                            {
                                continue;
                            }
                            var sz = Row.Field<Int32>(0);
                            TLCUT_ExpectedUnits eUnits = new TLCUT_ExpectedUnits();
                            if(PrevCSSelected)
                            {
                                eUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == selected.TLCutSH_Pk && x.TLCUTE_Size_FK == sz).FirstOrDefault();
                                if (eUnits != null)
                                {
                                    lAdd = false;
                                }
                                else
                                {
                                    eUnits = new TLCUT_ExpectedUnits();
                                }
                            }
                            eUnits.TLCUTE_CutSheet_FK = cutSheet.TLCutSH_Pk;
                            eUnits.TLCUTE_Size_FK = sz;
                            eUnits.TLCUTE_MarkerRatio = Row.Field<decimal>(2);
                            eUnits.TLCUTE_NoofGarments = Row.Field<Int32>(3);
                            eUnits.TLCUTE_NoOfBinding =  Row.Field<decimal>(4);
                            eUnits.TLCUTE_NoOfTrims = Row.Field<Decimal>(5);
                            eUnits.TLCUTE_EstNettWeight = Row.Field<decimal>(7);
                            if (lAdd)
                            {
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
                            if (vRep != null)
                            {
                                vRep.Close();
                                vRep.Dispose();
                            }
                            vRep = new frmCutViewRep(2, cutSheet.TLCutSH_Pk);
                            h = Screen.PrimaryScreen.WorkingArea.Height;
                            w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                            if (vRep != null)
                            {
                                vRep.Close();
                                vRep.Dispose();
                            }
                        }

                        MessageBox.Show("Data saved successfully to database");
                        SetUp(true);
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
                    LowerTable.Rows.Clear();
                    using (var context = new TTI2Entities())
                    {
                        var AvailSizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                        foreach (var Size in AvailSizes)
                        {
                            DataRow dr = LowerTable.NewRow();

                            dr[0] = Size.SI_id;
                            dr[1] = Size.SI_Description;
                            dr[2] = 0.00M;
                            dr[3] = 0; // row.Value.ToString();
                            dr[4] = 0.00M; // row.Value.ToString();
                            dr[5] = 0.00M; // row.Value.ToString();
                            dr[6] = Size.SI_PowerN; // row.Value.ToString();
                            dr[7] = 0.00M; // row.Value.ToString();
                            dr[8] = false;
                            LowerTable.Rows.Add(dr);
                        }

                       

                        groupBox5.Visible = false;
                        chkDownSize.Checked = false;

                        if(UDet._SuperUser || UDet._DownSizeAuthority)
                        {
                            chkDownSize.Visible = true;
                        }

                        cmboDepartment.SelectedValue = selected.TLCutSH_Department_FK;

                        txtNoBinding.Text = "0";
                        txtNoGarments.Text = "0";
                        txtNoTrims.Text = "0";

                        GetDyeBatch(selected.TLCutSH_DyeBatch_FK, true);

                    
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

        private void chkDownSize_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null && formloaded)
            {
                if (oChk.Checked)
                {
                    using (  var context = new TTI2Entities())
                    {
                        var CutSh = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                        if(CutSh != null)
                        {
                            var EU = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CutSh.TLCutSH_Pk).Count();
                            if(EU > 1)
                            {
                                MessageBox.Show("This CutSheet is a multi marker and therefore this option is not available");
                                return;
                            }

                            formloaded = false;
                            var Sizes = context.TLADM_Sizes.Where(x => !x.SI_Discontinued && x.SI_id != CutSh.TLCutSH_Size_FK).OrderBy(x=>x.SI_DisplayOrder).ToList();
                            
                            cmboDownSize.DataSource = Sizes;
                            cmboDownSize.DisplayMember =  "SI_Description";
                            cmboDownSize.ValueMember = "SI_Id";
                            formloaded = true;

                            groupBox5.Visible = true;
                            label22.Visible = true;
                            cmboDownSize.Visible = true;
                            cmboDownSize.Enabled = true;
                        }
                    }
                }
                else
                {
                    label22.Visible = false;
                    cmboDownSize.Visible = false; 
                }
            }
        }

        private void cmboDownSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            var OriginalSize_Fk = 0;

            TLADM_ProductRating MarkerRating = null;

            if(oCmbo != null && formloaded)
            {
                var CSheet = (TLCUT_CutSheet)cmboCutSheet.SelectedItem;
                var Size = (TLADM_Sizes)oCmbo.SelectedItem;

                using ( var context = new TTI2Entities())
                {
                    MarkerRating = context.TLADM_ProductRating.Where(x => x.Pr_Style_FK == CSheet.TLCutSH_Styles_FK && x.Pr_PowerN  == Size.SI_PowerN).FirstOrDefault();
                    if(MarkerRating == null)
                    {
                        MessageBox.Show("There is no valid Product rating for this Style and Size");
                        return;
                    }

                    SetUp(false);
                    var CS = context.TLCUT_CutSheet.Find(CSheet.TLCutSH_Pk);
                    if(CS != null)
                    {
                        OriginalSize_Fk = CSheet.TLCutSH_Size_FK;

                        CSheet.TLCutSH_Size_FK = Size.SI_id;
                        CSheet.TLCutSH_Size_PN = Size.SI_PowerN;

                        CS.TLCutSH_Size_FK = Size.SI_id;
                        CS.TLCutSH_Size_PN = Size.SI_PowerN; 
                    }

                    foreach(DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if(!(bool)Row.Cells[2].Value)
                        {
                            continue;
                        }

                        var Pk = (int)Row.Cells[0].Value;

                        var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Find(Pk);

                        if(DyeBatchDetail != null)
                        {
                            DyeBatchDetail.DYEBO_ProductRating_FK = MarkerRating.Pr_Id;
                            context.SaveChanges();

                            DataGridViewCellEventArgs ee = new DataGridViewCellEventArgs(9999, Pk);
                            dataGridView1_CellContentClick(dataGridView1, ee);

                        }
                    }

                    context.SaveChanges();
                }
            }
        }

        private void cmboLabels_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

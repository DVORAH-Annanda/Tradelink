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

namespace TTI2_WF
{
    public partial class frmTLADMCustomerTypes : Form
    {
        int option;
        bool formloaded;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;

        DataGridViewComboBoxColumn oCmbBoxA;
        DataGridViewComboBoxColumn oCmbBoxB;
        DataGridViewComboBoxColumn oCmbBoxC;
       
        DataGridViewCheckBoxColumn oChkA;

        Util core;

        public frmTLADMCustomerTypes(int OptionType)
        {
            InitializeComponent();
           
            Setup(OptionType);
            core = new Util();
        }

        void Setup(int opt)
        {
            dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            option = opt;

            formloaded = false;
            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            oTxtBoxA.HeaderText = "Primary Key";

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxF = new DataGridViewTextBoxColumn();
            
            oCmbBoxA = new DataGridViewComboBoxColumn();
            oCmbBoxB = new DataGridViewComboBoxColumn();
            oCmbBoxC = new DataGridViewComboBoxColumn();

            oChkA = new DataGridViewCheckBoxColumn();

            groupBox1.Visible = false;
            groupBox2.Visible = false;


            label1.Enabled = false;
            label1.Visible = false;
            cmboMeasurement.Visible = false;
            cmboMeasurement.Enabled = false;

            if (opt == 1)
            {
                oTxtBoxB.HeaderText = "Customer Type Short code";
                oTxtBoxC.HeaderText = "Customer Type Description";
            }
            else if (opt == 2)
            {
                oTxtBoxB.HeaderText = "Product Group  Short code";
                oTxtBoxC.HeaderText = "Stock Group Description";
            }
            else if (opt == 3)
            {
                oTxtBoxB.HeaderText = "Store Type Short code";
                oTxtBoxC.HeaderText = "Store Type Description";
            }
            else if (opt == 4)
            {
                oTxtBoxB.HeaderText = "NS Item category Short code";
                oTxtBoxC.HeaderText = "NS Item Description";
            }
            else if (opt == 5)
            {
                oTxtBoxB.HeaderText = "Machine Maintenance Short Code";
                oTxtBoxC.HeaderText = "Machine Maintenance Description";
                oTxtBoxD.HeaderText = "Maintenance Interval";
                oTxtBoxE.HeaderText = "Machine Planned Downtime";
                oCmbBoxA.HeaderText = "UOM";
            }
            else if (opt == 6)
            {
                oTxtBoxB.HeaderText = "Cotton origin Short Code";
                oTxtBoxC.HeaderText = "Cotton origin Description";
            }
            else if (opt == 7)
            {
                oTxtBoxB.HeaderText = "UOM Short Code";
                oTxtBoxC.HeaderText = "UOM Description";
            }
            else if (opt == 8)
            {
                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxC.HeaderText = "Description";
                oChkA.HeaderText = "NCR Form Required";
            }
            else if (opt == 9)
            {
                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxC.HeaderText = "Description";
                oChkA.HeaderText = "Additional Resources";
            }
            else if (opt == 10)
            {
                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxC.HeaderText = "Description";
            }
            else if (opt == 11)
            {
                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxC.HeaderText = "Description";
            }
            else if (opt == 12)
            {
                oTxtBoxB.HeaderText = "Size";
                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLADM_CutAreaLocations.Where(x=>!x.TLCUTAL_PPS).OrderBy(x => x.TLCUTAL_Pk).ToList();
                    foreach (var Row in Existing)
                    {
                        if (Row.TLCUTAL_Pk == 1)
                            oTxtBoxB.HeaderText = Row.TLCUTAL_Description;
                        else if (Row.TLCUTAL_Pk == 2)
                            oTxtBoxC.HeaderText = Row.TLCUTAL_Description;
                        else if (Row.TLCUTAL_Pk == 3)
                            oTxtBoxD.HeaderText = Row.TLCUTAL_Description;
                        else
                            oTxtBoxE.HeaderText = Row.TLCUTAL_Description;
                        
                      
                    }
                    oCmbBoxB.HeaderText = "Size";
                }
            }
            else if (opt == 13)
            {
                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxC.HeaderText = "Description";
            }
            else if (opt == 14 || opt == 15)
            {
                oTxtBoxB.HeaderText = "Short Code";
            }
            else if (opt == 16)
            {

                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxC.HeaderText = "Description";

            }
            else if (opt == 17)
            {
                oTxtBoxD.HeaderText = "Display Order ";
                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxC.HeaderText = "Description";
                oChkA.HeaderText = "B2M Raw Panels";
            }
            else if (opt == 18)
            {
                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxC.HeaderText = "Description";
                oChkA.HeaderText = "Manufacturing";

            }
            else if (opt == 20)
            {
                oChkA.HeaderText = "PPS";
                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxC.HeaderText = "Description";
           
            }
            else if (opt == 21)
            {
                groupBox2.Visible = true;
               
                label1.Enabled = true;
                label1.Visible = true;
                label1.Text = "Current CMT's";

                cmboMeasurement.Visible = true;
                cmboMeasurement.Enabled = true;

                dataGridView1.Columns.Add(oTxtBoxA);
                oCmbBoxA.HeaderText = "Style";
                dataGridView1.Columns.Add(oCmbBoxA);
                oCmbBoxB.HeaderText = "Colour";
                dataGridView1.Columns.Add(oCmbBoxB);
                oCmbBoxC.HeaderText = "Size";
                dataGridView1.Columns.Add(oCmbBoxC);

                oTxtBoxB.HeaderText = "Production Charges";
                oTxtBoxC.HeaderText = "Production Damage";
                oTxtBoxD.HeaderText = "Production Shrinkage";
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns.Add(oTxtBoxD);

            }
            else if (opt == 22)
            {
                dataGridView1.Columns.Add(oTxtBoxA);
                oCmbBoxA.HeaderText = "Primary WareHouse";
                dataGridView1.Columns.Add(oCmbBoxA);
                oCmbBoxB.HeaderText = "Associated B Grade";
                dataGridView1.Columns.Add(oCmbBoxB);

            }
            else if (opt == 23)
            {
                dataGridView1.Columns.Add(oTxtBoxA);
                oTxtBoxB.HeaderText = "Short Code";
                dataGridView1.Columns.Add(oTxtBoxB);
                oTxtBoxC.HeaderText = "Description";
                dataGridView1.Columns.Add(oTxtBoxC);

            }
            else if (opt == 24)
            {
                dataGridView1.Columns.Add(oTxtBoxA);
                oCmbBoxA.HeaderText = "Box Type";
                dataGridView1.Columns.Add(oCmbBoxA);
                oCmbBoxB.HeaderText = "Style";
                dataGridView1.Columns.Add(oCmbBoxB);
                oCmbBoxC.HeaderText = "Size";
                dataGridView1.Columns.Add(oCmbBoxC);
                oTxtBoxC.HeaderText = "Qty";
                oTxtBoxC.ValueType = typeof(System.Int32); 
                dataGridView1.Columns.Add(oTxtBoxC);
            }

            if (opt != 12 && opt != 21 && opt != 22  && opt != 23 && opt != 24 )
            {
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns.Add(oTxtBoxB);
            }

            if(opt != 12 && opt != 14 && opt != 15 && opt != 19 && opt != 21 && opt!= 22 && opt!= 23 && opt!=24)
               dataGridView1.Columns.Add(oTxtBoxC);

          

            if (opt == 8 || opt == 9)
            {
                dataGridView1.Columns.Add(oChkA);
            }
            else if (opt == 12)
            {
                dataGridView1.Columns.Add(oTxtBoxA);
                oTxtBoxA.ValueType = typeof(int);
                dataGridView1.Columns.Add(oCmbBoxB);
                oTxtBoxB.ValueType = typeof(decimal);
                dataGridView1.Columns.Add(oTxtBoxB);
                oTxtBoxC.ValueType = typeof(decimal);
                dataGridView1.Columns.Add(oTxtBoxC);
                oTxtBoxD.ValueType = typeof(decimal);
                dataGridView1.Columns.Add(oTxtBoxD);
                oTxtBoxE.ValueType = typeof(decimal);
                dataGridView1.Columns.Add(oTxtBoxE);

                groupBox1.Visible = true;
                radPPS.Checked = true;

            }
            else if (opt == 14 || opt == 15)
            {
                dataGridView1.Columns.Add(oCmbBoxA);
            }
            else if (opt == 17)
            {
                dataGridView1.Columns.Add(oChkA);
                dataGridView1.Columns.Add(oTxtBoxD);
            }
            else if (opt == 18)
            {
                dataGridView1.Columns.Add(oChkA);
            }

            using (var context = new TTI2Entities())
            {
                if (opt == 1)
                {
                    this.Text = "Customer Types Update / Edit Facility";
                    var existingData = context.TLADM_CustomerTypes
                                       .OrderBy(x => x.CT_Id).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.CT_Id;
                        dataGridView1.Rows[index].Cells[1].Value = rw.CT_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.CT_Description;
                    }

                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSizeColumnsMode =
                            DataGridViewAutoSizeColumnsMode.AllCells;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;

                }
                else if (opt == 2)
                {


                    this.Text = "Product Group Update / Edit Facility";
                    var existingData = context.TLADM_StockTypes
                                      .OrderBy(x => x.ST_Id).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.ST_Id;
                        dataGridView1.Rows[index].Cells[1].Value = rw.ST_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.ST_Description;
                    }

                    dataGridView1.AutoResizeColumns();
                    dataGridView1.AutoSizeColumnsMode =
                            DataGridViewAutoSizeColumnsMode.AllCells;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (opt == 3)
                {
                    this.Text = "Store Types Update / Edit Facility";
                    var existingData = context.TLADM_StoreTypes
                                      .OrderBy(x => x.StoreT_Id).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.StoreT_Id;
                        dataGridView1.Rows[index].Cells[1].Value = rw.StoreT_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.StoreT_Description;
                    }

                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[2].Width = 250;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (opt == 4)
                {


                    this.Text = "Non Stock Items Category Update / Edit Facility";
                    var existingData = context.TLADM_NonStockCat
                                      .OrderBy(x => x.NSC_Pk).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.NSC_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.NSC_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.NSC_Description;
                    }

                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[2].Width = 250;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (opt == 5)
                {
                    /*
                    dataGridView1.Columns.Add(oTxtBoxD);
                    dataGridView1.Columns.Add(oTxtBoxE);
                    dataGridView1.Columns.Add(oTxtBoxF);

                    oCmbBoxA.DataSource = context.TLADM_UOM
                                     .OrderBy(x => x.UOM_ShortCode).ToList();
                    oCmbBoxA.ValueMember = "UOM_Pk";
                    oCmbBoxA.DisplayMember = "UOM_Description";

                    dataGridView1.Columns.Add(oCmbBoxA);

                    dataGridView1.Columns[5].Visible = false;


                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 5;
                    ((DataGridViewTextBoxColumn)dataGridView1.Columns[2]).MaxInputLength = 50;
                    Util core = new Util();
                    this.Text = "Machine Maintenance Update / Edit Facility";

                    var existingData = context.TLADM_MachineMaintenance
                                     .OrderBy(x => x.Maint_ShortCode).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.Maint_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.Maint_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.Maint_Description;
                        dataGridView1.Rows[index].Cells[3].Value = rw.Maint_IntervalDownTime;
                        dataGridView1.Rows[index].Cells[4].Value = rw.Maint_DownTimePeriod;
                        dataGridView1.Rows[index].Cells[5].Value = rw.Maint_PowerN;
                        dataGridView1.Rows[index].Cells[6].Value = rw.Maint_UOM_FK;
                    }
                    */
                }
                else if (opt == 6)
                {
                    Util core = new Util();
                    this.Text = "Cotton Origin Update / Edit Facility";

                    var existingData = context.TLADM_CottonOrigin
                                     .OrderBy(x => x.CottonOrigin_ShortCode).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.CottonOrigin_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.CottonOrigin_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.CottonOrigin_Description;

                    }

                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;

                }

                else if (opt == 7)
                {

                    Util core = new Util();
                    this.Text = "Units of Measure Update / Edit Facility";

                    var existingData = context.TLADM_UOM
                                     .OrderBy(x => x.UOM_ShortCode).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.UOM_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.UOM_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.UOM_Description;

                    }

                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;

                }
                else if (opt == 8)
                {

                    Util core = new Util();
                    this.Text = "Dye House Quality Deficiency Codes";

                    var existingData = context.TLADM_DyeQDCodes
                                     .OrderBy(x => x.QDF_Pk).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.QDF_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.QDF_Code;
                        dataGridView1.Rows[index].Cells[2].Value = rw.QDF_Description;
                        dataGridView1.Rows[index].Cells[3].Value = rw.QDF_NCRRequired;
                    }

                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;

                }
                else if (opt == 9)
                {

                    Util core = new Util();
                    this.Text = "Dye House Remedies Update / Edit Facility";

                    var existingData = context.TLADM_DyeRemendyCodes
                                     .OrderBy(x => x.QRC_Pk).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.QRC_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.QRC_Code;
                        dataGridView1.Rows[index].Cells[2].Value = rw.QRC_Description;
                        dataGridView1.Rows[index].Cells[3].Value = rw.QRC_AdditionalResources;
                    }


                }
                else if (opt == 10)
                {
                    Util core = new Util();
                    this.Text = "Box type Update / Edit Facility";

                    var existingData = context.TLADM_BoxTypes
                                     .OrderBy(x => x.TLADMBT_ShortCode).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLADMBT_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLADMBT_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLADMBT_Description;

                    }
                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (opt == 11)
                {

                    Util core = new Util();
                    this.Text = "Cutting Department measurement Areas Update / Edit Facility";

                    var existingData = context.TLADM_CutMeasureArea
                                     .OrderBy(x => x.TLCUTA_Pk).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLCUTA_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLCUTA_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLCUTA_Description;

                    }
                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;

                }
                else if (opt == 12)
                {
                    formloaded = false;
                    groupBox2.Visible = true;

                    cmboMeasurement.DataSource = context.TLADM_Styles
                                     .OrderBy(x => x.Sty_Description).ToList();
                    cmboMeasurement.ValueMember = "Sty_Id";
                    cmboMeasurement.DisplayMember = "Sty_Description";
                    cmboMeasurement.SelectedValue = -1;

                    oCmbBoxB.DataSource = context.TLADM_Sizes.OrderBy(z => z.SI_DisplayOrder).ToList();
                    oCmbBoxB.ValueMember = "SI_Id";
                    oCmbBoxB.DisplayMember = "SI_Description";

                    cmboMeasurement.Visible = true;
                    cmboMeasurement.Enabled = true;
                    
                    cmboCmtLines.Visible = false;
                    cmboCmtLines.Enabled = false;


                    label1.Text = "Available Styles";
               
                    label1.Visible = true;
                    label1.Enabled = true;

                    label2.Visible = false;
                    cmboCmtLines.Visible = false;

                    Util core = new Util();
                    this.Text = "Cutting Department Measurement Standards Update / Edit Facility";


                }
                else if (opt == 13)
                {

                    Util core = new Util();
                    this.Text = "Cutting Department Trims recorded on the Cut / Edit Facility";

                    var existingData = context.TLADM_CutTrims
                                     .OrderBy(x => x.TLCUTTOC_ShortCode).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLCUTTOC_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLCUTTOC_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLCUTTOC_Description;

                    }
                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;

                }
                else if (opt == 14)
                {
                    Util core = new Util();
                    this.Text = "Cutting Department Fleece Cuffs / Edit Facility";

                    oCmbBoxA.DataSource = context.TLADM_Sizes.ToList();
                    oCmbBoxA.ValueMember = "SI_Id";
                    oCmbBoxA.DisplayMember = "SI_Description";

                    var existingData = context.TLADM_CutFleeceCuffs
                                     .OrderBy(x => x.TLADMFC_shortCode).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLADMFC_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLADMFC_shortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLADMFC_Size_FK;
                    }
                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (opt == 15)
                {
                    Util core = new Util();
                    this.Text = "Cutting Department Fleece Waistbands Update / Edit Facility";

                    oCmbBoxA.DataSource = context.TLADM_Sizes.ToList();
                    oCmbBoxA.ValueMember = "SI_Id";
                    oCmbBoxA.DisplayMember = "SI_Description";

                    var existingData = context.TLADM_CutFleeceWaist
                                     .OrderBy(x => x.TLADMFW_ShortCode).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLADMFW_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLADMFW_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLADMFW_Size_FK;
                    }
                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (opt == 16)
                {
                    //  ((DataGridViewTextBoxColumn)dataGridView1.Columns[1]).MaxInputLength = 5;
                    //  ((DataGridViewTextBoxColumn)dataGridView1.Columns[2]).MaxInputLength = 50;
                    Util core = new Util();
                    this.Text = "Standard Product Update / Edit Facility";

                    var existingData = context.TLADM_StandardProduct
                                     .OrderBy(x => x.TLADMSP_Pk).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLADMSP_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLADMSP_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLADMSP_Description;

                    }

                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;


                }
                else if (opt == 17)
                {

                    Util core = new Util();
                    this.Text = "Bulk Final Audit Measurement Points Update / Edit Facility";

                    var existingData = context.TLADM_CMTMeasurementPoints
                                     .OrderBy(x => x.CMTMP_DisplayOrder).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.CMTMP_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.CMTMP_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.CMTMP_Description;
                        dataGridView1.Rows[index].Cells[3].Value = rw.CMTMP_B2MRawPanels;
                        dataGridView1.Rows[index].Cells[4].Value = rw.CMTMP_DisplayOrder;
                    }
                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (opt == 18)
                {

                    Util core = new Util();
                    this.Text = "Garment defect flaws Update / Edit Facility";

                    var existingData = context.TLCMT_DeflectFlaw
                                     .OrderBy(x => x.TLCMTDF_ShortCode).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLCMTDF_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLCMTDF_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLCMTDF_Description;
                        dataGridView1.Rows[index].Cells[3].Value = rw.TLCMTDF_Manufacturing;

                    }
                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;

                }
                else if (opt == 19)
                {
                    Util core = new Util();
                    this.Text = "Dye House Production Stages ";

                    var existingData = context.TLADM_QADyeProcess
                                     .OrderBy(x => x.QADYEP_Pk).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.QADYEP_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.QADYEP_Description;
                    }

                    dataGridView1.Columns[1].Width = 250;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 10;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (opt == 20)
                {
                    Util core = new Util();
                    this.Text = "Cut Area Locations";

                    dataGridView1.Columns.Add(oChkA);

                    var existingData = context.TLADM_CutAreaLocations
                                     .OrderBy(x => x.TLCUTAL_Pk).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLCUTAL_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLCUTAL_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLCUTAL_Description;
                        dataGridView1.Rows[index].Cells[3].Value = rw.TLCUTAL_PPS;

                    }


                }
                else if (opt == 21)
                {
                    Util core = new Util();
                    this.Text = "CMT Charges (Expressed in South African Rand)";

                    cmboMeasurement.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                    cmboMeasurement.DisplayMember = "Dep_Description";
                    cmboMeasurement.ValueMember = "Dep_Id";
                    cmboMeasurement.SelectedIndex = -1;
                                        
                    oCmbBoxA.DataSource = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                    oCmbBoxA.ValueMember = "Sty_Id";
                    oCmbBoxA.DisplayMember = "Sty_Description";

                    oCmbBoxB.DataSource = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                    oCmbBoxB.ValueMember = "Col_Id";
                    oCmbBoxB.DisplayMember = "Col_Display";

                    oCmbBoxC.DataSource = context.TLADM_Sizes.OrderBy(x => x.SI_DisplayOrder).ToList();
                    oCmbBoxC.ValueMember = "SI_Id";
                    oCmbBoxC.DisplayMember = "SI_Description";
                 
                }
                else if (opt == 22)
                {
                    Util core = new Util();
                    this.Text = "Warehouse Associations";

                    oCmbBoxA.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_GradeA && x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Description).ToList();
                    oCmbBoxA.ValueMember = "WhStore_Id";
                    oCmbBoxA.DisplayMember = "WhStore_Description";

                    oCmbBoxB.DataSource = context.TLADM_WhseStore.Where(x => !x.WhStore_GradeA && x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Id).ToList();
                    oCmbBoxB.ValueMember = "WhStore_Id";
                    oCmbBoxB.DisplayMember = "WhStore_Description";

                    var existingData = context.TLADM_WareHouseAssociation.OrderBy(x => x.TLWA_PrimaryWareHouse).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLWA_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLWA_PrimaryWareHouse;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLWA_SecondaryWareHouse;
                    }
                }
                else if (opt == 23)
                {
                    Util core = new Util();
                    this.Text = "CMT Cut Non Compliance Facility";

                    var existingData = context.TLADM_CMTNonCompliance
                                 .OrderBy(x => x.CMTNC_Pk).ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.CMTNC_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.CMTNC_ShortCode;
                        dataGridView1.Rows[index].Cells[2].Value = rw.CMTNC_Description;
                    }

                    dataGridView1.Columns[2].Width = 150;
                    int width = 0;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        width += col.Width;

                    }

                    dataGridView1.Width = width + 40;

                    dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
                    dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
                }
                else if (opt == 24)
                {
                    Util core = new Util();
                    this.Text = "VicBay Packaging Specifications)";

                    oCmbBoxA.DataSource = context.TLADM_BoxTypes.OrderBy(x => x.TLADMBT_Description).ToList();
                    oCmbBoxA.ValueMember = "TLADMBT_Pk";
                    oCmbBoxA.DisplayMember = "TLADMBT_Description";

                    oCmbBoxB.DataSource = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                    oCmbBoxB.ValueMember = "Sty_Id";
                    oCmbBoxB.DisplayMember = "Sty_Description";

                    oCmbBoxC.DataSource = context.TLADM_Sizes.OrderBy(x => x.SI_DisplayOrder).ToList();
                    oCmbBoxC.ValueMember = "SI_Id";
                    oCmbBoxC.DisplayMember = "SI_Description";

                    var existingData = context.TLADM_BoxType_Packing_Specifications.ToList();

                    foreach (var rw in existingData)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = rw.TLBPS_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = rw.TLBPS_BoxType_Fk;
                        dataGridView1.Rows[index].Cells[2].Value = rw.TLBPS_Style_Fk;
                        dataGridView1.Rows[index].Cells[3].Value = rw.TLBPS_Size_Fk;
                        dataGridView1.Rows[index].Cells[4].Value = rw.TLBPS_Quantity;
                    }
                }

            }
            formloaded = true;
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;
            var Cell = oDgv.CurrentCell;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    if (option == 12 && Cell.ColumnIndex == 3)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (option == 17)
                    {
                        if (Cell.ColumnIndex == 4) 
                        {
                            e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                            e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                            e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                            e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                        }

                    }
                    else if (option == 21)
                    {
                        if (Cell.ColumnIndex == 4 ||
                            Cell.ColumnIndex == 5 ||
                            Cell.ColumnIndex == 6)
                        {
                            e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                            e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                            e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                            e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                        }
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
               }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool Add = false;
            bool success = true;
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                using ( var context = new TTI2Entities())
                {
                    if (option == 1)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_CustomerTypes ct = new TLADM_CustomerTypes();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                ct = context.TLADM_CustomerTypes.Find(index);
                            }

                            ct.CT_ShortCode = row.Cells[1].Value.ToString();
                            ct.CT_Description = row.Cells[2].Value.ToString();

                            if (Add)
                                context.TLADM_CustomerTypes.Add(ct);
                        }

                        try
                        {
                            context.SaveChanges();
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
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }

                    }
                    else if (option == 2)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_StockTypes ct = new TLADM_StockTypes();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                ct = context.TLADM_StockTypes.Find(index);
                            }

                            ct.ST_ShortCode = row.Cells[1].Value.ToString();
                            ct.ST_Description = row.Cells[2].Value.ToString();

                            if (Add)
                                context.TLADM_StockTypes.Add(ct);
                         
                        }

                        try
                        {
                            context.SaveChanges();
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
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }

                    }
                    else if (option == 3)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_StoreTypes ct = new TLADM_StoreTypes();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                ct = context.TLADM_StoreTypes.Find(index);
                            }

                            ct.StoreT_ShortCode = row.Cells[1].Value.ToString();
                            ct.StoreT_Description = row.Cells[2].Value.ToString();

                            if (Add)
                                context.TLADM_StoreTypes.Add(ct);
                         
                        }
                        try
                        {
                            context.SaveChanges();
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
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 4)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_NonStockCat ct = new TLADM_NonStockCat();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                ct = context.TLADM_NonStockCat.Find(index);
                            }

                            ct.NSC_ShortCode = row.Cells[1].Value.ToString();
                            ct.NSC_Description = row.Cells[2].Value.ToString();

                            if (Add)
                                context.TLADM_NonStockCat.Add(ct);
                          
                      }
                      try
                      {
                            context.SaveChanges();
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

                      catch (Exception ex)
                      {
                            MessageBox.Show(ex.Message);
                            success = false;
                      }
    
                    }
                    else if (option == 5)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            /*
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_MachineMaintenance ct = new TLADM_MachineMaintenance();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                ct = context.TLADM_MachineMaintenance.Find(index);
                            }

                            ct.Maint_ShortCode = row.Cells[1].Value.ToString();
                            ct.Maint_Description = row.Cells[2].Value.ToString();

                            if (row.Cells[3].Value != null)
                            {
                                ct.Maint_IntervalDownTime = Convert.ToInt32(row.Cells[3].Value.ToString());
                            }
                            else
                                ct.Maint_IntervalDownTime = 0;

                            if (row.Cells[4].Value != null)
                            {
                                ct.Maint_DownTimePeriod = Convert.ToInt32(row.Cells[4].Value.ToString());
                            }
                            else
                                ct.Maint_DownTimePeriod = 0;

                            if (row.Cells[5].Value == null && Add)
                            {
                                ct.Maint_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                            }
                            else
                                ct.Maint_PowerN = Convert.ToInt32(row.Cells[5].Value.ToString());


                            if (row.Cells[6].Value != null)
                               ct.Maint_UOM_FK = Convert.ToInt32(row.Cells[6].Value.ToString());
                            else
                                ct.Maint_UOM_FK = 1;

                            if (Add)
                                context.TLADM_MachineMaintenance.Add(ct);

                            */
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }

                    }
                    else if (option == 6)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_CottonOrigin ct = new TLADM_CottonOrigin();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                ct = context.TLADM_CottonOrigin.Find(index);
                            }

                            ct.CottonOrigin_ShortCode = row.Cells[1].Value.ToString();
                            ct.CottonOrigin_Description = row.Cells[2].Value.ToString();

                           

                            if (Add)
                                context.TLADM_CottonOrigin.Add(ct);

                            
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }

                    }
                    else if (option == 7)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_UOM ct = new TLADM_UOM();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                ct = context.TLADM_UOM.Find(index);
                            }

                            ct.UOM_ShortCode = row.Cells[1].Value.ToString();
                            ct.UOM_Description = row.Cells[2].Value.ToString();

                            if (Add)
                                context.TLADM_UOM.Add(ct);

                           
                        }
                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 8)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_DyeQDCodes ct = new TLADM_DyeQDCodes();
                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                ct = context.TLADM_DyeQDCodes.Find(index);
                            }

                            ct.QDF_Code = row.Cells[1].Value.ToString();
                            ct.QDF_Description = row.Cells[2].Value.ToString();

                            if (row.Cells[3].Value.ToString() == bool.TrueString)
                                ct.QDF_NCRRequired = true;
                            else
                                ct.QDF_NCRRequired = false;

                            if (Add)
                                context.TLADM_DyeQDCodes.Add(ct);

                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }

                    }
                    else if (option == 9)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_DyeRemendyCodes ct = new TLADM_DyeRemendyCodes();
                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                ct = context.TLADM_DyeRemendyCodes.Find(index);
                            }

                            ct.QRC_Code = row.Cells[1].Value.ToString();
                            ct.QRC_Description = row.Cells[2].Value.ToString();

                            if (row.Cells[3].Value.ToString() == bool.TrueString)
                                ct.QRC_AdditionalResources = true;
                            else
                                ct.QRC_AdditionalResources = false;


                            if (Add)
                                context.TLADM_DyeRemendyCodes.Add(ct);
                           
                        }
                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 10)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_BoxTypes bt = new TLADM_BoxTypes();
                            
                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLADM_BoxTypes.Find(index);
                            }

                            bt.TLADMBT_ShortCode = row.Cells[1].Value.ToString();
                            bt.TLADMBT_Description = row.Cells[2].Value.ToString();

                            if (Add)
                                context.TLADM_BoxTypes.Add(bt);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 11)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_CutMeasureArea bt = new TLADM_CutMeasureArea();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLADM_CutMeasureArea.Find(index);
                            }

                            bt.TLCUTA_ShortCode = row.Cells[1].Value.ToString();
                            bt.TLCUTA_Description = row.Cells[2].Value.ToString();

                            if (Add)
                                context.TLADM_CutMeasureArea.Add(bt);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 12)
                    {
                        var selected = (TLADM_Styles)cmboMeasurement.SelectedItem;
                        if (selected != null)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Cells[1].Value == null)
                                    continue;

                                Add = true;
                            
                                TLADM_CutMeasureStandards bt = new TLADM_CutMeasureStandards();

                                if (row.Cells[0].Value != null)
                                {
                                    Add = !Add;
                                    var index = row.Cells[0].Value;
                                    bt = context.TLADM_CutMeasureStandards.Find(index);
                                }

                                bt.TLCUTAS_Style_FK = selected.Sty_Id;
                                bt.TLCUTAS_Size_FK = (int)row.Cells[1].Value;
                                bt.TLCUTAS_Col1 = (decimal)row.Cells[2].Value;
                                bt.TLCUTAS_Col2 = (decimal)row.Cells[3].Value;
                                bt.TLCUTAS_Col3 = (decimal)row.Cells[4].Value;
                                bt.TLCUTAS_Col4 = (decimal)row.Cells[5].Value;
                                bt.TLCUTAS_Standard = 0.00M;
                                if(radPPS.Checked)
                                   bt.TLCUTAS_PPS = true;
                                
                                if (Add)
                                    context.TLADM_CutMeasureStandards.Add(bt);
                            }

                            try
                            {
                                context.SaveChanges();
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

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                success = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select a valid area");
                            return;
                        }
                    }
                    else if (option == 13)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_CutTrims Ct = new TLADM_CutTrims();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                Ct = context.TLADM_CutTrims.Find(index);
                            }

                            Ct.TLCUTTOC_ShortCode = row.Cells[1].Value.ToString();
                            Ct.TLCUTTOC_Description = row.Cells[2].Value.ToString();

                            if (Add)
                                context.TLADM_CutTrims.Add(Ct);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 14)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[2].Value == null)
                                continue;

                            Add = true;

                            TLADM_CutFleeceCuffs Ct = new TLADM_CutFleeceCuffs();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                Ct = context.TLADM_CutFleeceCuffs.Find(index);
                            }
                            
                            if(row.Cells[1].Value != null)
                                 Ct.TLADMFC_shortCode = row.Cells[1].Value.ToString();
                            Ct.TLADMFC_Size_FK = (int)row.Cells[2].Value;

                            if (Add)
                                context.TLADM_CutFleeceCuffs.Add(Ct);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 15)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[2].Value == null)
                                continue;

                            Add = true;

                            TLADM_CutFleeceWaist Ct = new TLADM_CutFleeceWaist();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                Ct = context.TLADM_CutFleeceWaist.Find(index);
                            }

                            if (row.Cells[1].Value != null)
                                Ct.TLADMFW_ShortCode = row.Cells[1].Value.ToString();

                            Ct.TLADMFW_Size_FK = (int)row.Cells[2].Value;

                            if (Add)
                                context.TLADM_CutFleeceWaist.Add(Ct);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }

                    }
                    else if (option == 16)
                    {
                       
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_StandardProduct bt = new TLADM_StandardProduct();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLADM_StandardProduct.Find(index);
                            }

                            bt.TLADMSP_ShortCode = row.Cells[1].Value.ToString();
                            bt.TLADMSP_Description = row.Cells[2].Value.ToString();

                            if (Add)
                                context.TLADM_StandardProduct.Add(bt);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                         
                    }
                    else if (option == 17)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_CMTMeasurementPoints bt = new TLADM_CMTMeasurementPoints();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLADM_CMTMeasurementPoints.Find(index);
                            }

                            bt.CMTMP_ShortCode = row.Cells[1].Value.ToString();
                            bt.CMTMP_Description = row.Cells[2].Value.ToString();
                            if ((bool)row.Cells[3].Value == true)
                                bt.CMTMP_B2MRawPanels = true;
                            bt.CMTMP_DisplayOrder = Convert.ToInt32(row.Cells[4].Value.ToString());

                            if (Add)
                                context.TLADM_CMTMeasurementPoints.Add(bt);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 18)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLCMT_DeflectFlaw bt = new TLCMT_DeflectFlaw();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLCMT_DeflectFlaw.Find(index);
                            }

                            bt.TLCMTDF_ShortCode     = row.Cells[1].Value.ToString();
                            bt.TLCMTDF_Description   = row.Cells[2].Value.ToString();
                            bt.TLCMTDF_Manufacturing = (bool)row.Cells[3].Value;

                            if (Add)
                                context.TLCMT_DeflectFlaw.Add(bt);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 19)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_QADyeProcess bt = new TLADM_QADyeProcess();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLADM_QADyeProcess.Find(index);
                            }

                            bt.QADYEP_Description = row.Cells[1].Value.ToString();
                   
                            if (Add)
                                context.TLADM_QADyeProcess.Add(bt);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 20)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            Add = true;

                            TLADM_CutAreaLocations bt = new TLADM_CutAreaLocations();

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLADM_CutAreaLocations.Find(index);
                            }

                            bt.TLCUTAL_ShortCode = row.Cells[1].Value.ToString();
                            bt.TLCUTAL_Description = row.Cells[2].Value.ToString();
                            bt.TLCUTAL_PPS = (bool)row.Cells[3].Value;
                            if (Add)
                                context.TLADM_CutAreaLocations.Add(bt);
                        }

                        try
                        {
                            context.SaveChanges();
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

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 21)
                    {
                        var Departments = (TLADM_Departments)cmboMeasurement.SelectedItem;
                        if(Departments == null)
                        {
                            MessageBox.Show("Please select a CMT to process");
                            return;
                        }
                        var Selectedline = (TLCMT_FactConfig)cmboCmtLines.SelectedItem;
                        if(Selectedline == null)
                        {
                            MessageBox.Show("Please select a CMT Line to process");
                            return;
                        }

                        var CMT_Id = Departments.Dep_Id;
                        var CMT_Line = Selectedline.TLCMTCFG_Pk;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {

                            if (row.Cells[1].Value == null)
                                continue;

                            TLCMT_ProductionCosts bt = new TLCMT_ProductionCosts();
                            Add = true;
                            

                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLCMT_ProductionCosts.Find(index);
                            }

                            if (row.Cells[1].Value != null)
                            {
                               

                                bt.CMTP_Style_FK = (int)row.Cells[1].Value;
                                bt.CMTP_CMTLineNo_FK = CMT_Line;

                                if (row.Cells[2].Value != null)
                                   bt.CMTP_Colour_FK = (int)row.Cells[2].Value;
                                else
                                    bt.CMTP_Colour_FK = null;
                                
                                if (row.Cells[3].Value != null)
                                    bt.CMTP_Size_FK = (int)row.Cells[3].Value;
                                else
                                    bt.CMTP_Size_FK = null;

                                if (row.Cells[4].Value != null)
                                    bt.CMTP_Production_Cost = Convert.ToDecimal(row.Cells[4].Value.ToString());
                                else
                                    bt.CMTP_Production_Cost = 0.00M;

                                if (row.Cells[5].Value != null)
                                    bt.CMTP_Production_Damage = Convert.ToDecimal(row.Cells[5].Value.ToString());
                                else
                                    bt.CMTP_Production_Damage = 0.00M;

                                if (row.Cells[6].Value != null)
                                   bt.CMTP_Production_Loss = Convert.ToDecimal(row.Cells[6].Value.ToString());
                                else
                                   bt.CMTP_Production_Loss = 0.00M;


                                if (Add)
                                {
                                    bt.CMTP_CMTFacility_FK = CMT_Id;
                                    context.TLCMT_ProductionCosts.Add(bt);
                                }
                            }
                        }

                        try
                        {
                             context.SaveChanges();
                             success = true;  
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
                            success = false;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 22)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {

                            if (row.Cells[1].Value == null)
                                continue;

                            TLADM_WareHouseAssociation bt = new TLADM_WareHouseAssociation();
                            Add = true;


                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLADM_WareHouseAssociation.Find(index);
                            }

                            if (row.Cells[1].Value != null)
                            {
                                bt.TLWA_PrimaryWareHouse = (int)row.Cells[1].Value;

                                if (row.Cells[2].Value != null)
                                    bt.TLWA_SecondaryWareHouse = (int)row.Cells[2].Value;
                                else
                                    bt.TLWA_SecondaryWareHouse = null;

                                if (Add)
                                    context.TLADM_WareHouseAssociation.Add(bt);
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                            success = true;
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
                            success = false;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 23)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            TLADM_CMTNonCompliance bt = new TLADM_CMTNonCompliance();
                            Add = true;


                            if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLADM_CMTNonCompliance.Find(index);
                            }

                            bt.CMTNC_ShortCode = row.Cells[1].Value.ToString();
                            bt.CMTNC_Description = row.Cells[2].Value.ToString();

                            if(Add)
                                context.TLADM_CMTNonCompliance.Add(bt);
                       }
                      

                        try
                        {
                            context.SaveChanges();
                            success = true;
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
                            success = false;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    else if (option == 24)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value == null)
                                continue;

                            TLADM_BoxType_Packing_Specifications  bt = new TLADM_BoxType_Packing_Specifications();
                            Add = true;


                                if (row.Cells[0].Value != null)
                            {
                                Add = !Add;
                                var index = row.Cells[0].Value;
                                bt = context.TLADM_BoxType_Packing_Specifications.Find(index);
                            }

                            bt.TLBPS_BoxType_Fk = (int)row.Cells[1].Value;

                            bt.TLBPS_Style_Fk = (int)row.Cells[2].Value;
                            bt.TLBPS_Size_Fk = (int)row.Cells[3].Value;
                            bt.TLBPS_Quantity = (int)row.Cells[4].Value;

                            if (Add)
                                context.TLADM_BoxType_Packing_Specifications.Add(bt);
                        }


                        try
                        {
                            context.SaveChanges();
                            success = true;
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
                            success = false;
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            success = false;
                        }
                    }
                    if (success)
                    {
                        MessageBox.Show("Record / s successfully saved to database");
                        this.Close();
                    }
                }
            }
        }

        private void cmboMeasurement_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            IList<TLADM_CutMeasureStandards> Existing = null;

            if (oCmbo != null && formloaded)
            {
                if (option != 21)
                {
                    var selected = (TLADM_Styles)oCmbo.SelectedItem;
                    if (selected != null)
                    {
                        using (var context = new TTI2Entities())
                        {
                            dataGridView1.Rows.Clear();
                            if (radPPS.Checked)
                            {
                                Existing = context.TLADM_CutMeasureStandards.Where(x => x.TLCUTAS_Style_FK == selected.Sty_Id && x.TLCUTAS_PPS).ToList();
                            }
                            else
                            {
                                Existing = context.TLADM_CutMeasureStandards.Where(x => x.TLCUTAS_Style_FK == selected.Sty_Id && !x.TLCUTAS_PPS).ToList();
                            }

                            Existing = (from Exist in Existing
                                        join SZ in context.TLADM_Sizes on Exist.TLCUTAS_Size_FK equals SZ.SI_id
                                        orderby SZ.SI_DisplayOrder
                                        select Exist).ToList();

                            foreach (var Record in Existing)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = Record.TLCUTAS_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = Record.TLCUTAS_Size_FK;
                                dataGridView1.Rows[index].Cells[2].Value = Record.TLCUTAS_Col1;    //Col 1
                                dataGridView1.Rows[index].Cells[3].Value = Record.TLCUTAS_Col2;       //Col 2 
                                dataGridView1.Rows[index].Cells[4].Value = Record.TLCUTAS_Col3;        //Col 3
                                dataGridView1.Rows[index].Cells[5].Value = Record.TLCUTAS_Col4;
                            }
                        }

                    }
                }
                else
                {
                    var selected = (TLADM_Departments)oCmbo.SelectedItem;
                    if (selected != null)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var FactConFig = context.TLCMT_FactConfig.Where(x => x.TLCMTCFG_Department_FK == selected.Dep_Id).ToList();

                            cmboCmtLines.DataSource = null;
                            cmboCmtLines.Items.Clear();

                            formloaded = false;
                            cmboCmtLines.DataSource = FactConFig;
                            cmboCmtLines.ValueMember = "TLCMTCFG_Pk";
                            cmboCmtLines.DisplayMember = "TLCMTCFG_Description";
                            cmboCmtLines.SelectedValue = -1;
                            formloaded = true;
                            
                            /*
                            dataGridView1.Rows.Clear();

                            var existingData = context.TLCMT_ProductionCosts.Where(x=>x.CMTP_CMTFacility_FK == selected.Dep_Id).ToList();

                            foreach (var rw in existingData)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = rw.CMTP_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = rw.CMTP_Style_FK;
                                dataGridView1.Rows[index].Cells[2].Value = rw.CMTP_Colour_FK;
                                dataGridView1.Rows[index].Cells[3].Value = rw.CMTP_Size_FK;
                                dataGridView1.Rows[index].Cells[4].Value = rw.CMTP_Production_Cost;
                                dataGridView1.Rows[index].Cells[5].Value = rw.CMTP_Production_Damage;
                                dataGridView1.Rows[index].Cells[6].Value = rw.CMTP_Production_Loss;
                            }
                            */

                        }
                     
                    }
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.Button.ToString() == "Right")
            {
                if (oDgv.SelectedRows.Count > 0)
                {
                     DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                     if (res == DialogResult.OK)
                     {
                         DataGridViewRow cr = oDgv.CurrentRow;

                         int RecNo = Convert.ToInt32(cr.Cells[0].Value.ToString());

                         using (var context = new TTI2Entities())
                         {
                                 if (option == 1)
                                  {
                                      var Record = context.TLADM_CustomerTypes.Find(RecNo);
                                     if(Record != null)
                                     {
                                         context.TLADM_CustomerTypes.Remove(Record);
                                         try
                                         {
                                             context.SaveChanges();
                                             oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                         }
                                         catch (Exception ex)
                                         {
                                             MessageBox.Show(ex.Message);
                                         }
                                     }
                                  }
                                  else if (option == 2)
                                  {
                                      var Record = context.TLADM_StockTypes.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_StockTypes.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                         
                                      }
                                  }
                                  else if (option == 3)
                                  {
                                      var Record = context.TLADM_StoreTypes.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_StoreTypes.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }
                                  }
                                  else if (option == 4)
                                  {
                                      var Record = context.TLADM_NonStockCat.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_NonStockCat.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }
                                  }
                                  else if (option == 5)
                                  {
                                      oCmbBoxA.DataSource = context.TLADM_UOM
                                                       .OrderBy(x => x.UOM_ShortCode).ToList();

                                      var Record = context.TLADM_UOM.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_UOM.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }
                                  }
                                  else if (option == 6)
                                  {
                                     var Record = context.TLADM_CottonOrigin.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_CottonOrigin.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }
                                  }
                                  else if (option == 7)
                                  {
                                      var Record = context.TLADM_UOM.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_UOM.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }
                                  }
                                  else if (option == 8)
                                  {
                                      var Record = context.TLADM_DyeQDCodes.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_DyeQDCodes.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }
                                  }
                                  else if (option == 9)
                                  {
                                      var Record = context.TLADM_DyeRemendyCodes.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_DyeRemendyCodes.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }
                                  }
                                  else if (option == 10)
                                  {
                                      var Record = context.TLADM_BoxTypes.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_BoxTypes.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                      }
                                  }
                                  else if (option == 11)
                                  {
                                      var Record = context.TLADM_CutMeasureArea.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_CutMeasureArea.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }
                                  }
                                  else if (option == 12)
                                  {
                                      var Record = context.TLADM_CutMeasureArea.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_CutMeasureArea.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }
                                  }
                                  else if (option == 13)
                                  {
                                      var Record = context.TLADM_CutTrims.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_CutTrims.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }

                                  }
                                  else if (option == 14)
                                  {
                                      var Record = context.TLADM_CutFleeceCuffs.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_CutFleeceCuffs.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }

                                  }
                                  else if (option == 15)
                                  {
                                      var Record = context.TLADM_CutFleeceWaist.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_CutFleeceWaist.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                         
                                      }
                                  }
                                  else if (option == 16)
                                  {
                                      var Record = context.TLADM_StandardProduct.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_StandardProduct.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }


                                  }
                                  else if (option == 17)
                                  {
                                      var Record = context.TLADM_CMTMeasurementPoints.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_CMTMeasurementPoints.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }

                                  }
                                  else if (option == 18)
                                  {
                                      var Record = context.TLCMT_DeflectFlaw.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLCMT_DeflectFlaw.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                          
                                      }

                                  }
                                  else if (option == 19)
                                  {
                                      var Record = context.TLADM_QADyeProcess.Find(RecNo);
                                      if (Record != null)
                                      {
                                          context.TLADM_QADyeProcess.Remove(Record);
                                          try
                                          {
                                              context.SaveChanges();
                                              oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                          }
                                          catch (Exception ex)
                                          {
                                              MessageBox.Show(ex.Message);
                                          }
                                      }
                                  }
                                 else if (option == 21)
                                 {
                                     var Record = context.TLCMT_ProductionCosts.Find(RecNo);
                                     if (Record != null)
                                     {
                                         context.TLCMT_ProductionCosts.Remove(Record);
                                         try
                                         {
                                             context.SaveChanges();
                                             oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                         }
                                         catch (Exception ex)
                                         {
                                             MessageBox.Show(ex.Message);
                                         }
                                     }
                                 }
                            }
                     }
                }
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }

        private void radPPS_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null)
            {
                using (var context = new TTI2Entities())
                {
                    if (oRad.Checked)
                    {
                        dataGridView1.Rows.Clear();

                        var CutAreas = context.TLADM_CutAreaLocations.Where(x => x.TLCUTAL_PPS).OrderBy(x => x.TLCUTAL_Pk).ToList();
                        foreach (var Row in CutAreas)
                        {
                            if (Row.TLCUTAL_Pk == 5)
                                oTxtBoxB.HeaderText = Row.TLCUTAL_Description;
                            else if (Row.TLCUTAL_Pk == 6)
                                oTxtBoxC.HeaderText = Row.TLCUTAL_Description;
                            else if (Row.TLCUTAL_Pk == 7)
                                oTxtBoxD.HeaderText = Row.TLCUTAL_Description;
                            else
                                oTxtBoxE.HeaderText = Row.TLCUTAL_Description;


                        }
                    }
                }
            }
        }

        private void radPanel_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null)
            {
                using (var context = new TTI2Entities())
                {
                    if (oRad.Checked)
                    {
                        dataGridView1.Rows.Clear();

                        var CutAreas = context.TLADM_CutAreaLocations.Where(x => !x.TLCUTAL_PPS).OrderBy(x => x.TLCUTAL_Pk).ToList();
                        foreach (var Row in CutAreas)
                        {
                            if (Row.TLCUTAL_Pk == 1)
                                oTxtBoxB.HeaderText = Row.TLCUTAL_Description;
                            else if (Row.TLCUTAL_Pk == 2)
                                oTxtBoxC.HeaderText = Row.TLCUTAL_Description;
                            else if (Row.TLCUTAL_Pk == 3)
                                oTxtBoxD.HeaderText = Row.TLCUTAL_Description;
                            else
                                oTxtBoxE.HeaderText = Row.TLCUTAL_Description;


                        }
                    }
                }
            }

        }

        private void cmboCmtLines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if(oCmbo != null && formloaded)
            {
                
                var SelectedDept = (TLADM_Departments)cmboMeasurement.SelectedItem;
                if (SelectedDept != null)
                {
                    var SelectedLine = (TLCMT_FactConfig)oCmbo.SelectedItem;
                    if (SelectedLine != null)
                    {
                        
                        dataGridView1.Rows.Clear();
                        using (var context = new TTI2Entities())
                        {
                              var existingData = context.TLCMT_ProductionCosts.Where(x => x.CMTP_CMTFacility_FK == SelectedDept.Dep_Id && x.CMTP_CMTLineNo_FK == SelectedLine.TLCMTCFG_Pk).ToList();

                              foreach (var rw in existingData)
                              {
                                    var index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells[0].Value = rw.CMTP_Pk;
                                    dataGridView1.Rows[index].Cells[1].Value = rw.CMTP_Style_FK;
                                    dataGridView1.Rows[index].Cells[2].Value = rw.CMTP_Colour_FK;
                                    dataGridView1.Rows[index].Cells[3].Value = rw.CMTP_Size_FK;
                                    dataGridView1.Rows[index].Cells[4].Value = rw.CMTP_Production_Cost;
                                    dataGridView1.Rows[index].Cells[5].Value = rw.CMTP_Production_Damage;
                                    dataGridView1.Rows[index].Cells[6].Value = rw.CMTP_Production_Loss;
                               }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a CMT ");
                    return;
                }

            }
        }
    }
}

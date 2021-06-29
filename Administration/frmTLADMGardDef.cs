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
    public partial class frmTLADMGardDef : Form
    {
       
        DateTimePicker dtpObject;
        /******************************************************************/
        /// <summary>
        /// Text Box Objects for general use in the program
        /// </summary>
        DataGridViewTextBoxColumn oTxtBoxA;  // 1 Reserved for Description 
        DataGridViewTextBoxColumn oTxtBoxB;  // 3 Reserved For Disconcontinued Date 
        DataGridViewTextBoxColumn oTxtBoxC;  // 4 Reserved For Power Numbers
        //-------------------------------------------------------------------------------------------
        DataGridViewTextBoxColumn oTxtBoxD;  // 5
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;
        DataGridViewTextBoxColumn oTxtBoxG;
        DataGridViewTextBoxColumn oTxtBoxH;
        DataGridViewTextBoxColumn oTxtBoxJ;
        DataGridViewTextBoxColumn oTxtBoxK;
        DataGridViewTextBoxColumn oTxtBoxL;
        DataGridViewTextBoxColumn oTxtBoxM;
        DataGridViewTextBoxColumn oTxtBoxQ;
        DataGridViewTextBoxColumn oTxtBoxR;
        DataGridViewTextBoxColumn oTxtBoxS;
        DataGridViewTextBoxColumn oTxtBoxT;
        DataGridViewTextBoxColumn oTxtBoxU;
        /******************************************************************/
        /// <summary>
        /// Check Box objects for general use in the program 
        /// </summary>
        DataGridViewCheckBoxColumn oChkBoxA; //  2 Resevered for Discountinued Y/N 
        //--------------------------------------------------------------------------------------------
        DataGridViewCheckBoxColumn oChkBoxB;
        DataGridViewCheckBoxColumn oChkBoxC;
        DataGridViewCheckBoxColumn oChkBoxD;
        DataGridViewCheckBoxColumn oChkBoxE;
        //DataGridViewCheckBoxColumn oChkBoxF;
        //DataGridViewCheckBoxColumn oChkBoxG;
        /********************************************************************/
        /// <summary>
        /// Combo box objects for general use 
        /// </summary>
        DataGridViewComboBoxColumn oCmbBoxA;
        DataGridViewComboBoxColumn oCmbBoxB;
        DataGridViewComboBoxColumn oCmbBoxC;
        DataGridViewComboBoxColumn oCmbBoxD;
        DataGridViewComboBoxColumn oCmbBoxE;
       //DataGridViewComboBoxColumn oCmbBoxF;
        DataGridViewComboBoxColumn oCmbBoxG;
        DataGridViewComboBoxColumn oCmbBoxH;
        DataGridViewComboBoxColumn oCmbBoxJ;
        DataGridViewComboBoxColumn oCmbBoxK;
        
        //==========================================================================
        Administration.CheckComboBox oCmbChkBoxA;
        /*********************************************************************/
        /// <summary>
        /// Buttomn objects for general use in the program    
        /// </summary>
        DataGridViewButtonColumn oBtnA;
        DataGridViewButtonColumn oBtnB;
        DataGridViewButtonColumn oBtnC;
        DataGridViewButtonColumn oBtnD;
        DataGridViewButtonColumn oBtnE;
        /**********************************************************************/
        DataTable dataT = new DataTable();
        
        //-----------------------------------------------------------------------------------------------------------------
        // Used by the following modules        Yarn(3)           Greige(4)           Fabric(16)             Panels(17)
        //-------------------------------------------------------------------------------------------------------------------
        // DataGridViewTextBoxColumn  selectl;   // Tex Count        ROL in UOM
        // DataGridViewTextBoxColumn  selectm;   // Twist            ROQ in UOM 
        // DataGridViewComboBoxColumn selectn;   // Cotton Origin    Fabric Weight        Brand
        // DataGridViewComboBoxColumn selecto;   // Yarn Supplier    Fabric Width         Fabric Type
        // DataGridViewComboBoxColumn selectp;   // Product Type     Machine Number       Product Group
        // DataGridViewComboBoxColumn selectq;   // Product Group    Yarn Number          Colour 
        // DataGridViewCheckBoxColumn selectr;   // Blocked          Barcode              Blocked 
        // DataGridViewComboBoxColumn selects;   // UOM              UOM                  Prefered Supplier 
        // DataGridViewCheckBoxColumn selectt;   // Std Cost Y/N     Show Qty             Barcode
        // DataGridViewTextBoxColumn  selectu;   // Std Cost Unit
        // DataGridViewTextBoxColumn  selectv;   // ROL in UOM 
        // DataGridViewTextBoxColumn  selectw;   // EOQ in UOM 
        // DataGridViewCheckBoxColumn selectx;   // Show Qty (y/n)   Show Qty             Show Qty
        // DataGridViewComboBoxColumn selecty;   //                  Stock Take Category  Product Group
        // DataGridViewComboBoxColumn selectz;   //                  Griege Quality       UOM 
        // DataGridViewComboBoxColumn selecta1;  //                  Product Type         Greige Type 
        //--------------------------------------------------------------------------------------------------------------
        // Warning messages for Yarn 
        //--------------------------------------------------------------------------------------------------------------
        string[][] MandatoryFields;
        bool[] MandSelected;
        Util core;
        //-----------------------------------------------------------------------------------------
        int frmNumber = 0;
        bool allowDisc = false;
        bool allowDateSelection = false;
        bool formLoaded = false;
        bool LabelSelected = false;
        int ActiveRow;
        int CoNumSelected;
        //-----------------------------------------------------------------------------------------------
        
        public frmTLADMGardDef(int frmNum)
        {
            InitializeComponent();
            frmNumber = frmNum;
            Setup(frmNumber);
            
        }

        public frmTLADMGardDef(int frmNum, int CoNum)
        {
            InitializeComponent();
            frmNumber = frmNum;
            CoNumSelected = CoNum;
            Setup(frmNumber);
        }

        private void Setup(int fn)
        {
            
            core = new Util();
            if (fn == 1)   // Styles 
            {
                MandatoryFields = new string[][]
                {   new string[] {"0", "Please enter a style Description", "0"},
                    new string[] {"5", "Please select the appropriate sizes", "1"},
                    new string[] {"10", "Please enter a Pastel code for this style", "2"},
                    new string[] {"11", "Please enter a Pastel quality code for this style", "3"},
                    new string[] {"12", "Please enter a cotton cone factor", "4"},
                    new string[] {"13", "Please enter the number of bags required", "5"},
                    new string[] {"14", "Please enter the buttons per garment", "6"}
                };
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            }
            else if (fn == 3) // Yarn 
            {
                MandatoryFields = new string[][]
                {   new string[] {"0", "Please enter a Yarn Description", "0"},
                    new string[] {"5", "Please enter a Yarn Type", "1"},
                    new string[] {"6", "Please enter a tex count", "2"}, 
                    new string[] {"7", "Please enter a twist", "3"},
                    new string[] {"8", "Please enter a cotton origin", "4" },
                    new string[] {"9", "Please enter a yarn supplier", "5"},
                    new string[] {"10", "Please enter a product type", "6"}, 
                    new string[] {"12", "Please enter a UOM", "7"},
                    new string[] {"14", "Please enter a standard cost per unit", "8"},
                    new string[] {"15", "Please enter a ROL in UOM", "9"},
                    new string[] {"16", "Please enter a ROQ in UOM", "10"},
                    new string[] {"17", "Please enter the average pallet weight", "11"}
                };
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            }
            else if(fn == 4) // Greige 
            {
                MandatoryFields = new string[][]
                {   new string[] {"0", "Please enter a Greige Description", "0"},
                    new string[] {"5", "Please enter a Greige Quality", "1"},
                    new string[] {"6", "Please enter a Yarn type", "2"}, 
                    new string[] {"7", "Please enter the fabric weight", "3"},
                    new string[] {"8", "Please enter the fabric width", "4" },
                    new string[] {"9", "Please enter the machine number", "5"},
                    new string[] {"10", "Please enter a product type", "6"}, 
                    new string[] {"12", "Please enter a UOM", "7"},
                    new string[] {"13", "Please enter a ROL in UOM", "8"},
                    new string[] {"14", "Please enter a ROQ in UOM", "9"},
                    new string[] {"16", "Please enter a stock take frequency", "10"}
                };
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            }
            else if (fn == 16)  // Fabric type 
            {
                /*
                MandatoryFields = new string[][]
                {   new string[] {"0", "Please enter a Fabric Description", "0"},
                    new string[] {"5", "Please select a Greige Type", "1"},
                    new string[] {"6", "Please select a product type", "3"}, 
                    new string[] {"7", "Please select a Colour", "4"}, 
                    new string[] {"8", "Please select a product group", "5"},
                    new string[] {"9", "Please select a UOM", "6"},
                    new string[] {"12", "Please select a prefered supplier", "7"}
                };
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
                 * */
            }
            else if (fn == 17) // Panels
            {
                MandatoryFields = new string[][]
                {   new string[] {"0", "Please enter a Panel Description", "0"},
                    new string[] {"5", "Please enter a grade", "1"},
                    new string[] {"6", "Please select a size", "2"}, 
                    new string[] {"8", "Please select a UOM", "3"}, 
                    new string[] {"9", "Please select a prefered supplier", "4"},
                    new string[] {"11", "Please select a fabric", "5"}
                };
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            }

            formLoaded = false;
            LabelSelected = false;
            int pn = 0;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToOrderColumns = false;

            dtpObject         = new DateTimePicker();
            dtpObject.Format  = DateTimePickerFormat.Short;
            dtpObject.Visible = false;
            dtpObject.Width   = 100;
            dataGridView1.Controls.Add(dtpObject);

            dtpObject.ValueChanged           += this.dtpObject_valueChanged;
            this.dataGridView1.CellBeginEdit += this.dataGridView1_CellBeginEdit;
            this.dataGridView1.CellEndEdit   += this.dataGridView1_CellEndEdit;
            this.dataGridView1.RowsAdded     += new DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

            oTxtBoxA = new DataGridViewTextBoxColumn(); //0

            cmbLabels.Visible = false;
            label1.Visible = false;

            if (frmNumber == 1)    // Id No 1000  Styles Selection
            {
                using (var context = new TTI2Entities())
                {
                   try
                   {
                        cmbLabels.DataSource    = context.TLADM_CustomerFile.OrderBy(x=>x.Cust_Description).ToList();
                        cmbLabels.ValueMember   = "Cust_Pk";
                        cmbLabels.DisplayMember = "Cust_Description";
                        cmbLabels.SelectedIndex = 0;
                        pn = Convert.ToInt32(cmbLabels.SelectedValue.ToString());
                            LabelSelected = true;
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message);
                       this.Close();
                   }
                }
                cmbLabels.Visible = true;
                label1.Visible = true;

                oTxtBoxA.HeaderText = "Style Description";
                this.Text = "Styles Description Update / Edit Facility";
            }
            else if (frmNumber == 2) //ID 2000
            {
                oTxtBoxA.HeaderText = "Colours Description";
                this.Text = "Colours Description Update / Edit Facility";
            }
            else if (frmNumber == 3) //ID 3000
            {
                oTxtBoxA.HeaderText = "Yarn Description";
                this.Text = "Yarn Description Update / Edit Facility";
            }
            else if (frmNumber == 4) //ID 4000
            {
                oTxtBoxA.HeaderText = "Greige Description";
                this.Text = "Greige Description Update / Edit Facility";
            }
            else if (frmNumber == 5) // ID 5000
            {
                oTxtBoxA.HeaderText = "Fabric Width Description";
                this.Text = "Fabric Width Description Update / Edit Facility";
            }
            else if (frmNumber == 6) // ID 6000
            {
                oTxtBoxA.HeaderText = "Aux Colours";
                this.Text = "Aux Colours Description Update / Edit Facility";
            }
            else if (frmNumber == 7) // ID 7000
            {
                oTxtBoxA.HeaderText = "Trims Description";
                this.Text = "Trims Description Update / Edit Facility";
            }
            else if (frmNumber == 8) // ID 8000
            {
                oTxtBoxA.HeaderText = "Sizes Description";
                this.Text = "Sizes Description Update / Edit Facility";
            }
            else if (frmNumber == 9) // ID 9000
            {
                oTxtBoxA.HeaderText = "Labels";
                this.Text = "Labels Update / Edit Facility";
            }
            else if (frmNumber == 10) // ID 10000
            {
                oTxtBoxA.HeaderText = "Product Ratings";
                this.Text = "Product Rating Update / Edit Facility";
            }
            else if (frmNumber == 11) // ID 11000
            {
                oTxtBoxA.HeaderText = "Garment Definition";
                this.Text = "Garment Definition Update / Edit Facility";
            }
            else if (frmNumber == 12) // ID 12000
            {
                oTxtBoxA.HeaderText = "Fabric Product Definition";
                this.Text = "Fabric Product Update / Edit Facility";
            }
            else if (frmNumber == 13) // ID 13000
            {
                oTxtBoxA.HeaderText = "Fabric Weight Definition";
                this.Text = "Fabric Weight Update / Edit Facility";
            }
            else if (frmNumber == 14) //    
            {
                oTxtBoxA.HeaderText = "Greige Quality Definition";
                this.Text = "Greige Quality Update / Edit Facility";
            }
            else if (frmNumber == 15) //    
            {
                oTxtBoxA.HeaderText = "Units Of Measure";
                this.Text = "Units of Measure Update / Edit Facility";
            }
            else if (frmNumber == 16) //    
            {
                oTxtBoxA.HeaderText = "Fabric Description";
                this.Text = "Fabric Attributes Update / Edit Facility";
            }
            else if (frmNumber == 17) //    Panel Definition
            {
                oTxtBoxA.HeaderText = "Panel Description";
                this.Text = "Panel Attributes Update / Edit Facility";
            }

            else if (frmNumber == 18) //    Departments Production Loss
            {
                oTxtBoxA.HeaderText = "Departments Description";
                this.Text = "Production Loss Update / Edit Facility";
            }

            else if (frmNumber == 19) //    Departments Production Loss
            {
                oTxtBoxA.HeaderText = "Styles Description";
                this.Text = "Pastel Style Grades Definition";
            }
            //------------------------------------------------------------------------------------
            // General for Everybody 
            //-----------------------------------------------------------------------------------------------
         
            if (frmNumber != 18 && frmNumber != 19) // form option 18 (Production Loss) / 19 Grade Codes does not require this facility 
            {
                dataGridView1.Columns.Add(oTxtBoxA);     //0

                oChkBoxA = new DataGridViewCheckBoxColumn();      // 1
                oChkBoxA.HeaderText = "Discontinued?";
                dataGridView1.Columns.Add(oChkBoxA);

                oTxtBoxB = new DataGridViewTextBoxColumn();       // 2
                oTxtBoxB.HeaderText = "Discontinued Date";
                dataGridView1.Columns.Add(oTxtBoxB);
            }

            oTxtBoxC = new DataGridViewTextBoxColumn();           // 3
            oTxtBoxC.HeaderText = "Primary Key";
            oTxtBoxC.Visible = false;
            dataGridView1.Columns.Add(oTxtBoxC);

            oTxtBoxD = new DataGridViewTextBoxColumn();           // 4
            oTxtBoxD.HeaderText = "Size Power Number";
            oTxtBoxD.Visible = false;
            dataGridView1.Columns.Add(oTxtBoxD); //0
            //--------------------------------------------------------------------------------------------------------------

            if (frmNumber == 1) // Styles 
            {
                oBtnA = new DataGridViewButtonColumn();
                oBtnA.HeaderText = "Size Selection";
                oBtnB = new DataGridViewButtonColumn();
                oBtnB.HeaderText = "Trims Selection";
                oBtnC = new DataGridViewButtonColumn();
                oBtnC.HeaderText = "Colours Selection";
                oBtnD = new DataGridViewButtonColumn();
                oBtnD.HeaderText = "Label Selection";
                oChkBoxB = new DataGridViewCheckBoxColumn();
                oChkBoxB.HeaderText = "Mandatory QA Check";
                oChkBoxB.ValueType = typeof(bool);
                oTxtBoxE = new DataGridViewTextBoxColumn();
                oTxtBoxE.HeaderText = "Pastel Code Number";
                oTxtBoxE.ValueType = typeof(int);
                oTxtBoxF = new DataGridViewTextBoxColumn();
                oTxtBoxF.HeaderText = "ReOrder Level";
                oTxtBoxF.ValueType = typeof(int);
                oTxtBoxG = new DataGridViewTextBoxColumn();
                oTxtBoxG.HeaderText = "Expected Weekly Sales";
                oTxtBoxG.ValueType = typeof(int);
                oTxtBoxH = new DataGridViewTextBoxColumn();
                oTxtBoxH.HeaderText = "Pastel Quality Code";
                oTxtBoxH.ValueType = typeof(string);
                oTxtBoxJ = new DataGridViewTextBoxColumn();
                oTxtBoxJ.HeaderText = "Cotton Factor";
                oTxtBoxJ.ValueType = typeof(int);
                oTxtBoxK = new DataGridViewTextBoxColumn();
                oTxtBoxK.HeaderText = "Bags";
                oTxtBoxK.ValueType = typeof(int);
                oChkBoxA = new DataGridViewCheckBoxColumn();
                oChkBoxA.HeaderText = "Buttons Per Garment";
                oChkBoxA.ValueType = typeof(bool);
                oChkBoxC = new DataGridViewCheckBoxColumn();
                oChkBoxC.HeaderText = "Bought in Fabric";
                oChkBoxC.ValueType = typeof(bool);
                oTxtBoxL = new DataGridViewTextBoxColumn();
                oTxtBoxL.HeaderText = "Display Position";
                oTxtBoxL.ValueType = typeof(int);
                oChkBoxD = new DataGridViewCheckBoxColumn();
                oChkBoxD.HeaderText = "Conti Wear";
                oChkBoxD.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oBtnA);       //   5 Sizes 
                dataGridView1.Columns.Add(oBtnB);       //   6 Trims 
                dataGridView1.Columns.Add(oBtnC);       //   7 Style Colour  
                dataGridView1.Columns.Add(oBtnD);       //   8 Style Lable  
                dataGridView1.Columns.Add(oChkBoxB);    //   9 Mandatory QA Check
                dataGridView1.Columns.Add(oTxtBoxE);    //   10 Pastel Code Number
                dataGridView1.Columns.Add(oTxtBoxH);    //   11 Pastel quality Code
                dataGridView1.Columns.Add(oTxtBoxJ);    //   12 Cotton Factor 
                dataGridView1.Columns.Add(oTxtBoxK);    //   13 Bags
                dataGridView1.Columns.Add(oChkBoxA);    //   14 Buttons
                dataGridView1.Columns.Add(oChkBoxC);    //   15 Buttons
                dataGridView1.Columns.Add(oTxtBoxL);    //   16 Display Position
                dataGridView1.Columns.Add(oChkBoxD);    //   17 WorkWear
                //-------------------------------------------------------------------------------------------------
                // This is the row leave event that only needs to be fired for a select group of modules
                //------------------------------------------------------------------------------------------------------
                this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridView_RowLeave);
                
            }
            else if (frmNumber == 2)  // Colours 
            {
                oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.HeaderText = "Colour Number";


                oTxtBoxE = new DataGridViewTextBoxColumn();
                oTxtBoxE.HeaderText = "Std Times";
                oTxtBoxE.ValueType = typeof(decimal);

                oTxtBoxF = new DataGridViewTextBoxColumn();
                oTxtBoxF.HeaderText = "Current Ratio";
                oTxtBoxF.ValueType = typeof(decimal);

                oBtnA = new DataGridViewButtonColumn();
                oBtnA.HeaderText = "Measured Colour";

                oChkBoxB = new DataGridViewCheckBoxColumn();
                oChkBoxB.HeaderText = "Benchmark";

                oChkBoxC = new DataGridViewCheckBoxColumn();
                oChkBoxC.HeaderText = "Padding Y/N";

                dataGridView1.Columns.Add(oTxtBoxD);    //5
                dataGridView1.Columns.Add(oTxtBoxE);    //6
                dataGridView1.Columns.Add(oBtnA);       //7
                dataGridView1.Columns.Add(oChkBoxB);    //8
                dataGridView1.Columns.Add(oTxtBoxF);    //9
                dataGridView1.Columns.Add(oChkBoxC);    //10

            }
          else if (frmNumber == 3)    // Yarn Definition 
          {
              oTxtBoxD = new DataGridViewTextBoxColumn();
              oTxtBoxD.HeaderText = "Yarn Type";
              oTxtBoxE = new DataGridViewTextBoxColumn();
              oTxtBoxE.HeaderText = "Text Count";
              oTxtBoxF = new DataGridViewTextBoxColumn();
              oTxtBoxF.HeaderText = "Twist";
              oChkBoxB = new DataGridViewCheckBoxColumn();
              oChkBoxB.HeaderText = "Blocked";               //11 
              oChkBoxC = new DataGridViewCheckBoxColumn();
              oChkBoxC.HeaderText = "Standard Cost";         //13
              oTxtBoxK = new DataGridViewTextBoxColumn();
              oTxtBoxK.HeaderText = "Standard Cost / Unit";   //14
              oTxtBoxL = new DataGridViewTextBoxColumn();
              oTxtBoxL.HeaderText = "ROL in UOM";            //15
              oTxtBoxM = new DataGridViewTextBoxColumn();
              oTxtBoxM.HeaderText = "ROQ (UOM)";             //16
              oChkBoxD = new DataGridViewCheckBoxColumn();
              oChkBoxD.HeaderText = "Show Qty";              //17 
              oTxtBoxQ = new DataGridViewTextBoxColumn();
              oTxtBoxQ.HeaderText = "Cone Colour";          // 18
              oTxtBoxR = new DataGridViewTextBoxColumn();
              oTxtBoxR.HeaderText = "Pallet Weight";          // 19\
              oTxtBoxR.ValueType = typeof(decimal);

              //---- Retrieve data from database-----------------------------------------------------------------
              using (var context = new TTI2Entities())
              {
                  oCmbBoxA = new DataGridViewComboBoxColumn();
                  oCmbBoxA.HeaderText = "Cotton Origin";         
                  oCmbBoxA.DataSource = context.TLADM_CottonOrigin.OrderBy(x => x.CottonOrigin_ShortCode).ToList();
                  oCmbBoxA.DisplayMember = "CottonOrigin_Description";
                  oCmbBoxA.ValueMember = "CottonOrigin_Pk";

                  oCmbBoxE = new DataGridViewComboBoxColumn();
                  oCmbBoxE.HeaderText = "UOM";
                  oCmbBoxE.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                  oCmbBoxE.DisplayMember = "UOM_Description";
                  oCmbBoxE.ValueMember = "UOM_Pk";
                    
                  oCmbBoxB = new DataGridViewComboBoxColumn();
                  oCmbBoxB.HeaderText = "Yarn Supplier";
                  oCmbBoxB.DataSource = context.TLADM_Suppliers.OrderBy(x => x.Sup_Code).ToList();
                  oCmbBoxB.ValueMember = "Sup_Pk";
                  oCmbBoxB.DisplayMember = "Sup_Description";

                  oCmbBoxC = new DataGridViewComboBoxColumn();
                  oCmbBoxC.HeaderText = "Product Type";
                  oCmbBoxC.DataSource = context.TLADM_ProductTypes.OrderBy(x => x.PT_ShortCode).ToList();
                  oCmbBoxC.ValueMember = "PT_Pk";
                  oCmbBoxC.DisplayMember = "PT_Description";

                  oCmbBoxK = new DataGridViewComboBoxColumn();
                  oCmbBoxK.HeaderText = "Colour Availability";
                  oCmbBoxK.DataSource = context.TLADM_Colours.Where(x => (bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
                  oCmbBoxK.ValueMember = "Col_Id";
                  oCmbBoxK.DisplayMember = "Col_Display";

              }

              dataGridView1.Columns.Add(oTxtBoxD); //5  Yarn Type
              dataGridView1.Columns.Add(oTxtBoxE); //6  Tex Count
              dataGridView1.Columns.Add(oTxtBoxF); //7  Twist
              dataGridView1.Columns.Add(oCmbBoxA); //8  Cotton Origin
              dataGridView1.Columns.Add(oCmbBoxB); //9  Supplier  
              dataGridView1.Columns.Add(oCmbBoxC); //10 Product Type 
              dataGridView1.Columns.Add(oChkBoxB); //11 Blocked 
              dataGridView1.Columns.Add(oCmbBoxE); //12 UOM 
              dataGridView1.Columns.Add(oChkBoxC); //13 Std Cost Y/N
              dataGridView1.Columns.Add(oTxtBoxK); //14 Standard Cost Unit 
              dataGridView1.Columns.Add(oTxtBoxL); //15 ROL in UOM 
              dataGridView1.Columns.Add(oTxtBoxM); //16 ROQ in UOM 
              dataGridView1.Columns.Add(oChkBoxD); //17 Show Qty 
              dataGridView1.Columns.Add(oTxtBoxQ); //18 Show Qty 
              dataGridView1.Columns.Add(oTxtBoxR); //19 Pallet Weight 
              dataGridView1.Columns.Add(oCmbBoxK); //20 Colour

              //-------------------------------------------------------------------------------------------------
              // This is the row leave event that only needs to be fired for a select group of modules
              //------------------------------------------------------------------------------------------------------
              this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridView_RowLeave);
          }
          else if (frmNumber == 4)
          {
              var Repo = new Administration.AdminRepository();

              oBtnA = new DataGridViewButtonColumn();
              oBtnA.HeaderText = "Yarn Type";
              oCmbBoxH = new DataGridViewComboBoxColumn();   // Griege Quality    
              oCmbBoxH.HeaderText = "Greige Quality";
              oCmbBoxB = new DataGridViewComboBoxColumn();   // Fabric Weight
              oCmbBoxB.HeaderText = "Fabric Weight";
              oCmbBoxC = new DataGridViewComboBoxColumn();   // Fabric Width 
              oCmbBoxC.HeaderText = "Fabric Width";
              oCmbBoxD = new DataGridViewComboBoxColumn();   // Machine No
              oCmbBoxD.HeaderText = "Machine No";
              oChkBoxB = new DataGridViewCheckBoxColumn();   // Barcode   
              oChkBoxB.HeaderText = "Barcode";
              oCmbBoxE = new DataGridViewComboBoxColumn();   // UOM 
              oCmbBoxE.HeaderText = "UOM";
              oTxtBoxL = new DataGridViewTextBoxColumn();    // ROL in UOM 
              oTxtBoxL.HeaderText = "ROL in UOM";
              oTxtBoxM = new DataGridViewTextBoxColumn();    // ROQ in UOM
              oTxtBoxM.HeaderText = "ROQ (UOM)";
              oChkBoxC = new DataGridViewCheckBoxColumn();   // Show Qty
              oChkBoxC.HeaderText = "Show Qty";
              oCmbBoxG = new DataGridViewComboBoxColumn();   // Stock Take Category
              oCmbBoxG.HeaderText = "Stock Take Category";
              oCmbBoxJ = new DataGridViewComboBoxColumn();   // Product Type  
              oCmbBoxJ.HeaderText = "Product Type";
              oTxtBoxK = new DataGridViewTextBoxColumn();    // Kgs Per Piece 
              oTxtBoxK.HeaderText = "Kg's Per Piece";
              oTxtBoxS = new DataGridViewTextBoxColumn();    // Meters to measure
              oTxtBoxS.HeaderText = "Meters Measured";
              oTxtBoxS.ValueType = typeof(Int32);
              oTxtBoxT = new DataGridViewTextBoxColumn();    // Faults Allowed
              oTxtBoxT.HeaderText = "Faults Allowed";
              oTxtBoxT.ValueType = typeof(Int32);
              oBtnB = new DataGridViewButtonColumn();        // Colours abal to select 
              oBtnB.HeaderText = "Colours";
              oChkBoxD = new DataGridViewCheckBoxColumn();
              oChkBoxD.HeaderText = "Bought In";
              oChkBoxD.ValueType = typeof(bool);
              oTxtBoxU = new DataGridViewTextBoxColumn();    // Faults Allowed
              oTxtBoxU.HeaderText = "Dsk Weight";
              
              dataT = new DataTable();

              DataColumn column = new DataColumn();


              //---- Retrieve data from database-----------------------------------------------------------------
              using (var context = new TTI2Entities())
              {
                  oCmbBoxH.DataSource = context.TLADM_GreigeQuality.OrderBy(x => x.GQ_Description).ToList();
                  oCmbBoxH.DisplayMember = "GQ_Description";
                  oCmbBoxH.ValueMember = "GQ_Pk";

                  oCmbBoxB.DataSource = context.TLADM_FabricWeight.OrderBy(x => x.FWW_Description).ToList();
                  oCmbBoxB.DisplayMember = "FWW_Description";
                  oCmbBoxB.ValueMember = "FWW_Id";

                  oCmbBoxC.DataSource = context.TLADM_FabWidth.OrderBy(x => x.FW_Description).ToList();
                  oCmbBoxC.ValueMember = "FW_Id";
                  oCmbBoxC.DisplayMember = "FW_Description";

                  oCmbBoxD.DataSource = context.TLADM_MachineDefinitions.OrderBy(x => x.MD_AlternateDesc).ToList();
                  oCmbBoxD.ValueMember = "MD_Pk";
                  oCmbBoxD.DisplayMember = "MD_Description";

                  oCmbBoxE.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                  oCmbBoxE.ValueMember = "UOM_Pk";
                  oCmbBoxE.DisplayMember = "UOM_Description";

                  oCmbBoxG.DataSource = context.TLADM_StockTakeFreq.OrderBy(x => x.STF_ShortCode).ToList();
                  oCmbBoxG.ValueMember = "STF_Pk";
                  oCmbBoxG.DisplayMember = "STF_Description";

                  oCmbBoxJ.DataSource = context.TLADM_ProductTypes.OrderBy(x => x.PT_ShortCode).ToList();
                  oCmbBoxJ.ValueMember = "PT_Pk";
                  oCmbBoxJ.DisplayMember = "PT_Description";

              }
              //-------------------------------------------------------------------------------------------------
              dataGridView1.Columns.Add(oCmbBoxH);  // 5  Greige Quality
              dataGridView1.Columns.Add(oBtnA);     // 6  Yarn Tex
              dataGridView1.Columns.Add(oCmbBoxB);  // 7  Fabric Weight
              dataGridView1.Columns.Add(oCmbBoxC);  // 8  Fabric Width 
              dataGridView1.Columns.Add(oCmbBoxD);  // 9  Machine No  
              dataGridView1.Columns.Add(oCmbBoxJ);  //10  Product Type  
              dataGridView1.Columns.Add(oChkBoxB);  //11 Barcode 
              dataGridView1.Columns.Add(oCmbBoxE);  //12 UOM 
              dataGridView1.Columns.Add(oTxtBoxL);  //13 ROL in UOM 
              dataGridView1.Columns.Add(oTxtBoxM);  //14 ROQ in UOM 
              dataGridView1.Columns.Add(oChkBoxC);  //15 Show Qty
              dataGridView1.Columns.Add(oCmbBoxG);  //16 Stock Take Category
              dataGridView1.Columns.Add(oTxtBoxK);  //17 Stock Take Category
              dataGridView1.Columns.Add(oTxtBoxS);  //18 Meters 
              dataGridView1.Columns.Add(oTxtBoxT);  //19 Faults Allowed
              dataGridView1.Columns.Add(oBtnB);     // 20 Colour
              dataGridView1.Columns.Add(oChkBoxD);  // 21 Bought In Fabric for which we are going to knit the trims
              dataGridView1.Columns.Add(oTxtBoxU);  // 22 Dsk Weight
              //-------------------------------------------------------------------------------------------------
              // This is the row leave event that only needs to be fired for a select group of modules
              //------------------------------------------------------------------------------------------------------
              this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridView_RowLeave);
          }
          else if (frmNumber == 5)
          {
               
          }
          else if (frmNumber == 6)
          {
              oTxtBoxE = new DataGridViewTextBoxColumn();
              oTxtBoxE.HeaderText = "Finished Code";
          }
          else if (frmNumber == 7)   
          {
              oChkBoxB = new DataGridViewCheckBoxColumn();
              oChkBoxB.HeaderText = "Body";

              oCmbBoxA = new DataGridViewComboBoxColumn();
              oCmbBoxA.HeaderText = "Fabric Width";

              oCmbBoxB = new DataGridViewComboBoxColumn();
              oCmbBoxB.HeaderText = "Fabric Weight";

              oCmbBoxC = new DataGridViewComboBoxColumn();
              oCmbBoxC.HeaderText = "Greige";

              oChkBoxC = new DataGridViewCheckBoxColumn();
              oChkBoxC.HeaderText = "Sizes Relevant";

              oCmbBoxD = new DataGridViewComboBoxColumn();
              oCmbBoxD.HeaderText = "Sizes";

              using (var context = new TTI2Entities())
              {
                  oCmbBoxA.DataSource = context.TLADM_FabWidth.OrderBy(x => x.FW_Description).ToList();
                  oCmbBoxA.ValueMember = "FW_Id";
                  oCmbBoxA.DisplayMember = "FW_Description";

                  oCmbBoxB.DataSource = context.TLADM_FabricWeight.OrderBy(x => x.FWW_Description).ToList();
                  oCmbBoxB.ValueMember = "FWW_Id";
                  oCmbBoxB.DisplayMember = "FWW_Description";

                  oCmbBoxC.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                  oCmbBoxC.ValueMember = "TLGreige_Id";
                  oCmbBoxC.DisplayMember = "TLGreige_Description";

                  oCmbBoxD.DataSource = context.TLADM_Sizes.OrderBy(x => x.SI_DisplayOrder).ToList();
                  oCmbBoxD.ValueMember = "SI_Id";
                  oCmbBoxD.DisplayMember = "SI_Description";
              }
               
          }
          else if (frmNumber == 8)    // Sizes 
          {
              this.dataGridView1.AllowUserToAddRows = false;

              oTxtBoxD = new DataGridViewTextBoxColumn();
              oTxtBoxD.HeaderText = "Pastel Code";
              oTxtBoxD.ValueType = typeof(int);
              dataGridView1.Columns.Add(oTxtBoxD);

              oTxtBoxF = new DataGridViewTextBoxColumn();
              oTxtBoxF.HeaderText = "Display Order";
              oTxtBoxF.ValueType = typeof(int);
              dataGridView1.Columns.Add(oTxtBoxF);

              oTxtBoxE = new DataGridViewTextBoxColumn();
              oTxtBoxE.HeaderText = "Column Code";
              oTxtBoxE.ValueType = typeof(int);
              dataGridView1.Columns.Add(oTxtBoxE);

              oTxtBoxG = new DataGridViewTextBoxColumn();
              oTxtBoxG.HeaderText = "UI Display";
              oTxtBoxG.ValueType = typeof(String);
              dataGridView1.Columns.Add(oTxtBoxG);

              oTxtBoxH = new DataGridViewTextBoxColumn();
              oTxtBoxH.HeaderText = "Conti Size";
              oTxtBoxH.ValueType = typeof(int);
              dataGridView1.Columns.Add(oTxtBoxH);

            }
          else if (frmNumber == 9)
          {
              oCmbBoxA = new DataGridViewComboBoxColumn();   // Customer Details    
              oCmbBoxA.HeaderText = "Customer";

              using (var context = new TTI2Entities())
              {
                  oCmbBoxA.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                  oCmbBoxA.ValueMember = "Cust_Pk";
                  oCmbBoxA.DisplayMember = "Cust_Description";
              }

              dataGridView1.Columns.Add(oCmbBoxA);
          }
          else if (frmNumber == 13)
          {
              oTxtBoxE = new DataGridViewTextBoxColumn();
              oTxtBoxE.HeaderText = "Fabric Weight Calculation";
          }
          else if (frmNumber == 16)  // Fabric Attributes
          {/*
              oCmbBoxB = new DataGridViewComboBoxColumn();   // Product Type
              oCmbBoxB.HeaderText = "Product Type";
              oBtnA = new DataGridViewButtonColumn();
              oBtnA.HeaderText = "Please Select a Colour";
              oCmbBoxG = new DataGridViewComboBoxColumn();   // Product Group
              oCmbBoxG.HeaderText = "Product Group";
              oChkBoxB = new DataGridViewCheckBoxColumn();   // Blocked   
              oChkBoxB.HeaderText = "Blocked";
              oCmbBoxH = new DataGridViewComboBoxColumn();   // UOM 
              oCmbBoxH.HeaderText = "UOM";
              oCmbBoxE = new DataGridViewComboBoxColumn();   // Preferred Supplier    
              oCmbBoxE.HeaderText = "Preferred Supplier";
              oChkBoxC = new DataGridViewCheckBoxColumn();   // Barcode
              oChkBoxC.HeaderText = "Barcode";
              oChkBoxD = new DataGridViewCheckBoxColumn();   // Show Qty
              oChkBoxD.HeaderText = "Show Qty";
              oCmbBoxJ = new DataGridViewComboBoxColumn();   //Greige Type    
              oCmbBoxJ.HeaderText = "Greige Type";
              //---- Retrieve data from database-----------------------------------------------------------------
              using (var context = new TTI2Entities())
              {
                  oCmbBoxA.DataSource = context.TLADM_Labels.OrderBy(x => x.Lbl_Description).ToList();
                  oCmbBoxA.DisplayMember = "Lbl_Description";
                  oCmbBoxA.ValueMember = "Lbl_Id";

                  oCmbBoxB.DataSource = context.TLADM_ProductTypes.OrderBy(x => x.PT_Description).ToList();
                  oCmbBoxB.DisplayMember = "PT_Description";
                  oCmbBoxB.ValueMember = "PT_Pk";

                  oCmbBoxG.DataSource = context.TLADM_FabricProduct.OrderBy(x => x.FP_Description).ToList();
                  oCmbBoxG.ValueMember = "FP_Id";
                  oCmbBoxG.DisplayMember = "FP_Description";

                  oCmbBoxH.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                  oCmbBoxH.ValueMember = "UOM_Pk";
                  oCmbBoxH.DisplayMember = "UOM_Description";

                  oCmbBoxE.DataSource = context.TLADM_Suppliers.OrderBy(x => x.Sup_Description).ToList();
                  oCmbBoxE.ValueMember = "Sup_Pk";
                  oCmbBoxE.DisplayMember = "Sup_Description";

                  oCmbBoxJ.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                  oCmbBoxJ.ValueMember = "TLGreige_Id";
                  oCmbBoxJ.DisplayMember = "TLGreige_Description";
              }

              dataGridView1.Columns.Add(oCmbBoxJ);     //5  Greige
             // dataGridView1.Columns.Add(oCmbBoxA);     //6  Brand
              dataGridView1.Columns.Add(oCmbBoxB);     //7  Product Type
              dataGridView1.Columns.Add(oBtnA);        //8  Colour
              dataGridView1.Columns.Add(oCmbBoxG);     //9  Product Group 
              dataGridView1.Columns.Add(oChkBoxB);     //10 Blocked  
              dataGridView1.Columns.Add(oCmbBoxH);     //11  UOM  
              dataGridView1.Columns.Add(oCmbBoxE);    //12  Preferred Supplier 
              dataGridView1.Columns.Add(oChkBoxC);     //13  Barcode 
              dataGridView1.Columns.Add(oChkBoxD);     //14  Show Qty 
              //-------------------------------------------------------------------------------------------------
              // This is the row leave event that only needs to be fired for a select group of modules
              //------------------------------------------------------------------------------------------------------
              this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridView_RowLeave);
            * */
            }
            else if (frmNumber == 17)   // Panel Attributes
            {
                oTxtBoxE = new DataGridViewTextBoxColumn();
                oTxtBoxE.HeaderText = "Grade";
                oBtnA = new DataGridViewButtonColumn();        // Size
                oBtnA.HeaderText = "Size";
                oBtnB = new DataGridViewButtonColumn();        // Size
                oBtnB.HeaderText = "Fabric";
                oChkBoxB = new DataGridViewCheckBoxColumn();   // Blocked   
                oChkBoxB.HeaderText = "Blocked";
                oCmbBoxH = new DataGridViewComboBoxColumn();   // UOM 
                oCmbBoxH.HeaderText = "UOM";
                oCmbBoxE = new DataGridViewComboBoxColumn();   // Preferred Supplier    
                oCmbBoxE.HeaderText = "Preferred Supplier";
                oChkBoxC = new DataGridViewCheckBoxColumn();   // Show Qty
                oChkBoxC.HeaderText = "Show Qty";
                //---- Retrieve data from database-----------------------------------------------------------------              
                using (var context = new TTI2Entities())
                {
                    oCmbBoxH.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                    oCmbBoxH.ValueMember = "UOM_Pk";
                    oCmbBoxH.DisplayMember = "UOM_Description";

                    oCmbBoxE.DataSource = context.TLADM_Suppliers.OrderBy(x => x.Sup_Description).ToList();
                    oCmbBoxE.ValueMember = "Sup_Pk";
                    oCmbBoxE.DisplayMember = "Sup_Description";
                }

                dataGridView1.Columns.Add(oTxtBoxE);      // 6  Grade
                dataGridView1.Columns.Add(oBtnA);         // 7  Size
                dataGridView1.Columns.Add(oChkBoxB);      // 8  Blocked 
                dataGridView1.Columns.Add(oCmbBoxH);      // 9  UOM  
                dataGridView1.Columns.Add(oCmbBoxE);      //10  Preferred Supplier 
                dataGridView1.Columns.Add(oChkBoxC);      //11  Show Qty 
                dataGridView1.Columns.Add(oBtnB);         //14  Size
                
                //-------------------------------------------------------------------------------------------------
                // This is the row leave event that only needs to be fired for a select group of modules
                //------------------------------------------------------------------------------------------------------
                this.dataGridView1.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridView_RowLeave);
              
            }
            else if (frmNumber == 18)
            {
                oCmbBoxA = new DataGridViewComboBoxColumn();   // Departments  
                oCmbBoxA.HeaderText = "Departments";

                oTxtBoxE = new DataGridViewTextBoxColumn();
                oTxtBoxE.HeaderText = "Production Loss %";

                oTxtBoxF = new DataGridViewTextBoxColumn();
                oTxtBoxF.HeaderText = "Production Loss Kg";

                //---- Retrieve data from database-----------------------------------------------------------------              
                using (var context = new TTI2Entities())
                {
                    oCmbBoxA.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                    oCmbBoxA.ValueMember = "Dep_Id";
                    oCmbBoxA.DisplayMember = "Dep_Description";

                }

                dataGridView1.Columns.Add(oCmbBoxA);      // 1  Departments  
                dataGridView1.Columns.Add(oTxtBoxE);      // 2  Production Loss % 
                dataGridView1.Columns.Add(oTxtBoxF);      // 3  Production Loss Kgs 
                

            }
            else if (frmNumber == 19)
            {

                oCmbBoxA = new DataGridViewComboBoxColumn();   // Styles  
                oCmbBoxA.HeaderText = "Styles";

                oTxtBoxE = new DataGridViewTextBoxColumn();
                oTxtBoxE.HeaderText = "Grade A Code";

                oTxtBoxF = new DataGridViewTextBoxColumn();
                oTxtBoxF.HeaderText = "Grade B Code";

                //---- Retrieve data from database-----------------------------------------------------------------              
                using (var context = new TTI2Entities())
                {
                    oCmbBoxA.DataSource = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                    oCmbBoxA.ValueMember = "Sty_Id";
                    oCmbBoxA.DisplayMember = "Sty_Description";
                }

                dataGridView1.Columns.Add(oCmbBoxA);      // 1  Departments  
                dataGridView1.Columns.Add(oTxtBoxE);      // 2  Production Loss % 
                dataGridView1.Columns.Add(oTxtBoxF);      // 3  Production Loss Kgs 
            }
            //---------------------------------------------------------------------------------------
            // End of General Definition
            //---------------------------------------------------------------------------------------------------
            if (frmNumber == 6)
            {
                dataGridView1.Columns.Add(oTxtBoxE);
            }

            else if (frmNumber == 7)
            {
                dataGridView1.Columns.Add(oChkBoxB);
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns.Add(oCmbBoxB);
                dataGridView1.Columns.Add(oCmbBoxC);
                dataGridView1.Columns.Add(oChkBoxC);
                dataGridView1.Columns.Add(oCmbBoxD);

              
            }
            else if (frmNumber == 13)
            {
                dataGridView1.Columns.Add(oTxtBoxE);
            }
            //-------------------------------------------------------------------------------------------
            // Now to retrieve the data from the database
            //-------------------------------------------------------------------------------------------------
            if (frmNumber == 1)
            {
               dataGridView1 = core.Get_Styles(dataGridView1, pn);
               if (dataGridView1.RowCount > 0)
                   MandSelected = core.PopulateArray(MandatoryFields.Length, true);
            }
            else if (frmNumber == 2)
            {
                dataGridView1 = core.Get_Colours(dataGridView1);
            }
            else if (frmNumber == 3)
            {
                dataGridView1 = core.Get_Yarn(dataGridView1);
                if (dataGridView1.RowCount > 0)
                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);
            }
            else if (frmNumber == 4)
            {
                dataGridView1 = core.Get_Griege(dataGridView1);
                if (dataGridView1.RowCount > 0)
                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);
             }
            else if (frmNumber == 5)
            {
                dataGridView1 = core.Get_FabricWidth(dataGridView1);
            }
            else if (frmNumber == 6)
            {
                //dataGridView1 = core.Get_AuxColours(dataGridView1);
            }
            else if (frmNumber == 7)
            {
                dataGridView1 = core.Get_Trims(dataGridView1);
            }
            else if (frmNumber == 8)
            {
                dataGridView1 = core.Get_Sizes(dataGridView1);
            }
            else if (frmNumber == 9)
            {
                dataGridView1 = core.Get_Labels(dataGridView1);
            }
            else if (frmNumber == 12)
            {
               dataGridView1 = core.Get_FabricProduct(dataGridView1);
            }
            else if (frmNumber == 13)
            {
                dataGridView1 = core.Get_FabricWeight(dataGridView1);
            }
            else if (frmNumber == 14)
            {
                dataGridView1 = core.Get_GreigeQuality(dataGridView1);
                dataGridView1.Columns[1].Width = 150;
            }
            else if (frmNumber == 16)
            {
                /*
                dataGridView1 = core.Get_FabricAttributes(dataGridView1);
                if (dataGridView1.RowCount > 0)
                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);
                 * */
            }

            else if (frmNumber == 17)
            {
                dataGridView1 = core.Get_PanelAttributes(dataGridView1);
                if (dataGridView1.RowCount > 0)
                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);
            }

            else if (frmNumber == 18)
            {
                dataGridView1 = core.Get_ProductionLoss(dataGridView1);
            }

            else if (frmNumber == 19)
            {
                dataGridView1 = core.Get_StyleGrades(dataGridView1);
            }

            formLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            
            if (oBtn != null)
            {
                var lTransSuccessful = false;

                if (frmNumber == 1)
                {
                    if (LabelSelected)
                    {
                        try
                        {
                            var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                            if (!string.IsNullOrEmpty(errorM))
                            {
                                MessageBox.Show(errorM);
                                return;
                            }
                            
                            var lbl = (TLADM_CustomerFile)cmbLabels.SelectedItem;
                            lTransSuccessful = core.Save_Style(dataGridView1, lbl.Cust_Pk);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a label from the drop down box");
                        return;

                    }
                }
                else if (frmNumber == 2)
                {
                    try
                    {
                        lTransSuccessful = core.Save_Colours(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else if (frmNumber == 3)
                {
                    try
                    {
                       var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                       if (!string.IsNullOrEmpty(errorM))
                       {
                            MessageBox.Show(errorM);
                            return;
                       }
                       lTransSuccessful = core.Save_Yarn(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else if (frmNumber == 4)
                {
                    try
                    {
                        var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                        if (!string.IsNullOrEmpty(errorM))
                        {
                            MessageBox.Show(errorM);
                            return;
                        }

                        lTransSuccessful = core.Save_Griege(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else if (frmNumber == 5)
                {
                    try
                    {
                        lTransSuccessful = core.Save_FabricWidth(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                
                    /*
                else if (frmNumber == 6)
                {
                    try
                    {
                        lTransSuccessful = core.Save_AuxColours(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                 */
 
                else if (frmNumber == 7)
                {
                    try
                    {
                        lTransSuccessful = core.Save_Trims(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else if (frmNumber == 8)
                {
                    try
                    {
                        lTransSuccessful = core.Save_Sizes(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else if (frmNumber == 9)
                {
                    try
                    {
                        lTransSuccessful = core.Save_Labels(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }
                else if (frmNumber == 12)
                {
                    try
                    {
                        lTransSuccessful = core.Save_FabricProduct(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }

                else if (frmNumber == 13)
                {
                    try
                    {
                        lTransSuccessful = core.Save_FabricWeight(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }

                else if (frmNumber == 14)
                {
                    try
                    {
                        lTransSuccessful = core.Save_GreigeQuality(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }

                else if (frmNumber == 16)
                {
                    try
                    {
                        //lTransSuccessful = core.Save_FabricAttributes(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }

                else if (frmNumber == 17)
                {
                    try
                    {
                        lTransSuccessful = core.Save_PanelAttributes(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }

                else if (frmNumber == 18)
                {
                    try
                    {
                        lTransSuccessful = core.Save_ProductionLoss(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }

                else if (frmNumber == 19)
                {
                    try
                    {
                        lTransSuccessful = core.Save_StyleGrades(dataGridView1);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Close();
                    }
                }

                if (lTransSuccessful)
                {
                    if (frmNumber == 1    || 
                        frmNumber == 3    || 
                        frmNumber == 4    || 
                        frmNumber == 16   ||
                        frmNumber == 17)     
                    {
                        this.dataGridView1.RowLeave -= new DataGridViewCellEventHandler(this.datagridView_RowLeave);
                        this.dataGridView1.Rows.Clear();
                    }
                    else
                    {
                        this.dataGridView1.Rows.Clear();
                    }
                    
                    if (dtpObject != null)
                    {
                        if (dtpObject.Visible)
                            dtpObject.Visible = false;
                    }

                    MessageBox.Show("Record/s successfully stored to database");
                }
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var oDgv = sender as DataGridView;
          
            try
            {
                if (oDgv != null && formLoaded)
                {
                    allowDisc = false;
                    allowDateSelection = false;
                    /*
                    if (frmNumber == 1  || 
                        frmNumber == 3  || 
                        frmNumber == 4  || 
                        frmNumber == 16 || 
                        frmNumber == 17)
                    {
                        if (oDgv.CurrentCell != null)
                        {
                            ActiveRow = oDgv.CurrentCell.RowIndex;
                            MandSelected = core.PopulateArray(MandatoryFields.Length, false);
                        }
                    }
                     */ 
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var oDgv = sender as DataGridView;

            if (frmNumber != 18 && frmNumber != 19)
            {
                try
                {
                    if (oDgv != null && oDgv.Focused)
                    {
                        if (oDgv.CurrentCell.ColumnIndex == 1)
                        {

                        }
                        else if (oDgv.CurrentCell.ColumnIndex == 2 && allowDateSelection)
                        {
                            dtpObject.Location = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                            dtpObject.Visible = true;
                            if (oDgv.CurrentCell.Value != null && (string)oDgv.CurrentCell.Value != string.Empty)
                            {
                                dtpObject.Value = (DateTime)oDgv.CurrentCell.Value;
                            }
                            else
                            {
                                dtpObject.Value = DateTime.Today;
                            }
                        }

                    }
                    else
                    {
                        dtpObject.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            try
            {
                if (frmNumber != 18 && frmNumber != 19)
                {
                    if (oDgv != null && oDgv.Focused)
                    {
                        if (oDgv.CurrentCell.ColumnIndex == 0)
                        {

                            allowDisc = true;
                            if (!btnSave.Enabled)
                                btnSave.Enabled = true;

                        }
                        else if (oDgv.CurrentCell.ColumnIndex == 2)
                        {
                            if (allowDateSelection)
                                oDgv.CurrentCell.Value = dtpObject.Value.Date;
                            else
                                oDgv.CurrentCell.Value = String.Empty;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtpObject_valueChanged(object sender, EventArgs e)
        {
            var oDtp = sender as DateTimePicker;

            if (frmNumber != 18 && frmNumber != 19)
            {
                try
                {
                    if (oDtp != null)
                        dataGridView1.CurrentCell.Value = oDtp.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                bool isChecked = (bool)oDgv[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                if (!allowDisc && isChecked)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)oDgv.CurrentCell;
                    chk.EditingCellFormattedValue = false;
                    oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !isChecked;
                    oDgv.EndEdit();
                    return;
                }
                if (!isChecked)
                {
                    if (e.ColumnIndex == 1)
                    {
                        oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = string.Empty;
                        allowDateSelection = false;
                        dtpObject.Visible = false;
                        oDgv.EndEdit();
                        return;
                    }
                }

                allowDateSelection = true;
                oDgv.EndEdit();
            }
            else if (oDgv.Focused && oDgv.CurrentCell is DataGridViewButtonCell)
            {
                var Id = 0;
                if (frmNumber == 1)
                {
                    Id = 1000 + e.ColumnIndex;
                    if (e.ColumnIndex == 6)
                        Id += 1;
                    else if (e.ColumnIndex == 7)
                    {
                        Id = 1006;

                    }
                    else if (e.ColumnIndex == 8)
                    {
                        Id = 1015;

                    }
                }
                else if (frmNumber == 2)
                {
                   
                   /* var chkCell = oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex - 1];
                    if (!Convert.ToBoolean(chkCell.Value.ToString()))
                    {
                        MessageBox.Show("Please select the auxiliary checkbox");
                        return;
                    }*/
                     
                    Id = 2000 + e.ColumnIndex;
                }
                else if (frmNumber == 4)    // Greige Module 
                {
                    Id = 4000 + e.ColumnIndex;
                }
                else if (frmNumber == 16)    // Fabric Module 
                {
                    Id = -2 + 1000 + e.ColumnIndex;
                }
                else if (frmNumber == 17)    // Panel Module 
                {
                    if (e.ColumnIndex != 11)
                        Id = -1 + 1000 + e.ColumnIndex;
                    else
                        Id = 17000 + e.ColumnIndex;
                }
                if (Id != 1011)
                {
                    int pn;
                                                      
                    if (!string.IsNullOrEmpty(oDgv.CurrentCell.EditedFormattedValue.ToString()))
                        pn = Convert.ToInt32(oDgv.CurrentCell.EditedFormattedValue);
                    else
                        pn = 0;

                    if (frmNumber == 1 && Id == 1014)
                    {
                        var selected = (TLADM_CustomerFile)cmbLabels.SelectedItem;
                        if (selected != null)
                        {
                            frmTLADMGardProp aprop = new frmTLADMGardProp(Id, pn, selected.Cust_Pk);
                            aprop.ShowDialog();
                            if(!aprop.UserFormClosed)
                               oDgv.CurrentCell.Value = aprop.TotalPN.ToString();
                        }
                    }
                   
                    else if (frmNumber == 1 && Id == 1006)
                    {
                        if (oDgv.Rows[oDgv.CurrentCell.RowIndex].Cells[3].Value != null)
                        {
                            pn = Convert.ToInt32(oDgv.Rows[oDgv.CurrentCell.RowIndex].Cells[3].Value);
                            if (pn != 0)
                            {
                                frmTLADMGardProp aprop = new frmTLADMGardProp(Id, pn);
                                aprop.ShowDialog();
                                if(!aprop.UserFormClosed)
                                    oDgv.CurrentCell.Value = aprop.TotalPN.ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please save master record prior to setting either colours or Trims");
                        }
                    }
                    else if (frmNumber == 1 && Id == 1007)
                    {
                        if (oDgv.Rows[oDgv.CurrentCell.RowIndex].Cells[6].Value != null)
                        {
                            // Get the Styles Key Value link to TLADM_StyTrim 
                            //=================================================
                            pn = Convert.ToInt32(oDgv.Rows[oDgv.CurrentCell.RowIndex].Cells[3].Value);
                            frmTLADMGardProp aprop = new frmTLADMGardProp(Id, pn);
                            aprop.ShowDialog();
                            if(!aprop.UserFormClosed)
                                oDgv.CurrentCell.Value = aprop.TotalPN.ToString();
                            
                        }
                        
                    }
                    else
                    {
                        if (Id == 2006 || Id == 4020)
                            pn = Convert.ToInt32(oDgv.Rows[oDgv.CurrentCell.RowIndex].Cells[3].Value.ToString());

                        if (frmNumber != 2)
                        {
                            frmTLADMGardProp aprop = new frmTLADMGardProp(Id, pn);
                            aprop.ShowDialog();
                            if(!aprop.UserFormClosed)
                                oDgv.CurrentCell.Value = aprop.TotalPN.ToString();
                         }
                    }
                }
                else
                {
                    var CurrentCell = oDgv.CurrentCell;
                    int PanelPk     = Convert.ToInt32(oDgv.Rows[e.RowIndex].Cells[6].Value.ToString());
                    int SizeFK      = 0;

                    using (var context = new TTI2Entities())
                    {
                        var data = context.TLADM_PanelAttributes.Where(x => x.Pan_PK == PanelPk).FirstOrDefault();
                        if (data != null)
                        {
                            SizeFK = data.Pan_Size_FK;
                        }
                        else
                        {
                            MessageBox.Show("Panel info not entered correctly");
                            return;
                        }
                    }

                    frmTLADMGardAdditional apropAdditional = new frmTLADMGardAdditional(PanelPk, SizeFK);
                    apropAdditional.ShowDialog(this);
                }
            }
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                bool isChecked = (bool)oDgv[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                if (!allowDisc && isChecked)
                {

                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)oDgv.CurrentCell;
                    chk.EditingCellFormattedValue = false;
                    oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = !isChecked;
                    return;
                }
            }
        }


        private void cmb(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;

            if (formLoaded && oCmb != null && oCmb.Focused)
            {
                Util core = new Util();
                var lbls = (TLADM_CustomerFile)cmbLabels.SelectedItem;
                dataGridView1 = core.Get_Styles(dataGridView1, lbls.Cust_Pk);
                if (dataGridView1.Rows.Count > 0)
                {
                    LabelSelected = true;
                }
            }
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            
            if (formLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && frmNumber == 1)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 9 || /*Cell.ColumnIndex == 11 ||*/ Cell.ColumnIndex == 12 || Cell.ColumnIndex == 13)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                       e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                       e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
                else if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && frmNumber == 5)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
                else if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && frmNumber == 3)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 6 || Cell.ColumnIndex == 7 || Cell.ColumnIndex == 14 || Cell.ColumnIndex == 19)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 15 || Cell.ColumnIndex == 16)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    }
                }
                else if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && frmNumber == 4)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 13 || Cell.ColumnIndex == 14)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 17)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);

                    }
                    else if (Cell.ColumnIndex == 18 || Cell.ColumnIndex == 19)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);

                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
                else if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && frmNumber == 8)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 5 || Cell.ColumnIndex == 6)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);

                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
                else if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && frmNumber == 13)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex != 0)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);

                    }

                }
                else if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell && frmNumber == 18)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 3)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if(Cell.ColumnIndex == 4)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                    }

                }
            }
        }

       
        private void datagridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            bool[] complete = null;

            if (oDgv != null && formLoaded)
            {
               
               if (frmNumber == 1  || 
                       frmNumber == 3  || 
                       frmNumber == 4  || 
                       frmNumber == 16 || 
                       frmNumber == 17)
               {
                       if (oDgv.CurrentCell != null)
                       {
                           ActiveRow = oDgv.CurrentCell.RowIndex;
                           var CurrentRow = oDgv.CurrentRow;
                           if (CurrentRow != null)
                           {
                               complete = core.RowComplete(CurrentRow, MandatoryFields);
                               
                           }
                           
                           MandSelected = complete; // core.PopulateArray(MandatoryFields.Length, complete);
                       }
                }
               
            }
        }

        private void datagridView_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            bool[] complete = null;
            if (oDgv != null && formLoaded && ActiveRow == oDgv.CurrentCell.RowIndex)
             {
                 var CurrentRow = oDgv.CurrentRow;
                 if (CurrentRow != null)
                 {
                     complete = core.RowComplete(CurrentRow, MandatoryFields);

                 }

                /*
                 int InCorrect = complete.Where(x => x == false).Count();

                 if (InCorrect != MandatoryFields.Count())
                 {
                     MandSelected = complete;

                     var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                     if (!string.IsNullOrEmpty(errorM))
                     {
                         MessageBox.Show(errorM);
                         return;
                     }
                 }
                 */ 
             }
            
        }

        private void dataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null)
                if (dtpObject.Visible)
                    dtpObject.Visible = false;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.Button.ToString() == "Right")
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction" + Environment.NewLine + " This action could possible cause a system abend", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        DataGridViewRow cr = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            int RecNo = Convert.ToInt32(cr.Cells[3].Value.ToString());
                            //------------------------------------------------------------------------------------
                            // First we must ensure that all subordinate records are deleted first
                            //---------------------------------------------------------------------------
                            if (frmNumber == 1)
                            {
                                var locRec = context.TLADM_Styles.Find(RecNo);
                                if (locRec != null)
                                {
                                    context.TLADM_Styles.Remove(locRec);
                                    
                                }
                            }
                            else if (frmNumber == 9)
                            {
                                var locRec = context.TLADM_Labels.Find(RecNo);
                                if (locRec != null)
                                {
                                    context.TLADM_Labels.Remove(locRec);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Delete for the transaction as yet not defined");
                                return;
                            }
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
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }
    }
}

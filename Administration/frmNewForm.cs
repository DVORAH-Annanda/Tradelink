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

namespace Administration
{
    public partial class frmNewForm : Form
    {
        protected readonly TTI2Entities _context;
        int TransNumber;
        Util core;


        DataTable DataT;
        DataColumn column;
        BindingSource BindingSrc;
        bool FormLoaded;
        DateTimePicker dtp;
        public frmNewForm(int TransNo)
        {
            InitializeComponent();

            core = new Util();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToOrderColumns = false;

            DataGridViewTextBoxColumn oTxtBoxA = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxB = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxC = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxD = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxE = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxF = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxG = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxH = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxJ = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxK = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxL = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxM = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxN = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxO = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxP = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxQ = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxR = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxS = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxT = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn oTxtBoxU = new DataGridViewTextBoxColumn();

            DataGridViewCheckBoxColumn oChkBoxB = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxC = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxD = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxE = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxF = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxG = new DataGridViewCheckBoxColumn();
           
            DataGridViewButtonColumn oBtnA = new DataGridViewButtonColumn();
            DataGridViewButtonColumn oBtnB = new DataGridViewButtonColumn();

            DataGridViewComboBoxColumn oCmbBoxA = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxB = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxC = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxD = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxE = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxF = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxG = new DataGridViewComboBoxColumn();

                       
            _context = new TTI2Entities();
            DataT = new DataTable();
            BindingSrc = new BindingSource();

            TransNumber = TransNo;
            if (TransNo == 2)
            {
                this.Text = "Colour Description Update / Edit Facility";
                //0
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Col_Id";
                column.Caption = "Col Id Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //1
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Col_Discontinued";
                column.Caption = "Discontinued";
                column.DefaultValue = false;
                DataT.Columns.Add(column);
                
                //2
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "Col_Description";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);
                //3
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "Col_Number";
                column.Caption = "Colour Number";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);
                //4
                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "Col_StdTimes";
                column.Caption = "Std Times";
                column.DefaultValue = 0.00M;
                DataT.Columns.Add(column);
                //5
                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "Col_CurrentRatio";
                column.Caption = "Current Ratio";
                column.DefaultValue = 0.00M;
                DataT.Columns.Add(column);
                //6
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Col_Benchmark";
                column.Caption = "BenchMark";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //7
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Col_Padding";
                column.Caption = "Padding";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //8
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "Col_Pastel";
                column.Caption = "Pastel No";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                //9
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Col_CostColour";
                column.Caption = "Cost Colour";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //======================================================
                oTxtBoxA.Name = "Col0";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Panel Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].DisplayIndex = 0;

                oChkBoxD.Name = "Col1";
                oChkBoxD.ValueType = typeof(bool);
                oChkBoxD.HeaderText = "Discontinue";
                oChkBoxD.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oChkBoxD);
                dataGridView1.Columns[1].DisplayIndex = 1;

                oTxtBoxB.Name = "Col2";
                oTxtBoxB.ValueType = typeof(string);
                oTxtBoxB.HeaderText = "Colour Description";
                oTxtBoxB.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[2].Visible = true;
                dataGridView1.Columns[2].DisplayIndex = 2;

                oTxtBoxC.Name = "Col3";
                oTxtBoxC.ValueType = typeof(string);
                oTxtBoxC.HeaderText = "Colour Number";
                oTxtBoxC.DataPropertyName = DataT.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[3].DisplayIndex = 3;

                oTxtBoxD.Name = "Col4";
                oTxtBoxD.ValueType = typeof(Decimal);
                oTxtBoxD.HeaderText = "Colour StdTimes";
                oTxtBoxD.DataPropertyName = DataT.Columns[4].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[4].DisplayIndex = 4;

                oTxtBoxE.Name = "Col5";
                oTxtBoxE.ValueType = typeof(Decimal);
                oTxtBoxE.HeaderText = "Colour Current Ratios";
                oTxtBoxE.DataPropertyName = DataT.Columns[5].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxE);
                dataGridView1.Columns[5].DisplayIndex = 5;

                oChkBoxB.Name = "Col6";
                oChkBoxB.ValueType = typeof(bool);
                oChkBoxB.HeaderText = "BenchMark";
                oChkBoxB.DataPropertyName = DataT.Columns[6].ColumnName;
                dataGridView1.Columns.Add(oChkBoxB);
                dataGridView1.Columns[6].DisplayIndex = 6;
                
                oChkBoxC.Name = "Col7";
                oChkBoxC.ValueType = typeof(bool);
                oChkBoxC.HeaderText = "Padding";
                oChkBoxC.DataPropertyName = DataT.Columns[7].ColumnName;
                dataGridView1.Columns.Add(oChkBoxC);
                dataGridView1.Columns[7].DisplayIndex = 7;

                oTxtBoxF.Name = "Col8";
                oTxtBoxF.ValueType = typeof(string);
                oTxtBoxF.HeaderText = "Pastel No";
                oTxtBoxF.DataPropertyName = DataT.Columns[8].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxF);
                dataGridView1.Columns[8].DisplayIndex = 8;

                oChkBoxE.Name = "Col9";
                oChkBoxE.ValueType = typeof(bool);
                oChkBoxE.HeaderText = "Cost Colours";
                oChkBoxE.DataPropertyName = DataT.Columns[9].ColumnName;
                dataGridView1.Columns.Add(oChkBoxE);
                dataGridView1.Columns[9].DisplayIndex = 9;
                
                var Entities = _context.TLADM_Colours.OrderBy(x=>x.Col_Display);
                foreach(var Entity in Entities)
                {
                    DataRow NewRow = DataT.NewRow();
                    NewRow[0] = Entity.Col_Id;
                    NewRow[1] = Entity.Col_Discontinued;
                    NewRow[2] = Entity.Col_Description;
                    NewRow[3] = Entity.Col_FinishedCode;
                    NewRow[4] = Entity.Col_StandardTime;
                    NewRow[5] = Entity.Col_Ratio;
                    NewRow[6] = Entity.Col_Benchmark;
                    NewRow[7] = Entity.Col_Padding;
                    NewRow[8] = Entity.Col_Pastel;
                    NewRow[9] = Entity.Col_ColCosting;
                    DataT.Rows.Add(NewRow);

                }
                /*oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.HeaderText = "Colour Number";


                oTxtBoxE = new DataGridViewTextBoxColumn();
                oTxtBoxE.HeaderText = "Std Times";
                oTxtBoxE.ValueType = typeof(decimal);

                oTxtBoxF = new DataGridViewTextBoxColumn();
                oTxtBoxF.HeaderText = "Current Ratio";
                oTxtBoxF.ValueType = typeof(decimal);

                oChkBoxB = new DataGridViewCheckBoxColumn();
                oChkBoxB.HeaderText = "Benchmark";

                oChkBoxC = new DataGridViewCheckBoxColumn();
                oChkBoxC.HeaderText = "Padding Y/N";

                oChkBoxD = new DataGridViewCheckBoxColumn();
                oChkBoxD.HeaderText = "Measured Colour Y/N";

                dataGridView1.Columns.Add(oTxtBoxD);    //5
                dataGridView1.Columns.Add(oTxtBoxE);    //6
                dataGridView1.Columns.Add(oChkBoxB);    //7
                dataGridView1.Columns.Add(oTxtBoxF);    //8
                dataGridView1.Columns.Add(oChkBoxC);    //9
                dataGridView1.Columns.Add(oChkBoxD);    //10*/

            }
            else if (TransNo == 4)
            {
                /*CREATE TABLE[dbo].[TLADM_Griege](
                TLGreige_Id][int] IDENTITY(1, 1) NOT NULL,
                TLGreige_Description] [varchar](50) NOT NULL,
                TLGreige_OldCode] [int] NULL,
	            TLGreige_PowerN] [int] NOT NULL,
                TLGriege_Discontinued] [bit] NULL,
	            TLGreige_Discontinued_Date] [date] NULL,
	            TLGreige_BarCode] [bit] NOT NULL,
                TLGreige_FabricWeight_FK] [int] NOT NULL,
                TLGreige_FabricWidth_FK] [int] NOT NULL,
                TLGreige_Machine_FK] [int] NOT NULL,
                TLGreige_ProductType_FK] [int] NOT NULL,
                TLGreige_Quality_FK] [int] NOT NULL,
                TLGreige_ROL] [int] NOT NULL,
                TLGreige_ROQ] [int] NOT NULL,
                TLGreige_ShowQty] [bit] NOT NULL,
                TLGreige_StockTakeFreq_FK] [int] NOT NULL,
                TLGreige_UOM_Fk] [int] NOT NULL,
                TLGreige_YarnPowerN] [int] NOT NULL,
                TLGreige_KgPerPiece] [decimal](18, 1) NOT NULL,
                TLGreige_LatestSuggest] [decimal](18, 4) NOT NULL,
                TLGreige_Meters] [int] NOT NULL,
                TLGreige_FaultsAllowed] [int] NOT NULL,
                TLGreige_IsBoughtIn] [bit] NOT NULL,
                TLGreige_IsLining] [bit] NOT NULL,
                TLGreige_CubicWeight] [decimal](18, 4) NOT NULL,
                TLGreige_Body] [bit] NOT NULL,
                */

                this.Text = "Greige Description Update / Edit Facility";
                
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_Id";
                column.Caption = "Greige Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "TLGreige_Description";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_OldCode";
                column.Caption = "Old Code";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_PowerN";
                column.Caption = "Power No";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);


                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "TLGreige_BarCode";
                column.Caption = "BarCode";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "TLGriege_Discontinued";
                column.Caption = "Discontinued";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(DateTime);
                column.ColumnName = "TLGreige_Discontinued_Date";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_FabricWeight_FK";
                column.Caption = "Fabric Weight";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_FabricWidth_FK";
                column.Caption = "Fabric Width";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_Machine_FK";
                column.Caption = "Machine";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_ProductType_FK";
                column.Caption = "Product Type";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_Quality_FK";
                column.Caption = "Quality Type";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_ROL";
                column.Caption = "Repeat Order Limit";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_ROQ";
                column.Caption = "Repeat Order Quantity";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "TLGreige_ShowQty";
                column.Caption = "Show Quantity";
                column.DefaultValue = false;
                DataT.Columns.Add(column); 
                
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_StockTakeFreq_FK";
                column.Caption = "Stock Take Frequency";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_UOM_Fk";
                column.Caption = "Unit Of Measure";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_YarnPowerN";
                column.Caption = "Yarn Power No";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "TLGreige_KgPerPiece";
                column.Caption = "Kgs Per Piece";
                column.DefaultValue = 0.00;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "TLGreige_LatestSuggest";
                column.Caption = "Kgs Per Piece";
                column.DefaultValue = 0.00;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_Meters";
                column.Caption = "Meters";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_FaultsAllowed";
                column.Caption = "No of Faults";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "TLGreige_IsBoughtIn";
                column.Caption = "Bought In";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "TLGreige_IsLining";
                column.Caption = "Lining";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "TLGreige_CubicWeight";
                column.Caption = "Dsk Weight";
                column.DefaultValue = 0.00M;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "TLGreige_Body";
                column.Caption = "Body";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //1 -- 
                //--------------------------------------------
                oTxtBoxA.Name = "Col1";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Panel Key";
                oTxtBoxA.DataPropertyName = DataT.Columns["TLGreige_Id"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns["Col1"].Visible = false;
                dataGridView1.Columns["Col1"].DisplayIndex = 0;
                
                oTxtBoxB.Name = "Col2";
                oTxtBoxB.ValueType = typeof(string);
                oTxtBoxB.HeaderText = "Description";
                oTxtBoxB.DataPropertyName = DataT.Columns["TLGreige_Description"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns["Col2"].DisplayIndex = 1;

                oTxtBoxC.Name = "Col3";
                oTxtBoxC.ValueType = typeof(Int32);
                oTxtBoxC.HeaderText = "Old Code";
                oTxtBoxC.DataPropertyName = DataT.Columns["TLGreige_OldCode"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns["Col3"].DisplayIndex = 2;

                oTxtBoxD.Name = "Col4";
                oTxtBoxD.ValueType = typeof(Int32);
                oTxtBoxD.HeaderText = "Power No";
                oTxtBoxD.DataPropertyName = DataT.Columns["TLGreige_PowerN"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns["Col4"].DisplayIndex = 3;

                oChkBoxB.Name = "Col5";
                oChkBoxB.ValueType = typeof(bool);
                oChkBoxB.HeaderText = "Discontinued";
                oChkBoxB.DataPropertyName = DataT.Columns["TLGriege_Discontinued"].ColumnName;
                dataGridView1.Columns.Add(oChkBoxB);
                dataGridView1.Columns["Col5"].DisplayIndex = 4;
               
                oTxtBoxE.Name = "Col6";
                oTxtBoxE.ValueType = typeof(DateTime);
                oTxtBoxE.HeaderText = "Discontinued Date";
                oTxtBoxE.DataPropertyName = DataT.Columns["TLGreige_Discontinued_Date"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxE);
                dataGridView1.Columns["Col6"].DisplayIndex = 5;

                oChkBoxC.Name = "Col7";
                oChkBoxC.ValueType = typeof(bool);
                oChkBoxC.HeaderText = "Barcode";
                oChkBoxC.DataPropertyName = DataT.Columns["TLGriege_Barcode"].ColumnName;
                dataGridView1.Columns.Add(oChkBoxC);
                dataGridView1.Columns["Col7"].DisplayIndex = 6;

                oCmbBoxA.Name = "Col8";
                oCmbBoxA.HeaderText = "Fabric Weight";
                oCmbBoxA.DataPropertyName = DataT.Columns["TLGreige_FabricWeight_FK"].ColumnName;
                oCmbBoxA.DataSource = _context.TLADM_FabricWeight.ToList();
                oCmbBoxA.ValueMember = "FWW_ID";
                oCmbBoxA.DisplayMember = "FWW_Descriptiion";
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns["Col8"].DisplayIndex = 7;

                oCmbBoxB.Name = "Col9";
                oCmbBoxB.HeaderText = "Fabric Width";
                oCmbBoxB.DataPropertyName = DataT.Columns["TLGreige_FabricWidth_FK"].ColumnName;
                oCmbBoxB.DataSource = _context.TLADM_FabWidth.ToList();
                oCmbBoxB.ValueMember = "FW_ID";
                oCmbBoxB.DisplayMember = "FW_Descriptiion";
                dataGridView1.Columns.Add(oCmbBoxB);
                dataGridView1.Columns["Col9"].DisplayIndex = 8;

                oCmbBoxC.Name = "Col10";
                oCmbBoxC.HeaderText = "Machine";
                oCmbBoxC.DataPropertyName = DataT.Columns["TLGreige_Machine_FK"].ColumnName;
                oCmbBoxC.DataSource = _context.TLADM_MachineDefinitions.Where(x=>x.MD_Department_FK == 11).ToList();
                oCmbBoxC.ValueMember = "MD_PK";
                oCmbBoxC.DisplayMember = "MD_Description";
                dataGridView1.Columns.Add(oCmbBoxC);
                dataGridView1.Columns["Col10"].DisplayIndex = 9;

                oCmbBoxD.Name = "Col11";
                oCmbBoxD.HeaderText = "Product Type";
                oCmbBoxD.DataSource = _context.TLADM_ProductTypes.ToList();
                oCmbBoxD.ValueMember = "PT_Pk";
                oCmbBoxD.DisplayMember = "PT_Description";
                oCmbBoxD.DataPropertyName = DataT.Columns["TLGreige_ProductType_FK"].ColumnName;
                dataGridView1.Columns.Add(oCmbBoxD);
                dataGridView1.Columns["Col11"].DisplayIndex = 10;

                oCmbBoxE.Name = "Col12";
                oCmbBoxE.HeaderText = "Quality";
                oCmbBoxE.DataSource = _context.TLADM_Griege.ToList();
                oCmbBoxE.ValueMember = "TLGreige_Id";
                oCmbBoxE.DisplayMember = "TlGreige_Description";
                oCmbBoxE.DataPropertyName = DataT.Columns["TLGreige_Quality_FK"].ColumnName;
                dataGridView1.Columns.Add(oCmbBoxE);
                dataGridView1.Columns["Col12"].DisplayIndex = 11;

                oTxtBoxL.Name = "Col13";
                oTxtBoxL.ValueType = typeof(int);
                oTxtBoxL.HeaderText = "ROL";
                oTxtBoxL.DataPropertyName = DataT.Columns["TLGreige_ROL"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxL);
                dataGridView1.Columns["Col13"].DisplayIndex = 12;

                oTxtBoxM.Name = "Col14";
                oTxtBoxM.ValueType = typeof(int);
                oTxtBoxM.HeaderText = "ROQ";
                oTxtBoxM.DataPropertyName = DataT.Columns["TLGreige_ROQ"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxM);
                dataGridView1.Columns["Col14"].DisplayIndex = 13;

                oChkBoxD.Name = "Col15";
                oChkBoxD.ValueType = typeof(bool);
                oChkBoxD.HeaderText = "Show Qty";
                oChkBoxD.DataPropertyName = DataT.Columns["TLGreige_ShowQty"].ColumnName;
                dataGridView1.Columns.Add(oChkBoxD);
                dataGridView1.Columns["Col15"].DisplayIndex = 14;

                oCmbBoxF.Name = "Col16";
                oCmbBoxF.HeaderText = "Stock Take Freq";
                oCmbBoxF.DataPropertyName = DataT.Columns["TLGreige_StockTakeFreq"].ColumnName;
                oCmbBoxF.DataSource = _context.TLADM_StockTakeFreq.ToList();
                oCmbBoxF.ValueMember = "STF_Pk";
                oCmbBoxF.DisplayMember = "STF_Description";
                dataGridView1.Columns.Add(oCmbBoxF);
                dataGridView1.Columns["Col14"].DisplayIndex = 15;

                oCmbBoxG.Name = "Col17";
                oCmbBoxG.HeaderText = "UOM";
                oCmbBoxG.DataSource = _context.TLADM_UOM.ToList();
                oCmbBoxG.ValueMember = "UOM_Pk";
                oCmbBoxG.DisplayMember = "UOM_Description";
                oCmbBoxG.DataPropertyName = DataT.Columns["TLGreige_UOM_Fk"].ColumnName;
                dataGridView1.Columns.Add(oCmbBoxG);
                dataGridView1.Columns["Col17"].DisplayIndex = 16;

                oBtnA.Name = "Col18";
                oBtnA.HeaderText = "Yarn Power No";
                oBtnA.DataPropertyName = DataT.Columns["TLGreige_YarnPowerN"].ColumnName;
                dataGridView1.Columns.Add(oBtnA);
                dataGridView1.Columns["Col18"].DisplayIndex = 17;

                oTxtBoxQ.Name = "Col19";
                oTxtBoxQ.ValueType = typeof(decimal);
                oTxtBoxQ.HeaderText = "Kg Per Piece";
                oTxtBoxQ.DataPropertyName = DataT.Columns["TLGreige_KgPerPiece"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxQ);
                dataGridView1.Columns["Col19"].DisplayIndex = 18;

                oTxtBoxR.Name = "Col20";
                oTxtBoxR.ValueType = typeof(decimal);
                oTxtBoxR.HeaderText = "Latest Suggested";
                oTxtBoxR.DataPropertyName = DataT.Columns["TLGreige_LatestSuggest"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxR);
                dataGridView1.Columns["Col20"].DisplayIndex = 19;

                oTxtBoxS.Name = "Col21";
                oTxtBoxS.ValueType = typeof(decimal);
                oTxtBoxS.HeaderText = "Latest Suggested";
                oTxtBoxS.DataPropertyName = DataT.Columns["TLGreige_Meters"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxS);
                dataGridView1.Columns["Col21"].DisplayIndex = 20;

                oTxtBoxT.Name = "Col22";
                oTxtBoxT.ValueType = typeof(decimal);
                oTxtBoxT.HeaderText = "Faults Allowed";
                oTxtBoxT.DataPropertyName = DataT.Columns["TLGreige_FaultsAllowed"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxT);
                dataGridView1.Columns["Col22"].DisplayIndex = 21;

                oChkBoxE.Name = "Col23";
                oChkBoxE.ValueType = typeof(bool);
                oChkBoxE.HeaderText = "Bought In";
                oChkBoxE.DataPropertyName = DataT.Columns["TLGreige_IsBoughtIn"].ColumnName;
                dataGridView1.Columns.Add(oChkBoxE);
                dataGridView1.Columns["Col23"].DisplayIndex = 22;

                oChkBoxF.Name = "Col24";
                oChkBoxF.ValueType = typeof(bool);
                oChkBoxF.HeaderText = "Lining";
                oChkBoxF.DataPropertyName = DataT.Columns["TLGreige_IsLining"].ColumnName;
                dataGridView1.Columns.Add(oChkBoxF);
                dataGridView1.Columns["Col24"].DisplayIndex = 23;

                oTxtBoxU.Name = "Col25";
                oTxtBoxU.ValueType = typeof(decimal);
                oTxtBoxU.HeaderText = "Dsk Weight";
                oTxtBoxU.DataPropertyName = DataT.Columns["TLGreige_CubicWeight"].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxU);
                dataGridView1.Columns["Col25"].DisplayIndex = 24;

                oChkBoxG.Name = "Col26";
                oChkBoxG.ValueType = typeof(bool);
                oChkBoxG.HeaderText = "Body";
                oChkBoxG.DataPropertyName = DataT.Columns["TLGreige_Body"].ColumnName;
                dataGridView1.Columns.Add(oChkBoxG);
                dataGridView1.Columns["Col26"].DisplayIndex = 25;

                oBtnB.Name = "Col27";
                oBtnB.HeaderText = "Colour";
                dataGridView1.Columns.Add(oBtnB);
                dataGridView1.Columns["Col27"].DisplayIndex = 26;

                var Entities = _context.TLADM_Griege.ToList();
                /*CREATE TABLE[dbo].[TLADM_Griege](
               TLGreige_Id][int] IDENTITY(1, 1) NOT NULL,
               TLGreige_Description] [varchar](50) NOT NULL,
               TLGreige_OldCode] [int] NULL,
               TLGreige_PowerN] [int] NOT NULL,
               TLGriege_Discontinued] [bit] NULL,
               TLGreige_Discontinued_Date] [date] NULL,
               TLGreige_BarCode] [bit] NOT NULL,
               TLGreige_FabricWeight_FK] [int] NOT NULL,
               TLGreige_FabricWidth_FK] [int] NOT NULL,
               TLGreige_Machine_FK] [int] NOT NULL,
               TLGreige_ProductType_FK] [int] NOT NULL,
               TLGreige_Quality_FK] [int] NOT NULL,
               TLGreige_ROL] [int] NOT NULL,
               TLGreige_ROQ] [int] NOT NULL,
               TLGreige_ShowQty] [bit] NOT NULL,
               TLGreige_StockTakeFreq_FK] [int] NOT NULL,
               TLGreige_UOM_Fk] [int] NOT NULL,
               TLGreige_YarnPowerN] [int] NOT NULL,
               TLGreige_KgPerPiece] [decimal](18, 1) NOT NULL,
               TLGreige_LatestSuggest] [decimal](18, 4) NOT NULL,
               TLGreige_Meters] [int] NOT NULL,
               TLGreige_FaultsAllowed] [int] NOT NULL,
               TLGreige_IsBoughtIn] [bit] NOT NULL,
               TLGreige_IsLining] [bit] NOT NULL,
               TLGreige_CubicWeight] [decimal](18, 4) NOT NULL,
               TLGreige_Body] [bit] NOT NULL,
               */
                foreach ( var Entity in Entities)
                {
                    DataRow NewRow = DataT.NewRow();
                    NewRow["TLGreige_Id"] = Entity.TLGreige_Id;
                    NewRow["TLGreige_Description"] = Entity.TLGreige_Description;
                    NewRow["TLGreige_OldCode"] = Entity.TLGreige_OldCode;
                    NewRow["TLGreige_PowerN"] = Entity.TLGreige_PowerN;
                    NewRow["TLGriege_Discontinued"] = Entity.TLGriege_Discontinued;
                    NewRow["TLGreige_Discontinued_Date"] = Entity.TLGreige_Discontinued_Date;
                    NewRow["TLGreige_BarCode"] = Entity.TLGreige_BarCode;
                    NewRow["TLGreige_FabricWeight_FK"] = Entity.TLGreige_FabricWeight_FK;
                    NewRow["TLGreige_FabricWidth_FK"] = Entity.TLGreige_FabricWidth_FK;
                    NewRow["TLGreige_Machine_FK"] = Entity.TLGreige_Machine_FK;
                    NewRow["TLGreige_ProductType_FK"] = Entity.TLGreige_ProductType_FK;
                    NewRow["TLGreige_Quality_FK"] = Entity.TLGreige_Quality_FK;
                    NewRow["TLGreige_ROL"] = Entity.TLGreige_ROL;
                    NewRow["TLGreige_ROQ"] = Entity.TLGreige_ROQ;
                    NewRow["TLGreige_ShowQty"] = Entity.TLGreige_ShowQty;
                    NewRow["TLGreige_StockTakeFreq_FK"] = Entity.TLGreige_StockTakeFreq_FK;
                    NewRow["TLGreige_UOM_Fk"] = Entity.TLGreige_UOM_Fk;
                    NewRow["TLGreige_YarnPowerN"] = Entity.TLGreige_YarnPowerN;
                    NewRow["TLGreige_KgPerPiece"] = Entity.TLGreige_KgPerPiece;
                    NewRow["TLGreige_LatestSuggest"] = Entity.TLGreige_LatestSuggest;
                    NewRow["TLGreige_Meters"] = Entity.TLGreige_Meters;
                    NewRow["TLGreige_FaultsAllowed"] = Entity.TLGreige_FaultsAllowed;
                    NewRow["TLGreige_IsBoughtIn"] = Entity.TLGreige_IsBoughtIn;
                    NewRow["TLGreige_IsLining"] = Entity.TLGreige_IsLining;
                    NewRow["TLGreige_CubicWeight"] = Entity.TLGreige_CubicWeight;
                    NewRow["TLGreige_Body"] = Entity.TLGreige_Body;

                    DataT.Rows.Add(NewRow);

                }
            }
            else if(TransNo == 12)
            {
                this.Text = "Product Types";

                /*
                [PT_pk][int] IDENTITY(1, 1) NOT NULL,
                [PT_ShortCode] [varchar](5) NOT NULL,
                [PT_Description] [varchar](50) NOT NULL,
                [PT_UOMFk] [int] NOT NULL,
                [PT_StdCost] [bit] NOT NULL,
                [PT_Hazardous] [bit] NOT NULL,
                [PT_StdCostValue] [money] NOT NULL,
                 */

                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "ProductType_Pk";
                column.Caption = "ProductType Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                //----------------------------------------------
                // Col1 
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "PTShortCode";
                column.Caption = "ShortCode";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);
                //----------------------------------------------
                // Col1 
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "PTDescrip";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "PTUOM_FK";
                column.Caption = "Unit Of Measure";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "PTSTDCost";
                column.Caption = "Standard Cost";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "PTHazadous";
                column.Caption = "Hazadous";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "PTSTDCost_Value";
                column.Caption = "Standard Cost Value";
                column.DefaultValue = 0.00M;
                DataT.Columns.Add(column);

                //--------------------------------------------
                oTxtBoxA.Name = "ProductType_PK";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Product Type Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns["ProductType_PK"].Visible = false;

                oTxtBoxB.Name = "ProductType_SC";
                oTxtBoxB.ValueType = typeof(String);
                oTxtBoxB.HeaderText = "Product Short Code";
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;
              
                oTxtBoxC.Name = "ProductType_Desc";
                oTxtBoxC.ValueType = typeof(String);
                oTxtBoxC.HeaderText = "Product Description";
                oTxtBoxC.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[2].DisplayIndex = 2;

                oCmbBoxA.Name = "ProductType_UOM";
                oCmbBoxA.ValueType = typeof(int);
                oCmbBoxA.HeaderText = "UOM";
                oCmbBoxA.DataPropertyName = DataT.Columns[3].ColumnName;
                oCmbBoxA.DataSource = _context.TLADM_UOM.ToList();
                oCmbBoxA.ValueMember = "UOM_Pk";
                oCmbBoxA.DisplayMember = "UOM_Description";
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns[3].DisplayIndex = 3;
             
                oChkBoxB.Name = "ProductType_StanCost";
                oChkBoxB.HeaderText = "Use Std Cost";
                oChkBoxB.ValueType = typeof(bool);
                oChkBoxB.DataPropertyName = DataT.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oChkBoxB);
                dataGridView1.Columns[4].DisplayIndex = 4;

                oChkBoxC.Name = "ProductType_Haz";
                oChkBoxC.HeaderText = "Hazadous";
                oChkBoxC.ValueType = typeof(bool);
                oChkBoxC.DataPropertyName = DataT.Columns[5].ColumnName;
                dataGridView1.Columns.Add(oChkBoxC);
                dataGridView1.Columns[5].DisplayIndex = 5;

                oTxtBoxD.Name = "ProductType_StdCostValue";
                oTxtBoxD.HeaderText = "Std Cost Value";
                oTxtBoxD.ValueType = typeof(decimal);
                oTxtBoxD.DataPropertyName = DataT.Columns[6].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[6].DisplayIndex = 6;

                var Existing = _context.TLADM_ProductTypes.ToList();

                foreach(var Record in Existing)
                {
                    DataRow NewRow = DataT.NewRow();
                    NewRow[0] = Record.PT_pk;
                    NewRow[1] = Record.PT_ShortCode;
                    NewRow[2] = Record.PT_Description;
                    NewRow[3] = Record.PT_UOMFk;
                    NewRow[4] = Record.PT_StdCost;
                    NewRow[5] = Record.PT_Hazardous;
                    NewRow[6] = Record.PT_StdCostValue;

                    DataT.Rows.Add(NewRow);
                }


            }
            else if(TransNo == 17)
            {
                this.Text = "Panel Stock";
             
                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Panel_Pk";
                column.Caption = "Panel Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                DataT.PrimaryKey = new DataColumn[] { DataT.Columns[0] };

                //----------------------------------------------
                // Col1 
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "Description_Pk";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);
            
                //--------------------------------------------------------
                // Col 2
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Discontinued_Pk";
                column.Caption = "Discontinued";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //--------------------------------------------------------
                // Col 3
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(DateTime);
                column.ColumnName = "DateDiscontinued";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                //-----------------------
                // Col 4
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Size_PN";
                column.Caption = "Size Power No";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //-----------------------
                // Col 5
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Show_Qty";
                column.Caption = "Show Qty";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //------------------------------
                // Col 6
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "SingleColour";
                column.Caption = "Single Colour";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //-----------------------------------------------------
                // col 7
                //-------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "SingleColour_Pk";
                column.Caption = "Single Colour";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                //-----------------------------------------------------
                // col 8
                //-------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Style_Pk";
                column.Caption = "Style";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //1 -- 
                //--------------------------------------------
                oTxtBoxA.Name = "Panel_PK";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Panel Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns["Panel_PK"].Visible = false;

                //---- 1 
                oTxtBoxB.Name = "Panel_PK";
                oTxtBoxB.ValueType = typeof(String);
                oTxtBoxB.HeaderText = "Description";
                oTxtBoxB.Visible = true;
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;

                //2 -- Open / Closed 
                //------------------------------------------------

                oChkBoxB.Name = "Discontinued";
                oChkBoxB.HeaderText = "Discontinued";
                oChkBoxB.DataPropertyName = DataT.Columns[2].ColumnName;
                oChkBoxB.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkBoxB);
                dataGridView1.Columns[2].DisplayIndex = 2;

                //3 -- Column to Date Discontinued
                //----------------------------------------------
                oTxtBoxC = new DataGridViewTextBoxColumn();
                oTxtBoxC.Visible = true;
                oTxtBoxC.HeaderText = "Date Discontinued";
                oTxtBoxC.DataPropertyName = DataT.Columns[3].ColumnName;
                oTxtBoxC.Width = 100;
                oTxtBoxC.ValueType = typeof(DateTime);
                oTxtBoxC.DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns.Add(oTxtBoxC);
                
                
                oBtnA.Name = "PowerN";
                oBtnA.ValueType = typeof(Int32);
                oBtnA.DataPropertyName = DataT.Columns[4].ColumnName;
                oBtnA.HeaderText = "Sizes";
                dataGridView1.Columns.Add(oBtnA);
                dataGridView1.Columns[3].DisplayIndex = 3;

                //4 -- Show Quantity
                //----------------------------------------------
                oChkBoxC.Name = "Panel_ShowQty";
                oChkBoxC.HeaderText = "Show Qty";
                oChkBoxC.ValueType = typeof(Boolean);
                oChkBoxC.DataPropertyName = DataT.Columns[5].ColumnName;
                dataGridView1.Columns.Add(oChkBoxC);
                dataGridView1.Columns[4].DisplayIndex = 4;

                //5 -- is it a Single Colour
                //----------------------------------------------
                oChkBoxD.Name = "Single_Clr";
                oChkBoxD.HeaderText = "Single Colour";
                oChkBoxD.ValueType = typeof(Boolean);
                oChkBoxD.DataPropertyName = DataT.Columns[6].ColumnName;
                dataGridView1.Columns.Add(oChkBoxD);
                dataGridView1.Columns[5].DisplayIndex = 5;

                // 6 -- Colours
                //--------------------------------------------------------
                oCmbBoxA.Name = "SingColours";
                oCmbBoxA.HeaderText = "Colours";
                oCmbBoxA.ValueType = typeof(Int32);
                oCmbBoxA.DataSource = _context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).ToList();
                oCmbBoxA.DisplayMember = "Col_Display";
                oCmbBoxA.ValueMember = "Col_Id";
                oCmbBoxA.DataPropertyName = DataT.Columns[7].ColumnName;
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns[6].DisplayIndex = 6;

                //7 -- Styles
                //--------------------------------------------------------
                oCmbBoxB.Name = "Styles";
                oCmbBoxB.HeaderText = "Styles";
                oCmbBoxB.ValueType = typeof(Int32);
                oCmbBoxB.DataSource = _context.TLADM_Styles.Where(x => !(bool)x.Sty_Discontinued).ToList();
                oCmbBoxB.DisplayMember = "Sty_Description";
                oCmbBoxB.ValueMember = "Sty_Id";
                oCmbBoxB.DataPropertyName = DataT.Columns[8].ColumnName;
                dataGridView1.Columns.Add(oCmbBoxB);
                dataGridView1.Columns[7].DisplayIndex = 7;

                var Entities = _context.TLADM_PanelAttributes.OrderBy(x => x.Pan_Description).ToList();
                foreach (var Entity in Entities)
                {
                     DataRow NRow = DataT.NewRow();
                     NRow[0] = Entity.Pan_PK;
                     NRow[1] = Entity.Pan_Description;
                     NRow[2] = Entity.Pan_Discontinued;
                    if (Entity.Pan_Discontinued_Date != null)
                    {
                        NRow[3] = Entity.Pan_Discontinued_Date;
                    }
                    else
                    {
                        NRow[3] = DBNull.Value;
                    }
                    NRow[4] = Entity.Pan_PowerN;
                    NRow[5] = Entity.Pan_ShowQty;
                    NRow[6] = Entity.Pan_Single_Colour;
                    if (Entity.Pan_Single_Colour_FK != null)
                        NRow[7] = Entity.Pan_Single_Colour_FK;
                    else
                        NRow[7] = DBNull.Value;

                    NRow[8] = Entity.Pan_Style_FK;

                    DataT.Rows.Add(NRow);
                }
                

            }

            BindingSrc.DataSource = DataT;
            dataGridView1.DataSource = BindingSrc;



        }

        private void frmNewForm_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            if (TransNumber == 17)
            {
                /*
               [Pan_PK] [int] IDENTITY(1,1) NOT NULL,
               [Pan_Description] [varchar](50) NOT NULL,
               [Pan_Discontinued] [bit] NULL,
               [Pan_Discontinued_Date] [date] NULL,
               [Pan_PowerN] [int] NOT NULL,
               [Pan_ShowQty] [bit] NOT NULL,
               [Pan_Single_Colour] [bit] NOT NULL,
               [Pan_Single_Colour_FK] [int] NULL,
               [Pan_Style_FK] [int] NULL,
              */


            }

            FormLoaded = true;

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var oDlg = (DataGridView)sender;
            if (oDlg != null && FormLoaded)
            {
                if (TransNumber == 2)
                {
                    if (oDlg.Focused && oDlg.CurrentCell is DataGridViewTextBoxCell)
                    {
                        var Cell = oDlg.CurrentCell;

                        if (Cell.ColumnIndex == 3)
                        {
                            e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                            e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                            e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                            e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                        }
                        else if (Cell.ColumnIndex == 4 || Cell.ColumnIndex == 5)
                        {
                            e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                            e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                            e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                            e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                        }
                    }
                }
                else if (TransNumber == 4)
                {
                }
                else if (TransNumber == 12)
                {
                }
                else if (TransNumber == 17)
                {
                    if (oDlg.Focused && oDlg.CurrentCell is DataGridViewTextBoxCell)
                    {

                    }
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var SGrid = (DataGridView)sender;
            int PN = 0;
            

            if (SGrid != null && SGrid.Focused && FormLoaded)
            {
                if (TransNumber == 17)
                {
                    if (e.ColumnIndex == 2)
                    {
                        var ISchecked = (bool)SGrid.CurrentCell.EditedFormattedValue;

                        //TODO - Button Clicked - Execute Code Here
                        //===============================================
                        if (ISchecked)
                        {
                            SGrid.Columns[e.ColumnIndex].DefaultCellStyle.Format = "dd/MM/yyyy";
                            dtp = new DateTimePicker();
                            dtp.Value = DateTime.Now;

                            dataGridView1.Controls.Add(dtp);
                            dtp.Format = DateTimePickerFormat.Short;
                            Rectangle Rectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex + 1, e.RowIndex, true);
                            dtp.Size = new Size(Rectangle.Width, Rectangle.Height);
                            dtp.Location = new Point(Rectangle.X, Rectangle.Y);

                            dtp.CloseUp += new EventHandler(dtp_CloseUp);
                            dtp.TextChanged += new EventHandler(dtp_OnTextChange);

                            dtp.Visible = true;
                        }
                        else
                        {
                            var DataRow = DataT.Rows.Find(e.RowIndex + 1);
                            if(DataRow != null)
                            {
                                DataRow[e.ColumnIndex] = false;
                                // DataRow[e.ColumnIndex + 1] = DBNull.Value;
                                SGrid.Rows[e.RowIndex].Cells[e.ColumnIndex+1].Value = DBNull.Value ;
                                dtp_CloseUp(this, null);
                            }
                       }
                    }
                    else if (e.ColumnIndex == 4)
                    {
                        PN = Convert.ToInt32(SGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

                        TTI2_WF.frmTLADMGardProp apropAdditional = new TTI2_WF.frmTLADMGardProp(1005, PN);
                        apropAdditional.ShowDialog(this);

                        SGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = apropAdditional.TotalPN;
                    }
                }

            }
        }

        private void dtp_OnTextChange(object sender, EventArgs e)
        {
            var CurrentRow = dataGridView1.CurrentRow;
            CurrentRow.Cells[3].Value = dtp.Value;
            if(CurrentRow.Cells[3].Value != null && CurrentRow.Cells[2].Value == null)
            {
                FormLoaded = false;
                CurrentRow.Cells[2].Value = true;
                FormLoaded = true;
            }
        }

        void dtp_CloseUp(object sender, EventArgs e)
        {
            if(dtp != null)
                dtp.Visible = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool Add;
            bool lSave = false;

            if(oBtn != null && FormLoaded)
            {
                if (TransNumber == 2)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;

                        TLADM_Colours Clrs = new TLADM_Colours();

                        if (Row.Field<int>(0) != 0)
                        {
                            Clrs = _context.TLADM_Colours.Find(Row.Field<int>(0));
                            if (!lSave)
                                lSave = true;
                        }
                        else
                        {
                            Add = true;
                            if (!lSave)
                                lSave = true;
                        }

                        Clrs.Col_Discontinued = Row.Field<bool>(1);
                        Clrs.Col_Description = Row.Field<string>(2);
                        Clrs.Col_FinishedCode = Row.Field<string>(3);
                        Clrs.Col_StandardTime = Row.Field<decimal>(4);
                        Clrs.Col_Ratio = Row.Field<decimal>(5);
                        Clrs.Col_Benchmark = Row.Field<bool>(6);
                        Clrs.Col_Padding = Row.Field<bool>(7);
                        Clrs.Col_Pastel = Row.Field<string>(8);
                        Clrs.Col_ColCosting = Row.Field<bool>(9);

                        if(Clrs.Col_Display != Clrs.Col_FinishedCode + " " + Clrs.Col_Description)
                        {
                            Clrs.Col_Display = Clrs.Col_FinishedCode + " " + Clrs.Col_Description; 
                        }
                       
                        if(Add)
                        {
                            _context.TLADM_Colours.Add(Clrs);
                        }

                    }
                }
                else if (TransNumber == 4)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;

                        TLADM_Griege Gr = new TLADM_Griege();
                        if (Row.Field<int>(0) != 0)
                        {
                            Gr = _context.TLADM_Griege.Find(Row.Field<int>(0));
                            if (!lSave)
                                lSave = true;
                        }
                        else
                        {
                            Add = true;
                            if (!lSave)
                                lSave = true;
                        }

                        Gr.TLGreige_Description = Row.Field<String>("TLGreige_Description");
                        Gr.TLGreige_OldCode = Row.Field<int>("TLGreige_OldCode");
                        Gr.TLGreige_PowerN = Row.Field<int>("TLGreige_PowerN");
                        Gr.TLGriege_Discontinued = Row.Field<bool>("TLGriege_Discontinued");
                        Gr.TLGreige_Discontinued_Date = Row.Field<DateTime>("TLGreige_Discontinued_Date");
                        Gr.TLGreige_BarCode = Row.Field<bool>("TLGreige_BarCode");
                        Gr.TLGreige_FabricWeight_FK = Row.Field<int>("TLGreige_FabricWeight_FK");
                        Gr.TLGreige_FabricWidth_FK = Row.Field<int>("TLGreige_FabricWidth_FK");
                        Gr.TLGreige_Machine_FK = Row.Field<int>("TLGreige_Machine_FK");
                        Gr.TLGreige_ProductType_FK = Row.Field<int>("TLGreige_ProductType_FK");
                        Gr.TLGreige_Quality_FK = Row.Field<int>("TLGreige_Quality_FK");
                        Gr.TLGreige_ROL = Row.Field<int>("TLGreige_ROL");
                        Gr.TLGreige_ROQ = Row.Field<int>("TLGreige_ROQ");
                        Gr.TLGreige_ShowQty = Row.Field<bool>("TLGreige_ShowQty");
                        Gr.TLGreige_StockTakeFreq_FK = Row.Field<int>("TLGreige_StockTakeFreq_FK");
                        Gr.TLGreige_UOM_Fk = Row.Field<int>("TLGreige_UOM_Fk");
                        Gr.TLGreige_YarnPowerN = Row.Field<int>("TLGreige_YarnPowerN");
                        Gr.TLGreige_KgPerPiece = Row.Field<decimal>("TLGreige_KgPerPiece");
                        Gr.TLGreige_LatestSuggest = Row.Field<decimal>("TLGreige_LatestSuggest");
                        Gr.TLGreige_Meters = Row.Field<int>("TLGreige_Meters");
                        Gr.TLGreige_FaultsAllowed = Row.Field<int>("TLGreige_FaultsAllowed");
                        Gr.TLGreige_IsBoughtIn = Row.Field<bool>("TLGreige_IsBoughtIn");
                        Gr.TLGreige_IsLining = Row.Field<bool>("TLGreige_IsLining");
                        Gr.TLGreige_CubicWeight = Row.Field<decimal>("TLGreige_CubicWeight");
                        Gr.TLGreige_Body = Row.Field<bool>("TLGreige_Body");

                        if (Add)
                        {
                            _context.TLADM_Griege.Add(Gr);
                        }
                    }
                }
                else if (TransNumber == 12)
                {
                    foreach (DataRow PTRow in DataT.Rows)
                    {
                        Add = false;

                        TLADM_ProductTypes Types = new TLADM_ProductTypes();
                        if (PTRow.Field<int>(0) != 0)
                        {
                            Types = _context.TLADM_ProductTypes.Find(PTRow.Field<int>(0));
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            if (!lSave)
                            {
                                lSave = true;
                            }

                            Add = true;
                        }

                        Types.PT_ShortCode = PTRow.Field<String>(1);
                        Types.PT_Description = PTRow.Field<string>(2);
                        Types.PT_UOMFk = PTRow.Field<int>(3);
                        Types.PT_StdCost = PTRow.Field<bool>(4);
                        Types.PT_Hazardous = PTRow.Field<bool>(5);
                        Types.PT_StdCostValue = PTRow.Field<decimal>(6);

                        if (Add)
                        {
                            DataT.Rows.Add(Types);
                        }
                    }
                }
                else if (TransNumber == 17)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;

                        TLADM_PanelAttributes PanAttrib = new TLADM_PanelAttributes();
                        if (Row.Field<int>(0) != 0)
                        {
                            PanAttrib = _context.TLADM_PanelAttributes.Find(Row.Field<int>(0));
                            if (!lSave)
                                lSave = true;
                        }
                        else
                        {
                            Add = true;
                            if (!lSave)
                                lSave = true;
                        }

                        PanAttrib.Pan_Description = Row.Field<string>(1);
                        PanAttrib.Pan_Discontinued = Row.Field<bool>(2);
                        if (Row[3] != DBNull.Value)
                        {
                            PanAttrib.Pan_Discontinued_Date = Row.Field<DateTime>(3);
                        }
                        else
                        {
                            PanAttrib.Pan_Discontinued_Date = null;
                        }
                        PanAttrib.Pan_PowerN = Row.Field<int>(4);
                        PanAttrib.Pan_ShowQty = Row.Field<bool>(5);
                        PanAttrib.Pan_Single_Colour = Row.Field<bool>(6);
                        if (Row[7] != DBNull.Value)
                        {
                            PanAttrib.Pan_Single_Colour_FK = Row.Field<int>(7);
                        }
                        else
                        {
                            PanAttrib.Pan_Single_Colour_FK = null;
                        }

                        PanAttrib.Pan_Style_FK = Row.Field<int>(8);

                        if (Add)
                        {
                            _context.TLADM_PanelAttributes.Add(PanAttrib);
                        }
                    }
                }

                if (lSave)
                {
                    try
                    {
                        _context.SaveChanges();
                        MessageBox.Show("Data successfully saved to data base");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }
                }
            }
        }

        private void frmNewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        
    }
}

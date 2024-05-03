using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Administration;
using Utilities;
using System.IO;
using System.Threading;
using LinqKit;
using EntityFramework.Extensions;
using Microsoft.Office.Interop;
using System.IO.Pipes;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.OleDb;
using System.Linq.Expressions;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Office.Interop.Excel;
using static log4net.Appender.ColoredConsoleAppender;
using System.Security.Policy;
using System.Runtime.Remoting.Contexts;
using System.CodeDom;

namespace Administration
{
    public partial class frmNewForm : Form
    {
        protected readonly TTI2Entities _context;
        int TransNumber;
        Util core;


        System.Data.DataTable DataT;
        DataColumn column;
        BindingSource BindingSrc;
        bool FormLoaded;
        DateTimePicker dtp;
        DateTimePicker dateTimePicker1;

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

            DataGridViewCheckBoxColumn oChkBoxA = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxB = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxC = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxD = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxE = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxF = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxG = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxH = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn oChkBoxJ = new DataGridViewCheckBoxColumn();

            DataGridViewButtonColumn oBtnA = new DataGridViewButtonColumn();
            DataGridViewButtonColumn oBtnB = new DataGridViewButtonColumn();
            DataGridViewButtonColumn oBtnC = new DataGridViewButtonColumn();
            DataGridViewButtonColumn oBtnD = new DataGridViewButtonColumn();
            DataGridViewButtonColumn oBtnE = new DataGridViewButtonColumn();

            DataGridViewComboBoxColumn oCmbBoxA = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxB = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxC = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxD = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxE = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxF = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxColumn oCmbBoxG = new DataGridViewComboBoxColumn();

            DateTimePicker oDateTimePicker = new DateTimePicker();
            DateTimePicker oDateTimePicker1 = new DateTimePicker();

            _context = new TTI2Entities();
            DataT = new System.Data.DataTable();
            BindingSrc = new BindingSource();
            label1.Visible = false;
            cmboCustomers.Visible = false;
            groupBox1.Visible = false;

            TransNumber = TransNo;
            if (TransNo == 1)
            {
                this.Text = "Styles Update / Edit Facility";

                //00
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Col_Id";
                column.Caption = "Col Id Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //01
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "Col_Description";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                //02
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Col_Discontinued";
                column.Caption = "Discontinued";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //03
                column = new DataColumn();
                column.DataType = typeof(DateTime);
                column.ColumnName = "TLGreige_Discontinued_Date";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                //----------------------------------------------------
                //columns, 4,5,6,7.....in the datagridview1  
                // as they are buttons and the data is stored in another table
                //------------------------------------------------------------------
                //04
                //--------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Check_Mandatory";
                column.Caption = "Check Mandatory";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //05
                //-------------------------------
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Pastel_Number";
                column.Caption = "Pastel Number";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //06
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "Pastel_Code";
                column.Caption = "Pastel Code";
                column.DefaultValue = String.Empty;
                DataT.Columns.Add(column);

                //07
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Cotton_Factor";
                column.Caption = "Cotton Factor";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //08
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Sty_Bags";
                column.Caption = "Style Bags";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //09
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Sty_Buttons";
                column.Caption = "Style Buttons";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //10
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Sty_BoughtIn";
                column.Caption = "Bought In";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //11
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Sty_Equiv";
                column.Caption = "Style Equiv";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //12
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Sty_DisplayOrder";
                column.Caption = "Display Order";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //13
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Sty_Units_Per_Hour";
                column.Caption = "Units Per Hour";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //14
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Sty_WorkWear";
                column.Caption = "Conti WorkWear";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //15
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Sty_PFD";
                column.Caption = "PFD";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //======================================================
                //
                //================================================

                oTxtBoxA.Name = "Col0";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Panel Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].DisplayIndex = 0;

                oTxtBoxB.Name = "Col1";
                oTxtBoxB.ValueType = typeof(string);
                oTxtBoxB.HeaderText = "Style Description";
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].Visible = true;
                dataGridView1.Columns[1].DisplayIndex = 1;

                oChkBoxA.Name = "Col2";
                oChkBoxA.ValueType = typeof(bool);
                oChkBoxA.HeaderText = "Discontinue";
                oChkBoxA.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oChkBoxA);
                dataGridView1.Columns[2].Visible = true;
                dataGridView1.Columns[2].DisplayIndex = 2;

                oTxtBoxL.Name = "Col3";
                oTxtBoxL.ValueType = typeof(DateTime);
                oTxtBoxL.HeaderText = "Date Discontinued";
                oTxtBoxL.DataPropertyName = DataT.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxL);
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns[3].DisplayIndex = 3;

                oBtnA = new DataGridViewButtonColumn();
                oBtnA.HeaderText = "Size Selection";
                dataGridView1.Columns.Add(oBtnA);
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[4].DisplayIndex = 4;

                oBtnB = new DataGridViewButtonColumn();
                oBtnB.HeaderText = "Trims Selection";
                dataGridView1.Columns.Add(oBtnB);
                dataGridView1.Columns[5].Visible = true;
                dataGridView1.Columns[5].DisplayIndex = 5;

                oBtnC = new DataGridViewButtonColumn();
                oBtnC.HeaderText = "Colours Selection";
                dataGridView1.Columns.Add(oBtnC);
                dataGridView1.Columns[6].Visible = true;
                dataGridView1.Columns[6].DisplayIndex = 6;

                oBtnD = new DataGridViewButtonColumn();
                oBtnD.HeaderText = "Label Selection";
                dataGridView1.Columns.Add(oBtnD);
                dataGridView1.Columns[7].Visible = true;
                dataGridView1.Columns[7].DisplayIndex = 7;

                oChkBoxC.Name = "Col4";
                oChkBoxC.ValueType = typeof(bool);
                oChkBoxC.HeaderText = "Check Mandatory";
                oChkBoxC.DataPropertyName = DataT.Columns[4].ColumnName;
                dataGridView1.Columns.Add(oChkBoxC);
                dataGridView1.Columns[8].Visible = true;
                dataGridView1.Columns[8].DisplayIndex = 8;

                oTxtBoxD.Name = "Col5";
                oTxtBoxD.ValueType = typeof(int);
                oTxtBoxD.HeaderText = "Pastel Number";
                oTxtBoxD.DataPropertyName = DataT.Columns[5].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[9].Visible = true;
                dataGridView1.Columns[9].DisplayIndex = 9;

                oTxtBoxE.Name = "Col5";
                oTxtBoxE.ValueType = typeof(string);
                oTxtBoxE.HeaderText = "Pastel Code";
                oTxtBoxE.DataPropertyName = DataT.Columns[6].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxE);
                dataGridView1.Columns[10].Visible = true;
                dataGridView1.Columns[10].DisplayIndex = 10;

                oTxtBoxF.Name = "Col6";
                oTxtBoxF.ValueType = typeof(int);
                oTxtBoxF.HeaderText = "Cotton Factor";
                oTxtBoxF.DataPropertyName = DataT.Columns[7].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxF);
                dataGridView1.Columns[11].Visible = true;
                dataGridView1.Columns[11].DisplayIndex = 11;

                oTxtBoxG.Name = "Col7";
                oTxtBoxG.ValueType = typeof(int);
                oTxtBoxG.HeaderText = "Bags";
                oTxtBoxG.DataPropertyName = DataT.Columns[8].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxG);
                dataGridView1.Columns[12].Visible = true;
                dataGridView1.Columns[12].DisplayIndex = 12;

                oChkBoxD.Name = "Col8";
                oChkBoxD.ValueType = typeof(bool);
                oChkBoxD.HeaderText = "Buttons";
                oChkBoxD.DataPropertyName = DataT.Columns[9].ColumnName;
                dataGridView1.Columns.Add(oChkBoxD);
                dataGridView1.Columns[13].Visible = true;
                dataGridView1.Columns[13].DisplayIndex = 13;

                oChkBoxE.Name = "Col9";
                oChkBoxE.ValueType = typeof(bool);
                oChkBoxE.HeaderText = "Bought In Fabric";
                oChkBoxE.DataPropertyName = DataT.Columns[10].ColumnName;
                dataGridView1.Columns.Add(oChkBoxE);
                dataGridView1.Columns[14].Visible = true;
                dataGridView1.Columns[14].DisplayIndex = 14;

                oBtnE.Name = "Col10";
                oBtnE.ValueType = typeof(int);
                oBtnE.HeaderText = "Quality";
                dataGridView1.Columns.Add(oBtnE);
                dataGridView1.Columns[15].Visible = true;
                dataGridView1.Columns[15].DisplayIndex = 15;

                oChkBoxF.Name = "Col11";
                oChkBoxF.ValueType = typeof(bool);
                oChkBoxF.HeaderText = "Equiv";
                oChkBoxF.DataPropertyName = DataT.Columns[11].ColumnName;
                dataGridView1.Columns.Add(oChkBoxF);
                dataGridView1.Columns[16].Visible = true;
                dataGridView1.Columns[16].DisplayIndex = 16;

                oTxtBoxJ.Name = "Col12";
                oTxtBoxJ.ValueType = typeof(int);
                oTxtBoxJ.HeaderText = "Display Order";
                oTxtBoxJ.DataPropertyName = DataT.Columns[12].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxJ);
                dataGridView1.Columns[17].Visible = true;
                dataGridView1.Columns[17].DisplayIndex = 17;

                oTxtBoxK.Name = "Col13";
                oTxtBoxK.ValueType = typeof(int);
                oTxtBoxK.HeaderText = "Units Per Hour";
                oTxtBoxK.DataPropertyName = DataT.Columns[13].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxK);
                dataGridView1.Columns[18].Visible = true;
                dataGridView1.Columns[18].DisplayIndex = 18;

                oChkBoxG.Name = "Col14";
                oChkBoxG.ValueType = typeof(bool);
                oChkBoxG.HeaderText = "Work Wear";
                oChkBoxG.DataPropertyName = DataT.Columns[14].ColumnName;
                dataGridView1.Columns.Add(oChkBoxG);
                dataGridView1.Columns[19].Visible = true;
                dataGridView1.Columns[19].DisplayIndex = 19;

                oChkBoxH.Name = "Col15";
                oChkBoxH.ValueType = typeof(bool);
                oChkBoxH.HeaderText = "PF Dye";
                oChkBoxH.DataPropertyName = DataT.Columns[15].ColumnName;
                dataGridView1.Columns.Add(oChkBoxH);
                dataGridView1.Columns[20].Visible = true;
                dataGridView1.Columns[20].DisplayIndex = 20;
                //==============================================================
                // This option is populated from a combo box selected change event
                //================================================================
            }
            else if (TransNo == 2)
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

                var Entities = _context.TLADM_Colours.OrderBy(x => x.Col_Display);
                foreach (var Entity in Entities)
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
            }
            else if (TransNo == 4)
            {
                this.Text = "Greige Description Update / Edit Facility";
                //0
                DataColumn[] keys = new DataColumn[1];
                //------------------------------------
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_Id";
                column.Caption = "Greige Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                keys[0] = column;
                DataT.PrimaryKey = keys;
                //1 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "TLGreige_Description";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                //2
                //-----------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "TLGriege_Discontinued";
                column.Caption = "Discontinued";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //3
                //----------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(DateTime);
                column.ColumnName = "TLGreige_Discontinued_Date";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                //4 
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_OldCode";
                column.Caption = "Old Code";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //5
                //---------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_PowerN";
                column.Caption = "Power No";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //6
                //------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "TLGreige_BarCode";
                column.Caption = "BarCode";
                column.DefaultValue = false; 
                DataT.Columns.Add(column);

                //7
                //------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_FabricWeight_FK";
                column.Caption = "Fabric Weight";
                column.DefaultValue = _context.TLADM_FabWidth.FirstOrDefault().FW_Id;
                DataT.Columns.Add(column);

                //8
                //------------------------------------------------------

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_FabricWidth_FK";
                column.Caption = "Fabric Width";
                column.DefaultValue = _context.TLADM_FabricWeight.FirstOrDefault().FWW_Id;
                DataT.Columns.Add(column);


                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_Machine_FK";
                column.Caption = "Machine";
                column.DefaultValue = _context.TLADM_MachineDefinitions.Where(x=>x.MD_Department_FK == 11).FirstOrDefault().MD_Pk;
                DataT.Columns.Add(column);


                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_ProductType_FK";
                column.Caption = "Product Type";
                column.DefaultValue = _context.TLADM_ProductTypes.FirstOrDefault().PT_pk;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TLGreige_Quality_FK";
                column.Caption = "Quality Type";
                column.DefaultValue = _context.TLADM_GreigeQuality.FirstOrDefault().GQ_Pk;
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
                column.DefaultValue = _context.TLADM_StockTakeFreq.FirstOrDefault().STF_Pk;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TLGreige_UOM_Fk";
                column.Caption = "Unit Of Measure";
                column.DefaultValue = _context.TLADM_UOM.FirstOrDefault().UOM_Pk;
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

                //--------------------------------------------------
                //1  
                //--------------------------------------------
                oTxtBoxA.Name = "Col1";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Panel Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].DisplayIndex = 0;

                oTxtBoxB.Name = "Col2";
                oTxtBoxB.ValueType = typeof(string);
                oTxtBoxB.HeaderText = "Description";
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;


                oChkBoxC.Name = "Col3";
                oChkBoxC.ValueType = typeof(bool);
                oChkBoxC.HeaderText = "Discontinued";
                oChkBoxC.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oChkBoxC);
                dataGridView1.Columns[2].DisplayIndex = 2;

                oTxtBoxD.Name = "Col4";
                oTxtBoxD.ValueType = typeof(DateTime);
                oTxtBoxD.HeaderText = "Discontinued Date";
                oTxtBoxD.DataPropertyName = DataT.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns[3].DisplayIndex = 3;

                oTxtBoxE.Name = "Col5";
                oTxtBoxE.ValueType = typeof(int);
                oTxtBoxE.HeaderText = "Old Code";
                oTxtBoxE.DataPropertyName = DataT.Columns[4].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxE);
                dataGridView1.Columns[4].DisplayIndex = 4;

                oTxtBoxF.Name = "Col6";
                oTxtBoxF.ValueType = typeof(int);
                oTxtBoxF.HeaderText = "Power No";
                oTxtBoxF.DataPropertyName = DataT.Columns[5].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxF);
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[5].DisplayIndex = 5;


                oChkBoxD.Name = "Col7";
                oChkBoxD.ValueType = typeof(bool);
                oChkBoxD.HeaderText = "Barcode";
                oChkBoxD.DataPropertyName = DataT.Columns[6].ColumnName;
                dataGridView1.Columns.Add(oChkBoxD);
                dataGridView1.Columns[6].DisplayIndex = 6;


                oCmbBoxA.Name = "Col8";
                oCmbBoxA.HeaderText = "Fabric Weight";
                oCmbBoxA.DataPropertyName = DataT.Columns[7].ColumnName;
                oCmbBoxA.DataSource = _context.TLADM_FabricWeight.ToList();
                oCmbBoxA.ValueMember = "FWW_ID";
                oCmbBoxA.DisplayMember = "FWW_Description";
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns[7].DisplayIndex = 7;


                oCmbBoxB.Name = "Col9";
                oCmbBoxB.HeaderText = "Fabric Width";
                oCmbBoxB.DataPropertyName = DataT.Columns[8].ColumnName;
                oCmbBoxB.DataSource = _context.TLADM_FabWidth.ToList();
                oCmbBoxB.ValueMember = "FW_ID";
                oCmbBoxB.DisplayMember = "FW_Description";
                dataGridView1.Columns.Add(oCmbBoxB);
                dataGridView1.Columns[8].DisplayIndex = 8;


                oCmbBoxC.Name = "Col10";
                oCmbBoxC.HeaderText = "Machine";
                oCmbBoxC.DataPropertyName = DataT.Columns[9].ColumnName;
                oCmbBoxC.DataSource = _context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == 11).ToList();
                oCmbBoxC.ValueMember = "MD_PK";
                oCmbBoxC.DisplayMember = "MD_Description";
                dataGridView1.Columns.Add(oCmbBoxC);
                dataGridView1.Columns[9].DisplayIndex = 9;


                oCmbBoxD.Name = "Col11";
                oCmbBoxD.HeaderText = "Product Type";
                oCmbBoxD.DataSource = _context.TLADM_ProductTypes.ToList();
                oCmbBoxD.ValueMember = "PT_Pk";
                oCmbBoxD.DisplayMember = "PT_Description";
                oCmbBoxD.DataPropertyName = DataT.Columns[10].ColumnName;
                dataGridView1.Columns.Add(oCmbBoxD);
                dataGridView1.Columns[10].DisplayIndex = 10;

                oCmbBoxE.Name = "Col12";
                oCmbBoxE.HeaderText = "Quality Types";
                oCmbBoxE.DataSource = _context.TLADM_GreigeQuality.ToList();
                oCmbBoxE.ValueMember = "GQ_Pk";
                oCmbBoxE.DisplayMember = "GQ_Description";
                oCmbBoxE.DataPropertyName = DataT.Columns[11].ColumnName;
                dataGridView1.Columns.Add(oCmbBoxE);
                dataGridView1.Columns[11].DisplayIndex = 11;

                oTxtBoxL.Name = "Col13";
                oTxtBoxL.ValueType = typeof(int);
                oTxtBoxL.HeaderText = "ROL";
                oTxtBoxL.DataPropertyName = DataT.Columns[12].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxL);
                dataGridView1.Columns[12].DisplayIndex = 12;

                oTxtBoxM.Name = "Col14";
                oTxtBoxM.ValueType = typeof(int);
                oTxtBoxM.HeaderText = "ROQ";
                oTxtBoxM.DataPropertyName = DataT.Columns[13].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxM);
                dataGridView1.Columns[13].DisplayIndex = 13;

                oChkBoxE.Name = "Col15";
                oChkBoxE.ValueType = typeof(bool);
                oChkBoxE.HeaderText = "Show Qty";
                oChkBoxE.DataPropertyName = DataT.Columns[14].ColumnName;
                dataGridView1.Columns.Add(oChkBoxE);
                dataGridView1.Columns[14].DisplayIndex = 14;

                oCmbBoxF.Name = "Col16";
                oCmbBoxF.HeaderText = "Stock Take Freq";
                oCmbBoxF.DataPropertyName = DataT.Columns[15].ColumnName;
                oCmbBoxF.DataSource = _context.TLADM_StockTakeFreq.ToList();
                oCmbBoxF.ValueMember = "STF_Pk";
                oCmbBoxF.DisplayMember = "STF_Description";
                dataGridView1.Columns.Add(oCmbBoxF);
                dataGridView1.Columns[15].DisplayIndex = 15;

                oCmbBoxG.Name = "Col17";
                oCmbBoxG.HeaderText = "UOM";
                oCmbBoxG.DataSource = _context.TLADM_UOM.ToList();
                oCmbBoxG.ValueMember = "UOM_Pk";
                oCmbBoxG.DisplayMember = "UOM_Description";
                oCmbBoxG.DataPropertyName = DataT.Columns[16].ColumnName;
                dataGridView1.Columns.Add(oCmbBoxG);
                dataGridView1.Columns[16].DisplayIndex = 16;

                oBtnA.Name = "Col18";
                oBtnA.HeaderText = "Yarn Selection";
                oBtnA.DataPropertyName = DataT.Columns[17].ColumnName;
                dataGridView1.Columns.Add(oBtnA);
                dataGridView1.Columns[17].DisplayIndex = 17;

                oTxtBoxQ.Name = "Col19";
                oTxtBoxQ.ValueType = typeof(decimal);
                oTxtBoxQ.HeaderText = "Kg Per Piece";
                oTxtBoxQ.DataPropertyName = DataT.Columns[18].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxQ);
                dataGridView1.Columns[18].DisplayIndex = 18;

                oTxtBoxR.Name = "Col20";
                oTxtBoxR.ValueType = typeof(decimal);
                oTxtBoxR.HeaderText = "Suggested Kgs Per Roll";
                oTxtBoxR.DataPropertyName = DataT.Columns[19].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxR);
                dataGridView1.Columns[19].DisplayIndex = 19;

                oTxtBoxS.Name = "Col21";
                oTxtBoxS.ValueType = typeof(decimal);
                oTxtBoxS.HeaderText = "Suggested Meters Per Roll";
                oTxtBoxS.DataPropertyName = DataT.Columns[20].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxS);
                dataGridView1.Columns["Col21"].DisplayIndex = 20;

                oTxtBoxT.Name = "Col22";
                oTxtBoxT.ValueType = typeof(decimal);
                oTxtBoxT.HeaderText = "Faults Allowed";
                oTxtBoxT.DataPropertyName = DataT.Columns[21].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxT);
                dataGridView1.Columns[21].DisplayIndex = 21;

                oChkBoxF.Name = "Col23";
                oChkBoxF.ValueType = typeof(bool);
                oChkBoxF.HeaderText = "Bought In";
                oChkBoxF.DataPropertyName = DataT.Columns[22].ColumnName;
                dataGridView1.Columns.Add(oChkBoxF);
                dataGridView1.Columns[22].DisplayIndex = 22;

                oChkBoxG.Name = "Col24";
                oChkBoxG.ValueType = typeof(bool);
                oChkBoxG.HeaderText = "Lining";
                oChkBoxG.DataPropertyName = DataT.Columns[23].ColumnName;
                dataGridView1.Columns.Add(oChkBoxG);
                dataGridView1.Columns[23].DisplayIndex = 23;

                oTxtBoxU.Name = "Col25";
                oTxtBoxU.ValueType = typeof(decimal);
                oTxtBoxU.HeaderText = "Dsk Weight";
                oTxtBoxU.DataPropertyName = DataT.Columns[24].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxU);
                dataGridView1.Columns[24].DisplayIndex = 24;

                oChkBoxH.Name = "Col26";
                oChkBoxH.ValueType = typeof(bool);
                oChkBoxH.HeaderText = "Body";
                oChkBoxH.DataPropertyName = DataT.Columns[25].ColumnName;
                dataGridView1.Columns.Add(oChkBoxH);
                dataGridView1.Columns[25].DisplayIndex = 25;

                oBtnB.Name = "Col27";
                oBtnB.HeaderText = "Colour";
                dataGridView1.Columns.Add(oBtnB);
                dataGridView1.Columns[26].DisplayIndex = 26;

                var Entities = _context.TLADM_Griege.ToList();
                foreach (var Entity in Entities)
                {
                    DataRow NewRow = DataT.NewRow();

                    NewRow[0] = Entity.TLGreige_Id;
                    NewRow[1] = Entity.TLGreige_Description;
                    NewRow[2] = Entity.TLGriege_Discontinued ?? false;
                    if (Entity.TLGreige_Discontinued_Date == null)
                    {
                        NewRow[3] = DBNull.Value;
                    }
                    else
                    {
                        NewRow[3] = Entity.TLGreige_Discontinued_Date;
                    }
                    NewRow[4] = Entity.TLGreige_OldCode ?? 0;
                    NewRow[5] = Entity.TLGreige_PowerN;
                    NewRow[6] = Entity.TLGreige_BarCode;
                    NewRow[7] = Entity.TLGreige_FabricWeight_FK;
                    NewRow[8] = Entity.TLGreige_FabricWidth_FK;
                    NewRow[9] = Entity.TLGreige_Machine_FK;
                    NewRow[10] = Entity.TLGreige_ProductType_FK;
                    if (Entity.TLGreige_ProductType_FK != 0)
                    {
                        if (Entity.TLGreige_Quality_FK != 0)
                        {
                            NewRow[11] = Entity.TLGreige_Quality_FK;
                        }
                        else
                        {
                            NewRow[11] = _context.TLADM_GreigeQuality.FirstOrDefault().GQ_Pk;
                        }
                    }
                    else
                    {
                        NewRow[11] = _context.TLADM_GreigeQuality.FirstOrDefault().GQ_Pk;
                    }

                    NewRow[12] = Entity.TLGreige_ROL;
                    NewRow[13] = Entity.TLGreige_ROQ;
                    NewRow[14] = Entity.TLGreige_ShowQty;
                    NewRow[15] = Entity.TLGreige_StockTakeFreq_FK;
                    NewRow[16] = Entity.TLGreige_UOM_Fk;
                    NewRow[17] = Entity.TLGreige_YarnPowerN;
                    NewRow[18] = Entity.TLGreige_KgPerPiece;
                    NewRow[19] = Entity.TLGreige_LatestSuggest;
                    NewRow[20] = Entity.TLGreige_Meters;
                    NewRow[21] = Entity.TLGreige_FaultsAllowed;
                    NewRow[22] = Entity.TLGreige_IsBoughtIn;
                    NewRow[23] = Entity.TLGreige_IsLining;
                    NewRow[24] = Entity.TLGreige_CubicWeight;
                    NewRow[25] = Entity.TLGreige_Body;
                    DataT.Rows.Add(NewRow);

                }
            }
            else if (TransNo == 5)
            {
                this.Text = "Fabric Width ";
                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Group_Pk";
                column.Caption = "Group Primary Key";
                column.DefaultValue = -1;
                DataT.Columns.Add(column);

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
                column.DataType = typeof(System.DateTime);
                column.ColumnName = "DateDiscontinued";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                //--------------------------------------------------------
                // Col 4
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Weight_Calc";
                column.Caption = "Weight";
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
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns[3].DisplayIndex = 3;

                oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.HeaderText = "Calculation Value";
                oTxtBoxD.DataPropertyName = DataT.Columns[4].ColumnName;
                oTxtBoxD.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxD);

                var Entities = _context.TLADM_FabWidth.OrderBy(x => x.FW_Description).ToList();
                foreach (var Entity in Entities)
                {
                    DataRow NRow = DataT.NewRow();
                    NRow[0] = Entity.FW_Id;
                    NRow[1] = Entity.FW_Description;
                    NRow[2] = Entity.FW_Discontinued;
                    if (Entity.FW_Discontinued_Date != null)
                    {
                        NRow[3] = (DateTime)Entity.FW_Discontinued_Date;
                    }
                    NRow[4] = Entity.FW_Calculation_Value;

                    DataT.Rows.Add(NRow);
                }

            }
            else if (TransNo == 7)
            {
                this.Text = "Units of Measure";
                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Group_Pk";
                column.Caption = "Group Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //----------------------------------------------
                // Col1 
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "ShortCode_Pk";
                column.Caption = "Short Code";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                //----------------------------------------------
                // Col2 
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "Description_Pk";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                //--------------------------------------------------------
                // Col3
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "Discontinued_Pk";
                column.Caption = "Discontinued";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                //--------------------------------------------------------
                // Col 4
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(System.DateTime);
                column.ColumnName = "DateDiscontinued";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                //0 -- 
                //--------------------------------------------
                oTxtBoxA.Name = "Panel_PK";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Panel Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns[0].Visible = false;

                //---- 1
                oTxtBoxB.Name = "ShortCode_PK";
                oTxtBoxB.ValueType = typeof(String);
                oTxtBoxB.HeaderText = "Short Code";
                oTxtBoxB.Visible = true;
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;
                dataGridView1.Columns[1].Visible = true;

                //---- 2
                oTxtBoxC.Name = "Description";
                oTxtBoxC.ValueType = typeof(String);
                oTxtBoxC.HeaderText = "Description";
                oTxtBoxC.Visible = true;
                oTxtBoxC.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[2].DisplayIndex = 2;
                dataGridView1.Columns[2].Visible = true;

                //---- 3 -- Open / Closed 
                //------------------------------------------------

                oChkBoxB.Name = "Discontinued";
                oChkBoxB.HeaderText = "Discontinued";
                oChkBoxB.DataPropertyName = DataT.Columns[3].ColumnName;
                oChkBoxB.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkBoxB);
                dataGridView1.Columns[3].DisplayIndex = 3;
                dataGridView1.Columns[3].Visible = true;

                //---- 4 -- Open / Closed 
                //------------------------------------------------
                oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.Visible = true;
                oTxtBoxD.HeaderText = "Date Discontinued";
                oTxtBoxD.DataPropertyName = DataT.Columns[4].ColumnName;
                oTxtBoxD.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns[4].DisplayIndex = 4;

                var Entities = _context.TLADM_UOM.ToList();
                foreach (var Entity in Entities)
                {
                    DataRow NRow = DataT.NewRow();
                    NRow[0] = Entity.UOM_Pk;
                    NRow[1] = Entity.UOM_ShortCode;
                    NRow[2] = Entity.UOM_Description;
                    NRow[3] = Entity.UOM_Discontinued;
                    if (Entity.UOM_DiscontinuedDate != null)
                    {
                        NRow[4] = (DateTime)Entity.UOM_DiscontinuedDate;
                    }
                    else
                    {
                        NRow[4] = DBNull.Value;
                    }

                    DataT.Rows.Add(NRow);
                }
            }
            else if (TransNo == 8)
            {
                this.Text = "Size Update and Edit facility";

                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Group_Pk";
                column.Caption = "Group Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

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
                column.DataType = typeof(System.DateTime);
                column.ColumnName = "DateDiscontinued";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "PowerNo";
                column.Caption = "Power No";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "FinishedCode";
                column.Caption = "Finished Code";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "ColumnNumber";
                column.Caption = "Column Number";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "PastelNumber";
                column.Caption = "Pastel Number";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "DisplayOrder";
                column.Caption = "Display Order";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "AdultSize";
                column.Caption = "Adult Size";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "ContiSize";
                column.Caption = "Conti Size";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(String);
                column.ColumnName = "DisplayString";
                column.Caption = "Display String";
                DataT.Columns.Add(column);

                //1 -- 
                //--------------------------------------------
                oTxtBoxA.Name = "Panel_PK";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Panel Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns[0].Visible = false;

                //---- 2
                oTxtBoxB.Name = "Description_PK";
                oTxtBoxB.ValueType = typeof(String);
                oTxtBoxB.HeaderText = "Description";
                oTxtBoxB.Visible = true;
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;
                dataGridView1.Columns[1].Visible = true;
                //---- 3 -- Open / Closed 
                //------------------------------------------------

                oChkBoxB.Name = "Discontinued";
                oChkBoxB.HeaderText = "Discontinued";
                oChkBoxB.DataPropertyName = DataT.Columns[2].ColumnName;
                oChkBoxB.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkBoxB);
                dataGridView1.Columns[2].DisplayIndex = 2;
                dataGridView1.Columns[2].Visible = true;

                oTxtBoxC = new DataGridViewTextBoxColumn();
                oTxtBoxC.Visible = true;
                oTxtBoxC.HeaderText = "Date Discontinued";
                oTxtBoxC.DataPropertyName = DataT.Columns[3].ColumnName;
                oTxtBoxC.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns[3].DisplayIndex = 3;

                oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.Visible = false;
                oTxtBoxD.HeaderText = "Power No";
                oTxtBoxD.DataPropertyName = DataT.Columns[4].ColumnName;
                oTxtBoxD.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[4].DisplayIndex = 4;
                dataGridView1.Columns[4].Visible = false;

                oTxtBoxE = new DataGridViewTextBoxColumn();
                oTxtBoxE.Visible = false;
                oTxtBoxE.HeaderText = "Finished Code";
                oTxtBoxE.DataPropertyName = DataT.Columns[5].ColumnName;
                oTxtBoxE.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxE);
                dataGridView1.Columns[5].DisplayIndex = 5;
                dataGridView1.Columns[5].Visible = false;

                oTxtBoxF = new DataGridViewTextBoxColumn();
                oTxtBoxF.Visible = true;
                oTxtBoxF.HeaderText = "Column Number";
                oTxtBoxF.DataPropertyName = DataT.Columns[6].ColumnName;
                oTxtBoxF.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxF);
                dataGridView1.Columns[6].DisplayIndex = 6;
                dataGridView1.Columns[6].Visible = true;

                oTxtBoxG = new DataGridViewTextBoxColumn();
                oTxtBoxG.Visible = true;
                oTxtBoxG.HeaderText = "Pastel Number";
                oTxtBoxG.DataPropertyName = DataT.Columns[7].ColumnName;
                oTxtBoxG.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxG);
                dataGridView1.Columns[7].DisplayIndex = 7;
                dataGridView1.Columns[7].Visible = true;

                oTxtBoxH = new DataGridViewTextBoxColumn();
                oTxtBoxH.Visible = true;
                oTxtBoxH.HeaderText = "Display Order";
                oTxtBoxH.DataPropertyName = DataT.Columns[8].ColumnName;
                oTxtBoxH.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxH);
                dataGridView1.Columns[8].DisplayIndex = 8;
                dataGridView1.Columns[8].Visible = true;

                oChkBoxC.Name = "Adult";
                oChkBoxC.HeaderText = "Adult";
                oChkBoxC.DataPropertyName = DataT.Columns[9].ColumnName;
                oChkBoxC.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkBoxC);
                dataGridView1.Columns[9].DisplayIndex = 9;
                dataGridView1.Columns[9].Visible = true;

                oTxtBoxJ = new DataGridViewTextBoxColumn();
                oTxtBoxJ.Visible = true;
                oTxtBoxJ.HeaderText = "Conti Size";
                oTxtBoxJ.DataPropertyName = DataT.Columns[10].ColumnName;
                oTxtBoxJ.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxJ);
                dataGridView1.Columns[10].DisplayIndex = 10;
                dataGridView1.Columns[10].Visible = true;

                oTxtBoxK = new DataGridViewTextBoxColumn();
                oTxtBoxK.Visible = true;
                oTxtBoxK.HeaderText = "Size Display";
                oTxtBoxK.DataPropertyName = DataT.Columns[11].ColumnName;
                oTxtBoxK.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxK);
                dataGridView1.Columns[11].DisplayIndex = 11;
                dataGridView1.Columns[11].Visible = true;

                var Entities = _context.TLADM_Sizes.OrderBy(x => x.SI_DisplayOrder).ToList();
                foreach (var Entity in Entities)
                {
                    DataRow NRow = DataT.NewRow();
                    NRow[0] = Entity.SI_id;
                    NRow[1] = Entity.SI_Description;
                    NRow[2] = Entity.SI_Discontinued;
                    if (Entity.SI_Discontinued_Date != null)
                    {
                        NRow[3] = (DateTime)Entity.SI_Discontinued_Date;
                    }
                    else
                    {
                        NRow[3] = DBNull.Value;
                    }

                    NRow[4] = Entity.SI_PowerN;
                    NRow[5] = Entity.SI_FinishedCode;
                    NRow[6] = Entity.SI_ColNumber;
                    NRow[7] = Entity.SI_PastelNo;
                    NRow[8] = Entity.SI_DisplayOrder;
                    NRow[9] = Entity.SI_Adult;
                    NRow[10] = Entity.SI_ContiSize;
                    NRow[11] = Entity.SI_Display;


                    DataT.Rows.Add(NRow);
                }
            }
            else if (TransNo == 9)
            {
                this.Text = "Labels";

                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Group_Pk";
                column.Caption = "Group Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

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
                column.DataType = typeof(System.DateTime);
                column.ColumnName = "DateDiscontinued";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                //--------------------------------------------------------
                // Col 4
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Customer_Fk";
                column.Caption = "Customer";
                DataT.Columns.Add(column);

                //--------------------------------------------------------
                // Col 4
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "PowerN_Fk";
                column.Caption = "Power No";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //1 -- 
                //--------------------------------------------
                oTxtBoxA.Name = "Panel_PK";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Panel Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                oTxtBoxA.MinimumWidth = 10;
                oTxtBoxA.FillWeight = 1;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns[0].Visible = false;

                //---- 2
                oTxtBoxB.Name = "Description_PK";
                oTxtBoxB.ValueType = typeof(String);
                oTxtBoxB.HeaderText = "Description";
                oTxtBoxB.Visible = true;
                oTxtBoxB.MinimumWidth = 200;
                oTxtBoxB.FillWeight = 100;
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;
                dataGridView1.Columns[1].Visible = true;
                //---- 3 -- Open / Closed 
                //------------------------------------------------

                oChkBoxB.Name = "Discontinued";
                oChkBoxB.HeaderText = "Discontinued";
                oChkBoxB.DataPropertyName = DataT.Columns[2].ColumnName;
                oChkBoxB.MinimumWidth = 10;
                oChkBoxB.FillWeight = 75;
                oChkBoxB.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkBoxB);
                dataGridView1.Columns[2].DisplayIndex = 2;
                dataGridView1.Columns[2].Visible = true;

                oTxtBoxC = new DataGridViewTextBoxColumn();
                oTxtBoxC.Visible = true;
                oTxtBoxC.HeaderText = "Date Discontinued";
                oTxtBoxC.DataPropertyName = DataT.Columns[3].ColumnName;
                oTxtBoxC.MinimumWidth = 100;
                oTxtBoxC.FillWeight = 90;
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns[3].DisplayIndex = 3;


                oCmbBoxA.Name = "Customer_UOM";
                oCmbBoxA.ValueType = typeof(int);
                oCmbBoxA.HeaderText = "Customer";
                oCmbBoxA.DataPropertyName = DataT.Columns[4].ColumnName;
                oCmbBoxA.DataSource = _context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                oCmbBoxA.ValueMember = "Cust_Pk";
                oCmbBoxA.DisplayMember = "Cust_Description";
                oCmbBoxA.MinimumWidth = 250;
                oCmbBoxA.FillWeight = 300;
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns[4].DisplayIndex = 4;
                dataGridView1.Columns[4].Visible = true;

                oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.Visible = true;
                oTxtBoxD.HeaderText = "Power No";
                oTxtBoxD.DataPropertyName = DataT.Columns[5].ColumnName;
                oTxtBoxD.MinimumWidth = 10;
                oTxtBoxD.FillWeight = 2;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[5].DisplayIndex = 5;
                dataGridView1.Columns[5].Visible = false;

                var Entities = _context.TLADM_Labels.OrderBy(x => x.Lbl_Description).ToList();
                foreach (var Entity in Entities)
                {
                    DataRow NRow = DataT.NewRow();
                    NRow[0] = Entity.Lbl_Id;
                    NRow[1] = Entity.Lbl_Description;
                    NRow[2] = Entity.Lbl_Discontinued;
                    if (Entity.Lbl_Discontinued_Date != null)
                    {
                        NRow[3] = (DateTime)Entity.Lbl_Discontinued_Date;
                    }
                    else
                    {
                        NRow[3] = DBNull.Value;
                    }

                    NRow[4] = Entity.Lbl_Customer_FK;
                    NRow[5] = Entity.Lbl_PowerN;
                    DataT.Rows.Add(NRow);
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            else if (TransNo == 11)
            {
                this.Text = "Department Areas";
                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "DArea_Pk";
                column.Caption = "DArea Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                /* DataColumn[] keys = new DataColumn[1];
                 keys[0] = column;
                 DataT.PrimaryKey = keys;*/


                //----------------------------------------------
                // Col1 
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "DAAreaDescription";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                // Col 2
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "DADiscontinued";
                column.Caption = "Discontinued";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                // Col 3
                //----------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(DateTime);
                column.ColumnName = "DADiscontinuedDate";
                column.Caption = "Discontinued Date";
                column.DefaultValue = DBNull.Value;
                DataT.Columns.Add(column);

                // col 4 
                //--------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "DADepartment_FK";
                column.Caption = "Department";
                DataT.Columns.Add(column);

                oTxtBoxA.Name = "ProductType_PK";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Product Type Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                oTxtBoxA.Resizable = DataGridViewTriState.False;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns[0].Visible = false;

                oTxtBoxB.Name = "ProductType_SC";
                oTxtBoxB.ValueType = typeof(String);
                oTxtBoxB.HeaderText = "Description";
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;


                oChkBoxA.Name = "Discontinued";
                oChkBoxA.ValueType = typeof(bool);
                oChkBoxA.HeaderText = "Discontinued";
                oChkBoxA.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oChkBoxA);
                dataGridView1.Columns[2].DisplayIndex = 2;

                oTxtBoxC.Name = "Dis_Date";
                oTxtBoxC.HeaderText = "Discontinued Date";
                oTxtBoxC.DataPropertyName = DataT.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[3].DisplayIndex = 3;
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";

                oCmbBoxA.Name = "ProductCost_LinePK";
                oCmbBoxA.ValueType = typeof(Int32);
                oCmbBoxA.HeaderText = "Departments";
                oCmbBoxA.DataPropertyName = DataT.Columns[4].ColumnName;
                oCmbBoxA.DataSource = _context.TLADM_Departments.ToList();
                oCmbBoxA.ValueMember = "Dep_Id";
                oCmbBoxA.DisplayMember = "Dep_Description";
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns[4].DisplayIndex = 4;

                var Existing = _context.TLADM_DepartmentsArea.ToList();

                foreach (var Record in Existing)
                {
                    DataRow NewRow = DataT.NewRow();
                    NewRow[0] = Record.DeptA_Pk;
                    NewRow[1] = Record.DeptA_Description;
                    NewRow[2] = Record.DeptA_Discontinued;
                    if (Record.DeptA_DiscontinuedDate != null)
                    {
                        NewRow[3] = Convert.ToDateTime(Record.DeptA_DiscontinuedDate);
                    }
                    else
                    {
                        NewRow[3] = DBNull.Value;
                    }
                    NewRow[4] = Record.DeptA_Dep_Fk;

                    DataT.Rows.Add(NewRow);
                }
            }
            else if (TransNo == 12)
            {
                this.Text = "Product Types (Fabric)";

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
                column.ColumnName = "PTDescription";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                // Col 2
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "PTDiscontinued";
                column.Caption = "Discontinued";
                column.DefaultValue = false;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(DateTime);
                column.ColumnName = "PTDiscontinuedDate";
                column.Caption = "Discontinued Date";
                column.DefaultValue = DBNull.Value;
                DataT.Columns.Add(column);

                //--------------------------------------------
                oTxtBoxA.Name = "ProductType_PK";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Product Type Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                oTxtBoxA.Resizable = DataGridViewTriState.False;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns[0].Visible = false;

                oTxtBoxB.Name = "ProductType_SC";
                oTxtBoxB.ValueType = typeof(String);
                oTxtBoxB.HeaderText = "Description";
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;


                oChkBoxA.Name = "Discontinued";
                oChkBoxA.ValueType = typeof(bool);
                oChkBoxA.HeaderText = "Discontinued";
                oChkBoxA.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oChkBoxA);
                dataGridView1.Columns[2].DisplayIndex = 2;

                oTxtBoxD.Name = "Dis_Date";
                oTxtBoxD.HeaderText = "Discontinued Date";
                oChkBoxD.DataPropertyName = DataT.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[3].DisplayIndex = 3;
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";


                var Existing = _context.TLADM_FabricProduct.ToList();

                foreach (var Record in Existing)
                {
                    DataRow NewRow = DataT.NewRow();
                    NewRow[0] = Record.FP_Id;
                    NewRow[1] = Record.FP_Description;
                    NewRow[2] = Record.FP_Discontinued;
                    if (Record.FP_Discontinued_Date != null)
                    {
                        NewRow[3] = (DateTime)Record.FP_Discontinued_Date;
                    }
                    else
                    {
                        NewRow[3] = DBNull.Value;
                    }

                    DataT.Rows.Add(NewRow);
                }


            }
            else if (TransNo == 13)
            {
                this.Text = "Fabric Weight ";
                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Group_Pk";
                column.Caption = "Group Primary Key";
                column.DefaultValue = -1;
                DataT.Columns.Add(column);

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
                column.DataType = typeof(System.DateTime);
                column.ColumnName = "DateDiscontinued";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                //--------------------------------------------------------
                // Col 4
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Weight_Calc";
                column.Caption = "Weight";
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
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns[3].DisplayIndex = 3;

                oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.HeaderText = "Calculation Value";
                oTxtBoxD.DataPropertyName = DataT.Columns[4].ColumnName;
                oTxtBoxD.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxD);

                var Entities = _context.TLADM_FabricWeight.OrderBy(x => x.FWW_Description).ToList();
                foreach (var Entity in Entities)
                {
                    DataRow NRow = DataT.NewRow();
                    NRow[0] = Entity.FWW_Id;
                    NRow[1] = Entity.FWW_Description;
                    NRow[2] = Entity.FWW_Discontinued;
                    if (Entity.FWW_Discontinued_Date != null)
                    {
                        NRow[3] = (DateTime)Entity.FWW_Discontinued_Date;
                    }
                    NRow[4] = Entity.FWW_Calculation_Value;

                    DataT.Rows.Add(NRow);
                }
            }
            else if (TransNo == 14)
            {
                this.Text = "Greige Quality Groups";
                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Group_Pk";
                column.Caption = "Group Primary Key";
                column.DefaultValue = -1;
                DataT.Columns.Add(column);

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
                column.DataType = typeof(System.DateTime);
                column.ColumnName = "DateDiscontinued";
                column.Caption = "Date";
                DataT.Columns.Add(column);

                //--------------------------------------------------------
                // Col 4
                //----------------------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(System.Int32);
                column.ColumnName = "Power_No";
                column.Caption = "PowerNo";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);

                //---------------------------------------------------
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
                oTxtBoxB.MinimumWidth = 200;
                oTxtBoxB.FillWeight = 200;
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;

                //2 -- Open / Closed 
                //------------------------------------------------

                oChkBoxB.Name = "Discontinued";
                oChkBoxB.HeaderText = "Discontinued";
                oChkBoxB.DataPropertyName = DataT.Columns[2].ColumnName;
                oChkBoxB.MinimumWidth = 100;
                oChkBoxB.FillWeight = 50;
                oChkBoxB.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkBoxB);
                dataGridView1.Columns[2].DisplayIndex = 2;

                //3 -- Column to Date Discontinued
                //----------------------------------------------
                oTxtBoxC = new DataGridViewTextBoxColumn();
                oTxtBoxC.Visible = true;
                oTxtBoxC.HeaderText = "Date Discontinued";
                oTxtBoxC.MinimumWidth = 100;
                oTxtBoxC.FillWeight = 100;
                oTxtBoxC.DataPropertyName = DataT.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns[3].DisplayIndex = 3;

                oTxtBoxD = new DataGridViewTextBoxColumn();
                oTxtBoxD.Visible = false;
                oTxtBoxD.HeaderText = "PowerNo";
                oTxtBoxD.DataPropertyName = DataT.Columns[4].ColumnName;
                oTxtBoxD.Width = 100;
                dataGridView1.Columns.Add(oTxtBoxD);

                var Entities = _context.TLADM_GreigeQuality.OrderBy(x => x.GQ_Description).ToList();
                foreach (var Entity in Entities)
                {
                    DataRow NRow = DataT.NewRow();
                    NRow[0] = Entity.GQ_Pk;
                    NRow[1] = Entity.GQ_Description;
                    NRow[2] = Entity.GQ_Discontinued;
                    if (Entity.GQ_Discontinued_Date != null)
                    {
                        NRow[3] = (DateTime)Entity.GQ_Discontinued_Date;
                    }
                    NRow[4] = Entity.GQ_PowerN;

                    DataT.Rows.Add(NRow);
                }
            }
            else if (TransNo == 17)
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
            else if (TransNo == 18)
            {
                this.Text = "Department Production Loss / Edit Facility";
                //==========================================================================================
                // 1st task is to create the data table
                // Col 0
                //=====================================================================
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "DPPLoss_Pk";
                column.Caption = "DPLoss Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                DataT.PrimaryKey = new DataColumn[] { DataT.Columns[0] };

                //----------------------------------------------
                // Col1 
                //-----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "Department_Pk";
                column.Caption = "Description";
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "ProductionLoss_Percentage";
                column.Caption = "Production Loss %";
                column.DefaultValue = 0.00M;
                DataT.Columns.Add(column);

                column = new DataColumn();
                column.DataType = typeof(Decimal);
                column.ColumnName = "ProductionLoss_Kg";
                column.Caption = "Production Kg %";
                column.DefaultValue = 0.00M;
                DataT.Columns.Add(column);

                oCmbBoxA.Name = "ProductCost_LinePK";
                oCmbBoxA.ValueType = typeof(Int32);
                oCmbBoxA.HeaderText = "Department";
                oCmbBoxA.DataPropertyName = DataT.Columns[1].ColumnName;
                oCmbBoxA.DataSource = _context.TLCMT_FactConfig.ToList();
                oCmbBoxA.ValueMember = "Dep_Id";
                oCmbBoxA.DisplayMember = "Dep_Description";
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns[1].DisplayIndex = 1;

                oTxtBoxA.Name = "ProductPrc_PK";
                oTxtBoxA.ValueType = typeof(decimal);
                oTxtBoxA.HeaderText = "Production Loss %";
                oTxtBoxA.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[2].DisplayIndex = 2;

                oTxtBoxB.Name = "ProductKg_PK";
                oTxtBoxB.ValueType = typeof(decimal);
                oTxtBoxB.HeaderText = "Production Kg";
                oTxtBoxB.DataPropertyName = DataT.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[3].DisplayIndex = 3;

                var ExistingData = _context.TLADM_ProductionLoss.ToList();

                foreach (var row in ExistingData)
                {
                    DataRow NewRow = DataT.NewRow();

                    NewRow[0] = row.TLProdLoss_Pk;
                    NewRow[1] = row.TLProdLoss_Dept_Fk;
                    NewRow[2] = row.TLProdLoss_Percent;
                    NewRow[3] = row.TLProdLoss_Kg;

                    DataT.Rows.Add(NewRow);
                }
            }
            else if (TransNo == 21)
            {
                this.Text = "CMT Costing Update / Edit Facility";
                groupBox1.Visible = true;
                chkExport.Enabled = true;
                chkImport.Enabled = true;

                FormLoaded = false;
                DataColumn[] keys = new DataColumn[1];
                //===================================
                // 00
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "CMTCost_Id";
                column.Caption = "CMT Cost Id Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                keys[0] = column;
                DataT.PrimaryKey = keys;

                //01
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "CMTCost_LineId";
                column.Caption = "CMT Cost Line Id";
                column.DefaultValue = _context.TLCMT_FactConfig.FirstOrDefault().TLCMTCFG_Pk;
                DataT.Columns.Add(column);

                // 02
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "CMTCost_Style";
                column.Caption = "CMT Cost Style";
                column.DefaultValue = _context.TLADM_Styles.Where(x => (bool)!x.Sty_Discontinued).FirstOrDefault().Sty_Id; ;
                DataT.Columns.Add(column);
                // 03
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "CMTCost_Colour";
                column.Caption = "CMT Cost Colour";
                column.DefaultValue = _context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).FirstOrDefault().Col_Id;
                DataT.Columns.Add(column);
                //04
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "CMTCost_Size";
                column.Caption = "CMT Cost Size";
                column.DefaultValue = _context.TLADM_Sizes.Where(x => !(bool)x.SI_Discontinued).FirstOrDefault().SI_id;
                DataT.Columns.Add(column);
                //05
                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "CMTCost_ProdCost";
                column.Caption = "CMT Cost Production ";
                column.DefaultValue = 0.00M;
                DataT.Columns.Add(column);
                //06
                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "CMTCost_ProdDamage";
                column.Caption = "CMT Cost Damage";
                column.DefaultValue = 0.00M;
                DataT.Columns.Add(column);
                //07
                column = new DataColumn();
                column.DataType = typeof(decimal);
                column.ColumnName = "CMTCost_ProdLoss";
                column.Caption = "CMT Cost Loss";
                column.DefaultValue = 0.00M;
                DataT.Columns.Add(column);

                //--------------------------------------------
                //
                //-----------------------------------------------------
                // 01
                oTxtBoxA.Name = "ProductCost_PK";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Product Cost Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns[0].Visible = false;

                oCmbBoxA.Name = "ProductCost_LinePK";
                oCmbBoxA.ValueType = typeof(Int32);
                oCmbBoxA.HeaderText = "Product Cost Line";
                oCmbBoxA.DataPropertyName = DataT.Columns[1].ColumnName;
                oCmbBoxA.DataSource = _context.TLCMT_FactConfig.ToList();
                oCmbBoxA.ValueMember = "TLCMTCFG_Pk";
                oCmbBoxA.DisplayMember = "TLCMTCFG_Description";
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns[1].DisplayIndex = 1;

                // 03
                //=============================================
                oCmbBoxB.Name = "ProductCost_Style";
                oCmbBoxB.ValueType = typeof(Int32);
                oCmbBoxB.HeaderText = "Style";
                oCmbBoxB.DataPropertyName = DataT.Columns[2].ColumnName;
                oCmbBoxB.DataSource = _context.TLADM_Styles.ToList();
                oCmbBoxB.ValueMember = "Sty_Id";
                oCmbBoxB.DisplayMember = "Sty_Description";
                dataGridView1.Columns.Add(oCmbBoxB);
                dataGridView1.Columns[2].ReadOnly = false;
                dataGridView1.Columns[2].DisplayIndex = 2;

                // 04
                //=================================================
                oCmbBoxC.Name = "ProductCost_Colour";
                oCmbBoxC.ValueType = typeof(Int32);
                oCmbBoxC.HeaderText = "Colour";
                oCmbBoxC.DataPropertyName = DataT.Columns[3].ColumnName;
                oCmbBoxC.DataSource = _context.TLADM_Colours.ToList();
                oCmbBoxC.ValueMember = "Col_Id";
                oCmbBoxC.DisplayMember = "Col_Display";
                dataGridView1.Columns.Add(oCmbBoxC);
                dataGridView1.Columns[3].ReadOnly = false;
                dataGridView1.Columns[3].DisplayIndex = 3;

                // 05
                //==============================================
                oCmbBoxD.Name = "ProductCost_Size";
                oCmbBoxD.ValueType = typeof(Int32);
                oCmbBoxD.HeaderText = "Size";
                oCmbBoxD.DataPropertyName = DataT.Columns[4].ColumnName;
                oCmbBoxD.DataSource = _context.TLADM_Sizes.ToList();
                oCmbBoxD.ValueMember = "SI_Id";
                oCmbBoxD.DisplayMember = "SI_Description";
                dataGridView1.Columns.Add(oCmbBoxD);
                dataGridView1.Columns[4].ReadOnly = false;
                dataGridView1.Columns[4].DisplayIndex = 4;

                // 06
                //=======================================================
                oTxtBoxD.Name = "ProductCost_Cost";
                oTxtBoxD.ValueType = typeof(decimal);
                oTxtBoxD.HeaderText = "Production Cost";
                oTxtBoxD.DataPropertyName = DataT.Columns[5].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[5].ReadOnly = false;
                dataGridView1.Columns[5].DisplayIndex = 5;

                // 07 
                //=========================================================
                oTxtBoxE.Name = "ProductCost_Damage";
                oTxtBoxE.ValueType = typeof(decimal);
                oTxtBoxE.HeaderText = "Production Cost Damage";
                oTxtBoxE.DataPropertyName = DataT.Columns[6].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxE);
                dataGridView1.Columns[6].ReadOnly = false;
                dataGridView1.Columns[6].DisplayIndex = 6;

                //08
                //===========================================================
                oTxtBoxF.Name = "ProductCost_Loss";
                oTxtBoxF.ValueType = typeof(decimal);
                oTxtBoxF.HeaderText = "Production Cost Loss";
                oTxtBoxF.DataPropertyName = DataT.Columns[7].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxF);
                dataGridView1.Columns[7].ReadOnly = false;
                dataGridView1.Columns[7].DisplayIndex = 7;

                //========================================
                //
                //==================================================
                cmboCMT.DataSource = _context.TLADM_Departments.Where(x => x.Dep_IsCMT).OrderBy(x => x.Dep_Description).ToList();
                cmboCMT.DisplayMember = "Dep_Description";
                cmboCMT.ValueMember = "Dep_Id";
                cmboCMT.SelectedValue = -1;

                
                                                
                FormLoaded = true;


            }
            else if(TransNo == 49)
            {
                /*[TrxT_Pk][int] IDENTITY(1, 1) NOT NULL,
                  [TrxT_Description] [varchar] (50) NOT NULL,
                  [TrxT_Discontinued] [bit] NOT NULL,
                  [TrxT_DiscontinuedDate] [date] NULL,
	              [TrxT_Department_FK][int] NOT NULL,
                  [TrxT_Number] [int] NOT NULL,
                  [TrxT_FromWhse_FK] [int] NULL,
	              [TrxT_ToWhse_FK][int] NULL,
	              [TrxT_FinishedGoods_FK][int] NOT NULL,*/
                this.Text = "Transaction Types Update / Edit Facility";
               
                //===================================
                // 00
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "TTType_Id";
                column.Caption = "TransType Id Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
              

                //01
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "TTType_Description";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);

                // 02
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "TTType_Discontinued";
                column.Caption = "Discontinued";
                column.DefaultValue = false ;
                DataT.Columns.Add(column);
                
                // 03
                column = new DataColumn();
                column.DataType = typeof(DateTime);
                column.ColumnName = "TT_DiscontinuedDate";
                column.Caption = "Discontinued Date";
                column.DefaultValue = DBNull.Value;
                DataT.Columns.Add(column);
                //04
                
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TTType_Department";
                column.Caption = "Departments";
                column.DefaultValue = _context.TLADM_Departments.FirstOrDefault().Dep_Id;
                DataT.Columns.Add(column);
                //05
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TTType_TNumber";
                column.Caption = "TransNumber Production ";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
                //06
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TTType_FromWareHouse";
                column.Caption = "From WareHouse";
                column.DefaultValue = _context.TLADM_WhseStore.OrderBy(x => x.WhStore_Description).FirstOrDefault().WhStore_Id;
                DataT.Columns.Add(column);
                //07
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TTYpe_ToWareHouse";
                column.Caption = "To WareHouse";
                column.DefaultValue = _context.TLADM_WhseStore.OrderBy(x => x.WhStore_Description).FirstOrDefault().WhStore_Id; ;
                DataT.Columns.Add(column);
                //08
                column = new DataColumn();
                column.DataType = typeof(int);
                column.ColumnName = "TTYpe_FinishedGoods";
                column.Caption = "Finished Goods";
                column.DefaultValue = _context.TLADM_FinishedGoods.OrderBy(x => x.Fin_Description).FirstOrDefault().Fin_Pk;
                DataT.Columns.Add(column);

                //=====================================================

                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns[0].Visible = false;

                oTxtBoxB.HeaderText = "Description";
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                oTxtBoxB.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;
                

                oChkBoxA.HeaderText = "Discontinued";
                oChkBoxA.ValueType = typeof(bool);
                oChkBoxA.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oChkBoxA);
                dataGridView1.Columns[2].DisplayIndex = 2;

                oTxtBoxC.HeaderText = "Descontinued Date";
                oTxtBoxC.DataPropertyName = DataT.Columns[3].ColumnName;
                oTxtBoxC.ValueType = typeof(DateTime);
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[3].DisplayIndex = 3;

                oCmbBoxA.HeaderText = "Departments";
                oCmbBoxA.DataPropertyName = DataT.Columns[4].ColumnName;
                oCmbBoxA.DataSource = _context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                oCmbBoxA.DisplayMember = "Dep_description";
                oCmbBoxA.ValueType = typeof(int);
                oCmbBoxA.ValueMember = "Dep_Id";
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns[4].DisplayIndex = 4;

                oTxtBoxD.HeaderText = "Trans Number";
                oTxtBoxD.DataPropertyName = DataT.Columns[5].ColumnName;
                oTxtBoxD.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[5].DisplayIndex = 5;

                oCmbBoxB.HeaderText = "From WareHouse";
                oCmbBoxB.DataPropertyName = DataT.Columns[6].ColumnName;
                oCmbBoxB.ValueType = typeof(int);
                oCmbBoxB.DataSource = _context.TLADM_WhseStore.OrderBy(x => x.WhStore_Description).ToList();
                oCmbBoxB.DisplayMember = "WhStore_Description";
                oCmbBoxB.ValueMember = "WhStore_Id";
                dataGridView1.Columns.Add(oCmbBoxB);
                dataGridView1.Columns[6].DisplayIndex = 6;

                oCmbBoxC.HeaderText = "To WareHouse";
                oCmbBoxC.DataPropertyName = DataT.Columns[7].ColumnName;
                oCmbBoxC.ValueType = typeof(int);
                oCmbBoxC.DataSource = _context.TLADM_WhseStore.OrderBy(x => x.WhStore_Description).ToList();
                oCmbBoxC.DisplayMember = "WhStore_Description";
                oCmbBoxC.ValueMember = "WhStore_Id";
                dataGridView1.Columns.Add(oCmbBoxC);
                dataGridView1.Columns[7].DisplayIndex = 7;

                oCmbBoxD.HeaderText = "Finished Goods";
                oCmbBoxD.DataPropertyName = DataT.Columns[8].ColumnName;
                oCmbBoxD.ValueType = typeof(int);
                oCmbBoxD.DataSource = _context.TLADM_FinishedGoods.OrderBy(x => x.Fin_Description).ToList();
                oCmbBoxD.DisplayMember = "Fin_Description";
                oCmbBoxD.ValueMember = "Fin_Pk";
                dataGridView1.Columns.Add(oCmbBoxD);
                dataGridView1.Columns[8].DisplayIndex = 8;

                var Existing = _context.TLADM_TranactionType.OrderBy(x => x.TrxT_Number).ToList();
                foreach(var Row in Existing)
                {
                    DataRow NewRow = DataT.NewRow();
                    NewRow[0] = Row.TrxT_Pk;
                    NewRow[1] = Row.TrxT_Description;
                    NewRow[2] = Row.TrxT_Discontinued;
                    if (Row.TrxT_DiscontinuedDate != null)
                    {
                        NewRow[3] = Row.TrxT_DiscontinuedDate;
                    }
                    else
                    {
                        NewRow[3] = DBNull.Value;
                    }
                    NewRow[4] = Row.TrxT_Department_FK;
                    NewRow[5] = Row.TrxT_Number;
                    NewRow[6] = Row.TrxT_FromWhse_FK;
                    NewRow[7] = Row.TrxT_ToWhse_FK;
                    NewRow[8] = Row.TrxT_FinishedGoods_FK;
                    DataT.Rows.Add(NewRow);

                }
            }
            else if (TransNo == 50)
            {
                this.Text = "Shift Definition";
                
                //======================
                // 00
                //==========================================
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "ShftDef_Id";
                column.Caption = "Shift Definition Primary Key";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
             
                //=================================
                //01
                //========================================
                column = new DataColumn();
                column.DataType = typeof(string);
                column.ColumnName = "ShftDef_Desc";
                column.Caption = "Description";
                column.DefaultValue = string.Empty;
                DataT.Columns.Add(column);
                
                //==========================
                // 02
                //==========================================
                column = new DataColumn();
                column.DataType = typeof(bool);
                column.ColumnName = "ShftDef_Dscontinued";
                column.Caption = "Discontinued";
                column.DefaultValue = false;
                DataT.Columns.Add(column);
                
                //==============================
                // 03
                //==============================================
                column = new DataColumn();
                column.DataType = typeof(DateTime);
                column.ColumnName = "Shft_DiscontinuedDate";
                column.Caption = "Date";
                column.DefaultValue = DBNull.Value;
                DataT.Columns.Add(column);
                
                //=============================
                //04
                //===================================
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Shft_Dept_Fk";
                column.Caption = "Department";
                column.DefaultValue = _context.TLADM_Departments.FirstOrDefault().Dep_Id;
                DataT.Columns.Add(column);
                
                //===================================
                //05
                //=======================================
                column = new DataColumn();
                column.DataType = typeof(TimeSpan);
                column.ColumnName = "Shft_Start";
                column.Caption = "Shift Start";
                DataT.Columns.Add(column);
                

                //==============================
                //06
                //===============================================
                column = new DataColumn();
                column.DataType = typeof(TimeSpan);
                column.ColumnName = "Shft_End";
                column.Caption = "Shift End";
                DataT.Columns.Add(column);

                //--------------------------------------------
                //
                //-----------------------------------------------------
                // 01
                oTxtBoxA.Name = "Shft_PK";
                oTxtBoxA.ValueType = typeof(Int32);
                oTxtBoxA.HeaderText = "Shift Primary Key";
                oTxtBoxA.DataPropertyName = DataT.Columns[0].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxA);
                dataGridView1.Columns[0].DisplayIndex = 0;
                dataGridView1.Columns[0].Visible = false;

                //--------------------------------------------
                //
                //-----------------------------------------------------
                // 02
                oTxtBoxB.Name = "Shft_Desc";
                oTxtBoxB.ValueType = typeof(Int32);
                oTxtBoxB.HeaderText = "Shift Description";
                oTxtBoxB.DataPropertyName = DataT.Columns[1].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxB);
                dataGridView1.Columns[1].DisplayIndex = 1;
                // 03
                oChkBoxA.Name = "Shft_Discontinued";
                oChkBoxA.ValueType = typeof(bool);
                oChkBoxA.HeaderText = "Shift Discontinued";
                oChkBoxA.DataPropertyName = DataT.Columns[2].ColumnName;
                dataGridView1.Columns.Add(oChkBoxA);
                dataGridView1.Columns[2].DisplayIndex = 2;

                // 04
                oTxtBoxC.Name = "Shft_DiscontinuedDate";
                oTxtBoxC.ValueType = typeof(DateTime);
                oTxtBoxC.HeaderText = "Shift Discontinued Date";
                oTxtBoxC.DataPropertyName = DataT.Columns[3].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxC);
                dataGridView1.Columns[3].DisplayIndex = 3;

                oCmbBoxA.Name = "Dept_Details";
                oCmbBoxA.ValueType = typeof(int);
                oCmbBoxA.HeaderText = "Department";
                oCmbBoxA.DataPropertyName = DataT.Columns[4].ColumnName;
                oCmbBoxA.DisplayMember = "Dep_Description";
                oCmbBoxA.ValueMember = "Dep_Id";
                oCmbBoxA.DataSource = _context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                dataGridView1.Columns.Add(oCmbBoxA);
                dataGridView1.Columns[4].DisplayIndex = 4;

                oTxtBoxD.Name = "Shft_Start";
                oTxtBoxD.ValueType = typeof(TimeSpan);
                oTxtBoxD.HeaderText = "Shift Start";
                oTxtBoxD.DataPropertyName = DataT.Columns[5].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxD);
                dataGridView1.Columns[5].DisplayIndex = 5;

                oTxtBoxE.Name = "Shft_End";
                oTxtBoxE.ValueType = typeof(TimeSpan);
                oTxtBoxE.HeaderText = "Shift End";
                oTxtBoxE.DataPropertyName = DataT.Columns[6].ColumnName;
                dataGridView1.Columns.Add(oTxtBoxE);
                dataGridView1.Columns[6].DisplayIndex = 6;

                var ExistingData = _context.TLADM_Shifts.ToList();

                foreach (var row in ExistingData)
                {
                    DataRow NewRow = DataT.NewRow();

                    NewRow[0] = row.Shft_Pk;
                    NewRow[1] = row.Shft_Description;
                    NewRow[2] = row.Shft_Discontinued;
                    if (row.Shft_DiscontinuedDate != null)
                    {
                        NewRow[3] = row.Shft_DiscontinuedDate;
                    }
                    else
                    {
                        NewRow[3] = DBNull.Value;
                    }
                    NewRow[4] = row.Shft_Dept_FK;
                    NewRow[5] = row.Shft_Start;
                    NewRow[6] = row.Shft_End;

                    DataT.Rows.Add(NewRow);
                }
            }

            BindingSrc.DataSource = DataT;
            dataGridView1.DataSource = BindingSrc;
            var Cols = dataGridView1.Columns.Count;




        }

        private void frmNewForm_Load(object sender, EventArgs e)
        {
            FormLoaded = false;


            if (TransNumber == 1)
            {
                label1.Visible = true;
                cmboCustomers.Visible = true;

                cmboCustomers.DataSource = _context.TLADM_CustomerFile.Where(x => !x.Cust_Blocked).OrderBy(x => x.Cust_Description).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedValue = -1;
            }

            FormLoaded = true;

        }

        private void oDateTimePicker_OnTextChange(object sender, EventArgs e)
        {
            if (TransNumber == 50)
            {
                // Saving the 'Selected Date on Calendar' into DataGridView current cell  
                //============================================================================
                dataGridView1.CurrentCell.Value = dateTimePicker1.Value.TimeOfDay;
                oDateTimePicker_CloseUp(this, null);
                base.OnTextChanged(e);
            }
        }
        void oDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            if (TransNumber == 50 )
            {
                // Hiding the control after use
                //=========================================
                if (dateTimePicker1 != null)
                {
                    dateTimePicker1.Visible = false;
                }
            }
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            var CurrentRow = oDgv.CurrentRow;
            var ButtonCell = oDgv.CurrentCell is DataGridViewButtonCell;
            if (oDgv.Focused && ButtonCell)
            {
                var DataRow = oDgv.CurrentRow;
                if (TransNumber == 1)
                {
                    if (DataT.Rows.Count > 0)
                    {
                        try
                        {
                            int number = DataT.Rows[e.RowIndex].Field<int>(0);
                            if (number <= 0)
                            {
                                using (DialogCenteringService svces = new DialogCenteringService(this))
                                {
                                    MessageBox.Show("Master Record incomplete");
                                    return;
                                }
                            }

                            frmTest test = new frmTest(TransNumber, e.ColumnIndex, number);
                            test.ShowDialog();
                        }
                        catch (Exception ex)
                        { return; }
                    }
                }
                else if (TransNumber == 4)
                {
                    if (e.ColumnIndex == 17)
                    {
                        if (DataT.Rows.Count > 0)
                        {
                            try
                            {
                                int number = DataT.Rows[e.RowIndex].Field<int>(0);
                                if (number <= 0)
                                {
                                    using (DialogCenteringService svces = new DialogCenteringService(this))
                                    {
                                        MessageBox.Show("Master Record incomplete");
                                        return;
                                    }
                                }
                                // AS20240308 - v5.0.0.123: use correct key for yarn allocations 
                                int key = (int)oDgv.CurrentRow.Cells[0].Value;
                                frmTest test = new frmTest(TransNumber, e.ColumnIndex, key);
                                test.ShowDialog();
                            }
                            catch (Exception ex)
                            { return; }
                        }
                    }
                    else if (e.ColumnIndex == 26)
                    {
                        using (DialogCenteringService svces = new DialogCenteringService(this))
                        {
                            MessageBox.Show("Facility no longer required");
                            return;
                        }
                    }
                }
            }
            else
            {
             
                if(TransNumber == 50 && (e.ColumnIndex == 5 | e.ColumnIndex == 6))
                {
                    // Initialize the dateTimePicker1.
                    dateTimePicker1 = new DateTimePicker();
                    // Adding the dateTimePicker1 into DataGridView.   
                    dataGridView1.Controls.Add(dateTimePicker1);
                    // Setting the format i.e. mm/dd/yyyy)
                    dateTimePicker1.Format = DateTimePickerFormat.Time;
                    dateTimePicker1.CustomFormat = "dd:HH:ss";
                    dateTimePicker1.ShowUpDown = true;
                    // Create retangular area that represents the display area for a cell.
                    System.Drawing.Rectangle Rectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    // Setting area for dateTimePicker1.
                    dateTimePicker1.Size = new Size(Rectangle.Width, Rectangle.Height);
                    dateTimePicker1.Location = new System.Drawing.Point(Rectangle.X, Rectangle.Y);

                    // An event attached to dateTimePicker1 which is fired when any date is selected.
                    dateTimePicker1.TextChanged += new EventHandler(oDateTimePicker_OnTextChange);
                    // An event attached to dateTimePicker1 which is fired when DateTimeControl is closed.
                    dateTimePicker1.CloseUp += new EventHandler(oDateTimePicker_CloseUp);
                }
           

            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            var CurrentRow = oDgv.CurrentRow;
            var CheckCell = oDgv.CurrentCell is DataGridViewCheckBoxCell;
            if (CheckCell)
            {
                if ((e.ColumnIndex == 2 && TransNumber != 7) 
                     || (e.ColumnIndex == 3 && TransNumber == 7))
                {
                    var ColNumb = e.ColumnIndex;

                    bool Chked = (bool)oDgv.CurrentCell.EditedFormattedValue;

                    if (Chked)
                    {
                        oDgv.Columns[ColNumb + 1].DefaultCellStyle.Format = "dd/MM/yyyy";
                        dtp = new DateTimePicker();
                        CurrentRow.Cells[ColNumb + 1].Value = DateTime.Now.Date;

                        dtp.Format = DateTimePickerFormat.Custom;
                        dtp.CustomFormat = "dd/MM/yyyy";
                        dataGridView1.Controls.Add(dtp);
                        System.Drawing.Rectangle Rectangle = dataGridView1.GetCellDisplayRectangle(ColNumb + 1, e.RowIndex, true);
                        dtp.Size = new Size(Rectangle.Width, Rectangle.Height);
                        dtp.Location = new System.Drawing.Point(Rectangle.X, Rectangle.Y);

                                               
                        dtp.TextChanged += new EventHandler(dtp_OnTextChange);
                        dtp.CloseUp += new EventHandler(dtp_CloseUp);
                        dtp.Visible = true;
                        base.OnTextChanged(e);
                    }
                    else
                    {

                        var DataRow = oDgv.CurrentRow;
                        DataRow.Cells[ColNumb].Value = false;
                        DataRow.Cells[ColNumb + 1].Value = DBNull.Value;
                        dtp_CloseUp(this, null);
                        base.OnTextChanged(e);
                    }
                }
            }
           
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var oDlg = (DataGridView)sender;
            DataGridViewCell Cell = null;
            if (oDlg != null && FormLoaded)
            {
                Cell = oDlg.CurrentCell;
                if (oDlg.Focused && Cell is DataGridViewTextBoxCell)
                {
                    if (TransNumber == 1)
                    {

                        if (Cell.ColumnIndex == 8 ||
                            Cell.ColumnIndex == 11 ||
                            Cell.ColumnIndex == 14 ||
                            Cell.ColumnIndex == 16 ||
                            Cell.ColumnIndex == 17)
                        {
                            e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                            e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                            e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                            e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                        }
                    }
                    else if (TransNumber == 2)
                    {
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
                    else if (TransNumber == 4)
                    {
                    }
                    else if (TransNumber == 12)
                    {
                    }
                    else if (TransNumber == 5 || TransNumber == 13)
                    {
                        if (Cell.ColumnIndex == 4)
                        {
                            e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                            e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                            e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                            e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                        }
                    }
                    else if (TransNumber == 17)
                    {

                    }
                    else if (TransNumber == 21)
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
                    else if (TransNumber == 50)
                    {
                        if (Cell.ColumnIndex == 5 ||
                            Cell.ColumnIndex == 6)
                        {
                            e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                            e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                            e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                            e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                        }
                    }
                }
            }
        }

        private void dtp_OnTextChange(object sender, EventArgs e)
        {
            var CurrentRow = dataGridView1.CurrentRow;

            if (TransNumber != 7 && TransNumber != 50)
            {
                CurrentRow.Cells[3].Value = (DateTime)dtp.Value;

                if (CurrentRow.Cells[3].Value != null && CurrentRow.Cells[2].Value == null)
                {
                    FormLoaded = false;
                    CurrentRow.Cells[2].Value = true;
                    FormLoaded = true;
                }
            }
            else if (TransNumber == 7)
            {
                CurrentRow.Cells[4].Value = (DateTime)dtp.Value;

                if (CurrentRow.Cells[4].Value != null && CurrentRow.Cells[3].Value == null)
                {
                    FormLoaded = false;
                    CurrentRow.Cells[3].Value = true;
                    FormLoaded = true;
                }
            }
        }
            
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            /*
            var oDgv = sender as DataGridView;
            if (TransNumber != 7 && TransNumber != 50 && e.ColumnIndex == 3)
            {
                if ((bool)oDgv.CurrentRow.Cells[e.ColumnIndex - 1].Value == true)
                {
                    oDgv.CurrentRow.Cells[e.ColumnIndex].Value = dtp.Value;
                }
                else
                {
                    oDgv.CurrentRow.Cells[e.ColumnIndex].Value = DBNull.Value;
                }

            }
            else if (TransNumber == 7 && e.ColumnIndex == 4)
            {
                if ((bool)oDgv.CurrentRow.Cells[e.ColumnIndex - 1].Value == true)
                {
                    oDgv.CurrentRow.Cells[e.ColumnIndex].Value = dtp.Value;
                }
                else
                {
                    oDgv.CurrentRow.Cells[e.ColumnIndex].Value = DBNull.Value;
                }
            }
            else if (TransNumber == 50 && (e.ColumnIndex == 5 || e.ColumnIndex == 6))
            {
                if ((bool)oDgv.CurrentRow.Cells[e.ColumnIndex - 1].Value == true)
                {
                    oDgv.CurrentCell.Value = dtp.Value;
                }
                else
                {
                    oDgv.CurrentCell.Value = DBNull.Value;
                }
            }
            */
            
        }

        void dtp_CloseUp(object sender, EventArgs e)
        {
            if (dtp != null)
            {
                dtp.Visible = false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button oBtn = sender as System.Windows.Forms.Button;
            bool Add;
            bool lSave = false;

            if (oBtn != null && FormLoaded)
            {
                if (TransNumber == 1)
                {
                    var SelCustomer = (TLADM_CustomerFile)cmboCustomers.SelectedItem;
                    if (SelCustomer == null)
                    {
                        MessageBox.Show("Please select a customer from the drop down box");
                        return;
                    }

                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;
                        TLADM_Styles NewStyles = new TLADM_Styles();

                        if (Row.Field<int>(0) != 0)
                        {
                            NewStyles = _context.TLADM_Styles.Find(Row.Field<int>(0));
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            Add = true;
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }

                        NewStyles.Sty_Description = Row.Field<string>(1);
                        NewStyles.Sty_Customer_Fk = SelCustomer.Cust_Pk;
                        NewStyles.Sty_Discontinued = Row.Field<bool>(2);
                        if (Row.Field<bool>(2) && Row[3] != DBNull.Value)
                        {
                            NewStyles.Sty_Discontinued_Date = (DateTime)Row.Field<DateTime>(3);
                        }

                        NewStyles.Sty_ChkMandatory = Row.Field<bool>(4);
                        NewStyles.Sty_PastelNo = Row.Field<int>(5);
                        NewStyles.Sty_PastelCode = Row.Field<string>(6);
                        NewStyles.Sty_CottonFactor = Row.Field<int>(7);
                        NewStyles.Sty_Bags = Row.Field<int>(8);
                        NewStyles.Sty_Buttons = Row.Field<bool>(9);
                        NewStyles.Sty_BoughtIn = Row.Field<bool>(10);
                        NewStyles.Sty_Equiv = Row.Field<bool>(11);
                        NewStyles.Sty_DisplayOrder = Row.Field<int>(12);
                        NewStyles.Sty_Units_Per_Hour = Row.Field<int>(13);
                        NewStyles.Sty_WorkWear = Row.Field<bool>(14);
                        NewStyles.Sty_PFD = Row.Field<bool>(15);

                        if (Add)
                        {
                            _context.TLADM_Styles.Add(NewStyles);
                        }
                    }
                }
                else if (TransNumber == 2)
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

                        if (Clrs.Col_Display != Clrs.Col_FinishedCode + " " + Clrs.Col_Description)
                        {
                            Clrs.Col_Display = Clrs.Col_FinishedCode + " " + Clrs.Col_Description;
                        }

                        if (Add)
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

                        Gr.TLGreige_Description = Row.Field<String>(1);
                        Gr.TLGriege_Discontinued = Row.Field<bool>(2);
                        Gr.TLGreige_Discontinued_Date = Row.Field<DateTime?>(3);
                        Gr.TLGreige_OldCode = Row.Field<int>(4);
                        Gr.TLGreige_PowerN = Row.Field<int>(5);
                        Gr.TLGreige_BarCode = Row.Field<bool>(6);
                        Gr.TLGreige_FabricWeight_FK = Row.Field<int>(7);
                        Gr.TLGreige_FabricWidth_FK = Row.Field<int>(8);
                        Gr.TLGreige_Machine_FK = Row.Field<int>(9);
                        Gr.TLGreige_ProductType_FK = Row.Field<int>(10);
                        Gr.TLGreige_Quality_FK = Row.Field<int>(11);
                        Gr.TLGreige_ROL = Row.Field<int>(12);
                        Gr.TLGreige_ROQ = Row.Field<int>(13);
                        Gr.TLGreige_ShowQty = Row.Field<bool>(14);
                        Gr.TLGreige_StockTakeFreq_FK = Row.Field<int>(15);
                        Gr.TLGreige_UOM_Fk = Row.Field<int>(16);
                        Gr.TLGreige_YarnPowerN = Row.Field<int>(17);
                        Gr.TLGreige_KgPerPiece = Row.Field<decimal>(18);
                        Gr.TLGreige_LatestSuggest = Row.Field<decimal>(19);
                        Gr.TLGreige_Meters = Row.Field<int>(20);
                        Gr.TLGreige_FaultsAllowed = Row.Field<int>(21);
                        Gr.TLGreige_IsBoughtIn = Row.Field<bool>(22);
                        Gr.TLGreige_IsLining = Row.Field<bool>(23);
                        Gr.TLGreige_CubicWeight = Row.Field<decimal>(24);
                        Gr.TLGreige_Body = Row.Field<bool>(25);

                        if (Add)
                        {
                            _context.TLADM_Griege.Add(Gr);
                        }
                    }
                }
                else if (TransNumber == 5)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;

                        TLADM_FabWidth Quality = null;
                        var PrimKey = Row.Field<int>(0);
                        if (PrimKey > 0)
                        {
                            Quality = _context.TLADM_FabWidth.Find(PrimKey);
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            Quality = new TLADM_FabWidth();
                            Add = true;
                            if (!lSave)
                            {

                                lSave = true;
                            }
                        }

                        Quality.FW_Id = Convert.ToInt32(PrimKey); ;
                        Quality.FW_Description = Row.Field<string>(1);
                        Quality.FW_Discontinued = (bool)Row.Field<bool>(2);
                        if (Row.Field<bool>(2) && Row[3] != DBNull.Value)
                        {
                            Quality.FW_Discontinued_Date = Convert.ToDateTime(Row.Field<DateTime>(3));
                        }
                        else
                        {
                            Quality.FW_Discontinued_Date = null;
                        }
                        Quality.FW_Calculation_Value = Row.Field<Int32>(4);

                        if (Add)
                        {
                            _context.TLADM_FabWidth.Add(Quality); ;
                        }
                    }

                }
                else if (TransNumber == 7)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;

                        TLADM_UOM Quality = null;
                        var PrimKey = Row.Field<int>(0);
                        if (PrimKey > 0)
                        {
                            Quality = _context.TLADM_UOM.Find(PrimKey);
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            Quality = new TLADM_UOM();
                            Add = true;
                            if (!lSave)
                            {

                                lSave = true;
                            }
                        }

                        Quality.UOM_ShortCode = Row.Field<string>(1);
                        Quality.UOM_Description = Row.Field<string>(2);
                        Quality.UOM_Discontinued = (bool)Row.Field<bool>(3);
                        if (Row.Field<bool>(3) && Row[4] != DBNull.Value)
                        {
                            Quality.UOM_DiscontinuedDate = Convert.ToDateTime(Row.Field<DateTime>(4));
                        }
                        else
                        {
                            Quality.UOM_DiscontinuedDate = null;
                        }


                        if (Add)
                        {
                            _context.TLADM_UOM.Add(Quality); ;
                        }
                    }

                }
                else if (TransNumber == 8)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;

                        TLADM_Sizes Quality = new TLADM_Sizes();
                        var PrimKey = Row.Field<int>(0);
                        if (PrimKey > 0)
                        {
                            Quality = _context.TLADM_Sizes.Find(PrimKey);
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            Add = true;
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        Quality.SI_Description = Row.Field<string>(1);
                        Quality.SI_Discontinued = Row.Field<bool>(2);

                        if (Row.Field<bool>(2) && Row[3] != DBNull.Value)
                        {
                            Quality.SI_Discontinued_Date = Convert.ToDateTime(Row.Field<DateTime>(3));
                        }
                        else
                        {
                            Quality.SI_Discontinued_Date = null;
                        }

                        Quality.SI_PowerN = Row.Field<int>(4);
                        Quality.SI_FinishedCode = Row.Field<string>(5);
                        Quality.SI_ColNumber = Row.Field<int>(6);
                        Quality.SI_PastelNo = Row.Field<int>(7);
                        Quality.SI_DisplayOrder = Row.Field<int>(8);
                        Quality.SI_Adult = Row.Field<bool>(9);
                        Quality.SI_ContiSize = Row.Field<int>(10);
                        Quality.SI_Display = Row.Field<string>(11);

                        if (Add)
                        {
                            _context.TLADM_Sizes.Add(Quality);
                        }
                    }
                }
                else if (TransNumber == 9)
                {

                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;

                        TLADM_Labels Quality = new TLADM_Labels();
                        var PrimKey = Row.Field<int>(0);
                        if (PrimKey > 0)
                        {
                            Quality = _context.TLADM_Labels.Find(PrimKey);
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            Add = true;
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }

                        Quality.Lbl_Id = Convert.ToInt32(PrimKey); ;
                        Quality.Lbl_Description = Row.Field<string>(1);
                        Quality.Lbl_Discontinued = (bool)Row.Field<bool>(2);
                        if (Row.Field<bool>(2) && Row[3] != DBNull.Value)
                        {
                            Quality.Lbl_Discontinued_Date = Convert.ToDateTime(Row.Field<DateTime>(3));
                        }
                        else
                        {
                            Quality.Lbl_Discontinued_Date = null;
                        }
                        Quality.Lbl_Customer_FK = Row.Field<Int32>(4);

                        if (Add)
                        {
                            var RecCnt = DataT.Rows.Count;
                            Quality.Lbl_PowerN = (int)Math.Pow(2.00D, RecCnt);
                            _context.TLADM_Labels.Add(Quality);
                        }
                    }

                }
                else if (TransNumber == 11)
                {
                    foreach (DataRow PTRow in DataT.Rows)
                    {
                        Add = false;

                        TLADM_DepartmentsArea Types = new TLADM_DepartmentsArea();
                        if (PTRow.Field<int>(0) != 0)
                        {
                            Types = _context.TLADM_DepartmentsArea.Find(PTRow.Field<int>(0));
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

                        Types.DeptA_Description = PTRow.Field<String>(1);
                        Types.DeptA_Discontinued = PTRow.Field<bool>(2);
                        if (PTRow.Field<bool>(2) && PTRow[3] != DBNull.Value)
                        {
                            Types.DeptA_DiscontinuedDate = Convert.ToDateTime(PTRow.Field<DateTime>(3));
                        }
                        else
                        {

                            Types.DeptA_DiscontinuedDate = null;
                        }
                        Types.DeptA_Dep_Fk = PTRow.Field<int>(4);

                        if (Add)
                        {
                            _context.TLADM_DepartmentsArea.Add(Types);
                        }
                    }
                }
                else if (TransNumber == 12)
                {
                    foreach (DataRow PTRow in DataT.Rows)
                    {
                        Add = false;

                        TLADM_FabricProduct Types = new TLADM_FabricProduct();
                        if (PTRow.Field<int>(0) != 0)
                        {
                            Types = _context.TLADM_FabricProduct.Find(PTRow.Field<int>(0));
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

                        Types.FP_Description = PTRow.Field<String>(1);
                        Types.FP_Discontinued = PTRow.Field<bool>(2);
                        if (PTRow.Field<bool>(2) && PTRow[3] != DBNull.Value)
                        {
                            Types.FP_Discontinued_Date = Convert.ToDateTime(PTRow.Field<DateTime>(3));
                        }

                        if (Add)
                        {
                            var RecCnt = DataT.Rows.Count;
                            Types.FP_PowerN = (int)Math.Pow(2.00D, RecCnt);
                            _context.TLADM_FabricProduct.Add(Types);
                        }
                    }
                }
                else if (TransNumber == 13)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;

                        TLADM_FabricWeight Quality = null;
                        var PrimKey = Row.Field<int>(0);
                        if (PrimKey > 0)
                        {
                            Quality = _context.TLADM_FabricWeight.Find(PrimKey);
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            Quality = new TLADM_FabricWeight();
                            Add = true;
                            if (!lSave)
                            {

                                lSave = true;
                            }
                        }

                        Quality.FWW_Id = Convert.ToInt32(PrimKey); ;
                        Quality.FWW_Description = Row.Field<string>(1);
                        Quality.FWW_Discontinued = (bool)Row.Field<bool>(2);
                        if (Row.Field<bool>(2) && Row[3] != DBNull.Value)
                        {
                            Quality.FWW_Discontinued_Date = Convert.ToDateTime(Row.Field<DateTime>(3));
                        }
                        else
                        {
                            Quality.FWW_Discontinued_Date = null;
                        }
                        Quality.FWW_Calculation_Value = Row.Field<Int32>(4);

                        if (Add)
                        {
                            _context.TLADM_FabricWeight.Add(Quality); ;
                        }
                    }
                }
                else if (TransNumber == 14)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;

                        TLADM_GreigeQuality Quality = null;
                        var PrimKey = Row.Field<int>(0);
                        if (PrimKey != -1)
                        {
                            Quality = _context.TLADM_GreigeQuality.Find(PrimKey);
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            Quality = new TLADM_GreigeQuality();
                            Add = true;
                            if (!lSave)
                            {

                                lSave = true;
                            }
                        }

                        Quality.GQ_Pk = Convert.ToInt32(PrimKey); ;
                        Quality.GQ_Description = Row.Field<string>(1);
                        Quality.GQ_Discontinued = (bool)Row.Field<bool>(2);
                        //if ((bool)Row.Field<bool>(2) && Row.Field<Nullable<DateTime>>(3) != null)
                        if ((bool)Row.Field<bool>(2) && Row[3] != DBNull.Value)
                        {
                            Quality.GQ_Discontinued_Date = Convert.ToDateTime(Row.Field<DateTime>(3));
                        }
                        else
                        {
                            Quality.GQ_Discontinued_Date = null;
                        }
                        Quality.GQ_PowerN = Row.Field<Int32>(4);

                        if (Add)
                        {
                            _context.TLADM_GreigeQuality.Add(Quality); ;
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
                        if (Row.Field<bool>(2) && Row[3] != DBNull.Value)
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
                        PanAttrib.Pan_Single_Colour_FK = null;
                        if (Row[7] != DBNull.Value)
                        {
                            PanAttrib.Pan_Single_Colour_FK = Row.Field<int>(7);
                        }

                        PanAttrib.Pan_Style_FK = Row.Field<int>(8);

                        if (Add)
                        {
                            _context.TLADM_PanelAttributes.Add(PanAttrib);
                        }
                    }
                }
                else if (TransNumber == 18)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;
                        TLADM_ProductionLoss ExistingData = new TLADM_ProductionLoss();

                        if (Row.Field<int>(0) != 0)
                        {
                            ExistingData = _context.TLADM_ProductionLoss.Find(Row.Field<int>(0));
                            if (!lSave)
                                lSave = true;
                        }
                        else
                        {
                            Add = true;
                            if (!lSave)
                                lSave = true;
                        }

                        ExistingData.TLProdLoss_Dept_Fk = Row.Field<int>(1);
                        ExistingData.TLProdLoss_Percent = Row.Field<int>(2);
                        ExistingData.TLProdLoss_Kg = Row.Field<int>(3);

                        if (Add)
                        {
                            _context.TLADM_ProductionLoss.Add(ExistingData);
                        }
                    }

                }
                else if (TransNumber == 21)
                {
                    var SelectedCMT = (TLADM_Departments)cmboCMT.SelectedItem;

                    if (SelectedCMT == null)
                    {
                        using (DialogCenteringService svc = new DialogCenteringService(this))
                        {
                            MessageBox.Show("Please select a CMT facility");
                            return;
                        }
                    }

                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;
                        var Costs_Pk = Row.Field<int>(0);
                        TLCMT_ProductionCosts ProdCosts = new TLCMT_ProductionCosts();

                        if (Costs_Pk != 0)
                        {
                            ProdCosts = _context.TLCMT_ProductionCosts.Find(Costs_Pk);
                            if (ProdCosts != null)
                            {
                                ProdCosts.CMTP_Style_FK = Row.Field<int>(2);
                                ProdCosts.CMTP_Colour_FK = Row.Field<int>(3);
                                ProdCosts.CMTP_Size_FK = Row.Field<int>(4);
                                ProdCosts.CMTP_Production_Cost = Row.Field<Decimal>(5);
                                ProdCosts.CMTP_Production_Damage = Row.Field<Decimal>(6);
                                ProdCosts.CMTP_Production_Loss = Row.Field<Decimal>(7);

                                if (!lSave)
                                    lSave = true;
                            }
                        }
                        else
                        {
                            ProdCosts = new TLCMT_ProductionCosts();
                            ProdCosts.CMTP_CMTFacility_FK = SelectedCMT.Dep_Id;
                            ProdCosts.CMTP_CMTLineNo_FK = Row.Field<Int32>(1); ;
                            ProdCosts.CMTP_Style_FK = Row.Field<int>(2);
                            ProdCosts.CMTP_Colour_FK = Row.Field<int>(3);
                            ProdCosts.CMTP_Size_FK = Row.Field<int>(4);
                            ProdCosts.CMTP_Production_Cost = Row.Field<Decimal>(5);
                            ProdCosts.CMTP_Production_Damage = Row.Field<Decimal>(6);
                            ProdCosts.CMTP_Production_Loss = Row.Field<Decimal>(7);

                            Add = true;
                            if (!lSave)
                                lSave = true;
                        }

                        if (Add)
                        {
                            _context.TLCMT_ProductionCosts.Add(ProdCosts);
                        }
                    }

                }
                else if (TransNumber == 49)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;
                        TLADM_TranactionType ExistingData = new TLADM_TranactionType();

                        if (Row.Field<int>(0) != 0)
                        {
                            ExistingData = _context.TLADM_TranactionType.Find(Row.Field<int>(0));
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            Add = true;
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }

                        ExistingData.TrxT_Description = Row.Field<string>(1);
                        ExistingData.TrxT_Discontinued = Row.Field<bool>(2);
                        ExistingData.TrxT_DiscontinuedDate = Row.Field<DateTime?>(3);
                        ExistingData.TrxT_Department_FK = Row.Field<int>(4);
                        ExistingData.TrxT_Number = Row.Field<int>(5);
                        ExistingData.TrxT_FromWhse_FK = Row.Field<int>(6);
                        ExistingData.TrxT_ToWhse_FK = Row.Field<int>(7);
                        ExistingData.TrxT_FinishedGoods_FK = Row.Field<int>(8);
                        if (Add)
                        {
                            _context.TLADM_TranactionType.Add(ExistingData);
                        }

                    }
                }
                else if (TransNumber == 50)
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        Add = false;
                        TLADM_Shifts ExistingData = new TLADM_Shifts();

                        if (Row.Field<int>(0) != 0)
                        {
                            ExistingData = _context.TLADM_Shifts.Find(Row.Field<int>(0));
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }
                        else
                        {
                            Add = true;
                            if (!lSave)
                            {
                                lSave = true;
                            }
                        }

                        ExistingData.Shft_Description = Row.Field<string>(1);
                        ExistingData.Shft_Discontinued = Row.Field<bool>(2);
                        ExistingData.Shft_DiscontinuedDate = Row.Field<DateTime ?>(3);
                        ExistingData.Shft_Dept_FK = Row.Field<int>(4);
                        ExistingData.Shft_Start = Row.Field<TimeSpan>(5);
                        ExistingData.Shft_End = Row.Field<TimeSpan>(6);

                        if(Add)
                        {
                            _context.TLADM_Shifts.Add(ExistingData);
                        }

                    }
                }

                try
                {
                    _context.SaveChanges();
                    using (DialogCenteringService svc = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Data successfully saved to data base");

                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void frmNewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && FormLoaded)
            {
                DataT.Rows.Clear();

                var Cust = (TLADM_CustomerFile)oCmbo.SelectedItem;

                if (Cust != null)
                {
                    var Styles = _context.TLADM_Styles.Where(x => x.Sty_Customer_Fk == Cust.Cust_Pk).OrderBy(x => x.Sty_Description).ToList();
                    foreach (var Style in Styles)
                    {
                        DataRow NewRow = DataT.NewRow();
                        NewRow[0] = Style.Sty_Id;
                        NewRow[1] = Style.Sty_Description;
                        NewRow[2] = Style.Sty_Discontinued;
                        if (Style.Sty_Discontinued_Date != null)
                        {
                            NewRow[3] = Style.Sty_Discontinued_Date;
                        }
                        else
                        {
                            NewRow[3] = DBNull.Value;
                        }
                        NewRow[4] = Style.Sty_ChkMandatory;
                        NewRow[5] = Style.Sty_PastelNo;
                        NewRow[6] = Style.Sty_PastelCode;
                        NewRow[7] = Style.Sty_CottonFactor;
                        NewRow[8] = Style.Sty_Bags;
                        NewRow[9] = Style.Sty_Buttons;
                        NewRow[10] = Style.Sty_BoughtIn;
                        NewRow[11] = Style.Sty_Equiv;
                        NewRow[12] = Style.Sty_DisplayOrder;
                        NewRow[13] = Style.Sty_Units_Per_Hour;
                        NewRow[14] = Style.Sty_WorkWear;
                        NewRow[15] = Style.Sty_PFD;

                        DataT.Rows.Add(NewRow);
                    }
                }

            }
        }

        private void cmboFactConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && FormLoaded)
            {
                var Dep = (TLADM_Departments)cmboCMT.SelectedItem;
                if (Dep == null)
                {
                    using (DialogCenteringService svces = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a CMT Facility from the drop down box");
                        return;
                    }

                }

                DataT.Rows.Clear();
                
                var SelFactConfig = (TLCMT_FactConfig)oCmbo.SelectedItem;
                IList<TLCMT_ProductionCosts> DataEntries = _context.TLCMT_ProductionCosts.Where(x => x.CMTP_CMTFacility_FK == Dep.Dep_Id && x.CMTP_CMTLineNo_FK == SelFactConfig.TLCMTCFG_Pk).ToList();

                foreach (var DataEntry in DataEntries)
                {
                    DataRow row = DataT.NewRow();
                    row[0] = DataEntry.CMTP_Pk;
                    row[1] = DataEntry.CMTP_CMTLineNo_FK;
                    row[2] = DataEntry.CMTP_Style_FK;
                    row[3] = DataEntry.CMTP_Colour_FK;
                    row[4] = DataEntry.CMTP_Size_FK;
                    row[5] = DataEntry.CMTP_Production_Cost;
                    row[6] = DataEntry.CMTP_Production_Damage;
                    row[7] = DataEntry.CMTP_Production_Loss;

                    DataT.Rows.Add(row);
                }
            }
            */

        }
        private void cmboCMT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && FormLoaded)
            {
                var Selected = (TLADM_Departments)oCmbo.SelectedItem;
                if (Selected != null)
                {
                    FormLoaded = false;
                    DataT.Rows.Clear();

                    //cmboFactConfig.DataSource = _context.TLCMT_FactConfig.Where(x => x.TLCMTCFG_Department_FK == Selected.Dep_Id).OrderBy(x => x.TLCMTCFG_LineNo).ToList();
                    //cmboFactConfig.SelectedValue = -1;
                    //

                    IList<TLCMT_ProductionCosts> DataEntries = _context.TLCMT_ProductionCosts.Where(x => x.CMTP_CMTFacility_FK == Selected.Dep_Id).ToList();

                    foreach (var DataEntry in DataEntries)
                    {
                        DataRow row = DataT.NewRow();
                        row[0] = DataEntry.CMTP_Pk;
                        row[1] = DataEntry.CMTP_CMTLineNo_FK;
                        row[2] = DataEntry.CMTP_Style_FK;
                        row[3] = DataEntry.CMTP_Colour_FK;
                        row[4] = DataEntry.CMTP_Size_FK;
                        row[5] = DataEntry.CMTP_Production_Cost;
                        row[6] = DataEntry.CMTP_Production_Damage;
                        row[7] = DataEntry.CMTP_Production_Loss;

                        DataT.Rows.Add(row);
                    }
                    FormLoaded = true;

                }
                else
                {
                    using (DialogCenteringService svces = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a CMT facility");
                        return;
                    }
                }
            }
        }

        private void chkExport_CheckedChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.CheckBox oChk = sender as System.Windows.Forms.CheckBox;
            if (oChk != null && FormLoaded && oChk.Checked)
            {
                var SelectedCMT = (TLADM_Departments)cmboCMT.SelectedItem;
                if (SelectedCMT == null)
                {
                    using (DialogCenteringService svces = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a CMT Facility");
                        FormLoaded = false;
                        if (oChk.Checked)
                        {
                            oChk.Checked = false;
                        }
                        FormLoaded = true;
                        return;
                    }
                }

                /*
                DataT.Rows.Clear();
                IList<TLCMT_ProductionCosts> DataEntries = _context.TLCMT_ProductionCosts.Where(x => x.CMTP_CMTFacility_FK == SelectedCMT.Dep_Id).ToList();

                foreach (var DataEntry in DataEntries)
                {
                    DataRow row = DataT.NewRow();
                    row[0] = DataEntry.CMTP_Pk;
                    row[1] = DataEntry.CMTP_CMTLineNo_FK;
                    row[2] = DataEntry.CMTP_Style_FK;
                    row[3] = DataEntry.CMTP_Colour_FK;
                    row[4] = DataEntry.CMTP_Size_FK;
                    row[5] = DataEntry.CMTP_Production_Cost;
                    row[6] = DataEntry.CMTP_Production_Damage;
                    row[7] = DataEntry.CMTP_Production_Loss;

                    DataT.Rows.Add(row);
                }
                */

                /*Set up work book, work sheets, and excel application*/
                //==========================================================
                Microsoft.Office.Interop.Excel.Application oexcel = new Microsoft.Office.Interop.Excel.Application();

                string path = AppDomain.CurrentDomain.BaseDirectory;
                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook obook = oexcel.Workbooks.Add(misValue);
                Microsoft.Office.Interop.Excel.Worksheet osheet = new Microsoft.Office.Interop.Excel.Worksheet();

                osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Sheets["Sheet1"];
                int colIndex = 0;
                int rowIndex = 1;
                // must not forget that the output file now has a column for the key 
                //=================================================================
                foreach (DataColumn dc in DataT.Columns)
                {
                    colIndex++;
                    if (colIndex == 1)
                    {
                        osheet.Cells[1, colIndex] = "RecordKey";
                    }
                    else if (colIndex == 2)
                    {
                        osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                        osheet.Cells[1, colIndex] = "Line Details";
                    }
                    else if (colIndex == 3)
                    {
                        osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                        osheet.Cells[1, colIndex] = "Styles";
                    }
                    else if (colIndex == 4)
                    {
                        osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                        osheet.Cells[1, colIndex] = "Colour";
                    }
                    else if (colIndex == 5)
                    {
                        osheet.Columns[colIndex].NumberFormat = "@";   // column as a text
                        osheet.Cells[1, colIndex] = "Size";
                    }
                    else
                    {
                        osheet.Cells[1, colIndex] = dc.ColumnName;
                    }
                }

                foreach (DataRow dr in DataT.Rows)
                {
                    rowIndex++;
                    colIndex = 0;

                    foreach (DataColumn dc in DataT.Columns)
                    {
                        colIndex++;

                        if (colIndex == 1)
                        {
                            osheet.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                        }
                        else if (colIndex == 2)
                        {
                            var ColRowIndex = (int)dr[dc.ColumnName];
                            osheet.Cells[rowIndex, colIndex] = _context.TLCMT_FactConfig.FirstOrDefault(s => s.TLCMTCFG_Pk == ColRowIndex).TLCMTCFG_Description;
                        }
                        else if (colIndex == 3)
                        {
                            var ColRowIndex = (int)dr[dc.ColumnName];
                            osheet.Cells[rowIndex, colIndex] = _context.TLADM_Styles.FirstOrDefault(s => s.Sty_Id == ColRowIndex).Sty_Description;
                        }
                        else if (colIndex == 4)
                        {
                            var ColRowIndex = (int)dr[dc.ColumnName];
                            osheet.Cells[rowIndex, colIndex] = _context.TLADM_Colours.FirstOrDefault(s => s.Col_Id == ColRowIndex).Col_Display;
                        }
                        else if (colIndex == 5)
                        {
                            var ColRowIndex = (int)dr[dc.ColumnName];
                            osheet.Cells[rowIndex, colIndex] = _context.TLADM_Sizes.FirstOrDefault(s => s.SI_id == ColRowIndex).SI_Description;
                        }
                        else
                        {
                            osheet.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                        }
                    }
                }

                osheet.Columns.AutoFit();

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = "C:\\Temp";
                sfd.Filter = "Excel |*.xlsx";

                DialogResult xdr = sfd.ShowDialog();
                if (xdr == DialogResult.OK)
                {
                    try
                    {
                        obook.SaveAs(sfd.FileName);
                        obook.Saved = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

                //=====================================================
                //Release and terminate excel
                //====================================================================
                obook.Close();
                oexcel.Quit();
                chkExport.Checked = false;
                releaseObject(osheet);
                releaseObject(obook);
                releaseObject(oexcel);
                GC.Collect();
                using (DialogCenteringService svces = new DialogCenteringService(this))
                {
                    MessageBox.Show("Export finished successfully");
                }
            }
        }

        private void chkImport_CheckedChanged(object sender, EventArgs e)
        {
            var oChkImport = sender as System.Windows.Forms.CheckBox;
            if (oChkImport != null && FormLoaded && oChkImport.Checked)
            {
                var SelectedCMT = (TLADM_Departments)cmboCMT.SelectedItem;
                if (SelectedCMT == null)
                {
                    using (DialogCenteringService svces = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a CMT Facility");
                        FormLoaded = false;
                        if (oChkImport.Checked)
                        {
                            oChkImport.Checked = false;
                        }
                        FormLoaded = true;
                        return;
                    }
                }

                // Create an instance of the open file dialog box.
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                // Set filter options and filter index.
                openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;

                openFileDialog1.Multiselect = false;

                // Call the ShowDialog method to show the dialog box.
                DialogResult res = openFileDialog1.ShowDialog(this);
                // Process input if the user clicked OK.
                if (res == DialogResult.OK)
                {
                    System.IO.Stream fileStream = openFileDialog1.OpenFile();
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                    {
                        string[] input = reader.ReadToEnd().Split('\r');
                        foreach (var line in input)
                        {
                            string[] row = line.Split('\t');
                            if (row[0].Contains("RecordKey"))
                                continue;

                            if (row.Count() > 7)
                            {
                                var RecKey = Convert.ToInt32(row[0]);
                                var DataR = DataT.Rows.Find(RecKey);
                                if (DataR != null)
                                {
                                    DataR[5] = Convert.ToDecimal(row[5]);
                                    DataR[6] = Convert.ToDecimal(row[6]);
                                    DataR[7] = Convert.ToDecimal(row[7]);
                                }
                                else
                                {
                                    using (DialogCenteringService svs = new DialogCenteringService(this))
                                    {
                                        MessageBox.Show("Data key not found. Insure correct spreadsheet imported");
                                    }
                                    return;
                                }
                            }                          

                        }

                        FormLoaded = false;
                        chkImport.Checked = false;
                        FormLoaded = true;
                    }
                }

            }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var oDlg = sender as DataGridView; 
        }

      
    }
}

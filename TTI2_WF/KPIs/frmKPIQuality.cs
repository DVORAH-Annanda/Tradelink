using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Spinning;
using Knitting;
using DyeHouse;
using Cutting;
using CMT;
using Utilities;

using TTI2_WF.KPIs.Spinning;
using TTI2_WF.KPIs.Knitting;

namespace TTI2_WF
{
    public partial class frmKPIQuality : Form
    {
        TTI2_WF.KPIs.Spinning.YarnProduction yarnProduction;
        DateTime KPIFromDate;
        DateTime KPIToDate;

        public frmKPIQuality()
        {
            this.Cursor = Cursors.WaitCursor;

            InitializeComponent();            

            this.rbtnQuality.Checked = true;

            KPIFromDate = dtpDateFromKPI.Value = DateTime.Now.AddDays(-1).Date.AddHours(7);
            KPIFromDate = dtpDateFromKPI.Value = DateTime.Now.AddDays(-30).Date.AddHours(7); //Testing
            KPIToDate = dtpDateToKPI.Value = DateTime.Now.Date.AddHours(7);

            TreeNode spinningMainNode = new TreeNode();
            spinningMainNode.Name = "spinningMainNode";
            spinningMainNode.Text = "Spinning";
            this.tvwKPIQuality.Nodes.Add(spinningMainNode);

            TreeNode knittingMainNode = new TreeNode();
            knittingMainNode.Name = "knittingMainNode";
            knittingMainNode.Text = "Knitting";
            this.tvwKPIQuality.Nodes.Add(knittingMainNode);

            TreeNode knittingGreigeQAMachinesByDateNode = new TreeNode();
            knittingGreigeQAMachinesByDateNode.Name = "knittingGreigeQAMachinesByDateNode";
            knittingGreigeQAMachinesByDateNode.Text = "Greige QA by Machine (Total Pieces Knit by Date)";
            this.tvwKPIQuality.Nodes["knittingMainNode"].Nodes.Add(knittingGreigeQAMachinesByDateNode);

            TreeNode knittingGreigeQAGradesPerMachineNode = new TreeNode();
            knittingGreigeQAGradesPerMachineNode.Name = "knittingGreigeQAGradesPerMachineNode";
            knittingGreigeQAGradesPerMachineNode.Text = "Greige QA by Machine (Total Pieces Knit by Grade)";            
            this.tvwKPIQuality.Nodes["knittingMainNode"].Nodes.Add(knittingGreigeQAGradesPerMachineNode);

            TreeNode dyeingMainNode = new TreeNode();
            dyeingMainNode.Name = "dyeingMainNode";
            dyeingMainNode.Text = "Dyeing";
            this.tvwKPIQuality.Nodes.Add(dyeingMainNode);

            TreeNode cuttingMainNode = new TreeNode();
            cuttingMainNode.Name = "cuttingMainNode";
            cuttingMainNode.Text = "Cutting";
            this.tvwKPIQuality.Nodes.Add(cuttingMainNode);

            TreeNode cmtMainNode = new TreeNode();
            cmtMainNode.Name = "cmtMainNode";
            cmtMainNode.Text = "CMT";
            this.tvwKPIQuality.Nodes.Add(cmtMainNode);

            //Knitting - default
            tvwKPIQuality.SelectedNode = knittingGreigeQAMachinesByDateNode;
            tvwKPIQuality.HideSelection = false;
            tvwKPIQuality.Focus();

            grpCardMachineQualityCheck.Visible = false;
            dgvByMachineByGradeQA.Visible = true;

            this.Cursor = Cursors.Default;
        }

        private void tvwKPIQuality_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                //grpCardMachineQualityCheck
                this.lblKPI.Visible = false;
                this.lblKPIDescription.Visible = false;


                if (this.tvwKPIQuality.SelectedNode.Parent == null)
                {
                    this.lblQualityKPI.Text = this.tvwKPIQuality.SelectedNode.Text.ToString();
                    if (tvwKPIQuality.SelectedNode.Text.ToString() == "Spinning")
                    {
                        this.lblKPI.Text = "Click a button to view the breakdown(report)";
                        this.lblKPI.Visible = true;
                        grpCardMachineQualityCheck.Visible = true;
                        dgvByMachineByGradeQA.Visible = false;
                    }
                    else
                        grpCardMachineQualityCheck.Visible = false;

                    if (tvwKPIQuality.SelectedNode.Text.ToString() != "Knitting")
                        dgvByMachineByGradeQA.Visible = false;
                }
                else
                {
                   
                    //tvwKPIQuality.SelectedNode.BackColor = SystemColors.Highlight;
                    //tvwKPIQuality.SelectedNode.ForeColor = SystemColors.HighlightText;
                    this.lblQualityKPI.Text = this.tvwKPIQuality.SelectedNode.Parent.Text.ToString();
                    this.lblKPI.Text = this.tvwKPIQuality.SelectedNode.Text.ToString();
                    this.lblKPI.Visible = true;
                    if (this.lblKPI.Text == "Greige QA by Machine (Total Pieces Knit by Date)")
                    {
                        grpCardMachineQualityCheck.Visible = false;
                        //this.lblKPIDescription.Text = "Greige QA by Machine (Total Pieces Knit by Date)";
                        //this.lblKPIDescription.Visible = true;

                        dgvByMachineByGradeQA.Visible = true;

                        //QueryParams
                        KnitQueryParameters queryParms = new KnitQueryParameters();
                        KnitRepository repo = new Knitting.KnitRepository();
                        YarnReportOptions YarnOpts = new YarnReportOptions();

                        queryParms.Customers.Add(repo.LoadCustomer(24));
                        queryParms.FromDate = KPIFromDate;
                        queryParms.ToDate = KPIToDate;

                        DataTable machinesTable = new DataTable("Machines");
                        DataColumn dtColumn;
                        DataRow dataRow;

                        // Create id column  
                        dtColumn = new DataColumn();
                        dtColumn.DataType = typeof(Int32);
                        dtColumn.ColumnName = "id";
                        dtColumn.AutoIncrement = true;
                        dtColumn.ReadOnly = true;
                        dtColumn.Unique = true;
                        machinesTable.Columns.Add(dtColumn);

                        dtColumn = new DataColumn();
                        dtColumn.DataType = typeof(DateTime);
                        dtColumn.ColumnName = "Date";
                        dtColumn.ReadOnly = true;
                        dtColumn.Unique = false;
                        machinesTable.Columns.Add(dtColumn);

                        using (var context = new TTI2Entities())
                        {
                            var Data = repo.QAGreigeProduction(queryParms);

                            var Grps = Data.GroupBy(x => new { x.GreigeP_Machine_FK }).ToList();
                            List<string> MachineList = new List<string>();
                            foreach (var prodGrp in Grps)
                            {
                                var Pk = prodGrp.First().GreigeP_Machine_FK;
                                var Machine = context.TLADM_MachineDefinitions.Find(Pk);
                                
                                if(!MachineList.Contains(Machine.MD_MachineCode))
                                    MachineList.Add(Machine.MD_MachineCode);
                                MachineList = MachineList.OrderBy(m => m).ToList();
                            }

                            foreach (string machine in MachineList)
                            {
                                dtColumn = new DataColumn();
                                dtColumn.DataType = typeof(String);
                                dtColumn.ColumnName = machine;
                                dtColumn.ReadOnly = true;
                                dtColumn.Unique = false;
                                machinesTable.Columns.Add(dtColumn);
                            }

                            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
                            PrimaryKeyColumns[0] = machinesTable.Columns["id"];
                            machinesTable.PrimaryKey = PrimaryKeyColumns;

                            dgvByMachineByGradeQA.DataSource = machinesTable;

                            dgvByMachineByGradeQA.Columns["id"].Visible = false;
                            dgvByMachineByGradeQA.Sort(this.dgvByMachineByGradeQA.Columns["Date"], ListSortDirection.Ascending);

                            foreach (var prodGrp in Grps)
                            {
                                dataRow = machinesTable.NewRow();
                                var Pk = prodGrp.FirstOrDefault().GreigeP_Machine_FK;
                                var Machine = context.TLADM_MachineDefinitions.Find(Pk);
                                if (Machine != null)
                                {
                                    dataRow[Machine.MD_MachineCode] = prodGrp.Count();

                                    dataRow["Date"] = prodGrp.FirstOrDefault().GreigeP_InspDate;
                                }

                                //dataRow["aPiecesWithWarning"] = prodGrp.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Count();
                                //dataRow["bGradePieces"] = prodGrp.Where(x => x.GreigeP_Grade == "B").Count();
                                //dataRow["cGradePieces"] = prodGrp.Where(x => x.GreigeP_Grade == "C").Count();

                                machinesTable.Rows.Add(dataRow);
                            }
                        }
                    }
                    if (this.lblKPI.Text == "Greige QA by Machine (Total Pieces Knit by Grade)")
                    {
                        //this.lblKPIDescription.Text = "Greige QA by Machine (Total Pieces Knit by Date))";
                        //this.lblKPIDescription.Visible = true;
                        grpCardMachineQualityCheck.Visible = false;
                        dgvByMachineByGradeQA.Visible = true;

                        //QueryParams
                        KnitQueryParameters queryParms = new KnitQueryParameters();
                        KnitRepository repo = new Knitting.KnitRepository();
                        YarnReportOptions YarnOpts = new YarnReportOptions();

                        queryParms.Customers.Add(repo.LoadCustomer(24));
                        queryParms.FromDate = KPIFromDate;
                        queryParms.ToDate = KPIToDate;

                        DataTable machinesbyGradeTable = new DataTable("MachinesByGrade");
                        DataColumn dtColumn;
                        DataRow dataRow;

                        // Create id column  
                        dtColumn = new DataColumn();
                        dtColumn.DataType = typeof(Int32);
                        dtColumn.ColumnName = "id";
                        dtColumn.AutoIncrement = true;
                        dtColumn.ReadOnly = true;
                        dtColumn.Unique = true;
                        machinesbyGradeTable.Columns.Add(dtColumn);

                        dtColumn = new DataColumn();
                        dtColumn.DataType = typeof(String);
                        dtColumn.ColumnName = "knittingMachine";
                        dtColumn.ReadOnly = true;
                        dtColumn.Unique = true;
                        machinesbyGradeTable.Columns.Add(dtColumn);

                        dtColumn = new DataColumn();
                        dtColumn.DataType = typeof(String);
                        dtColumn.ColumnName = "totalPiecesInspected";
                        dtColumn.ReadOnly = true;
                        dtColumn.Unique = false;
                        machinesbyGradeTable.Columns.Add(dtColumn);

                        dtColumn = new DataColumn();
                        dtColumn.DataType = typeof(String);
                        dtColumn.ColumnName = "aGradePieces";
                        dtColumn.AutoIncrement = false;
                        dtColumn.ReadOnly = true;
                        dtColumn.Unique = false;
                        machinesbyGradeTable.Columns.Add(dtColumn);

                        dtColumn = new DataColumn();
                        dtColumn.DataType = typeof(String);
                        dtColumn.ColumnName = "aPiecesWithWarning";
                        dtColumn.AutoIncrement = false;
                        dtColumn.ReadOnly = true;
                        dtColumn.Unique = false;
                        machinesbyGradeTable.Columns.Add(dtColumn);

                        dtColumn = new DataColumn();
                        dtColumn.DataType = typeof(String);
                        dtColumn.ColumnName = "bGradePieces";
                        dtColumn.AutoIncrement = false;
                        dtColumn.ReadOnly = true;
                        dtColumn.Unique = false;
                        machinesbyGradeTable.Columns.Add(dtColumn);

                        dtColumn = new DataColumn();
                        dtColumn.DataType = typeof(String);
                        dtColumn.ColumnName = "cGradePieces";
                        dtColumn.AutoIncrement = false;
                        dtColumn.ReadOnly = true;
                        dtColumn.Unique = false;
                        machinesbyGradeTable.Columns.Add(dtColumn);

                        DataColumn[] PrimaryKeyColumns = new DataColumn[1];
                        PrimaryKeyColumns[0] = machinesbyGradeTable.Columns["id"];
                        machinesbyGradeTable.PrimaryKey = PrimaryKeyColumns;

                        dgvByMachineByGradeQA.DataSource = machinesbyGradeTable;

                        dgvByMachineByGradeQA.Columns["id"].Visible = false;
                        dgvByMachineByGradeQA.Columns["knittingMachine"].HeaderText = "Knitting Machine";
                        dgvByMachineByGradeQA.Columns["totalPiecesInspected"].HeaderText = "Total Pieces Inspected";
                        dgvByMachineByGradeQA.Columns["aGradePieces"].HeaderText = "A Grade Pieces";
                        dgvByMachineByGradeQA.Columns["aPiecesWithWarning"].HeaderText = "A Pieces with Warning";
                        dgvByMachineByGradeQA.Columns["bGradePieces"].HeaderText = "B Grade Pieces";
                        dgvByMachineByGradeQA.Columns["cGradePieces"].HeaderText = "C Grade Pieces";
                        dgvByMachineByGradeQA.Sort(this.dgvByMachineByGradeQA.Columns["knittingMachine"], ListSortDirection.Ascending);
                        using (var context = new TTI2Entities())
                        {
                            var Data = repo.QAGreigeProduction(queryParms);

                            var Grps = Data.GroupBy(x => new { x.GreigeP_Machine_FK }).ToList();
                            int i = 0;
                            foreach (var prodGrp in Grps)
                            {

                                var Pk = prodGrp.FirstOrDefault().GreigeP_Machine_FK;
                                var Machine = context.TLADM_MachineDefinitions.Find(Pk);
                                dataRow = machinesbyGradeTable.NewRow();
                                if (Machine != null)
                                    dataRow["KnittingMachine"] = Machine.MD_MachineCode;


                                dataRow["totalPiecesInspected"] = prodGrp.Count(); //

                                dataRow["aGradePieces"] = prodGrp.Where(x => x.GreigeP_Grade == "A" && !x.GreigeP_WarningMessage).Count();

                                dataRow["aPiecesWithWarning"] = prodGrp.Where(x => x.GreigeP_Grade == "A" && x.GreigeP_WarningMessage).Count();
                                dataRow["bGradePieces"] = prodGrp.Where(x => x.GreigeP_Grade == "B").Count();
                                dataRow["cGradePieces"] = prodGrp.Where(x => x.GreigeP_Grade == "C").Count();

                                machinesbyGradeTable.Rows.Add(dataRow);
                            }
                        }
                    }
                    else
                    {
                        //this.lblKPI.Visible = false;
                        //this.lblKPIDescription.Visible = false;
                    }
                }
            }
            catch { }
        }

        private void rbtnQuality_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnProduction.Checked)
            {
                this.Cursor = Cursors.WaitCursor;
                new frmKPI().Show();
                this.Close();
            }
        }

        private void DtpDateFromKPI_ValueChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null)
            {

                KPIFromDate = dtpDateFromKPI.Value.Date.AddHours(7);

                if (KPIFromDate <= KPIToDate)
                {
                    lblDataLoading.Text = "Loading Data...";
                    lblDataLoading.Refresh();

                    UpdateProgressBar("Card Averages per Period", 50);
                    for (int i = 0; i < 6; i++)
                    {
                        GetCardQAMeasurement(i);
                    }
                    //GetCard1QAMeasurement();
                    //UpdateProgressBar("Card 2 Avg per Period", 50);
                    //GetCard2QAMeasurement();
                    //UpdateProgressBar("Yarn Production", 12);
                    //GetTotalYarnProduced();
                    //ShowTotalYarnProducedPerMachineChart();
                    ////GetRSBQAMeasurementTotal();
                    ////GetSpinningMachineTotal();
                    //UpdateProgressBar("Greige Production", 25);
                    //GetTotalGreigeProduced();
                    //ShowTotalGreigeProducedPerGradeChart("All Machines");
                    //UpdateProgressBar("Dye Production", 45);
                    //GetFabricDyedTotals();
                    ////CreateFabricDyedTotalsNormalised();
                    //UpdateProgressBar("Cut Production", 85);
                    //GetTotalCuttingProduced();
                    //UpdateProgressBar("CMT Production", 95);
                    //GetCMTProductionTotals();
                    UpdateProgressBar("Data loaded", 100);
                }
            }

            if (prgBarKPI.Value == 100)
                lblDataLoading.Text = "Data Loaded";
            else
                lblDataLoading.Text = "Data Loading...";
            this.Cursor = Cursors.Arrow;
        }

        private void DtpDateToKPI_ValueChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null)
            {
                KPIToDate = dtpDateToKPI.Value.Date.AddHours(7);


                if (KPIFromDate <= KPIToDate)
                {
                    lblDataLoading.Text = "Loading Data...";
                    lblDataLoading.Refresh();
                    //MessageBox.Show(lblDataLoading.Text);

                    //cmbMachines.SelectedItem = "All Machines";

                    UpdateProgressBar("Card Averages per Period", 50);
                    for (int i = 0; i < 6; i++)
                    {
                        GetCardQAMeasurement(i);
                    }
                    //UpdateProgressBar("Yarn Production", 12);
                    //GetTotalYarnProduced();
                    //ShowTotalYarnProducedPerMachineChart();
                    ////GetRSBQAMeasurementTotal();
                    ////GetSpinningMachineTotal();
                    //UpdateProgressBar("Greige Production", 25);
                    //GetTotalGreigeProduced();
                    //ShowTotalGreigeProducedPerGradeChart("All Machines");
                    //UpdateProgressBar("Dye Production", 45);
                    //GetFabricDyedTotals();
                    ////CreateFabricDyedTotalsNormalised();
                    //UpdateProgressBar("Cut Production", 85);
                    //GetTotalCuttingProduced();
                    //UpdateProgressBar("CMT Production", 95);
                    //GetCMTProductionTotals();
                    UpdateProgressBar("Data loaded", 100);
                }
            }

            if (prgBarKPI.Value == 100)
                lblDataLoading.Text = "Data Loaded";
            else
                lblDataLoading.Text = "Data Loading...";
            this.Cursor = Cursors.Arrow;
        }

        private void GetCardQAMeasurement(int i)
        {
            IList<TLSPN_QAMeasurements> Transactions = null;

            using (var context = new TTI2Entities())
            {
                SpinningQueryParameters QueryParms = new SpinningQueryParameters();
                QueryParms.LowerTolerance = 4410;
                QueryParms.UpperTolerance = 4590;
                QueryParms.MeasurementOpt = 1;
                QueryParms.FromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString()); ;
                QueryParms.ToDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString()); 

                SpinningRepository repo = new SpinningRepository();
                
                switch (i)
                {
                    case 0:
                        QueryParms.Machines.Add(repo.LoadMachine(92));
                        Transactions = repo.QAMeasurementQuery(QueryParms).ToList();
                        decimal avg92 = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();
                        CreateCard1QAMeasurementButton(avg92);
                        break;
                    case 1:
                        QueryParms.Machines.Add(repo.LoadMachine(93));
                        Transactions = repo.QAMeasurementQuery(QueryParms).ToList();
                        decimal avg93 = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();
                        CreateCard2QAMeasurementButton(avg93);
                        break;
                    case 2:
                        QueryParms.Machines.Add(repo.LoadMachine(94));
                        Transactions = repo.QAMeasurementQuery(QueryParms).ToList();
                        decimal avg94 = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();
                        CreateCard3QAMeasurementButton(avg94);
                        break;
                    case 3:
                        QueryParms.Machines.Add(repo.LoadMachine(95));
                        Transactions = repo.QAMeasurementQuery(QueryParms).ToList();
                        decimal avg95 = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();
                        CreateCard4QAMeasurementButton(avg95);
                        break;
                    case 4:
                        QueryParms.Machines.Add(repo.LoadMachine(96));
                        Transactions = repo.QAMeasurementQuery(QueryParms).ToList();
                        decimal avg96 = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();
                        CreateRSB1QAMeasurementButton(avg96);
                        break;
                    case 5:
                        QueryParms.Machines.Add(repo.LoadMachine(97));
                        Transactions = repo.QAMeasurementQuery(QueryParms).ToList();
                        decimal avg97 = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();
                        CreateRSB2QAMeasurementButton(avg97);
                        break;
                    default:
                        QueryParms.Machines.Add(repo.LoadMachine(92));
                        Transactions = repo.QAMeasurementQuery(QueryParms).ToList();
                        decimal avg = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();
                        CreateCard1QAMeasurementButton(avg);
                        break;
                }
                //QueryParms.Machines.Add(repo.LoadMachine(92));
                
            }

            //decimal avg = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();

            //CreateCard1QAMeasurementButton(avg);
        }

        private void CreateCard1QAMeasurementButton(decimal card1Avg)
        {
            //foreach (Button btn in groupBox9.Controls.OfType<Button>())
            //{
            //    if (btn.Name == "btnCardQAMeasurementTotal")
            //    {
            //        groupBox9.Controls.Remove(btn);
            //    }
            //}

            Button btnCard1QAMeasurement = new Button();
            btnCard1QAMeasurement.Name = "btnCard1QAMeasurement";
            btnCard1QAMeasurement.Width = 190;
            btnCard1QAMeasurement.Height = 50;
            btnCard1QAMeasurement.Location = new Point(5, 20);
            btnCard1QAMeasurement.Click += (sender, EventArgs) => { btnCardQAMeasurement_Click(sender, EventArgs, 92); };

            btnCard1QAMeasurement.Text = "";
            Bitmap bmp = new Bitmap(btnCard1QAMeasurement.ClientRectangle.Width, btnCard1QAMeasurement.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnCard1QAMeasurement.BackColor);

                string line1 = "Card 1 Avg";
                string line2 = Math.Round(card1Avg, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnCard1QAMeasurement.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Green, btnCard1QAMeasurement.ClientRectangle, SF);
                }
            }
            btnCard1QAMeasurement.Image = bmp;
            btnCard1QAMeasurement.ImageAlign = ContentAlignment.MiddleCenter;

            grpCardMachineQualityCheck.Controls.Add(btnCard1QAMeasurement);
        }

        private void GetCard2QAMeasurement()
        {
            IList<TLSPN_QAMeasurements> Transactions = null;

            using (var context = new TTI2Entities())
            {
                SpinningQueryParameters QueryParms = new SpinningQueryParameters();
                QueryParms.LowerTolerance = 4410;
                QueryParms.UpperTolerance = 4590;
                QueryParms.OutSideTolerance = true;
                QueryParms.MeasurementOpt = 1;
                QueryParms.FromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString()); ;
                QueryParms.ToDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString()); 
                
                SpinningRepository repo = new SpinningRepository();
                QueryParms.Machines.Add(repo.LoadMachine(93));
                Transactions = repo.QAMeasurementQuery(QueryParms).ToList();
            }

            decimal avg = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();

            CreateCard2QAMeasurementButton(avg);
        }

        private void CreateCard2QAMeasurementButton(decimal card2Avg)
        {
            foreach (Button btn in grpCardMachineQualityCheck.Controls.OfType<Button>())
            {
                if (btn.Name == "btnCard2QAMeasurement")
                {
                    grpCardMachineQualityCheck.Controls.Remove(btn);
                }
            }

            Button btnCard2QAMeasurement = new Button();
            btnCard2QAMeasurement.Name = "btnCard2QAMeasurement";
            btnCard2QAMeasurement.Width = 190;
            btnCard2QAMeasurement.Height = 50;
            btnCard2QAMeasurement.Location = new Point(5, 72);
            btnCard2QAMeasurement.Click += (sender, EventArgs) => { btnCardQAMeasurement_Click(sender, EventArgs, 93); };

            btnCard2QAMeasurement.Text = "";
            Bitmap bmp = new Bitmap(btnCard2QAMeasurement.ClientRectangle.Width, btnCard2QAMeasurement.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnCard2QAMeasurement.BackColor);

                string line1 = "Card 2 Avg";
                string line2 = Math.Round(card2Avg, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnCard2QAMeasurement.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Green, btnCard2QAMeasurement.ClientRectangle, SF);
                }
            }
            btnCard2QAMeasurement.Image = bmp;
            btnCard2QAMeasurement.ImageAlign = ContentAlignment.MiddleCenter;

            grpCardMachineQualityCheck.Controls.Add(btnCard2QAMeasurement);
        }

        private void CreateCard3QAMeasurementButton(decimal cardAvg)
        {
            foreach (Button btn in grpCardMachineQualityCheck.Controls.OfType<Button>())
            {
                if (btn.Name == "btnCard3QAMeasurement")
                {
                    grpCardMachineQualityCheck.Controls.Remove(btn);
                }
            }

            Button btnCard3QAMeasurement = new Button();
            btnCard3QAMeasurement.Name = "btnCard3QAMeasurement";
            btnCard3QAMeasurement.Width = 190;
            btnCard3QAMeasurement.Height = 50;
            btnCard3QAMeasurement.Location = new Point(5, 124);
            btnCard3QAMeasurement.Click += (sender, EventArgs) => { btnCardQAMeasurement_Click(sender, EventArgs, 94); };

            btnCard3QAMeasurement.Text = "";
            Bitmap bmp = new Bitmap(btnCard3QAMeasurement.ClientRectangle.Width, btnCard3QAMeasurement.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnCard3QAMeasurement.BackColor);

                string line1 = "Card 3 Avg";
                string line2 = Math.Round(cardAvg, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnCard3QAMeasurement.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Green, btnCard3QAMeasurement.ClientRectangle, SF);
                }
            }
            btnCard3QAMeasurement.Image = bmp;
            btnCard3QAMeasurement.ImageAlign = ContentAlignment.MiddleCenter;

            grpCardMachineQualityCheck.Controls.Add(btnCard3QAMeasurement);
        }

        private void CreateCard4QAMeasurementButton(decimal cardAvg)
        {
            foreach (Button btn in grpCardMachineQualityCheck.Controls.OfType<Button>())
            {
                if (btn.Name == "btnCard4QAMeasurement")
                {
                    grpCardMachineQualityCheck.Controls.Remove(btn);
                }
            }

            Button btnCard4QAMeasurement = new Button();
            btnCard4QAMeasurement.Name = "btnCard4QAMeasurement";
            btnCard4QAMeasurement.Width = 190;
            btnCard4QAMeasurement.Height = 50;
            btnCard4QAMeasurement.Location = new Point(5, 176);
            btnCard4QAMeasurement.Click += (sender, EventArgs) => { btnCardQAMeasurement_Click(sender, EventArgs, 95); };

            btnCard4QAMeasurement.Text = "";
            Bitmap bmp = new Bitmap(btnCard4QAMeasurement.ClientRectangle.Width, btnCard4QAMeasurement.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnCard4QAMeasurement.BackColor);

                string line1 = "Card 4 Avg";
                string line2 = Math.Round(cardAvg, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnCard4QAMeasurement.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Green, btnCard4QAMeasurement.ClientRectangle, SF);
                }
            }
            btnCard4QAMeasurement.Image = bmp;
            btnCard4QAMeasurement.ImageAlign = ContentAlignment.MiddleCenter;

            grpCardMachineQualityCheck.Controls.Add(btnCard4QAMeasurement);
        }

        private void CreateRSB1QAMeasurementButton(decimal cardAvg)
        {
            foreach (Button btn in grpCardMachineQualityCheck.Controls.OfType<Button>())
            {
                if (btn.Name == "btnRSB1QAMeasurement")
                {
                    grpCardMachineQualityCheck.Controls.Remove(btn);
                }
            }

            Button btnRSB1QAMeasurement = new Button();
            btnRSB1QAMeasurement.Name = "btnRSB1QAMeasurement";
            btnRSB1QAMeasurement.Width = 190;
            btnRSB1QAMeasurement.Height = 50;
            btnRSB1QAMeasurement.Location = new Point(5, 228);
            btnRSB1QAMeasurement.Click += (sender, EventArgs) => { btnCardQAMeasurement_Click(sender, EventArgs, 96); };

            btnRSB1QAMeasurement.Text = "";
            Bitmap bmp = new Bitmap(btnRSB1QAMeasurement.ClientRectangle.Width, btnRSB1QAMeasurement.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnRSB1QAMeasurement.BackColor);

                string line1 = "RSB 1 Avg";
                string line2 = Math.Round(cardAvg, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnRSB1QAMeasurement.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Green, btnRSB1QAMeasurement.ClientRectangle, SF);
                }
            }
            btnRSB1QAMeasurement.Image = bmp;
            btnRSB1QAMeasurement.ImageAlign = ContentAlignment.MiddleCenter;

            grpCardMachineQualityCheck.Controls.Add(btnRSB1QAMeasurement);
        }

        private void CreateRSB2QAMeasurementButton(decimal cardAvg)
        {
            foreach (Button btn in grpCardMachineQualityCheck.Controls.OfType<Button>())
            {
                if (btn.Name == "btnRSB2QAMeasurement")
                {
                    grpCardMachineQualityCheck.Controls.Remove(btn);
                }
            }

            Button btnRSB2QAMeasurement = new Button();
            btnRSB2QAMeasurement.Name = "btnRSB2QAMeasurement";
            btnRSB2QAMeasurement.Width = 190;
            btnRSB2QAMeasurement.Height = 50;
            btnRSB2QAMeasurement.Location = new Point(5, 280);
            btnRSB2QAMeasurement.Click += (sender, EventArgs) => { btnCardQAMeasurement_Click(sender, EventArgs, 97); };

            btnRSB2QAMeasurement.Text = "";
            Bitmap bmp = new Bitmap(btnRSB2QAMeasurement.ClientRectangle.Width, btnRSB2QAMeasurement.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnRSB2QAMeasurement.BackColor);

                string line1 = "RSB 2 Avg";
                string line2 = Math.Round(cardAvg, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnRSB2QAMeasurement.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Green, btnRSB2QAMeasurement.ClientRectangle, SF);
                }
            }
            btnRSB2QAMeasurement.Image = bmp;
            btnRSB2QAMeasurement.ImageAlign = ContentAlignment.MiddleCenter;

            grpCardMachineQualityCheck.Controls.Add(btnRSB2QAMeasurement);
        }
        /// <summary>
        /// Old Code Below - DELETE
        /// </summary>

        private void btnTotalCuttingProduced_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblDataLoading.Text = "Loading Data...";
            lblDataLoading.Refresh();
            UpdateProgressBar("Cut Production Detail Report Data", 18);
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                UpdateProgressBar("Cut Production Detail Report Data", 25);
                CutReportOptions repOptions = new CutReportOptions();
                repOptions.fromDate = KPIFromDate;
                repOptions.toDate = KPIToDate;
                //repOptions.toDate = repOptions.toDate.AddHours(23);

                UpdateProgressBar("Cut Production Detail Report Data", 45);
                frmCutViewRep vRep = new frmCutViewRep(5, repOptions);

                UpdateProgressBar("Cut Production Detail Report Data", 65);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;

                UpdateProgressBar("Cut Production Detail Report Data", 95);
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);                
            }
            UpdateProgressBar("Cut Production Detail Report Data", 100);
            lblDataLoading.Text = "Data Loaded";
            lblDataLoading.Refresh();
            this.Cursor = Cursors.Arrow;
        }

        private Button CreateButton(
            string name, int width, int height, int pointX, int pointY, string line1, string line2, Brush brushColour)
        {
            Button button = new Button();
            button.Name = name;
            button.Width = width;
            button.Height = height;
            button.Location = new Point(pointX, pointY);

            Bitmap bmp = new Bitmap(button.ClientRectangle.Width, button.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(button.BackColor);

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = button.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    //G.DrawString(line2, arial, brushColour, btnTotalYarnProduced.ClientRectangle, SF);
                }
            }
            button.Image = bmp;
            button.ImageAlign = ContentAlignment.MiddleCenter;

            return button;
        }

        private void GetTotalCuttingProduced()
        {
            IList<TLCUT_CutSheetReceipt> Existing = new List<TLCUT_CutSheetReceipt>();
            double total = 0;
            using (var context = new TTI2Entities())
            {
                try
                {
                    Existing = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_DateIntoPanelStore != null && (x.TLCUTSHR_DateIntoPanelStore >= KPIFromDate && x.TLCUTSHR_DateIntoPanelStore <= KPIToDate)).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                foreach (var row in Existing)
                {
                    //DataRow dr = dt.NewRow();
                    //dr[0] = 1;
                    var CS = context.TLCUT_CutSheet.Find(row.TLCUTSHR_CutSheet_FK);

                    if (CS != null)
                    {
                        if (CS.TLCutSH_Closed)
                            continue;

                        var CSDetail = context.TLCUT_CutSheetDetail.Where(x => x.TLCutSHD_CutSheet_FK == CS.TLCutSH_Pk).ToList();
                        if (CSDetail != null)
                            total = total + CSDetail.Sum(x => (double?)x.TLCUTSHD_NettWeight) ?? 0.00;
                        else
                            total = 0.00;
                    }
                }
            }

            //CreateTotalCuttingProducedButton(total);
        }

        private void GetTotalYarnProduced()
        {
            Spinning.DataSet1 ds1 = new Spinning.DataSet1();
            Spinning.DataSet12.DataTable1DataTable datatable1 = new Spinning.DataSet12.DataTable1DataTable();
            //Spinning.DataSet12.DataTable2DataTable datatable2 = new DataSet12.DataTable2DataTable();
            IList<TLSPN_YarnOrderPallets> Pallets = new List<TLSPN_YarnOrderPallets>();
            YarnProductionSel yarnProduction = new YarnProductionSel();

            using (var context = new TTI2Entities())
            {
                yarnProduction.fromDate = dtpDateFromKPI.Value;
                yarnProduction.toDate = dtpDateToKPI.Value;
                //yarnProduction.toDate = yarnProduction.toDate.AddHours(23.59);
                Pallets = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_DatePacked >= yarnProduction.fromDate && x.YarnOP_DatePacked <= yarnProduction.toDate && x.YarnOP_Complete).ToList();
                foreach (var row in Pallets)
                {
                    Spinning.DataSet12.DataTable1Row nr = datatable1.NewDataTable1Row();
                    nr.Date = (DateTime)row.YarnOP_DatePacked;
                    var Order = context.TLSPN_YarnOrder.Find(row.YarnOP_YarnOrder_FK);
                    if (Order != null)
                    {
                        nr.YarnOrder = Order.YarnO_OrderNumber;
                        var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == Order.Yarno_YarnType_FK).FirstOrDefault();
                        if (YarnType != null)
                        {
                            nr.TexCount = YarnType.YA_TexCount;
                            nr.Twist = YarnType.YA_Twist;
                        }
                    }

                    nr.PalletNo = row.YarnOP_PalletNo.ToString();
                    nr.NoOfCones = row.YarnOP_NoOfCones;
                    nr.Gross = row.YarnOP_GrossWeight;
                    nr.Tare = row.YarnOP_TareWeight;
                    nr.Nett = nr.Gross - nr.Tare; // row.YarnOP_NettWeight;
                    nr.Key = 1;
                    datatable1.AddDataTable1Row(nr);

                }

                ds1.Tables.Add(datatable1);
                //ds1.Tables.Add(datatable2);

                var yProduction = ds1.Tables["datatable1"].AsEnumerable();

                decimal yarnProductionTotal = yProduction.Sum(datarow => datarow.Field<decimal>("Nett"));

                //CreateTotalYarnProducedButton(yarnProductionTotal);
            }
        }

        private void GetRSBQAMeasurementTotal()
        {
            Spinning.DataSet1 ds1 = new Spinning.DataSet1();
            Spinning.DataSet9.DataTable1DataTable datatable1 = new Spinning.DataSet9.DataTable1DataTable();
            Spinning.DataSet10.DataTable1DataTable datatable2 = new Spinning.DataSet10.DataTable1DataTable();
            Spinning.DataSet10.DataTable2DataTable datatable3 = new Spinning.DataSet10.DataTable2DataTable();
            int _YarnOrderNo = 0;
            string[] machdet = new string[5];
            IList<QAData> QAD = new List<QAData>();

            SpinningQueryParameters QueryParms = new SpinningQueryParameters();
            QueryParms.LowerTolerance = 4410;
            QueryParms.UpperTolerance = 4590;
            QueryParms.MeasurementOpt = 3;
            QueryParms.FromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString()); ;
            QueryParms.ToDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());

            SpinningRepository repo = new SpinningRepository();

            using (var context = new TTI2Entities())
            {
                var Existing = repo.QAMeasurementQuery(QueryParms).ToList();
                foreach (var rowDate in Existing)
                {
                    Spinning.DataSet9.DataTable1Row nr = datatable1.NewDataTable1Row();
                    var yofk = rowDate.YarnQA_YarnOrder_FK;
                    var YarnOrder = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == yofk).FirstOrDefault();
                    if (YarnOrder != null)
                    {
                        nr.YarnOrderNo = YarnOrder.YarnO_OrderNumber;
                        _YarnOrderNo = YarnOrder.YarnO_OrderNumber;
                        var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == YarnOrder.Yarno_YarnType_FK).FirstOrDefault();
                        if (YarnType != null)
                        {
                            nr.Tex = YarnType.YA_TexCount;
                        }
                    }

                    nr.Count = (decimal)rowDate.YarnQA_08H00;
                    nr.CV = (decimal)rowDate.YarnQA_10H00;
                    nr.Thin = (int)rowDate.YarnQA_12H00;
                    nr.Thick = (int)rowDate.YarnQA_14H00;
                    nr.Nep = (int)rowDate.YarnQA_16H00;
                    nr.IPI = (int)rowDate.YarnQA_18H00;
                    nr.Date = rowDate.YarnQA_Date;
                    var MachineDetail = context.TLADM_MachineDefinitions.Find(rowDate.YarnQA_MachineNo_FK);
                    if (MachineDetail != null)
                    {
                        nr.MachNo = MachineDetail.MD_MachineCode;
                        nr.Description = MachineDetail.MD_Description;
                    }
                    datatable1.AddDataTable1Row(nr);
                }
            }
            //CreateRSBQAMeasurementTotalButton(0);
        }

        private void CreateRSBQAMeasurementTotalButton(decimal rsbTotal)
        {
            //foreach (Button btn in groupBox9.Controls.OfType<Button>())
            //{
            //    if (btn.Name == "btnRSBQAMeasurementTotal")
            //    {
            //        groupBox9.Controls.Remove(btn);
            //    }
            //}

            Button btnRSBQAMeasurementTotal = new Button();
            btnRSBQAMeasurementTotal.Name = "btnRSBQAMeasurementTotal";
            btnRSBQAMeasurementTotal.Width = 190;
            btnRSBQAMeasurementTotal.Height = 50;
            btnRSBQAMeasurementTotal.Location = new Point(5, 70);
            btnRSBQAMeasurementTotal.Click += new EventHandler(btnRSBQAMeasurementTotal_Click);

            btnRSBQAMeasurementTotal.Text = "";
            Bitmap bmp = new Bitmap(btnRSBQAMeasurementTotal.ClientRectangle.Width, btnRSBQAMeasurementTotal.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnRSBQAMeasurementTotal.BackColor);

                string line1 = "RSB Totals (avg per period)";
                string line2 = Math.Round(rsbTotal, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnRSBQAMeasurementTotal.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Red, btnRSBQAMeasurementTotal.ClientRectangle, SF);
                }
            }
            btnRSBQAMeasurementTotal.Image = bmp;
            btnRSBQAMeasurementTotal.ImageAlign = ContentAlignment.MiddleCenter;

            //groupBox9.Controls.Add(btnRSBQAMeasurementTotal);
        }

        private void btnRSBQAMeasurementTotal_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                QAYarnReportOptions repsel = new QAYarnReportOptions();
                SpinningQueryParameters QueryParms = new SpinningQueryParameters();

                repsel.from = dtpDateFromKPI.Value; //Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                repsel.to = dtpDateToKPI.Value; //Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());

                repsel.MeasurementOption = 1;

                QueryParms.LowerTolerance = 4410;
                QueryParms.UpperTolerance = 4590;

                repsel.QASummary = true;

                QueryParms.FromDate = repsel.from;
                QueryParms.ToDate = repsel.to;

                frmViewReport vRep = new frmViewReport(14, QueryParms, repsel);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void GetSpinningMachineTotal()
        {
            Spinning.DataSet1 ds1 = new Spinning.DataSet1();
            Spinning.DataSet9.DataTable1DataTable datatable1 = new Spinning.DataSet9.DataTable1DataTable();
            int _YarnOrderNo = 0;
            string[] machdet = new string[5];
            IList<QAData> QAD = new List<QAData>();

            SpinningRepository repo = new SpinningRepository();

            SpinningQueryParameters QueryParms = new SpinningQueryParameters();
            QueryParms.LowerTolerance = 4410;
            QueryParms.UpperTolerance = 4590;
            QueryParms.MeasurementOpt = 2;
            QueryParms.FromDate = Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString()); ;
            QueryParms.ToDate = Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());

            using (var context = new TTI2Entities())
            {

                var Existing = repo.QAMeasurementQuery(QueryParms).ToList();
                foreach (var rowDate in Existing)
                {
                    Spinning.DataSet9.DataTable1Row nr = datatable1.NewDataTable1Row();
                    var yofk = rowDate.YarnQA_YarnOrder_FK;
                    var YarnOrder = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == yofk).FirstOrDefault();
                    if (YarnOrder != null)
                    {
                        nr.YarnOrderNo = YarnOrder.YarnO_OrderNumber;
                        _YarnOrderNo = YarnOrder.YarnO_OrderNumber;
                        var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == YarnOrder.Yarno_YarnType_FK).FirstOrDefault();
                        if (YarnType != null)
                        {
                            nr.Tex = YarnType.YA_TexCount;
                        }
                    }

                    nr.Count = (decimal)rowDate.YarnQA_08H00;
                    nr.CV = (decimal)rowDate.YarnQA_10H00;
                    nr.Thin = (int)rowDate.YarnQA_12H00;
                    nr.Thick = (int)rowDate.YarnQA_14H00;
                    nr.Nep = (int)rowDate.YarnQA_16H00;
                    nr.IPI = (int)rowDate.YarnQA_18H00;
                    nr.Date = rowDate.YarnQA_Date;
                    var MachineDetail = context.TLADM_MachineDefinitions.Find(rowDate.YarnQA_MachineNo_FK);
                    if (MachineDetail != null)
                    {
                        nr.MachNo = MachineDetail.MD_MachineCode;
                        nr.Description = MachineDetail.MD_Description;
                    }
                    datatable1.AddDataTable1Row(nr);
                }
            }

            ds1.Tables.Add(datatable1);
            var smTotal = ds1.Tables["datatable1"].AsEnumerable();

            decimal spinningMachineTotal = smTotal.Sum(datarow => datarow.Field<int>("IPI"));

            //CreateSpinningMachineTotalButton(spinningMachineTotal);
        }

        private void CreateSpinningMachineTotalButton(decimal spinningMachineTotal)
        {
            //foreach (Button btn in groupBox9.Controls.OfType<Button>())
            //{
            //    if (btn.Name == "btnSpinningMachineTotal")
            //    {
            //        groupBox9.Controls.Remove(btn);
            //    }
            //}

            Button btnSpinningMachineTotal = new Button();
            btnSpinningMachineTotal.Name = "btnSpinningMachineTotal";
            btnSpinningMachineTotal.Width = 190;
            btnSpinningMachineTotal.Height = 50;
            btnSpinningMachineTotal.Location = new Point(5, 120);
            btnSpinningMachineTotal.Click += new EventHandler(btnSpinningMachineTotal_Click);

            btnSpinningMachineTotal.Text = "";
            Bitmap bmp = new Bitmap(btnSpinningMachineTotal.ClientRectangle.Width, btnSpinningMachineTotal.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnSpinningMachineTotal.BackColor);

                string line1 = "Spinning Machine Total (IPI)";
                string line2 = Math.Round(spinningMachineTotal, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnSpinningMachineTotal.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Green, btnSpinningMachineTotal.ClientRectangle, SF);
                }
            }
            btnSpinningMachineTotal.Image = bmp;
            btnSpinningMachineTotal.ImageAlign = ContentAlignment.MiddleCenter;

            //groupBox9.Controls.Add(btnSpinningMachineTotal);
        }

        private void btnSpinningMachineTotal_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                QAYarnReportOptions repsel = new QAYarnReportOptions();
                SpinningQueryParameters QueryParms = new SpinningQueryParameters();

                repsel.from = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                repsel.to = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());

                repsel.MeasurementOption = 2;

                QueryParms.LowerTolerance = 4410;
                QueryParms.UpperTolerance = 4590;
                repsel.QASummary = false;

                QueryParms.FromDate = repsel.from;
                QueryParms.ToDate = repsel.to;

                frmViewReport vRep = new frmViewReport(14, QueryParms, repsel);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void GetSpinningMachineProductionTotal()
        {
            using (var context = new TTI2Entities())
            {
                //DateTime fromDate = dtpDateFromKPI.Value.Date;
                //DateTime toDate = dtpDateToKPI.Value.Date;
                var cardTotal = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate.Value >= KPIFromDate && x.YarnProductionDate.Value <= KPIToDate && (x.MachineNo_FK == 92 || x.MachineNo_FK == 93 || x.MachineNo_FK == 94 || x.MachineNo_FK == 95)).AsEnumerable().Sum(yp => yp.YarnProduction);
                var RSBTotal = context.TLSPN_YarnProductionPerMachine.Where(x => x.YarnProductionDate.Value >= KPIFromDate && x.YarnProductionDate.Value <= KPIToDate && (x.MachineNo_FK == 96 || x.MachineNo_FK == 97)).AsEnumerable().Sum(yp => yp.YarnProduction);

                //CreateCardProductionTotalButton((decimal)cardTotal);
                //CreateRSBProductionTotalButton((decimal)RSBTotal);
            }
        }

        private void CreateTotalYarnProducedButton(decimal yarnProductionTotal)
        {
            foreach (Button button in grpCardMachineQualityCheck.Controls.OfType<Button>())
            {
                if (button.Name == "btnTotalYarnProduced")
                    grpCardMachineQualityCheck.Controls.Remove(button);
            }

            Button btnTotalYarnProduced = CreateButton(
                "btnTotalYarnProduced", 190, 50, 5, 180,
                "Total Yarn Produced (kg)",
                Math.Round(yarnProductionTotal, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnTotalYarnProduced.Click += new EventHandler(btnTotalYarnProduced_Click);
            grpCardMachineQualityCheck.Controls.Add(btnTotalYarnProduced);
        }

        private void CreateCardProductionTotalButton(decimal cardTotal)
        {
            UpdateProgressBar("Sliver Card Production", 7);
            foreach (Button btn in grpCardMachineQualityCheck.Controls.OfType<Button>())
            {
                if (btn.Name == "btnCardProductionTotal")
                {
                    grpCardMachineQualityCheck.Controls.Remove(btn);
                }
            }
            Button btnCardProductionTotal = CreateButton(
                                                "btnCardProductionTotal", 190, 50, 5, 15,
                                                "Card Totals (kg)",
                                                Math.Round(cardTotal, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnCardProductionTotal.Click += new EventHandler(btnCardProductionTotal_Click);
            grpCardMachineQualityCheck.Controls.Add(btnCardProductionTotal);
        }

        private void btnCardProductionTotal_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblDataLoading.Text = "Loading Data...";
            lblDataLoading.Refresh();
            UpdateProgressBar("Sliver Production Detail Report Data", 18);
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                UpdateProgressBar("Sliver Production Detail Report Data", 25);
                SliverProductionSelection sliverProductionSelection = new SliverProductionSelection();
                sliverProductionSelection.DateFrom = Convert.ToDateTime(KPIFromDate);
                sliverProductionSelection.DateTo = Convert.ToDateTime(KPIToDate);
                sliverProductionSelection.Detail = true;

                UpdateProgressBar("Sliver Production Detail Report Data", 45);
                frmViewReport vRep = new frmViewReport(24, sliverProductionSelection);

                UpdateProgressBar("Sliver Production Detail Report Data", 65);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;

                UpdateProgressBar("Sliver Production Detail Report Data", 95);
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            UpdateProgressBar("Sliver Production Detail Report Data", 100);
            lblDataLoading.Text = "Data Loaded";
            lblDataLoading.Refresh();
            this.Cursor = Cursors.Arrow;
        }

        private void CreateRSBProductionTotalButton(decimal rsbTotal)
        {
            UpdateProgressBar("Sliver RSB Production", 10);
            foreach (Button btn in grpCardMachineQualityCheck.Controls.OfType<Button>())
            {
                if (btn.Name == "btnRSBProductionTotal")
                {
                    grpCardMachineQualityCheck.Controls.Remove(btn);
                }
            }
            Button btnRSBProductionTotal = CreateButton(
                                                "btnRSBProductionTotal", 190, 50, 5, 65,
                                                "RSB Totals (kg)",
                                                Math.Round(rsbTotal, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnRSBProductionTotal.Click += new EventHandler(btnRSBProductionTotal_Click);
            grpCardMachineQualityCheck.Controls.Add(btnRSBProductionTotal);
        }

        private void btnRSBProductionTotal_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblDataLoading.Text = "Loading Data...";
            lblDataLoading.Refresh();
            UpdateProgressBar("Sliver Production Summary Report Data", 18);
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                UpdateProgressBar("Sliver Production Summary Report Data", 25);
                SliverProductionSelection sliverProductionSelection = new SliverProductionSelection();
                sliverProductionSelection.DateFrom = Convert.ToDateTime(KPIFromDate);
                sliverProductionSelection.DateTo = Convert.ToDateTime(KPIToDate);
                sliverProductionSelection.Summary = true;

                UpdateProgressBar("Sliver Production Summary Report Data", 65);
                frmViewReport vRep = new frmViewReport(24, sliverProductionSelection);

                UpdateProgressBar("Sliver Production Summary Report Data", 85);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;

                UpdateProgressBar("Sliver Production Summary Report Data", 95);
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            UpdateProgressBar("Sliver Production Summary Report Data", 100);
            lblDataLoading.Text = "Data Loaded";
            lblDataLoading.Refresh();
            this.Cursor = Cursors.Arrow;
        }

        private void btnCardQAMeasurement_Click(object sender, EventArgs e, int i)
        {
            this.Cursor = Cursors.WaitCursor;
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                QAYarnReportOptions repsel = new QAYarnReportOptions();
                SpinningQueryParameters QueryParms = new SpinningQueryParameters();

                repsel.from = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                repsel.to = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());

                repsel.MeasurementOption = 1;
                repsel.QASummary = false;

                QueryParms.LowerTolerance = 4410;
                QueryParms.UpperTolerance = 4590;
                QueryParms.OutSideTolerance = true;
                

                QueryParms.FromDate = repsel.from;
                QueryParms.ToDate = repsel.to;

                SpinningRepository repo = new SpinningRepository();
                QueryParms.Machines.Add(repo.LoadMachine(i));

                frmViewReport vRep = new frmViewReport(14, QueryParms, repsel);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnTotalYarnProduced_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblDataLoading.Text = "Loading Data...";
            lblDataLoading.Refresh();
            UpdateProgressBar("Yarn Production Detail Report Data", 18);
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                UpdateProgressBar("Yarn Production Detail Report Data", 25);
                YarnProductionSel yarnProduction = new YarnProductionSel();

                UpdateProgressBar("Yarn Production Detail Report Data", 45);
                yarnProduction.fromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                yarnProduction.toDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());

                yarnProduction.QASummary = false;

                UpdateProgressBar("Yarn Production Detail Report Data", 65);
                frmViewReport vRep = new frmViewReport(16, yarnProduction);

                UpdateProgressBar("Yarn Production Detail Report Data", 85);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;

                UpdateProgressBar("Yarn Production Detail Report Data", 95);
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            UpdateProgressBar("Yarn Production Detail Report Data", 100);
            lblDataLoading.Text = "Data Loaded";
            lblDataLoading.Refresh();
            this.Cursor = Cursors.Arrow;
        }



        private void GetTotalGreigeProduced()
        {
            IList<TLKNI_GreigeProduction> ProdGroups = null;

            KnitRepository knitRepo = new KnitRepository();
            KnitQueryParameters knitQryParams = new KnitQueryParameters();
            knitQryParams.FromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
            knitQryParams.ToDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());
            //knitQryParams.ToDate = knitQryParams.ToDate.AddHours(23.59);
            decimal totalGreigeProduced = 0;
            using (var context = new TTI2Entities())
            {
                var Data = knitRepo.GreigeProduction(knitQryParams); // context.TLKNI_GreigeProduction.Where(x => x.GreigeP_PDate >= _opts.fromDate && x.GreigeP_PDate <= _opts.toDate).ToList();

                ProdGroups = Data.Where(x => !x.GreigeP_CommisionCust).ToList();
                totalGreigeProduced = ProdGroups.Sum(x => (decimal?)x.GreigeP_weight) ?? 0.00M;
                totalGreigeProduced = 0;
                foreach (var Greige in context.TLADM_Griege)
                {
                    var GProduced = ProdGroups.Where(x => x.GreigeP_Greige_Fk == Greige.TLGreige_Id).ToList();
                    totalGreigeProduced = totalGreigeProduced + GProduced.Sum(x => (decimal?)x.GreigeP_weight) ?? 0.00M;
                }
            }
            //CreateTotalGreigeProducedButton(totalGreigeProduced);

        }





        private decimal[] TotalGreigeProducedPerGrade(string machine)
        {
            KnitQueryParameters knitQryParams = new KnitQueryParameters();
            knitQryParams.FromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
            knitQryParams.ToDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());
            //knitQryParams.ToDate = knitQryParams.ToDate.AddHours(23.59);

            IList<TLKNI_GreigeProduction> ProdGroups = null;
            KnitRepository knitRepo = new KnitRepository();

            decimal totalGreigeProduced = 0;
            decimal greigeProducedGradeA = 0;
            decimal greigeProducedGradeB = 0;
            decimal greigeProducedGradeC = 0;

            using (var context = new TTI2Entities())
            {
                var Data = knitRepo.GreigeProduction(knitQryParams); // context.TLKNI_GreigeProduction.Where(x => x.GreigeP_PDate >= _opts.fromDate && x.GreigeP_PDate <= _opts.toDate).ToList();

                ProdGroups = Data.Where(x => !x.GreigeP_CommisionCust).ToList();

                //totalGreigeProduced = ProdGroups.Sum(x => (decimal?)x.GreigeP_weight) ?? 0.00M;

                foreach (var Greige in context.TLADM_Griege)
                {
                    var GProduced = ProdGroups.Where(x => x.GreigeP_Greige_Fk == Greige.TLGreige_Id).ToList();

                    if (machine == "All Machines")
                    {
                        totalGreigeProduced = totalGreigeProduced + GProduced.Sum(x => (decimal?)x.GreigeP_weight) ?? 0.00M;
                        //greigeProducedGrade = totalGreigeProduced + GProduced.Sum(x => (decimal?)x.GreigeP_weight) ?? 0.00M;
                        greigeProducedGradeA = greigeProducedGradeA + GProduced.Where(x => x.GreigeP_Grade == "A").Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                        greigeProducedGradeB = greigeProducedGradeB + GProduced.Where(x => x.GreigeP_Grade == "B").Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                        greigeProducedGradeC = greigeProducedGradeC + GProduced.Where(x => x.GreigeP_Grade == "C").Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                    }
                    else
                    {
                        var MachineGroup = GProduced.GroupBy(x => x.GreigeP_Machine_FK);
                        //totalGreigeProduced = 0;
                        foreach (var mGroup in MachineGroup)
                        {
                            var Pk = mGroup.FirstOrDefault().GreigeP_Machine_FK;
                            var MachDet = context.TLADM_MachineDefinitions.Find(Pk);
                            if (MachDet != null && machine == MachDet.MD_MachineCode)
                            {
                                totalGreigeProduced = totalGreigeProduced + GProduced.Sum(x => (decimal?)x.GreigeP_weight) ?? 0.00M;
                                greigeProducedGradeA = greigeProducedGradeA + GProduced.Where(x => x.GreigeP_Grade == "A").Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                                greigeProducedGradeB = greigeProducedGradeB + GProduced.Where(x => x.GreigeP_Grade == "B").Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                                greigeProducedGradeC = greigeProducedGradeC + GProduced.Where(x => x.GreigeP_Grade == "C").Sum(x => (decimal?)x.GreigeP_weight) ?? 0.0M;
                            }
                        }
                    }
                }
            }

            decimal totalGreigeProducedPerGrade = greigeProducedGradeA + greigeProducedGradeB + greigeProducedGradeC;

            decimal greigeProducedGradeAPerc = totalGreigeProduced != 0 ? greigeProducedGradeA / totalGreigeProduced * 100 : 0;
            //CreateGreigeProducedAGradePercButton(greigeProducedGradeAPerc);
            decimal greigeProducedGradeBPerc = totalGreigeProduced != 0 ? greigeProducedGradeB / totalGreigeProduced * 100 : 0;
            //CreateGreigeProducedBGradePercButton(greigeProducedGradeBPerc);
            decimal greigeProducedGradeCPerc = totalGreigeProduced != 0 ? greigeProducedGradeC / totalGreigeProduced * 100 : 0;
            //CreateGreigeProducedCGradePercButton(greigeProducedGradeCPerc);

            decimal[] greigeProducedPerGrade = { greigeProducedGradeA, greigeProducedGradeB, greigeProducedGradeC };
            return greigeProducedPerGrade;
        }

        private void GetFabricDyedTotals()
        {       
            DyeReportOptions dyeReportOptions = new DyeReportOptions();
            dyeReportOptions.fromDate = KPIFromDate; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
            dyeReportOptions.toDate = KPIToDate; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());
                                                 //QueryParms.ToDate = QueryParms.ToDate.AddHours(23.59);
            IList<TLADM_Colours> colours = null;
            IList<TLDYE_DyeBatch> dyeBatch = new List<TLDYE_DyeBatch>();

            using (var context = new TTI2Entities())
            {
                colours = context.TLADM_Colours.ToList();

                //================================================================
                // for normalised production relating to colours we need the colour that has been designated as the bench mark
                //======================================================================
                TLADM_Colours colourBenchMark = colours.Where(x => x.Col_Benchmark).FirstOrDefault();
                  
                dyeBatch = context.TLDYE_DyeBatch.Where(x => x.DYEB_OutProcess && x.DYEB_OutProcessDate >= dyeReportOptions.fromDate && x.DYEB_OutProcessDate <= dyeReportOptions.toDate).ToList();

                GetFabricDyedNotFinishedTotals(dyeReportOptions, colours, colourBenchMark, dyeBatch);
                GetFabricDyedToQuarantineStoreTotals(dyeReportOptions, colours, colourBenchMark, dyeBatch);
            }
        }

        private void GetFabricDyedNotFinishedTotals(DyeReportOptions dyeReportOptions, IList<TLADM_Colours> colours, TLADM_Colours colourBenchMark, IList<TLDYE_DyeBatch> dyeBatch)
        {
            UpdateProgressBar("Fabric Dyed Not Finsihed", 65);
            
            decimal totalFabricDyed = 0;
            decimal totalFabricDyedNormalised = 0;
            
            using (var context = new TTI2Entities())
            {
                var resultFabricNotFinished = (from t in context.TLDYE_DyeBatch
                                               join x in context.TLDYE_DyeTransactions on t.DYEB_Pk equals x.TLDYET_Batch_FK
                                               where x.TLDYET_Date >= dyeReportOptions.fromDate && x.TLDYET_Date <= dyeReportOptions.toDate && x.TLDYET_Stage == 3 && !t.DYEB_Closed 
                                               select new { t, x }).ToList();

                decimal gross = 0;
                decimal normalised = 0;

                foreach (var row in resultFabricNotFinished)
                {
                    var DyeBatchDet = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.t.DYEB_Pk).ToList();
                    if (DyeBatchDet.Count > 0)
                    {
                        gross = DyeBatchDet.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
                        var colourDyedFabricNotFinished = colours.FirstOrDefault(s => s.Col_Id == row.t.DYEB_Colour_FK);
                        string colour = colourDyedFabricNotFinished.Col_Display;

                        if (colourDyedFabricNotFinished.Col_StandardTime > 0)
                        {
                            var ratio = colourDyedFabricNotFinished.Col_StandardTime / colourBenchMark.Col_StandardTime;
                            normalised = normalised + (gross * ratio);
                        }
                        else
                            normalised = normalised + 0.00M;

                        totalFabricDyed = totalFabricDyed + gross;
                    }
                }
                totalFabricDyedNormalised = normalised;

            }
        }

        private void GetFabricDyedToQuarantineStoreTotals(DyeReportOptions dyeReportOptions, IList<TLADM_Colours> colours, TLADM_Colours colourBenchMark,  IList<TLDYE_DyeBatch> dyeBatch)
        {
            UpdateProgressBar("Fabric to Quarantine Store", 85);

            IList<TLDYE_DyeBatch> dyeBatchFirstTime = new List<TLDYE_DyeBatch>();
            IList<TLDYE_DyeBatch> dyeBatchReprocessed = new List<TLDYE_DyeBatch>();

            decimal gross = 0;
            decimal nett = 0;
            decimal normalised = 0;

            decimal totalFirstTimeQSGross = 0;
            decimal totalFirstTimeQSNett = 0;
            decimal totalFirstTimeQSNormalised = 0;

            decimal totalReprocessedQSGross = 0;
            decimal totalReprocessedQSNett = 0;
            decimal totalReprocessedQSNormalised = 0;

            dyeBatchFirstTime = dyeBatch.Where(x => x.DYEB_SequenceNo == 0).ToList();
            dyeBatchReprocessed = dyeBatch.Where(x => x.DYEB_SequenceNo != 0).ToList();     

            using (var context = new TTI2Entities())
            {
                foreach (var row in dyeBatchFirstTime)
                {
                    var colourDyedFabricQSFirstTime = colours.FirstOrDefault(s => s.Col_Id == row.DYEB_Colour_FK);
                    string colour = colourDyedFabricQSFirstTime.Col_Display;

                    if (context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Count() != 0)
                    {
                        gross = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
                        if (colourDyedFabricQSFirstTime.Col_StandardTime > 0)
                        {
                            nett = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Sum(x => (decimal?)x.DYEBO_Nett ?? 0.00M);
                            var Ratio = colourDyedFabricQSFirstTime.Col_StandardTime / colourBenchMark.Col_StandardTime;
                            normalised = nett * Ratio;
                        }
                        else
                        {
                            nett = 0.00M;
                            normalised = 0.00M;
                        }
                    }
                    else
                    {
                        gross = 0.00M;
                        nett = 0.00M;
                        normalised = 0.00M;
                    }

                    totalFirstTimeQSGross = totalFirstTimeQSGross + gross;
                    totalFirstTimeQSNett = totalFirstTimeQSNett + nett;
                    totalFirstTimeQSNormalised = totalFirstTimeQSNormalised + normalised;
                }

                //totalReprocessedQSGross = dyeBatch.Where(x => x.DYEB_SequenceNo != 0).Sum(x => (decimal)x.DYEB_BatchKG);
                dyeBatchReprocessed = dyeBatch.Where(x => x.DYEB_SequenceNo != 0).ToList();
                foreach (var row in dyeBatchReprocessed)
                {
                    var colourDyedFabricQSReprocessed = colours.FirstOrDefault(s => s.Col_Id == row.DYEB_Colour_FK);
                    string colour = colourDyedFabricQSReprocessed.Col_Display;

                    if (context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Count() != 0)
                    {
                        gross = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
                        if (colourDyedFabricQSReprocessed.Col_StandardTime > 0)
                        {
                            nett = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Sum(x => (decimal?)x.DYEBO_Nett ?? 0.00M);
                            var Ratio = colourDyedFabricQSReprocessed.Col_StandardTime / colourBenchMark.Col_StandardTime;
                            normalised = nett * Ratio;
                        }
                        else
                        {
                            nett = 0.00M;
                            normalised = 0.00M;
                        }
                    }
                    else
                    {
                        gross = 0.00M;
                        nett = 0.00M;
                        normalised = 0.00M;
                    }

                    totalReprocessedQSGross = totalReprocessedQSGross + gross;
                    totalReprocessedQSNett = totalReprocessedQSNett + nett;
                    totalReprocessedQSNormalised = totalReprocessedQSNormalised + normalised;
                }
            }
        }

        private void btnFabricDyedNotFinished_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblDataLoading.Text = "Loading Data...";
            lblDataLoading.Refresh();
            UpdateProgressBar("Total Fabric Dyed Not Finished Detail Report Data", 18);
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                UpdateProgressBar("Total Fabric Dyed Not Finished Detail Report Data", 25);
                DyeReportOptions dyeReportOptions = new DyeReportOptions();

                dyeReportOptions.fromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                dyeReportOptions.toDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());
                                                              //QueryParms.ToDate = QueryParms.ToDate.AddHours(23.59);

                dyeReportOptions.FabricNotFinished = true;

                UpdateProgressBar("Total Fabric Dyed Not Finished Detail Report Data", 45);
                frmDyeViewReport vRep = new frmDyeViewReport(31, dyeReportOptions);

                UpdateProgressBar("Total Fabric Dyed Not Finished Detail Report Data", 65);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                UpdateProgressBar("Total Fabric Dyed Not Finished Detail Report Data", 95);
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            UpdateProgressBar("Total Fabric Dyed Not Finished Detail Report Data", 100);
            lblDataLoading.Text = "Data Loaded";
            lblDataLoading.Refresh();
            this.Cursor = Cursors.Arrow;
        }

        private void btnFirstTimeQS_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblDataLoading.Text = "Loading Data...";
            lblDataLoading.Refresh();
            UpdateProgressBar("Fabric to Quarantine Store First Time Detail Report Data", 18);
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                UpdateProgressBar("Fabric to Quarantine Store First Time Report Data", 25);
                DyeReportOptions dyeReportOptions = new DyeReportOptions();

                dyeReportOptions.fromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                dyeReportOptions.toDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());
                                                              //QueryParms.ToDate = QueryParms.ToDate.AddHours(23.59);
                dyeReportOptions.FabricToQ = true;
                dyeReportOptions.FirstTime = true;

                UpdateProgressBar("Fabric to Quarantine Store First Time Report Data", 65);
                frmDyeViewReport vRep = new frmDyeViewReport(31, dyeReportOptions);

                UpdateProgressBar("Fabric to Quarantine Store First Time Report Data", 85);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;

                UpdateProgressBar("Fabric to Quarantine Store First Time Report Data", 95);
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            UpdateProgressBar("Fabric to Quarantine Store First Time Report Data", 100);
            lblDataLoading.Text = "Data Loaded";
            lblDataLoading.Refresh();
            this.Cursor = Cursors.Arrow;
        }

        private void btnReprocessedQS_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                DyeReportOptions dyeReportOptions = new DyeReportOptions();

                dyeReportOptions.fromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                dyeReportOptions.toDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());
                                                              //QueryParms.ToDate = QueryParms.ToDate.AddHours(23.59);
                dyeReportOptions.FabricToQ = true;
                dyeReportOptions.FirstTime = false;

                frmDyeViewReport vRep = new frmDyeViewReport(31, dyeReportOptions);

                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void btnFabricDyedToFabricStore_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblDataLoading.Text = "Loading Data...";
            lblDataLoading.Refresh();
            UpdateProgressBar("Fabric Dyed to Fabric Store Detail Report Data", 18);
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                UpdateProgressBar("Fabric Dyed to Fabric Store Detail Report Data", 25);
                DyeReportOptions dyeReportOptions = new DyeReportOptions();

                UpdateProgressBar("Fabric Dyed to Fabric Store Detail Report Data", 45);
                dyeReportOptions.fromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                dyeReportOptions.toDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());
                                                              //QueryParms.ToDate = QueryParms.ToDate.AddHours(23.59);
                dyeReportOptions.FabricNotFinished = false;
                dyeReportOptions.FabricToStore = true;

                UpdateProgressBar("Fabric Dyed to Fabric Store Detail Report Data", 65);
                frmDyeViewReport vRep = new frmDyeViewReport(31, dyeReportOptions);

                UpdateProgressBar("Fabric Dyed to Fabric Store Detail Report Data", 85);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;

                UpdateProgressBar("Fabric Dyed to Fabric Store Detail Report Data", 95);
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            UpdateProgressBar("Fabric Dyed to Fabric Store Detail Report Data", 100);
            lblDataLoading.Text = "Data Loaded";
            lblDataLoading.Refresh();
            this.Cursor = Cursors.Arrow;
        }                  


        private void btnFabricDyedNotFinishedNormalised_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblDataLoading.Text = "Loading Data...";
            lblDataLoading.Refresh();
            UpdateProgressBar("Fabric Dyed Not Finished Summary Report Data", 18);
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                UpdateProgressBar("Fabric Dyed Not Finished Summary Report Data", 25);
                DyeReportOptions dyeReportOptions = new DyeReportOptions();
                dyeReportOptions.FabricNotFinished = true;
                dyeReportOptions.QASummary = true;
                dyeReportOptions.fromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                dyeReportOptions.toDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());
                                                              //QueryParms.ToDate = QueryParms.ToDate.AddHours(23.59);

                UpdateProgressBar("Fabric Dyed Not Finished Summary Report Data", 65);
                frmDyeViewReport vRep = new frmDyeViewReport(31, dyeReportOptions);

                UpdateProgressBar("Fabric Dyed Not Finished Summary Report Data", 85);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;

                UpdateProgressBar("Fabric Dyed Not Finished Summary Report Data", 95);
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            UpdateProgressBar("Fabric Dyed Not Finished Summary Report Data", 100);
            lblDataLoading.Text = "Data Loaded";
            lblDataLoading.Refresh();
            this.Cursor = Cursors.Arrow;
        }
        





        private void UpdateProgressBar(string message, int i)
        {
            //lblDataLoading.Text = "Loading Data...";
            if (prgBarKPI.InvokeRequired)
            {
                prgBarKPI.Invoke(new Action<string, int>(UpdateProgressBar), new Object[] { "Loading data...", i });
            }
            else
            {
                Thread.Sleep(200);
                prgBarKPI.Refresh();
                prgBarKPI.Value = i;
                prgBarKPI.CreateGraphics().DrawString(message, new Font("Arial",
                                                      (float)10.25, FontStyle.Italic),
                                                      Brushes.Black, new PointF(10, prgBarKPI.Height / 2 - 7));
            }
        }

        private void btnCMTProduction_Click(object sender, EventArgs e)
        {
            CMTReportOptions cmtReportOptions = new CMTReportOptions();
            cmtReportOptions.fromDate = KPIFromDate;
            cmtReportOptions.toDate = KPIToDate;
            //cmtReportOptions.toDate = cmtReportOptions.toDate.AddHours(23);
            cmtReportOptions.NoOfGarments = true;

            CMTQueryParameters QueryParms = new CMTQueryParameters();
            frmCMTViewRep vRep = new frmCMTViewRep(13, QueryParms, cmtReportOptions);
            int h = Screen.PrimaryScreen.WorkingArea.Height;
            int w = Screen.PrimaryScreen.WorkingArea.Width;
            vRep.ClientSize = new Size(w, h);
            vRep.ShowDialog(this);
        }


    }
}

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
    public partial class frmKPI : Form
    {
        TTI2_WF.KPIs.Spinning.YarnProduction yarnProduction;
        DateTime KPIFromDate;
        DateTime KPIToDate;

        public frmKPI()
        {            
            InitializeComponent();            

            KPIFromDate = dtpDateFromKPI.Value = DateTime.Now.AddDays(-1).Date.AddHours(7);
            KPIFromDate = dtpDateFromKPI.Value = DateTime.Now.AddDays(-7).Date.AddHours(7); //Testing
            KPIToDate = dtpDateToKPI.Value = DateTime.Now.Date.AddHours(7);

            cmbMachines.Items.Add("All Machines");
            using (var context = new TTI2Entities())
            {
                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
                    var Machines = context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).GroupBy(x => x.MD_MachineCode).OrderBy(x => x.Key).ToList();
                    foreach (var Machine in Machines)
                    {
                        cmbMachines.Items.Add(Machine.Key);
                    }
                }
            }
            cmbMachines.SelectedItem = "All Machines";
        }

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
                    G.DrawString(line2, arial, brushColour, btnTotalYarnProduced.ClientRectangle, SF);
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

            CreateTotalCuttingProducedButton(total);
        }

        private void CreateTotalCuttingProducedButton(double cutProductionTotal)
        {
            foreach (Button button in grpCutting.Controls.OfType<Button>())
            {
                if (button.Name == "btnTotalCuttingProduced")
                    grpCutting.Controls.Remove(button);
            }

            Button btnTotalCuttingProduced = CreateButton(
                "btnTotalCuttingProduced", 190, 50, 5, 55,
                "Cut Production (kg)",
                Math.Round(cutProductionTotal, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnTotalCuttingProduced.Click += new EventHandler(btnTotalCuttingProduced_Click);
            grpCutting.Controls.Add(btnTotalCuttingProduced);
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

                CreateTotalYarnProducedButton(yarnProductionTotal);
            }
        }

        private void GetCardQAMeasurementTotal()
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
                Transactions = repo.QAMeasurementQuery(QueryParms).ToList();
            }

            decimal avg = Transactions.Select(x => x.YarnQA_08H00).DefaultIfEmpty(0).Average();

            CreateCardQAMeasurementTotalButton(avg);
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
            CreateRSBQAMeasurementTotalButton(0);
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

                repsel.MeasurementOption = 3;

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

            CreateSpinningMachineTotalButton(spinningMachineTotal);
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

                CreateCardProductionTotalButton((decimal)cardTotal);
                CreateRSBProductionTotalButton((decimal)RSBTotal);
            }
        }

        private void CreateTotalYarnProducedButton(decimal yarnProductionTotal)
        {
            foreach (Button button in grpSpinning.Controls.OfType<Button>())
            {
                if (button.Name == "btnTotalYarnProduced")
                    grpSpinning.Controls.Remove(button);
            }

            Button btnTotalYarnProduced = CreateButton(
                "btnTotalYarnProduced", 190, 50, 5, 180,
                "Total Yarn Produced (kg)",
                Math.Round(yarnProductionTotal, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnTotalYarnProduced.Click += new EventHandler(btnTotalYarnProduced_Click);
            grpSpinning.Controls.Add(btnTotalYarnProduced);
        }

        private void CreateCardProductionTotalButton(decimal cardTotal)
        {
            UpdateProgressBar("Sliver Card Production", 7);
            foreach (Button btn in grpSpinningMachineProductionTotal.Controls.OfType<Button>())
            {
                if (btn.Name == "btnCardProductionTotal")
                {
                    grpSpinningMachineProductionTotal.Controls.Remove(btn);
                }
            }
            Button btnCardProductionTotal = CreateButton(
                                                "btnCardProductionTotal", 190, 50, 5, 15,
                                                "Card Totals (kg)",
                                                Math.Round(cardTotal, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnCardProductionTotal.Click += new EventHandler(btnCardProductionTotal_Click);
            grpSpinningMachineProductionTotal.Controls.Add(btnCardProductionTotal);
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
            foreach (Button btn in grpSpinningMachineProductionTotal.Controls.OfType<Button>())
            {
                if (btn.Name == "btnRSBProductionTotal")
                {
                    grpSpinningMachineProductionTotal.Controls.Remove(btn);
                }
            }
            Button btnRSBProductionTotal = CreateButton(
                                                "btnRSBProductionTotal", 190, 50, 5, 65,
                                                "RSB Totals (kg)",
                                                Math.Round(rsbTotal, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnRSBProductionTotal.Click += new EventHandler(btnRSBProductionTotal_Click);
            grpSpinningMachineProductionTotal.Controls.Add(btnRSBProductionTotal);
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

        private void CreateCardQAMeasurementTotalButton(decimal cardTotal)
        {
            //foreach (Button btn in groupBox9.Controls.OfType<Button>())
            //{
            //    if (btn.Name == "btnCardQAMeasurementTotal")
            //    {
            //        groupBox9.Controls.Remove(btn);
            //    }
            //}

            Button btnCardQAMeasurementTotal = new Button();
            btnCardQAMeasurementTotal.Name = "btnCardQAMeasurementTotal";
            btnCardQAMeasurementTotal.Width = 190;
            btnCardQAMeasurementTotal.Height = 50;
            btnCardQAMeasurementTotal.Location = new Point(5, 20);
            btnCardQAMeasurementTotal.Click += new EventHandler(btnCardQAMeasurementTotal_Click);

            btnCardQAMeasurementTotal.Text = "";
            Bitmap bmp = new Bitmap(btnCardQAMeasurementTotal.ClientRectangle.Width, btnCardQAMeasurementTotal.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnCardQAMeasurementTotal.BackColor);

                string line1 = "Card Totals (avg per period)";
                string line2 = Math.Round(cardTotal, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnCardQAMeasurementTotal.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Red, btnCardQAMeasurementTotal.ClientRectangle, SF);
                }
            }
            btnCardQAMeasurementTotal.Image = bmp;
            btnCardQAMeasurementTotal.ImageAlign = ContentAlignment.MiddleCenter;

            //groupBox9.Controls.Add(btnCardQAMeasurementTotal);
        }

        private void btnCardQAMeasurementTotal_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                QAYarnReportOptions repsel = new QAYarnReportOptions();
                SpinningQueryParameters QueryParms = new SpinningQueryParameters();

                repsel.from = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                repsel.to = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());

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

        public void ShowTotalYarnProducedPerMachineChart()
        {
            NSISelection nsiSelection = new NSISelection();
            nsiSelection.NSI = true;
            nsiSelection.fromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
            nsiSelection.toDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());

            DataSet ds1 = new DataSet();
            Spinning.DataSet11.DataTable1DataTable datatable1 = new Spinning.DataSet11.DataTable1DataTable();
            Spinning.DataSet11.DataTable2DataTable datatable2 = new Spinning.DataSet11.DataTable2DataTable();
            List<PalletProd> PalletProd = new List<PalletProd>();
            List<CorrellateData> StoreData = new List<CorrellateData>();
            IList<string> measureProp = new List<string>();
            decimal[] MeasurementspKg = null;
            string[] machdet = new string[8];

            using (var context = new TTI2Entities())
            {
                var Existing = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_DatePacked >= nsiSelection.fromDate && x.YarnOP_DatePacked <= nsiSelection.toDate && x.YarnOP_Complete).GroupBy(x => x.YarnOP_YarnOrder_FK).ToList();
                //------------------------------------------
                // 1st Thing is that we need the production for each machine
                //------------------------------------------------------------------
                foreach (var row in Existing)
                {
                    //------------------------------------------------------
                    // 1st get the original order from the file 
                    //-----------------------------------------------
                    var OrderKey = row.FirstOrDefault().YarnOP_YarnOrder_FK;
                    var orderDet = context.TLSPN_YarnOrder.Where(x => x.YarnO_Pk == OrderKey).FirstOrDefault();
                    if (orderDet != null)
                    {
                        //-------------------------------------------------------------
                        // need just to production numbers
                        //-------------------------------------------------------------
                        foreach (var pallet in row)
                        {
                            var p = PalletProd.Find(x => x._MachineKey == orderDet.Yarno_MachNo_FK);
                            if (p == null)
                            {
                                PalletProd np = new PalletProd(orderDet.Yarno_MachNo_FK, pallet.YarnOP_NettWeight, orderDet.Yarno_YarnType_FK);
                                PalletProd.Add(np);
                            }
                            else
                                p._NettWeight += pallet.YarnOP_NettWeight;
                        }

                    }
                }
                // End of Production collection
                //--------------------------------------------------------------
                if (!nsiSelection.NSI)    // This handles the Non Stock Items
                {
                    //-----------------------------------------------
                    // Get a list of the properties that generally get measured
                    //-----------------------------------------------------------------
                    var Properties = typeof(TLADM_MachineDefinitions).GetProperties();
                    foreach (var prop in Properties)
                    {
                        if (prop.Name.Contains("Measure_FK"))
                            measureProp.Add(prop.Name);
                    }

                    var MachineGrouped = PalletProd.GroupBy(x => x._MachineKey);
                    foreach (var MachineGroup in MachineGrouped)
                    {
                        decimal[] Measurements = new decimal[9];
                        MeasurementspKg = new decimal[8];

                        var MachineKey = MachineGroup.FirstOrDefault()._MachineKey;
                        var MachineInfo = context.TLADM_MachineDefinitions.Where(x => x.MD_Pk == MachineKey).FirstOrDefault();
                        if (MachineInfo != null)
                        {
                            foreach (var prop in measureProp)
                            {
                                var NettWeight = MachineGroup.Sum(x => (decimal?)x._NettWeight) ?? 0.00M;

                                if (prop.Contains("First"))
                                {
                                    if (MachineInfo.MD_FirstMeasure_FK == null)
                                        continue;

                                    //--------------------------------------------
                                    //now we know it has to be measured
                                    //------------------------------------
                                    Measurements[0] = NettWeight;
                                    Measurements[1] = (decimal)MachineInfo.MD_FirstMeasure_Qty * NettWeight;
                                }
                                else if (prop.Contains("Sec"))
                                {
                                    if (MachineInfo.MD_SecMeasure_FK == null)
                                        continue;

                                    //--------------------------------------------
                                    //now we know it has to be measured
                                    //------------------------------------
                                    Measurements[2] = (decimal)MachineInfo.MD_SecMeasure_Qty * NettWeight;
                                }
                                else if (prop.Contains("Third"))
                                {
                                    if (MachineInfo.MD_ThirdMeasure_FK == null)
                                        continue;

                                    //--------------------------------------------
                                    //now we know it has to be measured
                                    //------------------------------------
                                    Measurements[3] = (decimal)MachineInfo.MD_ThirdMeasure_Qty * NettWeight;
                                }
                                else if (prop.Contains("Fourth"))
                                {
                                    if (MachineInfo.MD_FourthMeasure_FK == null)
                                        continue;

                                    //--------------------------------------------
                                    //now we know it has to be measured
                                    //------------------------------------
                                    Measurements[4] = (decimal)MachineInfo.MD_FourthMeasure_Qty * NettWeight;

                                }
                                else if (prop.Contains("Fifth"))
                                {
                                    if (MachineInfo.MD_FifthMeasure_FK == null)
                                        continue;

                                    //--------------------------------------------
                                    //now we know it has to be measured
                                    //------------------------------------
                                    Measurements[5] = (decimal)MachineInfo.MD_FifthMeasure_Qty * NettWeight;

                                }
                                else if (prop.Contains("Six"))
                                {
                                    if (MachineInfo.MD_SixMeasure_FK == null)
                                        continue;

                                    //--------------------------------------------
                                    //now we know it has to be measured
                                    //------------------------------------
                                    Measurements[6] = (decimal)MachineInfo.MD_SixMeasure_Qty * NettWeight;

                                }
                                else if (prop.Contains("Seven"))
                                {
                                    if (MachineInfo.MD_SevenMeasure_FK == null)
                                        continue;

                                    //--------------------------------------------
                                    //now we know it has to be measured
                                    //------------------------------------
                                    Measurements[7] = (decimal)MachineInfo.MD_SecMeasure_Qty * NettWeight;
                                }

                                //----------------------------------------------------
                                // End of process of all machines write out the report
                                // start with the header 
                                //--------------------------------------------------
                            }

                            //---------------------------------------------------
                            // Now we have to work out the per kg factor for each machine
                            //--------------------------------------------------------
                            MeasurementspKg[0] = Measurements[1] / Measurements[0];
                            MeasurementspKg[1] = Measurements[2] / Measurements[0];
                            MeasurementspKg[2] = Measurements[3] / Measurements[0];
                            MeasurementspKg[3] = Measurements[4] / Measurements[0];
                            MeasurementspKg[4] = Measurements[5] / Measurements[0];
                            MeasurementspKg[5] = Measurements[6] / Measurements[0];
                            MeasurementspKg[6] = Measurements[7] / Measurements[0];
                            MeasurementspKg[7] = Measurements[8] / Measurements[0];

                            Spinning.DataSet11.DataTable1Row nr = datatable1.NewDataTable1Row();

                            nr.Measurement = MachineInfo.MD_MachineCode;
                            nr.Key = 1;
                            nr.Mach1 = Measurements[0];     // Production
                            nr.Mach2 = Measurements[1];     // Electricity KVA 
                            nr.Mach3 = Measurements[2];     // Electricity KWH
                            nr.Mach4 = Measurements[3];     // Labour
                            nr.Mach5 = MeasurementspKg[0];  // Electricity KVA / Kg
                            nr.Mach6 = MeasurementspKg[1];  // Electricity KWH / Kg
                            nr.Mach7 = MeasurementspKg[2];  // Labour
                            nr.Mach8 = MeasurementspKg[3];

                            datatable1.AddDataTable1Row(nr);
                        }
                    }

                    // End of process of all machines write out the report
                    // start with the header 
                    //--------------------------------------------------
                    Spinning.DataSet11.DataTable2Row hnr = datatable2.NewDataTable2Row();
                    hnr.Key = 1;
                    hnr.Head1 = string.Empty;
                    hnr.Head2 = string.Empty;
                    hnr.Head3 = string.Empty;
                    hnr.Head4 = string.Empty;
                    hnr.Head5 = string.Empty;
                    hnr.Head6 = string.Empty;
                    hnr.Head7 = string.Empty;
                    hnr.Head8 = string.Empty;
                    hnr.FromDate = nsiSelection.fromDate;
                    hnr.ToDate = nsiSelection.toDate;

                    datatable2.AddDataTable2Row(hnr);

                    DataView DataV = datatable1.DefaultView;
                    DataV.Sort = "Measurement";
                    ds1.Tables.Add(DataV.ToTable());
                    ds1.Tables.Add(datatable2);
                }
                else  // This handles the capacity utilisation
                {
                    //------------------------------------------------------------------
                    // The follow loop gets all the measurement details
                    //---------------------------------------------------------
                    string[] crosstabTitle = new string[6];
                    crosstabTitle[0] = "Production (Kg)";
                    crosstabTitle[1] = "Production Capacity 100%";
                    crosstabTitle[2] = "Production Capacity";
                    crosstabTitle[3] = "% utilisation @ 100%";
                    crosstabTitle[4] = "% utilisation";
                    crosstabTitle[5] = string.Empty;
                    //============================================================
                    //---------Define the datatable for the data element 
                    //=================================================================
                    System.Data.DataTable dt = new System.Data.DataTable();
                    DataColumn column;

                    //------------------------------------------------------
                    // Create column 0. // This is the Machine Description
                    //----------------------------------------------
                    column = new DataColumn();
                    column.DataType = typeof(String);
                    column.ColumnName = "Col0";
                    dt.Columns.Add(column);


                    //-----------------------------------------------------------
                    // Create column 1. // This is the Yarn Type
                    //----------------------------------------------
                    column = new DataColumn();
                    column.DataType = typeof(string);
                    column.ColumnName = "Col1";
                    dt.Columns.Add(column);


                    //-----------------------------------------------------------
                    // Create column 2. // This is the total production for the period 
                    //----------------------------------------------
                    column = new DataColumn();
                    column.DataType = typeof(Decimal);
                    column.ColumnName = "Col2";
                    dt.Columns.Add(column);

                    //-----------------------------------------------------------
                    // Create column 3. // This is the total production capacity @ 100 
                    //----------------------------------------------
                    column = new DataColumn();
                    column.DataType = typeof(Decimal);
                    column.ColumnName = "Col3";
                    dt.Columns.Add(column);

                    //-----------------------------------------------------------
                    // Create column 4. // This is the total production capacity @ 85
                    //----------------------------------------------
                    column = new DataColumn();
                    column.DataType = typeof(Decimal);
                    column.ColumnName = "Col4";
                    dt.Columns.Add(column);

                    //-----------------------------------------------------------
                    // Create column 5. // This is % utilisation @ 100 
                    //----------------------------------------------
                    column = new DataColumn();
                    column.DataType = typeof(Decimal);
                    column.ColumnName = "Col5";
                    dt.Columns.Add(column);

                    //-----------------------------------------------------------
                    // Create column 6. // This is % utilsation @ 85 
                    //----------------------------------------------
                    column = new DataColumn();
                    column.DataType = typeof(Decimal);
                    column.ColumnName = "Col6";
                    dt.Columns.Add(column);

                    var NoOfDays = nsiSelection.toDate - nsiSelection.fromDate;
                    var NoOfHours = Math.Round(NoOfDays.TotalHours, 0);           // This gives the number of hours worked 

                    foreach (var item in PalletProd)
                    {
                        var MachineInfo = context.TLADM_MachineDefinitions.Find(item._MachineKey);
                        if (MachineInfo != null)
                        {
                            //==================================
                            //Need to create a record
                            //==========================================
                            DataRow Row = dt.NewRow();

                            //----------------------------------------------------------------
                            // Initialise the control arrays
                            //-----------------------------------------------------------------
                            decimal[] Measurements = new decimal[5];
                            Measurements[0] = item._NettWeight;
                            Measurements[1] = (decimal)NoOfHours * MachineInfo.MD_MaxCapacity;
                            Measurements[2] = Measurements[1] * (MachineInfo.MD_Realistic / 100);
                            //----------------------------------------------------------------------
                            // Calculate the Capacity Utilisation 
                            //------------------------------------------------------------------------
                            Measurements[3] = Measurements[0] / Measurements[1] * 100;
                            Measurements[4] = Measurements[0] / Measurements[2] * 100;

                            Row[0] = MachineInfo.MD_MachineCode;

                            //--------------------------------------------------------------------
                            // find the tex type 1st the Yarn 
                            //---------------------------------------------------------------------
                            var Yarn = context.TLADM_Yarn.Where(x => x.YA_Id == item._YarnType_FK).FirstOrDefault();
                            if (Yarn != null)
                            {
                                Row[1] = Yarn.YA_TexCount.ToString();
                            }

                            Row[2] = Measurements[0];
                            Row[3] = Measurements[1];
                            Row[4] = Measurements[2];
                            Row[5] = Measurements[3];
                            Row[6] = Measurements[4];

                            dt.Rows.Add(Row);
                        }
                    }
                    //----------------------------------------------------
                    // End of process of all machines write out the report
                    // start with the header 
                    //--------------------------------------------------
                    Spinning.DataSet11.DataTable2Row hnr = datatable2.NewDataTable2Row();
                    hnr.Key = 1;
                    hnr.Head1 = crosstabTitle[0];
                    hnr.Head2 = crosstabTitle[1];
                    hnr.Head3 = crosstabTitle[2];
                    hnr.Head4 = crosstabTitle[3];
                    hnr.Head5 = crosstabTitle[4];
                    hnr.Head6 = crosstabTitle[5];

                    hnr.FromDate = nsiSelection.fromDate;
                    hnr.ToDate = nsiSelection.toDate;

                    datatable2.AddDataTable2Row(hnr);
                    //---------------------------------------------------
                    // Now the stored data 
                    //---------------------------------------------
                    foreach (DataRow Row in dt.Rows)
                    {
                        Spinning.DataSet11.DataTable1Row nr = datatable1.NewDataTable1Row();
                        nr.Measurement = Row.Field<String>(0) + " " + Row.Field<String>(1);
                        nr.Key = 1;
                        nr.Mach1 = Row.Field<Decimal>(2);
                        nr.Mach2 = Row.Field<Decimal>(3);
                        nr.Mach3 = Row.Field<Decimal>(4);
                        nr.Mach4 = Row.Field<Decimal>(5);
                        nr.Mach5 = Row.Field<Decimal>(6);

                        datatable1.AddDataTable1Row(nr);
                    }
                    try
                    {
                        ds1.Tables.Add(datatable1);
                        ds1.Tables.Add(datatable2);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }

            chrtSpinProductionTotals.Series.Clear();
            chrtSpinProductionTotals.DataSource = datatable1;
            chrtSpinProductionTotals.Legends[0].Position.Auto = false;
            chrtSpinProductionTotals.Legends[0].Docking = Docking.Top;

            Series serie = new Series();
            serie.Name = "SpinProductionTotals";
            serie.XValueMember = "Measurement";
            serie.YValueMembers = "Mach1";
            serie.ToolTip = serie.YValueMembers;
            chrtSpinProductionTotals.Series.Add(serie);
            chrtSpinProductionTotals.Series[0].Palette = ChartColorPalette.Pastel;
            chrtSpinProductionTotals.Series[0].ToolTip = "#VALY";
            chrtSpinProductionTotals.BorderlineColor = Color.DarkGray;
            chrtSpinProductionTotals.BorderlineWidth = 2;
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
            CreateTotalGreigeProducedButton(totalGreigeProduced);

        }

        private void CreateTotalGreigeProducedButton(decimal totalGreigeProduced)
        {
            foreach (Button btn in grpKnitting.Controls.OfType<Button>())
            {
                if (btn.Name == "btnTotalGreigeProduced")
                {
                    grpKnitting.Controls.Remove(btn);
                }
            }

            Button btnTotalGreigeProduced = new Button();
            btnTotalGreigeProduced.Name = "btnTotalGreigeProduced";
            btnTotalGreigeProduced.Width = 190;
            btnTotalGreigeProduced.Height = 50;
            btnTotalGreigeProduced.Location = new Point(5, 55);
            btnTotalGreigeProduced.Click += new EventHandler(btnTotalGreigeProduced_Click);

            btnTotalGreigeProduced.Text = "";
            Bitmap bmp = new Bitmap(btnTotalGreigeProduced.ClientRectangle.Width, btnTotalGreigeProduced.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.Clear(btnTotalGreigeProduced.BackColor);

                string line1 = "Total Greige Produced (kg)";
                string line2 = Math.Round(totalGreigeProduced, MidpointRounding.AwayFromZero).ToString(); ;

                StringFormat SF = new StringFormat();
                SF.Alignment = StringAlignment.Center;
                SF.LineAlignment = StringAlignment.Near;
                using (Font arial = new Font("Arial", 10))
                {
                    Rectangle RC = btnTotalGreigeProduced.ClientRectangle;
                    RC.Inflate(-5, -5);
                    G.DrawString(line1, arial, Brushes.Black, RC, SF);
                }

                using (Font arial = new Font("Arial", 18))
                {
                    SF.LineAlignment = StringAlignment.Far;
                    G.DrawString(line2, arial, Brushes.Green, btnTotalGreigeProduced.ClientRectangle, SF);
                }
            }
            btnTotalGreigeProduced.Image = bmp;
            btnTotalGreigeProduced.ImageAlign = ContentAlignment.MiddleCenter;

            grpKnitting.Controls.Add(btnTotalGreigeProduced);
        }

        private void btnTotalGreigeProduced_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            lblDataLoading.Text = "Loading Data...";
            lblDataLoading.Refresh();
            UpdateProgressBar("Greige Production Detail Report Data", 18);
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                UpdateProgressBar("Greige Production Detail Report Data", 25);
                YarnReportOptions YarnOpts = new YarnReportOptions();
                YarnOpts.K7rbQA1 = true;

                KnitQueryParameters QueryParms = new KnitQueryParameters();

                QueryParms.FromDate = dtpDateFromKPI.Value; // Convert.ToDateTime(dtpDateFromKPI.Value.ToShortDateString());
                QueryParms.ToDate = dtpDateToKPI.Value; // Convert.ToDateTime(dtpDateToKPI.Value.ToShortDateString());
                                                        //QueryParms.ToDate = QueryParms.ToDate.AddHours(23.59);

                UpdateProgressBar("Greige Production Detail Report Data", 65);
                frmKnitViewRep vRep = new frmKnitViewRep(23, QueryParms, YarnOpts);

                UpdateProgressBar("Greige Production Detail Report Data", 85);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;

                UpdateProgressBar("Greige Production Detail Report Data", 95);
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            UpdateProgressBar("Greige Production Detail Report Data", 100);
            lblDataLoading.Text = "Data Loaded";
            lblDataLoading.Refresh();
            this.Cursor = Cursors.Arrow;
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
            CreateGreigeProducedAGradePercButton(greigeProducedGradeAPerc);
            decimal greigeProducedGradeBPerc = totalGreigeProduced != 0 ? greigeProducedGradeB / totalGreigeProduced * 100 : 0;
            CreateGreigeProducedBGradePercButton(greigeProducedGradeBPerc);
            decimal greigeProducedGradeCPerc = totalGreigeProduced != 0 ? greigeProducedGradeC / totalGreigeProduced * 100 : 0;
            CreateGreigeProducedCGradePercButton(greigeProducedGradeCPerc);

            decimal[] greigeProducedPerGrade = { greigeProducedGradeA, greigeProducedGradeB, greigeProducedGradeC };
            return greigeProducedPerGrade;
        }

        public void ShowTotalGreigeProducedPerGradeChart(string machine)
        {
            //Grades
            string[] grades = { "A", "B", "C" };

            decimal[] y = TotalGreigeProducedPerGrade(machine);

            chrtKnitProductionTotals.Series[0].ChartType = SeriesChartType.Pie;
            chrtKnitProductionTotals.Palette = ChartColorPalette.None;
            chrtKnitProductionTotals.PaletteCustomColors = new Color[] { Color.FromArgb(252, 237, 209), Color.FromArgb(197, 197, 197), Color.FromArgb(240, 128, 128) };
            chrtKnitProductionTotals.Series[0].Points.DataBindXY(grades, y);
            chrtKnitProductionTotals.Legends[0].Enabled = true;
            chrtKnitProductionTotals.Legends[0].Title = "Grades";
            chrtKnitProductionTotals.Series[0].ToolTip = "#VALY";

            chrtKnitProductionTotals.Legends[0].Docking = Docking.Top;
            chrtKnitProductionTotals.Legends[0].Alignment = StringAlignment.Center;
            chrtKnitProductionTotals.ChartAreas[0].Area3DStyle.Enable3D = true;
            chrtKnitProductionTotals.BorderlineColor = Color.DarkGray;
            chrtKnitProductionTotals.BorderlineWidth = 2;
        }

        private void CmbMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null)
            {
                string machine = cmbMachines.SelectedItem.ToString();

                ShowTotalGreigeProducedPerGradeChart(machine);
            }
        }

        private void CreateGreigeProducedAGradePercButton(decimal greigeProducedGradeA)
        {
            foreach (Button btn in grpKnitting.Controls.OfType<Button>())
            {
                if (btn.Name == "btnGreigeProducedAGradePerc")
                {
                    grpKnitting.Controls.Remove(btn);
                }
            }
            Button btnGreigeProducedAGradePerc = CreateButton(
                                                "btnGreigeProducedAGradePerc", 190, 50, 5, 375,
                                                "A Grade (%)",
                                                Math.Round(greigeProducedGradeA, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            //btnGreigeProducedAGradePerc.Click += new EventHandler(btnRSBProductionTotal_Click);
            grpKnitting.Controls.Add(btnGreigeProducedAGradePerc);
        }

        private void CreateGreigeProducedBGradePercButton(decimal greigeProducedGradeB)
        {
            foreach (Button btn in grpKnitting.Controls.OfType<Button>())
            {
                if (btn.Name == "btnGreigeProducedBGradePerc")
                {
                    grpKnitting.Controls.Remove(btn);
                }
            }
            Button btnGreigeProducedBGradePerc = CreateButton(
                                                "btnGreigeProducedBGradePerc", 190, 50, 5, 425,
                                                "B Grade (%)",
                                                Math.Round(greigeProducedGradeB, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            //btnGreigeProducedAGradePerc.Click += new EventHandler(btnRSBProductionTotal_Click);
            grpKnitting.Controls.Add(btnGreigeProducedBGradePerc);
        }

        private void CreateGreigeProducedCGradePercButton(decimal greigeProducedGradeC)
        {
            foreach (Button btn in grpKnitting.Controls.OfType<Button>())
            {
                if (btn.Name == "btnGreigeProducedCGradePerc")
                {
                    grpKnitting.Controls.Remove(btn);
                }
            }
            Button btnGreigeProducedCGradePerc = CreateButton(
                                                "btnGreigeProducedCGradePerc", 190, 50, 5, 475,
                                                "C Grade (%)",
                                                Math.Round(greigeProducedGradeC, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            //btnGreigeProducedAGradePerc.Click += new EventHandler(btnRSBProductionTotal_Click);
            grpKnitting.Controls.Add(btnGreigeProducedCGradePerc);
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
                GetFabricDyedToFabricStoreTotals(dyeReportOptions, colours, colourBenchMark, dyeBatch);
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
                                               where x.TLDYET_Date >= dyeReportOptions.fromDate && x.TLDYET_Date <= dyeReportOptions.toDate && x.TLDYET_Stage == 3
                                               select new { t, x }).ToList();

                decimal gross = 0;
                decimal normalised = 0;

                foreach (var row in resultFabricNotFinished)
                {
                    gross = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.t.DYEB_Pk).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
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
                totalFabricDyedNormalised = normalised;
            }

            CreateFabricDyedNotFinishedGrossButton(totalFabricDyed);
            CreateFabricDyedNotFinishedNormalisedButton(totalFabricDyedNormalised);
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

            CreateFirstTimeQSGrossButton(totalFirstTimeQSGross);
            CreateReprocessedQSGrossButton(totalReprocessedQSGross);
            CreateFirstTimeQSNettButton(totalFirstTimeQSNett);
            CreateReprocessedQSNettButton(totalReprocessedQSNett);
            CreateFirstTimeQSNormalisedButton(totalFirstTimeQSNormalised);
            CreateReprocessedQSNormalisedButton(totalReprocessedQSNormalised);
        }

        private void GetFabricDyedToFabricStoreTotals(DyeReportOptions dyeReportOptions, IList<TLADM_Colours> colours, TLADM_Colours colourBenchMark, IList<TLDYE_DyeBatch> dyeBatch)
        {
            UpdateProgressBar("Fabric to Fabric Store", 95);

            IList<TLDYE_DyeBatch> dyeBatchFabricStore = new List<TLDYE_DyeBatch>();

            decimal gross = 0;
            decimal nett = 0;
            decimal normalised = 0;

            decimal totalFabricStoreGross = 0;
            decimal totalFabricStoreNett = 0;
            decimal totalFabricStoreNormalised = 0;

            using (var context = new TTI2Entities())
            {
                var resultFabricStore = from t in context.TLDYE_DyeBatch
                                        join x in context.TLDYE_DyeBatchDetails on t.DYEB_Pk equals x.DYEBD_DyeBatch_FK
                                        where x.DYEBO_QAApproved && x.DYEBO_ApprovalDate >= dyeReportOptions.fromDate && x.DYEBO_ApprovalDate <= dyeReportOptions.toDate && t.DYEB_OutProcess
                                        select t;

                //context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == dyeBatch.DYEB_Pk).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);

                dyeBatchFabricStore = resultFabricStore.Distinct().ToList();
                //totalFabricStoreGross = dyeBatchFabricStore.Sum(x => (decimal)x.DYEB_BatchKG);

                foreach (var row in dyeBatchFabricStore)
                {
                    var colourDyedFabricFS = colours.FirstOrDefault(s => s.Col_Id == row.DYEB_Colour_FK);
                    string colour = colourDyedFabricFS.Col_Display;

                    if (context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Count() != 0)
                    {
                        gross = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight ?? 0.00M);
                        if (colourDyedFabricFS.Col_StandardTime > 0)
                        {
                            nett = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == row.DYEB_Pk).Sum(x => (decimal?)x.DYEBO_Nett ?? 0.00M);
                            var ratio = colourDyedFabricFS.Col_StandardTime / colourBenchMark.Col_StandardTime;
                            normalised = nett * ratio;
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

                    totalFabricStoreGross = totalFabricStoreGross + gross;
                    totalFabricStoreNett = totalFabricStoreNett + nett;
                    totalFabricStoreNormalised = totalFabricStoreNormalised + normalised;
                }
            }

            CreateFabricStoreGrossButton(totalFabricStoreGross);
            CreateFabricStoreNettButton(totalFabricStoreNett);
            CreateFabricStoreNormalisedButton(totalFabricStoreNormalised);
        }

        private void CreateFabricDyedNotFinishedGrossButton(decimal totalFabricDyed)
        {
            foreach (Button btn in grpDyedFabricNotFinished.Controls.OfType<Button>())
            {
                if (btn.Name == "btnFabricDyedNotFinished")
                {
                    grpDyedFabricNotFinished.Controls.Remove(btn);
                }
            }

            Button btnFabricDyedNotFinished = CreateButton(
                                                "btnFabricDyedNotFinished", 190, 50, 5, 15,
                                                "Total Dyed (Gross kg)",
                                                Math.Round(totalFabricDyed, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnFabricDyedNotFinished.Click += new EventHandler(btnFabricDyedNotFinished_Click);
            grpDyedFabricNotFinished.Controls.Add(btnFabricDyedNotFinished);
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
                     
        private void CreateFabricDyedNotFinishedNormalisedButton(decimal totalFabricDyedNormalised)
        {
            foreach (Button btn in grpDyedFabricNotFinished.Controls.OfType<Button>())
            {
                if (btn.Name == "btnFabricDyedNotFinishedNormalised")
                {
                    grpDyedFabricNotFinished.Controls.Remove(btn);
                }
            }

            Button btnFabricDyedNotFinishedNormalised = CreateButton(
                                                "btnFabricDyedNotFinishedNormalised", 190, 50, 5, 65,
                                                "Total Dyed (Normalised)",
                                                Math.Round(totalFabricDyedNormalised, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);


            btnFabricDyedNotFinishedNormalised.Click += new EventHandler(btnFabricDyedNotFinishedNormalised_Click);
            grpDyedFabricNotFinished.Controls.Add(btnFabricDyedNotFinishedNormalised);
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
        
        private void CreateFirstTimeQSGrossButton(decimal totalFabricDyed)
        {
            foreach (Button btn in grpQuarantineStore.Controls.OfType<Button>())
            {
                if (btn.Name == "btnFirstTimeQSGross")
                {
                    grpQuarantineStore.Controls.Remove(btn);
                }
            }

            Button btnFirstTimeQSGross = CreateButton(
                                                "btnFirstTimeQSGross", 190, 50, 5, 15,
                                                "First Time Dyed (Gross kg)",
                                                Math.Round(totalFabricDyed, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnFirstTimeQSGross.Click += new EventHandler(btnFirstTimeQS_Click);
            grpQuarantineStore.Controls.Add(btnFirstTimeQSGross);
        }

        private void CreateReprocessedQSGrossButton(decimal totalReprocessedFabric)
        {
            foreach (Button btn in grpQuarantineStore.Controls.OfType<Button>())
            {
                if (btn.Name == "btnReprocessedQSGross")
                {
                    grpQuarantineStore.Controls.Remove(btn);
                }
            }


            Button btnReprocessedQSGross = CreateButton(
                                                "btnReprocessedQSGross", 190, 50, 5, 65,
                                                "Reprocessed (Gross kg)",
                                                Math.Round(totalReprocessedFabric, MidpointRounding.AwayFromZero).ToString(), Brushes.Red);

            btnReprocessedQSGross.Click += new EventHandler(btnReprocessedQS_Click);
            grpQuarantineStore.Controls.Add(btnReprocessedQSGross);
        }

        private void CreateFirstTimeQSNettButton(decimal totalFirstTimeDyed)
        {
            foreach (Button btn in grpQuarantineStore.Controls.OfType<Button>())
            {
                if (btn.Name == "btnFirstTimeQSNett")
                {
                    grpQuarantineStore.Controls.Remove(btn);
                }
            }

            Button btnFirstTimeQSNett = CreateButton(
                                                "btnFirstTimeQSNett", 190, 50, 5, 120,
                                                "First Time Dyed (Nett kg)",
                                                Math.Round(totalFirstTimeDyed, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnFirstTimeQSNett.Click += new EventHandler(btnFirstTimeQS_Click);
            grpQuarantineStore.Controls.Add(btnFirstTimeQSNett);
        }

        private void CreateReprocessedQSNettButton(decimal totalReprocessedNett)
        {
            foreach (Button btn in grpQuarantineStore.Controls.OfType<Button>())
            {
                if (btn.Name == "btnReprocessedQSNett")
                {
                    grpQuarantineStore.Controls.Remove(btn);
                }
            }

            Button btnReprocessedQSNett = CreateButton(
                                                "btnReprocessedQSNett", 190, 50, 5, 170,
                                                "Reprocessed (Nett kg)",
                                                Math.Round(totalReprocessedNett, MidpointRounding.AwayFromZero).ToString(), Brushes.Red);

            btnReprocessedQSNett.Click += new EventHandler(btnReprocessedQS_Click);
            grpQuarantineStore.Controls.Add(btnReprocessedQSNett);
        }

        private void CreateFirstTimeQSNormalisedButton(decimal totalFabricDyed)
        {
            foreach (Button btn in grpQuarantineStore.Controls.OfType<Button>())
            {
                if (btn.Name == "btnFirstTimeQSNormalised")
                {
                    grpQuarantineStore.Controls.Remove(btn);
                }
            }

            Button btnFirstTimeQSNormalised = CreateButton(
                                                "btnFirstTimeQSNormalised", 190, 50, 5, 225,
                                                "First Time (Normalised)",
                                                Math.Round(totalFabricDyed, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnFirstTimeQSNormalised.Click += new EventHandler(btnFirstTimeQS_Click);
            grpQuarantineStore.Controls.Add(btnFirstTimeQSNormalised);
        }

        private void CreateReprocessedQSNormalisedButton(decimal totalReprocessedQSNormalised)
        {
            foreach (Button btn in grpQuarantineStore.Controls.OfType<Button>())
            {
                if (btn.Name == "btnReprocessedQSNormalised")
                {
                    grpQuarantineStore.Controls.Remove(btn);
                }
            }

            Button btnReprocessedQSNormalised = CreateButton(
                                                "btnReprocessedQSNormalised", 190, 50, 5, 275,
                                                "Reprocessed (Normalised)",
                                                Math.Round(totalReprocessedQSNormalised, MidpointRounding.AwayFromZero).ToString(), Brushes.Red);

            btnReprocessedQSNormalised.Click += new EventHandler(btnReprocessedQS_Click);
            grpQuarantineStore.Controls.Add(btnReprocessedQSNormalised);
        }

        private void CreateFabricStoreGrossButton(decimal totalFabricStoreGross)
        {
            foreach (Button btn in grpFabricStore.Controls.OfType<Button>())
            {
                if (btn.Name == "btnFabricStoreGross")
                {
                    grpFabricStore.Controls.Remove(btn);
                }
            }

            Button btnFabricStoreGross = CreateButton(
                                                "btnFabricStoreGross", 190, 50, 5, 15,
                                                "Total Production (Gross Kg)",
                                                Math.Round(totalFabricStoreGross, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnFabricStoreGross.Click += new EventHandler(btnFabricDyedToFabricStore_Click);
            grpFabricStore.Controls.Add(btnFabricStoreGross);
        }


        private void CreateFabricStoreNettButton(decimal totalReprocessedQSNormalised)
        {
            foreach (Button btn in grpFabricStore.Controls.OfType<Button>())
            {
                if (btn.Name == "btnFabricStoreNett")
                {
                    grpFabricStore.Controls.Remove(btn);
                }
            }

            Button btnFabricStoreNett = CreateButton(
                                                "btnFabricStoreNett", 190, 50, 5, 65,
                                                "Total Production (Nett Kg)",
                                                Math.Round(totalReprocessedQSNormalised, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnFabricStoreNett.Click += new EventHandler(btnFabricDyedToFabricStore_Click);
            grpFabricStore.Controls.Add(btnFabricStoreNett);
        }

        private void CreateFabricStoreNormalisedButton(decimal totalReprocessedQSNormalised)
        {
            foreach (Button btn in grpFabricStore.Controls.OfType<Button>())
            {
                if (btn.Name == "btnFabricStoreNormalised")
                {
                    grpFabricStore.Controls.Remove(btn);
                }
            }

            Button btnFabricStoreNormalised = CreateButton(
                                                "btnFabricStoreNormalised", 190, 50, 5, 115,
                                                "Total Production (Norm)",
                                                Math.Round(totalReprocessedQSNormalised, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnFabricStoreNormalised.Click += new EventHandler(btnFabricDyedToFabricStore_Click);
            grpFabricStore.Controls.Add(btnFabricStoreNormalised);
        }

        private void GetCMTProductionTotals()
        {

            IList<TLCMT_CompletedWork> Completed = new List<TLCMT_CompletedWork>();

            CMTRepository cmtRepo = new CMTRepository();

            decimal garmentWeight = 0;
            int garmentSum = 0;

            using (var context = new TTI2Entities())
            {

                //var CompletedWork = context.TLCMT_CompletedWork.AsQueryable(); //.Where(x => x.TLCMTWC_TransactionDate >= KPIFromDate && x.TLCMTWC_TransactionDate <= KPIToDate);
                var CompletedGrouped = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_TransactionDate >= KPIFromDate && x.TLCMTWC_TransactionDate <= KPIToDate).GroupBy(x => x.TLCMTWC_CutSheet_FK).ToList();

                garmentWeight = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_TransactionDate >= KPIFromDate && x.TLCMTWC_TransactionDate <= KPIToDate).Sum(x => (decimal?)x.TLCMTWC_Weight) ?? 0;
                garmentSum = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_TransactionDate >= KPIFromDate && x.TLCMTWC_TransactionDate <= KPIToDate).Sum(x => (int?)x.TLCMTWC_Qty) ?? 0;

            }

            CreateCMTProductionGarmentWeightButton(garmentWeight);
            CreateCMTProductionGarmentUnitsButton(garmentSum);
        }

        private void CreateCMTProductionGarmentWeightButton(decimal cmtProduction)
        {
            foreach (Button btn in grpCMT.Controls.OfType<Button>())
            {
                if (btn.Name == "btnCMTProductionGarmentWeight")
                {
                    grpCMT.Controls.Remove(btn);
                }
            }

            Button btnCMTProductionGarmentWeight = CreateButton(
                                                "btnCMTProductionGarmentWeight", 190, 50, 5, 55,
                                                "Garments Prod Weight (kg)",
                                                Math.Round(cmtProduction, MidpointRounding.AwayFromZero).ToString(), Brushes.Green);

            btnCMTProductionGarmentWeight.Click += new EventHandler(btnCMTProduction_Click);
            grpCMT.Controls.Add(btnCMTProductionGarmentWeight);
        }

        private void CreateCMTProductionGarmentUnitsButton(int cmtProduction)
        {
            foreach (Button btn in grpCMT.Controls.OfType<Button>())
            {
                if (btn.Name == "btnCMTProductionGarmentUnits")
                {
                    grpCMT.Controls.Remove(btn);
                }
            }

            Button btnCMTProductionGarmentUnits = CreateButton(
                                                "btnCMTProductionGarmentUnits", 190, 50, 5, 105,
                                                "Number of Garments (units)",
                                                cmtProduction.ToString(), Brushes.Green);

            btnCMTProductionGarmentUnits.Click += new EventHandler(btnCMTProduction_Click);
            grpCMT.Controls.Add(btnCMTProductionGarmentUnits);
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
                    //MessageBox.Show(lblDataLoading.Text);
                    cmbMachines.SelectedItem = "All Machines";


                    UpdateProgressBar("Sliver Production", 5);
                    GetSpinningMachineProductionTotal();                    
                    UpdateProgressBar("Yarn Production", 12);
                    GetTotalYarnProduced();
                    ShowTotalYarnProducedPerMachineChart();
                    //GetCardQAMeasurementTotal();
                    //GetRSBQAMeasurementTotal();
                    //GetSpinningMachineTotal();
                    UpdateProgressBar("Greige Production", 25);
                    GetTotalGreigeProduced();
                    ShowTotalGreigeProducedPerGradeChart("All Machines");
                    UpdateProgressBar("Dye Production", 45);
                    GetFabricDyedTotals();
                    //CreateFabricDyedTotalsNormalised();
                    UpdateProgressBar("Cut Production", 85);
                    GetTotalCuttingProduced();
                    UpdateProgressBar("CMT Production", 95);
                    GetCMTProductionTotals();
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

                    cmbMachines.SelectedItem = "All Machines";

                    UpdateProgressBar("Sliver Production", 5);
                    GetSpinningMachineProductionTotal();
                    UpdateProgressBar("Yarn Production", 12);
                    GetTotalYarnProduced();
                    ShowTotalYarnProducedPerMachineChart();
                    //GetCardQAMeasurementTotal();
                    //GetRSBQAMeasurementTotal();
                    //GetSpinningMachineTotal();
                    UpdateProgressBar("Greige Production", 25);
                    GetTotalGreigeProduced();
                    ShowTotalGreigeProducedPerGradeChart("All Machines");
                    UpdateProgressBar("Dye Production", 45);
                    GetFabricDyedTotals();
                    //CreateFabricDyedTotalsNormalised();
                    UpdateProgressBar("Cut Production", 95);
                    GetTotalCuttingProduced();
                    UpdateProgressBar("CMT Production", 85);
                    GetCMTProductionTotals();
                    UpdateProgressBar("Data loaded", 100);
                }
            }

            if (prgBarKPI.Value == 100)
                lblDataLoading.Text = "Data Loaded";
            else
                lblDataLoading.Text = "Data Loading...";
            this.Cursor = Cursors.Arrow;
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

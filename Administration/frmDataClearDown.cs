using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using EntityFramework.Extensions;

using LinqKit;


namespace Administration
{
    public partial class frmDataClearDown : Form
    {
        protected readonly TTI2Entities _context;
        private System.ComponentModel.BackgroundWorker backGroundWorker1;
        DateTime DSelected;
        ProgressBar pBar = new ProgressBar();
        int ProgressState = 0;

        public frmDataClearDown()
        {
            InitializeComponent();
            this._context = new TTI2Entities();
            this.backGroundWorker1 = new System.ComponentModel.BackgroundWorker();
            
            this.backGroundWorker1.DoWork += new DoWorkEventHandler(this.BackGroundWork1_DoWork);
            this.backGroundWorker1.WorkerReportsProgress = true;
            this.backGroundWorker1.WorkerSupportsCancellation = true;
           
            this.backGroundWorker1.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            this.backGroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
          
            
        }

        private void frmDataClearDown_Load(object sender, EventArgs e)
        {
            dtpPriorDate.Value = DateTime.Now.AddDays(-1 * DateTime.Now.DayOfYear);
            
        }

        private void BackGroundWork1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker helperBW = sender as BackgroundWorker;
            int arg = (int)e.Argument;
            e.Result = BackgroundProcessLogicMethod(helperBW, arg);

            if (helperBW.CancellationPending)
            {
                e.Cancel = true;
            }

        }
        private void Worker_ProgressChanged(object sender,
                                     ProgressChangedEventArgs e)
        {
            // This is where you would have the UI related changes. 
            //In your case updating the progressbar. 
            // While the files are being copied this would update the UI.
            if(e.ProgressPercentage <= 100)
               pBar1.Value = e.ProgressPercentage;
           
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.backGroundWorker1.CancelAsync();
        }
        
       
        int BackgroundProcessLogicMethod(BackgroundWorker bw, int a)
        {
            int result = 0;
            var BoxSold = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Sold && (DateTime)x.TLSOH_SoldDate <= DSelected).GroupBy(x => x.TLSOH_CutSheet_FK).ToList();
            int Cnt = 0;
            int Before = 0;

            foreach (var BoxGroup in BoxSold)
            {
                var CutSheet_Pk = BoxGroup.FirstOrDefault().TLSOH_CutSheet_FK;
                var WcCount = _context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_CutSheet_FK == CutSheet_Pk).Count();
                var QtySold = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_CutSheet_FK == CutSheet_Pk).Count();

                if ((QtySold >= WcCount) || (WcCount - QtySold < 2))
                {
                    var CutSheet = _context.TLCUT_CutSheet.Find(CutSheet_Pk);
                    if (CutSheet != null)
                    {
                        CutSheet.TLCUTSH_MarkedForDeletion = true;
                    }
                }

                if (++Cnt > 500)
                {
                    ProgressState = Before + Cnt;
                    bw.ReportProgress(ProgressState);
                    Before += Cnt;
                    Cnt = 0; 
                }
               
            }
            try
            {
                _context.SaveChanges();

                DialogResult res = MessageBox.Show("Customer clear down successfully completed", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (res == DialogResult.No)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message " + ex.InnerException);
             
            }

           return result;
        }
        private void btnCommence_Click(object sender, EventArgs e)
        {
            DSelected = Convert.ToDateTime(dtpPriorDate.Value.ToShortDateString());

           //===========================================================================
           
            textBox1.Text = "Commencing Customer Services Clear Down";
            this.Refresh();
            pBar1.Maximum = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Sold && (DateTime)x.TLSOH_SoldDate <= DSelected).GroupBy(x => x.TLSOH_CutSheet_FK).Count();
            pBar.Step = 1;

            _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Sold && x.TLSOH_CutSheet_FK == 0 && x.TLSOH_SoldDate <= DSelected).Delete();

            try
            {
                _context.Configuration.AutoDetectChangesEnabled = false;
                _context.TLCUT_CutSheet.Update(x => new TLCUT_CutSheet() { TLCUTSH_MarkedForDeletion = false });

            }
            finally
            {
                _context.Configuration.AutoDetectChangesEnabled = true;
            }

            if (!backGroundWorker1.IsBusy)
            {
                backGroundWorker1.RunWorkerAsync(2000);

                pBar1.Visible = true;

                textBox1.Text = "Commencing Customer Services Delete";
                this.Refresh();
               
            }

            /*try
            {
                _context.SaveChanges();

                DialogResult res = MessageBox.Show("Customer clear down successfully completed", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (res == DialogResult.No)
                {
                    Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message " + ex.InnerException);
                return;
            }*/

        

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if(backGroundWorker1.IsBusy)
            {
                backGroundWorker1.CancelAsync();
            }
        }

        private void BackGroundProcessLogicMethod(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
               MessageBox.Show("Operation was cancelled");
            }
        }

        private void frmDataClearDown_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if (backGroundWorker1.IsBusy)
                {
                    backGroundWorker1.CancelAsync();
                }
            }
        }
    }
    
}

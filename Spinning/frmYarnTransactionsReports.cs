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

namespace Spinning
{
    public partial class frmYarnTransactionsReports : Form
    {
        int ReportSelected = 0;
        int[] transNumber;

        public frmYarnTransactionsReports()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {
            Util core = new Util();

            transNumber = new int[4];
            transNumber[0] = 800;        //  Report No 1  -----   Issues to Knitting
            transNumber[1] = 900;        //  Report No 2  -----   Yarn Sold  
            transNumber[2] = 1000;       //  Report No 3  -----   Yarn Scrapped
            transNumber[3] = 1100;       //  report No 4  ------  Yarn Adjustments 

            ReportSelected = 0;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && oRad.Checked)
            {
                ReportSelected = 1;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && oRad.Checked)
            {
                ReportSelected = 2;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && oRad.Checked)
            {
                ReportSelected = 3;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && oRad.Checked)
            {
                ReportSelected = 4;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                YarnTOptions repOptions = new YarnTOptions();
                repOptions.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                repOptions.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                repOptions.ReportSelected = ReportSelected;

                if (radioButton1.Checked)
                    repOptions.TransNumber = transNumber[0];
                else if (radioButton2.Checked)
                    repOptions.TransNumber = transNumber[1];
                else if (radioButton3.Checked)
                    repOptions.TransNumber = transNumber[2];
                else if (radioButton4.Checked)
                    repOptions.TransNumber = transNumber[3];
                
                frmViewReport vRep = new frmViewReport(13, repOptions);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && oRad.Checked)
            {
                ReportSelected = 0;
            }
        }

    }

    public class YarnTOptions
    {
        public YarnTOptions()
        {

        }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int ReportSelected { get; set; }
        public int TransNumber { get; set; }

        public bool YarnOrderAudit { get; set; }
        public bool KnitOrderAudit { get; set; } 
    }
}

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
    public partial class frmStockStatusSelection : Form
    {
        public frmStockStatusSelection()
        {
            InitializeComponent();
            SetUp();
        }

        private void SetUp()
        {
            rbOrderStatWIP.Checked     = false;
            rbStockStatMachine.Checked = false;
            rbStockStatTex.Checked     = false;
            rbStockStatYO.Checked      = false;
              
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null)
            {
               
                StockStatusReportOptions stat = new StockStatusReportOptions();
                //Yarn Stock Status
                //-----------------------------------------
                if (rbStockStatYO.Checked)
                {
                    stat.YSSOption = 1;
                }
                else if (rbStockStatTex.Checked)
                {
                    stat.YSSOption = 2;
                }
                else if (rbStockStatMachine.Checked)
                {
                    stat.YSSOption = 3;
                }
                // Yarn Order Status
                //------------------------------------------------------------
                if (rbOrderStatWIP.Checked)
                {
                    stat.OrderStatus = 1;
                    stat.YSSOption = 0;
                }
               
                //---------------------------------------------------------------------
                // 
                
                //---------------------------------------------------------------------------
                // Dates 
                //----------------------------------
                stat.DateFrom = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                stat.DateTo = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());

                //=================================================================

                frmViewReport vRep = new frmViewReport(12, stat);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                SetUp();
            }

        }
    }

    public class StockStatusReportOptions
    {
        public StockStatusReportOptions()
        {
        }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int YSSOption { get; set; }     
        public int OrderStatus { get; set;}
        public int StoreSelected { get; set; } 
    }
}

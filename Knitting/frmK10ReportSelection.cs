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


namespace Knitting
{
    public partial class frmK10ReportSelection : Form
    {
        public frmK10ReportSelection()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                cmboStockTake.DataSource = context.TLADM_StockTakeFreq.ToList();
                cmboStockTake.ValueMember = "STF_Pk";
                cmboStockTake.DisplayMember = "STF_Description";

                cmboProduct.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmboProduct.DisplayMember = "TLGreige_Description";
                cmboProduct.ValueMember = "TLGreige_Id";

                cmboStore.DataSource = context.TLADM_WhseStore.Where(x => !x.WhStore_WhseOrStore).OrderBy(x => x.WhStore_Description).ToList();
                cmboStore.ValueMember = "WhStore_Id";
                cmboStore.DisplayMember = "WhStore_Description";

                cmboProductGroup.DataSource = context.TLADM_GreigeQuality.OrderBy(x => x.GQ_Description).ToList();
                cmboProductGroup.ValueMember = "GQ_Pk";
                cmboProductGroup.DisplayMember = "GQ_Description";

            }

        }
    }
}

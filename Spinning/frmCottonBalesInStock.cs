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
    public partial class frmCottonBalesInStock : Form
    {
        TLSPN_CottonTransactions cotreceived = null;

        public frmCottonBalesInStock()
        {
            InitializeComponent();
            using (var context = new TTI2Entities())
            {
                var existing = context.AltSelectCottonRecords().OrderBy(x=>x.cotrx_LotNo).ToList();
                foreach (var row in existing)
                {
                    cotreceived = new TLSPN_CottonTransactions();
                    cotreceived.cotrx_pk = row.cotrx_pk;
                    cotreceived.cotrx_LotNo = row.cotrx_LotNo;

                    comboLotNo.Items.Add(cotreceived);

                }
                comboLotNo.ValueMember = "cotrx_LotNo";
                comboLotNo.DisplayMember = "cotrx_LotNo";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                var CotTrans = (TLSPN_CottonTransactions)comboLotNo.SelectedItem;
                if (CotTrans == null)
                {
                    MessageBox.Show("Please select a Lot number from the designated drop down box");
                    return;

                }

                ReportOptions repOpts = new ReportOptions();
                repOpts.Cotton_LotNo = CotTrans.cotrx_LotNo;

                frmViewReport vRep = new frmViewReport(22, repOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}

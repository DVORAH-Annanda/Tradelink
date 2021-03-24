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
        SpinningRepository repo;
        SpinningQueryParameters QueryParms;
        bool FormLoaded;
        public frmCottonBalesInStock()
        {
            InitializeComponent();
            repo = new SpinningRepository();

            this.comboLotNo.CheckStateChanged += new System.EventHandler(this.cmboLot_CheckStateChanged);
        }
        private void frmCottonBalesInStock_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new SpinningQueryParameters();
            if(comboLotNo.Items.Count != 0)
            {
                comboLotNo.Items.Clear();
            }

            using (var context = new TTI2Entities())
            {
                var GrpBalesinStock = context.TLSPN_CottonReceivedBales.Where(x=>!x.CoBales_IssuedToProd &&x.CotBales_ConfirmedByQA).OrderBy(x=>x.CotBales_LotNo).GroupBy(x => x.CotBales_LotNo).ToList();
                foreach (var Grp in GrpBalesinStock)
                {
                    comboLotNo.Items.Add(new Spinning.CheckComboBoxItem(Grp.FirstOrDefault().CotBales_LotNo , Grp.FirstOrDefault().CotBales_LotNo.ToString(), false));
                }
            }
            
            chkSummarised.Checked = false;

          
            FormLoaded = true;
        }

        private void cmboLot_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is Spinning.CheckComboBoxItem&& FormLoaded)
            {
                Spinning.CheckComboBoxItem item = (Spinning.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    QueryParms.CotReceivedBales.Add(repo.LoadLotNo(item._Pk));

                }
                else
                {
                    var value = QueryParms.CotReceivedBales.Find(it => it.CotBales_Pk == item._Pk);
                    if (value != null)
                        QueryParms.CotReceivedBales.Remove(value);

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                QueryParms.CottonRecSummarised = chkSummarised.Checked; 

                if(QueryParms.CotReceivedBales.Count == 0)
                {
                    MessageBox.Show("Please select a Lot number from the designated drop down box");
                    return;

                }

                
                Spinning.frmViewReport vRep = new Spinning.frmViewReport(22, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                frmCottonBalesInStock_Load(this, null); 
            }
        }

        private void comboLotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                if (!oCmbo.DroppedDown)
                    oCmbo.DroppedDown = true;
            }
        }
    }
}

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

namespace CustomerServices
{
    public partial class frmViewPickList : Form
    {
        string Mach_IP;
      
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Box Number 0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Box Qty    1 
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Grade      2
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Style      3
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // Colour     4
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();   // Size       5
      

        public frmViewPickList(String IP)
        {
            InitializeComponent();
            Mach_IP = IP;

            oTxtA.HeaderText = "Box Number";
            oTxtA.ValueType = typeof(string);
            oTxtA.Visible = true;
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.HeaderText = "Boxed Qty";
            oTxtB.ValueType = typeof(Int32);
            oTxtB.Visible = true;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Grade";
            oTxtC.ValueType = typeof(string);
            oTxtC.Visible = true;
            dataGridView1.Columns.Add(oTxtC);

            oTxtD.HeaderText = "Style";
            oTxtD.ValueType = typeof(string);
            oTxtD.Visible = true;
            dataGridView1.Columns.Add(oTxtD);

            oTxtE.HeaderText = "Colour";
            oTxtE.ValueType = typeof(string);
            oTxtE.Visible = true;
            dataGridView1.Columns.Add(oTxtE);

            oTxtF.HeaderText = "Size";
            oTxtF.ValueType = typeof(string);
            oTxtF.Visible = true;
            dataGridView1.Columns.Add(oTxtF);
        }

        private void frmViewPickList_Load(object sender, EventArgs e)
        {
            int cnt = 0;
          
            using (var context = new TTI2Entities())
            {
                var Existing = context.TLCSV_PickingListMaster.Where(x => x.TLPL_IPAddress == Mach_IP).ToList();
                foreach (var Row in Existing)
                {
                    var StockOnHand = context.TLCSV_StockOnHand.Find(Row.TLPL_StockOnHand_FK);
                    if (StockOnHand != null)
                    {
                        ++cnt;
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = StockOnHand.TLSOH_BoxNumber;
                        dataGridView1.Rows[index].Cells[1].Value = StockOnHand.TLSOH_BoxedQty;
                        dataGridView1.Rows[index].Cells[2].Value = StockOnHand.TLSOH_Grade;
                        dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Styles.Find(StockOnHand.TLSOH_Style_FK).Sty_Description;
                        dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(StockOnHand.TLSOH_Colour_FK).Col_Display;
                        dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Sizes.Find(StockOnHand.TLSOH_Size_FK).SI_Description;
                    }
                }
                
                var idx = dataGridView1.Rows.Add();
                dataGridView1.Rows[idx].Cells[0].Value = "No of Lines " + cnt.ToString();
             
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null)
                this.Close();
        }
    }
}

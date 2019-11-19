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

namespace CMT
{
    public partial class frmCMTOnHoldDetails : Form
    {
        int _CSR_FK;

        DataGridViewTextBoxColumn oTxtA;   // Primary Palet Index no
        DataGridViewTextBoxColumn oTxtB;   // Pallet Number
        DataGridViewTextBoxColumn oTxtC;   // Pallet Nett Weight 
        DataGridViewTextBoxColumn oTxtD;   // Pallet Cones Available
        DataGridViewTextBoxColumn oTxtE;   // Pallet Cones Reserved

        DataGridViewCheckBoxColumn oChkA;


        public frmCMTOnHoldDetails(int CSR)
        {
            InitializeComponent();
            _CSR_FK = CSR;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            //Initialise the dataGrid
            //------------------------------------

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Record Key 
            oTxtA.HeaderText = "Key";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;

            oTxtB = new DataGridViewTextBoxColumn();   // 1 Shift Description
            oTxtB.HeaderText = "Shift Description";
            oTxtB.ValueType = typeof(string);
            oTxtB.Visible = true;

            oTxtC = new DataGridViewTextBoxColumn();  // 2 Time Start 
            oTxtC.HeaderText = "Time Start";
            oTxtC.ValueType = typeof(TimeSpan);
            oTxtC.Visible = true;

            oTxtD = new DataGridViewTextBoxColumn();  // 3 Time End 
            oTxtD.HeaderText = "Time End";
            oTxtD.ValueType = typeof(TimeSpan);
            oTxtD.Visible = true;

            dataGridView1.Columns.Add(oTxtA);    // CSRD Key 
            dataGridView1.Columns.Add(oTxtB);    // Pallet No
            dataGridView1.Columns.Add(oTxtC);    // Nett Weight  
            dataGridView1.Columns.Add(oTxtD);    // Cones Available
        }

        private void frmCMTOnHoldDetails_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                var CSR = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == _CSR_FK).FirstOrDefault();
                if (CSR != null)
                {
                    var Details = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CSR.TLCUTSHR_CutSheet_FK).ToList();
                    foreach (var Detail in Details)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Detail.TLCUTSHRD_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;

                    }
                }
            }
        }
    }
}

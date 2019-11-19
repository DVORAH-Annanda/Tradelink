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
    public partial class CMTCutSheetView : Form
    {
        DataGridViewTextBoxColumn oTxtA;   // Primary Box Number
        DataGridViewTextBoxColumn oTxtB;   // Size
        DataGridViewTextBoxColumn oTxtC;   // Grade 
        DataGridViewTextBoxColumn oTxtD;   // Line No
        DataGridViewTextBoxColumn oTxtE;   // Box Qty
        DataGridViewTextBoxColumn oTxtF;   // Box Weight

        public CMTCutSheetView()
        {
            InitializeComponent();

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Record Key 
            oTxtA.HeaderText = "Box Number";
            oTxtA.ValueType = typeof(String);
          
            oTxtB = new DataGridViewTextBoxColumn();   // 1 Shift Description
            oTxtB.HeaderText = "Size";
            oTxtB.ValueType = typeof(string);
            oTxtB.Visible = true;

            oTxtC = new DataGridViewTextBoxColumn();  // 2 Time Start 
            oTxtC.HeaderText = "Grade";
            oTxtC.ValueType = typeof(string);
            oTxtC.Visible = true;

            oTxtD = new DataGridViewTextBoxColumn();  // 3 Time End 
            oTxtD.HeaderText = "Line No";
            oTxtD.ValueType = typeof(string);
            oTxtD.Visible = true;

            oTxtE = new DataGridViewTextBoxColumn();  // 3 Time End 
            oTxtE.HeaderText = "Boxed Qty";
            oTxtE.ValueType = typeof(int);
            oTxtE.Visible = true;

            oTxtF = new DataGridViewTextBoxColumn();  // 3 Time End 
            oTxtF.HeaderText = "Box Weight";
            oTxtF.ValueType = typeof(decimal);
            oTxtF.Visible = true;
            
         


            dataGridView1.Columns.Add(oTxtA);    // Pallet No Key 
            dataGridView1.Columns.Add(oTxtB);    // Pallet No
            dataGridView1.Columns.Add(oTxtC);    // Nett Weight  
            dataGridView1.Columns.Add(oTxtD);    // Cones Available
            dataGridView1.Columns.Add(oTxtE);    // Cones Available
            dataGridView1.Columns.Add(oTxtF);    // Cones Available

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox oText = sender as TextBox;
            if (oText != null && oText.Text.Length > 0)
            {
                using (var context = new TTI2Entities())
                {
                   
                }

            }
        }
    }
}

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
namespace Cutting
{
    public partial class frmKnitView : Form
    {
        bool FormLoaded;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;
        DataGridViewTextBoxColumn oTxtBoxG;
        DataGridViewTextBoxColumn oTxtBoxH;
        DataGridViewTextBoxColumn oTxtBoxJ;

        DataGridViewCheckBoxColumn oChkA;

        public frmKnitView()
        {
            InitializeComponent();

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ValueType = typeof(int);
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "CutSheet Number";
            oTxtBoxB.ValueType = typeof(string); 

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "DyeBatch Number";
            oTxtBoxC.ValueType = typeof(string);

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "DyeBatch Number";
            oTxtBoxD.ValueType = typeof(DateTime);

            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.HeaderText = "Quality";
            oTxtBoxE.ValueType = typeof(string);

            oTxtBoxF = new DataGridViewTextBoxColumn();
            oTxtBoxF.HeaderText = "Colour";
            oTxtBoxF.ValueType = typeof(string);
            
            oTxtBoxG = new DataGridViewTextBoxColumn();
            oTxtBoxG.HeaderText = "Size";
            oTxtBoxG.ValueType = typeof(string);

            oTxtBoxH = new DataGridViewTextBoxColumn();
            oTxtBoxH.HeaderText = "Style";
            oTxtBoxH.ValueType = typeof(string);

            oTxtBoxJ = new DataGridViewTextBoxColumn();
            oTxtBoxJ.HeaderText = "Customer";
            oTxtBoxJ.ValueType = typeof(string);
            
            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns.Add(oTxtBoxD);
            dataGridView1.Columns.Add(oTxtBoxE);
            dataGridView1.Columns.Add(oTxtBoxF);
            dataGridView1.Columns.Add(oTxtBoxG);
            dataGridView1.Columns.Add(oTxtBoxH);
            dataGridView1.Columns.Add(oTxtBoxJ);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

        }

        private void frmKnitView_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var Existing = context.TLCUT_CutSheet.Where(x => !x.TLCutSH_WIPComplete && x.TLCutSH_Accepted).ToList();
                foreach (var Row in Existing)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Row.TLCutSH_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = Row.TLCutSH_No;
                    dataGridView1.Rows[index].Cells[3].Value = context.TLDYE_DyeBatch.Find(Row.TLCutSH_DyeBatch_FK).DYEB_BatchNo;
                    dataGridView1.Rows[index].Cells[4].Value = Row.TLCutSH_Date.ToShortDateString();
                    dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Griege.Find(Row.TLCutSH_Quality_FK).TLGreige_Description;
                    dataGridView1.Rows[index].Cells[6].Value = context.TLADM_Colours.Find(Row.TLCutSH_Colour_FK).Col_Display;
                    if(Row.TLCutSH_Size_FK != 0)
                        dataGridView1.Rows[index].Cells[7].Value = context.TLADM_Sizes.Find(Row.TLCutSH_Size_FK).SI_Description;
                    dataGridView1.Rows[index].Cells[8].Value = context.TLADM_Styles.Find(Row.TLCutSH_Styles_FK).Sty_Description;
                    dataGridView1.Rows[index].Cells[9].Value = context.TLADM_CustomerFile.Find(Row.TLCutSH_Customer_FK).Cust_Description;
                }
            }
            FormLoaded = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
          
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    var CurrentRow = oDgv.CurrentRow;
                    var Pk = (int)CurrentRow.Cells[0].Value;
                   
                    
                }
            }
        }
    }
}

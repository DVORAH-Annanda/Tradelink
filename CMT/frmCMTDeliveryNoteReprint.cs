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
    public partial class frmCMTDeliveryNoteReprint : Form
    {
        bool FormLoaded;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key File Record         0
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // Check Box to select             1
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Delivery Note No                2
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();  //  Picking List No                 3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();  //  Transaction Date                

        CMTQueryParameters QueryParms;
        CMTRepository repo;

        public frmCMTDeliveryNoteReprint()
        {
            InitializeComponent();

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            //Initialise the dataGrid
            //------------------------------------

            oTxtA = new DataGridViewTextBoxColumn();   // 0 Record Key 
            oTxtA.HeaderText = "Key";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(Int32);
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();  // 1 Check Box
            oChkA.ValueType = typeof(Boolean);
            oChkA.HeaderText = "Select";
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();   // 2 Delivery Note No 
            oTxtB.HeaderText = "Delivery Note No";
            oTxtB.ValueType = typeof(Int32);
            dataGridView1.Columns.Add(oTxtB);
            oTxtB.ReadOnly = true;

            oTxtC = new DataGridViewTextBoxColumn();   // 3 Picking List No 
            oTxtC.HeaderText = "Picking List No";
            oTxtC.ValueType = typeof(Int32);
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);


            oTxtC = new DataGridViewTextBoxColumn();   // 4 Date 
            oTxtC.HeaderText = "Picking List No";
            oTxtC.ValueType = typeof(DateTime);
            oTxtC.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtC);

            repo = new CMTRepository();
        }

        private void frmCMTDeliveryNoteReprint_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new CMTQueryParameters();

            using (var context = new TTI2Entities())
            {
                DateTime StartDate = DateTime.Now.AddMonths(-6);

                var DeliveryNotes = context.TLCMT_PanelIssue.Where(x => x.CMTPI_Closed && x.CMTPI_Date >= StartDate).ToList();
                foreach (var DeliveryNote in DeliveryNotes)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = DeliveryNote.CMTPI_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = DeliveryNote.CMTPI_DeliveryNumber;
                    dataGridView1.Rows[index].Cells[3].Value = DeliveryNote.CMTPI_Number;
                    var dt = DeliveryNote.CMTPI_Date.ToString("dd/MM/yyyy");
                    dataGridView1.Rows[index].Cells[4].Value = DeliveryNote.CMTPI_Date;

                }
            }
            FormLoaded = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                var CurrentRow = oDgv.CurrentRow;

                var AllReadyTicked = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                      where (bool)Rows.Cells[1].Value == true
                                      select Rows).ToList();

                foreach (DataGridViewRow Row in AllReadyTicked)
                {
                    if (Row.Index == CurrentRow.Index)
                        continue;

                    dataGridView1.Rows[Row.Index].Cells[1].Value = false;
                }
               
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {

                var AllReadyTicked = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                      where (bool)Rows.Cells[1].Value == true
                                      select Rows).FirstOrDefault();
                
                if (AllReadyTicked != null)
                {
                    var Pk = (int)AllReadyTicked.Cells[0].Value;

                                   
                    
                    frmCMTViewRep vRep = new frmCMTViewRep(2, Pk, true);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    if (vRep != null)
                    {
                        vRep.Close();
                        vRep.Dispose();
                    }

                }

            }
        }
    }
}

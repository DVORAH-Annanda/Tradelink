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
    public partial class frmBIFTruckLoad : Form
    {
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;

        DataGridViewCheckBoxColumn oChkA;

        DataGridViewTextBoxColumn oTxtBoxAA;
        DataGridViewTextBoxColumn oTxtBoxBA;
        DataGridViewTextBoxColumn oTxtBoxCA;
        DataGridViewTextBoxColumn oTxtBoxDA;
        DataGridViewTextBoxColumn oTxtBoxEA;
        DataGridViewTextBoxColumn oTxtBoxFA;

        DataGridViewCheckBoxColumn oChkB;

        bool FormLoaded;

        Util core;


        public frmBIFTruckLoad()
        {
            InitializeComponent();

            oTxtBoxAA = new DataGridViewTextBoxColumn();
            oTxtBoxAA.ReadOnly = true;
            oTxtBoxAA.ValueType = typeof(Int32);
            oTxtBoxAA.Visible = false;
            oTxtBoxAA.HeaderText = "Primary Key";
            dataGridView2.Columns.Add(oTxtBoxAA);

            oChkB = new DataGridViewCheckBoxColumn();
            oChkB.ValueType = typeof(System.Boolean);
            oChkB.HeaderText = "Select";
            dataGridView2.Columns.Add(oChkB);

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.ValueType = typeof(Int32);
            oTxtBoxB.Visible = true;
            oTxtBoxB.HeaderText = "Picking List";
            dataGridView2.Columns.Add(oTxtBoxB);

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.ReadOnly = true;
            oTxtBoxC.ValueType = typeof(DateTime);
            oTxtBoxC.Visible = true;
            oTxtBoxC.HeaderText = "Date";
            dataGridView2.Columns.Add(oTxtBoxC);

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.ReadOnly = true;
            oTxtBoxD.ValueType = typeof(Int32);
            oTxtBoxD.Visible = true;
            oTxtBoxD.HeaderText = "From WareHouse";
            dataGridView2.Columns.Add(oTxtBoxD);

            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.ReadOnly = true;
            oTxtBoxE.ValueType = typeof(Int32);
            oTxtBoxE.Visible = true;
            oTxtBoxE.HeaderText = "To WareHouse";
            dataGridView2.Columns.Add(oTxtBoxE);

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToOrderColumns = false;
        }

        private void frmBIFTruckLoad_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var BoughtInFabrics = context.TLDYE_BIFInTransit.Where(x => x.BIFT_PickingList && !x.BIFT_Despatched).OrderBy(x => x.BIFT_PickingList_Number).ToList();
                foreach (var Piece in BoughtInFabrics)
                {
                    var index = dataGridView2.Rows.Add();
                    dataGridView2.Rows[index].Cells[0].Value = Piece.BIFT_Pk;
                    dataGridView2.Rows[index].Cells[1].Value = false;
                    dataGridView2.Rows[index].Cells[2].Value = Piece.BIFT_PickingList_Number;
                    dataGridView2.Rows[index].Cells[3].Value = Convert.ToDateTime(Piece.BIFT_PickingList_Date.ToShortDateString());
                    dataGridView2.Rows[index].Cells[4].Value = context.TLADM_WhseStore.Find((int)Piece.BIFT_FromFabric_FK).WhStore_Description ;
                    dataGridView2.Rows[index].Cells[5].Value = context.TLADM_WhseStore.Find((int)Piece.BIFT_ToFabric_FK).WhStore_Description;
                }
            }
            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            int LastNumberUsed = 0;

            if (oBtn != null && FormLoaded)
            {
                CutReportOptions ops = new CutReportOptions();
                using ( var context = new TTI2Entities())
                {
                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "CUT").FirstOrDefault();
                    if (Dept != null)
                    {
                        var LstNumberUsed = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();
                        if (LstNumberUsed != null)
                        {
                            LastNumberUsed = LstNumberUsed.col4;
                            LstNumberUsed.col4 += 1;
                        }
                     
                    }

                    foreach (DataGridViewRow Row in dataGridView2.Rows)
                    {
                        bool IsChecked = (bool)Row.Cells[1].Value;
                        if (!IsChecked)
                            continue;

                        int Pk = (int)Row.Cells[0].Value;
                        ops.BIFTransit.Add(Pk);

                        var BIFInTrans = context.TLDYE_BIFInTransit.Find(Pk);
                        if (BIFInTrans != null)
                        {
                            var BifDetails = context.TLDYE_BIFInTransitDetails.Where(x => x.BIFD_Intransit_FK == BIFInTrans.BIFT_Pk).ToList();
                            foreach (var Detail in BifDetails)
                            {
                                var DBDetail = context.TLDYE_DyeBatchDetails.Find(Detail.BIFD_DyeBatchDetail_FK);
                                if (DBDetail != null)
                                {
                                    DBDetail.DYEBO_CurrentStore_FK = BIFInTrans.BIFT_ToFabric_FK;  
                                }
                            }

                            BIFInTrans.BIFT_Delivery_Number = LastNumberUsed;

                             
                            BIFInTrans.BIFT_Despatched = true;
                            BIFInTrans.BIFT_Despatched_Date = DateTime.Now;


                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                 }

                Cutting.frmCutViewRep vRep = new Cutting.frmCutViewRep(17, ops);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog();
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
            }
        }

        
    }
}

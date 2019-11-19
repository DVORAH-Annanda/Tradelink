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

namespace DyeHouse
{
    public partial class frmFabricSalesDelivery : Form
    {
        bool formloaded;
        public frmFabricSalesDelivery()
        {
            InitializeComponent();
        }

        private void frmFabricSalesDelivery_Load(object sender, EventArgs e)
        {
            formloaded = false;
            DataGridViewTextBoxColumn oTxtIndex = new DataGridViewTextBoxColumn();
            oTxtIndex.HeaderText = "Index";
            oTxtIndex.ValueType = typeof(int);
            oTxtIndex.ReadOnly = true;
            oTxtIndex.Visible = false;

            DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.HeaderText = "Piece No";
            oTxtA.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.HeaderText = "Quality";
            oTxtB.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.HeaderText = "Colour";
            oTxtC.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.HeaderText = "Gross Weight";
            oTxtD.ReadOnly = true;
            oTxtD.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.HeaderText = "Nett Weight";
            oTxtE.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.HeaderText = "Disk";
            oTxtF.ReadOnly = true;
            oTxtF.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();
            oTxtG.HeaderText = "Length %";
            oTxtG.ValueType = typeof(decimal);
            oTxtG.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();
            oTxtH.HeaderText = "Width %";
            oTxtH.ValueType = typeof(decimal);
            oTxtH.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn();
            oTxtJ.HeaderText = "Spirality %";
            oTxtJ.ValueType = typeof(decimal);
            oTxtJ.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtK = new DataGridViewTextBoxColumn();
            oTxtK.HeaderText = "Index position of Batch";
            oTxtK.ValueType = typeof(decimal);
            oTxtK.Visible = false;
            oTxtK.ReadOnly = true;

            DataGridViewCheckBoxColumn oChk = new DataGridViewCheckBoxColumn();
            oChk.HeaderText = "Select";
            oChk.ValueType = typeof(bool);

            dataGridView1.Columns.Add(oTxtIndex);  //0 Index
            dataGridView1.Columns.Add(oTxtA);      //1 Piece No
            dataGridView1.Columns.Add(oTxtB);      //2 Quality
            dataGridView1.Columns.Add(oTxtC);      //3 Color
            dataGridView1.Columns.Add(oTxtD);      //4 Gross Weight
            dataGridView1.Columns.Add(oTxtE);      //5 Nett Weight
            dataGridView1.Columns.Add(oTxtF);      //6 Disk
           // dataGridView1.Columns.Add(oTxtG);      //7 Length %
           // dataGridView1.Columns.Add(oTxtH);      //8 Width %
           // dataGridView1.Columns.Add(oTxtJ);      //9 Spirality
            dataGridView1.Columns.Add(oChk);       //10 Check
            dataGridView1.Columns.Add(oTxtK);      //11 Batch Foreign Key
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            using (var context = new TTI2Entities())
            {
                IList<TLDYE_DyeTransactions> batch = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_TransactionType == 48).ToList();
                cmboPendingSales.DataSource = batch;
                cmboPendingSales.DisplayMember = "TLDYET_TransactionNumber";
                cmboPendingSales.ValueMember = "TLDYET_TransactionNumber";
            }

            formloaded = true;
        }

        private void cmboPendingSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLDYE_DyeTransactions)oCmbo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_TransactionNo == selected.TLDYET_TransactionNumber).ToList();
                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.DYEBD_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = context.TLKNI_GreigeProduction.Find(row.DYEBD_GreigeProduction_FK).GreigeP_PieceNo;
                            dataGridView1.Rows[index].Cells[2].Value = context.TLADM_Griege.Find(row.DYEBD_QualityKey).TLGreige_Description;
                            var BD = context.TLDYE_DyeBatch.Find(row.DYEBD_DyeBatch_FK);
                            if (BD != null)
                                dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Colours.Find(BD.DYEB_Colour_FK).Col_Display;
                            dataGridView1.Rows[index].Cells[4].Value = row.DYEBD_GreigeProduction_Weight;
                            dataGridView1.Rows[index].Cells[5].Value = row.DYEBO_Nett;
                            dataGridView1.Rows[index].Cells[6].Value = row.DYEBO_DiskWeight;

                            //dataGridView1.Rows[index].Cells[7].Value = 0;
                            //dataGridView1.Rows[index].Cells[8].Value = 0;
                            //dataGridView1.Rows[index].Cells[9].Value = 0;

                            dataGridView1.Rows[index].Cells[7].Value = true;
                            dataGridView1.Rows[index].Cells[8].Value = row.DYEBD_DyeBatch_FK;

                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if ((bool)row.Cells[7].Value == false)
                        {
                            var index = (int)row.Cells[0].Value;
                            var Record = context.TLDYE_DyeBatchDetails.Find(index);
                            if (Record != null)
                            {
                                Record.DYEBO_DateSold = null;
                                Record.DYEBO_Sold = false;
                                Record.DYEBO_TransactionNo = string.Empty;
                            }

                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                        string transnumber = cmboPendingSales.SelectedValue.ToString();

                        frmDyeViewReport vRep = new frmDyeViewReport(18, transnumber);
                        vRep.ShowDialog(this);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}

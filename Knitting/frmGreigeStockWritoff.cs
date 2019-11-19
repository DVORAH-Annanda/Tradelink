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
    public partial class frmGreigeStockWritoff : Form
    {
        bool FormLoaded;
        
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewCheckBoxColumn oTxtA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;
        DataGridViewTextBoxColumn oTxtBoxG;
        DataGridViewTextBoxColumn oTxtBoxH;

        public frmGreigeStockWritoff()
        {
            InitializeComponent();
            
            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;
            oTxtBoxA.ValueType = typeof(Int32);

            oTxtA = new DataGridViewCheckBoxColumn();
            oTxtA.HeaderText = "Select";
            oTxtA.ValueType = typeof(Boolean);

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Piece Number";
            oTxtBoxB.ReadOnly = true;

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Piece Weight";
            oTxtBoxC.ValueType = typeof(decimal);
            oTxtBoxC.ReadOnly = true;

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "Machine";
            oTxtBoxD.ValueType = typeof(string);
            oTxtBoxD.ReadOnly = true;

            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.HeaderText = "Quality";
            oTxtBoxE.ValueType = typeof(string);
            oTxtBoxE.ReadOnly = true;

            oTxtBoxF = new DataGridViewTextBoxColumn();
            oTxtBoxF.HeaderText = "Grade";
            oTxtBoxF.ValueType = typeof(string);
            oTxtBoxF.ReadOnly = true;

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns.Add(oTxtBoxD);
            dataGridView1.Columns.Add(oTxtBoxE);
            dataGridView1.Columns.Add(oTxtBoxF                 );

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;


        }

        private void frmGreigeStockWritoff_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboGreige.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmboGreige.ValueMember = "TLGreige_Id";
                cmboGreige.DisplayMember = "TlGreige_Description";
                cmboGreige.SelectedIndex = -1;
            }
            FormLoaded = true;
        }

        private void cmboGreige_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                var Greige = (TLADM_Griege)oCmbo.SelectedItem;
                if (Greige != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Entries = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Greige_Fk == Greige.TLGreige_Id && !x.GreigeP_Dye).ToList();
                        foreach(var Entry in Entries)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = Entry.GreigeP_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = Entry.GreigeP_PieceNo;
                            dataGridView1.Rows[index].Cells[3].Value = Entry.GreigeP_weight;
                            if(Entry.GreigeP_Machine_FK != null)
                              dataGridView1.Rows[index].Cells[4].Value = context.TLADM_MachineDefinitions.Find(Entry.GreigeP_Machine_FK).MD_Description;
                            
                            dataGridView1.Rows[index].Cells[5].Value = context.TLADM_GreigeQuality.Find(Greige.TLGreige_Quality_FK).GQ_Description;
                            dataGridView1.Rows[index].Cells[6].Value = Entry.GreigeP_Grade;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a Greige item from the drop down facility provided");

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender as Button;
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Record in dataGridView1.Rows)
                    {
                        if ((bool)Record.Cells[1].Value == false)
                            continue;

                        int PrimaryKey = (int)Record.Cells[0].Value;
                        var GProd = context.TLKNI_GreigeProduction.Find(PrimaryKey);
                        if (GProd != null)
                        {
                            context.TLKNI_GreigeProduction.Remove(GProd);
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully stored to database");
                        dataGridView1.Rows.Clear();
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessages = new StringBuilder();
                        do
                        {
                            exceptionMessages.Append(ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);

                        MessageBox.Show(exceptionMessages.ToString());
                    }
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender as Button;
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    var GreigeProd = context.TLKNI_GreigeProduction.Where(X => X.GreigeP_PieceNo == txtPieceNumber.Text).FirstOrDefault();
                    if (GreigeProd != null)
                    {
                        if (GreigeProd.GreigeP_Dye)
                        {
                            MessageBox.Show("Records show that this piece number has been sent to dyeing. No further action allowed");
                            return;
                        }

                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = GreigeProd.GreigeP_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = GreigeProd.GreigeP_PieceNo;
                        dataGridView1.Rows[index].Cells[3].Value = GreigeProd.GreigeP_weight;
                        if (GreigeProd.GreigeP_Machine_FK != null)
                            dataGridView1.Rows[index].Cells[4].Value = context.TLADM_MachineDefinitions.Find(GreigeProd.GreigeP_Machine_FK).MD_Description;

                        var Greige = context.TLADM_Griege.Find(GreigeProd.GreigeP_Greige_Fk);
                        if (Greige != null)
                           dataGridView1.Rows[index].Cells[5].Value = context.TLADM_GreigeQuality.Find(Greige.TLGreige_Quality_FK).GQ_Description;
                       
                        dataGridView1.Rows[index].Cells[6].Value = GreigeProd.GreigeP_Grade;
                    }
                    else
                    {
                        MessageBox.Show("Piece Number not found");
                    }

                }
            }
        }
    }
}

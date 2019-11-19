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
    public partial class frmRejectReasons : Form
    {
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();

        bool formloaded;

        public frmRejectReasons()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;

            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oTxtB.HeaderText = "Short Code";
            oTxtB.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtB);

            oTxtC.HeaderText = "Description";
            oTxtC.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtC);
        }

        private void frmRejectReasons_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var Existing = context.TLCUT_RejectReasons.OrderBy(x => x.TLCUTRJR_Pk).ToList();
                foreach (var Record in Existing)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Record.TLCUTRJR_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = Record.TLCUTRJR_ShortCode;
                    dataGridView1.Rows[index].Cells[2].Value = Record.TLCUTRJR_Description;
                }
            }
            formloaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (Row.Cells[1].Value == null)
                            continue;

                        TLCUT_RejectReasons rr = new TLCUT_RejectReasons();
                        bool Add = true;
                        if (Row.Cells[0].Value != null)
                        {
                            var index = (int)Row.Cells[0].Value;
                            rr = context.TLCUT_RejectReasons.Find(index);
                            if (rr == null)
                                rr = new TLCUT_RejectReasons();
                            else
                                Add = false;
                        }

                        string ss = Row.Cells[1].Value.ToString();
                        if(ss.Length > 10)
                            ss = ss.Substring(0, 9);
                        
                        rr.TLCUTRJR_ShortCode   = ss;
                        rr.TLCUTRJR_Description = Row.Cells[2].Value.ToString();

                        if (Add)
                           context.TLCUT_RejectReasons.Add(rr);
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                        dataGridView1.Rows.Clear();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException en)
                    {
                        foreach (var eve in en.EntityValidationErrors)
                        {
                            MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                            foreach (var ve in eve.ValidationErrors)
                            {
                                MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                            }
                        }
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

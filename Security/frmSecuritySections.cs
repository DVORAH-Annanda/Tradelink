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

namespace Security
{
    public partial class frmSecuritySections : Form
    {
        bool formloaded;
        DataGridViewTextBoxColumn oTxtA;  
        DataGridViewTextBoxColumn oTxtB;
        DataGridViewTextBoxColumn oTxtC;

        public frmSecuritySections()
        {
            InitializeComponent();
        }

        private void frmSecuritySections_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cmboDepartments.DataSource = context.TLSEC_Departments.OrderBy(x => x.TLSECDT_Pk).ToList();
                cmboDepartments.ValueMember = "TLSECDT_Pk";
                cmboDepartments.DisplayMember = "TLSECDT_Description";
                cmboDepartments.SelectedIndex = -1;

                oTxtA = new DataGridViewTextBoxColumn();
                oTxtA.Visible = false;
                oTxtA.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtA);

                oTxtB = new DataGridViewTextBoxColumn();
                oTxtB.Visible = false;
                oTxtB.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtB);

                oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.HeaderText = "Application Sections";
                oTxtC.ValueType = typeof(string);
                oTxtC.Width = 250;
                dataGridView1.Columns.Add(oTxtC);

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
                dataGridView1.AutoGenerateColumns = false;
            }

            formloaded = true;

        }

        private void cmboDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCombo = sender as ComboBox;
            if (oCombo != null && formloaded)
            {
                var selected =  (TLSEC_Departments)oCombo.SelectedItem;
                if (selected != null)
                {
                    dataGridView1.Rows.Clear();
                    using (var context = new TTI2Entities())
                    {
                       
                        var Existing = context.TLSEC_Sections.Where(x => x.TLSECSect_Department_FK == selected.TLSECDT_Pk).ToList();
                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.TLSECSect_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = row.TLSECSect_Department_FK;
                            dataGridView1.Rows[index].Cells[2].Value = row.TLSECSect_Description;

                        }
                        
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var selected = (TLSEC_Departments)cmboDepartments.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[2].Value == null)
                                continue;

                            bool Add = true;
                            TLSEC_Sections sec = new TLSEC_Sections();
                            if (row.Cells[0].Value != null)
                            {
                                var index = (int)row.Cells[0].Value;
                                sec = context.TLSEC_Sections.Find(index);
                                if (sec == null)
                                {
                                    sec = new TLSEC_Sections();
                                }
                                else
                                    Add = false;

                             
                            }

                            
                            sec.TLSECSect_Department_FK = selected.TLSECDT_Pk;
                            sec.TLSECSect_Description = row.Cells[2].Value.ToString();
                            

                            if (Add)
                            {
                                context.TLSEC_Sections.Add(sec);
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data saved to database successfully");
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
                        catch (System.Data.Entity.Validation.DbUnexpectedValidationException en)
                        {
                            var exceptionMessages = new StringBuilder();
                            do
                            {
                                exceptionMessages.Append(en.Message);
                               
                            }
                            while (en != null);

                            MessageBox.Show(exceptionMessages.ToString());
                           
                            
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
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded && e.Button.ToString() == "Right")
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        DataGridViewRow cr = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            int RecNo = Convert.ToInt32(cr.Cells[0].Value.ToString());
                            //-----------------------------------------------------
                            //First find the record deatils 
                            //-----------------------------------------------
                            var RecDetails = context.TLSEC_Sections.Find(RecNo);
                            if (RecDetails != null)
                            {
                                try
                                {
                                    context.TLSEC_Sections.Remove(RecDetails);
                                    context.SaveChanges();
                                    MessageBox.Show("Record successfully deleted");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Utilities;

namespace Spinning
{
    public partial class frmCottonMerge : Form
    {
       

        private class ComboBoxItem 
        {
            public int CottonCon_Supplier_FK { get; set; }
            public string CottonCon_No { get; set; }
            public object CottonCon_Pk { get; set; }
            public bool Selectable { get; set; }

           
        }

        DataGridViewTextBoxColumn oTxtA  = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn oCmbA = new DataGridViewComboBoxColumn();
        DataGridViewComboBoxColumn oCmbB = new DataGridViewComboBoxColumn();
        DataGridViewTextBoxColumn oTxtB  = new DataGridViewTextBoxColumn();


        bool formloaded;
        Util core;

        bool Update;

     

        public frmCottonMerge()
        {
            InitializeComponent();

             core = new Util();

            oCmbA.HeaderText = "Cotton Supplier";
            oCmbA.Width = 135;
          //  oCmbA.FlatStyle = FlatStyle.Flat;

            oCmbB.HeaderText = "Cotton Contract";
            oCmbB.Width = 135;
          //  oCmbB.FlatStyle = FlatStyle.Flat;

            oTxtA.HeaderText = "Primary Key";
            oTxtA.ValueType = typeof(Int32);
            oTxtA.Visible = false;

            oTxtB.HeaderText = "Percentage split";
            oTxtB.ValueType = typeof(decimal);
            oTxtB.Visible = true;
            oTxtB.ReadOnly = false;
           

            dataGridView1.AllowUserToAddRows = true;

            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns.Add(oCmbA);
            dataGridView1.Columns.Add(oCmbB);
            dataGridView1.Columns.Add(oTxtB);

            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            dataGridView1.Rows.Clear();

            oCmbB.Items.Clear();
            txtMergeName.Text = string.Empty;

            Update = true;

            using (var context = new TTI2Entities())
            {
                cmboPrevious.DataSource = context.TLSPN_CottonMerge.ToList();
                cmboPrevious.DisplayMember = "TLCTM_Description";
                cmboPrevious.ValueMember = "TLCTM_Pk";
                cmboPrevious.SelectedValue = 0;

                oCmbA.DataSource = context.TLADM_Cotton.OrderBy(x => x.Cotton_Description).ToList();
                oCmbA.DisplayMember = "Cotton_Description";
                oCmbA.ValueMember = "Cotton_Pk";
                
                this.oCmbB.ValueMember = "CottonCon_Pk";
                this.oCmbB.DisplayMember = "CottonCon_No";

                var cc = context.TLADM_CottonContracts.ToList();
                foreach (var row in cc)
                {
                    ComboBoxItem ci = new ComboBoxItem();
                    ci.CottonCon_No = row.CottonCon_No;
                    ci.CottonCon_Pk = row.CottonCon_Pk;
                    ci.CottonCon_Supplier_FK = row.CottonCon_ConSupplier_FK;
                    ci.Selectable = true;
                                               
                    oCmbB.Items.Add(ci);
                }
                            
            }
            formloaded = true;
        }

        private void frmCottonLayDownSelection_Load(object sender, EventArgs e)
        {
            
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                //first do the header records
                if (String.IsNullOrEmpty(txtMergeName.Text))
                {
                    MessageBox.Show("Please complete a merge name");
                    return;
                }

                if (dataGridView1.Rows.Count == 0)
                {
                    MessageBox.Show("Please select a least one contract ");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    TLSPN_CottonMerge cm = new TLSPN_CottonMerge();
                    if (!Update)
                    {
                        var selected = (TLSPN_CottonMerge)cmboPrevious.SelectedItem;
                        if (selected != null)
                        {
                            cm = context.TLSPN_CottonMerge.Find(selected.TLCTM_Pk);
                        }
                    }

                    cm.TLCTM_Description = txtMergeName.Text;
                    cm.TLCTM_Date = DateTime.Now;

                    if (Update)
                    {
                        context.TLSPN_CottonMerge.Add(cm);

                        try
                        {
                            context.SaveChanges();
                            SetUp();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }


                    }
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value == null )
                            continue;
       
                        TLSPN_CottonMergeDetails cmd = new TLSPN_CottonMergeDetails();

                        Update = true;

                        if (row.Cells[0].Value != null)
                            Update = false;

                        if (!Update)
                        {
                            var Pk = (int)row.Cells[0].Value;
                            cmd = context.TLSPN_CottonMergeDetails.Find(Pk);
                        }

                        cmd.TLCTMD_CTM_FK = cm.TLCTM_Pk;
                        cmd.TLCTMD_Supplier_FK = (int)row.Cells[1].Value;
                        cmd.TLCTMD_Contract_FK = (int)row.Cells[2].Value;
                        cmd.TLCTMD_Split = (decimal)row.Cells[3].Value;

                        if (Update)
                        {
                            context.TLSPN_CottonMergeDetails.Add(cmd);
                        }
                    }
                    //--------------------------------------------
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data updated to database successfully");
                        SetUp();
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

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {   
            var oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if(oDgv.CurrentCell.ColumnIndex == 3)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
            else if (combo != null)
            {
                combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var cell = dataGridView1.CurrentCell;
            if (cell.ColumnIndex == 1 && cb.SelectedValue != null)
            {
                var selected = (TLADM_Cotton)cb.SelectedItem;
                foreach (ComboBoxItem itm in oCmbB.Items)
                {
                    if (itm.CottonCon_Supplier_FK == selected.Cotton_Pk)
                    {
                        itm.Selectable = true;
                       
                    }
                    else
                    {
                        itm.Selectable = false;
                        
                    }
                }

                this.oCmbB.Items.Clear();
                // this.dataGridView1.Rows.Clear();

                this.oCmbB.ValueMember = "CottonCon_Pk";
                this.oCmbB.DisplayMember = "CottonCon_No";
                using (var context = new TTI2Entities())
                {
                    var cc = context.TLADM_CottonContracts.Where(x=>x.CottonCon_ConSupplier_FK == selected.Cotton_Pk).ToList();
                    foreach (var row in cc)
                    {
                        ComboBoxItem ci = new ComboBoxItem();
                        ci.CottonCon_No = row.CottonCon_No;
                        ci.CottonCon_Pk = row.CottonCon_Pk;
                        ci.CottonCon_Supplier_FK = row.CottonCon_ConSupplier_FK;
                        ci.Selectable = true;

                        oCmbB.Items.Add(ci);
                    }
                }
            }
            else
            {
                if (cb.SelectedItem != null && cb.SelectedItem is ComboBoxItem && ((ComboBoxItem)cb.SelectedItem).Selectable == false)
                {
                    MessageBox.Show("Cotton Supplier / Contract Number Invalid");
                    // deselect item
                    cb.SelectedIndex = -1;
                }
            }
        }

       

        private void cmboPrevious_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                Update = false;

                var selected = (TLSPN_CottonMerge)oCmbo.SelectedItem;

                txtMergeName.Text = selected.TLCTM_Description;
                dataGridView1.Rows.Clear();

                using (var context = new TTI2Entities())
                {
                    var details = context.TLSPN_CottonMergeDetails.Where(x => x.TLCTMD_CTM_FK == selected.TLCTM_Pk).ToList();
                    foreach (var row in details)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = row.TLCTMD_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = row.TLCTMD_Supplier_FK;
                        dataGridView1.Rows[index].Cells[2].Value = row.TLCTMD_Contract_FK;
                        dataGridView1.Rows[index].Cells[3].Value = Math.Round(row.TLCTMD_Split,2);
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
                            var locRec = context.TLSPN_CottonMergeDetails.Find(RecNo);
                            if (locRec != null)
                            {
                                try
                                {
                                    context.TLSPN_CottonMergeDetails.Remove(locRec);
                                    context.SaveChanges();
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Administration
{
    public partial class frmFabricDyingStandards : Form
    {
        bool FormLoaded;
        Util core;
        BindingSource BindingSrc;
        DataTable DataT;
        DataColumn column;

        public frmFabricDyingStandards()
        {
          
            core = new Util();

            DataGridViewComboBoxColumn oCmboA = new DataGridViewComboBoxColumn();  // Qualities 

            BindingSrc = new BindingSource();

            InitializeComponent();
                        
            this.dataGridView1.Visible = true;
            
            using (var context = new TTI2Entities())
            {
                oCmboA = new DataGridViewComboBoxColumn();
                oCmboA.HeaderText = "Quality Description";
                oCmboA.DataSource = context.TLADM_Griege.Where(x => !(bool)x.TLGriege_Discontinued).ToList();
                oCmboA.ValueMember = "TLGreige_Id";
                oCmboA.DisplayMember = "TLGreige_Description";
                oCmboA.DataPropertyName = "Greige_Pk";
                dataGridView1.Columns.Add(oCmboA);
            }
                       
            dataGridView1.AllowUserToOrderColumns = false;
         
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
        }

        private void frmFabricDyingStandards_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
         
            
            using (var context = new TTI2Entities())
            {
                //============================================================
                //---------Define the datatable 
                //=================================================================
                DataT = new System.Data.DataTable();

                //------------------------------------------------------
                // Create column 0. // This is Index Position of the measurement in the TLADM_QADyeProcessFields Table
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "DyeStandard_Pk";
                column.DefaultValue = 0;
                DataT.Columns.Add(column);
               //  DataT.PrimaryKey = new DataColumn[] { DataT.Columns[0] };

                //------------------------------------------------------
                // Create column 1. // This is Index Position of the quality in the TLADM_Greige Table
                //----------------------------------------------
                column = new DataColumn();
                column.DataType = typeof(Int32);
                column.ColumnName = "Greige_Pk";
                column.Caption = "Quality";
                DataT.Columns.Add(column);

                var ProcessFieldGroups = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK > 4).GroupBy(x => x.TLQADPF_Process_FK).ToList();
                foreach (var PFieldGroup in ProcessFieldGroups)
                {
                    foreach (var Item in PFieldGroup)
                    {
                        column = new DataColumn();
                        column.DataType = typeof(Int32);
                        column.ExtendedProperties.Add("PrimaryKey", Item.TLQADPF_Pk);
                        column.DefaultValue = 0;
                        column.ColumnName = Item.TLQADPF_Pk.ToString();
                        column.Caption = context.TLADM_QADyeProcess.Find(Item.TLQADPF_Process_FK).QADYEP_Description + " " + Item.TLQADPF_Description;

                        DataT.Columns.Add(column);
                    }

                }

                int idx = 0;

                this.BindingSrc.DataSource = DataT;
                this.dataGridView1.DataSource = BindingSrc;
                this.dataGridView1.Columns[0].Visible = false;

                foreach (DataColumn col in DataT.Columns)
                {
                    if (++idx > 2)
                        dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
                }


                var CurrentGroups = context.TLDYE_DyeingStandards.GroupBy(x => x.DyeStan_Quality_FK).ToList();
                foreach (var CGroup in CurrentGroups)
                {
                    DataRow newRow = DataT.NewRow();
                    bool First = true;
                    foreach (var Quality in CGroup)
                    {
                        if (First)
                        {
                            First = !First;
                            newRow[0] = Quality.DyeStan_Pk;
                            newRow[1] = Quality.DyeStan_Quality_FK;
                        }

                        var ColIndex = DataT.Columns.IndexOf(Quality.DyeStan_QAProccessField_FK.ToString());
                        if (ColIndex != 0)
                        {
                            newRow[ColIndex] = Quality.DyeStan_Value;
                        }
                    }
                    DataT.Rows.Add(newRow);
                }
            }

            FormLoaded = true;

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var Cell = oDgv.CurrentCell;

            if (oDgv.Focused && Cell is DataGridViewTextBoxCell && Cell.ColumnIndex > 1)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && FormLoaded && e.Button.ToString() == "Right")
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        DataGridViewRow cr = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            //------------------------------------------------------------------------------------
                            // First we must ensure that all subordinate records are deleted first
                            //---------------------------------------------------------------------------
                            try
                            {
                                int QualityFk = Int32.Parse(cr.Cells[1].Value.ToString());
                                context.TLDYE_DyeingStandards.RemoveRange(context.TLDYE_DyeingStandards.Where(x => x.DyeStan_Quality_FK == QualityFk));
                                context.SaveChanges();
                                
                                MessageBox.Show("Actioned as per your request");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
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

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && FormLoaded)
            {
               if (!dataGridView1.Visible)
                    dataGridView1.Visible = true;

                var SelectedCustomer = (TLADM_CustomerFile)oCmbo.SelectedItem;
                var CustPk = SelectedCustomer.Cust_Pk;

                using (var context = new TTI2Entities())
                {
                      //============================================================
                     //---------Define the datatable 
                     //=================================================================
                     DataT = new System.Data.DataTable();
                    
                     //------------------------------------------------------
                     // Create column 0. // This is Index Position of the measurement in the TLADM_QADyeProcessFields Table
                     //----------------------------------------------
                     column = new DataColumn();
                     column.DataType = typeof(Int32);
                     column.ColumnName = "DyeStandard_Pk";
                     column.DefaultValue = 0;
                     DataT.Columns.Add(column);
                     DataT.PrimaryKey = new DataColumn[] { DataT.Columns[0] };

                     //------------------------------------------------------
                     // Create column 1. // This is Index Position of the quality in the TLADM_Greige Table
                     //----------------------------------------------
                     column = new DataColumn();
                     column.DataType = typeof(Int32);
                     column.ColumnName = "Greige_Pk";
                     column.Caption = "Quality";
                     DataT.Columns.Add(column);

                     var ProcessFieldGroups = context.TLADM_QADyeProcessFields.Where(x => x.TLQADPF_Process_FK > 4).GroupBy(x=>x.TLQADPF_Process_FK).ToList();
                     foreach (var PFieldGroup in ProcessFieldGroups)
                     {
                        foreach (var Item in PFieldGroup)
                        {
                            column = new DataColumn();
                            column.DataType = typeof(Int32);
                            column.ExtendedProperties.Add("PrimaryKey", Item.TLQADPF_Pk);
                            column.DefaultValue = 0;
                            column.ColumnName = Item.TLQADPF_Pk.ToString();
                            column.Caption = context.TLADM_QADyeProcess.Find(Item.TLQADPF_Process_FK).QADYEP_Description + " " +Item.TLQADPF_Description;

                            DataT.Columns.Add(column);
                        }
                    
                     }

                      int idx = 0;

                      this.BindingSrc.DataSource = DataT;
                      this.dataGridView1.DataSource = BindingSrc;
                      this.dataGridView1.Columns[0].Visible = false;

                      foreach (DataColumn col in DataT.Columns)
                      {
                           if (++idx > 2)
                              dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
                      }

                   
                    var CurrentGroups = context.TLDYE_DyeingStandards.GroupBy(x => x.DyeStan_Quality_FK).ToList();
                    foreach(var CGroup in CurrentGroups)
                    {
                        DataRow newRow = DataT.NewRow();
                        bool First = true;
                        foreach(var Quality in CGroup)
                        {
                            if(First)
                            {
                                First = !First;
                                newRow[0] = Quality.DyeStan_Pk;
                                newRow[1] = Quality.DyeStan_Quality_FK;
                            }

                            var ColIndex = DataT.Columns.IndexOf(Quality.DyeStan_QAProccessField_FK.ToString());
                            if (ColIndex != 0)
                            {
                                newRow[ColIndex] = Quality.DyeStan_Value;
                            }
                        }
                        DataT.Rows.Add(newRow);
                    }
                     

                }
            }
            */
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && FormLoaded)
            {
                DataT.Rows.Clear();

                using ( var context = new TTI2Entities())
                {

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TLDYE_DyeingStandards DyeStandards = null;

            Button oBtn = sender as Button;
            bool Add; 
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataRow Row in DataT.Rows)
                    {
                        foreach (DataColumn col in DataT.Columns)
                        {
                            if(col.ExtendedProperties.Count == 0)
                            {
                                continue;
                            }

                            var ProcessPk = Int32.Parse(col.ExtendedProperties["PrimaryKey"].ToString());
                            if (ProcessPk != 0)
                            {
                                Add = true;
                                DyeStandards = new TLDYE_DyeingStandards();
                                if (Row.Field<Int32>(0) == 0)
                                {
                                    DyeStandards.DyeStan_QAProccessField_FK = ProcessPk;
                                    DyeStandards.DyeStan_Quality_FK = Row.Field<Int32>(1);
                                }
                                else
                                {
                                    var QualPk = Row.Field<Int32>(1);
                                    DyeStandards = context.TLDYE_DyeingStandards.Where(x=>x.DyeStan_Quality_FK == QualPk && x.DyeStan_QAProccessField_FK == ProcessPk).FirstOrDefault();
                                    if (DyeStandards == null)
                                    {
                                        DyeStandards = new TLDYE_DyeingStandards();
                                        DyeStandards.DyeStan_QAProccessField_FK = ProcessPk;
                                        DyeStandards.DyeStan_Quality_FK = Row.Field<Int32>(1);
                                    }
                                    else
                                    {
                                        Add = false;
                                    }
                                }
                                
                                var ColIndex = DataT.Columns.IndexOf(col.ColumnName);
                                if(ColIndex != 0)
                                {
                                    DyeStandards.DyeStan_Value = Row.Field<Int32>(ColIndex);
                                }
                               
                                    
                                if(Add)
                                {
                                    context.TLDYE_DyeingStandards.Add(DyeStandards);
                                }
                            }
                        }

                    }


                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                        frmFabricDyingStandards_Load(this, null);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                frmAdminViewRep vRep = new frmAdminViewRep(13);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
        }
    }
}

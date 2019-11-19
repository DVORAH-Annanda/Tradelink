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
    public partial class frmBFADefinition : Form
    {
        bool formloaded;

        Util core;

        DataGridViewComboBoxColumn oCmboA;
    
        System.Data.DataTable DataT;
        System.Data.DataTable dtConnectorSource;


        public frmBFADefinition()
        {
            InitializeComponent();
          
            dataGridView1.AllowUserToOrderColumns = false;

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
        }

        private void frmBFADefinition_Load(object sender, EventArgs e)
        {
            formloaded = false;

            dataGridView1.Visible = false;

            using (var context = new TTI2Entities())
            {
                cmboCustomer.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmboCustomer.DisplayMember = "Cust_Description";
                cmboCustomer.ValueMember = "Cust_Pk";
                cmboCustomer.SelectedValue = -1;
 
                dtConnectorSource = new DataTable();
                dtConnectorSource.Columns.Add("CMTBFA_Pk", typeof(int));
                dtConnectorSource.Columns.Add("CMTMP_Pk", typeof(int));
                dtConnectorSource.Columns.Add("CMTMP_Description", typeof(String));

                oCmboA = new DataGridViewComboBoxColumn();
                oCmboA.HeaderText = "Measurement Description";
                oCmboA.DataSource = context.TLADM_CMTMeasurementPoints.OrderBy(x => x.CMTMP_DisplayOrder).ToList();
                oCmboA.ValueMember = "CMTMP_Pk";
                oCmboA.DisplayMember = "CMTMP_Description";
                oCmboA.DataPropertyName = "CMTMP_Pk";
                
                dataGridView1.Columns.Add(oCmboA);
            }

            core = new Util();
            
       

 
            formloaded = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var Cell = oDgv.CurrentCell;

            if (oDgv.Focused && Cell is DataGridViewTextBoxCell && Cell.ColumnIndex > 1)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
        }
           

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && formloaded)
            {
               
 
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
                        var Style = (TLADM_Styles)cmboStyles.SelectedItem;
                        if(Style == null)
                        {
                            MessageBox.Show("Please select a Style to work with from the facility provided");
                            return;
                        }

                        DataGridViewRow cr = oDgv.CurrentRow;
                        using (var context = new TTI2Entities())
                        {
                            //------------------------------------------------------------------------------------
                            // First we must ensure that all subordinate records are deleted first
                            //---------------------------------------------------------------------------
                           try
                           {
                               int MeasureFk = Convert.ToInt32(cr.Cells[0].Value.ToString());
                               context.TLCMT_AuditMeasureRecorded.RemoveRange(context.TLCMT_AuditMeasureRecorded.Where(x => x.TLBFAR_AuditMeasure_FK == MeasureFk));
                               MeasureFk = Convert.ToInt32(cr.Cells[1].Value.ToString());
                               context.TLCMT_AuditMeasurements.RemoveRange(context.TLCMT_AuditMeasurements.Where(x => x.CMTBFA_MeasureP_FK == MeasureFk && x.CMTBFA_Style_FK == Style.Sty_Id));
                           
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

        private void cmboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_CustomerFile)oCmbo.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        cmboStyles.DataSource = context.TLADM_Styles.Where(x => x.Sty_Label_FK == selected.Cust_Pk).OrderBy(x => x.Sty_Description).ToList();
                        cmboStyles.ValueMember = "Sty_Id";
                        cmboStyles.DisplayMember = "Sty_Description";
                        cmboStyles.SelectedValue = -1;
                        formloaded = true;
                    }
                }
            }
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            DataRow Row = null;

            if (oCmbo != null && formloaded)
            {
                using ( var context = new TTI2Entities())
                {
                    var SelectedCustomer = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                    if (SelectedCustomer == null)
                    {
                        MessageBox.Show("Please select a customer from the drop down box", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!dataGridView1.Visible)
                        dataGridView1.Visible = true;

                    var SelectedStyle = (TLADM_Styles)oCmbo.SelectedItem;
                    if (SelectedStyle == null)
                    {
                        MessageBox.Show("Please select a style from the drop down box", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //============================================================
                    //---------Define the datatable 
                    //=================================================================
                    DataT = new System.Data.DataTable();
                    DataColumn column;

                    //------------------------------------------------------
                    // Create column 1. // This is Index Position of the measurement in the TLCMT_AuditMeasurent Recorded Table
                    //----------------------------------------------
                    column = new DataColumn();
                    column.DataType = typeof(Int32); 
                    column.ColumnName = "CMTBFA_Pk";
                    DataT.Columns.Add(column);
                   
                    //-----------------------------------------------------------
                    // Create column 2. // This is the Index position of the measurement 
                    //----------------------------------------------
                    column = new DataColumn();
                    column.DataType = typeof(int);
                    column.ColumnName = "CMTMP_Pk";
                    column.Caption = "Measurement Description";

                    DataT.Columns.Add(column);

                    var Measurements = (from AuditM in context.TLCMT_AuditMeasurements
                               join Measure in context.TLADM_CMTMeasurementPoints on AuditM.CMTBFA_MeasureP_FK equals Measure.CMTMP_Pk
                               where AuditM.CMTBFA_Customer_FK == SelectedCustomer.Cust_Pk && AuditM.CMTBFA_Style_FK == SelectedStyle.Sty_Id
                               orderby Measure.CMTMP_DisplayOrder
                               select AuditM).ToList();

                    //we need to find out the sizes associated with this style
                    //============================================================
                    var Sizes = core.ExtrapNumber(SelectedStyle.Sty_Sizes_PN, context.TLADM_Sizes.Count());

                    var SizeSorted = (from Si in Sizes
                               join Details in context.TLADM_Sizes
                               on Si.ToString() equals Details.SI_PowerN.ToString()
                               orderby Details.SI_DisplayOrder
                               where !Details.SI_Discontinued 
                               select Si).ToList();

                    foreach (var Size in SizeSorted)
                    {
                        var SizeDetail = context.TLADM_Sizes.FirstOrDefault(x=>x.SI_PowerN ==Size);
                        if (SizeDetail != null)
                        {
                            if (SizeDetail.SI_Discontinued)
                                continue;

                            column = new DataColumn();
                            column.DataType = typeof(Decimal);
                            column.DefaultValue = 0.0;
                            column.ColumnName = SizeDetail.SI_id.ToString();
                            column.Caption = SizeDetail.SI_Description;
 
                            DataT.Columns.Add(column);
                        }
                    }

                    var GroupedM = Measurements.GroupBy(x => x.CMTBFA_MeasureP_FK);
                    foreach (var Group in GroupedM)
                    {
                        bool First = true;
                        // int Index = 0;
                        Row = DataT.NewRow();

                        foreach (var Sze in Group)
                        {
                            
                            if (First)
                            {
                                First = !First;
                                Row[0] = Group.FirstOrDefault().CMTBFA_Pk;
                                Row[1] = Group.FirstOrDefault().CMTBFA_MeasureP_FK;
                            }

                            var Ind = DataT.Columns.IndexOf(Sze.CMTBFA_Size_FK.ToString());
                            if (Ind > 0)
                            {
                                Row[Ind] = Sze.CMTBFA_Measurement;
                              
                            }
                        }
                        DataT.Rows.Add(Row);
                    }

                    dataGridView1.DataSource = DataT;
                    dataGridView1.Columns[0].Visible = false;
                    int idx = 0;

                    foreach (DataColumn col in  DataT.Columns)
                    {
                        if (++idx > 2)
                            dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
                    }
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            var DisplayOrder = 0;

            if (oBtn != null && formloaded)
            {
                var CustomerSelected = (TLADM_CustomerFile)cmboCustomer.SelectedItem;
                if (CustomerSelected == null)
                {
                    MessageBox.Show("Please select a customer from the drop down box");
                    return;
                }

                var StyleSelected = (TLADM_Styles)cmboStyles.SelectedItem;
                if (StyleSelected == null)
                {
                    MessageBox.Show("Please select a style from the drop down list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (Row.Cells[1].Value == null)
                            continue;

                        DisplayOrder += 1;

                        if (Row.Cells[0].Value == null)
                        {
                            //--------------------------------------------------------------
                            // takes care of any new records that might be entered 
                            //---------------------------------------------------------------
                            for (int i = 2; i < dataGridView1.Columns.Count; i++)
                            {
                                TLCMT_AuditMeasurements audm = new TLCMT_AuditMeasurements();
                                audm.CMTBFA_Customer_FK = CustomerSelected.Cust_Pk;
                                audm.CMTBFA_MeasureP_FK = (int)Row.Cells[1].Value;
                                audm.CMTBFA_Size_FK = Convert.ToInt32(dataGridView1.Columns[i].Name);
                                audm.CMTBFA_Style_FK = StyleSelected.Sty_Id;
                                
                                if (Row.Cells[i].Value != null)
                                    audm.CMTBFA_Measurement = (Decimal)Row.Cells[i].Value;
                                else
                                    audm.CMTBFA_Measurement = 0.0M;

                                context.TLCMT_AuditMeasurements.Add(audm);
                            }
               
                        }
                        else
                        {
                            // takes care of any existing  records  
                            //---------------------------------------------------------------

                            for (int i = 2; i < dataGridView1.Columns.Count; i++)
                            {
                                var Name = dataGridView1.Columns[i].Name;
                                var Size = Convert.ToInt32(Name);
                                var Measure = (int)Row.Cells[1].Value;

                                var Audm = context.TLCMT_AuditMeasurements.Where(x => x.CMTBFA_Style_FK == StyleSelected.Sty_Id && x.CMTBFA_Size_FK == Size && x.CMTBFA_MeasureP_FK == Measure).FirstOrDefault();
                                if (Audm != null)
                                {
                                    //--------------------------------------------
                                    // Here we have an existing record just need to check if the amts are different
                                    //-----------------------------------------------------------------------------
                                    if (Row.Cells[i].Value != null && Measure != Audm.CMTBFA_Measurement)
                                        Audm.CMTBFA_Measurement = (Decimal)Row.Cells[i].Value;
                                    else
                                        Audm.CMTBFA_Measurement = 0;

                                    
                                }
                                else
                                {
                                    //------------------------------------------------------
                                    // the User may have decided to change a Description
                                    // and the new record might not be in the database
                                    // so lets add to the data table 
                                    //--------------------------------------------------------
                                    TLCMT_AuditMeasurements audm = new TLCMT_AuditMeasurements();
                                    
                                    audm.CMTBFA_Customer_FK = CustomerSelected.Cust_Pk;
                                    audm.CMTBFA_MeasureP_FK = (int)Row.Cells[1].Value;
                                    audm.CMTBFA_Size_FK = Convert.ToInt32(dataGridView1.Columns[i].Name);
                                    audm.CMTBFA_Style_FK = StyleSelected.Sty_Id;
                                    if (Row.Cells[i].Value != null)
                                       audm.CMTBFA_Measurement = (Decimal)Row.Cells[i].Value;
                                    else
                                        audm.CMTBFA_Measurement = 0.0M;

                                    context.TLCMT_AuditMeasurements.Add(audm);
                               }
                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");
                        dataGridView1.DataSource = null;
                        formloaded = false;
                        cmboStyles.DataSource = null;
                        cmboStyles.Items.Clear();
                        formloaded = true;

                        dataGridView1.Columns.Clear(); 
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
                    finally
                    {
                        this.frmBFADefinition_Load(this, null);
                    }
                }
            }
        }
    }
}

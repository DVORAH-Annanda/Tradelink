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
    public partial class frmCloseDyeBatches : Form
    {
        Util core;
        DataTable ExistingDyeBatches;
        bool FormLoaded;

        DataColumn column;
        BindingSource BindingSrc;


        public frmCloseDyeBatches()
        {
            InitializeComponent();

            BindingSrc = new BindingSource();
            ExistingDyeBatches = new DataTable();

            //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "DyeBatch_Pk";
            column.Caption = "Dye Batch Key";
            column.DefaultValue = 0;
            ExistingDyeBatches.Columns.Add(column);

            //=================================================
            // Col 1 
            //=====================================================
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "SelectRecord_Pk";
            column.Caption = "Select Dye Batch";
            column.DefaultValue = false;
            ExistingDyeBatches.Columns.Add(column);

            //=====================================================
            // Col 2 
            //======================================================
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "DyeBatch_Number";
            column.Caption = "DyeBatch Number";
            column.DefaultValue = string.Empty;
            ExistingDyeBatches.Columns.Add(column);

            //======================================================
            // Col 3
            //================================================================
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Quality_Pk";
            column.Caption = "Quality";
            column.DefaultValue = string.Empty;
            ExistingDyeBatches.Columns.Add(column);

            //======================================================================
            // Col 4 
            //================================================================================
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Style_Pk";
            column.Caption = "Style";
            column.DefaultValue = string.Empty;
            ExistingDyeBatches.Columns.Add(column);

            //===========================================================
            // Col 5 
            //===========================================================
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Colour_Pk";
            column.Caption = "Colour";
            column.DefaultValue = string.Empty;
            ExistingDyeBatches.Columns.Add(column);
                      

            BindingSrc.DataSource = ExistingDyeBatches;
            dataGridView1.DataSource = BindingSrc;


            var idx = -1;
            foreach (DataColumn col in ExistingDyeBatches.Columns)
            {
                if (++idx == 0)
                    dataGridView1.Columns[idx].Visible = false;
                else
                {
                    dataGridView1.Columns[idx].HeaderText = col.Caption;
                }
            }

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToDeleteRows = false;


        }

        private void frmCloseDyeBatches_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            using (var context = new TTI2Entities())
            {
                var DBatches = context.TLDYE_DyeBatch.Where(x => !x.DYEB_Closed).ToList();
                
                foreach(var DyeBatch in DBatches)
                {
                    DataRow Row = ExistingDyeBatches.NewRow();
                    Row[0] = DyeBatch.DYEB_Pk;
                    Row[1] = false;
                    Row[2] = DyeBatch.DYEB_BatchNo;
                    Row[3] = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK).TLGreige_Description;
                    var DyeOrder = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                    if(DyeOrder != null)
                    {
                        Row[4] = context.TLADM_Styles.Find(DyeOrder.TLDYO_Style_FK).Sty_Description;
                        Row[5] = context.TLADM_Colours.Find(DyeOrder.TLDYO_Colour_FK).Col_Display;
                    }


                    ExistingDyeBatches.Rows.Add(Row);
                }
            }

            FormLoaded = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataRow Row in ExistingDyeBatches.Rows)
                    {
                        if (!Row.Field<bool>(1))
                            continue;

                        var Pk = Row.Field<int>(0);

                        var DB = context.TLDYE_DyeBatch.Find(Pk);
                        if(DB != null)
                        {
                            DB.DYEB_Closed = true;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                        {
                            MessageBox.Show("Data successfully stored to the database");
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        this.frmCloseDyeBatches_Load(this, null);
                    }
                }
            }
        }
    }
}

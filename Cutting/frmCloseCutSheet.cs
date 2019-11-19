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
    public partial class frmCloseCutSheet : Form
    {
        bool FormLoaded;
       
        DataTable dt;
 
        public frmCloseCutSheet()
        {
            InitializeComponent();

            dt = new DataTable();

            DataColumn Column;

            Column = new DataColumn();
            Column.DataType = typeof(Int32);
            Column.ColumnName = "CS_Pk";
            Column.Caption = "Cut Sheet Primary";
            Column.ReadOnly = true;
            Column.DefaultValue = 0;
            dt.Columns.Add(Column);

            Column = new DataColumn();
            Column.DataType = typeof(bool);
            Column.ColumnName = "CS_Select";
            Column.Caption = "Select";
            Column.DefaultValue = false;
            dt.Columns.Add(Column);

            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "CS_No";
            Column.Caption = "Cut Sheet Number";
            Column.ReadOnly = true;
            Column.DefaultValue = string.Empty;
            dt.Columns.Add(Column);

            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "CS_Style";
            Column.Caption = "Style";
            Column.DefaultValue = string.Empty;
            Column.ReadOnly = true;
            dt.Columns.Add(Column);

            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "CS_Colour";
            Column.Caption = "Colour";
            Column.DefaultValue = string.Empty;
            Column.ReadOnly = true;
            
            dt.Columns.Add(Column);
            
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            dataGridView1.DataSource = dt;

            var idx = -1;
            foreach (DataColumn col in dt.Columns)
            {
                if (++idx == 0)
                    dataGridView1.Columns[idx].Visible = false;
                else
                {
                    dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
                    if(idx == 1)
                        dataGridView1.Columns[col.ColumnName].Width = 75;
                    else
                        dataGridView1.Columns[col.ColumnName].Width = 125;
                }
            }
        }

        private void frmCloseCutSheet_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            rbCloseCutSheet.Checked = true;
            ExtractData(true);
            FormLoaded = true;
        }

        private void rbCloseCutSheet_CheckedChanged(object sender, EventArgs e)
        {
            // Close open cut sheet 
            //-----------------------------------------------
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                ExtractData(true);
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //Reopen Close cut sheet 
            //---------------------------------------------------------
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && FormLoaded)
            {
                if (oRad.Checked)
                {
                    ExtractData(false);
                }
            }
        }

        private void ExtractData(bool Flag)
        {
            IList<TLCUT_CutSheet> CutSheets = null;

            using (var context = new TTI2Entities())
            {
                if (Flag)
                {
                    CutSheets = (from CS in context.TLCUT_CutSheet
                                 where !CS.TLCutSH_Closed && !CS.TLCUTSH_OnHold && !CS.TLCutSH_WIPComplete 
                                 orderby CS.TLCutSH_No
                                 select CS).ToList();
                }
                else
                {
                    CutSheets = (from CS in context.TLCUT_CutSheet
                                 where CS.TLCutSH_Closed && !CS.TLCutSH_WIPComplete 
                                 orderby CS.TLCutSH_No 
                                 select CS).ToList();
                }

                dt.Rows.Clear();

                foreach (var CutSheet in CutSheets)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = CutSheet.TLCutSH_Pk;
                    dr[1] = false;
                    dr[2] = CutSheet.TLCutSH_No;
                    dr[3] = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                    dr[4] = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;

                    dt.Rows.Add(dr);
                }
           }
        }
       

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                using ( var context = new TTI2Entities())
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if(!dr.Field<bool>(1))
                           continue;
                        
                        var CS = context.TLCUT_CutSheet.Find(dr.Field<Int32>(0));
                        if (CS != null)
                        {
                            if (rbCloseCutSheet.Checked)
                            {
                                CS.TLCutSH_Closed = true;
                                CS.TLCutSH_ClosedDate = dtpCloseDate.Value;
                            }
                            else
                            {
                                CS.TLCutSH_ClosedDate = null;
                                CS.TLCutSH_Closed = false;
                            }
                       }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        frmCloseCutSheet_Load(this, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
        }
    }
}

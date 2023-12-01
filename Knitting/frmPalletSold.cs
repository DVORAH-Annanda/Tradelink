using Spinning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Knitting
{
    public partial class frmPalletSold : Form
    {
        bool FormLoaded;
        Util core;
        TTI2Entities _context;
        BindingSource BindingSrc;
        DataColumn column;
        DataTable DataT;
        public frmPalletSold()
        {
            InitializeComponent();
            core = new Util();
            _context = new TTI2Entities();

            DataT = new DataTable();
            DataGridView oDgv = new DataGridView();
            BindingSrc = new BindingSource();

            DataGridViewTextBoxColumn oTxtA;   // Whse
            DataGridViewTextBoxColumn oTxtB;   // Box Number
            DataGridViewTextBoxColumn oTxtC;   // Box Number
            
            DataGridViewCheckBoxColumn oChkA; 

            //------------------------------------------------------
            // Create column 0. // This is the style index 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.DefaultValue = 0;
            column.ColumnName = "Col0";
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 1. // This is the Selection 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "Col1";
            column.DefaultValue = false;    ;
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 3. // This is the PalletNumber 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Col2";
            column.DefaultValue = string.Empty;
            DataT.Columns.Add(column);

            //------------------------------------------------------
            // Create column 4. // This is the PallWePalletNumber 
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "Col3";
            column.DefaultValue = 0.00M;
            DataT.Columns.Add(column);

            oTxtA = new DataGridViewTextBoxColumn();   // 4 Number of items 
            oTxtA.HeaderText = "Number Of";
            oTxtA.ValueType = typeof(int);
            oTxtA.DataPropertyName = DataT.Columns[0].ColumnName;
            oTxtA.Visible = false;
            oTxtA.Width = 100;
            dataGridView1.Columns.Add(oTxtA);
            dataGridView1.Columns[0].DisplayIndex = 0;

            oChkA = new DataGridViewCheckBoxColumn();   // 4 Number of items 
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            oChkA.DataPropertyName = DataT.Columns[1].ColumnName;
            oChkA.Visible = true;
            oChkA.Width = 100;
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns[1].DisplayIndex = 1;

            oTxtB = new DataGridViewTextBoxColumn();   // 4 Number of items 
            oTxtB.HeaderText = "Pallet No";
            oTxtB.ValueType = typeof(string);
            oTxtB.DataPropertyName = DataT.Columns[2].ColumnName;
            oTxtB.Visible = true;;
            oTxtB.Width = 100;
            oTxtB.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtB);
            dataGridView1.Columns[2].DisplayIndex = 2;

            oTxtC = new DataGridViewTextBoxColumn();   // 4 Number of items 
            oTxtC.HeaderText = "Pallet Weightf";
            oTxtC.ValueType = typeof(Decimal);
            oTxtC.DataPropertyName = DataT.Columns[3].ColumnName;
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;
            oTxtC.Width = 100;
            dataGridView1.Columns.Add(oTxtC);
            dataGridView1.Columns[3].DisplayIndex = 3;

            BindingSrc.DataSource = DataT;
            dataGridView1.DataSource = BindingSrc;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void frmPalletSold_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            
            oCmboA.DataSource = _context.TLSPN_YarnOrder.ToList();
            oCmboA.ValueMember = "YarnO_Pk";
            oCmboA.DisplayMember = "YarnO_OrderNumber";
            oCmboA.SelectedIndex = -1;

            FormLoaded = true; 

        }

        private void frmPalletSold_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }

        private void oCmboA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(FormLoaded)
            {
                var oCmboA = sender as ComboBox;
                if(oCmboA!= null)
                {
                    var Item = (TLSPN_YarnOrder)oCmboA.SelectedItem;

                    if(Item !=  null)
                    {
                        DataT.Rows.Clear();
                        var Pallets = _context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_YarnOrder_FK == Item.YarnO_Pk && !x.TLKNIOP_PalletAllocated).ToList();
                        if(Pallets.Count == 0)
                        {
                            using (DialogCenteringService svces = new DialogCenteringService(this))
                            {
                                {
                                    MessageBox.Show("There are no pallets to select");
                                    return;
                                }
                                                                      }
                        }
                        foreach(var Pallet in Pallets)
                        {
                            DataRow dr = DataT.NewRow();
                            dr[0] = Pallet.TLKNIOP_Pk;
                            dr[1] = false;
                            dr[2] = Pallet.TLKNIOP_PalletNo.ToString();
                            dr[3] = Pallet.TLKNIOP_NettWeight;
                            DataT.Rows.Add(dr);
                        }

                    }
                }
            }
        }

        private void btnSAve_Click(object sender, EventArgs e)
        {
            if (FormLoaded)
            {
                foreach (DataRow dr in DataT.Rows)
                {
                    if (!(bool)dr.Field<bool>(1))
                    {
                        continue;
                    }

                    var IndexKey = dr.Field<int>(0);

                    var YOP = _context.TLKNI_YarnOrderPallets.Find(IndexKey);
                    if (YOP != null)
                    {
                        //put code here
                        YOP.TLKNIOP_PalletAllocated = true;
                        
                       
                    }

                }

                using (DialogCenteringService svces = new DialogCenteringService(this))
                {
                    try
                    {
                        _context.SaveChanges();
                        MessageBox.Show("Data successfully updated");
                   }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}

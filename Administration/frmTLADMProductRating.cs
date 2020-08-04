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

namespace TTI2_WF
{
    public partial class frmTLADMProductRating : Form
    {
        /*
         * Program notes DJL 12th August, 2015 
         * The field Pr_Marker_Length in Table TLADM_ProductRating contains either the value of the length of the marker
         * or the weight of the fabric according whether the record is for a body or trim designated as a non rated item 
         */
        bool formloaded;
        int labelIndex = 0;
        int styleindex = 0;


        DataTable ProductRating;
        DataColumn column; 

        int OriginalWidth = 0;
        Util core;

        DataGridViewTextBoxColumn selecta;     // 0 
        DataGridViewButtonColumn  selectb;     // 1
        DataGridViewCheckBoxColumn oChkA;      // 2  
        DataGridViewTextBoxColumn selectc;     // 3
        DataGridViewTextBoxColumn selectd;     // 4 
        DataGridViewTextBoxColumn selecte;     // 5
        DataGridViewButtonColumn  selectf;     // 6
        DataGridViewTextBoxColumn selectg;     // 7
        DataGridViewTextBoxColumn selecth;     // 8
        DataGridViewTextBoxColumn selectj;     // 9
        DataGridViewTextBoxColumn selectk;     // 10
        DataGridViewTextBoxColumn selectl;     // 11

        IList<TLADM_Sizes> Listsizes = null;
        IList<TLADM_Trims> TrimSizes = null;

        BindingSource BindingSrc;
       
        public frmTLADMProductRating(int CoNum)
        {
            InitializeComponent();
            core = new Util();

            ProductRating = new DataTable();
            BindingSrc = new BindingSource();

            dataGridView1.AutoGenerateColumns = false;

            //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "ProdRating_Pk";
            column.Caption = "Product Rating Key";
            column.DefaultValue = 0;
            ProductRating.Columns.Add(column);
            ProductRating.PrimaryKey = new DataColumn[] { ProductRating.Columns[0] };


            //----------------------------------------------
            // Col1 
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Description_Pk";
            column.Caption = "Description";
            column.DefaultValue = string.Empty;
            ProductRating.Columns.Add(column); 

            //--------------------------------------------------------
            // Col 2
            //----------------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Closed_Pk";
            column.Caption = "Closed";
            column.DefaultValue = false;
            ProductRating.Columns.Add(column);

            //-----------------------
            // Col 3
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "FabWidth_Pk";
            column.Caption = "Fabric Width";
            column.DefaultValue = string.Empty;
            ProductRating.Columns.Add(column);

            //-----------------------
            // Col 4
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Ratio_Pk";
            column.Caption = "Product Ratio";
            column.DefaultValue = 0.0M;
            ProductRating.Columns.Add(column);

            //------------------------------
            // Col 5
            //-----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "MarkerLength_Pk";
            column.Caption = "Marker Length";
            column.DefaultValue = 0.0M;
            ProductRating.Columns.Add(column);

            //-----------------------------------------------------
            // col 6
            //-------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(decimal);
            column.ColumnName = "Rating_Pk";
            column.Caption = "Rating";
            column.DefaultValue = 0.0M;
            ProductRating.Columns.Add(column);

            //-----------------------------------------------------
            // col 7
            //-------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Ratio_Detail_Pk";
            column.Caption = "Ratio Detail";
            column.DefaultValue = String.Empty;
            ProductRating.Columns.Add(column);
            
            //-----------------------------------------------------
            // col 8
            //-------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Multi_Marker";
            column.Caption = "MultiMarker";
            column.DefaultValue =  false;
            ProductRating.Columns.Add(column);

            //-----------------------------------------------------
            // col 9
            //-------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "SizesPN";
            column.Caption = "SizesPN";
            column.DefaultValue = 0;
            ProductRating.Columns.Add(column);

            //-----------------------------------------------------
            // col 10 Is this a new record added in this session
            //-------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "RecordAdded";
            column.Caption = "Record Added";
            column.DefaultValue = false;
            ProductRating.Columns.Add(column);

            //-----------------------------------------------------
            // col 11 
            //-------------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "SizePk";
            column.Caption = "SizePk";
            column.DefaultValue = false;
            ProductRating.Columns.Add(column);

            //0 -- index of record 
            //--------------------------------------------
            selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "PrRatingIndex";
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = ProductRating.Columns[0].ColumnName;
            selecta.HeaderText = "Rating Index"; 
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns["PrRatingIndex"].DisplayIndex= 0;


            //1 -- Button 
            //--------------------------------------------
            selectb = new DataGridViewButtonColumn();
            selectb.Name = "SizeButton";
            selectb.ValueType = typeof(string);
            selectb.HeaderText = "Sizes";
            selectb.DataPropertyName= ProductRating.Columns[1].ColumnName;
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns["SizeButton"].DisplayIndex = 1;

            //2 -- Open / Closed 
            //------------------------------------------------
            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Name = "Open";
            oChkA.HeaderText = "Discontinued";
            oChkA.DataPropertyName = ProductRating.Columns[2].ColumnName;
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns["Open"].DisplayIndex = 2;

            //3 -- Fabric Width
            //----------------------------------------------
            selectg = new DataGridViewTextBoxColumn();
            selectg.Name = "FabWidth";
            selectg.ValueType = typeof(string);
            selectg.DataPropertyName = ProductRating.Columns[3].ColumnName;
            selectg.HeaderText = "Fabric Width";
            dataGridView1.Columns.Add(selectg);
            dataGridView1.Columns["FabWidth"].DisplayIndex = 3;

            //4 -- Ratio
            //----------------------------------------------
            selectc = new DataGridViewTextBoxColumn();
            selectc.Name = "Ratio";
            selectc.HeaderText = "Ratio";
            selectc.ValueType = typeof(decimal);
            selectc.DataPropertyName = ProductRating.Columns[4].ColumnName;
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns["Ratio"].DisplayIndex = 4;

            //5 -- Marker Length
            //-------------------------------------------------
            selectd = new DataGridViewTextBoxColumn();
            selectd.Name = "MarkerLength";
            selectd.HeaderText = "Marker Length";
            selectd.ValueType = typeof(decimal);
            selectd.DataPropertyName = ProductRating.Columns[5].ColumnName;
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns["MarkerLength"].DisplayIndex = 5;

            //6 -- Rating
            //--------------------------------------------------------
            selecte = new DataGridViewTextBoxColumn();
            selecte.Name = "Ratings";
            selecte.HeaderText = "Rating's";
            selecte.ValueType = typeof(decimal);
            selecte.DataPropertyName = ProductRating.Columns[6].ColumnName;
            dataGridView1.Columns.Add(selecte);
            dataGridView1.Columns["Ratings"].DisplayIndex = 6;

            //7 -- Ratio Detail
            //--------------------------------------------------------
            selectf = new DataGridViewButtonColumn();
            selectf.Name = "RatioDet";
            selectf.HeaderText = "Ratio Detail";
            selectf.ValueType = typeof(string);
            selectf.DataPropertyName = ProductRating.Columns[7].ColumnName;
            dataGridView1.Columns.Add(selectf);
            dataGridView1.Columns["RatioDet"].DisplayIndex = 7;

            //8 boolean 
            //----------------------------------------------
            selecth = new DataGridViewTextBoxColumn();
            selecth.Name = "Multi_Marker_Pk";
            selecth.HeaderText = "MultiMarker";
            selecth.ValueType = typeof(Boolean);
            selecth.DataPropertyName = ProductRating.Columns[8].ColumnName;
            dataGridView1.Columns.Add(selecth);
            dataGridView1.Columns["Multi_Marker_Pk"].DisplayIndex = 8;
            
            //9 integer
            //----------------------------------------------
            selectj = new DataGridViewTextBoxColumn();
            selectj.Name = "SizesPN_Pk";
            selectj.HeaderText = "SizesPN";
            selectj.ValueType = typeof(Int32);
            selectj.DataPropertyName = ProductRating.Columns[9].ColumnName;
            dataGridView1.Columns.Add(selectj);
            dataGridView1.Columns["SizesPN_Pk"].DisplayIndex = 9;
            
            //10 bool
            //----------------------------------------------
            selectk = new DataGridViewTextBoxColumn();
            selectk.Name = "NewRecord_Pk";
            selectk.HeaderText = "NewRecord";
            selectk.ValueType = typeof(bool);
            selectk.DataPropertyName = ProductRating.Columns[10].ColumnName;
            dataGridView1.Columns.Add(selectk);
            dataGridView1.Columns["NewRecord_Pk"].DisplayIndex = 10;
          
            //11 integer 
            //----------------------------------------------
            selectl = new DataGridViewTextBoxColumn();
            selectl.Name = "Sizes_Pk";
            selectl.HeaderText = "Sizes_Pk";
            selectl.ValueType = typeof(Int32);
            selectl.DataPropertyName = ProductRating.Columns[11].ColumnName;
            dataGridView1.Columns.Add(selectl);
            dataGridView1.Columns["Sizes_Pk"].DisplayIndex = 11;

            BindingSrc.DataSource = ProductRating;
            dataGridView1.DataSource = BindingSrc;

            DataGridView oDgv = dataGridView1;

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToOrderColumns = false;

            OriginalWidth = dataGridView1.Width;
                        
            int idx = -1;

            foreach (DataColumn col in ProductRating.Columns)
            {
                if (++idx == 0 || idx > 7)
                    dataGridView1.Columns[idx].Visible = false;
                else
                    dataGridView1.Columns[idx].HeaderText = col.Caption;
                    
            }

        }

        private void frmTLADMProductRating_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cmbLabels.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                cmbLabels.DisplayMember = "Cust_Description";
                cmbLabels.ValueMember = "Cust_Pk";
            }

            formloaded = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var Cell = oDgv.CurrentCell;

            if (oDgv.Focused && Cell is DataGridViewTextBoxCell && Cell.ColumnIndex == 4 || Cell.ColumnIndex == 5 || Cell.ColumnIndex == 6)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
             }
        }

        private void cmbLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    // var lblSelected = (TLADM_Labels)oCmb.SelectedItem;

                    cmbStyles.SelectedValue = 0;

                    var lblSelected = (TLADM_CustomerFile)oCmb.SelectedItem;
                    labelIndex = lblSelected.Cust_Pk;
                    if (lblSelected != null)
                    {
                        formloaded = false;
                        cmbStyles.DataSource = context.TLADM_Styles
                                               .Where(x => x.Sty_Label_FK == lblSelected.Cust_Pk)
                                               .OrderBy(X => X.Sty_Description).ToList();
                        cmbStyles.ValueMember = "Sty_Id";
                        cmbStyles.DisplayMember = "Sty_Description";
                        formloaded = true;
                    }
                }
            }
        }

       
        private void cmbStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Util core = new Util();
            //Listsizes contains a list of sizes currently available 
            Listsizes = new List<TLADM_Sizes>();
            TrimSizes = new List<TLADM_Trims>();
            StringBuilder desciption;

            ComboBox oCmb = sender as ComboBox;
            formloaded = false;
            rbBody.Checked = true;
            formloaded = true;
            if (oCmb != null && formloaded)
            {
                ProductRating.Rows.Clear();
                var SelectedStyles = (TLADM_Styles)cmbStyles.SelectedItem;

                if (SelectedStyles != null)
                {
                    styleindex = SelectedStyles.Sty_Id;

                    using (var context = new TTI2Entities())
                    {
                        var StylesDetail = context.TLADM_Styles.Find(styleindex);

                        if (StylesDetail != null)
                        {
                            if (rbBody.Checked)
                            {
                                var SizePw = core.ExtrapNumber(StylesDetail.Sty_Sizes_PN, context.TLADM_Sizes.Count());
                                foreach (var nbr in SizePw)
                                {
                                    var Size = context.TLADM_Sizes.Where(x => x.SI_PowerN == nbr && !(bool)x.SI_Discontinued).FirstOrDefault();
                                    if(Size != null)
                                        Listsizes.Add(Size);
                                }
                            }
                            else if (rbTrims.Checked)
                            {
                                var SizePw = core.ExtrapNumber(StylesDetail.Sty_Trims_PN, context.TLADM_Trims.Count());
                                foreach (var nbr in SizePw)
                                {
                                    var Size = context.TLADM_Trims.FirstOrDefault(x => x.TR_powerN == nbr);
                                    if(Size != null)
                                        TrimSizes.Add(Size);
                                }
                            }

                        }

                        var cust = (TLADM_CustomerFile)cmbLabels.SelectedItem;
                        if (cust != null)
                        {
                            if (rbBody.Checked)
                            {
                                var ExistingData = context.TLADM_ProductRating.Where(x => x.Pr_Style_FK == styleindex && x.Pr_BodyorRibbing == 1 && x.Pr_Customer_FK == cust.Cust_Pk).ToList();
                                if (ExistingData.Count != 0)
                                {
                                    foreach (var row in ExistingData)
                                    {
                                        desciption = new StringBuilder();
                                        
                                        DataRow NewRow = ProductRating.NewRow();
                                        NewRow[0] = row.Pr_Id;
                                        List<int> xx = core.ExtrapNumber(row.Pr_PowerN, context.TLADM_Sizes.Count());
                                        xx.Sort();
                                        foreach (var rw in xx)
                                        {
                                            foreach (var dd in Listsizes)
                                            {
                                                if (dd == null)
                                                    continue;

                                                if (dd.SI_PowerN == rw)
                                                {
                                                    if (desciption.Length == 0)
                                                        desciption.Append(dd.SI_Description);
                                                    else
                                                        desciption.Append(", " + dd.SI_Description);
                                                }
                                            }
                                        }

                                        NewRow[1]  = desciption;
                                        NewRow[2]  = row.Pr_Discontinued;
                                        NewRow[3]  = row.PR_FabricWidth;
                                        NewRow[4]  = row.Pr_Ratio;
                                        NewRow[5]  = Math.Round(row.Pr_Marker_Length, 4);
                                        NewRow[6]  = Math.Round(row.Pr_numeric_Rating, 4);
                                        NewRow[7]  = "Ratio Detail";
                                        NewRow[8]  = row.Pr_MultiMarker;
                                        NewRow[9]  = row.Pr_PowerN;
                                        NewRow[10] = false;
                                        NewRow[11] = row.Pr_Size_FK;

                                        ProductRating.Rows.Add(NewRow);
                                       
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }

        private void datagridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Util core = new Util();
            var oDgv = sender as DataGridView;
            int pn = 0;
            List<int> xx = null;
                  
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewButtonCell)
            {
                if (e.ColumnIndex == 1)
                {
                    if (rbBody.Checked)
                    {
                        var Id = 1005;

                        var RowPos = oDgv.CurrentRow.Index;
                        pn = int.Parse(oDgv.CurrentRow.Cells[9].Value.ToString());
                        frmTLADMGardProp aprop = new frmTLADMGardProp(Id, pn, Listsizes);
                        aprop.ShowDialog();
                        var sizes = aprop.TotalPN;
                        if (sizes != 0)
                        {
                           StringBuilder desciption = new StringBuilder();
                           int a = Convert.ToInt32(sizes);
                           using (var context = new TTI2Entities())
                           {
                                xx = core.ExtrapNumber(a, context.TLADM_Sizes.Count());
                           }
                           xx.Sort();

                           oDgv.CurrentRow.Cells[8].Value = false;
                           if (xx.Count > 1)
                           {
                                oDgv.CurrentRow.Cells[8].Value = true;
                           }

                           foreach (var rw in xx)
                           {
                                bool First = true;
                                foreach (var dd in Listsizes)
                                {
                                   if (dd.SI_PowerN == rw)
                                   {
                                        if(First)
                                        {
                                            First = !First;
                                            oDgv.CurrentRow.Cells[11].Value = dd.SI_id;
                                        }
                                        if (desciption.Length == 0)
                                          desciption.Append(dd.SI_Description);
                                        else
                                          desciption.Append(", " + dd.SI_Description);
                                   }
                                }
                           }

                            oDgv.CurrentRow.Cells[10].Value = false;
                            if ((int)oDgv.CurrentRow.Cells[0].Value == 0)
                            {
                                oDgv.CurrentRow.Cells[10].Value = true;
                            }

                            oDgv.CurrentRow.Cells[1].Value = desciption;
                            oDgv.CurrentRow.Cells[7].Value = "Ratio";
                            oDgv.CurrentRow.Cells[9].Value = sizes; 
                        }

                    }
                    else if (rbTrims.Checked)
                    {
                      
                        /* var StyleSelected = (TLADM_Styles)cmbStyles.SelectedItem;
                        if (StyleSelected != null)
                        {
                            if ((int)oDgv.Rows[e.RowIndex].Cells[11].Value == 0) 
                            {
                                var Id = 1007;
                                int[] TrimKeys = new int[2];
                                // First Element is the style
                                //======================================
                                TrimKeys[0] = StyleSelected.Sty_Id;
                                // Second element is the Primary Key of the Trim
                                //===============================================
                                TrimKeys[1] = (int)oDgv.Rows[e.RowIndex].Cells[11].Value;

                                var RowPos = oDgv.CurrentRow.Index;
                                pn = int.Parse(oDgv.CurrentRow.Cells[9].Value.ToString());
                                frmTLADMGardProp aprop = new frmTLADMGardProp(Id, TrimKeys, true);
                                aprop.ShowDialog();
                                var sizes = aprop._TrimKeys;
                                oDgv.CurrentRow.Cells[8].Value = false;

                                oDgv.CurrentRow.Cells[10].Value = false;
                                if ((int)oDgv.CurrentRow.Cells[0].Value == 0)
                                {
                                    oDgv.CurrentRow.Cells[10].Value = true;
                                }


                                if (sizes[1] > 0)
                                {
                                    oDgv.CurrentRow.Cells[11].Value = sizes[1];

                                    using (var context = new TTI2Entities())
                                    {
                                        oDgv.CurrentRow.Cells[1].Value = context.TLADM_Trims.Find(sizes[1]).TR_Description;
                                    }
                                }
                                oDgv.CurrentRow.Cells[7].Value = "Ratio";
                                oDgv.CurrentRow.Cells[9].Value = 0;
                            }
                            
                        }*/
                    }
                }
                else
                {
                    // Only available to body details
                    //-----------------------------------------------
                    if (!rbBody.Checked)
                    {
                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                        {
                            MessageBox.Show("Please select the body check box prior to selecting this option");
                        }
                        return;
                    }

                    //Only available to multi sized markers
                    //-----------------------------------------------
                    var CurrentRow = oDgv.CurrentRow;

                     if (!(bool)CurrentRow.Cells[8].Value)
                     {
                         using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                         {
                             MessageBox.Show("This option is only available to multi sized Markers");
                             return;
                         }
                    }
                    else
                    {
                        var PNumber = (int)CurrentRow.Cells[9].Value;
                        int PrimaryKey = (int)CurrentRow.Cells[0].Value;
                        
                        if(PrimaryKey == 0)
                        {
                            using (var context = new TTI2Entities())
                            {
                                TLADM_ProductRating ProductRating = new TLADM_ProductRating();
                                context.TLADM_ProductRating.Add(ProductRating);

                                try
                                {
                                    context.SaveChanges();
                                    PrimaryKey = ProductRating.Pr_Id;
                                    CurrentRow.Cells[0].Value = PrimaryKey;
                                    CurrentRow.Cells[10].Value = false;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.InnerException.Message.ToString());
                                    return;
                                }
                            }
                        }
                        
                       
                        frmTLADMProductRatingDetail prodDetail = new frmTLADMProductRatingDetail(PNumber, PrimaryKey);
                        prodDetail.ShowDialog(this);
                        if (prodDetail.ProductRatio != 0)
                        {
                             oDgv.Rows[e.RowIndex].Cells[4].Value = (decimal)prodDetail.ProductRatio;
                             if (oDgv.Rows[e.RowIndex].Cells[5].Value != null)
                             {
                                var ML = (decimal)oDgv.Rows[e.RowIndex].Cells[5].Value;
                                try
                                {
                                        oDgv.Rows[e.RowIndex].Cells[6].Value = Math.Round(ML / prodDetail.ProductRatio, 4);
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
        }

        private void rbBody_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRb = sender as RadioButton;

            if (formloaded && oRb != null)
            {
                if (oRb.Checked)
                {
                    dataGridView1.Columns[1].HeaderText = "Sizes";
                    dataGridView1.Refresh();
                    cmbStyles_SelectedIndexChanged(cmbStyles, null);
                }
            }
        }

        private void rbRibbing_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRb = sender as RadioButton;

            if (formloaded && oRb != null)
            {
                if (oRb.Checked)
                {
                    cmbStyles.SelectedIndexChanged -= new EventHandler(rbRibbing_Click);
                    cmbStyles.SelectedIndexChanged += new EventHandler(rbRibbing_Click);
                }
            }
        }

        private void rbRibbing_Click(object sender, EventArgs e)
        {
            RadioButton oRb = sender as RadioButton;

            if (formloaded && oRb != null)
            {
                cmbStyles_SelectedIndexChanged(cmbStyles, null);
            }
        }

        private void rbTrims_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRb = sender as RadioButton;

            if (formloaded && oRb != null)
            {
                if (oRb.Checked)
                {
                    ProductRating.Rows.Clear(); 

                    dataGridView1.Columns[1].HeaderText  = "Trims";

                    var Selected = (TLADM_Styles)cmbStyles.SelectedItem;

                    if (Selected != null)
                    {
                        using (var context = new TTI2Entities())
                        {
                            int DummyRecord = 91200;

                            var AvailTrims = (from styT in context.TLADM_StyleTrim
                                              join Styles in context.TLADM_Styles on styT.StyTrim_Styles_Fk equals Styles.Sty_Id
                                              where Styles.Sty_Id == Selected.Sty_Id
                                              select styT).ToList();
                            foreach(var Trim in AvailTrims)
                            {
                                DataRow nRow = ProductRating.NewRow();
                                if(Trim.StyTrim_ProdRating_FK > 0)
                                {
                                    nRow[0] = Trim.StyTrim_ProdRating_FK;
                                    nRow[10] = false;

                                    var ProdRating = context.TLADM_ProductRating.Find(Trim.StyTrim_ProdRating_FK);
                                    if(ProdRating != null)
                                    {
                                        nRow[2] = ProdRating.Pr_Discontinued;
                                        nRow[3] = ProdRating.PR_FabricWidth;
                                        nRow[4] = ProdRating.Pr_Ratio;
                                        nRow[5] = Math.Round(ProdRating.Pr_Marker_Length, 4);
                                        nRow[6] = Math.Round(ProdRating.Pr_numeric_Rating, 4);
                                    }
                                }
                                else
                                {
                                    nRow[0] = ++DummyRecord;
                                    nRow[2] = false;
                                    nRow[3] = 0;
                                    nRow[4] = 1.0000M;
                                    nRow[5] = 0.00M;
                                    nRow[6] = 0.00M;
                                    nRow[10] = true;
                                }

                                nRow[1] = context.TLADM_Trims.Find(Trim.StyTrim_Trim_Fk).TR_Description;
                                nRow[7] = string.Empty;
                                nRow[8] = false;
                                nRow[9] = 0;
                                nRow[11] = Trim.StyTrim_Trim_Fk;
                               
                                ProductRating.Rows.Add(nRow);
                            }

                         }
                    }
                    dataGridView1.Refresh();
                   // cmbStyles.SelectedIndexChanged -= new EventHandler(rbTrims_Click);
                   // cmbStyles.SelectedIndexChanged += new EventHandler(rbTrims_Click);
                }
            }
        }

        private void rbTrims_Click(object sender, EventArgs e)
        {
            RadioButton oRb = sender as RadioButton;

            if (formloaded && oRb != null)
            {
               // cmbTrims_SelectedIndexChanged(cmbStyles, null);
            }
        }

        private void cmbTrims_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = new ComboBox();
           
            if (formloaded && oCmb != null)
            {
                using (var context = new TTI2Entities())
                {
                    var cust = (TLADM_CustomerFile)cmbLabels.SelectedItem;
                    if (cust != null)
                    {
                        var ExistingData = context.TLADM_ProductRating.Where(x => x.Pr_Style_FK == styleindex && x.Pr_BodyorRibbing == 0 && x.Pr_Customer_FK == cust.Cust_Pk).ToList();
                        if (ExistingData.Count != 0)
                        {
                            foreach (var row in ExistingData)
                            {
                                DataRow NewRow = ProductRating.NewRow();
                                NewRow[0] = row.Pr_Id;
                                NewRow[1] = context.TLADM_Trims.Find(row.Pr_Trim_FK).TR_Description;
                                NewRow[2] = row.Pr_Discontinued;
                                NewRow[3] = row.PR_FabricWidth;
                                NewRow[4] = row.Pr_Ratio;
                                NewRow[5] = Math.Round(row.Pr_Marker_Length, 4);
                                NewRow[6] = Math.Round(row.Pr_numeric_Rating, 4);
                                NewRow[7] = String.Empty;
                                NewRow[8] = false;
                                NewRow[9] = 0;
                                NewRow[10] = false;
                                NewRow[11] = row.Pr_Trim_FK;

                                ProductRating.Rows.Add(NewRow);

                            }
                        }
                    }
                }
            }
            
        }

        private void btnSaveTrims_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool updateSuccessful = true;

            if (formloaded && oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataRow DRow in ProductRating.Rows)
                    {
                        TLADM_ProductRating pr = new TLADM_ProductRating();

                        if (!DRow.Field<bool>(10))
                        {
                            pr = context.TLADM_ProductRating.Find(DRow.Field<int>(0));
                        }

                        var Style = (TLADM_Styles)cmbStyles.SelectedItem;
                        if (Style != null)
                        {
                            pr.Pr_Style_FK = Style.Sty_Id;
                        }

                        if (rbBody.Checked)
                        {
                            pr.Pr_BodyorRibbing = 1;
                            pr.Pr_Size_FK = DRow.Field<int>(11);
                        }
                        else
                        {
                            pr.Pr_BodyorRibbing = 0;
                            pr.Pr_Trim_FK = DRow.Field<int>(11);

                            if (!DRow.Field<bool>(10))
                            {
                                var StyTrim = context.TLADM_StyleTrim.FirstOrDefault(x => x.StyTrim_Styles_Fk == pr.Pr_Style_FK && x.StyTrim_Trim_Fk == pr.Pr_Trim_FK);
                                if (StyTrim != null && StyTrim.StyTrim_ProdRating_FK != pr.Pr_Id)
                                    StyTrim.StyTrim_ProdRating_FK = pr.Pr_Id;
                            }
                        }
                       
                        var Cust = (TLADM_CustomerFile)cmbLabels.SelectedItem;
                        if (Cust != null)
                            pr.Pr_Customer_FK = Cust.Cust_Pk;

                    
                        pr.Pr_PowerN        = DRow.Field<int>(9);
                        pr.Pr_Discontinued  = DRow.Field<bool>(2);
                        pr.Pr_MultiMarker   = DRow.Field<bool>(8);
                        pr.Pr_Ratio = DRow.Field<decimal>(4);
                        if (!DRow.Field<bool>(8))
                        {
                            pr.Pr_Ratio = 1;
                        }
                        pr.Pr_Marker_Length = DRow.Field<decimal>(5);
                        pr.Pr_numeric_Rating = DRow.Field<decimal>(6);

                        pr.Pr_Display = DRow.Field<String>(1);
                        if (!String.IsNullOrWhiteSpace(DRow.Field<string>(3)))
                        {
                            pr.Pr_Display +=  " / " + DRow.Field<string>(3);
                        }

                        if (DRow.Field<bool>(10))
                        {
                            context.TLADM_ProductRating.Add(pr);
                            try
                            {
                                context.SaveChanges();
                                var StyTrim = context.TLADM_StyleTrim.FirstOrDefault(x => x.StyTrim_Styles_Fk == pr.Pr_Style_FK && x.StyTrim_Trim_Fk == pr.Pr_Trim_FK);
                                if (StyTrim != null)
                                {
                                    if (StyTrim.StyTrim_ProdRating_FK != pr.Pr_Id)
                                        StyTrim.StyTrim_ProdRating_FK = pr.Pr_Id;
                                }
                            }
                            catch(Exception ex)
                            {

                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        updateSuccessful = false;
                    }
                    finally
                    {
                        if (updateSuccessful)
                        {
                            ProductRating.Rows.Clear();
                            MessageBox.Show("Records successfully updated");
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var Cell = oDgv.CurrentCell;
            if ((e.ColumnIndex == 4 || e.ColumnIndex == 5) && !String.IsNullOrEmpty(Cell.EditedFormattedValue.ToString()))
            {
                if (e.ColumnIndex == 4)
                {
                    if (Cell.EditedFormattedValue != null)
                    {
                        var Ratio = Convert.ToDecimal(Cell.EditedFormattedValue);
                        var ML = Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[5].Value);
                        oDgv.Rows[Cell.RowIndex].Cells[6].Value = Math.Round(ML / Ratio, 4);
                    }
                }
                else if (e.ColumnIndex == 5)
                {
                    if (Cell.EditedFormattedValue != null)
                    {
                        var ML = Convert.ToDecimal(Cell.EditedFormattedValue);
                        var Ratio = Convert.ToDecimal(oDgv.Rows[Cell.RowIndex].Cells[5].Value);
                        oDgv.Rows[Cell.RowIndex].Cells[6].Value = Math.Round(ML / Ratio, 4);
                    }
                }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            int Ratings_FK = 0;
            if (oDgv != null && formloaded && e.Button.ToString() == "Right")
            {
                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        DataGridViewRow cr = oDgv.CurrentRow;
                        if (cr.Cells[0].Value != null)
                        {
                            // We need to check that there are no existing records 
                            //========================================================
                            Ratings_FK = Convert.ToInt32(cr.Cells[0].Value.ToString());
                            if (core.CheckActiveRatings(Ratings_FK))
                            {
                                using (var context = new TTI2Entities())
                                {
                                    //------------------------------------------------------------------------------------
                                    // First we must ensure that all subordinate records are deleted first
                                    //---------------------------------------------------------------------------
                                    context.TLADM_ProductRating_Detail.RemoveRange(context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == Ratings_FK));
                                    var locRec = context.TLADM_ProductRating.Find(Ratings_FK);
                                    if (locRec != null)
                                    {
                                        try
                                        {
                                            context.TLADM_ProductRating.Remove(locRec);
                                            context.SaveChanges();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                    }

                                    if (dataGridView1.IsCurrentRowDirty)
                                    {
                                        this.Validate();
                                    }

                                    oDgv.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                                }
                            }
                            else
                            {
                                MessageBox.Show("There are currently active records for this Rating", "Record cannot be deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }
    }
 
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TTI2_WF
{
    public partial class frmTLADMProduct_Rating : Form
    {
        private DataTable dt;
        private DataGridViewComboBoxColumn Style;
        private DataGridViewTextBoxColumn Quality;
        private DataGridViewTextBoxColumn Weight;
        private DataGridViewComboBoxColumn GarWidth;
        private DataGridViewComboBoxColumn GarSize;
        private DataGridViewTextBoxColumn Rating;
        private DataGridViewTextBoxColumn Yield;
        private DataGridViewTextBoxColumn Pieces;
        private DataGridViewTextBoxColumn Indexs;
        bool nonNumeric;
        const int TotNo = 50000;


        public frmTLADMProduct_Rating()
        {
            InitializeComponent();
            this.Text = "Product Ratings for Tradelink Garments";
            Setup();
        }

        private void Setup()
        {
            dt = new DataTable();
            dataGridView1.AutoGenerateColumns = false;

            Style = new DataGridViewComboBoxColumn(); //1
            Style.ValueMember = "Sty_Id"; //0
            Style.DisplayMember = "Sty_Description";
            Style.HeaderText = "Style";
            Style.DataPropertyName = "StyleIndex";
            Style.Width = 100;

            Quality = new DataGridViewTextBoxColumn(); //2
            Quality.HeaderText = "Quality";
            Quality.DataPropertyName = "QualityIndex";
            Quality.Width = 150;

            Weight = new DataGridViewTextBoxColumn(); //3
            Weight.HeaderText = "Weight";
            Weight.DataPropertyName = "WeightIndex";
            Weight.Width = 75;

            GarWidth = new DataGridViewComboBoxColumn(); //4
            GarWidth.ValueMember = "FW_Id"; //0
            GarWidth.DisplayMember = "FW_Description";
            GarWidth.HeaderText = "Width";
            GarWidth.DataPropertyName = "WidthIndex";
            GarWidth.Width = 75;

            GarSize = new DataGridViewComboBoxColumn(); //5
            GarSize.ValueMember = "SI_id"; //0
            GarSize.DisplayMember = "SI_Description";
            GarSize.HeaderText = "Size";
            GarSize.DataPropertyName = "SizeIndex";
            GarSize.Width = 100;

            Rating = new DataGridViewTextBoxColumn(); //6
            Rating.HeaderText = "Rating";
            Rating.DataPropertyName = "RatingIndex";
            Rating.Width = 75;
            
            Yield = new DataGridViewTextBoxColumn(); //7
            Yield.HeaderText = "Yield";
            Yield.DataPropertyName = "YieldIndex";
            Yield.Width = 75;
            Yield.ReadOnly = true;

            Pieces = new DataGridViewTextBoxColumn(); //8
            Pieces.HeaderText = "PCS/KG";
            Pieces.DataPropertyName = "PiecesIndex";
            Pieces.Width = 75;
            Pieces.ReadOnly = true;

            Indexs = new DataGridViewTextBoxColumn(); //9
            Indexs.HeaderText = "Indexs";
            Indexs.DataPropertyName = "IndexIndex";
            Indexs.Width = 50;
            Indexs.Visible = false;

           
            dataGridView1.Columns.Add(Style);   //1
            dataGridView1.Columns.Add(Quality); //2
            dataGridView1.Columns.Add(Weight);  //3
            dataGridView1.Columns.Add(GarWidth); //4
            dataGridView1.Columns.Add(GarSize);  //5
            dataGridView1.Columns.Add(Rating); //6
            dataGridView1.Columns.Add(Yield); //7
            dataGridView1.Columns.Add(Pieces); //8
            dataGridView1.Columns.Add(Indexs); //9

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

            using (var context = new TTI2Entities())
            {
                try
                {
                    Style.DataSource = context.TLADM_Styles.OrderBy(x => x.Sty_Id).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                }

                try
                {
                    GarSize.DataSource = context.TLADM_Sizes.OrderBy(x => x.SI_id).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                }

                try
                {
                    GarWidth.DataSource = context.TLADM_FabWidth.OrderBy(x => x.FW_Id).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();
                }
               var ExistingData = context.TLADM_ProductRating
                                   .OrderBy(x => x.PR_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = ExistingRow.PR_Style_FK;
                    dataGridView1.Rows[index].Cells[1].Value = ExistingRow.PR_Quality.ToString();
                    dataGridView1.Rows[index].Cells[2].Value = ExistingRow.Pr_Wieght;
                    dataGridView1.Rows[index].Cells[3].Value = ExistingRow.Pr_Width;
                    dataGridView1.Rows[index].Cells[4].Value = ExistingRow.Pr_Size_FK;
                    dataGridView1.Rows[index].Cells[5].Value = ExistingRow.PR_Rating;
                    dataGridView1.Rows[index].Cells[6].Value = Math.Round(ExistingRow.PR_Yield,4);
                    dataGridView1.Rows[index].Cells[7].Value = Math.Round(ExistingRow.PR_PCS_Kg, 4);
                    dataGridView1.Rows[index].Cells[8].Value = ExistingRow.PR_Id;
                }
            }
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var oDgv = sender as DataGridView;
            var oTextBox = e.Control as TextBox;
            ComboBox oCombo = e.Control as ComboBox;

            if (oTextBox != null)
            {
                //-------------------------------------------------------------------------------------------
                // Remove an existing event-handler, if present, to avoid 
                // adding multiple handlers when the editing control is reused.
                //-----------------------------------------------------------------------------------------
                if (oDgv.CurrentCellAddress.X > 4)
                    oTextBox.KeyDown -= new KeyEventHandler(txtWin_KeyDownOEM);
                else if (oDgv.CurrentCellAddress.X == 2 || oDgv.CurrentCellAddress.X == 3)
                    oTextBox.KeyDown -= new KeyEventHandler(txtWin_KeyDown);

                oTextBox.KeyPress -= new KeyPressEventHandler(txtWin_KeyPress);

                if (oDgv.CurrentCellAddress.X > 4)
                    oTextBox.KeyDown += new KeyEventHandler(txtWin_KeyDownOEM);
                else if (oDgv.CurrentCellAddress.X == 2 || oDgv.CurrentCellAddress.X == 3)
                    oTextBox.KeyDown += new KeyEventHandler(txtWin_KeyDown);
                oTextBox.KeyPress += new KeyPressEventHandler(txtWin_KeyPress);

                oTextBox.TextChanged -= new EventHandler(TextBox_Changed);
                oTextBox.TextChanged += new EventHandler(TextBox_Changed);

            }
            else if (oCombo != null)
            {
                oCombo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                oCombo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        private void TextBox_Changed(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
             var Cell = dataGridView1.CurrentCell;
            if (tb != null)
            {  
                if (Cell.ColumnIndex == 2)
                {
                    //Column 2 is the weight factor
                    // Yield = 50,000 / weight / width 
                    //-------------------------------------------------------
                    var fabIndex = dataGridView1.Rows[Cell.RowIndex].Cells[1 + Cell.ColumnIndex].EditedFormattedValue;
                    if (!String.IsNullOrEmpty(fabIndex.ToString()) && !String.IsNullOrEmpty(tb.Text))
                    {
                        int fw = Convert.ToInt32(fabIndex);
                        int wght = Convert.ToInt32(tb.Text);

                        dataGridView1.Rows[Cell.RowIndex].Cells[6].Value = Math.Round((decimal)TotNo / fw / wght, 4); 

                    }
                }
                else if (Cell.ColumnIndex == 5)
                {
                   //Column 5 is the rating factor 
                    // pcs / kg = Yield * Rating 
                   //----------------------------------------------
                      var yield = dataGridView1.Rows[Cell.RowIndex].Cells[1 + Cell.ColumnIndex].EditedFormattedValue;
                      if (!String.IsNullOrEmpty(yield.ToString()) && !String.IsNullOrEmpty(tb.Text))
                      {
                          decimal rating = Convert.ToDecimal(tb.Text);
                          decimal yld = Convert.ToDecimal(yield);

                          dataGridView1.Rows[Cell.RowIndex].Cells[7].Value = Math.Round(rating * yld, 4); 
                      }
                }
            
               
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            var Cell = dataGridView1.CurrentCell;
            if (cb != null)
            {
                if (Cell.ColumnIndex == 3)
                {
                   //1st Make sure that the cell left has a value 
                    var WeightValue = dataGridView1.Rows[Cell.RowIndex].Cells[-1 + Cell.ColumnIndex].EditedFormattedValue;
                    if (!String.IsNullOrEmpty(WeightValue.ToString()))
                    {
                        // Convert That to and integer.....It will be 
                        var Weight = Convert.ToInt32(WeightValue);

                        var oFabWidth = (TLADM_FabWidth)cb.SelectedItem;
                        if (oFabWidth != null)
                        {
                            var FabWidth = Convert.ToInt32(oFabWidth.FW_Description);
                            dataGridView1.Rows[Cell.RowIndex].Cells[3 + Cell.ColumnIndex].Value = Math.Round((decimal)TotNo / Weight / FabWidth, 2).ToString();
                        }
                    }
                    else
                        dataGridView1.Rows[Cell.RowIndex].Cells[3 + Cell.ColumnIndex].Value = null;
                }
            }
        }

        private void txtWin_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumeric = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace or a period. 
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }
            }
            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
        }

        private void txtWin_KeyDownOEM(object sender, KeyEventArgs e)
        {
            nonNumeric = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace or a period. 
                    if (e.KeyCode != Keys.Back && e.KeyCode != Keys.OemPeriod)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }
            }
            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
        }
        private void txtWin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumeric)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

            bool lAdd;
            bool lNoProblems = false;
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            lAdd = true;
                            TLADM_ProductRating pr = new TLADM_ProductRating();

                            if (row.Cells[8].Value != null)
                            {
                                var index = (int)row.Cells[8].Value;
                                pr = context.TLADM_ProductRating.Find(index);
                                lAdd = false;
                            }

                            pr.PR_Style_FK = (int)row.Cells[0].Value;
                            pr.PR_Quality = row.Cells[1].Value.ToString();
                            pr.Pr_Wieght = Convert.ToInt32(row.Cells[2].Value.ToString());
                            pr.Pr_Width = Convert.ToInt32(row.Cells[3].Value.ToString());
                            pr.Pr_Size_FK = Convert.ToInt32(row.Cells[4].Value.ToString());
                            pr.PR_Rating = Convert.ToDecimal(row.Cells[5].Value.ToString());
                            pr.PR_Yield = Convert.ToDecimal(row.Cells[6].Value.ToString());
                            pr.PR_PCS_Kg = Convert.ToDecimal(row.Cells[7].Value.ToString());


                            if (lAdd)
                                context.TLADM_ProductRating.Add(pr);

                            try
                            {
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                lNoProblems = true;
                            }
                        }
                    }
                }
                if (!lNoProblems)
                {
                    dataGridView1.Rows.Clear();
                    MessageBox.Show("All records stored to the database");
                }
            }
      
        }

        private void DataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = new DataGridView();
            if (oDgv != null && oDgv.Focused)
            {

                if (e.ColumnIndex == 6)
                {

                    // oDgv.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = (decimal) TotNo / oDgv.Rows[e.RowIndex].Cells[2].Value / oDgv.Rows[e.RowIndex].Cells[3].Value;

                }
            }
        }
      
    }
}

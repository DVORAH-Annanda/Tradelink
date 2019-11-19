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
    public partial class frmTLADMGardDef2 : Form
    {
        bool formLoaded = false;
        bool LabelSelected = false;

        DataGridViewTextBoxColumn Name_Of_Garment;
        DataGridViewTextBoxColumn AltName_Of_Garment;
        DataGridViewButtonColumn Styles;
        DataGridViewButtonColumn Color;
        DataGridViewButtonColumn Griege;
        DataGridViewButtonColumn Ribbing;
        DataGridViewButtonColumn Trim;
        DataGridViewButtonColumn Sizes;
        DataGridViewTextBoxColumn PK;

        public frmTLADMGardDef2()
        {
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            Util core = new Util();
            formLoaded = false;
            LabelSelected = false;
            int index = 0;
            dataGridView1.AutoGenerateColumns = false;

            using (var context = new TTI2Entities())
            {
                try
                {
                    cmbLabels.DataSource = context.TLADM_Labels.OrderBy(x => x.Lbl_Id).ToList();
                    cmbLabels.ValueMember = "Lbl_Id";
                    cmbLabels.DisplayMember = "Lbl_Description";
                    cmbLabels.SelectedIndex = 0;
                    index = Convert.ToInt32(cmbLabels.SelectedValue); 

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Close();

                }
            }

            Name_Of_Garment = new DataGridViewTextBoxColumn();
            Name_Of_Garment.HeaderText = "Name Of Garment";
            Name_Of_Garment.DataPropertyName = "NameOfGarmentIndex";
            Name_Of_Garment.Width = 100;

            AltName_Of_Garment = new DataGridViewTextBoxColumn();
            AltName_Of_Garment.HeaderText = "Alt Name Of Garment";
            AltName_Of_Garment.DataPropertyName = "AltNameOfGarmentIndex";
            AltName_Of_Garment.Width = 100;

            Styles = new DataGridViewButtonColumn();
            Styles.HeaderText = "Styles Selection";
            Styles.DataPropertyName = "Styles_selection";

            Color = new DataGridViewButtonColumn();
            Color.HeaderText = "Colour Selection";
            Color.DataPropertyName = "Colors_selection";

            Griege = new DataGridViewButtonColumn();
            Griege.HeaderText = "Griege Selection";
            Griege.DataPropertyName = "Griege_selection";

            Ribbing = new DataGridViewButtonColumn();
            Ribbing.HeaderText = "Ribbing Selection";
            Ribbing.DataPropertyName = "Ribbing_selection";

            Trim = new DataGridViewButtonColumn();
            Trim.HeaderText = "Trim Selection";
            Trim.DataPropertyName = "Trim_selection";

            Sizes = new DataGridViewButtonColumn();
            Sizes.HeaderText = "Size Selection";
            Sizes.DataPropertyName = "Sizes_selection";

            PK = new DataGridViewTextBoxColumn();
            PK.HeaderText = "Primary Key";
            PK.DataPropertyName = "NameOfGarmentIndex";
            PK.Width = 100;
            PK.Visible = false;

            dataGridView1.Columns.Add(Name_Of_Garment);   //1
            dataGridView1.Columns.Add(AltName_Of_Garment);   //2
            dataGridView1.Columns.Add(Styles);   //3
            dataGridView1.Columns.Add(Color);   //4
            dataGridView1.Columns.Add(Griege);   //5
            dataGridView1.Columns.Add(Ribbing);   //6
            dataGridView1.Columns.Add(Trim);   //7
            dataGridView1.Columns.Add(Sizes);   //8
            dataGridView1.Columns.Add(PK);   //9
            formLoaded = true;

            dataGridView1 = core.Get_GarDef(dataGridView1, index);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Util core = new Util();
            Button oBtn = sender as Button;

            if (oBtn != null)
            {
                if (!LabelSelected)
                {
                    MessageBox.Show("Please Select a label from the drop down list above");
                    return;
                }

               var lbl = (TLADM_Labels)cmbLabels.SelectedItem;
               if(lbl != null)
               {
                   try
                   {

                       bool success = core.Save_GarDef(dataGridView1, lbl.Lbl_Id);
                       if (success)
                       {
                           MessageBox.Show("Records saved to database");
                           dataGridView1.Rows.Clear();
                           LabelSelected = false;
                       }
                   }
                   catch (Exception ex)
                   {
                       MessageBox.Show(ex.Message);
                   }
               }
            }
        }

        private void cmbLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formLoaded)
            {
                LabelSelected = true;
                var lbls = (TLADM_Labels)cmbLabels.SelectedItem;
                if (lbls != null)
                {
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int pn = 0;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewButtonCell)
            {
                var Id = 1000 + e.ColumnIndex;
                
                if (!String.IsNullOrEmpty(oDgv.CurrentCell.EditedFormattedValue.ToString()))
                    pn = Convert.ToInt32(oDgv.CurrentCell.EditedFormattedValue.ToString());
                frmTLADMGardProp aprop = new frmTLADMGardProp(Id, pn);
                aprop.ShowDialog();
                oDgv.CurrentCell.Value = aprop.TotalPN.ToString();
            }
        }
    }
}

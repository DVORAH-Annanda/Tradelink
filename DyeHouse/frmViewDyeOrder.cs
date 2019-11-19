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
    public partial class frmViewDyeOrder : Form
    {
        bool FormLoaded;
        DyeRepository repo;
        DyeQueryParameters DyeParameters;

        DataGridViewTextBoxColumn oTxtBoxA = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxB = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxC = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxD = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxE = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxF = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxG = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxH = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxJ = new DataGridViewTextBoxColumn();
        

        public frmViewDyeOrder()
        {
            InitializeComponent();
            repo = new DyeRepository();
            oTxtBoxA.HeaderText = "pk";
            oTxtBoxA.ValueType = typeof(int);
            oTxtBoxA.Visible = false;

            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            oChkA.Visible = true;

            oTxtBoxB.HeaderText = "DyeOrder No";
            oTxtBoxB.ValueType = typeof(string);
            oTxtBoxB.ReadOnly = true;

            oTxtBoxC.HeaderText = "DyeOrder Date";
            oTxtBoxC.ValueType = typeof(DateTime);
            oTxtBoxC.ReadOnly = true;

            oTxtBoxD.HeaderText = "Customer";
            oTxtBoxD.ValueType = typeof(string);
            oTxtBoxD.ReadOnly = true;

            oTxtBoxE.HeaderText = "Style";
            oTxtBoxE.ValueType = typeof(string);
            oTxtBoxE.ReadOnly = true;

            oTxtBoxF.HeaderText = "Quality";
            oTxtBoxF.ValueType = typeof(string);
            oTxtBoxF.ReadOnly = true;

            oTxtBoxG.HeaderText = "Colour";
            oTxtBoxG.ValueType = typeof(string);
            oTxtBoxG.ReadOnly = true;

            oTxtBoxH.HeaderText = "Lable";
            oTxtBoxH.ValueType = typeof(string);
            oTxtBoxH.ReadOnly = true;

            oTxtBoxJ.HeaderText = "Notes";
            oTxtBoxJ.ValueType = typeof(string);
            oTxtBoxJ.ReadOnly = true;

          

            dataGridView1.Columns.Add(oTxtBoxA);    //0
            dataGridView1.Columns.Add(oChkA);       //1
            dataGridView1.Columns.Add(oTxtBoxB);    //2
            dataGridView1.Columns.Add(oTxtBoxC);    //3
            dataGridView1.Columns.Add(oTxtBoxD);    //4
            dataGridView1.Columns.Add(oTxtBoxE);    //5
            dataGridView1.Columns.Add(oTxtBoxF);    //6
            dataGridView1.Columns.Add(oTxtBoxG);    //7 
            dataGridView1.Columns.Add(oTxtBoxH);    //8
            dataGridView1.Columns.Add(oTxtBoxJ);    //9
           

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            this.comboColour.CheckStateChanged += new System.EventHandler(this.comboColour_CheckStateChanged);
            this.comboCustomers.CheckStateChanged += new EventHandler(this.comboCustomers_CheckStateChanged);
            this.comboQuality.CheckStateChanged += new System.EventHandler(this.comboQuality_CheckStateChanged);
            this.comboStyles.CheckStateChanged += new System.EventHandler(this.comboStyles_CheckStateChanged);
        }

        private void frmViewDyeOrder_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            DyeParameters = new DyeQueryParameters();

            using (var context = new TTI2Entities())
            {
                var Customers = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                foreach (var Customer in Customers)
                {
                    comboCustomers.Items.Add(new DyeHouse.CheckComboBoxItem(Customer.Cust_Pk, Customer.Cust_Description, false));
                }

                var Qualitys = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                foreach (var Quality in Qualitys)
                {
                    comboQuality.Items.Add(new DyeHouse.CheckComboBoxItem(Quality.TLGreige_Id, Quality.TLGreige_Description, false));
                }

                var Colours = context.TLADM_Colours.OrderBy(x => x.Col_Display).ToList();
                foreach (var Colour in Colours)
                {
                    comboColour.Items.Add(new DyeHouse.CheckComboBoxItem(Colour.Col_Id, Colour.Col_Display, false));
                }

                var Styles = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                foreach (var Style in Styles)
                {
                    comboStyles.Items.Add(new DyeHouse.CheckComboBoxItem(Style.Sty_Id, Style.Sty_Description, false));
                }

            }
            FormLoaded = true;


        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboColour_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    DyeParameters.Colours.Add(repo.LoadColour(item._Pk));
                }
                else
                {
                    var value = DyeParameters.Colours.Find(it => it.Col_Id == item._Pk);
                    if (value != null)
                        DyeParameters.Colours.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboStyles_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    DyeParameters.Styles.Add(repo.LoadStyle(item._Pk));
                }
                else
                {
                    var value = DyeParameters.Styles.Find(it => it.Sty_Id == item._Pk);
                    if (value != null)
                        DyeParameters.Styles.Remove(value);
                }
            }
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboCustomers_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    DyeParameters.Customers.Add(repo.LoadCustomer(item._Pk));
                }
                else
                {
                    var value = DyeParameters.Customers.Find(it => it.Cust_Pk == item._Pk);
                    if (value != null)
                        DyeParameters.Customers.Remove(value);
                }
            }
        }
        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboQuality_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem)
            {
                DyeHouse.CheckComboBoxItem item = (DyeHouse.CheckComboBoxItem)sender;
                if (item.CheckState)
                {
                    DyeParameters.Qualities.Add(repo.LoadQuality(item._Pk));
                }
                else
                {
                    var value = DyeParameters.Qualities.Find(it => it.TLGreige_Id == item._Pk);
                    if (value != null)
                        DyeParameters.Qualities.Remove(value);
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && FormLoaded)
            {
                dataGridView1.Rows.Clear();

                DyeParameters.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DyeParameters.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                DyeParameters.ToDate = DyeParameters.ToDate.AddHours(23);

                var DyeOrders = repo.SelectDyeOrders(DyeParameters);

                using (var context = new TTI2Entities())
                {
                    foreach (var DyeOrder in DyeOrders)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = DyeOrder.TLDYO_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = DyeOrder.TLDYO_DyeOrderNum;
                        dataGridView1.Rows[index].Cells[3].Value = DyeOrder.TLDYO_OrderDate.ToShortDateString();
                        var Pk = DyeOrder.TLDYO_Customer_FK;
                        dataGridView1.Rows[index].Cells[4].Value = context.TLADM_CustomerFile.Find(Pk).Cust_Description;
                        Pk = DyeOrder.TLDYO_Style_FK;
                        dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Styles.Find(Pk).Sty_Description;
                        Pk = DyeOrder.TLDYO_Greige_FK;
                        dataGridView1.Rows[index].Cells[6].Value = context.TLADM_Griege.Find(Pk).TLGreige_Description;
                        Pk = DyeOrder.TLDYO_Colour_FK;
                        dataGridView1.Rows[index].Cells[7].Value = context.TLADM_Colours.Find(Pk).Col_Display;
                        Pk = DyeOrder.TLDYO_Label_FK;
                        dataGridView1.Rows[index].Cells[8].Value = context.TLADM_Labels.Find(Pk).Lbl_Description;
                        dataGridView1.Rows[index].Cells[9].Value = DyeOrder.TLDYO_Notes;
                   
                    }
                }

                comboQuality.Items.Clear();
                comboColour.Items.Clear();
                comboCustomers.Items.Clear();

                frmViewDyeOrder_Load(this, null);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridView oDgv = sender as DataGridView;
             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 bool isChecked = (bool)oDgv[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                 if (isChecked)
                 {

                     using (var context = new TTI2Entities())
                     {
                        var CurrentRow = oDgv.CurrentRow;

                        var Pk = (int)CurrentRow.Cells[0].Value;

                        frmDyeViewReport vRep = new frmDyeViewReport(3, Pk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                     }
                 }
            }
       }
    }
}

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
    public partial class frmViewDyeBatches : Form
    {
        DyeRepository repo;
        DyeQueryParameters DyeParameters;
        bool FormLoaded;

        bool ViewData;
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
        DataGridViewTextBoxColumn oTxtBoxK = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxL = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxM = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxN = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxP = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtBoxQ = new DataGridViewTextBoxColumn();

        public frmViewDyeBatches(bool Vw)
        {
            InitializeComponent();

            ViewData = Vw;
 
            repo = new DyeRepository();

            if (ViewData)
                this.Text = "View Dye Batches - To Screen";
            else
                this.Text = "View Dye Batches - To Printer";

            oTxtBoxA.HeaderText = "pk";
            oTxtBoxA.ValueType = typeof(int);
            oTxtBoxA.Visible = false;

            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            oChkA.Visible = true;

            oTxtBoxB.HeaderText = "DyeBatch No";
            oTxtBoxB.ValueType = typeof(string);
            oTxtBoxB.ReadOnly = true;

            oTxtBoxC.HeaderText = "DyeBatch Date";
            oTxtBoxC.ValueType = typeof(DateTime);
            oTxtBoxC.ReadOnly = true;

            oTxtBoxD.HeaderText = "DyeBatch Required Date";
            oTxtBoxD.ValueType = typeof(DateTime);
            oTxtBoxD.ReadOnly = true;

            oTxtBoxE.HeaderText = "DyeBatch Weight";
            oTxtBoxE.ValueType = typeof(decimal);
            oTxtBoxE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            oTxtBoxE.ReadOnly = true;

            oTxtBoxF.HeaderText = "Notes";
            oTxtBoxF.ValueType = typeof(string);
            oTxtBoxF.ReadOnly = true;

            oTxtBoxG.HeaderText = "Closed";
            oTxtBoxG.ValueType = typeof(string);
            oTxtBoxG.ReadOnly = true;

            oTxtBoxH.HeaderText = "Transfer";
            oTxtBoxH.ValueType = typeof(string);
            oTxtBoxH.ReadOnly = true;

            oTxtBoxJ.HeaderText = "Allocated";
            oTxtBoxJ.ValueType = typeof(string);
            oTxtBoxJ.ReadOnly = true;

            oTxtBoxK.HeaderText = "Dyeing";
            oTxtBoxK.ValueType = typeof(string);
            oTxtBoxK.ReadOnly = true;

            oTxtBoxL.HeaderText = "Drying";
            oTxtBoxL.ValueType = typeof(string);
            oTxtBoxL.ReadOnly = true;

            oTxtBoxM.HeaderText = "Compacting";
            oTxtBoxM.ValueType = typeof(string);
            oTxtBoxM.ReadOnly = true;

            oTxtBoxN.HeaderText = "Out of Process";
            oTxtBoxN.ValueType = typeof(string);
            oTxtBoxN.ReadOnly = true;

            oTxtBoxP.HeaderText = "Quality";
            oTxtBoxP.ValueType = typeof(string);
            oTxtBoxP.ReadOnly = true;

            oTxtBoxQ.HeaderText = "Colour";
            oTxtBoxQ.ValueType = typeof(string);
            oTxtBoxQ.ReadOnly = true;

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns.Add(oTxtBoxC);
            dataGridView1.Columns.Add(oTxtBoxD);
            dataGridView1.Columns.Add(oTxtBoxE);
            dataGridView1.Columns.Add(oTxtBoxF);
            dataGridView1.Columns.Add(oTxtBoxG);
            dataGridView1.Columns.Add(oTxtBoxH);
            dataGridView1.Columns.Add(oTxtBoxJ);
            dataGridView1.Columns.Add(oTxtBoxK);
            dataGridView1.Columns.Add(oTxtBoxL);
            dataGridView1.Columns.Add(oTxtBoxM);
            dataGridView1.Columns.Add(oTxtBoxN);
            dataGridView1.Columns.Add(oTxtBoxP);
            dataGridView1.Columns.Add(oTxtBoxQ);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            if (!ViewData)
                dataGridView1.Visible = false;

            this.comboColour.CheckStateChanged += new System.EventHandler(this.comboColour_CheckStateChanged);
            this.comboCustomers.CheckStateChanged += new EventHandler(this.comboCustomers_CheckStateChanged);
            this.comboQuality.CheckStateChanged += new System.EventHandler(this.comboQuality_CheckStateChanged);
        }

        private void frmViewDyeBatches_Load(object sender, EventArgs e)
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
            }
            FormLoaded = true;
        }

        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void comboColour_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
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
        private void comboCustomers_CheckStateChanged(object sender, EventArgs e)
        {

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
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

            if (sender is DyeHouse.CheckComboBoxItem && FormLoaded)
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
                DyeParameters.FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DyeParameters.ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                DyeParameters.ToDate = DyeParameters.ToDate.AddHours(23);

                if (ViewData)
                {
                    dataGridView1.Rows.Clear();

                    var DyeBatches = repo.SelectViewDyeBatches(DyeParameters);

                    using (var context = new TTI2Entities())
                    {
                        foreach (var DyeBatch in DyeBatches)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = DyeBatch.DYEB_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = DyeBatch.DYEB_BatchNo;
                            dataGridView1.Rows[index].Cells[3].Value = DyeBatch.DYEB_BatchDate;
                            dataGridView1.Rows[index].Cells[4].Value = DyeBatch.DYEB_RequiredDate;
                            dataGridView1.Rows[index].Cells[5].Value = Math.Round(DyeBatch.DYEB_BatchKG, 2);
                            dataGridView1.Rows[index].Cells[6].Value = DyeBatch.DYEB_Notes;

                            var Status = "No";
                            if (DyeBatch.DYEB_Closed)
                                Status = "Yes";
                            dataGridView1.Rows[index].Cells[7].Value = Status;

                            Status = "No";
                            if (DyeBatch.DYEB_Transfered)
                                Status = "Yes";
                            dataGridView1.Rows[index].Cells[8].Value = Status;

                            Status = "No";
                            if (DyeBatch.DYEB_Allocated)
                                Status = "Yes";
                            dataGridView1.Rows[index].Cells[9].Value = Status;

                            Status = "No";
                            if (DyeBatch.DYEB_Stage1)
                                Status = "Yes";
                            dataGridView1.Rows[index].Cells[10].Value = Status;

                            Status = "No";
                            if (DyeBatch.DYEB_Stage2)
                                Status = "Yes";
                            dataGridView1.Rows[index].Cells[11].Value = Status;

                            Status = "No";
                            if (DyeBatch.DYEB_Stage3)
                                Status = "Yes";
                            dataGridView1.Rows[index].Cells[12].Value = Status;

                            Status = "No";
                            if (DyeBatch.DYEB_OutProcess)
                                Status = "Yes";
                            dataGridView1.Rows[index].Cells[13].Value = Status;

                            var Pk = DyeBatch.DYEB_Greige_FK;
                            dataGridView1.Rows[index].Cells[14].Value = context.TLADM_Griege.Find(Pk).TLGreige_Description;

                            Pk = DyeBatch.DYEB_Colour_FK;
                            dataGridView1.Rows[index].Cells[15].Value = context.TLADM_Colours.Find(Pk).Col_Display;
                        }
                    }
                    comboColour.Items.Clear();
                    comboCustomers.Items.Clear();
                    comboQuality.Items.Clear();
                }
                else
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(39, DyeParameters);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
            }
            frmViewDyeBatches_Load(this, null);
       }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                bool isChecked = (bool)oDgv[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                if (isChecked)
                {
                    using (var context = new TTI2Entities())
                    {
                        var CurrentRow = oDgv.CurrentRow;

                        var Pk = (int)CurrentRow.Cells[0].Value;
                  
                        frmDyeViewReport vRep = new frmDyeViewReport(4, Pk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);

                        DialogResult Result = MessageBox.Show("Would you like to print the transfer tickets", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (Result == DialogResult.Yes)
                        {
                            vRep = new frmDyeViewReport(5, Pk);
                            h = Screen.PrimaryScreen.WorkingArea.Height;
                            w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
                        }
                    }

                    var SingleRow = (  from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                        where (bool)Rows.Cells[1].Value == true
                                        select Rows).FirstOrDefault();

                    if (SingleRow != null)
                    {
                         SingleRow.Cells[1].Value = false; 
                    }
                }
            }
        }

        private void comboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void comboQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void comboColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }
      
    }
}

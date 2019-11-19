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

namespace Spinning
{
   
    public partial class frmDateSelectselection : Form
    {
        int _RepNo;

        public frmDateSelectselection(int RepNo)
        {
            InitializeComponent();
            _RepNo = RepNo;
            Setup();
        }

        void Setup()
        {
            dateTimePicker1.Visible = false;
            label1.Visible = false;

            groupBox1.Visible = false;

            using (var context = new TTI2Entities())
            {
                cmbBoxSupplier.DataSource = context.TLADM_Cotton.OrderBy(x => x.Cotton_Description).ToList();
                cmbBoxSupplier.ValueMember = "Cotton_Pk";
                cmbBoxSupplier.DisplayMember = "Cotton_Description";
            }

            if (_RepNo == 1)
            {
                this.Text = "Raw Cotton Movement Report";
                dateTimePicker1.Format = DateTimePickerFormat.Short;
                dateTimePicker1.Visible = true;
                label1.Visible = true;
            }
            else if (_RepNo == 2)
            {
                this.Text = "Cotton Contract Summary Details (KG)";
                groupBox1.Visible = true;
                cmbBoxSupplier.Visible = false;
                dateTimePicker2.Visible = false;

            }
            else if (_RepNo == 3)
            {
                this.Text = "WIP Spinning Movement Report ";
                groupBox1.Visible = false;
                label1.Visible = true;
                cmbBoxSupplier.Visible = false;
                dateTimePicker1.Visible = true;

            }

            LotsByTruck.Enabled = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                if ( _RepNo == 1)
                {
                    frmViewReport vRep = new frmViewReport(8, dateTimePicker1.Value);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                else if (_RepNo == 2)
                {
                    ReportOptions ropt = new ReportOptions();
                    if (PerSupplier.Checked)
                    {
                        ropt.SelectionOption = 1;
                        var selected = (TLADM_Cotton)cmbBoxSupplier.SelectedItem;
                        if (selected != null)
                        {
                            ropt.Supplier_FK = selected.Cotton_Pk;
                            ropt.SupplierDetail = selected.Cotton_Description;
                        }

                    }
                    else if (AllSuppliers.Checked)
                        ropt.SelectionOption = 2;
                    else if (ContractStartDate.Checked)
                    {
                        ropt.SelectionOption = 3;
                        ropt.DateSelected = Convert.ToDateTime(dateTimePicker2.Value.ToShortDateString());
                    }
                    else if (ContactsBaleStore.Checked)
                        ropt.SelectionOption = 4;
                    else if (LotsByTruck.Checked)
                    {
                        MessageBox.Show("Option currently still under development" + Environment.NewLine + " Please consult the system developer"); 
                        return;
                    }

                    if (ropt.SelectionOption > 0)
                    {
                        frmViewReport vRep = new frmViewReport(6, ropt);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                    }
                    else
                    {
                        MessageBox.Show("Please select the appropriate option");
                    }


                }
                else if (_RepNo == 3)
                {
                    frmViewReport vRep = new frmViewReport(17, dateTimePicker1.Value);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);

                }
            }
        }

        private void PerSupplier_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad.Checked)
               cmbBoxSupplier.Visible = true;
                
           else
                cmbBoxSupplier.Visible = false;
        }

        private void ContractStartDate_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad.Checked)
                dateTimePicker2.Visible = true;
            else
                dateTimePicker2.Visible = false;
        }

    }

    
}

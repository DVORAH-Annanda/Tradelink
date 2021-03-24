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

namespace CustomerServices
{
    public partial class frmReprintWareHouse : Form
    {
        bool FormLoaded;

        public frmReprintWareHouse()
        {
            InitializeComponent();
        }

        private void frmReprintWareHouse_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboCmt.DataSource = context.TLADM_Departments.Where(x => x.Dep_IsCMT).ToList();
                cmboCmt.ValueMember = "Dep_Id";
                cmboCmt.DisplayMember = "Dep_Description";
                cmboCmt.SelectedValue = -1;
            }

            cmboPreviousDelNotes.DataSource = null;


            FormLoaded = true;
        }

        private void cmboCmt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(FormLoaded)
            {
                var SelectedItem = (TLADM_Departments)cmboCmt.SelectedItem;

                if (SelectedItem != null)
                {
                    cmboPreviousDelNotes.Items.Clear();

                    using (var context = new TTI2Entities())
                    {
                        cmboPreviousDelNotes.DataSource = context.TLCSV_BoxSelected.Where(x => x.TLCSV_Despatched && x.TLCSV_From_FK == SelectedItem.Dep_Id).ToList();
                        cmboPreviousDelNotes.ValueMember = "TLCSV_Pk";
                        cmboPreviousDelNotes.DisplayMember = "TLCSV_PLDetails";
                        cmboPreviousDelNotes.SelectedValue = -1;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                var SelectedDept = (TLADM_Departments)cmboCmt.SelectedItem;
                if(SelectedDept == null)
                {
                    MessageBox.Show("Please select a CMT to process");
                    return;
                }

                if(cmboPreviousDelNotes.Items.Count == 0)
                {
                    MessageBox.Show("Please select a CMT to process");
                    return;
                }

                var SelectedDelNote = (TLCSV_BoxSelected)cmboPreviousDelNotes.SelectedItem;
                if(SelectedDelNote == null)
                {
                    MessageBox.Show("Please select a Delivery note to process");
                    return;

                }

                frmCSViewRep vRep = new frmCSViewRep(2, SelectedDelNote.TLCSV_Pk);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
                FormLoaded = false;
                frmReprintWareHouse_Load(this, null);
                FormLoaded = true;

            }
        }
    }
}

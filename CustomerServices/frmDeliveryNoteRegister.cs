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
    public partial class frmDeliveryNoteRegister : Form
    {
        bool _DeliveryNote;

        public frmDeliveryNoteRegister(bool DeliveryNote)
        {
            InitializeComponent();
            _DeliveryNote = DeliveryNote;
            if (DeliveryNote)
                this.Text = "Register of Delivery Notes";
            else
                this.Text = "Register of Picking Lists";
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                CSVServices svces = new CSVServices();
                
                svces.fromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                svces.toDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                svces.toDate = svces.toDate.AddHours(23);

                if (_DeliveryNote)
                    svces.DeliveryNote = _DeliveryNote;

                frmCSViewRep vRep = new frmCSViewRep(15, svces);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

            }
        }
    }
}

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
    public partial class frmSelQaReport : Form
    {
        int _RepNo;
 
        public frmSelQaReport(int RepNo)
        {
            InitializeComponent();

            _RepNo = RepNo;

        }

        private void frmSelQaReport_Load(object sender, EventArgs e)
        {
            if (_RepNo == 1)
                this.Text = "Berriebi Check Results";
            else if (_RepNo == 2)
                this.Text = "QA Results";
            else if (_RepNo == 3)
                this.Text = "Trims recorded on the cut";
            else
                this.Text = "Fleece Cuffs / Waistbands"; 
        }
    }
}

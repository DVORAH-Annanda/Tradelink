using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DyeHouse
{
    public partial class frmShowQtys : Form
    {
        DataTable _dt;
        public frmShowQtys(DataTable dt)
        {
            InitializeComponent();
            _dt = dt;

        }

        private void frmShowQtys_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dt;
        }
    }
}

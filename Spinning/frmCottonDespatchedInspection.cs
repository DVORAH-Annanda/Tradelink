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
    public partial class frmCottonDespatchedInspection : Form
    {
        bool FormLoaded;
        Util core;
        DataTable dt;
        DataColumn col;
        BindingSource BindingSrc;

        public frmCottonDespatchedInspection()
        {
            InitializeComponent();
            
            core = new Util();
            BindingSrc = new BindingSource();
            dt = new DataTable();
            col = new DataColumn();

            BindingSrc.DataSource = dt;
            dataGridView1.DataSource = BindingSrc;
        }

        private void frmCottonDespatchedInspection_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {

            }
            FormLoaded = true;
        }
    }
}

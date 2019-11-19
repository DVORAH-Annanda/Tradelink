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
    public partial class frmRemedies : Form
    {
        int _DyeBatch;
        bool formLoaded;

        Util core;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn oCmbA = new DataGridViewComboBoxColumn();

        public frmRemedies(int DyeBatch)
        {
            InitializeComponent();
            _DyeBatch = DyeBatch;

            core = new Util();

            dataGridView1.AutoGenerateColumns = false;

            oCmbA.HeaderText = "Cosumable Items";
            oCmbA.Width = 135;
            dataGridView1.Columns.Add(oCmbA);

            oTxtA.HeaderText = "Quantity";
            oTxtA.ValueType = typeof(decimal);
            dataGridView1.Columns.Add(oTxtA);


            SetUp();
        }

        void SetUp()
        {
            formLoaded = false;

            using (var context = new TTI2Entities())
            {
                var DB = context.TLDYE_DyeBatch.Find(_DyeBatch);
                if (DB != null)
                {
                    txtDyeBatchNo.Text = DB.DYEB_BatchNo;
                }

                oCmbA.DataSource = context.TLADM_ConsumablesDC.OrderBy(x => x.ConsDC_Code).ToList();
                oCmbA.DisplayMember = "ConsDC_Description";
                oCmbA.ValueMember = "ConsDC_Pk";


            }

            formLoaded = true;
            
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var oDgv = sender as DataGridView;

            if (oDgv.CurrentCell.ColumnIndex == 1)
            {
                e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
        }
    }
}

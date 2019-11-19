using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace TTI2_WF
{
    public partial class FrmTest : Form
    {
        
        private DataGridViewTextBoxColumn GName;
        private DataGridViewTextBoxColumn AltGName;
        private DataGridViewButtonColumn bstyle;
      
             
        public FrmTest()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {

            GName = new DataGridViewTextBoxColumn(); //0
            GName.HeaderText = "Name";
            GName.DataPropertyName = "NameIndex";
            GName.Width = 150;

            AltGName = new DataGridViewTextBoxColumn(); //1
            AltGName.HeaderText = "Alternate Name";
            AltGName.DataPropertyName = "AltNameIndex";
            AltGName.Width = 150;

            bstyle = new DataGridViewButtonColumn();
            bstyle.HeaderText = "style";
            bstyle.Width = 50;
         

            dataGridView1.Columns.Add(GName);      //0
            dataGridView1.Columns.Add(AltGName);   //1
            dataGridView1.Columns.Add(bstyle);     // 2
        }

      

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewButtonCell)
            {
                if (e.ColumnIndex == 2)
                {
                    Util core = new Util();
                    int stylePW = Convert.ToInt32(oDgv.CurrentCell.Value);


                    IList<int> nrwpw = core.ExtrapNumber(stylePW, 8);

                  
                }
            }
        }
    }
}

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
    public partial class Test : Form
    {
        private Repository repo;

        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            repo = new Repository();

            var allColors = repo.LoadColours().OrderBy(x=>x.Col_Display);

            foreach (var color in allColors)
            {
                checkedListBox1.Items.Add(new object[] { color.Col_Id, color.Col_Display, 0 });
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
           var parms = new ShirtQueryParameters();

           if (checkedListBox1.CheckedItems.Count != 0)
           {
               // If so, loop through all checked items and print results.
               string s = "";
                                       
               
               MessageBox.Show(s);
           }

           
        }
    }
}

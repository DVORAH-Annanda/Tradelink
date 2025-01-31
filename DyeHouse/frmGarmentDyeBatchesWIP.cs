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
    public partial class frmGarmentDyeBatchesWIP : Form
    {
        public frmGarmentDyeBatchesWIP()
        {
            InitializeComponent();

            this.cmboStyle.CheckStateChanged += new System.EventHandler(this.cmboStyles_CheckStateChanged);
            this.cmboColour.CheckStateChanged += new System.EventHandler(this.cmboColours_CheckStateChanged);
        }
    }
}

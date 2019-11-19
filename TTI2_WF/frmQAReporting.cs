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

namespace TTI2_WF
{
    public partial class frmQAReporting : Form
    {
        bool FormLoaded;

        public frmQAReporting()
        {
            InitializeComponent();
        }

        private void frmQAReporting_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            
            chkCMT.Checked = false;
            chkCutting.Checked = false;
            chkDyeing.Checked = false;
            chkKnitting.Checked = false;
            chkSpinning.Checked = false;

            FormLoaded = true;
        }

       
        private void chkSpinning_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null && oChk.Checked && FormLoaded)
            {
                try
                {

                    frmQASpinningSel CuttingSel = new frmQASpinningSel();
                    CuttingSel.ShowDialog(this);

                    frmQAReporting_Load(this, null);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void chkDyeing_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null && oChk.Checked && FormLoaded)
            {
                try
                {
                    frmQADyeSel CuttingSel = new frmQADyeSel();
                    CuttingSel.ShowDialog(this);

                    frmQAReporting_Load(this, null);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void chkCutting_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null && oChk.Checked && FormLoaded)
            {
                try
                {
                    frmQACuttingSel CuttingSel = new frmQACuttingSel();
                    CuttingSel.ShowDialog(this);

                    frmQAReporting_Load(this, null);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void chkKnitting_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null && oChk.Checked && FormLoaded)
            {
                try
                {
                    frmQAKnittingSel CuttingSel = new frmQAKnittingSel();
                    CuttingSel.ShowDialog(this);

                    frmQAReporting_Load(this, null);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void chkCMT_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null && oChk.Checked && FormLoaded)
            {
                try
                {
                    frmQACMTSel CuttingSel = new frmQACMTSel();
                    CuttingSel.ShowDialog(this);

                    frmQAReporting_Load(this, null);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        
    }
}

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
    public partial class frmRepackCenterStatus : Form
    {
        Boolean FormLoaded;
        CustomerServices.CustomerServicesParameters QueryParms;
        CustomerServices.Repository Repo;
        Util core;

        public frmRepackCenterStatus()
        {
            InitializeComponent();

            this.cmboRepackConfig.CheckStateChanged += new System.EventHandler(this.cmboRepackConfig_CheckStateChanged);
            this.Repo = new CustomerServices.Repository();

            core = new Util();
        }

        private void frmRepackCenterStatus_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            QueryParms = new CustomerServicesParameters();
            IList<TLCSV_RePackConfig> RePack = new List<TLCSV_RePackConfig>();
 
            using (var context = new TTI2Entities())
            {
                var ConFigs = context.TLCSV_RePackConfig.GroupBy(x=>x.PORConfig_BoxNumber_Key);
                foreach (var ConFig in ConFigs)
                {
                    var ConFigPk = ConFig.FirstOrDefault().PORConfig_BoxNumber_Key;
                    var ConFigDesc = ConFig.FirstOrDefault().PORConfig_BoxNumber_Key.ToString();
                    cmboRepackConfig.Items.Add(new CustomerServices.CheckComboBoxItem(ConFigPk, ConFigDesc,false));
                }
            }
            FormLoaded = true;
        }

        private void cmboRepackConfig_CheckStateChanged(object sender, EventArgs e)
        {
            if (sender is CustomerServices.CheckComboBoxItem && FormLoaded)
            {
                CustomerServices.CheckComboBoxItem item = (CustomerServices.CheckComboBoxItem)sender;
                    if (item.CheckState)
                    {
                        QueryParms.RePackConfigs.Add(Repo.LoadRePackConfig(item._Pk));

                    }
                    else
                    {
                        var value = QueryParms.RePackConfigs.Find(it => it.PORConfig_BoxNumber_Key == item._Pk);
                        if (value != null)
                            QueryParms.RePackConfigs.Remove(value);

                    }
                
            }
        }

        private void cmboRepackConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && !oCmbo.DroppedDown)
                oCmbo.DroppedDown = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                var csvService = new CSVServices();

                var vRep = new frmCSViewRep(23, QueryParms, csvService);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
                if (vRep != null)
                {
                    vRep.Close();
                    vRep.Dispose();
                }
            }
        }
    }
}

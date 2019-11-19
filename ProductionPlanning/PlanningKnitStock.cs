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

namespace ProductionPlanning
{
    public partial class PlanningKnitStock : Form
    {
        ProdQueryParameters QueryParms;
        PPSRepository repo;

        bool formloaded;

        public PlanningKnitStock()
        {
            InitializeComponent();
        }

        private void PlanningKnitStock_Load(object sender, EventArgs e)
        {
            formloaded = false;

            QueryParms = new ProdQueryParameters();
            repo = new PPSRepository();

            using (var context = new TTI2Entities())
            {
                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboGreige.DataSource = context.TLADM_Griege.ToList();
                    cmboGreige.ValueMember = "TLGreige_Id";
                    cmboGreige.DisplayMember = "TLGreige_Description";
                    cmboGreige.SelectedIndex = -1;

                    cmboGreigeQuality.DataSource = context.TLADM_GreigeQuality.ToList();
                    cmboGreigeQuality.ValueMember = "GQ_Pk";
                    cmboGreigeQuality.DisplayMember = "GQ_Description";
                    cmboGreigeQuality.SelectedIndex = -1;

                    cmboFabWeight.DataSource = context.TLADM_FabricWeight.ToList();
                    cmboFabWeight.ValueMember = "FWW_Id";
                    cmboFabWeight.DisplayMember = "FWW_Description";
                    cmboFabWeight.SelectedIndex = -1;

                    cmboFabWidth.DataSource = context.TLADM_FabWidth.ToList();
                    cmboFabWidth.ValueMember = "FW_Id";
                    cmboFabWidth.DisplayMember = "FW_description";
                    cmboFabWidth.SelectedIndex = -1;

                    cmboStore.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Dept.Dep_Id && !x.WhStore_WhseOrStore).ToList();
                    cmboStore.ValueMember = "WhStore_Id";
                    cmboStore.DisplayMember = "WhStore_Description";
                    cmboStore.SelectedIndex = -1;
                 
                }

                formloaded = true;
            }
        }

        private void cmboGreige_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_Griege)oCmbo.SelectedItem;
                if (selected != null)
                {
                    var value = QueryParms.Greiges.Find(item => item.TLGreige_Id == selected.TLGreige_Id);
                    if (value == null)
                        QueryParms.Greiges.Add(repo.LoadGriege(selected.TLGreige_Id));
                    else
                    {
                        MessageBox.Show("This item has already been selected");
                        return;
                    }
                }
            }

        }

        private void cmboGreigeQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_GreigeQuality)oCmbo.SelectedItem;
                if (selected != null)
                {
                    var value = QueryParms.GreigeQualities.Find(item => item.GQ_Pk == selected.GQ_Pk);
                    if (value == null)
                        QueryParms.GreigeQualities.Add(repo.LoadGreigeQuality(selected.GQ_Pk));
                    else
                    {
                        MessageBox.Show("This item has already been selected");
                        return;
                    }
                }
            }
        }

        private void cmboFabWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_FabricWeight)oCmbo.SelectedItem;
                if (selected != null)
                {
                    var value = QueryParms.FabricWeights.Find(item => item.FWW_Id == selected.FWW_Id);
                    if (value == null)
                        QueryParms.FabricWeights.Add(repo.LoadWeight(selected.FWW_Id));
                    else
                    {
                        MessageBox.Show("This item has already been selected");
                        return;
                    }
                }
            }
        }

        private void cmboFabWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_FabWidth)oCmbo.SelectedItem;
                if (selected != null)
                {
                    var value = QueryParms.FabricWidths.Find(item => item.FW_Id == selected.FW_Id);
                    if (value == null)
                        QueryParms.FabricWidths.Add(repo.LoadWidth(selected.FW_Id));
                    else
                    {
                        MessageBox.Show("This item has already been selected");
                        return;
                    }
                }
            }
        }

        private void cmboStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;

            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_WhseStore)oCmbo.SelectedItem;
                if (selected != null)
                {
                    var value = QueryParms.GreigeStores.Find(item => item.WhStore_Id == selected.WhStore_Id);
                    if (value == null)
                        QueryParms.GreigeStores.Add(repo.LoadStore(selected.WhStore_Id));
                    else
                    {
                        MessageBox.Show("This item has already been selected");
                        return;
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBth = sender as Button;
            if (oBth != null && formloaded)
            {
                frmPPSViewRep vRep = new frmPPSViewRep(3, QueryParms);
                vRep.ShowDialog();

                QueryParms = new ProdQueryParameters();
                cmboGreige.SelectedIndex = -1;
                cmboGreigeQuality.SelectedIndex = -1;
                cmboFabWeight.SelectedIndex = -1;
                cmboFabWidth.SelectedIndex = -1;
                cmboStore.SelectedIndex = -1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Administration;
using Knitting;
using Utilities;
using Spinning;
using System.Deployment.Application;
using DyeHouse;
using Cutting;
using CMT;
using CustomerServices;
using ProductionPlanning;
using ExecutiveReporting;
using System.Security.Principal;
using System.Security.Permissions;
using System.Threading;
using Security;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using EntityFramework.Extensions; 

namespace TTI2_WF
{

    public partial class frmTLMain : Form
    {
        public static int CompayNoSelected;
        UserDetails ud;

        Util core;

        WindowsIdentity currentIdentity;
        WindowsPrincipal currentPrincipal;
        int SecDeptPk = 0;

        List<TLSEC_UserSections> UserSections = null;
        List<TLSEC_Sections> AllSections = null;

        public frmTLMain()
        {
            InitializeComponent();


            /*
            // Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
            //                .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
            //                .ToString();
            */


            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                this.Text = string.Format("Your Application Name v{0}.{1}", ver.Major, ver.Revision);
            }

        }

        private void frmTLMain_Load(object sender, EventArgs e)
        {
            this.AutoSize = true;
            this.WindowState = FormWindowState.Normal;
            core = new Util();
            //========================================================================
            // This only needs to updated when either a new Department ie "CMT" or "Cutting" is added
            // or a new sub menu is added to either "CMT" or "Cutting" as an example
            // remember to point the connection string to the "Live" database in the App.config file  
            // UpdateSecurity(); // See below
            //=======================================================
        }


        private void UpdateSecurity()
        {
            List<ToolStripItem> allItems = new List<ToolStripItem>();
            Util core = new Util();

            foreach (ToolStripMenuItem toolItem in menuStrip1.Items)
            {
                int IndexPos = menuStrip1.Items.IndexOf(toolItem);
                if (IndexPos >= 0)
                {
                    SecDeptPk = UpDateDepts(toolItem.Text);

                    allItems.Add(toolItem);
                    //----------------------------------------------
                    //add sub items
                    //-----------------------------------------------------------------
                    allItems.AddRange(GetItems(toolItem));
                    using (var contextx = new TTI2Entities())
                    {
                        foreach (ToolStripItem toolxItem in allItems)
                        {
                            if (toolItem is ToolStripMenuItem)
                            {
                                ToolStripMenuItem tsmi = (toolxItem as ToolStripMenuItem);
                                if (!tsmi.HasDropDownItems)
                                {
                                    TLSEC_Sections secs = new TLSEC_Sections();
                                    secs = contextx.TLSEC_Sections.Where(x => x.TLSECSect_Name == tsmi.Name).FirstOrDefault();
                                    if (secs == null)
                                    {
                                        secs = new TLSEC_Sections();
                                        secs.TLSECSect_Department_FK = SecDeptPk;
                                        secs.TLSECSect_Description = tsmi.Text;
                                        secs.TLSECSect_Name = tsmi.Name;

                                        contextx.TLSEC_Sections.Add(secs);
                                    }
                                    else
                                    {
                                        if (!string.Equals(secs.TLSECSect_Description, tsmi.Text))
                                        {
                                            secs.TLSECSect_Description = tsmi.Text;
                                        }
                                    }
                                }
                                else
                                {
                                    var secs = contextx.TLSEC_Sections.Where(x => x.TLSECSect_Name == tsmi.Name).FirstOrDefault();
                                    if (secs == null)
                                    {
                                        secs = new TLSEC_Sections();
                                        secs.TLSECSect_Department_FK = SecDeptPk;
                                        secs.TLSECSect_Description = tsmi.Text;
                                        secs.TLSECSect_Name = tsmi.Name;

                                        contextx.TLSEC_Sections.Add(secs);

                                    }
                                    else
                                    {
                                        if (!string.Equals(secs.TLSECSect_Description, tsmi.Text))
                                        {
                                            secs.TLSECSect_Description = tsmi.Text;
                                        }
                                    }

                                }
                            }
                            else if (toolxItem is ToolStripSeparator)
                            {
                                ToolStripSeparator tss = (toolxItem as ToolStripSeparator);
                            }
                        }
                        try
                        {
                            contextx.SaveChanges();
                        }
                        catch (System.Data.Entity.Validation.DbEntityValidationException en)
                        {
                            foreach (var eve in en.EntityValidationErrors)
                            {
                                MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    allItems = new List<ToolStripItem>();
                }
            }
        }

        private int UpDateDepts(string Name)
        {
            int Pk = 0;
            TLSEC_Departments secDept = new TLSEC_Departments();
            using (var context = new TTI2Entities())
            {
                secDept = context.TLSEC_Departments.Where(x => x.TLSECDT_Description == Name).FirstOrDefault();
                if (secDept == null)
                {
                    secDept = new TLSEC_Departments();
                    MessageBox.Show(Name, Name.Length.ToString());
                    secDept.TLSECDT_Description = Name;
                    context.TLSEC_Departments.Add(secDept);

                    try
                    {
                        context.SaveChanges();
                        Pk = secDept.TLSECDT_Pk;
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException en)
                    {
                        foreach (var eve in en.EntityValidationErrors)
                        {
                            MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                            foreach (var ve in eve.ValidationErrors)
                            {
                                MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessages = new StringBuilder();
                        do
                        {
                            exceptionMessages.Append(ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);
                        MessageBox.Show(exceptionMessages.ToString());
                        this.Close();
                    }
                }
                else
                    Pk = secDept.TLSECDT_Pk;
            }

            return Pk;
        }


        private void frmTLMain_Shown(object sender, EventArgs e)
        {
            frmSecurity frmLogin = new frmSecurity();
            frmLogin.StartPosition = FormStartPosition.CenterParent;
            frmLogin.ShowDialog(this);

            if (!frmLogin.IsAuthorised)
            {
                this.Close();
                return;
            }

            ud = new UserDetails();
            ud._IsAuthorised = frmLogin.IsAuthorised;
            ud._SuperUser = frmLogin.IsSuperUser;
            ud._UserName = frmLogin.UserName;
            ud._UserPk = frmLogin.UserPk;
            ud._NotAuthorisedMessage = "You are not authorised to use this function";
            ud._External = frmLogin.IsExternal;
            ud._QAFunction = frmLogin.QAFunction;
            ud._DownSizeAuthority = frmLogin.DownSizeAllowed;
            this.Text += " Welcome - " + ud._UserName;

            if (!ud._SuperUser)
                ManageViews(ud);

            //----------------------------------------------------------------------------
            currentIdentity = WindowsIdentity.GetCurrent();
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            currentPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
            //--------------------------------------------------------------------------------
            if (ud._SuperUser)
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    DialogResult res = MessageBox.Show("Would you like to view the KPI's", "KPI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        ShowKPIs();
                    }
                }
            }
        }

        private void frmTLMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (ud._SuperUser && e.Button.ToString() == "Right")
            {
                DialogResult res = MessageBox.Show("Would you like to view the KPI's", "KPI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    ShowKPIs();
                }
            }
        }

        private void ManageViews(UserDetails ud)
        {
            SECRepository repo = new SECRepository();
            SECQueryParameters QueryParms = new SECQueryParameters();
            IList<TLSEC_Sections> Sections = null;
            IList<TLSEC_UserSections> UserSections = null;

            using (var context = new TTI2Entities())
            {
                Sections = context.TLSEC_Sections.ToList();
                UserSections = context.TLSEC_UserSections.Where(x => x.TLSECDEP_User_FK == ud._UserPk).ToList();
            }
            foreach (ToolStripMenuItem toolItem in menuStrip1.Items)
            {
                if (core.GetUserAuthorisation(ud, toolItem.Name))
                {
                    if (toolItem is ToolStripMenuItem)
                    {
                        TreeNode tn = new TreeNode(toolItem.Text);
                        bool Test = getChildNodes(toolItem, tn, Sections, UserSections);
                    }
                }
                else
                {
                    toolItem.Enabled = false;
                }

            }
        }

        private bool getChildNodes(ToolStripDropDownItem mi, TreeNode tn, IList<TLSEC_Sections> Sect, IList<TLSEC_UserSections> UserSect)
        {
            var VSible = false;
            var OrigVisible = false;
            int a = 0;
            foreach (ToolStripMenuItem item in mi.DropDownItems)
            {
                if (item is ToolStripItem)
                {
                    TreeNode node = new TreeNode(((ToolStripDropDownItem)item).Text);
                    tn.Nodes.Add(node);
                 
                    var Existing = Sect.FirstOrDefault(s => s.TLSECSect_Name == item.Name);
                    if (Existing != null)
                    {
                        var Access = UserSect.FirstOrDefault(s => s.TLSECDEP_Section_FK == Existing.TLSECSect_Pk);
                        if (Access != null && Access.TLSECDEP_AccessGranted)
                        {
                            item.Visible = true;
                            OrigVisible = true;

                            if (item.HasDropDownItems)
                                item.Visible = getChildNodes(((ToolStripDropDownItem)item), node, Sect, UserSect);
                        }
                        else
                        {
                            item.Visible = false;
                        }
                    }
                    else
                        if (!ud._SuperUser)
                    {
                        item.Visible = false;
                    }
                }
            }

            VSible = OrigVisible;
            return VSible;
        }

        private IEnumerable<ToolStripItem> GetItems(ToolStripItem item)
        {
            if (item is ToolStripMenuItem)
            {
                foreach (ToolStripItem tsi in (item as ToolStripMenuItem).DropDownItems)
                {
                    if (tsi is ToolStripMenuItem)
                    {
                        if ((tsi as ToolStripMenuItem).HasDropDownItems)
                        {
                            foreach (ToolStripItem subItem in GetItems((tsi as ToolStripMenuItem)))
                                yield return subItem;
                        }
                        yield return (tsi as ToolStripMenuItem);
                    }
                    else if (tsi is ToolStripSeparator)
                    {
                        yield return (tsi as ToolStripSeparator);
                    }
                }
            }
            else if (item is ToolStripSeparator)
            {
                yield return (item as ToolStripSeparator);
            }
        }



        private void weightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    //-----------------------------------------------------------------------
                    // Fabric Weight Definition 
                    //-----------------------------------------------------------------------------------------
                    frmTLADMGardDef gd = new frmTLADMGardDef(13);
                    gd.ShowDialog();

                }
                catch (Exception ex)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }


            }
        }

        private void widthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Fabric Width Definition 
                //-----------------------------------------------------------------------------------------
                //Fabric Weight 
                //---------------------------------------------------------------------------------------------
                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(5);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void productTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Fabric Product / Type 
                //---------------------------------------------------------------------------------------------
                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(12);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void greigeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Greige
                //---------------------------------------------------------------------------
                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(4);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //Yarn
                //---------------------------------------------------------------------------
                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(3);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }

        }

        private void trimsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //Trims 
                //----------------------------------------------------------------
                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(7);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void stylesDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // The defining of styles 
                //-----------------------------------------------------------------------------------------
                try
                {
                    using (frmTLADMGardDef gd = new frmTLADMGardDef(1, CompayNoSelected))
                    {
                        DialogResult dr = gd.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void productRatingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMProductRating pr = new frmTLADMProductRating(CompayNoSelected);
                    pr.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //Sizes Definition
                //---------------------------------------------------------------------
                try
                {
                    DialogResult Res = MessageBox.Show("As we have reached the limit in sizes please dont add anymore new sizes", "Contact System developer for further information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmTLADMGardDef gd = new frmTLADMGardDef(8);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void coloursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //Colours 
                //-------------------------------------------------------------------------------
                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(2);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void auxColoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Aux Colours
                //-----------------------------------------------------------------------------------------
                try
                {
                    //frmTLADMGardDef gd = new frmTLADMGardDef(6);
                    //gd.ShowDialog();

                    MessageBox.Show("This facility is now withdrawn. Please see Colours above");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void labelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //--------------------------------------------------------------
                // Labels 
                //------------------------------------------------

                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(9);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void storeTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes ct = new frmTLADMCustomerTypes(3);
                    ct.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void productGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //Product Groups 
                //--------------------------------------------------------------------------------------
                try
                {
                    frmTLADMCustomerTypes ct = new frmTLADMCustomerTypes(2);
                    ct.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void nonStockCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //Non Stock Category 
                //--------------------------------------------------------------------------------------
                try
                {
                    frmTLADMCustomerTypes ct = new frmTLADMCustomerTypes(4);
                    ct.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void customerTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //-----------------------------------------------------------------------------
                // Customer Types 
                //--------------------------------------------------------------------------------------------------------
                try
                {
                    frmTLADMCustomerTypes ct = new frmTLADMCustomerTypes(1);
                    ct.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void consumableGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //-----------------------------------------------------------------
                // Consumable Groups 
                //---------------------------------------------------------------------
                try
                {
                    frmTLADM_QualityDefinition qd = new frmTLADM_QualityDefinition(3);
                    qd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //-------------------------------------------------------------------------
                // Department Definition
                //--------------------------------------------------------------------------------------------
                try
                {
                    frmTLADMDeptdef dept = new frmTLADMDeptdef();
                    dept.StartPosition = FormStartPosition.CenterParent;
                    dept.Width = 760;
                    dept.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void nonStockItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //---------------------------------------------
                // Non Stock Items 
                //------------------------------------------------------
                try
                {
                    frmTLADMNonStockItems nsti = new frmTLADMNonStockItems();
                    nsti.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cottonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //--------------------------------------------------------------------
                // Cotton 
                //-------------------------------------------------------------------
                try
                {
                    frmTLADM_Cotton cotton = new frmTLADM_Cotton();
                    cotton.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void warehousesAndStoresToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //---------------------------------------------------------------------------------
                // Warehouse and Stores 
                //-----------------------------------------------------------------------------------
                try
                {
                    frmTLADMWhseStores whstores = new frmTLADMWhseStores();
                    whstores.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void consumablesOtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //---------------------------------------------------------------------------
                // Consumables Other 
                //--------------------------------------------------------------------------------

                try
                {
                    frmTLADM_ConsumablesOther consother = new frmTLADM_ConsumablesOther();
                    consother.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void stockTakeFrequencyCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //---------------------------------------------------------------------------
                // Stock Take Frequency Codes 
                //------------------------------------------------------------------------------

                try
                {
                    frmTLADM_QualityDefinition qd = new frmTLADM_QualityDefinition(4);
                    qd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void consumablesDyesAndChemicalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //---------------------------------------------------------------------------
                // Consumables Dyes and Chemicals  
                //--------------------------------------------------------------------------------
                try
                {
                    frmTLADM_ConsumablesDC dyc = new frmTLADM_ConsumablesDC();
                    dyc.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void customerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //Updating of customer Details 
                //------------------------------------------------------------------
                try
                {
                    frmTLADM_Customers customers = new frmTLADM_Customers();
                    customers.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void machineOperatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADM_QualityDefinition qd = new frmTLADM_QualityDefinition(5);
                    
                    var myScreen = Screen.FromControl(this);
                    var mySecondScreen = Screen.AllScreens.FirstOrDefault(s => !s.Equals(myScreen)) ?? myScreen;
                    qd.Left = mySecondScreen.Bounds.Left;
                    qd.Top = mySecondScreen.Bounds.Top;
                    qd.StartPosition = FormStartPosition.Manual;
                    qd.Width = 900;
                    qd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void garmentDEfectCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADM_QualityDefinition qd = new frmTLADM_QualityDefinition(1);
                    qd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void garmentRejectReasonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADM_QualityDefinition qd = new frmTLADM_QualityDefinition(6);
                    qd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void supplierDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADM_Suppliers suppliers = new frmTLADM_Suppliers();
                    suppliers.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void productTypeGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADM_QualityDefinition qd = new frmTLADM_QualityDefinition(7);
                    qd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void machineDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADM_MachineDefinition machines = new frmTLADM_MachineDefinition();
                    machines.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }

        }

        private void finishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADM_QualityDefinition qd = new frmTLADM_QualityDefinition(8);
                    qd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void machineMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes ct = new frmTLADMCustomerTypes(5);
                    ct.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cottonOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                //Cotton Origins 
                //--------------------------------------------------------------------------------
                try
                {
                    frmTLADMCustomerTypes ct = new frmTLADMCustomerTypes(6);
                    ct.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigeQualityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(14);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void unitsOfMeasureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(7);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void definitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmFabricDyingStandards gd = new frmFabricDyingStandards();
                gd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
        }

        private void panelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(17);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void companyDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCompanyDetails compDet = new frmTLADMCompanyDetails();
                    compDet.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void Main_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void cottonAgentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCottonAgents ca = new frmTLADMCottonAgents();
                    ca.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void hauliersDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCottonHauliers ch = new frmCottonHauliers();
                    ch.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cottonDeliveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCottonDelivery CD = new frmCottonDelivery();
                    CD.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cottonReturnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCottonReturns cotReturns = new frmCottonReturns();
                    cotReturns.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cottonStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCottonAdjustments cotA = new frmCottonAdjustments();
                    cotA.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cottonStockSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCottonStockSales cotStockSales = new frmCottonStockSales();
                    cotStockSales.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cottonIssuesToProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCottonIssueToProd ci2Prod = new frmCottonIssueToProd();
                    ci2Prod.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnOrder yo = new frmYarnOrder();
                    yo.ShowDialog(this);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException en)
                {
                    foreach (var eve in en.EntityValidationErrors)
                    {
                        MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void palletLabelsStickersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnLabels yarnLabels = new frmYarnLabels();
                    yarnLabels.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnProduction yarnP = new frmYarnProduction();
                    yarnP.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void closeYarnOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCloseYarnOrder CloseYO = new frmCloseYarnOrder(true);
                    CloseYO.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void inspectionAndQAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDQControlReport DQCR = new frmDQControlReport();
                    DQCR.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void issueToRoductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnIssueToKnitting ITOKnitting = new frmYarnIssueToKnitting();
                    ITOKnitting.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void sellingOfYarnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnSales yarnSls = new frmYarnSales();
                    yarnSls.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void scrappingOfYarnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnScrapping yarnScr = new frmYarnScrapping();
                    yarnScr.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void yarnStockAdjustmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            try
            {
                frmYarnAdjustment yarnA = new frmYarnAdjustment(true);
                yarnA.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void qAConfirmationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCottonQA cotQA = new frmCottonQA();
                    cotQA.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void transactionTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                try
                {
                    frmTransactionType transType = new frmTransactionType();
                    transType.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cottonContractSummaryDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    /*
                    frmViewReport vRep = new frmViewReport(6);
                    vRep.ShowDialog(this);
                    */
                    frmDateSelectselection datesel = new frmDateSelectselection(2);
                    datesel.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void rawCottonStockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmViewReport vRep = new frmViewReport(7);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void rawCottonMovementReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDateSelectselection datesel = new frmDateSelectselection(1);
                    datesel.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnTransactionReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnTransactionsReports transRep = new frmYarnTransactionsReports();
                    transRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void nonStockItemsConsumptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmNSISelection nsiSel = new frmNSISelection(true);
                    nsiSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void capacityUtilisationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmNSISelection nsiSel = new frmNSISelection(false);
                    nsiSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnProdSel ysel = new frmYarnProdSel();
                    ysel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void departmentAreasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDepartmentAreas ysel = new frmDepartmentAreas();
                    ysel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void closingWIPStockRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmWIPClosingStock ysel = new frmWIPClosingStock();
                    ysel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void wIPSpinningMovementReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDateSelectselection datesel = new frmDateSelectselection(3);
                    datesel.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnStockOnHandByStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnStockOHSel YOH = new frmYarnStockOHSel();
                    YOH.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void commissionKnittingReceiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCommKnitting knit = new frmCommKnitting();
                    knit.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void receiptOfYarn3rdPartyPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnPurchased3rdParty thirdp = new frmYarnPurchased3rdParty();
                    thirdp.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnReturnedToSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnReturnToSupplier returned = new frmYarnReturnToSupplier(1);
                    returned.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void knittingOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                frmKnitOrder knitOrder = new frmKnitOrder();
                knitOrder.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             */
        }

        private void shiftDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmShiftDefinition shiftdef = new frmShiftDefinition();
                    shiftdef.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnStockAdjustmentsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnStockAdj yarnAdj = new frmYarnStockAdj();
                    yarnAdj.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void createAKnittingOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmKnitOrder knitOrder = new frmKnitOrder();
                    knitOrder.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void greigeProductionRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmGreigeRecordOfProd greigeProd = new frmGreigeRecordOfProd();
                    greigeProd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void closeAKnitOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCloseKnitOrder cko = new frmCloseKnitOrder();
                    cko.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void reconcileAKnitOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmReconKnitOrder reconKO = new frmReconKnitOrder();
                    reconKO.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigeInspectionResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmGreigeRecording GreigeRecording = new frmGreigeRecording();
                    GreigeRecording.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigeStockAdjustmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmGreigeAdjust GreigeAdj = new frmGreigeAdjust();
                    GreigeAdj.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void thirdPartyGreigeReceivedForCommissionDyeingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmGreigeReceived3P GreigeReceived = new frmGreigeReceived3P();
                    GreigeReceived.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void thirdPartyGreigeStockAdjustmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmGreigeReceived3PStockAdjustment Greige3rdPAdj = new frmGreigeReceived3PStockAdjustment();
                    Greige3rdPAdj.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigePlanningToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void yarnReturnToSupplierCommissionKnittingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnReturnToSupplier returned = new frmYarnReturnToSupplier(2);
                    returned.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void reinstateOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCloseYarnOrder CloseYO = new frmCloseYarnOrder(false);
                    CloseYO.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnWasteRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnWasteRecording YarnWaste = new frmYarnWasteRecording();
                    YarnWaste.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnWithdrawalNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnwithdrawl yarnWD = new frmYarnwithdrawl();
                    yarnWD.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnStockOnHandByStoreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    YarnReportOptions opts = new YarnReportOptions();
                    opts.reportChoice = 2;

                    frmKnitViewRep vRep = new frmKnitViewRep(16, opts);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnStockOnHandByStoreCommissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    YarnReportOptions opts = new YarnReportOptions();
                    opts.reportChoice = 1;

                    frmKnitViewRep vRep = new frmKnitViewRep(16, opts);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigeItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmKnitViewRep vRep = new frmKnitViewRep(15);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void knittingWIPGreigeProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmKnitViewRep vRep = new frmKnitViewRep(17);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void knitOrdersProcessLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmK05ReportSel ProcessLoss = new frmK05ReportSel();
                    ProcessLoss.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigeProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmK07ReportSel greigeP = new frmK07ReportSel(true);
                    greigeP.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigeKnittedQAResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmK08ReportSel QAResults = new frmK08ReportSel();
                    QAResults.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigeEfficiencyUtilisationForGreigeKnittedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmK09ReportSel effSel = new frmK09ReportSel();
                    effSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigeStockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmK10ReportSel repSel = new frmK10ReportSel(true);
                    repSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cGradeReportByPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmK12ReportSel repSel = new frmK12ReportSel();
                    repSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnStockOnHandByStoreOwnYarnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmKnitViewRep vRep = new frmKnitViewRep(20);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(1);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void customerByCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(2);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void categoriesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(3);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void supplierByCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(4);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void warehouseStoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(5);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void nonStockItemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(6);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void operatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(7);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void byDepartmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(8);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnOrderPendingProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmViewReport vRep = new frmViewReport(20);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void receipeDefinitiionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLDYEReceipe receipe = new frmTLDYEReceipe();
                    receipe.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }


        }

        private void deptProductionLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Production loss 
                //--------------------------------------------------------
                try
                {
                    frmTLADMGardDef gd = new frmTLADMGardDef(18);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {

                    frmDyeOrders gd = new frmDyeOrders();
                    gd.ShowDialog();

                }
                catch (Exception ex)
                {
                    var exceptionMessages = new StringBuilder();
                    do
                    {
                        exceptionMessages.Append(ex.Message);
                        ex = ex.InnerException;
                    }
                    while (ex != null);
                    MessageBox.Show(exceptionMessages.ToString());
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void stockItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(9);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void stockItemsOtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(10);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void currentReceipesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
             try
             {

                 frmDyeViewReport vRep = new frmDyeViewReport(1);
                 vRep.ShowDialog(this);

             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
            */

        }

        private void dyOrdersGarmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeOrder1 gd = new frmDyeOrder1();
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }


        }

        private void dyeBatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            /*
            try
            {
                 frmDyeBatch gd = new frmDyeBatch(true);
                 gd.ShowDialog();
                 

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             */


        }

        private void dyeHouseQDCCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(8);
                    gd.ShowDialog();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException en)
                {
                    foreach (var eve in en.EntityValidationErrors)
                    {
                        MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeHouseRemedyCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(9);
                    gd.ShowDialog();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException en)
                {
                    foreach (var eve in en.EntityValidationErrors)
                    {
                        MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }



        private void cottonMergeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCottonMerge gd = new frmCottonMerge();
                    gd.ShowDialog();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException en)
                {
                    foreach (var eve in en.EntityValidationErrors)
                    {
                        MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeBatchesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeBatch gd = new frmDyeBatch();
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void transferOfDyeBatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTransferToDyeHouse gd = new frmTransferToDyeHouse();
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }



        }

        private void cottonWasteRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // even though the form says Yarn waste this should be cotton waste  
                try
                {
                    frmYarnWasteRecording YarnWaste = new frmYarnWasteRecording();
                    YarnWaste.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cottonWasteSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCottonWaste YarnWaste = new frmCottonWaste();
                    YarnWaste.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void commissionDyeingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCommissionDyeing CommDyeing = new frmCommissionDyeing();
                    CommDyeing.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void allocateBatchToDyeMachineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAllocateToMachine Allocate = new frmAllocateToMachine();
                    Allocate.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void colourCheckAfterDyeingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                frmQAColourCheck colorCheck = new frmQAColourCheck();
                colorCheck.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             */
        }

        private void remedialActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmQARemedial remedial = new frmQARemedial();
                    remedial.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void stabilityCheckAfterDryingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                FrmStabilityDrying Stability = new FrmStabilityDrying(true);
                Stability.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            */
        }

        private void qAInputToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void colourAndStabilityCheckAfterCompactToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            // This form is also used for compacting
            //----------------------------------------------------------------
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    FrmStabilityDrying Stability = new FrmStabilityDrying(false);
                    Stability.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void reprintTransferTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                frmDyeReprint Reprint = new frmDyeReprint();
                Reprint.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             */
        }

        private void outstandingDyeOrderReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                frmDyeViewReport vRep = new frmDyeViewReport(10);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             */
        }

        private void allocateOperatorToBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAllocatedOperator vRep = new frmAllocatedOperator();
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeProcessOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeProcessOutput vRep = new frmDyeProcessOutput();
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void finalApprovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmFinalApproval vRep = new frmFinalApproval();
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void rejectFabricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmRejectFabric vRep = new frmRejectFabric();
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void writeOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmWriteOn vRep = new frmWriteOn();
                    /*int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);  */

                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeBatchesFabricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeBatch gd = new frmDyeBatch();
                    gd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fabricSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmFabricSales gd = new frmFabricSales();
                    gd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void testToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void griegeProductionTargetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmKnitViewRep vRep = new frmKnitViewRep(10);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void deliveriesToCommissionDyeingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // If Parameter is true then this is a Commission dyeing
                try
                {
                    frmCommissionDeliveries comdel = new frmCommissionDeliveries(true);
                    comdel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeReceipesDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {

                    frmSelReceipeDefinitions vRep = new frmSelReceipeDefinitions();
                    vRep.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void printTransactionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmReportOpts repOps = new frmReportOpts();
                    repOps.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeConsummablesStockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(13);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }



        //private void outstandingDyeOrderReportToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    ToolStripMenuItem oTi = sender as ToolStripMenuItem;
        //    if (core.GetUserAuthorisation(ud, oTi.Name))
        //    {
        //        try
        //        {
        //            frmDyeViewReport vRep = new frmDyeViewReport(10);
        //            int h = Screen.PrimaryScreen.WorkingArea.Height;
        //            int w = Screen.PrimaryScreen.WorkingArea.Width;
        //            vRep.ClientSize = new Size(w, h);
        //            vRep.ShowDialog(this);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
        //        {
        //            MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
        //        }
        //    }

        //}

        private void outstandingDyeOrderReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    DyeHouse.frmDyeOrdersSelection repOps = new frmDyeOrdersSelection();
                    repOps.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void dyeOrderReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeOrdersSel vRep = new frmDyeOrdersSel();
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeBatchGreigeStoreToPendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmReportOpts repOps = new frmReportOpts();
                    repOps.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeBatchReprintTransferTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeReprint Reprint = new frmDyeReprint();
                    Reprint.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fabricTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmKnitViewRep vRep = new frmKnitViewRep(15);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void printFabricSalesDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dyeBatchPendingToWIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmReportOpt2 Opt2 = new frmReportOpt2();
                    Opt2.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeBatchListOfRejectedDyeBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmReportOpts3 Opt2 = new frmReportOpts3();
                    Opt2.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeBatchListOfReprocessedBatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmReportOpts4 Opt2 = new frmReportOpts4();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fabricSaleDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // If Parameter is true then this is a Commission dyeing
                // else it is a fabric sales 
                try
                {
                    frmCommissionDeliveries comdel = new frmCommissionDeliveries(false);
                    comdel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fabricStockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmFabricSOHSel Opt2 = new frmFabricSOHSel();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyesAndChemicalConsumptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeAndChemSel Opt2 = new frmDyeAndChemSel();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeingProcessLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeingProcessLoss Opt2 = new frmDyeingProcessLoss();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeStockTakeSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmStockTake Opt2 = new frmStockTake();
                    Opt2.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void allShadedCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(26);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void allRemedyCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeViewReport vRep = new frmDyeViewReport(27);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void shadeResultsAfterDyeingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmShadeAfterDyeing vRep = new frmShadeAfterDyeing(3);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void colourCheckAfterDyeingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmQAColourCheck colorCheck = new frmQAColourCheck();
                    colorCheck.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void stabilityCheckAfterDryingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    FrmStabilityDrying Stability = new FrmStabilityDrying(true);
                    Stability.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void colourStabilityCheckAfterCompactingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    FrmStabilityDrying Stability = new FrmStabilityDrying(false);
                    Stability.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void nCRResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmNCRSelection NCR = new frmNCRSelection();
                    NCR.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void dyeConsummableReceiptsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeConsReceived gd = new frmDyeConsReceived();
                    gd.ShowDialog();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException en)
                {
                    foreach (var eve in en.EntityValidationErrors)
                    {
                        MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeConsummablesIntoKitchenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmConsumablesReceipt rcpt = new frmConsumablesReceipt();
                    rcpt.ShowDialog();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException en)
                {
                    foreach (var eve in en.EntityValidationErrors)
                    {
                        MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fabricProductionInQuaratineStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeProductionSel prodSel = new frmDyeProductionSel(1);
                    prodSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void createACutSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (var context = new TTI2Entities())
                {
                    var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == oTi.Name).FirstOrDefault();
                    if (Sections != null)
                    {
                        if (Sections.TLSECSect_InUse)
                        {
                            MessageBox.Show("This transaction is already in use", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            Sections.TLSECSect_InUse = true;

                        context.SaveChanges();
                    }
                }

                try
                {
                    frmCutSheet CutSheet = new frmCutSheet(ud);
                    CutSheet.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                using (var context = new TTI2Entities())
                {
                    var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == oTi.Name).FirstOrDefault();
                    if (Sections != null)
                    {
                        Sections.TLSECSect_InUse = false;
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void receiptACutSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCutSheetReceipt CutSheet = new frmCutSheetReceipt();
                    CutSheet.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void berriebiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmBerrieb1 CutSheet = new frmBerrieb1();
                    CutSheet.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void boxTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(10);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void receiptABoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCutBoxes cb = new frmCutBoxes();
                    cb.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cutDepartmentMeasurementAreasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(11);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void cutDepartmentMeasurementStandardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(12);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void productSpecSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmQASelection qaSel = new frmQASelection();
                    qaSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fabricReturnedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmFabricReturn qaSel = new frmFabricReturn();
                    qaSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cutDepartmentTrimsMeasurementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(13);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cutDepartmentFleeceCuffsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(14);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cutDepartmentFleeceWaistBandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(15);
                    gd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void trimsRecordedOnTheCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTrimsOnCuff gd = new frmTrimsOnCuff();
                    gd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fleeceCuffsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmFleeceData gd = new frmFleeceData();
                    gd.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cutSheetReprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmReprintCS reprintCS = new frmReprintCS();
                    reprintCS.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void rejectedPanelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmPanelReject rejectPanel = new frmPanelReject();
                    rejectPanel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void wIPCuttingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelWipCutting wipCutting = new frmSelWipCutting();
                    wipCutting.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cutProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelCutProduction wipCutting = new frmSelCutProduction();
                    wipCutting.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void panelIssueSelectionScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCMTPanelIssue cmtPanelIssue = new frmCMTPanelIssue();
                    cmtPanelIssue.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void truckLoadingInstructionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTruckLoading cmtTruckLoading = new frmTruckLoading();
                    cmtTruckLoading.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCMTDelivery cmtTruckLoading = new frmCMTDelivery();
                    cmtTruckLoading.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void panelIssueReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmPanelIssueReceipt panReceipt = new frmPanelIssueReceipt();
                    panReceipt.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void panelStoreStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelPanelStock selPanelStock = new frmSelPanelStock();
                    selPanelStock.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void rejectPanelReasonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmRejectReasons rejRes = new frmRejectReasons();
                    rejRes.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void rejectPanellStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelRejectedPanel rejPanel = new frmSelRejectedPanel(1);
                    rejPanel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void qAResultsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmQaReportSelection vReportSelection = new frmQaReportSelection())
                {
                    DialogResult dr = vReportSelection.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void standardProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(16);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void factoryConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCMTViewRep vRep = new frmCMTViewRep(4);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void lineConfigurationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmLineConfiguration LineConfig = new frmLineConfiguration();
                    LineConfig.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTMeasurementPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(17);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void stabilityCheckConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    CMT.frmBFADefinition gd = new CMT.frmBFADefinition();
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void finalStabilityCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmBFASel gd = new frmBFASel();
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void garmentDefectCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(18);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void workCompletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCompleted fc = new frmCompleted();
                    fc.ShowDialog(this);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException en)
                {
                    foreach (var eve in en.EntityValidationErrors)
                    {
                        MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void cMTStandardProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(16);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void panelStockAtPanelStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmPanelStock ps = new frmPanelStock();
                    ps.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void panelStockAtCMTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCMTPanelStock vRep = new frmCMTPanelStock();
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTReturnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCMTReturnsTransfers retTrans = new frmCMTReturnsTransfers();
                    retTrans.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void bFARecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmBFAMeasureRecording MeasRecording = new frmBFAMeasureRecording();
                    MeasRecording.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTDefectFlawsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCMTViewRep vRep = new frmCMTViewRep(9);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.Show(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTCurrentMeasurementValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCMTViewRep vRep = new frmCMTViewRep(10);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.Show(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTStockInDespatchCageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmStockCage stockCage = new frmStockCage();
                    stockCage.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTProductionReportByPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmProdByPeriodSel prodByPer = new frmProdByPeriodSel(true);
                    prodByPer.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTStockInDespatchCageFinishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmProdByPeriodSel prodByPer = new frmProdByPeriodSel(false);
                    prodByPer.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void panelsIssuedAsYetUndeliveredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCMTViewRep vRep = new frmCMTViewRep(14);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void ShowKPIs()
        {
            frmKPI KPIs = new frmKPI();
            KPIs.Show();
        }

        private void cMTWorkInProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCMTWip WIPSelection = new frmCMTWip();
                    WIPSelection.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTMeasurementsRecordedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmMPSel measurement = new frmMPSel();
                    measurement.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeHouseStagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(19);
                    gd.ShowDialog();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException en)
                {
                    foreach (var eve in en.EntityValidationErrors)
                    {
                        MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void shadeResultsAfterDryingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmShadeAfterDyeing vRep = new frmShadeAfterDyeing(4);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void shadeResultsAfterCompactingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmShadeAfterDyeing vRep = new frmShadeAfterDyeing(5);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void lineFeederQualityCheckListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmLineFeederBC LineFeeder = new frmLineFeederBC();
                    LineFeeder.ShowDialog(this);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException en)
                {
                    foreach (var eve in en.EntityValidationErrors)
                    {
                        MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                        foreach (var ve in eve.ValidationErrors)
                        {
                            MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void transactionTypesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(11);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void lineFeederQualityCheckListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelFeederFChk selFeeder = new frmSelFeederFChk();
                    selFeeder.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTTransferToWarehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (var context = new TTI2Entities())
                {
                    var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == oTi.Name).FirstOrDefault();
                    if (Sections != null)
                    {
                        if (Sections.TLSECSect_InUse)
                        {
                            MessageBox.Show("This transaction is already in use", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            Sections.TLSECSect_InUse = true;

                        context.SaveChanges();
                    }
                }
                try
                {
                    frmWareHouseTransfers whseTrans = new frmWareHouseTransfers();
                    whseTrans.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                using (var context = new TTI2Entities())
                {
                    var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == oTi.Name).FirstOrDefault();
                    if (Sections != null)
                    {
                        Sections.TLSECSect_InUse = false;
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void customerOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCustomerOrders CustOrders = new frmCustomerOrders();
                    CustOrders.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {

            }
        }

        private void cMTTransferToWarehouseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTransferConfirm TransConfirm = new frmTransferConfirm();
                    TransConfirm.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void outstandingPickingListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmPickingSlipReprint PLReprint = new frmPickingSlipReprint();
                    PLReprint.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void warehouseReceiptsexCMTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmWareHouseReceipt whouseR = new frmWareHouseReceipt();
                    whouseR.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void boxSplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmBoxSplit BoxSplt = new frmBoxSplit();
                    BoxSplt.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void salesPickingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (var context = new TTI2Entities())
                {
                    var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == oTi.Name).FirstOrDefault();
                    if (Sections != null)
                    {
                        if (Sections.TLSECSect_InUse)
                        {
                            MessageBox.Show("This transaction is already in use", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            Sections.TLSECSect_InUse = true;

                        context.SaveChanges();
                    }
                }

                try
                {
                    frmPickList plist = new frmPickList();
                    plist.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                using (var context = new TTI2Entities())
                {
                    var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == oTi.Name).FirstOrDefault();
                    if (Sections != null)
                    {
                        Sections.TLSECSect_InUse = false;
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void outstandingCustomerOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDetailCustomerOrders detCustOrders = new frmDetailCustomerOrders(ud);
                    detCustOrders.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void stockQuantitiesOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmStockOnHand SOH = new frmStockOnHand(1, ud);
                    SOH.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void boxesInStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmStockOnHand SOH = new frmStockOnHand(2, ud);
                    SOH.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void salesDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (var context = new TTI2Entities())
                {
                    var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == oTi.Name).FirstOrDefault();
                    if (Sections != null)
                    {
                        if (Sections.TLSECSect_InUse)
                        {
                            MessageBox.Show("This transaction is already in use", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                            Sections.TLSECSect_InUse = true;

                        context.SaveChanges();
                    }
                }

                try
                {
                    frmCustDeliveries CustDeliveries = new frmCustDeliveries();
                    CustDeliveries.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                using (var context = new TTI2Entities())
                {
                    var Sections = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == oTi.Name).FirstOrDefault();
                    if (Sections != null)
                    {
                        Sections.TLSECSect_InUse = false;
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void ordersPickedAwaitingDeliveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmOutStandingPL outPl = new frmOutStandingPL();
                    outPl.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void customerSalesReturnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCustomerReturns custReturns = new frmCustomerReturns();
                    custReturns.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void stockAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmStockAdj stockAdj = new frmStockAdj();
                    stockAdj.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {

            }

        }

        private void salesReturnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCustomerSales CustSales = new frmCustomerSales();
                    CustSales.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void pivotTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmPivotTables pivTables = new frmPivotTables(ud);
                    pivTables.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void replenishmentDefinitionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                try
                {
                    frmReplenishment replesh = new frmReplenishment();
                    replesh.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void replenishmentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelReorderDetails reorderDetails = new frmSelReorderDetails();
                    reorderDetails.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void replenishmentFinishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelFinishedGoods finishedGoods = new frmSelFinishedGoods(ud);
                    finishedGoods.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void replenishmentKnitStockLevelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmPlanningKnitStock planningKnitStock = new frmPlanningKnitStock();
                    planningKnitStock.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void replenishmentMachineCapacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSuggestedKO suggestedKO = new frmSuggestedKO();
                    suggestedKO.ShowDialog(this);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void applicationMethodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSecuritySections secSections = new frmSecuritySections();
                    secSections.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void grantUserAccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmUserAccess userAccess = new frmUserAccess(ud);
                    userAccess.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void outstandingDyeOrdersForGreigeItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dyeProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelResourceEffic resourceEff = new frmSelResourceEffic();
                    resourceEff.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void dyeMachinePerformanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelDyeMachPerformance DyeMachPerformance = new frmSelDyeMachPerformance();
                    DyeMachPerformance.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fabricSalesDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelFabricSales fabSales = new frmSelFabricSales();
                    fabSales.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void bundleStoreStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelBundleStore selBundleStore = new frmSelBundleStore();
                    selBundleStore.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void dyeChemicalsProductionPlanningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmProductionPlanDC prodPlanning = new frmProductionPlanDC();
                    prodPlanning.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void balesInStockByBaleNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    Spinning.frmCottonBalesInStock balesinstock = new Spinning.frmCottonBalesInStock();
                    balesinstock.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeOrdersToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmViewDyeOrder vDO = new frmViewDyeOrder())
                {

                    DialogResult dr = vDO.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void commissionBatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmViewCommissionBatches vDO = new frmViewCommissionBatches())
                {
                    DialogResult dr = vDO.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void commissionReceiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmViewCommissionReceipts vDO = new frmViewCommissionReceipts())
                {
                    DialogResult dr = vDO.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeBatchesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmViewDyeBatches vDO = new frmViewDyeBatches(true))
                {
                    DialogResult dr = vDO.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void commissionDeliveriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmViewCommissionDeliveries vDO = new frmViewCommissionDeliveries())
                {
                    DialogResult dr = vDO.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void fabricSalesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmViewFabricSales vDO = new frmViewFabricSales())
                {
                    DialogResult dr = vDO.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void allocateYarnToKnitOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                /*
                frmYarnAllocation YarnAllocation = new frmYarnAllocation();
                YarnAllocation.ShowDialog(this);
                 */

                MessageBox.Show("This facility is no longer available");
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void knitOrdersAwaitingYarnAssignmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                frmKnitViewRep vRep = new frmKnitViewRep(27);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void pastelCodeStyleDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // The defining of styles 
                //-----------------------------------------------------------------------------------------
                try
                {
                    using (frmTLADMGardDef gd = new frmTLADMGardDef(19))
                    {
                        DialogResult dr = gd.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void greigeProducedAwaitingInspectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                frmKnitViewRep vRep = new frmKnitViewRep(28);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void viewKnitOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmViewOrders ViewOrders = new frmViewOrders())
                {
                    DialogResult dr = ViewOrders.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void greigeConversionOldSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmConversion Conversion = new frmConversion())
                {
                    DialogResult dr = Conversion.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void yarnOrderAssignedToKnitOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmYarnOrderAssigned YOAssigned = new frmYarnOrderAssigned())
                {
                    DialogResult dr = YOAssigned.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void viewsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void discontinuedDyesAndChemicalsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                frmDyeViewReport vRep = new frmDyeViewReport(38);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void dyeBatchStatusReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmViewDyeBatches vDO = new frmViewDyeBatches(false))
                {
                    DialogResult dr = vDO.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }

        }

        private void cottonWasteStockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                frmViewReport vRep = new frmViewReport(23);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void greigeStockSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmK10ReportSel repSel = new frmK10ReportSel(false);
                    repSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cutDepartmentMeasurementLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmTLADMCustomerTypes vCustomerTypes = new frmTLADMCustomerTypes(20))
                {
                    DialogResult dr = vCustomerTypes.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void fabricProductionToFabricStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeProductionSel prodSel = new frmDyeProductionSel(2);
                    prodSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void berriebiResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    FrmBierriebiSel prodSel = new FrmBierriebiSel();
                    prodSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTGarmentCostsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmTLADMCustomerTypes vCustomerTypes = new frmTLADMCustomerTypes(21))
                {
                    DialogResult dr = vCustomerTypes.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTCostAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmCMTDateSelection DateSelection = new frmCMTDateSelection(false))
                {
                    DialogResult dr = DateSelection.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }

                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void stockTakeListsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmStockLists StkList = new frmStockLists())
                {
                    DialogResult dr = StkList.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void salesPickingListConfirmationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmSalesPickListConfirmation PckList = new frmSalesPickListConfirmation())
                {
                    DialogResult dr = PckList.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void wareHouseToWareHouseTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmSelWhseToWhse SelWhseToWhse = new frmSelWhseToWhse())
                {
                    DialogResult dr = SelWhseToWhse.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void wareHouseStockTakeOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmStockTakeOn TakeOn = new frmStockTakeOn())
                {
                    DialogResult dr = TakeOn.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTCostAnalysisValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmCostAnalysisValues CostValues = new frmCostAnalysisValues())
                {
                    DialogResult dr = CostValues.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void boxStyleAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmBoxStyleAdjustment CostValues = new frmBoxStyleAdjustment())
                {
                    DialogResult dr = CostValues.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTCostAnalysisTransactionsExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmCMTSelection CostSelection = new frmCMTSelection())
                {
                    DialogResult dr = CostSelection.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void salesPickingListResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmResetPickingList ResetPickingList = new frmResetPickingList())
                {
                    DialogResult dr = ResetPickingList.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTCompletedWorkAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmCMTFinishedWAnalysis CMTView = new frmCMTFinishedWAnalysis())
                {
                    DialogResult dr = CMTView.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void boxEnquiryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmBoxView BoxView = new frmBoxView())
                {
                    DialogResult dr = BoxView.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void pickingListInquiryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmPickListView PickListView = new frmPickListView(true))
                {
                    DialogResult dr = PickListView.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void deliveryNoteInquiryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmPickListView PickListView = new frmPickListView(false))
                {
                    DialogResult dr = PickListView.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void deliveryNoteRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmDeliveryNoteRegister PickListView = new frmDeliveryNoteRegister(true))
                {
                    DialogResult dr = PickListView.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cutSheetDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                using (frmCutView vCutView = new frmCutView())
                {
                    DialogResult dr = vCutView.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cutSheetDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmCMTView CMTCutView = new frmCMTView())
                {
                    DialogResult dr = CMTCutView.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void wareHouseAssociationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmTLADMCustomerTypes TLADMCustType = new frmTLADMCustomerTypes(22))
                {
                    DialogResult dr = TLADMCustType.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void greigeStockWriteOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmGreigeStockWritoff GreigeStockWOff = new frmGreigeStockWritoff())
                {
                    DialogResult dr = GreigeStockWOff.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTPanelIssueReprintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmCMTPanelReissue PanelReissue = new frmCMTPanelReissue())
                {
                    DialogResult dr = PanelReissue.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }


        private void pieceEnquiryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmPieceEnquiry PieceEnquiry = new frmPieceEnquiry())
                {
                    DialogResult dr = PieceEnquiry.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }



        private void lineFeederCheckListInputDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                using (frmLineFeederInput LineFeeder = new frmLineFeederInput())
                {
                    DialogResult dr = LineFeeder.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }

        }

        private void cutSheetsOnHoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmSelCSOnHold OnHold = new frmSelCSOnHold())
            {
                DialogResult dr = OnHold.ShowDialog(this);
                if (dr == DialogResult.OK)
                {
                }
            }


            /*
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                CMTReportOptions repOpts = new CMTReportOptions();
                CMTQueryParameters QueryParms = new CMTQueryParameters();
                frmCMTViewRep vRep = new frmCMTViewRep(24, QueryParms, repOpts);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
               
            }
             * */
        }

        private void purchaseOrderNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                using (frmPOSearch LineFeeder = new frmPOSearch())
                {
                    DialogResult dr = LineFeeder.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void pickingNoteRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Picking note 
                //========================================================================
                using (frmDeliveryNoteRegister PickListView = new frmDeliveryNoteRegister(false))
                {
                    DialogResult dr = PickListView.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void boxesDespatchedToWarehouseNotYetReceiptedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Picking note 
                //========================================================================
                using (frmBoxesDespatchedNoReceipted NotReceipted = new frmBoxesDespatchedNoReceipted())
                {
                    DialogResult dr = NotReceipted.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }


            }
        }

        private void cutSheetNumberRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Picking note 
                //========================================================================
                using (CMT.frmCMTDeliveryNoteReprint Register = new CMT.frmCMTDeliveryNoteReprint())
                {
                    DialogResult dr = Register.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }


            }
        }

        private void reprintADyeOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Picking note 
                //========================================================================
                using (frmDyeOrderReprint OrderReprint = new frmDyeOrderReprint(true))
                {
                    DialogResult dr = OrderReprint.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }


            }
        }

        private void reprintADyeBatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Picking note 
                //========================================================================
                using (frmDyeOrderReprint OrderReprint = new frmDyeOrderReprint(false))
                {
                    DialogResult dr = OrderReprint.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }


            }
        }

        private void outstandingCustomerOrdersByMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                using (frmSelOutStandingOrders OrderOutStanding = new frmSelOutStandingOrders())
                {
                    DialogResult dr = OrderOutStanding.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {

                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void autocloseCustomerOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    using (var context = new TTI2Entities())
                    {
                        var Orders = context.TLCSV_PurchaseOrder.Where(x => !x.TLCSVPO_Closeed).ToList();
                        foreach (var Order in Orders)
                        {
                            var OrderDetails = context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK == Order.TLCSVPO_Pk && !x.TLCUSTO_Closed).ToList();
                            if (OrderDetails.Count > 0)
                            {
                                foreach (var OrderDetail in OrderDetails)
                                {
                                    var TotalSales = context.TLCSV_StockOnHand.Where(x => x.TLSOH_Sold && x.TLSOH_POOrder_FK == Order.TLCSVPO_Pk && x.TLSOH_POOrderDetail_FK == OrderDetail.TLCUSTO_Pk).Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                                    if (TotalSales >= OrderDetail.TLCUSTO_Qty)
                                    {
                                        OrderDetail.TLCUSTO_Closed = true;
                                    }
                                }
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Transaction successfully completed");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTNonComplianceDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // The defining of Non Compliance Master Details 
                //-----------------------------------------------------------------------------------------
                try
                {
                    using (frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(23))
                    {
                        DialogResult dr = gd.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTCutNonComplianceDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // The defining of Non Compliance Transaction Details 
                //-----------------------------------------------------------------------------------------
                try
                {
                    using (frmCMTNonCompliance NonC = new frmCMTNonCompliance(0))
                    {
                        DialogResult dr = NonC.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void boughtInFabricToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // Bought in Fabric store to store movement 
                //-----------------------------------------------------------------------------------------
                try
                {
                    using (frmBIFStoreToStore Store2Store = new frmBIFStoreToStore())
                    {
                        DialogResult dr = Store2Store.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTCostingProfitabilityAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // The defining of Non Compliance Transaction Details 
                //-----------------------------------------------------------------------------------------
                try
                {

                    using (frmCostingProfitability Profitability = new frmCostingProfitability())
                    {
                        DialogResult dr = Profitability.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void reportsToolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void dyeReceipesDifinitionByConsumableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                // The defining of Non Compliance Transaction Details 
                //-----------------------------------------------------------------------------------------
                try
                {

                    using (frmSelectReceipeConsumable ReceipeConsumable = new frmSelectReceipeConsumable())
                    {
                        DialogResult dr = ReceipeConsumable.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTProductionAnalysisOverA12MonthSpreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    using (frmSelectProduction ProductionSelect = new frmSelectProduction())
                    {
                        DialogResult dr = ProductionSelect.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void salesByStyleByPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    using (frmSelSalesByPeriod SalesByPeriod = new frmSelSalesByPeriod(true))
                    {
                        DialogResult dr = SalesByPeriod.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void boughtInFabricFabricReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    using (frmBoughtInFabric BoughtInFabric = new frmBoughtInFabric())
                    {
                        DialogResult dr = BoughtInFabric.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cutSheetDownSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    using (frmCutSheetDownSize DownSize = new frmCutSheetDownSize())
                    {
                        DialogResult dr = DownSize.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void salesByStyleByCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    using (frmSelSalesByPeriod SalesByPeriod = new frmSelSalesByPeriod(false))
                    {
                        DialogResult dr = SalesByPeriod.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void boxNumberAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    using (fmtCuttingBoxesAdjustment CuttingBoxes = new fmtCuttingBoxesAdjustment())
                    {
                        DialogResult dr = CuttingBoxes.ShowDialog(this);
                        if (dr == DialogResult.OK)
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void boxPackagingSpecificationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmTLADMCustomerTypes gd = new frmTLADMCustomerTypes(24);
                    gd.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void nCRDetailsByMonthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmNCRByMonth NcrByMnth = new frmNCRByMonth();
                    NcrByMnth.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void rePackCenterStatusReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmRepackCenterStatus RePackStatus = new frmRepackCenterStatus();
                    RePackStatus.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cutsheetPriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCutSheetPriority prior = new frmCutSheetPriority();
                    prior.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cutSheetPriorityAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCutSheetPAnalysis prior = new frmCutSheetPAnalysis();
                    prior.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void itemStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmItemStatus ItemStatus = new frmItemStatus(true);
                    ItemStatus.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void crossBorderDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCrossBorderDoc BorderDoc = new frmCrossBorderDoc();
                    BorderDoc.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cutSheetProductionPlanningResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCutProductionPlan ProdPlan = new frmCutProductionPlan(true);
                    ProdPlan.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cutProductionDateRequiredEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCutProductionPlan ProdPlan = new frmCutProductionPlan(false);
                    ProdPlan.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void customerTransactionHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCustomerTransHistory CustHistory = new frmCustomerTransHistory(true);
                    CustHistory.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void daysSalesByStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        // MessageBox.Show("Current Work in Progress", "Facility under development");

                        frmDaysSales DaysSales = new frmDaysSales();
                        DaysSales.ShowDialog();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTRequiredDatesAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDateRequired DaysRequired = new frmDateRequired(true);
                    DaysRequired.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void dateRequiredEditFacilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDateRequired DaysRequired = new frmDateRequired(false);
                    DaysRequired.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void dyeBatchesDateRequiredManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDBDatesReq DaysRequired = new frmDBDatesReq();
                    DaysRequired.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void dyeBatchProductionDaysAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelProductionDays ProductionDays = new frmSelProductionDays();
                    ProductionDays.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void placeCutSheetOnOffHoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmOnOffHold OnOffHold = new frmOnOffHold();
                    OnOffHold.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTProductionTargetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmUnitProductionTargets Targets = new frmUnitProductionTargets();
                    Targets.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void daysOfSalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmItemStatus ItemStatus = new frmItemStatus(false);
                    ItemStatus.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void fabricProductionNotFinishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDyeProductionSel prodSel = new frmDyeProductionSel(3);
                    prodSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void dataClearDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmDataClearDown ClearDown = new frmDataClearDown();
                    ClearDown.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void yarnStockReceivedTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnReceiptTrans Yarnreceipt = new frmYarnReceiptTrans();
                    Yarnreceipt.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void qAReportingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void qAReportingManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmQAReporting QAReporting = new frmQAReporting();
                    QAReporting.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void daysDelayReportingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmQASDaysDelay QAReporting = new frmQASDaysDelay();
                    QAReporting.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void inspectionAfterDryingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmInsAfterDrying QInsAfterDrying = new frmInsAfterDrying(ud);
                    QInsAfterDrying.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmQAQualityException QualException = new frmQAQualityException();
                    QualException.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void executiveReportingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmExecSel ExecSel = new frmExecSel();
                    ExecSel.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void greigeKeyMeasurementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                Knitting.frmKnitViewRep vRep = new Knitting.frmKnitViewRep(33);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTMeasurementValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                CMT.frmCMTViewRep vRep = new CMT.frmCMTViewRep(32);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);



            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void negativeStockByCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmNegativeStock NegativeStock = new frmNegativeStock();
                    NegativeStock.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void salesPickingListStatusAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmPickingListStatusChange StatusChange = new frmPickingListStatusChange();
                    StatusChange.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cutSheetsOnOffHoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCutOnOffHold StatusChange = new frmCutOnOffHold();
                    StatusChange.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cutSheetOnHoldReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    Cutting.frmCutViewRep vRep = new Cutting.frmCutViewRep(21);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fabricOnOffHoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmFabricOnOffHold FabricOnOffHold = new frmFabricOnOffHold();
                    FabricOnOffHold.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void dyeBatchOnHoldReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    DyeHouse.frmDyeViewReport vRep = new DyeHouse.frmDyeViewReport(45);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void inUseResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    using (var context = new TTI2Entities())
                    {
                        context.TLSEC_Sections.Where(x => x.TLSECSect_InUse).Update(x => new TLSEC_Sections { TLSECSect_InUse = false });
                    }

                    MessageBox.Show("Database successfully updated");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void fabricReversalExRejectStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                try
                {
                    frmFabricReversal FabricReversal = new frmFabricReversal();
                    FabricReversal.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnOrderAuditTrailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                try
                {
                    frmYarnOrderAuditTrail auditTrail = new frmYarnOrderAuditTrail();
                    auditTrail.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void bundleStorePanelStoreQtysAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                try
                {
                    frmQtyAdjustment QtyAdjustment = new frmQtyAdjustment();
                    QtyAdjustment.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cutSheetOnHoldOffHoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                try
                {
                    frmCloseCutSheet CloseCutSheet = new frmCloseCutSheet();
                    CloseCutSheet.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void pastelReconciliationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                try
                {
                    frmPastelRecon PastelRecon = new frmPastelRecon();
                    PastelRecon.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cMTProductionAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {

                try
                {
                    frmCMTProdAnalysis ProdAnalysis = new frmCMTProdAnalysis();
                    ProdAnalysis.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void yarnProductionByYarnOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmYarnProduction yarnP = new frmYarnProduction();
                    yarnP.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dailyProductionByMachineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    Spinning.frmDailyProduction dailyYarnProduction = new frmDailyProduction();
                    dailyYarnProduction.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void customerAuditHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCustomerTransHistory CustHistory = new frmCustomerTransHistory(false);
                    CustHistory.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void closeYarnPalletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCloseYarnPsallet CloseYarnP = new frmCloseYarnPsallet();
                    CloseYarnP.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void yarnStockStoreToStoreTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmStoreToStoreTransfer StoreToStore = new frmStoreToStoreTransfer();
                    StoreToStore.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void closeCutSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCloseCutSheet CloseCutSheet = new frmCloseCutSheet();
                    CloseCutSheet.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void closeDyeOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCloseDyeOrders CloseDyeOrders = new frmCloseDyeOrders();
                    CloseDyeOrders.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void productRatingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSelectProductRating SelectProdRating = new frmSelectProductRating();
                    SelectProdRating.ShowDialog();

                    /*
                    frmAdminViewRep vRep = new frmAdminViewRep(12);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    */


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void captureSliverPoductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSliverProduction dailyYarnProduction = new frmSliverProduction();
                    dailyYarnProduction.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void sliverProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmSliverProductionSelection sliverProductionSelection = new frmSliverProductionSelection();
                    sliverProductionSelection.MaximizeBox = false;
                    sliverProductionSelection.MinimizeBox = false;

                    sliverProductionSelection.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this))
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void closeDyeBatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmCloseDyeBatches CloseDyeOrders = new frmCloseDyeBatches();
                    CloseDyeOrders.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void recalculateExpectedUnitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    ReCalculateExpectedUnits Recalculate = new ReCalculateExpectedUnits();
                    Recalculate.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void boxedQuantyAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    CMT.frmCMTBoxedQtyAdjustment BoxedQty = new frmCMTBoxedQtyAdjustment();
                    BoxedQty.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void cMTCutSheetNonComplianceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    CMT.frmNonComplianceSelection NonComSel = new CMT.frmNonComplianceSelection();
                    NonComSel.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void greigeStockLiningsTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    Knitting.frmLiningTransaction LiningTrans = new Knitting.frmLiningTransaction();
                    LiningTrans.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void interCMTTransfersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    CMT.frmInterCMTTransfer InterCMTTrnsfr = new CMT.frmInterCMTTransfer();
                    InterCMTTrnsfr.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void interDepartmentalFaultsComparisonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                   ProductionPlanning.frmInterDeptFaults  InterCMTTrnsfr = new ProductionPlanning.frmInterDeptFaults();
                    InterCMTTrnsfr.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void interDepartmentalAnalysisDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    ProductionPlanning.frmInterDept CMTTrnsfr = new ProductionPlanning.frmInterDept();
                    CMTTrnsfr.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void reOpenClosedOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    CustomerServices.frmPOReversal CMTTrnsfr = new CustomerServices.frmPOReversal();
                    CMTTrnsfr.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void reprintCMTTransferToWarehouseDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;

            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    CustomerServices.frmReprintWareHouse CMTTrnsfr = new CustomerServices.frmReprintWareHouse();
                    CMTTrnsfr.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }

            }
        }

        private void dskWeightVarianceReportingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmK07ReportSel greigeP = new frmK07ReportSel(false);
                    greigeP.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void cutSheetReturnsToOriginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                     CMT.frmCutSheetReturn greigeP = new CMT.frmCutSheetReturn();
                     greigeP.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }
        }

        private void dyeHouseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dyeHouseStandardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    Administration.frmFabricDyingStandards greigeP = new Administration.frmFabricDyingStandards();
                    greigeP.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                {
                    MessageBox.Show(ud._NotAuthorisedMessage, ud._UserName);
                }
            }

        }

        private void currentDyeStandardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    frmAdminViewRep vRep = new frmAdminViewRep(13);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dyeConsummablesInspectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    DyeHouse.frmConsummablesInspectionResults InsResults = new DyeHouse.frmConsummablesInspectionResults();
                    InsResults.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void interDepartmentalDskAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    ProductionPlanning.frmInterDeptDskAnalysis InsResults = new ProductionPlanning.frmInterDeptDskAnalysis();
                    InsResults.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void HydroMeasuresWidth_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    DyeHouse.frmHydroResults HydroResults = new DyeHouse.frmHydroResults();
                    HydroResults.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void fabricBasicQualityInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    DyeHouse.frmDyedBasicQual BasicQual = new DyeHouse.frmDyedBasicQual(true);
                    BasicQual.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void fabricBaqsicQualityInformationWeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    DyeHouse.frmDyedBasicQual BasicQual = new DyeHouse.frmDyedBasicQual(false);
                    BasicQual.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void contiWorkWearReceiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
            if (core.GetUserAuthorisation(ud, oTi.Name))
            {
                try
                {
                    CustomerServices.frmContiWorkWearReceipts ContiWear = new CustomerServices.frmContiWorkWearReceipts();
                    ContiWear.ShowDialog(this);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
       
}

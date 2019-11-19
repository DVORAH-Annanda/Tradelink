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
using System.Security.Principal;
using System.Security.Permissions;
using System.Threading;
using Security;
using System.Xml;
using System.Net;
using System.Net.Sockets;

namespace TTI2_WF
{
   
    public partial class frmTLMain : Form
    {
        public static int CompayNoSelected;

        String Mach_IP;

        Util core;

        WindowsIdentity currentIdentity;
        WindowsPrincipal currentPrincipal;

        public frmTLMain()
        {
            //---------------------------------------------------------------
            //  cottonDeliveryToolStripMenuItem
            //  cottonReturnsToolStripMenuItem
            //  cottonStockToolStripMenuItem
            //  cottonStockSalesToolStripMenuItem
            //  cottonIssuesToProductionToolStripMenuItem
            //  qAConfirmationToolStripMenuItem
            //------------------------------------------------------------------

            InitializeComponent();
            core = new Util();

            foreach (ToolStripMenuItem menu in menuStrip1.Items)
            {
                activateItems(menu);
            }

            //----------------------------------------------------------------------------
            currentIdentity = WindowsIdentity.GetCurrent();
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            currentPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
            //--------------------------------------------------------------------------------

            Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                            .AddressList.First(f => f.AddressFamily == AddressFamily.InterNetwork)
                            .ToString();


            if (ApplicationDeployment.IsNetworkDeployed)
            {
                var ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                this.Text = string.Format("Your Application Name v{0}.{1}", ver.Major, ver.Revision);
            }
        }

        private void activateItems(ToolStripMenuItem item)
        {
            item.Visible = true;
            using (var context = new TTI2Entities())
            {
                for (int i = 0; i < item.DropDown.Items.Count; i++)
                {
                    ToolStripItem subItem = item.DropDown.Items[i];

                    if (subItem.Name.Contains("1"))
                        continue;

                    if (subItem.IsOnDropDown)
                    {
                        var Existing = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == subItem.Name && x.TLSECSect_Description == subItem.Text).FirstOrDefault();
                        if (Existing == null)
                        {
                            TLSEC_Sections secs = new TLSEC_Sections();
                            secs.TLSECSect_Description = subItem.Text;
                            secs.TLSECSect_Name = subItem.Name;

                            context.TLSEC_Sections.Add(secs);
                        }


                        if (item is ToolStripMenuItem)
                        {
                            activateItems(subItem as ToolStripMenuItem);
                        }
                    }

                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                }
            }
        }

       
              

        private void weightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
           
            if (currentPrincipal.IsInRole(WindowsBuiltInRole.Administrator))
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
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(currentPrincipal.Identity.Name +  " You are not authorised to use this function");
            }
        }

        private void widthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem oTi = sender as ToolStripMenuItem;
                       
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
        
        private void productTypesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void greigeToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void trimsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void stylesDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void productRatingsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void sizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Sizes Definition
            //---------------------------------------------------------------------
            try
            {
                frmTLADMGardDef gd = new frmTLADMGardDef(8);
                gd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void coloursToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void auxColoursToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Aux Colours
            //-----------------------------------------------------------------------------------------
            try
            {
                frmTLADMGardDef gd = new frmTLADMGardDef(6);
                gd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void labelsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void storeTypesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void productGroupsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void nonStockCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void customerTypesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void consumableGroupsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //-------------------------------------------------------------------------
            // Department Definition
            //--------------------------------------------------------------------------------------------
            try
            {
                frmTLADMDeptdef dept = new frmTLADMDeptdef();
                dept.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nonStockItemsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //--------------------------------------------------------------------
            // Cotton 
            //-------------------------------------------------------------------
            try
            {
                frmTLADM_Cotton cotton = new frmTLADM_Cotton();
                cotton.ShowDialog(this);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void warehousesAndStoresToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void consumablesOtherToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void stockTakeFrequencyCodesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void consumablesDyesAndChemicalsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void customerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void machineOperatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmTLADM_QualityDefinition qd = new frmTLADM_QualityDefinition(5);
                qd.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void garmentDEfectCodeToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void garmentRejectReasonsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void supplierDetailsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void productTypeGoodsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void machineDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void finishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void machineMaintenanceToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonOriginToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void greigeQualityToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void unitsOfMeasureToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void definitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Facility is No Longer Required");
            /*
            try
            {
                frmTLADMGardDef gd = new frmTLADMGardDef(16);
                gd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             */ 
        }

        private void panelsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void companyDetailsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void Main_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void cottonAgentsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void hauliersDetailsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonDeliveryToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonReturnsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonStockToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonStockSalesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonIssuesToProductionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnOrderToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void palletLabelsStickersToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnProductionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void closeYarnOrderToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void inspectionAndQAToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void issueToRoductionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void sellingOfYarnToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void scrappingOfYarnToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnStockAdjustmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        private void transactionTypesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonContractSummaryDetailToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void rawCottonStockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmViewReport vRep = new frmViewReport(7);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void rawCottonMovementReportToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnTransactionReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmYarnTransactionsReports transRep = new frmYarnTransactionsReports();
                transRep.ShowDialog(this);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nonStockItemsConsumptionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void capacityUtilisationToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void departmentAreasToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void closingWIPStockRecordingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void wIPSpinningMovementReportToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnStockOnHandByStoreToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void commissionKnittingReceiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCommKnitting  knit = new frmCommKnitting();
                knit.ShowDialog(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void receiptOfYarn3rdPartyPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnReturnedToSupplierToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnStockAdjustmentsToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void createAKnittingOrderToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void greigeProductionRecordingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void closeAKnitOrderToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void reconcileAKnitOrderToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void greigeInspectionResultsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void greigeStockAdjustmentsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void thirdPartyGreigeReceivedForCommissionDyeingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void thirdPartyGreigeStockAdjustmentsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void greigePlanningToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void yarnReturnToSupplierCommissionKnittingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void reinstateOrderToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnWasteRecordingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnWithdrawalNoteToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnStockOnHandByStoreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                YarnReportOptions opts = new YarnReportOptions();
                opts.reportChoice = 2;

                frmKnitViewRep vRep = new frmKnitViewRep(16, opts);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void yarnStockOnHandByStoreCommissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                YarnReportOptions opts = new YarnReportOptions();
                opts.reportChoice = 1;

                frmKnitViewRep vRep = new frmKnitViewRep(16, opts);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void greigeItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmKnitViewRep vRep = new frmKnitViewRep(15);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void knittingWIPGreigeProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmKnitViewRep vRep = new frmKnitViewRep(17);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void knitOrdersProcessLossToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void greigeProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmK07ReportSel greigeP = new frmK07ReportSel();
                greigeP.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void greigeKnittedQAResultsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void greigeEfficiencyUtilisationForGreigeKnittedToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void greigeStockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmK10ReportSel repSel = new frmK10ReportSel();
                repSel.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cGradeReportByPeriodToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void yarnStockOnHandByStoreOwnYarnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmKnitViewRep vRep = new frmKnitViewRep(20);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(1);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void customerByCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(2);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void categoriesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(3);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void supplierByCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(4);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void warehouseStoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(5);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nonStockItemsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(6);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void operatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(7);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void byDepartmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(8);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void yarnOrderPendingProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmViewReport vRep = new frmViewReport(20);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void receipeDefinitiionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void deptProductionLossToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyeOrdersToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void stockItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(9);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stockItemsOtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(10);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void dyeHouseRemedyCodesToolStripMenuItem_Click(object sender, EventArgs e)
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

        

        private void cottonMergeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyeBatchesToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void transferOfDyeBatchesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonWasteRecordingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cottonWasteSalesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void commissionDyeingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void allocateBatchToDyeMachineToolStripMenuItem_Click(object sender, EventArgs e)
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
            // This form is also used for compacting
            //----------------------------------------------------------------
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
            try
            {
                frmAllocatedOperator vRep = new frmAllocatedOperator();
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dyeProcessOutputToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void finalApprovalToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void rejectFabricToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void writeOnOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmWriteOn vRep = new frmWriteOn();
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dyeBatchesFabricToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void fabricSalesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void testToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
         
        }

        private void griegeProductionTargetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmKnitViewRep vRep = new frmKnitViewRep(10);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deliveriesToCommissionDyeingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCommissionDeliveries comdel = new frmCommissionDeliveries();
                comdel.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dyeReceipesDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                frmDyeViewReport vRep = new frmDyeViewReport(1);
                vRep.ShowDialog(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printTransactionReportToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyeConsummablesStockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDyeViewReport vRep = new frmDyeViewReport(13);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void outstandingDyeOrderReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmDyeViewReport vRep = new frmDyeViewReport(10);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
        }

        private void dyeOrderReportToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void dyeBatchGreigeStoreToPendingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyeBatchReprintTransferTicketToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void fabricTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmKnitViewRep vRep = new frmKnitViewRep(15);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void printFabricSalesDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dyeBatchPendingToWIPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportOpt2 Opt2  = new frmReportOpt2();
                Opt2.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dyeBatchListOfRejectedDyeBatchToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyeBatchListOfReprocessedBatchesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void fabricSaleDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmFabricSalesDelivery Opt2 = new frmFabricSalesDelivery();
                Opt2.ShowDialog(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void fabricStockOnHandToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyesAndChemicalConsumptionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyeingProcessLossToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyeStockTakeSheetToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void allShadedCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDyeViewReport vRep  = new frmDyeViewReport(26);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void allRemedyCodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDyeViewReport vRep = new frmDyeViewReport(27);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void shadeResultsAfterDyeingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void colourCheckAfterDyeingToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void stabilityCheckAfterDryingToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void colourStabilityCheckAfterCompactingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void nCRResultsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyeConsummableReceiptsToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void dyeConsummablesIntoKitchenToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void fabricProductionInQuaratineStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmDyeProductionSel prodSel = new frmDyeProductionSel();
                prodSel.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void createACutSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCutSheet CutSheet = new frmCutSheet();
                CutSheet.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void receiptACutSheetToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void berriebiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmBerrieb1  CutSheet = new frmBerrieb1();
                CutSheet.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void boxTypesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void receiptABoxToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cutDepartmentMeasurementAreasToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cutDepartmentMeasurementStandardsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void productSpecSheetToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void fabricReturnedToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cutDepartmentTrimsMeasurementToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cutDepartmentFleeceCuffsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cutDepartmentFleeceWaistBandsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void trimsRecordedOnTheCutToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void fleeceCuffsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cutSheetReprintToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void rejectedPanelsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void wIPCuttingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cutProductionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void panelIssueSelectionScreenToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void truckLoadingInstructionToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cMTDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
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

        

        private void panelIssueReceiptToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void panelStoreStockToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void rejectPanelReasonsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void rejectPanellStoreToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void qAResultsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmSelRejectedPanel rejPanel = new frmSelRejectedPanel(2);
                rejPanel.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void standardProductsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void factoryConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCMTViewRep vRep = new frmCMTViewRep(4);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lineConfigurationToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void cMTMeasurementPointsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void stabilityCheckConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmBFADefinition gd = new frmBFADefinition();
                gd.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void finalStabilityCheckToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void garmentDefectCodesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void workCompletedToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cMTStandardProductsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void panelStockAtPanelStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCMTViewRep vRep = new frmCMTViewRep(6);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panelStockAtCMTToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cMTReturnsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void bFARecordingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cMTDefectFlawsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCMTViewRep vRep = new frmCMTViewRep(9);
                vRep.Show(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cMTCurrentMeasurementValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCMTViewRep vRep = new frmCMTViewRep(10);
                vRep.Show(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cMTStockInDespatchCageToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cMTProductionReportByPeriodToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cMTStockInDespatchCageFinishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void panelsIssuedAsYetUndeliveredToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCMTViewRep vRep = new frmCMTViewRep(14);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cMTWorkInProgressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCMTViewRep vRep = new frmCMTViewRep(15);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cMTMeasurementsRecordedToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void dyeHouseStagesToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void shadeResultsAfterDryingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void shadeResultsAfterCompactingToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void lineFeederQualityCheckListToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void transactionTypesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                frmAdminViewRep vRep = new frmAdminViewRep(11);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lineFeederQualityCheckListToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void cMTTransferToWarehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmWareHouseTransfers whseTrans = new frmWareHouseTransfers();
                whseTrans.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void customerOrdersToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void cMTTransferToWarehouseToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void outstandingPickingListsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void warehouseReceiptsexCMTToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void boxSplitToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void salesPickingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmPickList plist = new frmPickList();
                plist.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void outstandingCustomerOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                frmCSViewRep vRep = new frmCSViewRep(5);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             */

            try
            {
                frmDetailCustomerOrders detCustOrders = new frmDetailCustomerOrders();
                detCustOrders.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            
            }
        }

        private void stockQuantitiesOnHandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmStockOnHand SOH = new frmStockOnHand(1);
                SOH.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void boxesInStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmStockOnHand SOH = new frmStockOnHand(2);
                SOH.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void salesDeliveryNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustDeliveries CustDeliveries = new frmCustDeliveries();
                CustDeliveries.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ordersPickedAwaitingDeliveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                frmCSViewRep vRep = new frmCSViewRep(9);
                vRep.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             * */

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

        private void customerSalesReturnsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void stockAdjustmentToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void salesReturnsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void pivotTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmPivotTables pivTables = new frmPivotTables();
                pivTables.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       

        private void replenishmentDefinitionToolStripMenuItem1_Click(object sender, EventArgs e)
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

        private void replenishmentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void replenishmentFinishedGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSelFinishedGoods finishedGoods = new frmSelFinishedGoods();
                finishedGoods.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void replenishmentKnitStockLevelsToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void replenishmentMachineCapacityToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void applicationMethodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmSecuritySections secSections = new frmSecuritySections();
                secSections.ShowDialog(this);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

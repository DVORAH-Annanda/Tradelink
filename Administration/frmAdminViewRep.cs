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

namespace Administration
{
    public partial class frmAdminViewRep : Form
    {
        int _RepNo;
        int _RatingsReport;
        public frmAdminViewRep(int RepNo)
        {
            InitializeComponent();
            _RepNo = RepNo;
        }

        public frmAdminViewRep(int RepNo, int Rating)
        {
            InitializeComponent();
            _RepNo = RepNo;
            _RatingsReport = Rating;
        }


        private void ViewLoad(object sender, EventArgs e)
        {
            if (_RepNo == 1)
            {
                DataSet ds = new DataSet();
                DataSet1.TLADM_CustomerTypesDataTable typesTable = new DataSet1.TLADM_CustomerTypesDataTable();
                using (var context = new TTI2Entities())
                {
                    var Data = context.TLADM_CustomerTypes.OrderBy(x=>x.CT_Description).ToList();
                    foreach (var record in Data)
                    {
                        DataSet1.TLADM_CustomerTypesRow tr = typesTable.NewTLADM_CustomerTypesRow();
                        tr.CT_Description = record.CT_Description;
                        tr.CT_Id = record.CT_Id;
                        tr.CT_ShortCode = record.CT_ShortCode;

                        typesTable.AddTLADM_CustomerTypesRow(tr);
                    }
                }

                ds.Tables.Add(typesTable);
             
                CustomerCategories xtst = new CustomerCategories();
                xtst.SetDataSource(ds);
                crystalReportViewer1.ReportSource = xtst;
            }
            else if (_RepNo == 2)
            {
                DataSet ds = new DataSet();
                DataSet2.TLADM_CustomerFileDataTable custTable = new DataSet2.TLADM_CustomerFileDataTable();
                DataSet2.TLADM_CustomerTypesDataTable typesTable = new DataSet2.TLADM_CustomerTypesDataTable();

                using (var context = new TTI2Entities())
                {
                    var Data = context.TLADM_CustomerTypes.OrderBy(x => x.CT_Description).ToList();
                    foreach (var record in Data)
                    {
                        DataSet2.TLADM_CustomerTypesRow tr = typesTable.NewTLADM_CustomerTypesRow();
                        tr.CT_Description = record.CT_Description;
                        tr.CT_Id = record.CT_Id;
                        tr.CT_ShortCode = record.CT_ShortCode;

                        typesTable.AddTLADM_CustomerTypesRow(tr);
                    }

                    var Cust = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).ToList();
                    foreach (var record in Cust)
                    {
                        DataSet2.TLADM_CustomerFileRow cr = custTable.NewTLADM_CustomerFileRow();
                        cr.Cust_ContactPerson = record.Cust_ContactPerson;
                        cr.Cust_ContactPersonEMail = record.Cust_ContactPersonEmail;
                        cr.Cust_CustomerCat_FK = record.Cust_CustomerCat_FK;
                        cr.Cust_Description = record.Cust_Description;
                        cr.Cust_Pk = record.Cust_Pk;
                        cr.Cust_Telephone = record.Cust_Telephone;
                        cr.Cust_Code = record.Cust_Code;

                        custTable.AddTLADM_CustomerFileRow(cr);
                    }
                }

                ds.Tables.Add(typesTable);
                ds.Tables.Add(custTable);

                CustomerByCategory xtst = new CustomerByCategory();
                xtst.SetDataSource(ds);
                crystalReportViewer1.ReportSource = xtst;
            }
            else if (_RepNo == 3)
            {
                DataSet ds = new DataSet();
                DataSet3.TLADM_StockTypesDataTable typeTable = new DataSet3.TLADM_StockTypesDataTable();

                using (var context = new TTI2Entities())
                {
                    var Data = context.TLADM_StockTypes.OrderBy(x => x.ST_Description);
                    foreach (var row in Data)
                    {
                        DataSet3.TLADM_StockTypesRow tr = typeTable.NewTLADM_StockTypesRow();
                        tr.ST_Description = row.ST_Description;
                        tr.ST_Id = row.ST_Id;
                        tr.ST_ShortCode = row.ST_ShortCode;

                        typeTable.AddTLADM_StockTypesRow(tr);
                    }
                }

                ds.Tables.Add(typeTable);
                SupplierCategories xtst = new SupplierCategories();
                xtst.SetDataSource(ds);
                crystalReportViewer1.ReportSource = xtst;
            }
            else if (_RepNo == 4)
            {
                DataSet ds = new DataSet();
                DataSet4.TLADM_StockTypesDataTable typeTable = new DataSet4.TLADM_StockTypesDataTable();
                DataSet4.TLADM_SuppliersDataTable supplierTable = new DataSet4.TLADM_SuppliersDataTable();

                using (var context = new TTI2Entities())
                {
                    var Data = context.TLADM_ProductTypes.ToList();
                    foreach (var row in Data)
                    {
                        DataSet4.TLADM_StockTypesRow tr = typeTable.NewTLADM_StockTypesRow();
                        tr.ST_Description = row.PT_Description;
                        tr.ST_Id = row.PT_pk;
                        tr.ST_ShortCode = row.PT_ShortCode;

                        typeTable.AddTLADM_StockTypesRow(tr);
                    }

                    var Supplier = context.TLADM_Suppliers.OrderBy(x => x.Sup_Description).ToList();
                    foreach (var record in Supplier)
                    {
                        DataSet4.TLADM_SuppliersRow sr = supplierTable.NewTLADM_SuppliersRow();
                        sr.Suip_ShippingAddress1 = record.Suip_ShippingAddress1;
                        sr.Sup_AllowsConsignment = record.Sup_AllowsConsignment;
                        sr.Sup_AllowsEMail = (bool)record.Sup_AllowsEMail;
                        sr.Sup_Blocked = record.Sup_Blocked;
                        sr.Sup_Code = record.Sup_Code;
                        sr.Sup_ContactPerson = record.Sup_ContactPerson;
                        sr.Sup_Description = record.Sup_Description;
                        sr.Sup_DiscountStructure = record.Sup_DiscountStructure;
                        sr.Sup_EMailContact = record.Sup_EMailContact;
                        sr.Sup_Fax = record.Sup_Fax;
                        sr.Sup_Notes = record.Sup_Notes;
                        sr.Sup_Pk = record.Sup_Pk;
                        sr.Sup_PostalAddress = record.Sup_PostalAddress;
                        sr.Sup_ProductGroups_FK = record.Sup_ProductGroups_FK;
                        sr.Sup_ProductTypes_FK = record.Sup_ProductTypes_FK;
                        sr.Sup_ShippingAddress2 = record.Sup_ShippingAddress2;
                        sr.Sup_ShippingAddress3 = record.Sup_ShippingAddress3;
                        sr.Sup_ShippingAddress4 = record.Sup_ShippingAddress4;
                        sr.Sup_StdPayMentTerms = record.Sup_StdPayMentTerms;
                        sr.Sup_Telephone = record.Sup_Telephone;
                        sr.Sup_VatReference = record.Sup_VatReference;

                        supplierTable.AddTLADM_SuppliersRow(sr);

                    }
                }

                ds.Tables.Add(typeTable);
                ds.Tables.Add(supplierTable);

                Supplierlist rep = new Supplierlist();
                rep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep;
            }
            else if (_RepNo == 5)
            {
                DataSet ds = new DataSet();
                DataSet5.TLADM_DepartmentsDataTable DeptTable = new DataSet5.TLADM_DepartmentsDataTable();
                DataSet5.TLADM_StoreTypesDataTable storeTypeTable = new DataSet5.TLADM_StoreTypesDataTable();
                DataSet5.TLADM_WhseStoreDataTable whseStoreTable = new DataSet5.TLADM_WhseStoreDataTable();


                using (var context = new TTI2Entities())
                {
                    var Dept = context.TLADM_Departments.ToList();
                    foreach (var record in Dept)
                    {
                        DataSet5.TLADM_DepartmentsRow dr = DeptTable.NewTLADM_DepartmentsRow();
                        dr.Dep_Description = record.Dep_Description;
                        dr.Dep_Id = record.Dep_Id;
                        dr.Dep_ShortCode = record.Dep_ShortCode;
                        dr.Dep_UOM = record.Dep_UOM;
                        dr.Dep_PowerN = record.Dep_PowerN;

                        DeptTable.AddTLADM_DepartmentsRow(dr);
                    }

                    var Type = context.TLADM_StoreTypes.ToList();
                    foreach (var record in Type)
                    {
                        DataSet5.TLADM_StoreTypesRow tr = storeTypeTable.NewTLADM_StoreTypesRow();
                        tr.StoreT_Description = record.StoreT_Description;
                        tr.StoreT_Hazardous = record.StoreT_Hazardous;
                        tr.StoreT_Id = record.StoreT_Id;
                        tr.StoreT_ProdTypeFK = record.StoreT_ProdTypeFK;
                        tr.StoreT_ShortCode = record.StoreT_ShortCode;
                        tr.StoreT_UOMFK = record.StoreT_UOMFK;

                        storeTypeTable.AddTLADM_StoreTypesRow(tr);
                    }

                    var whse = context.TLADM_WhseStore.OrderBy(x => x.WhStore_DepartmentFK).ToList();
                    foreach (var record in whse)
                    {
                        DataSet5.TLADM_WhseStoreRow wr = whseStoreTable.NewTLADM_WhseStoreRow();
                        wr.WhStore_Code = record.WhStore_Code;
                        wr.WhStore_DepartmentFK = record.WhStore_DepartmentFK;
                        wr.WhStore_Description = record.WhStore_Description;
                        wr.WhStore_Id = record.WhStore_Id;
                        wr.WhStore_TypeFK = record.WhStore_TypeFK;

                        whseStoreTable.AddTLADM_WhseStoreRow(wr);
                    }
                }

                ds.Tables.Add(DeptTable);
                ds.Tables.Add(storeTypeTable);
                ds.Tables.Add(whseStoreTable);

                WhseStore rep = new WhseStore();
                rep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep;


            }
            else if (_RepNo == 6)
            {
                DataSet ds = new DataSet();
                DataSet6.TLADM_NonStockItemsDataTable nsiTable = new DataSet6.TLADM_NonStockItemsDataTable();
                DataSet6.TLADM_NonStockCatDataTable catTable = new DataSet6.TLADM_NonStockCatDataTable();
                DataSet6.TLADM_UOMDataTable uomTable = new DataSet6.TLADM_UOMDataTable();

                using (var context = new TTI2Entities())
                {
                    var NSI = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Code).ToList();
                    foreach (var record in NSI)
                    {
                        DataSet6.TLADM_NonStockItemsRow nsr = nsiTable.NewTLADM_NonStockItemsRow();
                        nsr.NSI_Category_FK = record.NSI_Category_FK;
                        nsr.NSI_Code = record.NSI_Code;
                        nsr.NSI_Department_PWN = record.NSI_Department_PWN;
                        nsr.NSI_Description = record.NSI_Description;
                        nsr.NSI_Pk = record.NSI_Pk;
                        nsr.NSI_ShowUnitCost = record.NSI_ShowUnitCost;
                        nsr.NSI_StockType_FK = record.NSI_StockType_FK;
                        nsr.NSI_UnitCost = record.NSI_UnitCost;
                        nsr.NSI_UOM_FK = record.NSI_UOM_FK;

                        nsiTable.AddTLADM_NonStockItemsRow(nsr);
                    }

                    var UOM = context.TLADM_UOM.ToList();
                    foreach (var record in UOM)
                    {
                        DataSet6.TLADM_UOMRow uomr = uomTable.NewTLADM_UOMRow();
                        uomr.UOM_Description = record.UOM_Description;
                        uomr.UOM_Pk = record.UOM_Pk;
                        uomr.UOM_ShortCode = record.UOM_ShortCode;

                        uomTable.AddTLADM_UOMRow(uomr);
                    }

                    var CAT = context.TLADM_NonStockCat.ToList();
                    foreach (var record in CAT)
                    {
                        DataSet6.TLADM_NonStockCatRow ncr = catTable.NewTLADM_NonStockCatRow();
                        ncr.NSC_Description = record.NSC_Description;
                        ncr.NSC_Pk = record.NSC_Pk;
                        ncr.NSC_ShortCode = record.NSC_ShortCode;

                        catTable.AddTLADM_NonStockCatRow(ncr);
                    }
                }

                ds.Tables.Add(nsiTable);
                ds.Tables.Add(catTable);
                ds.Tables.Add(uomTable);

                NSIItems rep = new NSIItems();
                rep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep;

            }
            else if (_RepNo == 7)
            {
                DataSet ds = new DataSet();
                DataSet7.TLADM_DepartmentsDataTable DeptsTable = new DataSet7.TLADM_DepartmentsDataTable();
                DataSet7.TLADM_MachineOperatorsDataTable OperatorsTable = new DataSet7.TLADM_MachineOperatorsDataTable();

                using (var context = new TTI2Entities())
                {
                    var DT = context.TLADM_Departments.ToList();
                    foreach (var record in DT)
                    {
                        DataSet7.TLADM_DepartmentsRow dtr = DeptsTable.NewTLADM_DepartmentsRow();
                        dtr.Dep_Description = record.Dep_Description;
                        dtr.Dep_Id = record.Dep_Id;
                        dtr.Dep_PowerN = record.Dep_PowerN;
                        dtr.Dep_ProductType_FK = (int)record.Dep_ProductType_FK;
                        dtr.Dep_ShortCode = record.Dep_ShortCode;
                        dtr.Dep_UOM = record.Dep_UOM;

                        DeptsTable.AddTLADM_DepartmentsRow(dtr);
                    }

                    var OPS = context.TLADM_MachineOperators.OrderBy(x => x.MachOp_Department_FK).ToList();
                    foreach (var record in OPS)
                    {
                        DataSet7.TLADM_MachineOperatorsRow opr = OperatorsTable.NewTLADM_MachineOperatorsRow();
                        opr.MachOp_Code = record.MachOp_Code;
                        opr.MachOp_Department_FK = record.MachOp_Department_FK;
                        opr.MachOp_Description = record.MachOp_Description;
                        opr.MachOp_Inspector = record.MachOp_Inspector;
                        opr.MachOp_Payroll_Code = record.MachOp_Payroll_Code;
                        opr.MachOp_Pk = record.MachOp_Pk;

                        OperatorsTable.AddTLADM_MachineOperatorsRow(opr);
                    }
                }
                ds.Tables.Add(DeptsTable);
                ds.Tables.Add(OperatorsTable);

                OpsByDept rep = new OpsByDept();
                rep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep;

            }
            else if (_RepNo == 8)
            {
                DataSet ds = new DataSet();
                DataSet8.TLADM_DepartmentsDataTable DeptTable = new DataSet8.TLADM_DepartmentsDataTable();
                DataSet8.DataTable1DataTable dataTable1 = new DataSet8.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    var Dept = context.TLADM_Departments.ToList();
                    foreach (var record in Dept)
                    {
                        DataSet8.TLADM_DepartmentsRow dr = DeptTable.NewTLADM_DepartmentsRow();
                        dr.Dep_Description = record.Dep_Description;
                        dr.Dep_Id = record.Dep_Id;
                        dr.Dep_PowerN = record.Dep_PowerN;
                        dr.Dep_ProductType_FK = (int)record.Dep_ProductType_FK;
                        dr.Dep_ShortCode = record.Dep_ShortCode;
                        dr.Dep_UOM = record.Dep_UOM;

                        DeptTable.AddTLADM_DepartmentsRow(dr);
                    }

                    var Mach = context.TLADM_MachineDefinitions.OrderBy(x => x.MD_MachineCode).ToList();
                    foreach (var record in Mach)
                    {
                        DataSet8.DataTable1Row mr = dataTable1.NewDataTable1Row();
                        mr.MD_Code = record.MD_MachineCode;
                        mr.MD_Description = record.MD_Description;
                        mr.MD_Department_FK = record.MD_Department_FK;

                        var dpt = context.TLADM_Departments.Find(record.MD_Department_FK);
                        if (dpt != null)
                        {
                            if (dpt.Dep_ShortCode.Contains("DYE"))
                            {
                                var fab = context.TLADM_FabricProduct.Find(record.MD_FabricType_FK);
                                if (fab != null)
                                    mr.MD_SubCode = fab.FP_Description;
                            }
                            else if (dpt.Dep_ShortCode.Contains("KNIT")) 
                            {
                                var greig = context.TLADM_Griege.Find(record.MD_GreigeType_FK);
                                if (greig != null)
                                {
                                    mr.MD_SubCode = greig.TLGreige_Description;
                                    
                                }
                            }
                            else if (dpt.Dep_ShortCode.Contains("SPIN"))
                            {
                                var fab = context.TLADM_FabricProduct.Find(record.MD_FabricType_FK);
                                if (fab != null)
                                    mr.MD_SubCode = fab.FP_Description;
                            }
                        }
                        
                        mr.MD_CapacityMax = record.MD_MaxCapacity;
                        mr.MD_CapacityRealistic = record.MD_Realistic;
                        mr.MD_UOM = "Kg";
                        mr.MD_Measure1 = (int)record.MD_FirstMeasure_Qty;
                        if(record.MD_SecMeasure_Qty != null)
                        mr.MD_Measure2 = (int)record.MD_SecMeasure_Qty;
                        if(record.MD_ThirdMeasure_Qty != null)
                            mr.MD_Measure3 = (int)record.MD_ThirdMeasure_Qty;

                       // mr.MD_SerialNo = record.MD_SerialNo;

                        mr.MD_SerialNo = record.MD_LastNumberUsed.ToString();

                        dataTable1.AddDataTable1Row(mr);

                    }
                }

                ds.Tables.Add(DeptTable);
                ds.Tables.Add(dataTable1);
                ListOfMachines rep = new ListOfMachines();
                rep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep;

            }
            else if (_RepNo == 9)
            {
                DataSet ds = new DataSet();
                DataSet9.TLADM_ConsumablesDCDataTable dataTable = new DataSet9.TLADM_ConsumablesDCDataTable();
                DataSet9.TLADM_UOMDataTable uomTable = new DataSet9.TLADM_UOMDataTable();

                using (var context = new TTI2Entities())
                {
                    var ExistingData = context.TLADM_ConsumablesDC.OrderBy(x=>x.ConsDC_Code).ToList();
                    foreach (var row in ExistingData)
                    {
                        DataSet9.TLADM_ConsumablesDCRow dcr = dataTable.NewTLADM_ConsumablesDCRow();
                        dcr.ConsDC_Code = row.ConsDC_Code;
                        dcr.ConsDC_Description = row.ConsDC_Description;
                        dcr.ConsDC_Economic_ReOrderQty = row.ConsDC_Economic_ReOrderQty;
                        dcr.ConsDC_MinReorderQty = row.ConsDC_MinReorderQty;
                        dcr.ConsDC_Pk = row.ConsDC_Pk;
                        dcr.ConsDC_StandardCost = row.ConsDC_StandardCost;
                        dcr.ConsDC_UOM_Fk = row.ConsDC_UOM_Fk;
                        dcr.ConsDC_ReOrderLevel = row.ConsDC_ReOrderLevel;
                        dcr.ConsDC_Discontinued = row.ConsDC_Discontinued;
                        if (row.ConsDC_DiscontinuedDate != null)
                            dcr.ConsDC_DiscontinuedDate = (DateTime)row.ConsDC_DiscontinuedDate;

                        dataTable.AddTLADM_ConsumablesDCRow(dcr);
                    }

                    var UOM = context.TLADM_UOM.ToList();
                    foreach (var row in UOM) 
                    {
                        DataSet9.TLADM_UOMRow ur = uomTable.NewTLADM_UOMRow();
                        ur.UOM_Description = row.UOM_Description;
                        ur.UOM_Pk = row.UOM_Pk;
                        ur.UOM_ShortCode = row.UOM_ShortCode;

                        uomTable.AddTLADM_UOMRow(ur);
                    }

                }

                ds.Tables.Add(dataTable);
                ds.Tables.Add(uomTable);

                ConsumableStock rep = new ConsumableStock();
                rep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep;


            }
            else if (_RepNo == 10)
            {
                DataSet ds = new DataSet();
                DataSet10.TLADM_ConsumablesOtherDataTable dataTable = new DataSet10.TLADM_ConsumablesOtherDataTable();
                DataSet10.TLADM_UOMDataTable uomTable = new DataSet10.TLADM_UOMDataTable();
                DataSet10.TLADM_StockTypesDataTable stockTable = new DataSet10.TLADM_StockTypesDataTable();

                using (var context = new TTI2Entities())
                {
                    var ExistingData = context.TLADM_ConsumablesOther.OrderBy(x => x.ConsOther_Code).ToList();
                    foreach (var row in ExistingData)
                    {
                        DataSet10.TLADM_ConsumablesOtherRow cor = dataTable.NewTLADM_ConsumablesOtherRow();
                        cor.ConsOther_Code = row.ConsOther_Code;
                        cor.ConsOther_Description = row.ConsOther_Description;
                        cor.ConsOther_EconomicReOrderQty = row.ConsOther_EconomicReOrderQty;
                        cor.ConsOther_Pk = row.ConsOther_Pk;
                        cor.ConsOther_ReOrderLevel = row.ConsOther_ReOrderLevel;
                        cor.ConsOther_StdCost = row.ConsOther_StdCost;
                        cor.ConsOther_StockType_Fk = row.ConsOther_StockType_Fk;
                        cor.ConsOther_UOM_FK = row.ConsOther_UOM_FK;

                        dataTable.AddTLADM_ConsumablesOtherRow(cor);
                       
                    }

                    var UOM = context.TLADM_UOM.ToList();
                    foreach (var row in UOM)
                    {
                        DataSet10.TLADM_UOMRow ur = uomTable.NewTLADM_UOMRow();
                        ur.UOM_Description = row.UOM_Description;
                        ur.UOM_Pk = row.UOM_Pk;
                        ur.UOM_ShortCode = row.UOM_ShortCode;

                        uomTable.AddTLADM_UOMRow(ur);
                    }

                    var ST = context.TLADM_StockTypes.ToList();
                    foreach (var row in ST)
                    {
                        DataSet10.TLADM_StockTypesRow tr = stockTable.NewTLADM_StockTypesRow();
                        tr.ST_Id = row.ST_Id;
                        tr.ST_ShortCode = row.ST_ShortCode;
                        tr.ST_Description = row.ST_Description;

                        stockTable.AddTLADM_StockTypesRow(tr);
                    }

                }

                ds.Tables.Add(dataTable);
                ds.Tables.Add(uomTable);
                ds.Tables.Add(stockTable);

                ConsumableStockOther rep = new ConsumableStockOther();
                rep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = rep;


            }
            else if (_RepNo == 11)
            {
                DataSet ds = new DataSet();
                DataSet11.DataTable1DataTable dataTable1 = new DataSet11.DataTable1DataTable();

                using (var context = new TTI2Entities())
                {
                    var Existing = context.TLADM_TranactionType.OrderBy(x => x.TrxT_Department_FK).ThenBy(x=>x.TrxT_Number).ToList();

                    foreach (var Department in Existing)
                    {
                        DataSet11.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Department = context.TLADM_Departments.Find(Department.TrxT_Department_FK).Dep_Description;
                        nr.TransNumber = Department.TrxT_Number;
                        nr.TransDescription = Department.TrxT_Description;
                        nr.FromWareHouse = context.TLADM_WhseStore.Find(Department.TrxT_FromWhse_FK).WhStore_Description;
                        nr.ToWareHouse = context.TLADM_WhseStore.Find(Department.TrxT_ToWhse_FK).WhStore_Description;
                        dataTable1.AddDataTable1Row(nr);
                    }
                }

                ds.Tables.Add(dataTable1);
                TransTypes TransTypes = new TransTypes();
                TransTypes.SetDataSource(ds);
                crystalReportViewer1.ReportSource = TransTypes;

            }
            else if (_RepNo == 12)
            {
                DataSet ds = new DataSet();
                DataSet12.DataTable1DataTable dataTable1 = new DataSet12.DataTable1DataTable();
                DataSet12.DataTable2DataTable dataTable2 = new DataSet12.DataTable2DataTable();
                Util core = new Util();
                // IList<int> Sizes = null;
                IList<TLADM_ProductRating> ProductRatings = null;

                using (var context = new TTI2Entities())
                {
                    ProductRatings = context.TLADM_ProductRating.OrderBy(x => x.Pr_Customer_FK).ToList();
                    if (_RatingsReport == 1)
                    {
                        ProductRatings = ProductRatings.Where(x=>!(bool)x.Pr_Discontinued).ToList();
                    }
                    else if(_RatingsReport == 2)
                    {
                        ProductRatings = ProductRatings.Where(x => (bool)x.Pr_Discontinued).ToList();
                    }
                    
                    ProductRatings = ProductRatings.OrderByDescending(x => x.Pr_BodyorRibbing).ToList();

                    foreach (var ProductRating in ProductRatings)
                    {
                        DataSet12.DataTable1Row nr = dataTable1.NewDataTable1Row();
                        nr.Pk = ProductRating.Pr_Id;
                        var Customer  = context.TLADM_CustomerFile.Find(ProductRating.Pr_Customer_FK);
                        if (Customer == null)
                            continue;
                        
                        nr.Customer = Customer.Cust_Description;
                        
                        nr.Style = context.TLADM_Styles.Find(ProductRating.Pr_Style_FK).Sty_Description;

                        if (ProductRating.Pr_BodyorRibbing == 1)
                        {
                            StringBuilder sb = new StringBuilder();
                            int cnt = 0;
                            
                            List<int> xx = core.ExtrapNumber(ProductRating.Pr_PowerN, context.TLADM_Sizes.Count());
                            xx.Sort();

                            foreach (var Size in xx)
                            {
                                var SI = context.TLADM_Sizes.Where(x => x.SI_PowerN == Size).FirstOrDefault();
                                if (SI != null)
                                {
                                    sb.Append(SI.SI_Description);
                                }

                                if (++cnt < xx.Count)
                                {
                                    sb.Append(",");
                                }
                            }
                            nr.Display = sb.ToString();
                        }
                        else
                        {
                            nr.Display = context.TLADM_Trims.Find(ProductRating.Pr_Trim_FK).TR_Description;  
                        }

                        nr.Ratio = ProductRating.Pr_Ratio;
                        nr.Marker_Length = ProductRating.Pr_Marker_Length;
                        nr.Numeric_Rating = ProductRating.Pr_numeric_Rating;
                        if (ProductRating.Pr_BodyorRibbing == 1)
                            nr.Body = true;
                        else
                            nr.Body = false;

                        dataTable1.AddDataTable1Row(nr);

                        var Details = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == ProductRating.Pr_Id).ToList();
                        foreach (var Detail in Details)
                        {
                            DataSet12.DataTable2Row xnr = dataTable2.NewDataTable2Row();
                            xnr.Pk = ProductRating.Pr_Id;
                            xnr.Size = context.TLADM_Sizes.Find(Detail.Prd_SizePN).SI_Description;
                            xnr.Ratio = Detail.Prd_MarkerRatio;

                            dataTable2.AddDataTable2Row(xnr);
                        }


                    }
                }

                ds.Tables.Add(dataTable1);
                ds.Tables.Add(dataTable2);

                ProductRating TransTypes = new ProductRating();
                TransTypes.SetDataSource(ds);
                crystalReportViewer1.ReportSource = TransTypes;

            }
            crystalReportViewer1.Refresh();
        }
    }
}

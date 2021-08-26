using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LinqKit;
using System.Reflection;
using System.Data;

namespace CustomerServices
{
    public class Repository : IDisposable
    {
        protected readonly TTI2Entities _context;

        public Repository()
        {
            _context = new TTI2Entities();
        }

        public TLCSV_StockOnHand LoadStockOnHand(int Pk)
        {
            return _context.TLCSV_StockOnHand.FirstOrDefault(s => s.TLSOH_Pk == Pk);
        }

        public TLCSV_StockOnHand LoadPickingList(int Pk)
        {
            return _context.TLCSV_StockOnHand.FirstOrDefault(s => s.TLSOH_PickListNo == Pk);
        }

        public TLCSV_OrderAllocated LoadOrderAllocated(int Pk)
        {
            return _context.TLCSV_OrderAllocated.FirstOrDefault(s => s.TLORDA_Pk == Pk);
        }

        public TLCSV_RePackConfig LoadRePackConfig(int Pk)
        {
            return _context.TLCSV_RePackConfig.FirstOrDefault(s => s.PORConfig_BoxNumber_Key  == Pk);
        }

        public TLADM_CustomerFile LoadCustomers(int Pk)
        {
            return _context.TLADM_CustomerFile.FirstOrDefault(s => s.Cust_Pk == Pk);    
        }

        public TLCSV_PurchaseOrder LoadPurchaseOrder(int Pk)
        {
            return _context.TLCSV_PurchaseOrder.FirstOrDefault(s => s.TLCSVPO_Pk == Pk);
        }

        public List<TLADM_Colours> LoadColours()
        {
            return _context.TLADM_Colours.ToList();
        }

        public List<TLADM_Sizes> LoadSizes()
        {
            return _context.TLADM_Sizes.ToList();
        }

       
        public List<TLADM_Styles> LoadStyles()
        {
            return _context.TLADM_Styles.ToList(); 
        }

        public TLADM_Months LoadMonths(int Pk)
        {
            return _context.TLADM_Months.FirstOrDefault(s => s.Mth_Pk == Pk);
        }
        public TLADM_Colours LoadColour(int Pk)
        {
            return _context.TLADM_Colours.FirstOrDefault(s => s.Col_Id == Pk);
        }

        public TLADM_Sizes LoadSize(int Pk)
        {
            return _context.TLADM_Sizes.FirstOrDefault(s => s.SI_id == Pk);
        }

        public TLADM_Styles LoadStyle(int Pk)
        {
            return _context.TLADM_Styles.FirstOrDefault(s => s.Sty_Id == Pk);
        }

        public TLADM_WhseStore LoadWhse(int Pk)
        {
            return _context.TLADM_WhseStore.FirstOrDefault(s => s.WhStore_Id == Pk);
        }
        
        public TLADM_Departments LoadDepart(int Pk)
        {
            return _context.TLADM_Departments.FirstOrDefault(s => s.Dep_Id == Pk);
        }

        public TLSEC_UserAccess LoadUserAccess(int Pk)
        {
            return _context.TLSEC_UserAccess.FirstOrDefault(s => s.TLSECUA_Pk == Pk);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
        public IQueryable<TLCSV_StockOnHand> PastelRecon(CustomerServicesParameters parameters)
        {
            var FmWhse = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Sold && x.TLSOH_SoldDate >= parameters.FromDate && x.TLSOH_SoldDate <= parameters.ToDate).AsQueryable();

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                FmWhse = FmWhse.AsExpandable().Where(stylePredicate);
            }

            return FmWhse;
        }

        public IQueryable<TLCSV_StockOnHand> FromWareHouse(CustomerServicesParameters parameters)
        {
            var FmWhse = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_WareHouse_FK == parameters.FromWhse && !x.TLSOH_Picked).AsQueryable();

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                FmWhse = FmWhse.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                FmWhse = FmWhse.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                FmWhse = FmWhse.AsExpandable().Where(sizePredicate);
            }
            return FmWhse;
        }

        public IQueryable<TLCSV_OrderAllocated> OrderAllocatedQuery(CustomerServicesParameters parameters)
        {
            var OrderAlloc = _context.TLCSV_OrderAllocated.Where(x=>x.TLORDA_PickListPrint && !x.TLORDA_Delivered).AsQueryable();
            if (parameters.Customers.Count() > 0)
            {
                var custPredicate = PredicateBuilder.New<TLCSV_OrderAllocated>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    custPredicate = custPredicate.Or(s => s.TLORDA_Customer_FK == temp.Cust_Pk);
                }

                OrderAlloc = OrderAlloc.AsExpandable().Where(custPredicate);

            }

            /*
            if (parameters.Whses.Count() > 0)
            {

                var whsePredicate = PredicateBuilder.New<TLCSV_OrderAllocated>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLORDA_WareHouse_FK == temp.WhStore_Id);
                }

                OrderAlloc = OrderAlloc.AsExpandable().Where(whsePredicate);
            }*/


            if (parameters.OrdersAllocated.Count() > 0)
            {
                var orderPredicate = PredicateBuilder.New<TLCSV_OrderAllocated>();
                foreach (var orderp in parameters.OrdersAllocated)
                {
                    var temp = orderp;
                    orderPredicate = orderPredicate.Or(s => s.TLORDA_Pk == temp.TLORDA_Pk);
                }

                OrderAlloc = OrderAlloc.AsExpandable().Where(orderPredicate); 
            }

            return OrderAlloc;
        }

        public IQueryable<TLCMT_CompletedWork> SelectNotReceipted(CustomerServicesParameters parameters)
        {
            var CW = _context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_Despatched && !x.TLCMTWC_BoxReceiptedWhse).AsQueryable();

            if (parameters.Whses.Count != 0)
            {
                var WhsePredicate = PredicateBuilder.New<TLCMT_CompletedWork>();

                foreach (var Whse in parameters.Whses)
                {
                    var temp = Whse;
                    WhsePredicate = WhsePredicate.Or(s => s.TLCMTWC_ToWhse_FK == temp.WhStore_Id);
                }
                CW = CW.AsExpandable().Where(WhsePredicate);
            }

            return CW;
           
        }

        public IQueryable<TLCSV_PuchaseOrderDetail> POQuery(CustomerServicesParameters parameters)
        {
            var POD = _context.TLCSV_PuchaseOrderDetail.Where(x => !x.TLCUSTO_Picked && !x.TLCUSTO_Closed && !x.TLCUSTO_Invoiced && !x.TLCUSTO_Delivered).AsQueryable();
            POD = (from i in POD
                   join o in _context.TLADM_Sizes
                   on i.TLCUSTO_Size_FK equals o.SI_id
                   orderby o.SI_DisplayOrder
                   select i);

            if (parameters.Customers.Count() > 0)
            {
                var custPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach(var cust in parameters.Customers)
                {
                    var temp = cust;
                    custPredicate = custPredicate.Or(s => s.TLCUSTO_Customer_FK == temp.Cust_Pk);
                }
                POD = POD.AsExpandable().Where(custPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLCUSTO_Style_FK == temp.Sty_Id);
                }

                POD = POD.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLCUSTO_Colour_FK == temp.Col_Id);
                }

                POD = POD.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLCUSTO_Size_FK == temp.SI_id);
                }

                POD = POD.AsExpandable().Where(sizePredicate);
            }

            return POD;
     
       }

        public IQueryable<TLCSV_PuchaseOrderDetail> WholeSaleOutStandingOrders(CustomerServicesParameters parameters)
        {
            var POD = (from T1 in _context.TLCSV_PurchaseOrder
                       join T2 in _context.TLCSV_PuchaseOrderDetail
                       on T1.TLCSVPO_Pk equals T2.TLCUSTO_PurchaseOrder_FK
                       join T3 in _context.TLADM_CustomerFile 
                       on T1.TLCSVPO_Customer_FK equals T3.Cust_Pk 
                       join T4 in _context.TLADM_CustomerTypes 
                       on T3.Cust_CustomerCat_FK equals T4.CT_Id 
                       where !T1.TLCSVPO_Closeed && !T2.TLCUSTO_Closed && T4.CT_ShortCode == "WS" && !T1.TLCSVPO_Provisional
                       select T2).AsQueryable();
            
            if (parameters.Customers.Count() > 0)
            {
                var custPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    custPredicate = custPredicate.Or(s => s.TLCUSTO_Customer_FK == temp.Cust_Pk);
                }
                POD = POD.AsExpandable().Where(custPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLCUSTO_Style_FK == temp.Sty_Id);
                }
                POD = POD.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLCUSTO_Colour_FK == temp.Col_Id);
                }
                POD = POD.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLCUSTO_Size_FK == temp.SI_id);
                }

                POD = POD.AsExpandable().Where(sizePredicate);
            }
         
            return POD;
       }

       public IQueryable<TLCSV_PuchaseOrderDetail> OutStandingOrders(CustomerServicesParameters parameters)
       {
           var POD = (from T1 in _context.TLCSV_PurchaseOrder
                      join T2 in _context.TLCSV_PuchaseOrderDetail
                      on T1.TLCSVPO_Pk equals T2.TLCUSTO_PurchaseOrder_FK
                      where !T1.TLCSVPO_Closeed && !T2.TLCUSTO_Closed && T2.TLCUSTO_DateRequired != null
                      select T2).AsQueryable();

            if (parameters.Customers.Count() > 0)
            {
                var custPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    custPredicate = custPredicate.Or(s => s.TLCUSTO_Customer_FK == temp.Cust_Pk);
                }
                POD = POD.AsExpandable().Where(custPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLCUSTO_Style_FK == temp.Sty_Id);
                }
                POD = POD.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLCUSTO_Colour_FK == temp.Col_Id);
                }
                POD = POD.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLCUSTO_Size_FK == temp.SI_id);
                }

                POD = POD.AsExpandable().Where(sizePredicate);
            }

            return POD;

       }

        public IQueryable<TLADM_WhseStore> SelWhse(CustomerServicesParameters parameters)
        {
            var Whse = _context.TLADM_WhseStore.Where(x => x.WhStore_WhseOrStore && x.WhStore_GradeA == parameters.GradeA).AsQueryable();
            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLADM_WhseStore>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.WhStore_Id == temp.WhStore_Id);
                }
                Whse = Whse.AsExpandable().Where(whsePredicate);
            }
            return Whse;
        }

        public IQueryable<TLCSV_StockOnHand> RePacSOHGradeAQuery(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Picked && x.TLSOH_Grade.Contains("A")
                && !x.TLSOH_InTransit && !x.TLSOH_Write_Off && x.TLSOH_BoxedQty > 0).AsQueryable();

            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var Size in parameters.Sizes)
                {
                    var temp = Size;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }

        public IQueryable<TLCSV_StockOnHand> SOHGradeAQuery(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Picked && x.TLSOH_Grade.Contains("A")
                && !x.TLSOH_Split && !x.TLSOH_InTransit && !x.TLSOH_Write_Off).AsQueryable();

            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var Size in parameters.Sizes)
                {
                    var temp = Size;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }
        public IQueryable<TLCSV_StockOnHand> SOHBoxesSplit(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Split).AsQueryable();

            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }

        public IQueryable<TLCSV_StockOnHand> SOHBoxesReturned(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Returned).AsQueryable();

            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }

        public IQueryable<TLCSV_StockOnHand> SOHOnHand(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Sold 
                                                            && !x.TLSOH_Split
                                                            && !x.TLSOH_InTransit 
                                                            && !x.TLSOH_Write_Off 
                                                            && !x.TLSOH_Returned).AsQueryable();
           
            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            /*
            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustPredicate);
            }
             */
 
            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var Colour in parameters.Colours)
                {
                    var temp = Colour;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var Size in parameters.Sizes)
                {
                    var temp = Size;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }

        public IQueryable<TLCSV_StockOnHand> SOHOnHandSplit(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Sold && x.TLSOH_Split && !x.TLSOH_InTransit && !x.TLSOH_Write_Off).AsQueryable();

            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }
        public IQueryable<TLCSV_StockOnHand> SOHStockTakeQuery(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Sold && !x.TLSOH_Split && !x.TLSOH_Write_Off).AsQueryable();
            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }
                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }
                SOH = SOH.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }
                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }
                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }
                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }

        public IQueryable<TLCSV_StockOnHand> CustomerAudit(CustomerServicesParameters parameters, int Customer_Pk)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Customer_Fk == Customer_Pk && x.TLSOH_Sold).AsQueryable();
           if(parameters.PurchaseOrders.Count() > 0 )
            {
                var POPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var POOrder in parameters.PurchaseOrders)
                {
                    var temp = POOrder;
                    POPredicate = POPredicate.Or(s => s.TLSOH_POOrder_FK == temp.TLCSVPO_Pk);
                }
                SOH = SOH.AsExpandable().Where(POPredicate);
            }
            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }
                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }
                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }
                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }
        public IQueryable<TLCSV_StockOnHand> SOHQuery(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Picked && !x.TLSOH_Split).AsQueryable();
            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }

        public IQueryable<TLCSV_StockOnHand>SOHSales(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x =>x.TLSOH_Sold && x.TLSOH_SoldDate >= parameters.FromDate && x.TLSOH_SoldDate <= parameters.ToDate).AsQueryable();
            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }
            
            if (parameters.Customers.Count() > 0)
            {
                var CustomerPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var Customer in parameters.Customers)
                {
                    var temp = Customer;
                    CustomerPredicate = CustomerPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustomerPredicate);
            }           

            return SOH;
        }

        public IQueryable<TLCSV_StockOnHand> GrossSOHQuery(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x => !x.TLSOH_Sold && !x.TLSOH_Split && !x.TLSOH_Write_Off && !x.TLSOH_Returned && !x.TLSOH_InTransit).AsQueryable();
            SOH = (from i in SOH
                            join o in _context.TLADM_Sizes
                            on i.TLSOH_Size_FK equals o.SI_id
                            orderby o.SI_DisplayOrder
                            select i);


            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }
        public IQueryable<TLCSV_StockOnHand> SoldQuery(CustomerServicesParameters parameters)
        {
            var SOHSortOrder = from T1 in _context.TLCSV_StockOnHand
                               join T2 in _context.TLADM_Styles on T1.TLSOH_Style_FK equals T2.Sty_Id
                               join T3 in _context.TLADM_Colours on T1.TLSOH_Colour_FK equals T3.Col_Id
                               join T4 in _context.TLADM_Sizes on T1.TLSOH_Size_FK equals T4.SI_id
                               join T5 in _context.TLADM_CustomerFile on T1.TLSOH_Customer_Fk equals T5.Cust_Pk
                               where T1.TLSOH_Sold
                               orderby T5.Cust_Description, T2.Sty_Description, T3.Col_Display, T4.SI_DisplayOrder
                               select T1;

            var SOH = SOHSortOrder.AsQueryable();
            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var cust in parameters.Customers)
                {
                    var temp = cust;
                    CustPredicate = CustPredicate.Or(s => s.TLSOH_Customer_Fk == temp.Cust_Pk);
                }

                SOH = SOH.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                SOH = SOH.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                SOH = SOH.AsExpandable().Where(sizePredicate);
            }

            return SOH;
        }

        public IQueryable<TLCSV_StockOnHand> StockAdjustments(CustomerServicesParameters parameters)
        {
            var SOH = _context.TLCSV_StockOnHand.Where(x=>!x.TLSOH_Picked || (x.TLSOH_Returned && !x.TLSOH_Sold)).AsQueryable();
            if (parameters.Whses.Count() > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    whsePredicate = whsePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                SOH = SOH.AsExpandable().Where(whsePredicate);
            }

           return SOH;
        }

        public IQueryable<TLCSV_PuchaseOrderDetail> OutstandingPOOrderDetails ( CustomerServicesParameters parameters)
        {
            var POD = _context.TLCSV_PuchaseOrderDetail.Where(x=>!x.TLCUSTO_Delivered && !x.TLCUSTO_Closed).AsQueryable();

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var Cust in parameters.Customers)
                {
                    var temp = Cust;
                    CustPredicate = CustPredicate.Or(s => s.TLCUSTO_Customer_FK == temp.Cust_Pk);
                }

                POD = POD.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var StylePredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var Style in parameters.Styles)
                {
                    var temp = Style;
                    StylePredicate = StylePredicate.Or(s => s.TLCUSTO_Style_FK == temp.Sty_Id);
                }

                POD = POD.AsExpandable().Where(StylePredicate);
            }

            return POD;
        }
        public IQueryable<TLCSV_PurchaseOrder>PurchaseOrder(CustomerServicesParameters parameters)
        {
            IQueryable<TLCSV_PurchaseOrder> PO = null;
            if (!parameters.Both)
            {
                PO = _context.TLCSV_PurchaseOrder.Where(x => !x.TLCSVPO_Closeed && x.TLCSVPO_Provisional == parameters.IncludeProvisional).AsQueryable();
            }
            else
            {
                PO = _context.TLCSV_PurchaseOrder.Where(x => !x.TLCSVPO_Closeed).AsQueryable();
            }

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_PurchaseOrder>();
                foreach (var Cust in parameters.Customers)
                {
                    var temp = Cust;
                    CustPredicate = CustPredicate.Or(s => s.TLCSVPO_Customer_FK == temp.Cust_Pk);
                }

                PO = PO.AsExpandable().Where(CustPredicate);
            }

            return PO;
        }

        public IQueryable<TLCSV_PuchaseOrderDetail> POPListQuery(CustomerServicesParameters parameters)
        {
            var POD = _context.TLCSV_PuchaseOrderDetail.Where(x=>x.TLCUSTO_Picked && !x.TLCUSTO_Delivered).AsQueryable();

            if (parameters.Customers.Count() > 0)
            {
                var CustPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var Cust in parameters.Customers)
                {
                    var temp = Cust;
                    CustPredicate = CustPredicate.Or(s => s.TLCUSTO_Customer_FK == temp.Cust_Pk);
                }

                POD = POD.AsExpandable().Where(CustPredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var StylePredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var Style in parameters.Styles)
                {
                    var temp = Style;
                    StylePredicate = StylePredicate.Or(s => s.TLCUSTO_Style_FK == temp.Sty_Id);
                }

                POD = POD.AsExpandable().Where(StylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var ColourPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var Colour in parameters.Colours)
                {
                    var temp = Colour;
                    ColourPredicate = ColourPredicate.Or(s => s.TLCUSTO_Colour_FK == temp.Col_Id);
                }

                POD = POD.AsExpandable().Where(ColourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var SizesPredicate = PredicateBuilder.New<TLCSV_PuchaseOrderDetail>();
                foreach (var xSize in parameters.Sizes)
                {
                    var temp = xSize;
                    SizesPredicate = SizesPredicate.Or(s => s.TLCUSTO_Size_FK == temp.SI_id);
                }

                POD = POD.AsExpandable().Where(SizesPredicate);
            }
            return POD;
        }

        public IQueryable<TLCSV_StockOnHand>PendingPS(CustomerServicesParameters parameters)
        {
            var POD = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Picked && !x.TLSOH_Sold).AsQueryable();
            
            if (parameters.Whses.Count() > 0)
            {
                var StylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var Whse in parameters.Whses)
                {
                    var temp = Whse;
                    StylePredicate = StylePredicate.Or(s => s.TLSOH_WareHouse_FK == temp.WhStore_Id);
                }

                POD = POD.AsExpandable().Where(StylePredicate);
            }

            if (parameters.Styles.Count() > 0)
            {
                var StylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var Style in parameters.Styles)
                {
                    var temp = Style;
                    StylePredicate = StylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                POD = POD.AsExpandable().Where(StylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var ColourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var Colour in parameters.Colours)
                {
                    var temp = Colour;
                    ColourPredicate = ColourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                POD = POD.AsExpandable().Where(ColourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var SizesPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                foreach (var xSize in parameters.Sizes)
                {
                    var temp = xSize;
                    SizesPredicate = SizesPredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                POD = POD.AsExpandable().Where(SizesPredicate);
            }
            return POD;
        }

        public IQueryable<TLCMT_CompletedWork> Query(CustomerServicesParameters parameters)
        {
            var completedWork = (from LI in _context.TLCMT_LineIssue 
                     join CW in _context.TLCMT_CompletedWork on LI.TLCMTLI_CutSheet_FK equals CW.TLCMTWC_CutSheet_FK 
                     where LI.TLCMTLI_WorkCompleted && !CW.TLCMTWC_Picked
                     select CW).AsQueryable();

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.New<TLCMT_CompletedWork>();
                foreach (var size in parameters.Sizes)
                {
                    var temp = size;
                    sizePredicate = sizePredicate.Or(s => s.TLCMTWC_Size_FK == temp.SI_id);
                }

                completedWork = completedWork.AsExpandable().Where(sizePredicate);
            }

            if (parameters.Colours.Count > 0)
            {
                var colourPredicate = PredicateBuilder.New<TLCMT_CompletedWork>();
                foreach (var colour in parameters.Colours)
                {
                    var temp = colour;
                    colourPredicate = colourPredicate.Or(s => s.TLCMTWC_Colour_FK == temp.Col_Id);
                }

                completedWork = completedWork.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Styles.Count > 0)
            {
                var stylePredicate = PredicateBuilder.New<TLCMT_CompletedWork>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLCMTWC_Style_FK == temp.Sty_Id);
                       
                }

                completedWork = completedWork.AsExpandable().Where(stylePredicate);
            }
            
            if (parameters.Whses.Count > 0)
            {
                var whsePredicate = PredicateBuilder.New<TLCMT_CompletedWork>();
                foreach (var whse in parameters.Whses)
                {
                    var temp = whse;
                    // whsePredicate = whsePredicate.Or(s=>s.TLCMTWC_ToWhse_FK == temp.WhStore_Id);
                }

                completedWork = completedWork.AsExpandable().Where(whsePredicate);
            }
            

            if (parameters.Depts.Count > 0)
            {
                var deptPredicate = PredicateBuilder.New<TLCMT_CompletedWork>();
                foreach (var dept in parameters.Depts)
                {
                    var temp = dept;
                    deptPredicate = deptPredicate.Or(s => s.TLCMTWC_CMTFacility_FK == temp.Dep_Id);
                }

                completedWork = completedWork.AsExpandable().Where(deptPredicate);
            }
            return completedWork;
        }

        
    }

    public static class MyExtentions
    {
        public static object ToNonAnonymousList<T>(this List<T> list, Type t)
        {
            //define system Type representing List of objects of T type:
            Type genericType = typeof(List<>).MakeGenericType(t);

            //create an object instance of defined type:
            object l = Activator.CreateInstance(genericType);

            //get method Add from from the list:
            MethodInfo addMethod = l.GetType().GetMethod("Add");

            //loop through the calling list:
            foreach (T item in list)
            {
                //convert each object of the list into T object by calling extension ToType<T>()
                //Add this object to newly created list:
                addMethod.Invoke(l, new[] { item.ToType(t) });
            }
            //return List of T objects:
            return l;
        }

        public static object ToType<T>(this object obj, T type)
        {
            //create instance of T type object:
            object tmp = Activator.CreateInstance(Type.GetType(type.ToString()));

            //loop through the properties of the object you want to covert:          
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                try
                {
                    //get the value of property and try to assign it to the property of T type object:
                    tmp.GetType().GetProperty(pi.Name).SetValue(tmp, pi.GetValue(obj, null), null);
                }
                catch (Exception ex)
                {
                    //Logging.Log.Error(ex);
                }
            }
            //return the T type object:         
            return tmp;
        }
    }
        
   
    public class CustomerServicesParameters
    {
        public List<TLADM_Sizes> Sizes;
        public List<TLADM_Colours> Colours;
        public List<TLADM_Styles> Styles;
        public List<TLADM_WhseStore> Whses;
        public List<TLADM_Departments> Depts;
        public List<TLADM_CustomerFile> Customers;
        public List<TLCSV_OrderAllocated> OrdersAllocated;
        public List<TLCSV_StockOnHand> StockOnHand;
        public List<TLCSV_StockOnHand> PendingPickingSlips;
        public List<TLCSV_RePackConfig> RePackConfigs;
        public List<TLCSV_PurchaseOrder> PurchaseOrders;
        public List<TLADM_Months> Months;
        public List<TLSEC_UserAccess> UserAccesses;
        public int FromWhse;
        public int ToWhse;
        public bool GradeA;
        public DateTime FromDate;
        public DateTime ToDate;

        public bool AllPurchaseOrders;
        public bool ClosedOrders;
        public bool SummarisedPurchaseOrders;
        public bool SummarisedSalesByCustomer;
        public bool SummarisedSalesByCompany;
        public bool RankedByStyleColour;
        public bool RankedByStyleColourSize;
        public bool RankedByStyleSize;
        public bool Discontinued;
        public bool GroupByWeek;
        public bool IncludeProvisional;
        public bool Both;
        public bool TransactHistory;
 
        public CustomerServicesParameters()
        {
            Sizes = new List <TLADM_Sizes>();
            Colours = new List<TLADM_Colours>();
            Styles = new List<TLADM_Styles>();
            Whses = new List<TLADM_WhseStore>();
            Depts = new List<TLADM_Departments>();
            Customers = new List<TLADM_CustomerFile>();
            PurchaseOrders = new List<TLCSV_PurchaseOrder>();
            UserAccesses = new List<TLSEC_UserAccess>();
            Months = new List<TLADM_Months>();
            OrdersAllocated = new List<TLCSV_OrderAllocated>();
            PendingPickingSlips = new List<TLCSV_StockOnHand>();
            StockOnHand = new List<TLCSV_StockOnHand>();
            RePackConfigs = new List<TLCSV_RePackConfig>();

            FromWhse = 0;
            ToWhse = 0;
            AllPurchaseOrders = true;
            ClosedOrders = false;
            GradeA = true;
            Discontinued = false;

            FromDate = new DateTime();
            ToDate = new DateTime();

            SummarisedPurchaseOrders = false;
            SummarisedSalesByCustomer = false;
            SummarisedSalesByCompany = false;
            TransactHistory = false;

            RankedByStyleColour = false;
            RankedByStyleColourSize = false;
            RankedByStyleSize = false;

            GroupByWeek = false;
            IncludeProvisional = false;
            Both = false;

        }

       
    }
}

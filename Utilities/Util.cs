using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Data.Entity;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Security.Principal;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;

namespace Utilities
{
    public class DialogCenteringService : IDisposable
    {
        private readonly IWin32Window owner;
        private readonly HookProc hookProc;
        private readonly IntPtr hHook = IntPtr.Zero;

        public DialogCenteringService(IWin32Window owner)
        {
            if (owner == null) throw new ArgumentNullException("owner");

            this.owner = owner;
            hookProc = DialogHookProc;

            hHook = SetWindowsHookEx(WH_CALLWNDPROCRET, hookProc, IntPtr.Zero, GetCurrentThreadId());
        }

        private IntPtr DialogHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }

            CWPRETSTRUCT msg = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
            IntPtr hook = hHook;

            if (msg.message == (int)CbtHookAction.HCBT_ACTIVATE)
            {
                try
                {
                    CenterWindow(msg.hwnd);
                }
                finally
                {
                    UnhookWindowsHookEx(hHook);
                }
            }

            return CallNextHookEx(hook, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(hHook);
        }

        private void CenterWindow(IntPtr hChildWnd)
        {
            Rectangle recChild = new Rectangle(0, 0, 0, 0);
            bool success = GetWindowRect(hChildWnd, ref recChild);

            if (!success)
            {
                return;
            }

            int width = recChild.Width - recChild.X;
            int height = recChild.Height - recChild.Y;

            Rectangle recParent = new Rectangle(0, 0, 0, 0);
            success = GetWindowRect(owner.Handle, ref recParent);

            if (!success)
            {
                return;
            }

            Point ptCenter = new Point(0, 0);
            ptCenter.X = recParent.X + ((recParent.Width - recParent.X) / 2);
            ptCenter.Y = recParent.Y + ((recParent.Height - recParent.Y) / 2);


            Point ptStart = new Point(0, 0);
            ptStart.X = (ptCenter.X - (width / 2));
            ptStart.Y = (ptCenter.Y - (height / 2));

            //MoveWindow(hChildWnd, ptStart.X, ptStart.Y, width, height, false);
            Task.Factory.StartNew(() => SetWindowPos(hChildWnd, (IntPtr)0, ptStart.X, ptStart.Y, width, height, SetWindowPosFlags.SWP_ASYNCWINDOWPOS | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOACTIVATE | SetWindowPosFlags.SWP_NOOWNERZORDER | SetWindowPosFlags.SWP_NOZORDER));
        }

        // some p/invoke

        // ReSharper disable InconsistentNaming
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public delegate void TimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime);

        private const int WH_CALLWNDPROCRET = 12;

        // ReSharper disable EnumUnderlyingTypeIsInt
        private enum CbtHookAction : int
        // ReSharper restore EnumUnderlyingTypeIsInt
        {
            // ReSharper disable UnusedMember.Local
            HCBT_MOVESIZE = 0,
            HCBT_MINMAX = 1,
            HCBT_QS = 2,
            HCBT_CREATEWND = 3,
            HCBT_DESTROYWND = 4,
            HCBT_ACTIVATE = 5,
            HCBT_CLICKSKIPPED = 6,
            HCBT_KEYSKIPPED = 7,
            HCBT_SYSCOMMAND = 8,
            HCBT_SETFOCUS = 9
            // ReSharper restore UnusedMember.Local
        }

        [DllImport("kernel32.dll")]
        static extern int GetCurrentThreadId();

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

        [DllImport("user32.dll")]
        private static extern int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosFlags uFlags);

        [DllImport("User32.dll")]
        public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll")]
        public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

        [DllImport("user32.dll")]
        public static extern int EndDialog(IntPtr hDlg, IntPtr nResult);

        [StructLayout(LayoutKind.Sequential)]
        public struct CWPRETSTRUCT
        {
            public IntPtr lResult;
            public IntPtr lParam;
            public IntPtr wParam;
            public uint message;
            public IntPtr hwnd;
        };
        // ReSharper restore InconsistentNaming
    }

    [Flags]
    public enum SetWindowPosFlags : uint
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
        /// </summary>
        SWP_ASYNCWINDOWPOS = 0x4000,

        /// <summary>
        ///     Prevents generation of the WM_SYNCPAINT message.
        /// </summary>
        SWP_DEFERERASE = 0x2000,

        /// <summary>
        ///     Draws a frame (defined in the window's class description) around the window.
        /// </summary>
        SWP_DRAWFRAME = 0x0020,

        /// <summary>
        ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
        /// </summary>
        SWP_FRAMECHANGED = 0x0020,

        /// <summary>
        ///     Hides the window.
        /// </summary>
        SWP_HIDEWINDOW = 0x0080,

        /// <summary>
        ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOACTIVATE = 0x0010,

        /// <summary>
        ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
        /// </summary>
        SWP_NOCOPYBITS = 0x0100,

        /// <summary>
        ///     Retains the current position (ignores X and Y parameters).
        /// </summary>
        SWP_NOMOVE = 0x0002,

        /// <summary>
        ///     Does not change the owner window's position in the Z order.
        /// </summary>
        SWP_NOOWNERZORDER = 0x0200,

        /// <summary>
        ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
        /// </summary>
        SWP_NOREDRAW = 0x0008,

        /// <summary>
        ///     Same as the SWP_NOOWNERZORDER flag.
        /// </summary>
        SWP_NOREPOSITION = 0x0200,

        /// <summary>
        ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        /// </summary>
        SWP_NOSENDCHANGING = 0x0400,

        /// <summary>
        ///     Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        SWP_NOSIZE = 0x0001,

        /// <summary>
        ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
        /// </summary>
        SWP_NOZORDER = 0x0004,

        /// <summary>
        ///     Displays the window.
        /// </summary>
        SWP_SHOWWINDOW = 0x0040,

        // ReSharper restore InconsistentNaming
    }

    public class ReportSecOptions
    {
        public ReportSecOptions()
        {
        }

        public bool AllRecords { get; set; }
        public bool ActiveRecords { get; set; }
        public bool DiscontinuedRecords { get; set; }

    }

    public class ReportOptions
    {
        public ReportOptions()
        {
        }

        public Int16 SelectionOption { get; set; }
        public Nullable<Int32> Supplier_FK { get; set; }
        public Nullable<DateTime> DateSelected { get; set; }
        public string SupplierDetail { get; set; }
        public int Cotton_LotNo { get; set; } 

    }

    public class UserDetails
    {
        public UserDetails()
        {
        }

        public bool _IsAuthorised { get; set; }
        public bool _SuperUser { get; set; }
        public string _UserName { get; set; }
        public string _NotAuthorisedMessage { get; set; }
        public int _UserPk { get; set; }
        public bool _External { get; set; }
        public bool _QAFunction { get; set; } 
        public bool _DownSizeAuthority { get; set; }
    }


    public class ProdPlanning
    {
        public ProdPlanning()
        {
        }
    }


    public class CSVServices
    {
        public CSVServices()
        {
        }

        public bool SOHClassification { get; set; } // True = Stock / false = Stock Available 
        public bool SOHBoxReturned { get; set; }  // True = Box Returned ; False = Still out there
        public DateTime DateIntoStock { get; set; } 

        //form DetailCustomerOrders
        //------------------------------------
        public int sortOrder { get; set; } 

        //form CustDeliveries 
        //----------------------------------------------
        public int TransNumber { get; set; }
        public int PickListNo { get; set; }
        public bool DeliveryNote { get; set; } 

        //form frmCustomerReturns 
        //--------------------------------------------------
        public DateTime ReturnDate { get; set; }
        public int Customer_Fk { get; set; }
        public string CustomerRef { get; set; } 
        public string ApprovedBy { get; set; }
        public string Reasons { get; set; }
        public int WareHouse { get; set; } 

        // frmCustomerSales 
        //-------------------------------------------------
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public bool Sales { get; set; }    // True = sales / false = returns
        public string Title { get; set; } 

        // frm DetailCustomerOrders 
        //----------------------------------------------
        public bool AllOrders { get; set; }
        public bool Closed { get; set; }
        public bool Open { get; set; }
        public bool IncludeProvisonal { get; set; }  

        //frm Picking Lists
        //-------------------------------------------------------
        public bool PLReprint { get; set; }
        public bool DNReprint { get; set; }
        public int POCustomer_PK { get; set; }
        public bool PLSummarised { get; set; }
        public bool PLStockOrder { get; set; } 
        //frm StockOnHand 
        //----------------------------------------------------------
        public bool AGrade { get; set; }
        public bool SplitBoxOnly { get; set; }
        public bool Discontinued { get; set; } 


    }

    public class CMTReportOptions
    {
        public CMTReportOptions()
        {
        }
        //---------------------------------------------------------------
        // Panel Stock at WareHouse 
        //--------------------------------------------------------------------
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        public int WhseNoSelected { get; set;}
        public int GreigeNoSelected { get; set; }
        public int ColourSelected { get; set; } 

        public int SortSequence { get; set; } 

        public string ReportTitle { get; set;} 
        //--------------------------------------------------------------------------
        // frm StockCage Report
        //-------------------------------------------------------------------------
        public int CMT { get; set; } 

        //----------------------------------------------------------------------------
        // frmProdbyPeriodSel;
        //------------------------------------------------------------------------------
        public int LineNoSelected { get; set; } 

        //--------------------------------------------------------------------------------
        public int MeasurePoint { get; set; }
        //----------------------------------------------------------------------------------

        //---------------------------------------------------------------
        public int WorkAnalysis { get; set; } 
        //-------------------------------------------------------------------

        //-----------------------------------------------------------------------------------
        // frmSelFeederFChk 
        //--------------------------------------------------------------

        public int SLFCmt { get; set; }
        public int SLFCmtLine { get; set; }
        public int SLFCmtSupervisor { get; set; }
        public int SLFCmtCutSheet { get; set; }
        public int SLFCmtStyle { get; set; }
        public int SLFReportOption { get; set; }
        public int SLFCutSheetFK { get; set; }


        public int PanelStockSortOrder { get; set; }
        //--------------------------------------------------------
        // frmProdByPeriodSel 
        //------------------------------------------------------------
        public bool NoOfGarments { get; set; }
        public bool NoOfBoxes { get; set; }

        //--------------------------------------------------------
        // frmCMTFinishedWAnalysis 
        //------------------------------------------------------------
        public bool QAReport { get; set; } 
        
        //-----------------------------------------------
        // frmSelectProduction 
        //----------------------------------------------------
        public bool GradeAOnly { get; set; }
        public bool GradeBOnly { get; set; }
        public bool GradeBoth { get; set; }

        public bool Boxes { get; set; }
        public bool Units { get; set; }

        public bool Exception { get; set; }
        public int Percentage_Exception { get; set; } 
 
    }

    public class KnitReportOptions
    {
        public KnitReportOptions()
        {
        }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public bool ThirdParty { get; set; } 

        public String GradeSelected { get; set; } 

    }

    public class CutReportOptions
    {
        public CutReportOptions()
        {
        }
        //-----------------------------------------------------
        // The following variables are used by the Panel Return Report
        //------------------------------------------------------------------
        public string ApprovedBy { get; set; }
        public DateTime TransDate { get; set; }
        public string remarks { get; set; }
        public int CutSheetPk { get; set; }
        public string ReturnedTo { get; set; }
        public int Pk { get; set; }

        public bool WhichStore { get; set; }  
        //--------------------------------------------------------------------------
        // The following variables are used by Report C1
        //-----------------------------------------------------------------------------

        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public int C1SortOption { get; set; }
        public int C1CustSheet { get; set; }
        public int C1DyeBatch { get; set; } 


        //---------------------------------------------------------------------------------
        // The following is used in the genetation of the BIF Fabric Delivery Note 
        //-----------------------------------------------------------------------------
        public List<int> BIFTransit = new List<int>();

        //--------------------------------------------------------------------------
        // The following variables are used by Report C2
        //-----------------------------------------------------------------------------
        public int C2SortOption { get; set; }

        public bool QAReport { get; set; } 
        //--------------------------------------------------------------------------
        // The following variables are used by Report C3
        //-----------------------------------------------------------------------------
        public int C3SortOption { get; set; }
        public int RepNo { get; set; } 
        //--------------------------------------------------------------------------
        // The following variables are used by Report C4
        //-----------------------------------------------------------------------------
        public int C4SortOption { get; set; }

        //----------------------------------------------------------------------------------------------
        //// Bundle Store Reporting 
        public int BSRRepSelection { get; set; }  

    }
    public class DyeReportOptions
    {
        public DyeReportOptions()
        {
        }

        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        //Dye Order Reports 
        public bool SelGarments { get; set; }
        public bool SelFabric { get; set; }
        public bool SelBoth { get; set; }

        //Dye Order Reports Order By Selection
        public bool DO_ByOrderNo { get; set; }
        public int DO_OptionSelected { get; set; }

        public bool DO_Open { get; set; }
        public bool DO_Closed { get; set; }
        public bool DO_Both { get; set; }
 
        public bool QASummary { get; set;}
        public StringBuilder MergeDetails { get; set; }  

        //-----------------------------------------
        // Dye Batches 
        //--------------------------------------------

        public bool CustomerSelected { get; set;}
        public bool AllCustomersSelected { get; set; } 
        public int CustomerNumberSelected { get; set; }
        public bool FirstTime { get; set; }

        public bool ExceptionOnly { get; set; }
        public int Percentage_Exception { get; set; }

        public bool DBPending { get; set; }
        public bool DBWIP { get; set; }
        public bool DBReprocess { get; set; }
        public bool DBAll { get; set; } 

        public bool SortByDate { get; set; } 
        public bool SortByBatchNumber { get; set;}
        public bool SortByQuality { get; set; }
        public bool SortByDyeOrderNum { get; set; }
        public bool SortByDayCreated { get; set; }
        public bool SortByColour { get; set; }
        public bool SortByCustomer { get; set; }
        public bool SortByDueDate { get; set; }
        public bool SortByMachine { get; set; }
        public bool SortByCause { get; set; }
        public bool SortByRemedy { get; set; }
        public bool SortByDateDyed { get; set; }
        public bool SortByOperator { get; set; }
        public bool ProductionResults { get; set; }  
        //---------------------------------------------------
        // Dye Batches List of oustanding pending dye batches
        //----------------------------------------------------------
        public int ops3_ComboSelected { get; set; }
        public int ops3_ComboSelectedValue { get; set; } 

        //----------------------------------------------------
        // Fabric Stock on Hand 
        //---------------------------------------------------------

        public bool OwnFabric { get; set; }
        public bool CommissionFabric { get; set; }
        public bool BothTypes { get; set; }

        public bool FabricStore { get; set; }

        public int FabricStoreSelected { get; set; }
        public bool FabricQSStore { get; set; }
        public bool FabricRejectStore { get; set; }
        public bool AllFabricStores { get; set; }
        public bool ShowIndvidualNo { get; set; }

        //True  = Standard ie Non Bought in Fabric 
        //False = Bought in Fabric Only.
        //--------------------------------------------------------
        public bool FabricType { get; set; }

        // Fabric that has completed Stage 1 only
        //-------------------------------------------------------
        public bool FabricNotFinished { get; set; }
        
        // Fabric that has completed the whole process and is waiting for QA Approval
        //-----------------------------------------------------------------------------
        public bool FabricToQ { get; set; }
        
        // Fabric that has completed the whole process and has been QA'ed and moveed to the fabric store
        //-----------------------------------------------------------------------------------
        public bool FabricToStore { get; set; } 
        //------------------------------
        // Dye and Chemical Consumption
        //-----------------------------------------------------

        //---------------------------------------------------------
        // Dye Process Loss 
        //--------------------------------------------------------------------
        public bool DPLBatchSel { get; set; }
        public bool DPLShadeSel { get; set; }
        public bool DPLColourSel { get; set; }
        public bool DPLMachineSel { get; set; }

        public int DPLBatchIndex { get; set; }
        public int DPLShadeIndex { get; set; }
        public int DPLColourIndex { get; set; }
        public int DPLMachineIndex { get; set; }

        public bool DPLSByBatch { get; set; }
        public bool DPLSByQuality { get; set; }
        public bool DPLSByColour { get; set; }
        public bool DPLSByShade { get; set; }
        public bool DPLSByMachine { get; set; }
        public bool DPLSByLoss { get; set; }
        
        //-------------------------------------------------------------------------------
        // Stock Take Sheet
        //-------------------------------------------------------------------------------------
        public bool stkWhseSelected { get; set;}
        public bool stkTypeSelected { get; set;}
        public bool stkTakeCategorySelected { get; set; }

        public int stkWhseIndex { get; set; }
        public int stkStockTypeIndex { get; set; }
        public int stkTakeCategory { get; set; } 
        
        //-----------------------------------------------------------------------------------------------
        // colour matching check after Dyeing 
        //--------------------------------------------------------

        public int fromStore { get; set; }
        public int toStore { get; set; }
        public int LNU { get; set; }

        public bool RejectedBatches { get; set; }
       
        //-----------------------------------------------------------------------------------------------
        // Dye Production Reports 
        //--------------------------------------------------------

        public int DYEPSort { get; set; }

        public bool DYEPFirstT { get; set; }
        public bool DYEPReprocessed { get; set; }
        public bool DYEPAll { get; set; }

        public int DYEPSortOption { get; set; } 

        public int DYEPCustNo { get; set; }
        public bool DYEPCustNoSelected { get; set; } 

        //------------------------------------------------------------------------
        // Dye Machine Performance 
        //------------------------------------------------------------------------------

        public int DYEMPMachineSelected { get; set; }
        // 1 -- Own first Time  2 -- Own Reprocessing  3 -- Commission Dyeing
        public int DYEMPProdType { get; set; }
        // 1 -- Summary   2 -- Detailed
        public int DYEMPReportType { get; set; }

        // ("0", "Machine"));
        // ("1", "Batch Number"));
        // ("2", "Quality"));
        // ("3", "Colour"));
        // ("4", "Result"));
        public int DYEMPSortOptions { get; set; }

        //------------------------------------------------------------------------
        // Dye Machine Resource efficiency 
        //------------------------------------------------------------------------------

        public int ResEficMachine { get; set; }
        // True first time processing      False -- Reprocessing 
        //-------------------------------------------------------------------------
        public bool ResEfficTrans { get; set; }

        //Fabric Type 
        //--------------------------------------------------------
        public int ResFabType { get; set; } 

    }
    public class YarnReportOptions
    {
        public YarnReportOptions()
        {

        }

        public int reportChoice { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public bool QASummary { get; set; } 

        ///--------------------------------------------------
        ///
        public bool YarnAuditTrail { get; set; } 
        public bool QualityAuditTrail { get; set;} 

        //------------------------------------------------
        // Report Process loss
        //--------------------------------------------------
        public bool KnitOrder { get; set; }
        public bool KnitMachines { get; set; }
        public bool GreigeProduct { get; set; }
        public bool ProcessLoss { get; set; }
        public bool YarnType { get; set; }
        public bool YarnTex { get; set; }


        public bool PrintWithCurrent { get; set; }
        public int ReportKey { get; set; }

        //------------------------------------------------
        // K7 QA Results for Greige producted for Tradelink 
        //-----------------------------------------------------------
        public int K7KnittingCustomer_FK { get; set;}
        public int K7GreigeProduct_FK { get; set;} 
        public String K7Grade { get; set;}
        public int K7Machine_FK { get; set; }
        public int K7Operator_FK { get; set; }
        public bool ExcludeCommission { get; set; } 

        public bool K7rbQA1 { get; set; }
        public bool K7rbQA2 { get; set; }
        public bool K7rbQA3 { get; set; }

        public bool K7CustSel { get; set; }
        public bool K7ProdSel { get; set; }
        public bool K7MachineSel { get; set; }
        public bool K7GradeSel { get; set; }
        public bool K7OperatorSel { get; set; }
        //------------------------------------------------
        // K8 QA Results for Greige Knitted 
        //-----------------------------------------------------------
        public int K8KnittingCustomer_FK { get; set;}
        public int K8GreigeProduct_FK { get; set; }
        public int K8KnittingMachine_FK { get; set; }
        public int K8Operator_Fk { get; set; }

        public bool K8CustSel { get; set; }
        public bool K8QualSel { get; set; }
        public bool K8MachineSel { get; set; }
        public bool K8OperatorSel { get; set; } 

        public bool K8rbQA1 { get; set; }
        public bool K8rbQA2 { get; set; }
        public bool K8rbQA3 { get; set; }
        public bool K8rbQA4 { get; set; }
        public bool K8rbQA5 { get; set; }
        public bool K8rbQA6 { get; set; }
        public bool K8rbQA7 { get; set; }
        public bool K8rbQA8 { get; set; }
        //-------------------------------------------------------------------
        // K9 Greige Stock on hand report
        //-------------------------------------------------------------------
        public bool SplitByShift { get; set; }
 

        //-------------------------------------------------------------------
        // K10 Greige Stock on hand report
        //-------------------------------------------------------------------

        public int K10StoreFK { get; set; }
        public string K10GradeFK { get; set;} 
        public int K10GreigeProductFK { get; set; }
        public int K10GreigeProductQFK { get; set; }
        public int K10StockTakeFreqFK { get; set; }



        public bool K10Store { get; set; }
        public bool K10Grade { get; set; }
        public bool K10Product { get; set; }
        public bool K10ProductQ { get; set; }
        public bool K10STF { get; set; }
        public bool BIFSummarised { get; set; } 
        //
        //-------------------------------------------------------------
        // C Grade Report designated K12 
        //-----------------------------------------------------------
        public bool K12KnitMachine { get; set; } 
        public bool K12YarnOrder { get; set; }
        public bool K12Operator { get; set; }
        public bool K12Spinning { get; set; }
        public bool K12PieceNo { get; set; } 

        //-----------------------------------------------------------------------
        // Greige Key Measurement Grouping options 
        //==========================================================================

        public bool GKM_Quality  { get; set;}
        public bool GKM_Machines  { get; set;}
        public bool GKM_Operators { get; set; }

    }

    public class Util
    {

        bool nonNumeric;
        
        public int CalculateSelection(bool[] SelectionMade)
        {
            int Res = 0;
            int Seed = 0;
            foreach (var item in SelectionMade)
            {
                if(item.Equals(true))
                {
                    Res += (int)Math.Pow(2.000, (double)Seed);
                }
                Seed += 1;
            }
            return Res;
        }
        public List<TLKNI_GreigeProduction> CalculateAvailableToBatch(int GradeSelection, int Greige, bool IncludeWarn)
        {
            using (var context = new TTI2Entities())
            {
                var GreigeP = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Greige_Fk == Greige && !x.GreigeP_Dye).ToList();

                if (GradeSelection == 1)
                {
                    GreigeP = GreigeP.Where(x => x.GreigeP_Grade != null && x.GreigeP_Grade.Trim() == "A").ToList();
                }
                else if (GradeSelection == 2)
                {
                    GreigeP = GreigeP.Where(x => x.GreigeP_Grade != null && x.GreigeP_Grade.Trim() == "B").ToList();
                }
                else if (GradeSelection == 3)
                {
                    GreigeP = GreigeP.Where(x => x.GreigeP_Grade != null && (x.GreigeP_Grade.Trim() == "A"
                                     || x.GreigeP_Grade.Trim() == "B")).ToList();
                }
                else if (GradeSelection == 4)
                {
                    GreigeP = GreigeP.Where(x => x.GreigeP_Grade != null && x.GreigeP_Grade.Trim() == "C").ToList();
                }
                else if (GradeSelection == 5)
                {
                    GreigeP = GreigeP.Where(x => x.GreigeP_Grade != null && (x.GreigeP_Grade.Trim() == "A" ||
                             x.GreigeP_Grade.Trim() == "C")).ToList();
                }
                else if (GradeSelection == 6)
                {
                    GreigeP = GreigeP.Where(x => x.GreigeP_Grade != null && (x.GreigeP_Grade.Trim() == "B" || x.GreigeP_Grade.Trim() == "C")).ToList();
                }
                

                if (GradeSelection == 1 || GradeSelection == 3 ||
                    GradeSelection == 5 || GradeSelection == 7)
                {
                    if (!IncludeWarn)
                        GreigeP = GreigeP.Where(x => !x.GreigeP_WarningMessage).ToList();
                }

                return GreigeP;

            }
        }
        public decimal DyeOrdersLT8Weeks(int Pk)
        {
            var Result = 0.00M;
            using(var context = new TTI2Entities())
            {
                int ThisWeek = GetIso8601WeekOfYear(DateTime.Now);
                ThisWeek += 8;

                int ThisYear = DateTime.Now.Year;
                int PrevYear = -1 + ThisYear;

                var LessThan = from DO in context.TLDYE_DyeOrder
                               join DOD in context.TLDYE_DyeOrderDetails on DO.TLDYO_Pk equals DOD.TLDYOD_DyeOrder_Fk
                               where DOD.TLDYOD_BodyOrTrim && !DO.TLDYO_Closed && 
                               ((DO.TLDYO_OrderDate.Year == ThisYear && DO.TLDYO_DyeReqWeek <= ThisWeek) ||
                               (DO.TLDYO_OrderDate.Year <= PrevYear)) && DO.TLDYO_Greige_FK == Pk
                               select new { DO, DOD };

                if (LessThan.Count() != 0)
                {
                    Result = (decimal)LessThan.Sum(x => (decimal?)x.DOD.TLDYOD_Kgs ?? 0.00M);

                    var AlreadyBatched = from LTH in LessThan
                                         join DB in context.TLDYE_DyeBatch on LTH.DO.TLDYO_Pk equals DB.DYEB_DyeOrder_FK
                                         where DB.DYEB_Allocated
                                         select DB;

                    if (AlreadyBatched.Count() != 0)
                    {
                        Result  -= (decimal)AlreadyBatched.Sum(x => (decimal?)x.DYEB_BatchKG ?? 0.00M);
                    }
                }


                
            }
            return Result;
        }

        public decimal DyeOrdersGT8Weeks(int Pk)
        {
            var Result = 0.00M;
            int ThisWeek = GetIso8601WeekOfYear(DateTime.Now);
            int ThisYear = DateTime.Now.Year; 

            using ( var context = new TTI2Entities())
            {
                var GreaterThan = (from DO in context.TLDYE_DyeOrder
                                   join DOD in context.TLDYE_DyeOrderDetails on DO.TLDYO_Pk equals DOD.TLDYOD_DyeOrder_Fk
                                   where DOD.TLDYOD_BodyOrTrim && !DO.TLDYO_Closed && DO.TLDYO_DyeReqWeek > (ThisWeek + 8) && DO.TLDYO_OrderDate.Year >= ThisYear && DO.TLDYO_Greige_FK == Pk
                                   select new { DO, DOD });

                if (GreaterThan.Count() > 0)
                {
                    Result = (decimal)GreaterThan.Sum(x => (decimal?)x.DOD.TLDYOD_Kgs ?? 0.00M);
                    var AlreadyBatched = from GT in GreaterThan
                                         join DB in context.TLDYE_DyeBatch on GT.DO.TLDYO_Pk equals DB.DYEB_DyeOrder_FK
                                         where DB.DYEB_Allocated
                                         select DB;

                    if (AlreadyBatched.Count() != 0)
                    {
                        Result -= (decimal)AlreadyBatched.Sum(x => (decimal?)x.DYEB_BatchKG ?? 0.00M);
                    }
                }
            }

            return Result;
        }

        public bool CheckActiveRatings(int RatingKey)
        {
            bool ReturnVal = false;
            using (var context = new TTI2Entities())
            {
                var DORecordCount = (from DOrder in context.TLDYE_DyeOrder
                           join DOrderD in context.TLDYE_DyeOrderDetails on DOrder.TLDYO_Pk equals DOrderD.TLDYOD_DyeOrder_Fk
                           where !DOrder.TLDYO_Closed && DOrderD.TLDYOD_MarkerRating_FK == RatingKey
                           select DOrderD).Count();
                if (DORecordCount == 0)
                {
                    var DBRecordCount = (from DBatch in context.TLDYE_DyeBatch
                                         join DBatchDet in context.TLDYE_DyeBatchDetails on DBatch.DYEB_Pk equals DBatchDet.DYEBD_DyeBatch_FK
                                         where !DBatch.DYEB_Closed && DBatchDet.DYEBO_ProductRating_FK == RatingKey
                                         select DBatchDet).Count();
                    if (DBRecordCount == 0)
                    {
                        ReturnVal = true;
                    }
                }
            }
            return ReturnVal;
        }

        public Decimal CalculateDskVariance(decimal BenchMark, decimal Actual)
        {
            var Tmp = -100 + ((Actual / BenchMark) * 100);
            return Tmp;
        }

        public void SendEmailtoContacts(string EMailAddress, DataTable _dt, int MailNo, DateTime Date,  string PO, String TransNo)
        {
            StringBuilder html = new StringBuilder();
            string subjectEmail = String.Empty;
            string bodyEmail = String.Empty;

            if (MailNo == 1)
            {
                subjectEmail = "Confirmation of Purchase Order";
                //Building an HTML string.

                html.Append("<h5>");
                html.Append("Please find set out below the items ordered as per your purchase order number ");
                html.Append(PO);
                html.Append(" dated ");
                html.Append(Date.ToString("dd/MM/yyyy"));
                html.Append("</h5>");
                html.Append("Our reference number is " + TransNo.TrimEnd() + ".");
                html.Append("<br>");
                html.Append("Kindly qoute this number in all correspondence");
                html.Append("</p>");
                //---------------------------------------------------
                //Table start.
                html.Append("<table border = '1'>");
            }
            else
            {
                subjectEmail = "Confirmation of CMT Picking List Order";
                html.Append("<h5>");
                html.Append("Please find set out below the items picked ");
                html.Append(PO);
                html.Append(" dated ");
                html.Append(Date.ToString("dd/MM/yyyy"));
                html.Append("</h5>");
                html.Append("This picklist is destined for warehouse " + PO + " The picklist No is " + TransNo.TrimEnd() + ".");
                html.Append("<br>");
                html.Append("</p>");
                //---------------------------------------------------
                //Table start.
                html.Append("<table border = '1'>");
            }
            //---------------------------------------------
            //Building the Header row.
            //-----------------------------------------
            html.Append("<tr>");
            foreach (DataColumn column in _dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }

            html.Append("</tr>");

            //Building the Data rows.
            //------------------------------------------------------
            foreach (DataRow row in _dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in _dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }

            //Table end.
            //-----------------------------------------
            html.Append("</table>");
            if (MailNo == 1)
            {
                html.Append("<p>");
                html.Append("Please verify the above mentioned details corresponds to your requirements");
                html.Append("</p>");
            }

            bodyEmail = html.ToString();
            CreateEmailItem(subjectEmail, EMailAddress, bodyEmail);
        }

        public void CreateEmailItem(string subjectEmail, string toEmail, string bodyEmail)
        {
            Microsoft.Office.Interop.Outlook.Application app = new Microsoft.Office.Interop.Outlook.Application();
            Microsoft.Office.Interop.Outlook.MailItem mailItem = app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

            mailItem.Subject = subjectEmail;
            mailItem.To = toEmail;
            mailItem.HTMLBody = bodyEmail;
            mailItem.Importance = Microsoft.Office.Interop.Outlook.OlImportance.olImportanceHigh;
            ((Microsoft.Office.Interop.Outlook._MailItem)mailItem).Send();

        }

        public decimal CalculatePalletNett(TLKNI_YarnOrderPallets Pallet)
        {
            Decimal NettValue = ((Pallet.TLKNIOP_NettWeight - Pallet.TLKNIOP_NettWeightReserved ) + Pallet.TLKNIOP_AdditionalYarn) + Pallet.TLKNIOP_NettWeightReturned;
            return  Math.Round(NettValue, 1);
        }


        public string ReturnPalletNo(int GriegePk)
        {
            string ot = string.Empty;
            using (var context = new TTI2Entities())
            {
                var GreigeProd = context.TLKNI_GreigeProduction.Find(GriegePk);
                if (GreigeProd != null)
                {
                    var KOrder = context.TLKNI_Order.Find(GreigeProd.GreigeP_KnitO_Fk);
                    if (KOrder != null)
                    {
                        var Pallet = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == KOrder.KnitO_Pk && x.TLKYT_TranType == 1).FirstOrDefault();
                        if (Pallet != null)
                        {
                            var YOrderPallet = context.TLKNI_YarnOrderPallets.Find(Pallet.TLKYT_YOP_FK);
                            if (YOrderPallet != null)
                            {
                                var YarnOrder = context.TLSPN_YarnOrder.Find(YOrderPallet.TLKNIOP_YarnOrder_FK);
                                if (YarnOrder != null)
                                {
                                    ot = YarnOrder.YarnO_OrderNumber.ToString() + " - " + YOrderPallet.TLKNIOP_PalletNo.ToString();
                                }
                            }
                        }
                    }
                }
            }
            return ot;
        }

        public string ReturnKnitONo(int GriegePk)
        {
            string ot = string.Empty;
            using (var context = new TTI2Entities())
            {
                var GreigeProd = context.TLKNI_GreigeProduction.Find(GriegePk);
                if (GreigeProd != null)
                {
                    var KOrder = context.TLKNI_Order.Find(GreigeProd.GreigeP_KnitO_Fk);
                    if (KOrder != null)
                    {
                        ot = "KO" + KOrder.KnitO_OrderNumber.ToString().PadLeft(6, '0');
                    }
                }
            }
            return ot;
        }
        /*
        public DateTime CenturyDate(Int32 CDN)
        {
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            DateTime now = centuryBegin.AddDays(CDN);
            return now;
        }

        public Int32 CenturyDayNumber(DateTime today)
        {
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            DateTime currentDate = today;

            long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

            return elapsedSpan.Days;

        }
         */
              
        public int GetWorkingDays(DateTime from, DateTime to)
        {
            int ans = 0;
            var dayDifference = (int)to.Subtract(from).TotalDays;
            if (dayDifference < 0)
            {
                dayDifference = (int)from.Subtract(to).TotalDays;

                ans = Enumerable
                      .Range(1, dayDifference)
                      .Select(x => to.AddDays(x))
                      .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday) * -1;
            }
            else
            {
                ans = Enumerable
               .Range(1, dayDifference)
               .Select(x => from.AddDays(x))
               .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday);
            }
            return ans; 
            
        }

        public int GetWeekNumber(DateTime dt)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

        public DataGridViewCell GetNextCell(DataGridView oDgv, DataGridViewCell currentCell)
        {
            int i = 0;
            DataGridViewCell nextCell = currentCell;

            do
            {
                int nextCellIndex = (nextCell.ColumnIndex + 1) % oDgv.ColumnCount;
                int nextRowIndex = nextCellIndex == 0 ? (nextCell.RowIndex + 1) % oDgv.RowCount : nextCell.RowIndex;
                nextCell = oDgv.Rows[nextRowIndex].Cells[nextCellIndex];
                i++;
            } while (i < oDgv.RowCount * oDgv.ColumnCount && nextCell.ReadOnly);

            return nextCell;
        }

        public decimal CalCulateVariance(decimal firstValue, decimal secondValue)
        {
                // the easiest way is :
                return Math.Round(100.0M * (secondValue / firstValue) - 100.0M, 2);
        }

        public bool GetUserAuthorisation(UserDetails ud, string SectionName)
        {
            bool IsAuthorised = false;

            if (!ud._SuperUser)
            {
                using (var context = new TTI2Entities())
                {
                    var Section = context.TLSEC_Sections.Where(x => x.TLSECSect_Name == SectionName).FirstOrDefault();
                    if(Section != null)
                    {
                        var User = context.TLSEC_UserSections.Where(x => x.TLSECDEP_Section_FK == Section.TLSECSect_Pk && x.TLSECDEP_User_FK == ud._UserPk).FirstOrDefault();
                        if (User != null && User.TLSECDEP_AccessGranted)
                            IsAuthorised = true;
                    }
                }
            }
            else
            {
                IsAuthorised = true;
            }
            return IsAuthorised;
        }

        public bool PPSCompareValues(TLPPS_Replenishment rep, DataGridViewRow row)
        {
            var Val = true;
            /* replen.TLREP_Discontinued = (bool)row.Cells[1].Value;
               replen.TLREP_Style_FK = (int)row.Cells[2].Value;
               replen.TLREP_Colour_FK = (int)row.Cells[3].Value;
               replen.TLREP_Size_FK = (int)row.Cells[4].Value;
               replen.TLREP_ExpectedSales = (int)row.Cells[5].Value;
               replen.TLREP_ReOrderLevelWeeks = (int)row.Cells[6].Value;
               replen.TLREP_ReorderQtyWeeks = (int)row.Cells[7].Value;
               replen.TLREP_ReOrderLevel = (int)row.Cells[5].Value * (int)row.Cells[6].Value;
               replen.TLREP_ReOrderQty = (int)row.Cells[5].Value * (int)row.Cells[7].Value; */

            foreach (DataGridViewCell Cell in row.Cells)
            {
                var ColIndex = Cell.ColumnIndex;

                if(ColIndex == 1 || ColIndex > 4)
                {
                    if (ColIndex == 1)
                    {
                        if ((bool)Cell.Value != rep.TLREP_Discontinued)
                        {
                            Val = false;
                            break;
                        }
                    }
                    else if (ColIndex == 5)
                    {
                        if ((int)Cell.Value != rep.TLREP_ExpectedSales)
                        {
                            Val = false;
                            break;
                        }

                    }
                    else if (ColIndex == 6)
                    {
                        if ((int)Cell.Value != rep.TLREP_ReOrderLevelWeeks)
                        {
                            Val = false;
                            break;
                        }
                    }
                    else if (ColIndex == 7)
                    {
                        if ((int)Cell.Value != rep.TLREP_ReorderQtyWeeks)
                        {
                            Val = false;
                            break;
                        }
                    }
                    else if (ColIndex == 8)
                    {
                        if ((int)Cell.Value != rep.TLREP_ReOrderLevel)
                        {
                            Val = false;
                            break;
                        }
                    }
                    else
                    {
                        if ((int)Cell.Value != rep.TLREP_ReOrderQty)
                        {
                            Val = false;
                            break;
                        }
                    }
                }
            }
            return Val;
        }
        public List<TLCUT_CutSheet> GetCSDetails(int Pk)
        {
            List<TLCUT_CutSheet> OutPut = new List<TLCUT_CutSheet>();
            using (var context = new TTI2Entities())
            {
                var pid = context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == Pk && !x.CMTPID_Receipted).ToList();

                foreach (var record in pid)
                {
                    var CSR = context.TLCUT_CutSheetReceipt.Find(record.CMTPID_CutSheet_FK);
                    if (CSR != null)
                    {
                        var CS = context.TLCUT_CutSheet.Find(CSR.TLCUTSHR_CutSheet_FK);
                        if (CS != null)
                        {
                            OutPut.Add(CS);
                        }
                    }
                }
            }

            return OutPut;
        }

        public List<DATA> test()
        {
            List<DATA> RatioData = new List<DATA>();
            return RatioData;
        }

        public string[][] CreateColumnNames()
        {
            string[][] ColumnNames = new string[][]
            {   new string[] {"Text4", string.Empty},        
                        new string[] {"Text5", string.Empty},
                        new string[] {"Text6", string.Empty},
                        new string[] {"Text7", string.Empty},
                        new string[] {"Text8", string.Empty},
                        new string[] {"Text9", string.Empty},
                        new string[] {"Text10", string.Empty},
                        new string[] {"Text11", string.Empty},
                        new string[] {"Text12", string.Empty},
                        new string[] {"Text13", string.Empty},
                        new string[] {"Text14", string.Empty}
                      
            };
            
            using (var context = new TTI2Entities())
            {
                var Sizes = context.TLADM_Sizes.Where(x => (bool)!x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).GroupBy(x => x.SI_ColNumber).ToList();
                foreach (var xSize in Sizes)
                {
                    var OrdxSize = xSize.OrderBy(x => x.SI_DisplayOrder);

                    StringBuilder Sb = new StringBuilder();
                    foreach (var Item in OrdxSize)
                    {
                        Sb.Append(Item.SI_Description + Environment.NewLine);
                    }

                    foreach (var ColumnName in ColumnNames)
                    {
                        if (string.IsNullOrEmpty(ColumnName[1]))
                        {
                            ColumnName[1] = Sb.ToString();
                            break;
                        }
                    }
                }
            }
            return ColumnNames;

        }
        public bool[] RowComplete(DataGridViewRow row, string[][] MandatoryFields)
        {
            bool[] complete = PopulateArray(MandatoryFields.Count(), false);

            foreach (DataGridViewCell cell in row.Cells)
            {
                var result = (from u in MandatoryFields
                              where u[0] == cell.ColumnIndex.ToString()
                              select u).FirstOrDefault();
                
                if (result == null)
                    continue;
               
                if (cell.Value == null && cell.EditedFormattedValue == null)
                    continue;

                if (cell.Value != null)
                {
                    if (string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        if (string.IsNullOrEmpty(cell.EditedFormattedValue.ToString()))
                            continue;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(cell.EditedFormattedValue.ToString()))
                        continue;
                }

                int nbr = Convert.ToInt32(result[2].ToString());
                complete[nbr] = true;
      
            }

            return complete;
        }

        public Int32 CalculateCustomerOrder_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
            IList<TLCSV_PuchaseOrderDetail> PODetail = null;

            var QtyOrdered = 0;
            var AllReadyPicked = 0;

            using (var context = new TTI2Entities())
            {

                PODetail = (from PO in context.TLCSV_PurchaseOrder
                            join POD in context.TLCSV_PuchaseOrderDetail on PO.TLCSVPO_Pk equals POD.TLCUSTO_PurchaseOrder_FK
                            where !PO.TLCSVPO_Closeed && !POD.TLCUSTO_Closed && POD.TLCUSTO_Style_FK == Style && POD.TLCUSTO_Colour_FK == Colour && POD.TLCUSTO_Size_FK == Size
                            select POD).ToList();

                if (PODetail.Count != 0)
                {
                    QtyOrdered = PODetail.Sum(x => (int?)x.TLCUSTO_Qty) ?? 0;
                    AllReadyPicked = PODetail.Sum(x => (int?)x.TLCUSTO_QtyPicked_ToDate) ?? 0;
                }
            }

            ExpectedUnits = QtyOrdered - AllReadyPicked;
            
            return ExpectedUnits;
        }

        public Int32 CalculateSales_Units(int Style, int Colour, int Size , DateTime DateFrom, DateTime DateTo)
        {
            int ExpectedUnits = 0;
            IList<TLCSV_StockOnHand> StockOnHand = null;

            using (var context = new TTI2Entities())
            {
                StockOnHand = (from soh in context.TLCSV_StockOnHand
                               where soh.TLSOH_Sold && !soh.TLSOH_Returned && soh.TLSOH_Style_FK == Style && soh.TLSOH_Colour_FK == Colour && soh.TLSOH_Size_FK == Size && soh.TLSOH_SoldDate >= DateFrom && soh.TLSOH_SoldDate <= DateTo
                               select soh).ToList();

                if (StockOnHand != null)
                {
                    ExpectedUnits = StockOnHand.Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
                }
            }
            return ExpectedUnits;
        }

        public Int32 CalculateSOH_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
            IList<TLCSV_StockOnHand> StockOnHand = null;
 
            using (var context = new TTI2Entities())
            {
                StockOnHand = (from soh in context.TLCSV_StockOnHand 
                               join whse in context.TLADM_WhseStore on soh.TLSOH_WareHouse_FK equals whse.WhStore_Id
                               where soh.TLSOH_Style_FK == Style 
                               && soh.TLSOH_Colour_FK == Colour 
                               && soh.TLSOH_Size_FK == Size
                               && soh.TLSOH_Is_A  
                               && !soh.TLSOH_Picked 
                               && !soh.TLSOH_Sold 
                               && !soh.TLSOH_Write_Off 
                               && !soh.TLSOH_Split 
                               && !soh.TLSOH_Returned
                               && whse.WhStore_GradeA
                               select soh).ToList();
 
               if (StockOnHand != null)
               {
                    ExpectedUnits = StockOnHand.Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;
               }
            }
            
            return ExpectedUnits;
        }

        public Int32 CalculateDO_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
          
            using (var context = new TTI2Entities())
            {
                var DyeOrders = context.TLDYE_DyeOrder.Where(x => x.TLDYO_Style_FK == Style && x.TLDYO_Colour_FK == Colour && !x.TLDYO_Closed).ToList();
                foreach (var DyeOrder in DyeOrders)
                {
                    //------------------------------------------------------
                    // Because of the concept of multi markers we have to calculate a ratio based on the the number of sizes
                    // originally entered 
                    //------------------------------------------------------------------
                    BindingList<KeyValuePair<int, decimal>> Ratios = null;

                    var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DyeOrder.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                    if (DyeOrderDetail != null)
                    {
                        Ratios = ReturnRatios(DyeOrderDetail.TLDYOD_MarkerRating_FK);

                        var Entry = Ratios.FirstOrDefault(x => x.Key == Size);
                        if (Entry.Key == 0)
                        {
                            //this item Size is not relevant to this dye order
                            //====================================================
                            continue;
                        }

                        decimal FabricYield = DyeOrderDetail.TLDYOD_Yield;
                        decimal FabricRating = DyeOrderDetail.TLDYOD_Rating;
                        decimal TotalWeight = (decimal)DyeOrderDetail.TLDYOD_Kgs;
                        var Amt = 0;

                        var DyeBatches = from T1 in context.TLDYE_DyeBatch
                                         join T2 in context.TLDYE_DyeBatchDetails on T1.DYEB_Pk equals T2.DYEBD_DyeBatch_FK
                                         where !T1.DYEB_CommissinCust && T1.DYEB_DyeOrder_FK == DyeOrder.TLDYO_Pk && T2.DYEBD_BodyTrim
                                         select T2;

                        if (DyeBatches.Count() != 0)
                        {
                            try
                            {
                                TotalWeight -= DyeBatches.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                                if (TotalWeight > 0)
                                {
                                    Amt = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                                    var TotalR = Ratios.Sum(x => x.Value);
                                    Amt = Convert.ToInt32((Entry.Value / TotalR) * Amt);
                                }
                                else
                                    Amt = 0;
                            }
                            catch (System.Exception ex)
                            {
                                Amt = 0;
                            }
                        }
                        else
                        {
                            Amt = DyeOrderDetail.TLDYOD_Units;
                            var TotalR = Ratios.Sum(x => x.Value);
                            Amt = Convert.ToInt32((Entry.Value / TotalR) * Amt);
                        }

                        //----------------------------------------------
                        //We have to allow for a loss factor of 5%
                        //---------------------------------------------
                        Amt = Convert.ToInt32(Amt * 0.95);
                        Amt = Convert.ToInt32(Amt * 0.95);

                        ExpectedUnits += Amt;
                    }
                }
            }
            return ExpectedUnits;
        }

      
        public Int32 CalculateDBPrep_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
            var Amt = 0;

            using (var context = new TTI2Entities())
            {
                //----------------------------------------------------------------
                // Because Dye Batches have no specified Style, Colour and size Key 
                // We have to get it from the respective Dye Orders 
                //---------------------------------------------------------------
                var DOrders = from T1 in context.TLDYE_DyeOrder
                              join T2 in context.TLDYE_DyeBatch on T1.TLDYO_Pk equals T2.DYEB_DyeOrder_FK
                              join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                              where !T2.DYEB_CommissinCust && T1.TLDYO_Style_FK == Style && T1.TLDYO_Colour_FK == Colour
                              && !T2.DYEB_Closed && !T2.DYEB_Allocated && T3.DYEBD_BodyTrim
                              select new { T1.TLDYO_Pk, T2.DYEB_Pk, T2.DYEB_Greige_FK, T3.DYEBD_GreigeProduction_Weight };

                var DOrdersx = DOrders.GroupBy(x => x.TLDYO_Pk);

                foreach (var DOOrder in DOrdersx)
                {
                    BindingList<KeyValuePair<int, decimal>> Ratios = null;

                   
                    var Order = DOOrder.FirstOrDefault();
                    
                    var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == Order.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                    if (DyeOrderDetail != null)
                    {
                        Ratios = ReturnRatios(DyeOrderDetail.TLDYOD_MarkerRating_FK);
                        var Entry = Ratios.FirstOrDefault(x => x.Key == Size);
                        if (Entry.Key == 0)
                        {
                            continue;
                        }

                        Amt = 0;
                        var FabricYield = DyeOrderDetail.TLDYOD_Yield;
                        var FabricRating = DyeOrderDetail.TLDYOD_Rating;
                        
                        try
                        {
                            var TotalWeight = DOOrder.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                            Amt = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                        }
                        catch (System.Exception ex)
                        {
                            Amt = 0;
                        }

                        var Total = Ratios.Sum(x => x.Value);
                        ExpectedUnits += Convert.ToInt32((Entry.Value / Total) * Amt);
                        // We dont need to allow for any losses
                        //------------------------------------------------------------
                        
                    }
                }
            }
            return ExpectedUnits;
        }

        public Int32 CalculateDBWIP_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
            using (var context = new TTI2Entities())
            {
                // 5th Task  -- DyeBatching (WIP) 
                //----------------------------------------------------------------
                // Because Dye Batches have no specified Style, Colour and size Key 
                // We have to get it from the respective Dye Orders 
                //---------------------------------------------------------------
                var DOrders = from T1 in context.TLDYE_DyeOrder
                          join T2 in context.TLDYE_DyeBatch on T1.TLDYO_Pk equals T2.DYEB_DyeOrder_FK
                          join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                          where !T2.DYEB_CommissinCust && T1.TLDYO_Style_FK == Style && T1.TLDYO_Colour_FK == Colour
                          && !T2.DYEB_Closed && T2.DYEB_Allocated && !T2.DYEB_OutProcess && T3.DYEBD_BodyTrim
                          select new { T1.TLDYO_Pk, T2.DYEB_Pk, T2.DYEB_Greige_FK, T3.DYEBD_GreigeProduction_Weight };

                var DOrdersx = DOrders.GroupBy(x => x.TLDYO_Pk);

                foreach (var DOOrder in DOrdersx)
                {
                    BindingList<KeyValuePair<int, decimal>> Ratios = null;

                    var Order = DOOrder.FirstOrDefault();

                    var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == Order.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                    if (DyeOrderDetail != null)
                    {
                        Ratios = ReturnRatios(DyeOrderDetail.TLDYOD_MarkerRating_FK);
                        var Entry = Ratios.FirstOrDefault(x => x.Key == Size);
                        if (Entry.Key == 0)
                        {
                            continue;
                        }

                        var FabricYield = DyeOrderDetail.TLDYOD_Yield;
                        var FabricRating = DyeOrderDetail.TLDYOD_Rating;
                        var ExUnits = 0;
                        try
                        {
                            var TotalWeight = DOOrder.Sum(x => (decimal?)x.DYEBD_GreigeProduction_Weight) ?? 0.00M;
                            ExUnits = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                        }
                        catch (System.Exception ex)
                        {
                            ExUnits = 0;
                        }

                        var Total = Ratios.Sum(x => x.Value);
                        var Answer = Convert.ToInt32((Entry.Value / Total) * ExUnits);

                        //----------------------------------------------
                        //We have to allow for a loss factor of 5%
                        //---------------------------------------------
                        Answer = Convert.ToInt32(Answer * 0.95);
                        Answer = Convert.ToInt32(Answer * 0.95);

                        ExpectedUnits += Answer;
                        
                    }
                }
            }
            return ExpectedUnits;
        }

        public Int32 CalculateDBFabric_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;

            using (var context = new TTI2Entities())
            {
                //-----------------------------------------------------------------
                // 6th Task  -- DyeBatching (FABRIC Store) 
                // Notes :- Request from Heath Wolmaraans to include The Quarantine store in the run 15/12/2017 
                //--------------------------------------------------------------
                // Because Dye Batches have no specified Style, Colour and size Key 
                // We have to get it from the respective Dye Orders 
                //---------------------------------------------------------------
                var DBOrders = from T1 in context.TLDYE_DyeOrder
                               join T2 in context.TLDYE_DyeBatch on T1.TLDYO_Pk equals T2.DYEB_DyeOrder_FK
                               join T3 in context.TLDYE_DyeBatchDetails on T2.DYEB_Pk equals T3.DYEBD_DyeBatch_FK
                               where !T2.DYEB_CommissinCust && T2.DYEB_OutProcess && !T3.DYEBO_Sold && T3.DYEBD_BodyTrim && !T3.DYEBO_Rejected && !T3.DYEBO_WriteOff && !T3.DYEBO_CutSheet && T1.TLDYO_Style_FK == Style && T1.TLDYO_Colour_FK == Colour
                               select new { T1.TLDYO_Pk, T2.DYEB_Pk, T3.DYEBO_Nett };

                var DBOrdersx = DBOrders.GroupBy(x => x.TLDYO_Pk);
                foreach (var Order in DBOrdersx)
                {
                    BindingList<KeyValuePair<int, decimal>> Ratios = null;
                    var FirstOrder = Order.FirstOrDefault();

                    var DyeOrderDetail = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == FirstOrder.TLDYO_Pk && x.TLDYOD_BodyOrTrim).FirstOrDefault();
                    if (DyeOrderDetail != null)
                    {
                        Ratios = ReturnRatios(DyeOrderDetail.TLDYOD_MarkerRating_FK);
                        var Entry = Ratios.FirstOrDefault(x => x.Key == Size);
                        if (Entry.Key != 0)
                        {
                            var EUnits = 0;
                            var FabricYield = DyeOrderDetail.TLDYOD_Yield;
                            var FabricRating = DyeOrderDetail.TLDYOD_Rating;
                            var TotalWeight = 0.00M;

                            try
                            {
                                TotalWeight = Order.Sum(x => (decimal?)x.DYEBO_Nett) ?? 0.00M;
                                EUnits = Convert.ToInt32(FabricYield / FabricRating * TotalWeight);
                            }
                            catch (System.Exception ex)
                            {
                                EUnits = 0;
                            }

                            var Total = Ratios.Sum(x => x.Value);
                            var Answer = Convert.ToInt32((Entry.Value / Total) * EUnits);
                            Answer = Convert.ToInt32(Answer * 0.95);
                            ExpectedUnits += Answer;
                        }
                    }
                }

            }
            return ExpectedUnits;
        }

        public Int32 CalculateCuttingWIP_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
            using ( var context = new TTI2Entities())
            {
                var CutSheets = context.TLCUT_CutSheet.Where(x => !x.TLCutSH_WIPComplete && x.TLCutSH_Accepted && x.TLCutSH_Styles_FK == Style && x.TLCutSH_Colour_FK == Colour && !x.TLCutSH_Closed).ToList();
                foreach (var CutSheet in CutSheets)
                {
                    var CutSheetDetails = context.TLCUT_CutSheetDetail.Where(x => x.TLCutSHD_CutSheet_FK == CutSheet.TLCutSH_Pk).ToList();
                    if (CutSheetDetails.Count() != 0)
                    {
                       var EUnits = context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CutSheet.TLCutSH_Pk && x.TLCUTE_Size_FK == Size).ToList();
                        if (EUnits.Count != 0)
                        {
                            int BoxedUnits = EUnits.Sum(x => (int?)x.TLCUTE_NoofGarments) ?? 0;

                            ExpectedUnits += BoxedUnits;
                        }
                    }
                }
                
            }
            return ExpectedUnits;
        }
        public Int32 CalculateCuttingPanelStore_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
            using (var context = new TTI2Entities())
            {
                //-------------------------------------------------------------------------------------------------------------
                // 8th Task Expected Units in Panel 
                //----------------------------------------------------------------------------------------------------
                var CutSheetR = from T1 in context.TLCUT_CutSheetReceipt
                                join T2 in context.TLCUT_CutSheetReceiptDetail on T1.TLCUTSHR_Pk equals T2.TLCUTSHRD_CutSheet_FK
                                where (!T1.TLCUTSHR_Issued || T1.TLCUTSHR_Issued && !T1.TLCUTSHR_InReceiptCage) && !T1.TLCUTSHR_InBundleStore && T1.TLCUTSHR_InPanelStore
                                && T1.TLCUTSHR_Style_FK == Style && T1.TLCUTSHR_Colour_FK == Colour && T2.TLCUTSHRD_Size_FK == Size
                                select new { T1.TLCUTSHR_Style_FK, T1.TLCUTSHR_Colour_FK, T2.TLCUTSHRD_Size_FK, T2.TLCUTSHRD_BoxUnits };

                if (CutSheetR.Count() != 0)
                {
                    ExpectedUnits += CutSheetR.Sum(x => (int?)x.TLCUTSHRD_BoxUnits) ?? 0; ;
                }
               
            }
            return ExpectedUnits;

        }
        public Int32 CalculateCMTReceiptCage_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
            using (var context = new TTI2Entities())
            {
                var CMTPanelStore = (from LI in context.TLCMT_LineIssue
                                    join CR in context.TLCUT_CutSheetReceipt on LI.TLCMTLI_CutSheet_FK equals CR.TLCUTSHR_CutSheet_FK
                                    join CRD in context.TLCUT_CutSheetReceiptDetail on CR.TLCUTSHR_Pk equals CRD.TLCUTSHRD_CutSheet_FK
                                    where LI.TLCMTLI_IssuedToLine == false && LI.TLCMTLI_WorkCompleted == false && CR.TLCUTSHR_Style_FK == Style && CR.TLCUTSHR_Colour_FK == Colour && CRD.TLCUTSHRD_Size_FK == Size
                                    select new { CR.TLCUTSHR_Style_FK, CR.TLCUTSHR_Colour_FK, CRD.TLCUTSHRD_Size_FK, CRD.TLCUTSHRD_BundleQty, CRD.TLCUTSHRD_RejectQty }).ToList();

                if (CMTPanelStore.Count != 0)
                {
                    var Answer = CMTPanelStore.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                    Answer = Convert.ToInt32(Answer * 0.95);
                    ExpectedUnits += Answer;
                }
            }
            return ExpectedUnits;
        }
        public Int32 CalculateCMTWIP_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
            using (var context = new TTI2Entities())
            {
                var CMTWIP = (from LI in context.TLCMT_LineIssue
                             join CR in context.TLCUT_CutSheetReceipt on LI.TLCMTLI_CutSheet_FK equals CR.TLCUTSHR_CutSheet_FK
                             join CRD in context.TLCUT_CutSheetReceiptDetail on CR.TLCUTSHR_Pk equals CRD.TLCUTSHRD_CutSheet_FK
                             where LI.TLCMTLI_IssuedToLine == true && LI.TLCMTLI_WorkCompleted == false && CR.TLCUTSHR_Style_FK == Style && CR.TLCUTSHR_Colour_FK == Colour && CRD.TLCUTSHRD_Size_FK == Size
                             select new { CR.TLCUTSHR_Style_FK, CR.TLCUTSHR_Colour_FK, CRD.TLCUTSHRD_Size_FK, CRD.TLCUTSHRD_BundleQty, CRD.TLCUTSHRD_RejectQty }).ToList();

                if (CMTWIP.Count != 0)
                {
                    var Answer = CMTWIP.Sum(x => (int?)x.TLCUTSHRD_BundleQty - x.TLCUTSHRD_RejectQty) ?? 0;
                    ExpectedUnits += Answer;
                }

            }
            return ExpectedUnits;
        }

        public Int32 CalculateCMTDespatchCage_Units(int Style, int Colour, int Size)
        {
            int ExpectedUnits = 0;
            using (var context = new TTI2Entities())
            {
                var QueryC = from T1 in context.TLCMT_CompletedWork
                             where (!T1.TLCMTWC_Despatched || T1.TLCMTWC_Despatched && !T1.TLCMTWC_BoxReceiptedWhse) && T1.TLCMTWC_Style_FK == Style && T1.TLCMTWC_Colour_FK == Colour && T1.TLCMTWC_Size_FK == Size
                             select new { T1.TLCMTWC_Style_FK, T1.TLCMTWC_Colour_FK, T1.TLCMTWC_Size_FK, T1.TLCMTWC_Qty };
                if (QueryC.Count() != 0)
                {
                    ExpectedUnits += QueryC.Sum(x => (int?)x.TLCMTWC_Qty) ?? 0;
                }

            }
            return ExpectedUnits;
        }

        public List<DATA> RatioRecalc(DataGridView oDgv, int NoOf)
        {
            List<DATA> RatioData = new List<DATA>();
            List<DATA> OutData = new List<DATA>();
            Decimal Total;
            foreach (DataGridViewRow rw in oDgv.Rows)
            {
                if (rw.Cells[0].Value != null)
                {
                    int Pk         = (int)rw.Cells[0].Value;
                    string size    = rw.Cells[1].Value.ToString();
                    decimal ratio  = 0.00M;
                    int Garments   = 0;
                    decimal Trims  = 0.00M;
                    decimal Binding    = 0.00M;
                    
                    if (!String.IsNullOrEmpty(rw.Cells[2].Value.ToString()))
                    {
                        ratio = Convert.ToDecimal(rw.Cells[2].Value.ToString());

                        if (rw.Cells[3].Value != null)
                            Garments = int.Parse(rw.Cells[3].Value.ToString());
                        if (rw.Cells[4].Value != null)
                            Binding = Decimal.Parse(rw.Cells[4].Value.ToString());
                        if (rw.Cells[5].Value != null)
                            Trims = Decimal.Parse(rw.Cells[5].Value.ToString());
                    }

                    RatioData.Add(new DATA(Pk, size, ratio, Garments, Binding, Trims));
                }
            }

            Total = RatioData.Sum(x => x.ratio);
            foreach (var Record in RatioData)
            {
                var Existing = OutData.Find(x => x.Pk == Record.Pk);
                if (Existing.Pk == 0)
                {
                    int Pk = Record.Pk;
                    string size = Record.size;
                    decimal ratio = Record.ratio;
                    if (Total > 0)
                    {
                        int Garments = (int)(Record.Garments / Total * NoOf);
                        int Trims = (int)(Record.Trims / Total * NoOf);
                        int Binding = (int)(Record.Binding / Total * NoOf);

                        OutData.Add(new DATA(Pk, size, ratio, Garments, Trims, Binding));
                    }
                }

            }
            return OutData;
        }
       
        public DataTable Cal(DataGridView oDgv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index", typeof(int));           // 0
            dt.Columns.Add("Size", typeof(string));         // 1
            dt.Columns.Add("Ratio", typeof(decimal));       // 2
            dt.Columns.Add("Garments", typeof(int));        // 3
            dt.Columns.Add("Binding", typeof(decimal));     // 4
            dt.Columns.Add("Trims", typeof(decimal));       // 5
            dt.Columns.Add("Power", typeof(int));           // 6

            decimal Total = 0.00M;
            int GTotal = 0;
            decimal GBinding = 0;
            decimal GTrims = 0;
           
            foreach (DataGridViewRow rw in oDgv.Rows)
            {
                if (rw.Cells[0].Value != null)
                {
                    DataRow drow = dt.NewRow();
                    drow[0] = (int)rw.Cells[0].Value;
                    drow[1] = rw.Cells[1].Value.ToString();
                    if (!String.IsNullOrEmpty(rw.Cells[2].Value.ToString()))
                    {
                        drow[2] = Convert.ToDecimal(rw.Cells[2].Value.ToString());

                        if (rw.Cells[3].Value != null)
                        {
                            int Value = 0;
                            int.TryParse(rw.Cells[3].Value.ToString(), out Value);
                            drow[3] = Value;
                        }
                        else
                            drow[3] = 0;

                        if (rw.Cells[4].Value != null)
                        {
                            decimal Value = 0.00M;
                            decimal.TryParse(rw.Cells[4].Value.ToString(), out Value);
                            drow[4] = Value;
                        }
                        else
                        {
                            drow[4] = 0.00;
                        }

                        if (rw.Cells[5].Value != null)
                        {
                            decimal Value = 0.00M;
                            decimal.TryParse(rw.Cells[5].Value.ToString(), out Value);
                            drow[5] = Value;
                        }
                        else
                        {
                            drow[5] = 0.00M;
                        }

                        if (rw.Cells[6].Value != null)
                        {
                            int Value = 0;
                            int.TryParse(rw.Cells[6].Value.ToString(), out Value);
                            drow[6] = Value;
                        }
                        else
                            drow[6] = 0;
                                             
                        Total += Convert.ToDecimal(rw.Cells[2].Value.ToString());
                        if(rw.Cells[3].Value != null)
                            GTotal += Convert.ToInt32(rw.Cells[3].Value.ToString());
                        if(rw.Cells[4].Value != null)
                            GBinding += Convert.ToDecimal(rw.Cells[4].Value.ToString());
                        if(rw.Cells[5].Value != null)
                            GTrims += Convert.ToInt32(rw.Cells[5].Value.ToString());

                    }
                    dt.Rows.Add(drow);
                }
            }

            if (Total != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr.ItemArray[2].ToString()))
                    {
                        dr[3] = Convert.ToDecimal(dr.ItemArray[2].ToString()) / Total * GTotal;

                        if (!string.IsNullOrEmpty(dr.ItemArray[4].ToString()))
                        {
                            dr[4] = Convert.ToDecimal(dr.ItemArray[2].ToString()) / Total * GTrims;
                        }

                        if (!string.IsNullOrEmpty(dr.ItemArray[5].ToString()))
                        {
                            dr[5] = Convert.ToDecimal(dr.ItemArray[2].ToString()) / Total * GBinding;
                        }
                    }
                }
             
            }
            return dt;
        }

        public BindingList<KeyValuePair<int, decimal>> ReturnRatios(int Pk)
        {
            var ratioOptions = new BindingList<KeyValuePair<int, decimal>>();

            using (var context = new TTI2Entities())
            {
                var Rating = context.TLADM_ProductRating.Find(Pk);
                if (Rating != null)
                {
                    if (Rating.Pr_Ratio > 1)
                    {
                        var Ratios = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == Rating.Pr_Id).ToList();
                        foreach (var Ratio in Ratios)
                        {
                           if (Ratio.Prd_MarkerRatio == 0)
                               continue;

                           ratioOptions.Add(new KeyValuePair<int, decimal>(Ratio.Prd_SizePN, Ratio.Prd_MarkerRatio));
                        }
                    }
                    else
                    {
                        ratioOptions.Add(new KeyValuePair<int, decimal>(Rating.Pr_Size_FK, 1));
                    }
               
                }
            }
            return ratioOptions;
        }

        public struct DATA
        {
            public int Pk;
            public string size;
            public decimal ratio;
            public int Garments;
            public decimal Trims;
            public decimal Binding;

            public DATA(int _Pk, string _size, decimal _ratio, int _garments, decimal _binding, decimal _trims)
            {
                this.Pk = _Pk;
                this.size = _size;
                this.ratio = _ratio;
                this.Garments = _garments;
                this.Binding = _binding;
                this.Trims = _trims;
            }
        }

        public bool IsAuthorised(string Sender)
        {
            bool IsAuth = false;
            
            /*
            WindowsIdentity currentIdentity;
            WindowsPrincipal currentPrincipal;

            //----------------------------------------------------------------------------
            currentIdentity = WindowsIdentity.GetCurrent();
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            currentPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;
            //--------------------------------------------------------------------------------

            Mach_IP = Dns.GetHostEntry(Dns.GetHostName())
                         .AddressList.First(f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                         .ToString();
            */

            
            return IsAuth;
        }

        public BindingList<KeyValuePair<int, int>> CalculateRatios(int Pk, int NoOfGarments)
        {
            var RatioOptions = new BindingList<KeyValuePair<int, int>>();
            IList<TLADM_ProductRating_Detail> PRatingDetail = null;

            using (var context = new TTI2Entities())
            {
                var Rating = context.TLADM_ProductRating.Find(Pk);
                if (Rating != null)
                {
                    if (Rating.Pr_MultiMarker)
                    {
                        PRatingDetail = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == Rating.Pr_Id).ToList();

                        if (PRatingDetail.Count != 0)
                        {
                            decimal Sum = PRatingDetail.Sum(x => x.Prd_MarkerRatio);

                            var Ratios = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == Rating.Pr_Id).ToList();
                            foreach (var Ratio in Ratios)
                            {
                                if (Ratio.Prd_MarkerRatio == 0)
                                    continue;

                                int Calc = (int)Math.Round(Ratio.Prd_MarkerRatio / Sum * NoOfGarments, 0);
                                RatioOptions.Add(new KeyValuePair<int, int>(Ratio.Prd_SizePN, Calc));
                            }
                        }
                        else
                        {
                            MessageBox.Show("Rating Detail Table does not correlate to Ratings Table. Check Rating Table. A default will apply ","Error", MessageBoxButtons.OK , MessageBoxIcon.Error  );
                            RatioOptions.Add(new KeyValuePair<int, int>(Rating.Pr_Size_FK, NoOfGarments));
                        }
                    }
                    else
                    {
                        RatioOptions.Add(new KeyValuePair<int, int>(Rating.Pr_Size_FK, NoOfGarments));
                    }
                }
            }

            return RatioOptions;
        }
       
        public string DetermineSizes(int Pk)
        {
            StringBuilder sb = new StringBuilder();
            using (var context = new TTI2Entities())
            {
                var Rating = context.TLADM_ProductRating.Find(Pk);
                if (Rating != null)
                {
                    var ExistingDetails = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == Rating.Pr_Id).ToList();
                    if (ExistingDetails.Count() > 0)
                    {
                        foreach (var row in ExistingDetails)
                        {
                            if (row.Prd_MarkerRatio == 0)
                                continue;
                            var Size = context.TLADM_Sizes.Where(x => x.SI_id == row.Prd_SizePN).FirstOrDefault();
                            if (Size != null)
                            {
                                if (sb.Length == 0)
                                    sb.Append(Size.SI_Description);
                                else
                                    sb.Append(", " + Size.SI_Description);
                            }
                        }
                    }
                    else
                    {
                        var Size = context.TLADM_Sizes.Where(x => x.SI_id == Rating.Pr_Size_Power).FirstOrDefault();
                        if(Size != null)
                            sb.Append(Size.SI_Description);
                    }
                }
            }
            return sb.ToString();
        }

        public BindingList<KeyValuePair<int, String>> CurrentCustomers()
        {
            var CC = new BindingList<KeyValuePair<int, string>>(); 
            using (var context = new TTI2Entities())
            {
                foreach (var Customer in context.TLADM_CustomerFile)
                {
                    var Orders = context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Customer_FK == Customer.Cust_Pk && !x.TLCSVPO_Closeed).FirstOrDefault();
                    if (Orders != null)
                    {
                        CC.Add(new KeyValuePair<int, string>(Customer.Cust_Pk, Customer.Cust_Code));
                    }
                }
            }
            return CC;
        }

        public List<string> CurrentWareHouses()
        {
            List<String> CC = new List<string>();

            using (var context = new TTI2Entities())
            {
                foreach (var Whse in context.TLADM_WhseStore)
                {
                    if (!Whse.WhStore_WhseOrStore || !Whse.WhStore_GradeA)
                        continue;
                   
                    CC.Add(Whse.WhStore_Code);
                   
                }
            }
            return CC;
        }

        public List<int> GetSizePk(int Pk)
        {
            List<int> sb = new List<int>();

            using (var context = new TTI2Entities())
            {
                var Rating = context.TLADM_ProductRating.Find(Pk);
                if (Rating != null)
                {
                    var ExistingDetails = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == Rating.Pr_Id).ToList();
                    if (ExistingDetails.Count() > 0)
                    {
                        foreach (var row in ExistingDetails)
                        {
                            if (row.Prd_MarkerRatio == 0)
                                continue;
                            var Size = context.TLADM_Sizes.Where(x => x.SI_id == row.Prd_SizePN).FirstOrDefault();
                            if (Size != null)
                            {
                                sb.Add(Size.SI_id);
                            }
                        }
                    }
                    else
                    {
                        var Size = context.TLADM_Sizes.Where(x => x.SI_id == Rating.Pr_Size_Power).FirstOrDefault();
                        if (Size != null)
                            sb.Add(Size.SI_id);
                    }
                }
            }
            return sb;
        }

        public DataTable ListDetermineSizes(int Pk)
        {
           //============================================================
           //---------Define the datatable 
           //=================================================================
            System.Data.DataTable dt = new System.Data.DataTable();
            DataColumn column;

            //------------------------------------------------------
            // Create column 1. // This is the primary Key Style
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Col0";

            //---------------------------------------------------------------
            // Add the column to the DataTable.Columns collection.
            //--------------------------------------------------------------
            dt.Columns.Add(column);
            //------------------------------------------------
            // Create column 2. // This is the Secondary Key  Colour
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Decimal");
            column.ColumnName = "Col1";
            
            // Add the column to the DataTable.Columns collection.
            //--------------------------------------------------------------
            dt.Columns.Add(column);
                     
            using (var context = new TTI2Entities())
            {
                var Rating = context.TLADM_ProductRating.Find(Pk);
                if (Rating != null)
                {
                    var ExistingDetails = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == Rating.Pr_Id).ToList();
                    if (ExistingDetails.Count() > 0)
                    {
                        foreach (var row in ExistingDetails)
                        {
                            if (row.Prd_MarkerRatio == 0)
                                continue;

                            DataRow DR = dt.NewRow();
                            DR[0] = row.Prd_SizePN;
                            DR[1] = row.Prd_MarkerRatio;
                            dt.Rows.Add(DR);
                         }
                    }
                    else
                    {
                        DataRow DR = dt.NewRow();
                        DR[0] = Rating.Pr_Size_Power;
                        DR[1] = 1.00;
                        dt.Rows.Add(DR);
                     
                    }
                }
            }
            return dt;
        }

        public DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);

            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstMonday = jan1.AddDays(daysOffset);

            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            return firstMonday.AddDays(weekOfYear * 7);
        }

        public decimal FabricYield(decimal weight, decimal width)
        {
            decimal answer = 0.00M;

            if(width > 0.00M && weight > 0.00M)
                answer = 50000 / weight / width;

            return answer;
        }


        public decimal ProdLoss(decimal Amt, decimal lossP)
        {
            decimal Perc = (100 + lossP) / 100;

            return Amt * Perc;
            
        }

        public decimal ProdNLoss(decimal Amt, decimal lossP)
        {
            decimal Perc = (100 - lossP) / 100;

            return Amt * Perc;

        }

        public int LastDayOfMonth(int Mth)
        {
            int LastDay = 0;
            int[][] MonthEndDays = null;

            if (!DateTime.IsLeapYear(DateTime.Now.Year))
            {

                MonthEndDays = new int[][]
                   { new int[] {1,31},
                    new int[] {2,28},
                    new int[] {3,31},
                    new int[] {4,30},
                    new int[] {5,31},
                    new int[] {6,30},
                    new int[] {7,31},
                    new int[] {8,31},
                    new int[] {9,30},
                    new int[] {10,31},
                    new int[] {11,30},
                    new int[] {12,31}
                   };
            }
            else
            {
                MonthEndDays = new int[][]
                   { new int[] {1,31},
                    new int[] {2,29},
                    new int[] {3,31},
                    new int[] {4,30},
                    new int[] {5,31},
                    new int[] {6,30},
                    new int[] {7,31},
                    new int[] {8,31},
                    new int[] {9,30},
                    new int[] {10,31},
                    new int[] {11,30},
                    new int[] {12,31}
                   };
            }
            var result = (from u in MonthEndDays
                            where u[0] == Mth
                            select u).FirstOrDefault();
            if (result != null)
                  LastDay = result[1];

            return LastDay;
        }

        public int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }


        public void txtWin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumeric)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        public string SuccessFullTransAction()
        {
            return "Record updated successfully to database";
        }

        public void txtWin_KeyDownOEM(object sender, KeyEventArgs e)
        {
            // does 1 decimal point only and negatives
            //-------------------------------------------------------------------
            TextBox oTxt = sender as TextBox;

            nonNumeric = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace. 
                    if (e.KeyCode != Keys.Back && e.KeyCode != Keys.Decimal && e.KeyCode != Keys.Subtract)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }

            }

            if (e.KeyCode == Keys.Decimal)
            {
                var cnt = oTxt.Text.Count(x => x == '.');
                if (cnt > 0)
                {
                    nonNumeric = true;
                }
            }

            if (e.KeyCode == Keys.Subtract)
            {
                var cnt = oTxt.Text.Count(x => x == '-');
                if (cnt > 0)
                {
                    nonNumeric = true;
                }
            }

            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
        }

        public void txtWin_KeyDownJI(object sender, KeyEventArgs e)
        {
            // Just Integers 
            //----------------------------------------------------------------------------
             nonNumeric = false;
            //-------------------------------------------------------------------------
            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace. 
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }
            }

            //if shift key was pressed, it's not a number. 
            //--------------------------------------------------------
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
       }

        public void txtWin_KeyDownTS(object sender, KeyEventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            nonNumeric = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace. 
                    if (e.KeyCode != Keys.Back && e.KeyCode != Keys.OemSemicolon && e.KeyCode != Keys.Subtract)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }
            }

            if (e.KeyCode == Keys.OemSemicolon)
            {
                var cnt = oTxt.Text.Count(x => x == ':');
                if (cnt > 0)
                {
                    nonNumeric = true;
                }
            }

            if (e.KeyCode == Keys.Subtract)
            {
                var cnt = oTxt.Text.Count(x => x == '-');
                if (cnt > 0)
                {
                    nonNumeric = true;
                }
            }

            //If shift key was pressed, it's not a number. 
            /*
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
             */
 
        }
        public void txtWin_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumeric = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace. 
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }

            }
            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
            }
        }

        public bool IsValueDidgit(string TextToCheck)
        {
            bool yesIsDidgit = true;

            foreach (var c in TextToCheck)
            {
                if (!char.IsDigit(c))
                {
                    yesIsDidgit = false;
                    break;
                }
            }
            return yesIsDidgit;
        }

        public int[] PopulateNumArray(int ArrLength, int NumValue)
        {
            var items = Enumerable.Repeat<int>(NumValue, ArrLength).ToArray();
            return items;
        }

        public bool[] PopulateArray(int ArrLength, bool boolValue)
        {
            var items = Enumerable.Repeat<bool>(boolValue, ArrLength).ToArray();
            return items;
        }

        public string returnMessage(bool[] selectedarray, bool addRec, string[][] flds)
        {
            int Cnt = 0;
            StringBuilder Mess = new StringBuilder();

            foreach (var ArrayElement in selectedarray)
            {
                if (bool.FalseString == ArrayElement.ToString())
                {
                    var result = (from u in flds
                                  where u[2] == Cnt.ToString()
                                  select u).FirstOrDefault();

                    Mess.Append(result[1] + Environment.NewLine);
                }

                Cnt += 1;
            }
            return Mess.ToString();
        }

        public void WriteLog(string message, string FileType)
        {
            string FileExt = ".txt";
            string FileName = FileType + FileExt;
            string Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Location + "\\" + FileName;
            if (!File.Exists(path))
            {
                using (StreamWriter outfile =
                new StreamWriter(path))
                {
                    outfile.WriteLine(message, Environment.NewLine);
                    outfile.Close();
                }
            }
            else
            {
                using (StreamWriter outfile = File.AppendText(path))
                {
                    outfile.WriteLine(message, Environment.NewLine);
                    outfile.Close();
                }
            }
        }

        public List<int> ExtrapNumber(int Number, int Cnt)
        {
            List<int> ans = new List<int>();
            while (Number > 0)
            {
                int power = (int)Math.Pow(2.00D, (double) Cnt);
                if (Number >= power)
                {
                    ans.Add(power);
                    Number -= power;
                }
                Cnt -= 1;
            }

            return ans;
        }

        

        public DateTime CenturyDate(Int32 CDN)
        {
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            DateTime now = centuryBegin.AddDays(CDN);
            return now;
           
    
        }

        public Int32 CenturyDayNumber(DateTime today)
        {
            DateTime centuryBegin = new DateTime(2001, 1, 1);
            DateTime currentDate = today;

            long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

            return  elapsedSpan.Days;

        }

        public DataGridView Get_Styles(DataGridView odgv, int LabelId)
        {
            DataGridView oDgv = odgv;
                    
            oDgv.Rows.Clear();

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Styles
                                   .Where(x=>x.Sty_Label_FK == LabelId)
                                   .OrderBy(x => x.Sty_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.Sty_Description;

                    if (ExistingRow.Sty_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.Sty_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.Sty_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.Sty_Id.ToString();
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.Sty_Sizes_PN;
                    oDgv.Rows[index].Cells[6].Value = ExistingRow.Sty_Trims_PN;
                    oDgv.Rows[index].Cells[7].Value = 0; //ExistingRow.Sty_Labels_FK;
                    oDgv.Rows[index].Cells[8].Value = ExistingRow.Sty_Labels_FK;
                    oDgv.Rows[index].Cells[9].Value = ExistingRow.Sty_ChkMandatory;
                    oDgv.Rows[index].Cells[10].Value = ExistingRow.Sty_PastelNo;
                    oDgv.Rows[index].Cells[11].Value = ExistingRow.Sty_PastelCode;
                    oDgv.Rows[index].Cells[12].Value = ExistingRow.Sty_CottonFactor;
                    oDgv.Rows[index].Cells[13].Value = ExistingRow.Sty_Bags;
                    oDgv.Rows[index].Cells[14].Value = ExistingRow.Sty_Buttons;
                    oDgv.Rows[index].Cells[15].Value = ExistingRow.Sty_BoughtIn;
                    oDgv.Rows[index].Cells[16].Value = ExistingRow.Sty_DisplayOrder;
               }
              
            }
            return oDgv;
        }

        public bool Save_Style(DataGridView oDgv, int SelectedLabel)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Styles clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Styles.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Styles();
                            lAdd = true;
                        }

                        clrs.Sty_Description = row.Cells[0].Value.ToString();
                        clrs.Sty_Discontinued = false;

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.Sty_Discontinued = true;
                            else
                                clrs.Sty_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.Sty_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.Sty_Discontinued_Date = null;
                        
                        if (row.Cells[5].Value != null)
                           // clrs.Sty_Sizes_PN = (int)Math.Pow(2.00D, (double)row.Index);
                           clrs.Sty_Sizes_PN = Convert.ToInt32(row.Cells[5].Value.ToString());

                        if (row.Cells[6].Value != null)
                           clrs.Sty_Trims_PN = Convert.ToInt32(row.Cells[6].Value.ToString());

                        if (row.Cells[7].Value != null)
                            clrs.Sty_Labels_FK = Convert.ToInt32(row.Cells[7].Value.ToString());

                        // Note for the file ... This is not a bug...Long story
                        //============================================================
                        if (row.Cells[8].Value != null)
                            clrs.Sty_Labels_FK = Convert.ToInt32(row.Cells[8].Value.ToString());

                        if (row.Cells[9].Value != null)
                        {
                            if (row.Cells[9].Value.ToString() == bool.TrueString)
                                clrs.Sty_ChkMandatory = true;
                            else
                                clrs.Sty_ChkMandatory = false;
                        }
                        else
                        {
                            clrs.Sty_ChkMandatory = false;
                        }

                        if (row.Cells[10].Value != null)
                        {
                               clrs.Sty_PastelNo = (int)row.Cells[10].Value;
                        }
                        else
                        {
                            clrs.Sty_PastelNo = 1;
                        }

                        if (row.Cells[11].Value != null)
                        {
                            clrs.Sty_PastelCode = row.Cells[11].Value.ToString();
                        }
                        else
                        {
                            clrs.Sty_PastelCode = string.Empty;
                        }

                        try
                        {
                            clrs.Sty_CottonFactor = Convert.ToInt32(row.Cells[12].Value.ToString());
                        }
                        catch (System.Exception ex)
                        {
                            clrs.Sty_CottonFactor = 0;
                        }

                        try
                        {
                            clrs.Sty_Bags = Convert.ToInt32(row.Cells[13].Value.ToString());
                        }
                        catch (System.Exception ex)
                        {
                            clrs.Sty_Bags = 0;
                        }

                        try
                        {
                            clrs.Sty_Buttons = Convert.ToBoolean(row.Cells[14].Value.ToString());
                        }
                        catch (System.Exception ex)
                        {
                            clrs.Sty_Buttons = false;
                        }

                        try
                        {
                            clrs.Sty_BoughtIn = Convert.ToBoolean(row.Cells[15].Value.ToString());
                        }
                        catch (System.Exception ex)
                        {
                            clrs.Sty_BoughtIn = false;
                        }

                        try
                        {
                            clrs.Sty_DisplayOrder = Convert.ToInt32(row.Cells[16].Value.ToString());
                        }
                        catch (System.Exception ex)
                        {
                            clrs.Sty_DisplayOrder = 0;
                        }

                        clrs.Sty_Label_FK = SelectedLabel;

                        if (lAdd)
                            Context.TLADM_Styles.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }


        public DataGridView Get_Colours(DataGridView odgv)
        {
            DataGridView oDgv = odgv;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Colours
                                   .OrderBy(x => x.Col_Description).ToList();
          
                ((DataGridViewTextBoxColumn)oDgv.Columns[5]).MaxInputLength = 5;
          
                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.Col_Description;

                    if (ExistingRow.Col_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.Col_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.Col_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.Col_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.Col_PowerN.ToString();
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.Col_FinishedCode;
                    oDgv.Rows[index].Cells[6].Value = ExistingRow.Col_StandardTime;
                    oDgv.Rows[index].Cells[7].Value = ExistingRow.Col_AuxPowerN.ToString();
                    
                    if(ExistingRow.Col_Benchmark) 
                        oDgv.Rows[index].Cells[8].Value = true;
                    else
                        oDgv.Rows[index].Cells[8].Value = false;

                    oDgv.Rows[index].Cells[9].Value = ExistingRow.Col_StandardTime;

                    oDgv.Rows[index].Cells[10].Value = (bool)ExistingRow.Col_Padding;

                }
            }
            return oDgv;
        }

        public bool Save_Colours(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Colours clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Colours.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Colours();
                            lAdd = true;
                        }

                        clrs.Col_Description = row.Cells[0].Value.ToString();
                        clrs.Col_Discontinued = false;

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.Col_Discontinued = true;
                            else
                                clrs.Col_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.Col_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.Col_Discontinued_Date = null;

                        clrs.Col_PowerN = (int)Math.Pow(2.00D, (double)row.Index);

                       
                        if (row.Cells[4].Value == null && lAdd)
                        {
                            ///clrs.Col_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                            // clrs.Col_PowerN = 1;
                        }
                        else
                            //clrs.Col_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());
                            // clrs.Col_PowerN = 1;

                        if (row.Cells[5].Value != null)
                            clrs.Col_FinishedCode = row.Cells[5].Value.ToString();
                        else
                            clrs.Col_FinishedCode = string.Empty;
                       

                        clrs.Col_Display = clrs.Col_FinishedCode + " " + clrs.Col_Description;

                        
                        if (row.Cells[6].Value != null)
                        {
                            if (row.Cells[6].Value != null)
                                clrs.Col_StandardTime = (decimal)row.Cells[6].Value;
                            else
                                clrs.Col_StandardTime = 0.00M;

                        }
                        

                        if (row.Cells[8].Value != null)
                        {
                            if (row.Cells[8].Value != null)
                                clrs.Col_Benchmark  = (bool)row.Cells[8].Value;
                            else
                                clrs.Col_Benchmark = false;

                        }

                        if (row.Cells[9].Value != null)
                        {
                            if (row.Cells[9].Value != null)
                                clrs.Col_StandardTime = (decimal)row.Cells[9].Value;
                            else
                                clrs.Col_StandardTime = 0.00M;
                        }

                        if((bool)row.Cells[10].Value)
                        {
                            clrs.Col_Padding = (bool)row.Cells[10].Value;
                        }
                        else
                        {
                            clrs.Col_Padding = false;
                        }

                        if (lAdd)
                            Context.TLADM_Colours.Add(clrs);

                        try
                        {

                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            var exceptionMessages = new StringBuilder();
                            do
                            {
                                exceptionMessages.Append(ex.Message);
                                ex = ex.InnerException;
                            }
                            while (ex != null);
                            MessageBox.Show(exceptionMessages.ToString());
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }

        /*

        public DataGridView Get_MaintenanceDetail(DataGridView odgv)
        {
           
        }

        public bool Save_MaintenanceDetail(DataGridView oDgv)
        {
           
        }
        */

        /*
        public DataGridView Get_AuxColours(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            ((DataGridViewTextBoxColumn)oDgv.Columns[3]).MaxInputLength = 5;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_AuxColours
                                   .OrderBy(x => x.AuxCol_Description).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.AuxCol_Description;
                    oDgv.Rows[index].Cells[1].Value = ExistingRow.AuxCol_Id;
                    oDgv.Rows[index].Cells[2].Value = ExistingRow.AuxCol_PowerN;
                    oDgv.Rows[index].Cells[3].Value = ExistingRow.AuxCol_FinishedCode;

                }
            }
            return oDgv;
        }

        public bool Save_AuxColours(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_AuxColours clrs = null;

                        if (row.Cells[1].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[1].Value.ToString());
                            clrs = Context.TLADM_AuxColours.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_AuxColours();
                            lAdd = true;
                        }

                        clrs.AuxCol_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[2].Value == null && lAdd)
                        {
                            clrs.AuxCol_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.AuxCol_PowerN = Convert.ToInt32(row.Cells[2].Value.ToString());

                        if (row.Cells[3].Value != null)
                            clrs.AuxCol_FinishedCode = row.Cells[3].Value.ToString();
                        else
                            clrs.AuxCol_FinishedCode = string.Empty; 
                       
                        if (lAdd)
                            Context.TLADM_AuxColours.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }
        */

        public DataGridView Get_Yarn(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
                    

            using (var Context = new TTI2Entities())
            {
                

                var ExistingData = Context.TLADM_Yarn
                                   .OrderBy(x => x.YA_Id).ToList();
               
                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.YA_Description;

                    if (ExistingRow.YA_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.YA_Discontnued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.YA_Discontnued_Date;

                    oDgv.Rows[index].Cells[3].Value  = ExistingRow.YA_Id;
                    oDgv.Rows[index].Cells[4].Value  = ExistingRow.YA_PowerN;
                    oDgv.Rows[index].Cells[5].Value  = ExistingRow.YA_YarnType;
                    oDgv.Rows[index].Cells[6].Value  = ExistingRow.YA_TexCount;
                    oDgv.Rows[index].Cells[7].Value  = ExistingRow.YA_Twist;
                    oDgv.Rows[index].Cells[8].Value  = ExistingRow.YA_CottonOrigin_FK;
                    oDgv.Rows[index].Cells[9].Value  = ExistingRow.YA_Supplier_FK;
                    oDgv.Rows[index].Cells[10].Value = ExistingRow.YA_ProductType_FK;
                    
                    if(ExistingRow.YA_Blocked)
                        oDgv.Rows[index].Cells[11].Value = true;
                    else
                         oDgv.Rows[index].Cells[11].Value = false;

                    oDgv.Rows[index].Cells[12].Value = ExistingRow.YA_UOM_Fk;
                    
                    if(ExistingRow.YA_StdCost_Show)
                        oDgv.Rows[index].Cells[13].Value = true;
                    else
                        oDgv.Rows[index].Cells[13].Value = false;

                    oDgv.Rows[index].Cells[14].Value = ExistingRow.YA_StdCost_Actual;
                    oDgv.Rows[index].Cells[15].Value = ExistingRow.YA_ROL;
                    oDgv.Rows[index].Cells[16].Value = ExistingRow.YA_ROQ;

                    if(ExistingRow.YA_Qty_Show)
                        oDgv.Rows[index].Cells[17].Value = true;
                    else
                        oDgv.Rows[index].Cells[17].Value = false;

                    oDgv.Rows[index].Cells[18].Value = ExistingRow.YA_ConeColour;
                   
                }
            }
            return oDgv;
        }

        public bool Save_Yarn(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;
                        TLADM_Yarn clrs = null;
                                            
                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Yarn.Find(index);
                        }
                        else
                        {
                            clrs = new TLADM_Yarn();
                            lAdd = true;
                        }

                        clrs.YA_Description = row.Cells[0].Value.ToString();
                        clrs.YA_Discontinued = false;

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.YA_Discontinued = true;
                            else
                                clrs.YA_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.YA_Discontnued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.YA_Discontnued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.YA_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.YA_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());


                        clrs.YA_YarnType = row.Cells[5].Value.ToString();
                        clrs.YA_TexCount = Convert.ToDecimal(row.Cells[6].Value.ToString());
                        clrs.YA_Twist = Convert.ToDecimal(row.Cells[7].Value.ToString());
                        clrs.YA_CottonOrigin_FK = Convert.ToInt32(row.Cells[8].Value.ToString());
                        clrs.YA_Supplier_FK = Convert.ToInt32(row.Cells[9].Value.ToString());
                        clrs.YA_ProductType_FK = Convert.ToInt32(row.Cells[10].Value.ToString());

                        if (row.Cells[11].Value != null)
                        {
                            if (row.Cells[11].Value.ToString() == bool.TrueString)
                                clrs.YA_Blocked = true;
                            else
                                clrs.YA_Blocked = false;
                        }
                        else
                            clrs.YA_Blocked = false;


                        clrs.YA_UOM_Fk = Convert.ToInt32(row.Cells[12].Value.ToString());

                        if (row.Cells[13].Value != null)
                        {
                            if (row.Cells[13].Value.ToString() == bool.TrueString)
                                clrs.YA_StdCost_Show = true;
                            else
                                clrs.YA_StdCost_Show = false;
                        }
                        else
                            clrs.YA_StdCost_Show = false;

                        clrs.YA_StdCost_Actual = Convert.ToDecimal(row.Cells[14].Value.ToString());

                        clrs.YA_ROL = Convert.ToInt32(row.Cells[15].Value.ToString());
                        clrs.YA_ROQ = Convert.ToInt32(row.Cells[16].Value.ToString());

                        if (row.Cells[17].Value != null)
                        {
                            if (row.Cells[17].Value.ToString() == bool.TrueString)
                                clrs.YA_Qty_Show = true;
                            else
                                clrs.YA_Qty_Show = false;
                        }
                        else
                            clrs.YA_Qty_Show = false;

                        clrs.YA_ConeColour = row.Cells[18].Value.ToString();
                        if (lAdd)
                            Context.TLADM_Yarn.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }

        public DataGridView Get_Griege(DataGridView odgv)
        {
            DataGridView oDgv = odgv;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Griege
                                   .OrderBy(x => x.TLGreige_Description).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.TLGreige_Description;

                    if (ExistingRow.TLGriege_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.TLGreige_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.TLGreige_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.TLGreige_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.TLGreige_PowerN.ToString();
                    //----------------------------------------------------------------------------------

                    oDgv.Rows[index].Cells[5].Value = ExistingRow.TLGreige_Quality_FK;
                    oDgv.Rows[index].Cells[6].Value = ExistingRow.TLGreige_YarnPowerN;
                    oDgv.Rows[index].Cells[7].Value = ExistingRow.TLGreige_FabricWeight_FK;
                    oDgv.Rows[index].Cells[8].Value = ExistingRow.TLGreige_FabricWidth_FK;
                    oDgv.Rows[index].Cells[9].Value = ExistingRow.TLGreige_Machine_FK;
                    oDgv.Rows[index].Cells[10].Value = ExistingRow.TLGreige_ProductType_FK;
                    oDgv.Rows[index].Cells[11].Value = ExistingRow.TLGreige_BarCode;
                    oDgv.Rows[index].Cells[12].Value = ExistingRow.TLGreige_UOM_Fk;
                    oDgv.Rows[index].Cells[13].Value = ExistingRow.TLGreige_ROL;
                    oDgv.Rows[index].Cells[14].Value = ExistingRow.TLGreige_ROQ;
                    oDgv.Rows[index].Cells[15].Value = ExistingRow.TLGreige_ShowQty;
                    oDgv.Rows[index].Cells[16].Value = ExistingRow.TLGreige_StockTakeFreq_FK;
                    oDgv.Rows[index].Cells[17].Value = ExistingRow.TLGreige_KgPerPiece;
                    oDgv.Rows[index].Cells[18].Value = ExistingRow.TLGreige_Meters;
                    oDgv.Rows[index].Cells[19].Value = ExistingRow.TLGreige_FaultsAllowed;
                    oDgv.Rows[index].Cells[21].Value = ExistingRow.TLGreige_IsBoughtIn;
                    oDgv.Rows[index].Cells[22].Value = ExistingRow.TLGreige_CubicWeight;

                }
            }
            return oDgv;
        }

        public bool Save_Griege(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Griege clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Griege.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Griege();
                            lAdd = true;
                        }

                        clrs.TLGreige_Description = row.Cells[0].Value.ToString();
                        clrs.TLGriege_Discontinued  = false;
                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.TLGriege_Discontinued = true;
                            else
                                clrs.TLGriege_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.TLGreige_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.TLGreige_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.TLGreige_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.TLGreige_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());


                        clrs.TLGreige_Quality_FK = Convert.ToInt32(row.Cells[5].Value.ToString());
                        clrs.TLGreige_YarnPowerN = Convert.ToInt32(row.Cells[6].Value.ToString());
                        clrs.TLGreige_FabricWeight_FK = Convert.ToInt32(row.Cells[7].Value.ToString());
                        clrs.TLGreige_FabricWidth_FK = Convert.ToInt32(row.Cells[8].Value.ToString());
                        clrs.TLGreige_Machine_FK = Convert.ToInt32(row.Cells[9].Value.ToString());
                        clrs.TLGreige_ProductType_FK = Convert.ToInt32(row.Cells[10].Value.ToString());
                        clrs.TLGreige_UOM_Fk = Convert.ToInt32(row.Cells[12].Value.ToString());
                        clrs.TLGreige_ROL = Convert.ToInt32(row.Cells[13].Value.ToString());
                        clrs.TLGreige_ROQ = Convert.ToInt32(row.Cells[14].Value.ToString());
                      

                        if (row.Cells[11].Value != null && !String.IsNullOrEmpty(row.Cells[11].Value.ToString()))
                        {
                            if (row.Cells[11].Value.ToString() == bool.TrueString)
                                clrs.TLGreige_BarCode = true;
                            else
                                clrs.TLGreige_BarCode = false;
                        }
                        else
                            clrs.TLGreige_BarCode = false;
                       
                        if (row.Cells[15].Value != null && !String.IsNullOrEmpty(row.Cells[15].Value.ToString()))
                        {
                            if (row.Cells[15].Value.ToString() == bool.TrueString)
                                clrs.TLGreige_ShowQty = true;
                            else
                                clrs.TLGreige_ShowQty = false;

                        }
                        else
                            clrs.TLGreige_ShowQty = false;

                        if (row.Cells[16].Value != null && !String.IsNullOrEmpty(row.Cells[16].Value.ToString()))
                        {
                            clrs.TLGreige_StockTakeFreq_FK = Convert.ToInt32(row.Cells[16].Value.ToString()); 
                        }

                        clrs.TLGreige_KgPerPiece = Convert.ToDecimal(row.Cells[17].Value.ToString());
                        clrs.TLGreige_Meters = Convert.ToInt32(row.Cells[18].Value.ToString());
                        clrs.TLGreige_FaultsAllowed = Convert.ToInt32(row.Cells[19].Value.ToString());
                        if (row.Cells[21].Value != null)
                            clrs.TLGreige_IsBoughtIn = (bool)row.Cells[21].Value;
                        else
                            clrs.TLGreige_IsBoughtIn = false;

                        clrs.TLGreige_CubicWeight = Decimal.Parse(row.Cells[22].Value.ToString());

                        //------------------------------------------------------------------------------------------------
                        if (lAdd)
                            Context.TLADM_Griege.Add(clrs);

                        //-----------------------------------------------------------------------------------------------
                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }
        public DataGridView Get_FabricWidth(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
         

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_FabWidth
                                   .OrderBy(x => x.FW_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.FW_Description;

                    if (ExistingRow.FW_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.FW_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.FW_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.FW_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.FW_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_FabricWidth(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_FabWidth clrs = null;
                       
                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_FabWidth.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_FabWidth();
                            lAdd = true;
                        }

                        clrs.FW_Description = row.Cells[0].Value.ToString();
                        clrs.FW_Discontinued = false;
                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.FW_Discontinued = true;
                            else
                                clrs.FW_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.FW_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.FW_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.FW_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.FW_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());


                        clrs.FW_Calculation_Value = Convert.ToInt32(clrs.FW_Description);

                        if (lAdd)
                            Context.TLADM_FabWidth.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }
        public DataGridView Get_Ribbing(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Ribbing
                                   .OrderBy(x => x.RI_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.RI_Description;

                    if (ExistingRow.RI_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.RI_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.RI_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.RI_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.RI_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_Ribbing(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Ribbing clrs = null;
                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Ribbing.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Ribbing();
                            lAdd = true;
                        }

                        clrs.RI_Description = row.Cells[0].Value.ToString();
                        clrs.RI_Discontinued = false;

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.RI_Discontinued = true;
                            else
                                clrs.RI_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.RI_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.RI_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.RI_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.RI_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (lAdd)
                            Context.TLADM_Ribbing.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }
        public DataGridView Get_Trims(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Trims
                                   .OrderBy(x => x.TR_Description).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.TR_Description;

                    if (ExistingRow.TR_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.TR_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.TR_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.TR_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.TR_powerN.ToString();
                   
                    if (ExistingRow.Tr_Body == true)
                    {
                        oDgv.Rows[index].Cells[5].Value = true;
                    }
                    else
                        oDgv.Rows[index].Cells[5].Value = false;

                    oDgv.Rows[index].Cells[6].Value = ExistingRow.TR_Width;
                    oDgv.Rows[index].Cells[7].Value = ExistingRow.TR_Weight;
                    if (ExistingRow.TR_Greige_FK != null)
                        oDgv.Rows[index].Cells[8].Value = (int)ExistingRow.TR_Greige_FK;

                    oDgv.Rows[index].Cells[9].Value = (bool)ExistingRow.TR_IsSizes;

                    if (ExistingRow.TR_Size_FK != null)
                        oDgv.Rows[index].Cells[10].Value = (int)ExistingRow.TR_Size_FK;

                }
            }
            return oDgv;
        }

        public bool Save_Trims(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;
            var Index = 1;
            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Trims clrs = null;

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Trims.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Trims();
                            lAdd = true;
                        }

                        clrs.TR_Description = row.Cells[0].Value.ToString();
                        clrs.TR_Discontinued = false;
                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.TR_Discontinued = true;
                            else
                                clrs.TR_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.TR_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.TR_Discontinued_Date = null;

                       // clrs.TR_powerN = (int)Math.Pow(2.00D, (double)Index++);
                                                
                        if (row.Cells[5].Value != null)
                        {
                            if (row.Cells[5].Value.ToString() == bool.TrueString)
                                clrs.Tr_Body = true;
                            else
                                clrs.Tr_Body = false;

                        }

                        if (row.Cells[6].Value != null)
                        {
                            clrs.TR_Width = (int)row.Cells[6].Value;
                        }

                        if (row.Cells[7].Value != null)
                        {
                            clrs.TR_Weight = (int)row.Cells[7].Value;
                        }

                        if (row.Cells[8].Value != null)
                        {
                            clrs.TR_Greige_FK = (int)row.Cells[8].Value;
                        }

                        if (row.Cells[9].Value == null)
                        {
                            row.Cells[9].Value = false;
                        }

                        clrs.TR_IsSizes = (bool)row.Cells[9].Value;

                        if (row.Cells[10].Value != null)
                        {
                            clrs.TR_Size_FK  = (int)row.Cells[10].Value;
                        }
                        if (lAdd)
                        {
                            Context.TLADM_Trims.Add(clrs);
                        }
                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }
            }
            return lTransSuccessful;
        }

        public DataGridView Get_Sizes(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Sizes
                                   .OrderBy(x => x.SI_DisplayOrder).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.SI_Description;

                    if (ExistingRow.SI_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.SI_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.SI_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.SI_id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.SI_PowerN.ToString();
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.SI_PastelNo;
                    oDgv.Rows[index].Cells[6].Value = ExistingRow.SI_DisplayOrder;
                    oDgv.Rows[index].Cells[7].Value = ExistingRow.SI_ColNumber;

                }
            }
            return oDgv;
        }

        public bool Save_Sizes(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Sizes clrs = null;
                      
                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Sizes.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Sizes();
                            lAdd = true;
                        }

                        clrs.SI_Description = row.Cells[0].Value.ToString();
                        clrs.SI_Discontinued = false;
                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.SI_Discontinued = true;
                            else
                                clrs.SI_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.SI_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.SI_Discontinued_Date = null;

                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.SI_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.SI_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (row.Cells[5].Value != null)
                            clrs.SI_PastelNo = (int)row.Cells[5].Value;
                        else
                            clrs.SI_PastelNo = 1;

                        if (row.Cells[6].Value != null)
                            clrs.SI_DisplayOrder = (int)row.Cells[6].Value;
                        else
                            clrs.SI_DisplayOrder = 999;

                        if (row.Cells[7].Value != null)
                            clrs.SI_ColNumber = (int)row.Cells[7].Value;
                        else
                            clrs.SI_ColNumber = 1;

                        if (lAdd)
                            Context.TLADM_Sizes.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }

        public DataGridView Get_Labels(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_Labels
                                   .OrderBy(x => x.Lbl_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.Lbl_Description;

                    if (ExistingRow.Lbl_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.Lbl_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.Lbl_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.Lbl_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.Lbl_PowerN.ToString();
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.Lbl_Customer_FK;
                }
            }
            return oDgv;
        }

        public bool Save_Labels(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_Labels clrs = null;
                      
                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_Labels.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_Labels();
                            lAdd = true;
                        }

                        clrs.Lbl_Discontinued= false;
                        clrs.Lbl_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.Lbl_Discontinued = true;
                            else
                                clrs.Lbl_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.Lbl_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.Lbl_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.Lbl_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.Lbl_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (row.Cells[5].Value != null)
                        {
                            clrs.Lbl_Customer_FK = Convert.ToInt32(row.Cells[5].Value.ToString());
                        }

                        if (lAdd)
                            Context.TLADM_Labels.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }

            return lTransSuccessful;
        }

        public DataGridView Get_FabricProduct(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_FabricProduct
                                   .OrderBy(x => x.FP_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.FP_Description;

                    if (ExistingRow.FP_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.FP_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.FP_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.FP_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.FP_PowerN.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_FabricProduct(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_FabricProduct clrs = null;
                       

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_FabricProduct.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_FabricProduct();
                            lAdd = true;
                        }

                        clrs.FP_Description = row.Cells[0].Value.ToString();
                        clrs.FP_Discontinued = false;
                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.FP_Discontinued = true;
                            else
                                clrs.FP_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.FP_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.FP_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.FP_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.FP_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (lAdd)
                            Context.TLADM_FabricProduct.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }

            return lTransSuccessful;
        }

        public DataGridView Get_FabricWeight(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_FabricWeight
                                   .OrderBy(x => x.FWW_Id).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.FWW_Description;

                    if (ExistingRow.FWW_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.FWW_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.FWW_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.FWW_Id.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.FWW_PowerN.ToString();
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.FWW_Calculation_Value.ToString();
                }
            }
            return oDgv;
        }

        public bool Save_FabricWeight(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_FabricWeight clrs = null;
                     
                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_FabricWeight.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_FabricWeight();
                            lAdd = true;
                        }

                        clrs.FWW_Description = row.Cells[0].Value.ToString();
                        clrs.FWW_Discontinued = false;

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.FWW_Discontinued = true;
                            else
                                clrs.FWW_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.FWW_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.FWW_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.FWW_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.FWW_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        if (row.Cells[5].Value != null)
                        {
                            clrs.FWW_Calculation_Value = Convert.ToInt32(row.Cells[5].Value.ToString());
                        }
                        else
                            clrs.FWW_Calculation_Value = 0;

                        if (lAdd)
                            Context.TLADM_FabricWeight.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }

            return lTransSuccessful;
        }

        public DataGridView Get_GreigeQuality(DataGridView odgv)
        {
            DataGridView oDgv = odgv;
            

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_GreigeQuality
                                   .OrderBy(x => x.GQ_Pk).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.GQ_Description;

                    if (ExistingRow.GQ_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.GQ_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.GQ_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.GQ_Pk.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.GQ_PowerN.ToString();
             
                }
            }
            return oDgv;
        }

        public bool Save_GreigeQuality(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_GreigeQuality clrs = null;
                       

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_GreigeQuality.Find(index);

                        }
                        else
                        {
                            clrs =  new TLADM_GreigeQuality();
                            lAdd = true;
                        }

                        clrs.GQ_Discontinued = false;

                        clrs.GQ_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.GQ_Discontinued = true;
                            else
                                clrs.GQ_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.GQ_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.GQ_Discontinued_Date = null;



                        clrs.GQ_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                                               
                        if (lAdd)
                            Context.TLADM_GreigeQuality.Add(clrs);

                       
                    }
                }

                try
                {
                    Context.SaveChanges();
                    lTransSuccessful = true;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    lTransSuccessful = false;
                }
            }
            return lTransSuccessful;
        }

        public DataGridView Get_FabricAttributes(DataGridView odgv)
        {
            DataGridView oDgv = odgv;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_FabricAttributes
                                   .OrderBy(x => x.FbAtrib_Pk).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.FbAtrib_Description;

                    if (ExistingRow.FbAtrib_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.FbAtrib_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.FbAtrib_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.FbAtrib_Pk.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.FbAtrib_PowerN.ToString();

                    oDgv.Rows[index].Cells[5].Value  = ExistingRow.FbAtrib_Greige_FK;
                    oDgv.Rows[index].Cells[6].Value  = ExistingRow.FbAtrib_Brand_FK;
                    oDgv.Rows[index].Cells[7].Value  = ExistingRow.FbAtrib_FabProductTypes_FK;
                    oDgv.Rows[index].Cells[8].Value  = ExistingRow.FbAtrib_Colour_FK;
                    oDgv.Rows[index].Cells[9].Value  = ExistingRow.FbAtrib_FabProduct_FK;
                    oDgv.Rows[index].Cells[11].Value = ExistingRow.FbAtrib_UOM_Fk; 
                    oDgv.Rows[index].Cells[12].Value = ExistingRow.FbAtrib_PreferedSupplier_FK;

                    if (ExistingRow.FbAtrib_Blocked)
                        oDgv.Rows[index].Cells[10].Value = true;
                    else
                        oDgv.Rows[index].Cells[10].Value = false;

                    if (ExistingRow.FbAtrib_BarCode)
                        oDgv.Rows[index].Cells[13].Value = true;
                    else
                        oDgv.Rows[index].Cells[13].Value = false;
                    
                    if (ExistingRow.FbAtrib_ShowQty)
                        oDgv.Rows[index].Cells[14].Value = true;
                    else
                        oDgv.Rows[index].Cells[14].Value = false;

                }
            }
            return oDgv;
        }

        public bool Save_FabricAttributes(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_FabricAttributes clrs = null;
                     

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_FabricAttributes.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_FabricAttributes();
                            lAdd = true;
                        }

                        clrs.FbAtrib_Discontinued = false;
                        clrs.FbAtrib_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.FbAtrib_Discontinued = true;
                            else
                                clrs.FbAtrib_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.FbAtrib_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.FbAtrib_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.FbAtrib_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.FbAtrib_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        //--------------------------------------------------------------------------------
                        //
                        //--------------------------------------------------------------------------------

                        clrs.FbAtrib_Greige_FK            = Convert.ToInt32(row.Cells[5].Value.ToString());
                        clrs.FbAtrib_Brand_FK             = Convert.ToInt32(row.Cells[6].Value.ToString());
                        clrs.FbAtrib_FabProductTypes_FK   = Convert.ToInt32(row.Cells[7].Value.ToString());
                        clrs.FbAtrib_Colour_FK            = Convert.ToInt32(row.Cells[8].Value.ToString());
                        clrs.FbAtrib_FabProduct_FK        = Convert.ToInt32(row.Cells[9].Value.ToString());
                        clrs.FbAtrib_UOM_Fk               = Convert.ToInt32(row.Cells[11].Value.ToString());
                        clrs.FbAtrib_PreferedSupplier_FK  = Convert.ToInt32(row.Cells[12].Value.ToString());

                        if (row.Cells[10].Value != null)
                        {
                            if (row.Cells[10].Value.ToString() == bool.TrueString)
                                clrs.FbAtrib_Blocked = true;
                            else
                                clrs.FbAtrib_Blocked = false;
                        }
                        else
                            clrs.FbAtrib_Blocked = false;

                        if (row.Cells[13].Value != null)
                        {
                            if (row.Cells[13].Value.ToString() == bool.TrueString)
                                clrs.FbAtrib_BarCode = true;
                            else
                                clrs.FbAtrib_BarCode = false;
                        }
                        else
                            clrs.FbAtrib_BarCode = false;

                        if (row.Cells[14].Value != null)
                        {
                            if (row.Cells[14].Value.ToString() == bool.TrueString)
                                clrs.FbAtrib_ShowQty = true;
                            else
                                clrs.FbAtrib_ShowQty = false;
                        }
                        else
                            clrs.FbAtrib_Blocked = false;
                       
                        if (lAdd)
                            Context.TLADM_FabricAttributes.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }

            }
            return lTransSuccessful;
        }

        public DataGridView Get_PanelAttributes(DataGridView odgv)
        {
            DataGridView oDgv = odgv;

            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_PanelAttributes
                                   .OrderBy(x => x.Pan_PK).ToList();

                foreach (var ExistingRow in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = ExistingRow.Pan_Description;

                    if (ExistingRow.Pan_Discontinued == true)
                        oDgv.Rows[index].Cells[1].Value = true;
                    else
                        oDgv.Rows[index].Cells[1].Value = false;

                    if (ExistingRow.Pan_Discontinued_Date != null)
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.Pan_Discontinued_Date.ToString();

                    oDgv.Rows[index].Cells[3].Value = ExistingRow.Pan_PK.ToString();
                    oDgv.Rows[index].Cells[4].Value = ExistingRow.Pan_PowerN.ToString();
                    //oDgv.Rows[index].Cells[5].Value = ExistingRow.Pan_Style_FK;
                    oDgv.Rows[index].Cells[5].Value = ExistingRow.Pan_Grade;
                    oDgv.Rows[index].Cells[6].Value = ExistingRow.Pan_Size_FK;
                    oDgv.Rows[index].Cells[8].Value = ExistingRow.Pan_UOM_FK;
                    oDgv.Rows[index].Cells[9].Value = ExistingRow.Pan_PreferedSupplier_FK;
                    oDgv.Rows[index].Cells[11].Value = ExistingRow.Pan_FabricAtributes_FK;

                    if (ExistingRow.Pan_Blocked)
                        oDgv.Rows[index].Cells[7].Value = true;
                    else
                        oDgv.Rows[index].Cells[7].Value = false;

                    if (ExistingRow.Pan_ShowQty)
                        oDgv.Rows[index].Cells[10].Value = true;
                    else
                        oDgv.Rows[index].Cells[10].Value = false;

                }
            }
            return oDgv;
        }

        public bool Save_PanelAttributes(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[0].Value != null && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                    {
                        lAdd = false;

                        TLADM_PanelAttributes clrs = null;
                      

                        if (row.Cells[3].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[3].Value.ToString());
                            clrs = Context.TLADM_PanelAttributes.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_PanelAttributes();
                            lAdd = true;
                        }

                        clrs.Pan_Discontinued = false;
                        clrs.Pan_Description = row.Cells[0].Value.ToString();

                        if (row.Cells[1].Value != null)
                        {
                            if (row.Cells[1].Value.ToString() == bool.TrueString)
                                clrs.Pan_Discontinued = true;
                            else
                                clrs.Pan_Discontinued = false;

                        }

                        if (row.Cells[2].Value != null && !String.IsNullOrEmpty(row.Cells[2].Value.ToString()))
                        {
                            clrs.Pan_Discontinued_Date = Convert.ToDateTime(row.Cells[2].Value.ToString());
                        }
                        else
                            clrs.Pan_Discontinued_Date = null;



                        if (row.Cells[4].Value == null && lAdd)
                        {
                            clrs.Pan_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            clrs.Pan_PowerN = Convert.ToInt32(row.Cells[4].Value.ToString());

                        //--------------------------------------------------------------------------------
                        //
                        //--------------------------------------------------------------------------------

                        // clrs.Pan_Style_FK = Convert.ToInt32(row.Cells[5].Value.ToString());
                        clrs.Pan_Grade = row.Cells[5].Value.ToString();

                        clrs.Pan_Size_FK = Convert.ToInt32(row.Cells[6].Value.ToString());
                        clrs.Pan_UOM_FK = Convert.ToInt32(row.Cells[8].Value.ToString());
                        clrs.Pan_PreferedSupplier_FK = Convert.ToInt32(row.Cells[9].Value.ToString());
                        clrs.Pan_FabricAtributes_FK = Convert.ToInt32(row.Cells[11].Value.ToString());
                   
                        if (row.Cells[7].Value != null)
                        {
                            if (row.Cells[7].Value.ToString() == bool.TrueString)
                                clrs.Pan_Blocked = true;
                            else
                                clrs.Pan_Blocked = false;
                        }
                        else
                            clrs.Pan_Blocked = false;

                        if (row.Cells[10].Value != null)
                        {
                            if (row.Cells[10].Value.ToString() == bool.TrueString)
                                clrs.Pan_ShowQty = true;
                            else
                                clrs.Pan_ShowQty = false;
                        }
                        else
                            clrs.Pan_ShowQty = false;

                       
                        if (lAdd)
                            Context.TLADM_PanelAttributes.Add(clrs);

                        try
                        {
                            Context.SaveChanges();
                            lTransSuccessful = true;
                        }
                        catch (System.Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lTransSuccessful = false;
                        }
                    }
                }
            }
            return lTransSuccessful;
        }

        public DataGridView Get_ProductionLoss(DataGridView oDgv)
        {
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_ProductionLoss.ToList();

                foreach (var row in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = row.TLProdLoss_Pk.ToString();
                    oDgv.Rows[index].Cells[2].Value = row.TLProdLoss_Dept_Fk;
                    oDgv.Rows[index].Cells[3].Value = row.TLProdLoss_Percent.ToString();
                    oDgv.Rows[index].Cells[4].Value = row.TLProdLoss_Kg.ToString();
                }
               
            }
            return oDgv;
        }

        public bool Save_ProductionLoss(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[3].Value == null)
                        continue;

                    lAdd = false;

                    TLADM_ProductionLoss clrs = null;
                    
                    if (row.Cells[0].Value != null)
                    {
                            int index = Convert.ToInt32(row.Cells[0].Value.ToString());
                            clrs = Context.TLADM_ProductionLoss.Find(index);

                    }
                    else
                    {
                            clrs = new TLADM_ProductionLoss();
                            lAdd = true;
                    }

                    clrs.TLProdLoss_Dept_Fk = Convert.ToInt32(row.Cells[2].Value.ToString());
                    clrs.TLProdLoss_Percent = Convert.ToDecimal(row.Cells[3].Value.ToString());
                    clrs.TLProdLoss_Kg = Convert.ToInt32(row.Cells[4].Value.ToString());

                    if (lAdd)
                            Context.TLADM_ProductionLoss.Add(clrs);
                }
                try
                {
                       Context.SaveChanges();
                       lTransSuccessful = true;
                }
                catch (System.Exception ex)
                {
                       MessageBox.Show(ex.Message);
                       lTransSuccessful = false;
                 }
            }
            return lTransSuccessful;
        }

        public DataGridView Get_StyleGrades(DataGridView oDgv)
        {
            using (var Context = new TTI2Entities())
            {
                var ExistingData = Context.TLADM_StylesGrades.ToList();

                foreach (var row in ExistingData)
                {
                    var index = oDgv.Rows.Add();
                    oDgv.Rows[index].Cells[0].Value = row.TLSG_Pk;
                    oDgv.Rows[index].Cells[2].Value = row.TLSG_Style_Fk;
                    oDgv.Rows[index].Cells[3].Value = row.TLSG_Grade_A;
                    oDgv.Rows[index].Cells[4].Value = row.TLSG_Grade_B;
                }

            }
            return oDgv;
        }

        public bool Save_StyleGrades(DataGridView oDgv)
        {
            var lAdd = false;
            var lTransSuccessful = false;

            using (var Context = new TTI2Entities())
            {
                foreach (DataGridViewRow row in oDgv.Rows)
                {
                    if (row.Cells[3].Value == null)
                        continue;

                    lAdd = false;

                    TLADM_StylesGrades clrs = null;
                   
                    if (row.Cells[0].Value != null)
                    {
                        int index = Convert.ToInt32(row.Cells[0].Value.ToString());
                        clrs = Context.TLADM_StylesGrades.Find(index);

                    }
                    else
                    {
                        clrs = new TLADM_StylesGrades();
                        lAdd = true;
                    }

                    clrs.TLSG_Style_Fk = Convert.ToInt32(row.Cells[2].Value.ToString());
                    clrs.TLSG_Grade_A = row.Cells[3].Value.ToString();
                    clrs.TLSG_Grade_B = row.Cells[4].Value.ToString();

                    if (lAdd)
                        Context.TLADM_StylesGrades.Add(clrs);
                }
                try
                {
                    Context.SaveChanges();
                    lTransSuccessful = true;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    lTransSuccessful = false;
                }
            }
            return lTransSuccessful;
        }

    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Utilities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TLCSV_RePackConfig
    {
        public int PORConfig_Pk { get; set; }
        public int PORConfig_PONumber_Fk { get; set; }
        public int PORConfig_Style_FK { get; set; }
        public int PORConfig_Colour_FK { get; set; }
        public int PORConfig_Size_FK { get; set; }
        public int PORConfig_SizeBoxQty { get; set; }
        public int PORConfig_SizeBoxQty_Picked { get; set; }
        public int PORConfig_StockOnHand_FK { get; set; }
        public int PORConfig_SizeBoxQty_Delivered { get; set; }
        public int PORConfig_TotalBoxes { get; set; }
        public string PORConfig_BoxNumber { get; set; }
        public string PORConfig_Display { get; set; }
        public int PORConfig_BoxNumber_Key { get; set; }
    }
}

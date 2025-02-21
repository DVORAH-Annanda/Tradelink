using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class TLADM_ProductCodes
    {
        public Int32 Id { get; set; }
        public string ProductCode { get; set; }
        public Int32 StyleId { get; set; }
        public Int32 ColourId { get; set; }
        public Int32 SizeId { get; set; }

        // Navigation properties (for relations to other tables)
        public virtual TLADM_Styles Style { get; set; }
        public virtual TLADM_Colours Colour { get; set; }
        public virtual TLADM_Sizes Size { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LinqKit;

namespace Administration
{
    public class AdminRepository : IDisposable
    {
        protected readonly TTI2Entities _context;

        public AdminRepository()
        {
                _context = new TTI2Entities();
        }

        public TLADM_Colours LoadColour(int Pk)
        {
            return _context.TLADM_Colours.FirstOrDefault(s => s.Col_Id == Pk);
        }

        public TLADM_Styles LoadStyles(int Pk)
        {
            return _context.TLADM_Styles.FirstOrDefault(s => s.Sty_Id == Pk);
        }

        public TLADM_Sizes LoadSizes(int Pk)
        {
            return _context.TLADM_Sizes.FirstOrDefault(s => s.SI_id == Pk);
        }

        public TLADM_Labels LoadLabels(int Pk)
        {
            return _context.TLADM_Labels.FirstOrDefault(s => s.Lbl_Id == Pk);
        }

        public TLADM_Trims LoadTrims(int Pk)
        {
            return _context.TLADM_Trims.FirstOrDefault(s => s.TR_Id == Pk);
        }

        public TLADM_Griege LoadQualities(int Pk)
        {
            return _context.TLADM_Griege.FirstOrDefault(s => s.TLGreige_Id == Pk);
        }


        public void Dispose()
       {
           if (_context != null)
           {
               _context.Dispose();
           }
       }


            
    }

    public class AdminQueryParameters
    {
        public List<TLADM_Colours> Colours;
        public List<TLADM_Styles> Styles;
        public List<TLADM_Sizes> Sizes;
        public List<TLADM_Griege> Qualities;
        public List<TLADM_Labels> Labels;
        public List<TLADM_Trims> Trims;
        // public List<TLADM_Colours> ColoursForDeletion;

        public AdminQueryParameters()
        {
            Colours = new List<TLADM_Colours>();
            Styles = new List<TLADM_Styles>();
            Sizes = new List<TLADM_Sizes>();
            Qualities = new List<TLADM_Griege>();
            Trims = new List<TLADM_Trims>();
            Labels = new List<TLADM_Labels>();
            // ColoursForDeletion = new List<TLADM_Colours>();
        }
    }
}

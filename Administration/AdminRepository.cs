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
        public List<TLADM_Colours> ColoursForDeletion;

        public AdminQueryParameters()
        {
            Colours = new List<TLADM_Colours>();
            ColoursForDeletion  = new List<TLADM_Colours>();
        }

    }
}

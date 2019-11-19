using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DyeHouse
{
    public class Repository : IDisposable
    {
        protected readonly TTI2Entities _context;

        public Repository()
        {
            _context = new TTI2Entities();
        }

        public List<TLADM_Colours> LoadColours()
        {
            return _context.TLADM_Colours.ToList();
        }

        public TLADM_Colours LoadColor(int ID)
        {
            return _context.TLADM_Colours.FirstOrDefault(c => c.Col_Id == ID);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public IQueryable<TLADM_Colours> Query(ShirtQueryParameters parameters)
        {
 
            var clrs = _context.TLADM_Colours.AsQueryable();
            //Query by color.
           if (parameters.Colors.Count > 0)
           {
               var colorPredicate = PredicateBuilder.False<TLADM_Colours>();
 
              foreach (var color in parameters.Colors)
              {
                     var temp = color;
                       colorPredicate = colorPredicate.Or(s => s.Col_Id == temp.Col_Id);
              }
 
              clrs = clrs.AsExpandable().Where(colorPredicate);
           }
 
           return clrs;
        }
    }

    public class ShirtQueryParameters
    {
        public List<TLADM_Colours> Colors;

        public ShirtQueryParameters()
        {
            Colors = new List<TLADM_Colours>();
        }
        
    }

   
}

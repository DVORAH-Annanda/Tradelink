using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LinqKit;

namespace ProductionPlanning
{
   public class SECRepository : IDisposable
   {
        protected readonly TTI2Entities _context;

        public SECRepository()
        {
                _context = new TTI2Entities();
        }
        public TLSEC_Departments LoadDepartment(int Pk)
        {
            return _context.TLSEC_Departments.FirstOrDefault(s => s.TLSECDT_Pk == Pk);
        }

        public TLSEC_Sections LoadSection(int Pk)
        {
            return _context.TLSEC_Sections.FirstOrDefault(s => s.TLSECSect_Pk == Pk);
        }

        public TLSEC_UserAccess LoadUserAccess(int Pk)
        {
            return _context.TLSEC_UserAccess.FirstOrDefault(s => s.TLSECUA_Pk == Pk);
        }

        public TLSEC_UserSections LoadUserSection(int Pk)
        {
            return _context.TLSEC_UserSections.FirstOrDefault(s => s.TLSECDEP_Pk == Pk);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public IQueryable<TLSEC_Sections> TLSEC_Sections(SECQueryParameters parameters)
        {
            var SecSection = _context.TLSEC_Sections.AsQueryable();
            return SecSection;

        }

        public IQueryable<TLSEC_UserSections> TLSEC_UserSections(SECQueryParameters parameters)
        {
            var UserSecs = _context.TLSEC_UserSections.AsQueryable();
            if(parameters.UserSections.Count != 0)
            {
                var UserSectionsPredicate = PredicateBuilder.New<TLSEC_UserSections>();
                foreach (var UserSection in parameters.UserSections)
                {
                    var temp = UserSection;
                    UserSectionsPredicate = UserSectionsPredicate.Or(s => s.TLSECDEP_User_FK == temp.TLSECDEP_User_FK);
                }
                UserSecs = UserSecs.AsExpandable().Where(UserSectionsPredicate);
            }

            return UserSecs;
        }
    }
   
    public class SECQueryParameters
    {
        public List<TLSEC_Departments> Departments;
        public List<TLSEC_Sections> Sections;
        public List<TLSEC_UserAccess> UserAccess;
        public List<TLSEC_UserSections> UserSections;
       
        public SECQueryParameters()
        {
            Departments = new List<TLSEC_Departments>();
            Sections = new List<TLSEC_Sections>();
            UserAccess = new List<TLSEC_UserAccess>();
            UserSections = new List<TLSEC_UserSections>();
        }

    }
}

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

namespace Security
{
    public partial class frmViewSecurity : Form
    {
        public int _RepNo;
        ReportSecOptions _RepOpts;

        public frmViewSecurity(int RNo, ReportSecOptions Rep)
        {
            InitializeComponent();
            _RepNo = RNo;
            _RepOpts = Rep;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            if (_RepNo == 1)
            {
                DataSet ds = new DataSet();
                DataSet1.DataTable1DataTable Table1 = new DataSet1.DataTable1DataTable();
                DataSet1.DataTable2DataTable Table2 = new DataSet1.DataTable2DataTable();
                List<TLSEC_UserAccess> Users = null; 
                using (var context = new TTI2Entities())
                {
                    if(_RepOpts.AllRecords)
                      Users = context.TLSEC_UserAccess.OrderBy(x=>x.TLSECUA_UserName).ToList();
                    else if(_RepOpts.ActiveRecords)
                      Users = context.TLSEC_UserAccess.Where(x=>!x.TLSECUA_Discontinued && x.TLSECUA_Pk == 57).OrderBy(x => x.TLSECUA_UserName).ToList();
                    else
                      Users = context.TLSEC_UserAccess.Where(x=>x.TLSECUA_Discontinued).OrderBy(x => x.TLSECUA_UserName).ToList();
                    int Cnt = 0;

                    foreach (var User in Users)
                    {
                        DataSet1.DataTable1Row t1r = Table1.NewDataTable1Row();
                        
                        t1r.Pk = ++Cnt;
                        t1r.UserName = User.TLSECUA_UserName;
                        t1r.SuperUser = User.TLSECUA_SuperUser;
                        t1r.Discontinued = User.TLSECUA_Discontinued;
                        if (User.TLSECUA_Discontinued)
                            t1r.DisDate = (DateTime)User.TLSECUA_DisDate;
                        t1r.ConfirmedPassword = User.TLSECUA_ConfirmedPassword;

                        Table1.AddDataTable1Row(t1r);

                        var UserSections = context.TLSEC_UserSections.Where(x => x.TLSECDEP_User_FK == User.TLSECUA_Pk).ToList();

                        foreach (var UserSection in UserSections)
                        {
                            DataSet1.DataTable2Row t2r = Table2.NewDataTable2Row();
                            t2r.Pk = Cnt;
                            var UserSec = context.TLSEC_Sections.Find(UserSection.TLSECDEP_Section_FK);
                            if(UserSec != null)
                            t2r.UserSection = UserSec.TLSECSect_Description;
                            t2r.Department = context.TLSEC_Departments.Find(UserSection.TLSECDEP_Department_FK).TLSECDT_Description;

                            Table2.AddDataTable2Row(t2r);

                        }

                    }

                    if (Table1.Rows.Count == 0)
                    {
                        DataSet1.DataTable1Row t1r = Table1.NewDataTable1Row();
                        t1r.Pk = 1;
                        t1r.NoRecords = "No Records found for selection made";
                        Table1.AddDataTable1Row(t1r);

                        DataSet1.DataTable2Row t2r = Table2.NewDataTable2Row();
                        t2r.Pk = 1;
                        Table2.AddDataTable2Row(t2r);

                    }
                    ds.Tables.Add(Table1);
                    ds.Tables.Add(Table2);

                    UserAccess UserA = new UserAccess();
                    UserA.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = UserA;
                }
            }
            crystalReportViewer1.Refresh();

        }
    }
}

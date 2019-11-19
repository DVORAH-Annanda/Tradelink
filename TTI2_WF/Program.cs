using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Security.Principal;
using System.Security.Permissions;
using System.Threading;
using Security;
using System.Xml;

namespace TTI2_WF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WindowsIdentity currentIdentity;
            WindowsPrincipal currentPrincipal;

            currentIdentity = WindowsIdentity.GetCurrent();
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            currentPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;

            try
            {
                Application.ThreadException += Application_ThreadException;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmTLMain());
            }
            catch (ObjectDisposedException ex)
            {
                //Write a log
             
            }
          
           
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string message = e.Exception.Message;
            if (e.Exception is System.Security.SecurityException)
            {
                var ex = ((System.Security.SecurityException)e.Exception);
                var xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.LoadXml(ex.PermissionState);
                string rolesName = string.Empty;

                foreach (System.Xml.Linq.XElement item in xmlDocument.GetElementsByTagName("Identity"))
                {
                    if (rolesName.Length != 0)
                        rolesName += ", ";

                    // rolesName += item.Attributes["Role"].Value;
                }

                message = string.Format("\n\nUnauthorised the following Role{0} required - '{1}'\n\n", rolesName.IndexOf(",") >= 0 ? "s are" : " is", rolesName);

            }

            MessageBox.Show(message);
        }
    }
}

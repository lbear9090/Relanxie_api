using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Avigma.Repository.Security;
//using Avigma.Repository.Library;
using Avigma.Repository.Lib;
namespace Avigma.Repository.SystemLic
{
    public class LicCheck
    {
        public bool CheckLic()
        {
            bool chk = false;
            Log log = new Log();
            try
            {
                string strDate = System.Configuration.ConfigurationManager.AppSettings["LicCheck"];
                SecurityHelper securityHelper = new SecurityHelper();
                string strDecDate = securityHelper.Decrypt(strDate, false);
                log.logErrorMessage(strDecDate);
                DateTime dateTime = Convert.ToDateTime(strDecDate);
                if (dateTime >= System.DateTime.Now)
                {
                    chk = true;
                    log.logErrorMessage("License Date Valid");
                }
                else
                {
                    log.logErrorMessage("License Date Expired");
                    log.logErrorMessage("Contact System Admin");
                    chk = false;
                }
            }
            catch (Exception ex)
            {
                chk = false;
                log.logErrorMessage("License Date Error");
                log.logErrorMessage(ex.StackTrace);
              

            }
            
            return chk;

        }
    }
}
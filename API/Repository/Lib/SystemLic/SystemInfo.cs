using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using Avigma.Repository.Lib; 

namespace Avigma.Repository.SystemLic
{
    public class SystemInfo
    {
        bool ret = false;

        Log log = new Log();
        public bool checkMacAddress()
        {


           

            try
            {
                string _macaddress =  System.Configuration.ConfigurationManager.AppSettings["MacAddress"];
                ManagementScope theScope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");

                StringBuilder theQueryBuilder = new StringBuilder();

                theQueryBuilder.Append("SELECT * FROM Win32_NetworkAdapter");

                ObjectQuery theQuery = new ObjectQuery(theQueryBuilder.ToString());

                ManagementObjectSearcher theSearcher = new ManagementObjectSearcher(theScope, theQuery);

                ManagementObjectCollection theCollectionOfResults = theSearcher.Get();

                foreach (ManagementObject theCurrentObject in theCollectionOfResults)
                {

                    if (theCurrentObject["MACAddress"] != null)
                    {

                        string macAdd = theCurrentObject["MACAddress"].ToString();
                     
                        if (macAdd.Replace(':', '-').Equals(_macaddress))
                        {

                            log.logDebugMessage("Mac Address Valid");
                            ret = true;
                            break;
                        }
                       
                    }

                }

                return ret;





            }
            catch (ManagementException e)
            {
                log.logDebugMessage(e.StackTrace);
                Environment.Exit(1);
            }
            catch (System.UnauthorizedAccessException e)
            {
                log.logDebugMessage(e.StackTrace);
                Environment.Exit(1);
            }
            return ret;

        }

        //public string GetSystemName()
        //{
        //    string systemname = System.Windows.Forms.SystemInformation.ComputerName;
        //    return systemname;
        //}
    }
}
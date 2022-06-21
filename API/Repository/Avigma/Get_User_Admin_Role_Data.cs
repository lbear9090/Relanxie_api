using Avigma.Repository.Lib;
using Avigma.Repository.Security;
using Avigma.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace API.Repository.Project
{
    public class Get_User_Admin_Role_Data
    {

        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private DataSet Get_User_Admin_Role(User_Admin_Master_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_User_Admin_Role]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);

                input_parameters.Add("@Type", 1 + "#int#" + model.Type);

                ds = obj.SelectSql(selectProcedure, input_parameters);
            }

            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }



            return ds;
        }

        public List<dynamic> Get_User_Admin_RoleDetails(User_Admin_Master_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_User_Admin_Role(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<User_Admin_Master_DTO> Get_details =
                   (from item in myEnumerableFeaprd
                    select new User_Admin_Master_DTO
                    {
                        Ad_User_PkeyID = item.Field<Int64>("Ad_User_PkeyID"),
                        Ad_User_Type = item.Field<int?>("Ad_User_Type"),
                        
                    }).ToList();

                objDynamic.Add(Get_details);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }
                    }
}
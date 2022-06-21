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
    public class User_Address_Master_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_User_Address_Master(User_Address_Master_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_User_Address_Master]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@UAM_PkeyID", 1 + "#bigint#" + model.UAM_PkeyID);
                input_parameters.Add("@UAM_User_PkeyID", 1 + "#bigint#" + model.UAM_User_PkeyID);
                input_parameters.Add("@UAM_Company_Name", 1 + "#nvarchar#" + model.UAM_Company_Name);
                input_parameters.Add("@UAM_Shipping_Address1", 1 + "#nvarchar#" + model.UAM_Shipping_Address1);
                input_parameters.Add("@UAM_Shipping_Address2", 1 + "#nvarchar#" + model.UAM_Shipping_Address2);
                input_parameters.Add("@UAM_Shipping_Pincode", 1 + "#nvarchar#" + model.UAM_Shipping_Pincode);
                input_parameters.Add("@UAM_Shipping_City", 1 + "#varchar#" + model.UAM_Shipping_City);
                input_parameters.Add("@UAM_Shipping_State", 1 + "#varchar#" + model.UAM_Shipping_State);
                input_parameters.Add("@UAM_Shipping_Country", 1 + "#varchar#" + model.UAM_Shipping_Country);
                input_parameters.Add("@UAM_Lat", 1 + "#nvarchar#" + model.UAM_Lat);
                input_parameters.Add("@UAM_Long", 1 + "#nvarchar#" + model.UAM_Long);
                input_parameters.Add("@UAM_Billing_Address1", 1 + "#nvarchar#" + model.UAM_Billing_Address1);
                input_parameters.Add("@UAM_Billing_Address2", 1 + "#nvarchar#" + model.UAM_Billing_Address2);
                input_parameters.Add("@UAM_Billing_Pincode", 1 + "#nvarchar#" + model.UAM_Billing_Pincode);
                input_parameters.Add("@UAM_Billing_City", 1 + "#varchar#" + model.UAM_Billing_City);
                input_parameters.Add("@UAM_Billing_State", 1 + "#varchar#" + model.UAM_Billing_State);
                input_parameters.Add("@UAM_Billing_Country", 1 + "#varchar#" + model.UAM_Billing_Country);
                input_parameters.Add("@UAM_IsActive", 1 + "#bit#" + model.UAM_IsActive);
                input_parameters.Add("@UAM_IsDelete", 1 + "#bit#" + model.UAM_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@UAM_PkeyID_Out", 2 + "#bigint#" + null);
                input_parameters.Add("@ReturnValue", 2 + "#int#" + null);
                objData = obj.SqlCRUD(insertProcedure, input_parameters);


            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;



        }

        private DataSet Get_User_Address_Master(User_Address_Master_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_User_Address_Master]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@UAM_PkeyID", 1 + "#bigint#" + model.UAM_PkeyID);

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

        public List<dynamic> CreateUpdate_User_Address_Master_DataDetails(User_Address_Master_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_User_Address_Master(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_User_Address_MasterDetails(User_Address_Master_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_User_Address_Master(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<User_Address_Master_DTO> Get_details =
                   (from item in myEnumerableFeaprd
                    select new User_Address_Master_DTO
                    {
                        UAM_PkeyID = item.Field<Int64>("UAM_PkeyID"),
                        UAM_User_PkeyID = item.Field<Int64?>("UAM_User_PkeyID"),
                        UAM_Company_Name = item.Field<String>("UAM_Company_Name"),
                        UAM_Shipping_Address1 = item.Field<String>("UAM_Shipping_Address1"),
                        UAM_Shipping_Address2 = item.Field<String>("UAM_Shipping_Address2"),
                        UAM_Shipping_Pincode = item.Field<String>("UAM_Shipping_Pincode"),
                        UAM_Shipping_City = item.Field<String>("UAM_Shipping_City"),
                        UAM_Shipping_State = item.Field<String>("UAM_Shipping_State"),
                        UAM_Shipping_Country = item.Field<String>("UAM_Shipping_Country"),
                        UAM_Lat = item.Field<String>("UAM_Lat"),
                        UAM_Long = item.Field<String>("UAM_Long"),
                        UAM_Billing_Address1 = item.Field<String>("UAM_Billing_Address1"),
                        UAM_Billing_Address2 = item.Field<String>("UAM_Billing_Address2"),
                        UAM_Billing_Pincode = item.Field<String>("UAM_Billing_Pincode"),
                        UAM_Billing_City = item.Field<String>("UAM_Billing_City"),
                        UAM_Billing_State = item.Field<String>("UAM_Billing_State"),
                        UAM_Billing_Country = item.Field<String>("UAM_Billing_Country"),
                        UAM_IsActive = item.Field<Boolean?>("UAM_IsActive"),
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
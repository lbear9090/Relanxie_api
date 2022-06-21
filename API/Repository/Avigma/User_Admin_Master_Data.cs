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
    public class User_Admin_Master_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_User_Admin_Master(User_Admin_Master_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_User_Admin_Master]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@Ad_User_PkeyID", 1 + "#bigint#" + model.Ad_User_PkeyID);
                input_parameters.Add("@Ad_User_First_Name", 1 + "#varchar#" + model.Ad_User_First_Name);
                input_parameters.Add("@Ad_User_Last_Name", 1 + "#varchar#" + model.Ad_User_Last_Name);
                input_parameters.Add("@Ad_User_Address", 1 + "#varchar#" + model.Ad_User_Address);
                input_parameters.Add("@Ad_User_City", 1 + "#varchar#" + model.Ad_User_City);
                input_parameters.Add("@Ad_User_State", 1 + "#varchar#" + model.Ad_User_State);
                input_parameters.Add("@Ad_User_Mobile", 1 + "#varchar#" + model.Ad_User_Mobile);
                input_parameters.Add("@Ad_User_Login_Name", 1 + "#varchar#" + model.Ad_User_Login_Name);
                input_parameters.Add("@Ad_User_Password", 1 + "#varchar#" + model.Ad_User_Password);
                input_parameters.Add("@Ad_User_UserVal", 1 + "#varchar#" + model.Ad_User_UserVal);
                input_parameters.Add("@Ad_User_Email", 1 + "#varchar#" + model.Ad_User_Email);
                input_parameters.Add("@Ad_User_IsActive", 1 + "#bit#" + model.Ad_User_IsActive);
                input_parameters.Add("@Ad_User_IsDelete", 1 + "#bit#" + model.Ad_User_IsDelete);
                input_parameters.Add("@Ad_User_Type", 1 + "#int#" + model.Ad_User_Type);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@Ad_User_Pkey_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_User_Admin_Master(User_Admin_Master_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_User_Admin_Master]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@Ad_User_PkeyID", 1 + "#bigint#" + model.Ad_User_PkeyID);

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

        public List<dynamic> CreateUpdate_User_Admin_Master_DataDetails(User_Admin_Master_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_User_Admin_Master(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_User_Admin_MasterDetails(User_Admin_Master_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_User_Admin_Master(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<User_Admin_Master_DTO> Get_details =
                   (from item in myEnumerableFeaprd
                    select new User_Admin_Master_DTO
                    {
                        Ad_User_PkeyID = item.Field<Int64>("Ad_User_PkeyID"),
                        Ad_User_First_Name = item.Field<String>("Ad_User_First_Name"),
                        Ad_User_Last_Name = item.Field<String>("Ad_User_Last_Name"),
                        Ad_User_Address = item.Field<String>("Ad_User_Address"),
                        Ad_User_City = item.Field<String>("Ad_User_City"),
                        Ad_User_State = item.Field<String>("Ad_User_State"),
                        Ad_User_Mobile = item.Field<String>("Ad_User_Mobile"),
                        Ad_User_Login_Name = item.Field<String>("Ad_User_Login_Name"),
                        Ad_User_Password = item.Field<String>("Ad_User_Password"),
                        Ad_User_UserVal = item.Field<String>("Ad_User_UserVal"),
                        Ad_User_Email = item.Field<String>("Ad_User_Email"),
                        Ad_User_Type = item.Field<int?>("Ad_User_Type"),
                        Ad_User_IsActive = item.Field<Boolean?>("Ad_User_IsActive"),

                    }).ToList();

                objDynamic.Add(Get_details);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }

            return objDynamic;





        }

    }
}
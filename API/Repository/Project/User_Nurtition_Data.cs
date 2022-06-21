using Avigma.Repository.Lib;
using Avigma.Repository.Security;
using API.Models.Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace API.Repository.Project
{
    public class User_Nurtition_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_User_Nutrition(User_Nurtition_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_User_Nutrition]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@NU_PKeyID", 1 + "#bigint#" + model.NU_PKeyID);
                input_parameters.Add("@NU_Name", 1 + "#varchar#" + model.NU_Name);
                input_parameters.Add("@NU_Description", 1 + "#nvarchar#" + model.NU_Description);
                input_parameters.Add("@NU_Type", 1 + "#nvarchar#" + model.NU_Type);
                input_parameters.Add("@NU_IsActive", 1 + "#bit#" + model.NU_IsActive);
                input_parameters.Add("@NU_IsDelete", 1 + "#bit#" + model.NU_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@NU_PkeyID_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_User_Nutrition(User_Nurtition_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_User_Nutrition]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@NU_PKeyID", 1 + "#bigint#" + model.NU_PKeyID);

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

        public List<dynamic> CreateUpdate_User_Nutrition_DataDetails(User_Nurtition_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_User_Nutrition(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_User_NutritionDetails(User_Nurtition_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_User_Nutrition(model);

                if (ds.Tables.Count > 0)
                {
                    objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[0]));
                }
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
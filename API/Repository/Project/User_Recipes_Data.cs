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
    public class User_Recipes_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_User_Recipes(User_Recipes_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_User_Recipes]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@UR_PKeyID", 1 + "#bigint#" + model.UR_PKeyID);
                input_parameters.Add("@UR_Name", 1 + "#varchar#" + model.UR_Name);
                input_parameters.Add("@UR_Description", 1 + "#nvarchar#" + model.UR_Description);
                input_parameters.Add("@UR_filePath", 1 + "#nvarchar#" + model.UR_filePath);
                input_parameters.Add("@UR_fileName", 1 + "#varchar#" + model.UR_fileName);
                input_parameters.Add("@UR_Types", 1 + "#int#" + model.UR_Types);
                input_parameters.Add("@UR_Desc_Show", 1 + "#bit#" + model.UR_Desc_Show);
                
                input_parameters.Add("@UR_IsActive", 1 + "#bit#" + model.UR_IsActive);
                input_parameters.Add("@UR_IsDelete", 1 + "#bit#" + model.UR_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@UR_PkeyID_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_User_Recipes(User_Recipes_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_User_Recipes]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@UR_PKeyID", 1 + "#bigint#" + model.UR_PKeyID);

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

        public List<dynamic> CreateUpdate_User_Recipes_DataDetails(User_Recipes_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_User_Recipes(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }


        public List<dynamic> Get_User_RecipesDetails(User_Recipes_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_User_Recipes(model);

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
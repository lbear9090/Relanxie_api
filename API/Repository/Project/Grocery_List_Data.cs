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
    public class Grocery_List_Data
    {

        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_Grocery_List(Grocery_List_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_Grocery_List]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@GR_PKeyID", 1 + "#bigint#" + model.GR_PKeyID);
                input_parameters.Add("@GR_Name", 1 + "#varchar#" + model.GR_Name);
                input_parameters.Add("@GR_Description", 1 + "#nvarchar#" + model.GR_Description);
                input_parameters.Add("@GR_Type", 1 + "#nvarchar#" + model.GR_Type);
                input_parameters.Add("@GR_IsActive", 1 + "#bit#" + model.GR_IsActive);
                input_parameters.Add("@GR_IsDelete", 1 + "#bit#" + model.GR_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@GR_PkeyID_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_Grocery_List(Grocery_List_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_Grocery_List]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@GR_PKeyID", 1 + "#bigint#" + model.GR_PKeyID);

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

        public List<dynamic> CreateUpdate_Grocery_List_DataDetails(Grocery_List_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_Grocery_List(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_Grocery_ListDetails(Grocery_List_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_Grocery_List(model);

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
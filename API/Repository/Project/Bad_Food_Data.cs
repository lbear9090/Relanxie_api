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
    public class Bad_Food_Data
    {

        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_Bad_Food(Bad_Food_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_Bad_Food]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@BF_PKeyID", 1 + "#bigint#" + model.BF_PKeyID);
                input_parameters.Add("@BF_Name", 1 + "#varchar#" + model.BF_Name);
                input_parameters.Add("@BF_Description", 1 + "#nvarchar#" + model.BF_Description);
                input_parameters.Add("@BF_Type", 1 + "#nvarchar#" + model.BF_Type);
                input_parameters.Add("@BF_IsActive", 1 + "#bit#" + model.BF_IsActive);
                input_parameters.Add("@BF_IsDelete", 1 + "#bit#" + model.BF_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@BF_PkeyID_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_Bad_Food(Bad_Food_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_Bad_Food]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@BF_PKeyID", 1 + "#bigint#" + model.BF_PKeyID);

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

        public List<dynamic> CreateUpdate_Bad_Food_DataDetails(Bad_Food_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_Bad_Food(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_Bad_FoodDetails(Bad_Food_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_Bad_Food(model);

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
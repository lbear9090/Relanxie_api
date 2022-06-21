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
    public class Brain_Food_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_Brain_Food(Brain_Food_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_Brain_Food]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@BRF_PKeyID", 1 + "#bigint#" + model.BRF_PKeyID);
                input_parameters.Add("@BRF_Name", 1 + "#varchar#" + model.BRF_Name);
                input_parameters.Add("@BRF_Description", 1 + "#nvarchar#" + model.BRF_Description);
                input_parameters.Add("@BRF_Type", 1 + "#nvarchar#" + model.BRF_Type);
                input_parameters.Add("@BRF_IsActive", 1 + "#bit#" + model.BRF_IsActive);
                input_parameters.Add("@BRF_IsDelete", 1 + "#bit#" + model.BRF_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@BRF_PkeyID_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_Brain_Food(Brain_Food_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_Brain_Food]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@BRF_PKeyID", 1 + "#bigint#" + model.BRF_PKeyID);

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

        public List<dynamic> CreateUpdate_Brain_Food_DataDetails(Brain_Food_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_Brain_Food(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_Brain_FoodDetails(Brain_Food_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_Brain_Food(model);

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
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
    public class Online_Therapy_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_Online_Therapy(Online__Therapy_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_Online_Therapy]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@OT_PKeyID ", 1 + "#bigint#" + model.OT_PKeyID);
                input_parameters.Add("@OT_Name", 1 + "#varchar#" + model.OT_Name);
                input_parameters.Add("@OT_Description", 1 + "#nvarchar#" + model.OT_Description);
                input_parameters.Add("@OT_Type", 1 + "#nvarchar#" + model.OT_Type);
                input_parameters.Add("@OT_IsActive", 1 + "#bit#" + model.OT_IsActive);
                input_parameters.Add("@OT_IsDelete", 1 + "#bit#" + model.OT_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@OT_PkeyID_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_Online_Therapy(Online__Therapy_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_Online_Therapy]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@OT_PKeyID", 1 + "#bigint#" + model.OT_PKeyID);

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

        public List<dynamic> CreateUpdate_Online_Therapy_DataDetails(Online__Therapy_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_Online_Therapy(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_Online_TherapyDetails(Online__Therapy_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_Online_Therapy(model);

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
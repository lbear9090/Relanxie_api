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
    public class User_Yoga_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_User_Yoga(User_Yoga_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_User_Yoga]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@YG_PKeyID", 1 + "#bigint#" + model.YG_PKeyID);
                input_parameters.Add("@YG_Name", 1 + "#varchar#" + model.YG_Name);
                input_parameters.Add("@YG_Description", 1 + "#nvarchar#" + model.YG_Description);
                input_parameters.Add("@YG_Type", 1 + "#nvarchar#" + model.YG_Type);
                input_parameters.Add("@YG_File_Name", 1 + "#nvarchar#" + model.YG_File_Name);
                input_parameters.Add("@YG_File_Path", 1 + "#nvarchar#" + model.YG_File_Path);
                input_parameters.Add("@YG_File_Type", 1 + "#nvarchar#" + model.YG_File_Type);
                input_parameters.Add("@YG_Timer", 1 + "#int#" + model.YG_Timer);
                input_parameters.Add("@YG_Size", 1 + "#decimal#" + model.YG_Size);
                input_parameters.Add("@YG_ThumbNail_Path", 1 + "#nvarchar#" + model.YG_ThumbNail_Path);
                input_parameters.Add("@YG_IsActive", 1 + "#bit#" + model.YG_IsActive);
                input_parameters.Add("@YG_IsDelete", 1 + "#bit#" + model.YG_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@YG_PkeyID_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_User_Yoga(User_Yoga_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_User_Yoga]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@YG_PKeyID", 1 + "#bigint#" + model.YG_PKeyID);

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

        public List<dynamic> CreateUpdate_User_Yoga_DataDetails(User_Yoga_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_User_Yoga(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }


        public List<dynamic> Get_User_YogaDetails(User_Yoga_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_User_Yoga(model);

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
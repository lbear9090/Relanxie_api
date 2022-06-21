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
    public class User_Medidation_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_User_Medidation(User_Medidation_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_User_Medidation]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@MD_PKeyID", 1 + "#bigint#" + model.MD_PKeyID);
                input_parameters.Add("@MD_Name", 1 + "#varchar#" + model.MD_Name);
                input_parameters.Add("@MD_Description", 1 + "#nvarchar#" + model.MD_Description);
                input_parameters.Add("@MD_Type", 1 + "#nvarchar#" + model.MD_Type);
                input_parameters.Add("@MD_File_Name", 1 + "#nvarchar#" + model.MD_File_Name);
                input_parameters.Add("@MD_File_Path", 1 + "#nvarchar#" + model.MD_File_Path);
                input_parameters.Add("@MD_File_Type", 1 + "#nvarchar#" + model.MD_File_Type);
                input_parameters.Add("@MD_Size", 1 + "#decimal#" + model.MD_Size);
                input_parameters.Add("@MD_ThumbNail_Path", 1 + "#nvarchar#" + model.MD_ThumbNail_Path);
                input_parameters.Add("@MD_File_Name_2", 1 + "#nvarchar#" + model.MD_File_Name_2);
                input_parameters.Add("@MD_File_Path_2", 1 + "#nvarchar#" + model.MD_File_Path_2);
                input_parameters.Add("@MD_File_Type_2", 1 + "#nvarchar#" + model.MD_File_Type_2);
                input_parameters.Add("@MD_Size_2", 1 + "#decimal#" + model.MD_Size_2);
                input_parameters.Add("@MD_ThumbNail_Path_2", 1 + "#nvarchar#" + model.MD_ThumbNail_Path_2);
                input_parameters.Add("@MD_IsActive", 1 + "#bit#" + model.MD_IsActive);
                input_parameters.Add("@MD_IsDelete", 1 + "#bit#" + model.MD_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@MD_Timer", 1 + "#int#" + model.MD_Timer);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@MD_PkeyID_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_User_Medidation(User_Medidation_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_User_Medidation]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@MD_PKeyID", 1 + "#bigint#" + model.MD_PKeyID);

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

        public List<dynamic> CreateUpdate_User_Medidation_DataDetails(User_Medidation_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_User_Medidation(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_User_MedidationDetails(User_Medidation_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_User_Medidation(model);

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
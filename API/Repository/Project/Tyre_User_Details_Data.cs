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
    public class Tyre_User_Details_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_Tyre_User_Details(Tyre_User_Details_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_Tyre_User_Details]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@TUD_PKeyID", 1 + "#bigint#" + model.TUD_PKeyID);
                input_parameters.Add("@TUD_TUM_PKeyID", 1 + "#bigint#" + model.TUD_TUM_PKeyID);
                input_parameters.Add("@TUD_Tyre_PKeyID", 1 + "#bigint#" + model.TUD_Tyre_PKeyID);
                input_parameters.Add("@TUD_Brand", 1 + "#int#" + model.TUD_Brand);
                input_parameters.Add("@TUD_Size", 1 + "#nvarchar#" + model.TUD_Size);
                input_parameters.Add("@TUD_Installation_Date", 1 + "#datetime#" + model.TUD_Installation_Date);
                input_parameters.Add("@TUD_Mileage", 1 + "#int#" + model.TUD_Mileage);
                input_parameters.Add("@TUD_User_PkeyID", 1 + "#bigint#" + model.TUD_User_PkeyID);
                input_parameters.Add("@TUD_TUMM_PKeyID", 1 + "#bigint#" + model.TUD_TUMM_PKeyID);
                input_parameters.Add("@TUD_TOC_PkeyID", 1 + "#bigint#" + model.TUD_TOC_PkeyID);
                input_parameters.Add("@TUD_Status", 1 + "#int#" + model.TUD_Status);
                input_parameters.Add("@TUD_Percent", 1 + "#int#" + model.TUD_Percent);
                input_parameters.Add("@TUD_Position", 1 + "#int#" + model.TUD_Position);
                input_parameters.Add("@TUD_IsActive", 1 + "#bit#" + model.TUD_IsActive);
                input_parameters.Add("@TUD_IsDelete", 1 + "#bit#" + model.TUD_IsDelete);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@TUD_PkeyID_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_Tyre_User_Details(Tyre_User_Details_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_Tyre_User_Details]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@TUD_PKeyID", 1 + "#bigint#" + model.TUD_PKeyID);
                input_parameters.Add("@TUD_TUM_PKeyID", 1 + "#bigint#" + model.TUD_TUM_PKeyID);
                input_parameters.Add("@TUD_Position", 1 + "#bigint#" + model.TUD_Position);
                input_parameters.Add("@TUD_User_PkeyID", 1 + "#bigint#" + model.TUD_User_PkeyID);
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

        public List<dynamic> CreateUpdate_Tyre_User_Details_DataDetails(Tyre_User_Details_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_Tyre_User_Details(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }


        public List<dynamic> Get_Tyre_User_DetailsDetails(Tyre_User_Details_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {
                if (model.Type == 4)
                {
                    model.TUD_User_PkeyID = model.UserID;
                }

                DataSet ds = Get_Tyre_User_Details(model);
                if (model.Type == 4)
                {
                    objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[0]));
                }
                else
                {
                    var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                    List<Tyre_User_Details_DTO> Get_details =
                       (from item in myEnumerableFeaprd
                        select new Tyre_User_Details_DTO
                        {
                            TUD_PKeyID = item.Field<Int64>("TUD_PKeyID"),
                            TUD_TUM_PKeyID = item.Field<Int64?>("TUD_TUM_PKeyID"),
                            TUD_Tyre_PKeyID = item.Field<Int64?>("TUD_Tyre_PKeyID"),
                            TUD_Brand = item.Field<int?>("TUD_Brand"),
                            TUD_Size = item.Field<String>("TUD_Size"),
                            TUD_Installation_Date = item.Field<DateTime?>("TUD_Installation_Date"),
                            TUD_Mileage = item.Field<int?>("TUD_Mileage"),
                            TUD_User_PkeyID = item.Field<Int64?>("TUD_User_PkeyID"),
                            TUD_TUMM_PKeyID = item.Field<Int64?>("TUD_TUMM_PKeyID"),
                            TUD_Status = item.Field<int?>("TUD_Status"),
                            TUD_Percent = item.Field<int?>("TUD_Percent"),
                            TUD_Position = item.Field<int?>("TUD_Percent"),
                            TUD_IsActive = item.Field<Boolean?>("TUD_IsActive"),
                        }).ToList();

                    objDynamic.Add(Get_details);
                }
                
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }


        private DataSet Get_UserTyreData(Tyre_User_Details_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_UserTyreData]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();


                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@TUD_TUM_PKeyID", 1 + "#bigint#" + model.TUD_TUM_PKeyID);
                
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

        public List<dynamic> Get_UserTyreDataDetails(Tyre_User_Details_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {
                DataSet ds = Get_UserTyreData(model);
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    objDynamic.Add(obj.AsDynamicEnumerable(ds.Tables[i]));
                }
                
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
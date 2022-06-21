using Avigma.Repository.Lib;
using Avigma.Repository.Security;
using Avigma.Models;
using API.Models.Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace API.Repository.Project
{
    public class Menu_Master_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_Menu_Master(Menu_Master_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_Menu_Master]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@MU_PkeyID", 1 + "#bigint#" + model.MU_PkeyID);
                input_parameters.Add("@MU_Name", 1 + "#varchar#" + model.MU_Name);
                input_parameters.Add("@MU_Description", 1 + "#nvarchar#" + model.MU_Description);
                input_parameters.Add("@MU_IsActive", 1 + "#bit#" + model.MU_IsActive);
                input_parameters.Add("@MU_IsDelete", 1 + "#bit#" + model.MU_IsDelete);

                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@MU_Pkey_Out", 2 + "#bigint#" + null);
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

        private DataSet Get_Menu_Master(Menu_Master_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_Menu_Master]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@MU_PkeyID", 1 + "#bigint#" + model.MU_PkeyID);

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
        public List<dynamic> CreateUpdate_Menu_Master_DataDetails(Menu_Master_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_Menu_Master(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }

        public List<dynamic> Get_Menu_MasterDetails(Menu_Master_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_Menu_Master(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<Menu_Master_DTO> Get_details =
                   (from item in myEnumerableFeaprd
                    select new Menu_Master_DTO
                    {
                        MU_PkeyID = item.Field<Int64>("MU_PkeyID"),
                        MU_Name = item.Field<String>("MU_Name"),
                        MU_Description = item.Field<String>("MU_Description"),
                        MU_IsActive = item.Field<Boolean?>("MU_IsActive"),

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
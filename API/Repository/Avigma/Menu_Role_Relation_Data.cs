using Avigma.Repository.Lib;
using Avigma.Repository.Security;
using Avigma.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace API.Repository.Project
{
    public class Menu_Role_Relation_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> CreateUpdate_Menu_Role_Relation(Menu_Role_Relation_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_Menu_Role_Relation]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@MUR_PkeyID", 1 + "#bigint#" + model.MUR_PkeyID);
                input_parameters.Add("@MUR_MenuID", 1 + "#bigint#" + model.MUR_MenuID);
                input_parameters.Add("@MUR_Role", 1 + "#bigint#" + model.MUR_Role);
                input_parameters.Add("@MUR_IsActive", 1 + "#bit#" + model.MUR_IsActive);
                input_parameters.Add("@MUR_IsDelete", 1 + "#bit#" + model.MUR_IsDelete);

                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@MUR_Pkey_Out", 2 + "#bigint#" + null);
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
        private DataSet Get_Menu_Role_Relation(Menu_Role_Relation_DTO model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_Menu_Role_Relation]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@MUR_PkeyID", 1 + "#bigint#" + model.MUR_PkeyID);
                input_parameters.Add("@MUR_Role", 1 + "#bigint#" + model.MUR_Role);
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
        public List<dynamic> CreateUpdate_Menu_Role_Relation_DataDetails(Menu_Role_Relation_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = CreateUpdate_Menu_Role_Relation(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
            }
            return objData;
        }
        public List<dynamic> Get_Menu_Role_RelationDetails(Menu_Role_Relation_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                DataSet ds = Get_Menu_Role_Relation(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<Menu_Role_Relation_DTO> Get_details =
                   (from item in myEnumerableFeaprd
                    select new Menu_Role_Relation_DTO
                    {
                        MUR_PkeyID = item.Field<Int64>("MUR_PkeyID"),
                        MUR_MenuID = item.Field<Int64?>("MUR_MenuID"),
                        MUR_Role = item.Field<Int64?>("MUR_Role"),
                        MUR_IsActive = item.Field<Boolean?>("MUR_IsActive"),

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
using Avigma.Repository.Lib;
using Avigma.Repository.Security;
using API.Models.Project;
using Avigma.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace API.Repository.Project
{
    public class UserMaster_Data
    {
        MyDataSourceFactory obj = new MyDataSourceFactory();
        Log log = new Log();
        SecurityHelper securityHelper = new SecurityHelper();

        private List<dynamic> AddUpdateUserMaster_Data(UserMaster_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_UserMaster]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@User_PkeyID", 1 + "#bigint#" + model.User_PkeyID);
                input_parameters.Add("@User_Name", 1 + "#varchar#" + model.User_Name);
                input_parameters.Add("@User_Email", 1 + "#nvarchar#" + model.User_Email);
                input_parameters.Add("@User_Password", 1 + "#nvarchar#" + model.User_Password);
                input_parameters.Add("@User_Phone", 1 + "#nvarchar#" + model.User_Phone);
                input_parameters.Add("@User_Address", 1 + "#nvarchar#" + model.User_Address);
                input_parameters.Add("@User_City", 1 + "#varchar#" + model.User_City);
                input_parameters.Add("@User_Country", 1 + "#varchar#" + model.User_Country);
                input_parameters.Add("@User_Zip", 1 + "#nvarchar#" + model.User_Zip);
                input_parameters.Add("@User_DOB", 1 + "#datetime#" + model.User_DOB);
                input_parameters.Add("@User_Gender", 1 + "#int#" + model.User_Gender);
                input_parameters.Add("@User_Type", 1 + "#int#" + model.User_Type);
                input_parameters.Add("@User_Image_Path", 1 + "#varchar#" + model.User_Image_Path);
                input_parameters.Add("@User_MacID", 1 + "#varchar#" + model.User_MacID);
                input_parameters.Add("@User_IsVerified", 1 + "#bit#" + model.User_IsVerified);
                input_parameters.Add("@User_IsActive", 1 + "#bit#" + model.User_IsActive);
                input_parameters.Add("@User_IsDelete", 1 + "#bit#" + model.User_IsDelete);
                input_parameters.Add("@User_latitude", 1 + "#varchar#" + model.User_latitude);
                input_parameters.Add("@User_longitude", 1 + "#varchar#" + model.User_longitude);
                input_parameters.Add("@User_PkeyID_Master", 1 + "#bigint#" + model.User_PkeyID_Master);
                input_parameters.Add("@User_IsActive_Prof", 1 + "#bit#" + model.User_IsActive_Prof);
                input_parameters.Add("@User_Token_val", 1 + "#varchar#" + model.User_Token_val);
                input_parameters.Add("@User_Login_Type", 1 + "#int#" + model.User_Login_Type);
                input_parameters.Add("@User_Image_Base", 1 + "#varchar#" + model.User_Image_Base);
                input_parameters.Add("@User_LastName", 1 + "#varchar#" + model.User_LastName);
                input_parameters.Add("@User_Company", 1 + "#varchar#" + model.User_Company);
                input_parameters.Add("@User_Stripe_CustID", 1 + "#nvarchar#" + model.User_Stripe_CustID);
                input_parameters.Add("@User_Stripe_SubsID", 1 + "#nvarchar#" + model.User_Stripe_SubsID);
                input_parameters.Add("@User_Stripe_AttachID", 1 + "#nvarchar#" + model.User_Stripe_AttachID);
                input_parameters.Add("@User_Stripe_PaymentID", 1 + "#nvarchar#" + model.User_Stripe_PaymentID);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@User_PkeyID_Out", 2 + "#bigint#" + null);
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
        public List<dynamic> AddUserMaster_Data(UserMaster_DTO model)
        {
            ImageGenerator imageGenerator = new ImageGenerator();
            string imgPath = string.Empty;
            List<dynamic> objData = new List<dynamic>();
            try
            {
                if (model.Type == 6)
                {
                    if (!String.IsNullOrEmpty(model.User_Image_Base))
                    {
                        imgPath = imageGenerator.Base64ToImage(model.User_Image_Base);
                        model.User_Image_Path = imgPath;
                        model.User_Image_Base = string.Empty;
                    }
                }
                objData.Add(AddUpdateUserMaster_Data(model));
                if (model.Type == 6)
                {
                    objData.Add(imgPath);

                }


            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }

        public List<dynamic> ChangePassword(UserMaster_ChangePassword model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                Int64 User_PkeyID = 0;
                if (!string.IsNullOrEmpty(model.User_PkeyID))
                {
                    User_PkeyID = Convert.ToInt64(securityHelper.Decode(model.User_PkeyID));
                }
                UserMaster_DTO userMaster_DTO = new UserMaster_DTO();
                userMaster_DTO.Type = model.Type;
                userMaster_DTO.User_PkeyID = User_PkeyID;
                userMaster_DTO.User_Password = model.User_Password;

                   objData = AddUpdateUserMaster_Data(userMaster_DTO);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }

        public List<dynamic> ChangePasswordByEmail(UserMaster_ChangePassword model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {



                switch (model.User_Type)
                {
                    case 1:
                        {
                            UserMaster_DTO userMaster_DTO = new UserMaster_DTO();
                            userMaster_DTO.Type = model.Type;
                            userMaster_DTO.User_Email = model.User_Email;
                            userMaster_DTO.User_Password = model.User_Password;
                            objData = AddUpdateUserMaster_Data(userMaster_DTO);
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }



        public List<dynamic> VerifyUserByEmail(UserMaster_DTO userMaster_DTO)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                   userMaster_DTO.Type = 10;
                   objData = AddUpdateUserMaster_Data(userMaster_DTO);

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }

        private DataSet Get_UserMaster(UserMaster_DTO model)
        {
            DataSet ds = null;
            try
            {

                string selectProcedure = "[Get_UserMaster]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@User_PkeyID", 1 + "#bigint#" + model.User_PkeyID);
                input_parameters.Add("@User_PkeyID_Master", 1 + "#bigint#" + model.User_PkeyID_Master);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);

                ds = obj.SelectSql(selectProcedure, input_parameters);
            }

            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }



            return ds;
        }


        public List<UserMaster_DTO> Get_UserMasterDetailsDTO(UserMaster_DTO model)
        {
            List<UserMaster_DTO> UserMaster = new List<UserMaster_DTO>();
            try
            {


                DataSet ds = Get_UserMaster(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                  UserMaster =
                   (from item in myEnumerableFeaprd
                    select new UserMaster_DTO
                    {
                        User_PkeyID = item.Field<Int64>("User_PkeyID"),
                        User_PkeyID_Master = item.Field<Int64?>("User_PkeyID_Master"),
                        User_Name = item.Field<String>("User_Name"),
                        User_Email = item.Field<String>("User_Email"),
                        User_Password = item.Field<String>("User_Password"),
                        User_Phone = item.Field<String>("User_Phone"),
                        User_Address = item.Field<String>("User_Address"),
                        User_City = item.Field<String>("User_City"),
                        User_Country = item.Field<String>("User_Country"),
                        User_Zip = item.Field<String>("User_Zip"),
                        User_DOB = item.Field<DateTime?>("User_DOB"),
                        User_Gender = item.Field<int?>("User_Gender"),
                        User_Type = item.Field<int?>("User_Type"),
                        User_Image_Path = item.Field<String>("User_Image_Path"),
                        User_MacID = item.Field<String>("User_MacID"),
                        User_IsVerified = item.Field<Boolean?>("User_IsVerified"),
                        User_IsActive = item.Field<Boolean?>("User_IsActive"),
                        User_latitude = item.Field<String>("User_latitude"),
                        User_longitude = item.Field<String>("User_longitude"),
                        User_Token_val = item.Field<String>("User_Token_val"),
                        User_Login_Type = item.Field<int?>("User_Login_Type"),
                        User_Image_Base = item.Field<String>("User_Image_Base"),
                        User_IsActive_Prof = item.Field<Boolean?>("User_IsActive_Prof"),
                        User_Company = item.Field<String>("User_Company"),
                        User_LastName = item.Field<String>("User_LastName"),
                        User_Ispaid = item.Field<bool?>("User_Ispaid")


                    }).ToList();

                
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return UserMaster;
        }


        public List<dynamic> Get_UserMasterDetails(UserMaster_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {


                DataSet ds = Get_UserMaster(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<UserMaster_DTO> UserMaster =
                   (from item in myEnumerableFeaprd
                    select new UserMaster_DTO
                    {
                        User_PkeyID = item.Field<Int64>("User_PkeyID"),
                        User_PkeyID_Master = item.Field<Int64?>("User_PkeyID_Master"),
                        User_Name = item.Field<String>("User_Name"),
                        User_Email = item.Field<String>("User_Email"),
                        User_Password = item.Field<String>("User_Password"),
                        User_Phone = item.Field<String>("User_Phone"),
                        User_Address = item.Field<String>("User_Address"),
                        User_City = item.Field<String>("User_City"),
                        User_Country = item.Field<String>("User_Country"),
                        User_Zip = item.Field<String>("User_Zip"),
                        User_DOB = item.Field<DateTime?>("User_DOB"),
                        User_Gender = item.Field<int?>("User_Gender"),
                        User_Type = item.Field<int?>("User_Type"),
                        User_Image_Path = item.Field<String>("User_Image_Path"),
                        User_MacID = item.Field<String>("User_MacID"),
                        User_IsVerified = item.Field<Boolean?>("User_IsVerified"),
                        User_IsActive = item.Field<Boolean?>("User_IsActive"),
                        User_latitude = item.Field<String>("User_latitude"),
                        User_longitude = item.Field<String>("User_longitude"),
                        User_Token_val = item.Field<String>("User_Token_val"),
                        User_Login_Type = item.Field<int?>("User_Login_Type"),
                        User_Image_Base = item.Field<String>("User_Image_Base"),
                        User_IsActive_Prof =  item.Field<Boolean?>("User_IsActive_Prof"),
                        User_Company = item.Field<String>("User_Company"),
                        User_LastName = item.Field<String>("User_LastName"),



                    }).ToList();

                objDynamic.Add(UserMaster);
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
using System;
using System.Collections.Generic;
using System.Linq;
using Avigma.Models;
using System.Data;
using Avigma.Repository.Lib;
using Avigma.Repository.Security;
using API.Repository.Project;
using API.Models.Project;
namespace Avigma.Repository.Lib
{
    public class GetSetUser
    {
        Log log = new Log();
        SecurityHelper SecurityHelper = new SecurityHelper();
        MyDataSourceFactory obj = new MyDataSourceFactory();



        private DataSet Get_UserMasterLogin(RootUserLogin model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_UserMasterLogin]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();
                List<dynamic> objdynamic = new List<dynamic>();
                List<dynamic> objdynamicret = new List<dynamic>();

                input_parameters.Add("@User_Email", 1 + "#nvarchar#" + model.Um_Email);
                input_parameters.Add("@User_Password", 1 + "#nvarchar#" + model.Um_Password);
                input_parameters.Add("@User_Name", 1 + "#nvarchar#" + model.Um_Name);
                input_parameters.Add("@User_Phone", 1 + "#nvarchar#" + model.MobileNumber);
                input_parameters.Add("@User_MacID", 1 + "#nvarchar#" + model.User_MacID);
                input_parameters.Add("@User_OTP", 1 + "#int#" + model.OTP);
                input_parameters.Add("@User_latitude", 1 + "#nvarchar#" + model.User_latitude);
                input_parameters.Add("@User_longitude", 1 + "#nvarchar#" + model.User_longitude);
                input_parameters.Add("@User_Login_Type", 1 + "#int#" + model.User_Login_Type);
                input_parameters.Add("@User_Token_val", 1 + "#nvarchar#" + model.User_Token_val);
                input_parameters.Add("@User_LastName", 1 + "#nvarchar#" + model.User_LastName);
                input_parameters.Add("@User_Company", 1 + "#nvarchar#" + model.User_Company);
                input_parameters.Add("@User_FB_GM_Token_val", 1 + "#nvarchar#" + model.User_FB_GM_Token_val);
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

        private DataSet Get_UserMasterAdminLogin(RootUserLogin model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[Get_UserMasterAdminLogin]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();
                List<dynamic> objdynamic = new List<dynamic>();
                List<dynamic> objdynamicret = new List<dynamic>();

                input_parameters.Add("@User_Email", 1 + "#nvarchar#" + model.Um_Email);
                input_parameters.Add("@User_Password", 1 + "#nvarchar#" + model.Um_Password);
                input_parameters.Add("@User_Name", 1 + "#nvarchar#" + model.Um_Name);
                input_parameters.Add("@User_Phone", 1 + "#nvarchar#" + model.MobileNumber);
                input_parameters.Add("@User_MacID", 1 + "#nvarchar#" + model.User_MacID);
                input_parameters.Add("@User_OTP", 1 + "#int#" + model.OTP);
                input_parameters.Add("@User_latitude", 1 + "#nvarchar#" + model.User_latitude);
                input_parameters.Add("@User_longitude", 1 + "#nvarchar#" + model.User_longitude);
                input_parameters.Add("@User_Login_Type", 1 + "#int#" + model.User_Login_Type);
                input_parameters.Add("@User_Token_val", 1 + "#nvarchar#" + model.User_Token_val);
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



        private DataSet Get_ForGetPassword(UserLogin model)
        {
            DataSet ds = null;
            try
            {
                string selectProcedure = "[GetForGotPassword]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();
                

                input_parameters.Add("@Um_Email", 1 + "#nvarchar#" + model.EmailID);
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

        public List<dynamic> GetVerificationLink(UserLogin userLogin)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {
                UserVerificationMaster_Data userVerificationMaster_Data = new UserVerificationMaster_Data();
                UserUserVerificationMaster_Details userUserVerificationMaster_Details = new UserUserVerificationMaster_Details();
                UserMaster_Data userMaster_Data = new UserMaster_Data();
                UserMaster_DTO userMaster_DTO = new UserMaster_DTO();
                List<UserMaster_DTO> userMaster_DTOs = new List<UserMaster_DTO>();
                userMaster_DTO.User_PkeyID = userLogin.User_PkeyID;
                userMaster_DTO.Type = 2;
                userMaster_DTOs = userMaster_Data.Get_UserMasterDetailsDTO(userMaster_DTO);
                for (int i = 0; i < userMaster_DTOs.Count; i++)
                {
                    userUserVerificationMaster_Details.User_FirstName = userMaster_DTOs[i].User_Name;
                    userUserVerificationMaster_Details.Email_Url = userLogin.Email_Url;
                    userUserVerificationMaster_Details.Device = userLogin.Device;
                    userUserVerificationMaster_Details.User_Email = userMaster_DTOs[i].User_Email;
                    userVerificationMaster_Data.GenerateLink(userUserVerificationMaster_Details, 1);

                }
                userLogin.UserCode = "Sucesss";
                userLogin.ErrorCode = "1";
                objDynamic.Add(userLogin);

            }
            catch (Exception ex)
            {
                userLogin.UserCode = "Error";
                userLogin.ErrorCode = "0";
                objDynamic.Add(userLogin);

                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);

            }
            return objDynamic;

        }

            public List<dynamic> GetForGetPassword(UserLogin userLogin)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {

                userLogin.Type = 1;
                DataSet ds = Get_ForGetPassword(userLogin);
                string User_PkeyID = string.Empty, User_Name = string.Empty;
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count>0)
                    {
                        //string strpassword = ds.Tables[0].Rows[0]["Um_Password"].ToString();
                        //string strpassword = ds.Tables[0].Rows[0]["Um_Password"].ToString();
                        //EmailTemplate emailTemplate = new EmailTemplate();
                        //userLogin.Password = strpassword;
                        //emailTemplate.SendRequestPassword(userLogin);
                        //userLogin = new UserLogin();
                        //userLogin.ErrorCode = "1";
                        //objDynamic.Add(userLogin);
                        UserVerificationMaster_Data userVerificationMaster_Data = new UserVerificationMaster_Data();
                        UserUserVerificationMaster_Details userUserVerificationMaster_Details = new UserUserVerificationMaster_Details();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                              User_PkeyID =  ds.Tables[0].Rows[i]["User_PkeyID"].ToString();
                              User_Name =   ds.Tables[0].Rows[i]["User_Name"].ToString();
                            userUserVerificationMaster_Details.User_Email = userLogin.EmailID;
                            userUserVerificationMaster_Details.User_FirstName = User_Name;
                            userUserVerificationMaster_Details.User_pkeyID = Convert.ToInt64(User_PkeyID);
                            userUserVerificationMaster_Details.Device = userLogin.Device;
                            userUserVerificationMaster_Details.Email_Url = userLogin.Email_Url;
                            userVerificationMaster_Data.GeneratePasswordLink(userUserVerificationMaster_Details, 2);

                        }
                        userLogin.UserCode = "Sucesss";
                        userLogin.ErrorCode = "1";
                        objDynamic.Add(userLogin);

                    }
                    else
                    {
                        userLogin.UserCode = "Error";
                        userLogin.ErrorCode = "0";
                        objDynamic.Add(userLogin);
                    }
                }

            }

            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }


        public List<dynamic> GetloginDetails(RootUserLogin model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            int intUSerId = 0;
            try
            {

                if (model.Type ==2)
                {
                    OTPGenerator oTPGenerator = new OTPGenerator();
                    model.OTP = oTPGenerator.GenerateRandomNo();

                }
                DataSet ds = Get_UserMasterLogin(model);
                if (ds.Tables.Count > 0)
                {
                    var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                    List<ViewLogin> ViewLogin =
                       (from item in myEnumerableFeaprd
                        select new ViewLogin
                        {
                            intUSerId = item.Field<Int64?>("User_PkeyID"),
                            IsVerified = item.Field<Boolean?>("User_IsVerified"),
                        }).ToList();
                    objDynamic.Add(ViewLogin);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["User_PkeyID"].ToString()))
                        {
                            intUSerId = Convert.ToInt32(ds.Tables[0].Rows[i]["User_PkeyID"].ToString());
                        }
                       
                    }
                }
                if (model.Type == 2 || model.Type == 4)
                {

                    //EmailDTO emailDTO = new EmailDTO();
                    //EmailTemplate emailTemplate = new EmailTemplate();
                    //emailDTO.To = model.Um_Email;
                    //emailDTO.FirstName = model.Um_Name;
                    //var emaildata = emailTemplate.VerifiedRegistration(emailDTO, model.OTP);

                    //if (intUSerId != -99 && intUSerId != 0)
                    //{
                    //    EmailTemplate emailTemplate = new EmailTemplate();
                    //    UserUserVerificationMaster_Details userUserVerificationMaster_Details = new UserUserVerificationMaster_Details();
                    //    UserVerificationMaster_Data userVerificationMaster_Data = new UserVerificationMaster_Data();
                    //    userUserVerificationMaster_Details.User_Email = model.Um_Email;
                    //    userUserVerificationMaster_Details.User_FirstName = model.Um_Name;
                    //    userUserVerificationMaster_Details.User_pkeyID = intUSerId;
                    //    userVerificationMaster_Data.GeneratePasswordLink(userUserVerificationMaster_Details, 1);

                    //}


                }
               
            }

            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }
        public List<dynamic> GetAdminloginDetails(RootUserLogin model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            int intUSerId = 0;
            try
            {

                if (model.Type == 2)
                {
                    OTPGenerator oTPGenerator = new OTPGenerator();
                    model.OTP = oTPGenerator.GenerateRandomNo();

                }
                DataSet ds = Get_UserMasterAdminLogin(model);
                if (ds.Tables.Count > 0)
                {
                    var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                    List<ViewLogin> ViewLogin =
                       (from item in myEnumerableFeaprd
                        select new ViewLogin
                        {
                            intUSerId = item.Field<Int64?>("User_PkeyID"),
                            IsVerified = item.Field<Boolean?>("User_IsVerified"),
                        }).ToList();
                    objDynamic.Add(ViewLogin);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["User_PkeyID"].ToString()))
                        {
                            intUSerId = Convert.ToInt32(ds.Tables[0].Rows[i]["User_PkeyID"].ToString());
                        }

                    }
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
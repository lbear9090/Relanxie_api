﻿using Avigma.Repository.Lib;
using Avigma.Repository.Security;
using Avigma.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;

namespace Avigma.Repository.Lib
{
    public class UserVerificationMaster_Data
    {

        MyDataSourceFactory obj = new MyDataSourceFactory();
        SecurityHelper securityHelper = new SecurityHelper();
        Log log = new Log();


        private List<dynamic> AddUpdateUserVerificationMaster_Data(UserVerificationMaster_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[CreateUpdate_UserVerificationMaster]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                input_parameters.Add("@UserVeriPKID", 1 + "#bigint#" + model.UserVeriPKID);
                input_parameters.Add("@UserVerificationID", 1 + "#varchar#" + model.UserVerificationID);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@VerificationCode", 1 + "#varchar#" + model.VerificationCode);
                input_parameters.Add("@Verification_IsActive", 1 + "#bit#" + model.Verification_IsActive);
                input_parameters.Add("@Verification_IsDelete", 1 + "#bit#" + model.Verification_IsDelete);

                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@VUserId", 1 + "#bigint#" + model.VUserId);
                input_parameters.Add("@UserVeriPKID_Out", 2 + "#bigint#" + null);
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
        public List<dynamic> AddUserVerificationMaster_Data(UserVerificationMaster_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                objData = AddUpdateUserVerificationMaster_Data(model);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }

        private DataSet Get_UserVerificationMaster(UserVerificationMaster_DTO model)
        {
            DataSet ds = null;
            try
            {

                string selectProcedure = "[Get_UserVerificationMaster]";
                Dictionary<string, string> input_parameters = new Dictionary<string, string>();

                input_parameters.Add("@UserVeriPKID", 1 + "#bigint#" + model.UserVeriPKID);

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

        public List<dynamic> Get_UserVerificationMasterDetails(UserVerificationMaster_DTO model)
        {
            List<dynamic> objDynamic = new List<dynamic>();
            try
            {


                DataSet ds = Get_UserVerificationMaster(model);

                var myEnumerableFeaprd = ds.Tables[0].AsEnumerable();
                List<UserVerificationMaster_DTO> UserVerificationMaster =
                   (from item in myEnumerableFeaprd
                    select new UserVerificationMaster_DTO
                    {
                        UserVeriPKID = item.Field<Int64>("UserVeriPKID"),
                        UserVerificationID = item.Field<String>("UserVerificationID"),
                        UserID = item.Field<Int64?>("UserID"),
                        VerificationCode = item.Field<String>("VerificationCode"),
                        Verification_IsActive = item.Field<Boolean?>("Verification_IsActive"),
                    }).ToList();

                objDynamic.Add(UserVerificationMaster);
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }

            return objDynamic;
        }

        //pradeep
        private List<dynamic> Get_CheckUserVerificationData(UserVerificationMaster_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();

            string insertProcedure = "[Get_CheckUserVerificationData]";

            Dictionary<string, string> input_parameters = new Dictionary<string, string>();
            try
            {
                
                input_parameters.Add("@UserVerificationID", 1 + "#varchar#" + model.UserVerificationID);
                input_parameters.Add("@VerificationCode", 1 + "#varchar#" + model.VerificationCode);
                input_parameters.Add("@User_Password", 1 + "#varchar#" + model.User_Password);
                input_parameters.Add("@UserID", 1 + "#bigint#" + model.UserID);
                input_parameters.Add("@Type", 1 + "#int#" + model.Type);
                input_parameters.Add("@User_LoginName", 2 + "#nvarchar#" + null);
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
        public List<dynamic> Check_User(UserVerificationMaster_DTO model)
        {
            List<dynamic> objData = new List<dynamic>();
            try
            {
                string decriptedUid = securityHelper.Decode(model.UserVerificationID);
                model.UserID = Convert.ToInt64(decriptedUid);
                objData = Get_CheckUserVerificationData(model);
                if (model.Type == 2)
                {
                    objData.Add(model.UserID);
                }
                
            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return objData;
        }


        public int GenerateLink(UserUserVerificationMaster_Details model, int Type)
        {
            int result = 0;
            try
            {
                EmailTemplate emailTemplate = new EmailTemplate();
                string VerificationLink = string.Empty;
                switch (model.Device)
                {
                    case 1:
                        {
                            VerificationLink = model.Email_Url;
                            log.logDebugMessage("Android--------->" + VerificationLink);
                            break;
                        }
                    case 2:
                        {

                            VerificationLink = model.Email_Url;
                            log.logDebugMessage("IOS--------->" + VerificationLink);
                            break;
                        }
                }
                string Fullname = model.User_FirstName + " " + model.User_LastName;
                string mes = emailTemplate.GetEmailMessageText(VerificationLink, Fullname, model.User_Email, Type);
                EmailDTO emailDTO = new EmailDTO();
                emailDTO.To = model.User_Email;
                emailDTO.Message = mes;
                emailTemplate.NewUserRegister(emailDTO, mes, Type);
                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
            }
            return result;
        }

            public int GeneratePasswordLink(UserUserVerificationMaster_Details model, int Type)
        {
            int result = 0;
            try
            {
                //pradeep
               
                EmailTemplate emailTemplate = new EmailTemplate();
                string VerificationLink = string.Empty;
               
                SecurityHelper securityHelper = new SecurityHelper();
                OTPGenerator oTPGenerator = new OTPGenerator();
                UserVerificationMaster_DTO userVerificationMaster_DTO = new UserVerificationMaster_DTO();

                int verificationCode = oTPGenerator.GenerateRandomNo();
                string encriptveryCode = securityHelper.Encode(verificationCode.ToString());
                string encripUid = securityHelper.Encode(model.User_pkeyID.ToString());
                userVerificationMaster_DTO.UserVerificationID = encripUid;
                userVerificationMaster_DTO.VerificationCode = encriptveryCode;
                userVerificationMaster_DTO.UserID = model.User_pkeyID; //model.UserID;
                userVerificationMaster_DTO.VUserId = model.User_pkeyID;
                userVerificationMaster_DTO.Verification_IsActive = true;
                userVerificationMaster_DTO.Verification_IsDelete = false;
                userVerificationMaster_DTO.Type = 1;
                AddUserVerificationMaster_Data(userVerificationMaster_DTO);

                


                //Send Email
                string sendEmailUrl = ConfigurationManager.AppSettings["sendEmailUrl"].ToString();

                // string VerificationLink = "" + sendEmailUrl + "?uid=" + encripUid + "&code=" + encriptveryCode + "&type="+ Type.ToString();
                //string VerificationLink = "" + sendEmailUrl + "/" + WebUtility.UrlEncode(encripUid)+ "/" + WebUtility.UrlEncode(encriptveryCode) + "/"+ Type.ToString();
                //VerificationLink = "" + sendEmailUrl + "/" + encripUid + "/" + encriptveryCode + "/" + Type.ToString();
                // string VerificationLink = "" + sendEmailUrl + "/" + encripUid + "/" + encriptveryCode + "/" + Type.ToString();
                switch (model.Device)
                {
                    case 1:
                        {
                            //VerificationLink = "<a href='http://intent://communav.ikaart.org/"+encripUid + "/" + encriptveryCode + "/" + Type.ToString()
                            //    + "#Intent;scheme=http;component=ResetPassword;package=com.communa_v;end' target = '_blank'>app </a>";

                            VerificationLink = model.Email_Url;
                            log.logDebugMessage("Android--------->"+ VerificationLink);
                            break;
                        }
                    case 2:
                        {
                            // VerificationLink = "http://communav.ikaart.org/ResetPassword" + "/" + encripUid + "/" + encriptveryCode + "/" + Type.ToString();
                            VerificationLink = model.Email_Url ;
                            log.logDebugMessage("IOS--------->" + VerificationLink);
                            break;
                        }
                }
                string Fullname = model.User_FirstName + " " + model.User_LastName;
                string mes = emailTemplate.GetEmailMessageText(VerificationLink, Fullname, model.User_Email, Type);
                EmailDTO emailDTO = new EmailDTO();
                emailDTO.To = model.User_Email;
                emailDTO.Message = mes;
                emailTemplate.NewUserRegister(emailDTO, mes, Type);
                result = 1;
                return result;
            }
            catch (Exception ex)
            {

                log.logErrorMessage(ex.StackTrace);
                log.logErrorMessage(ex.Message);
                return result ;
            }

        }
    }
}
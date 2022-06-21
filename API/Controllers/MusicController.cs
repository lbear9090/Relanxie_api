using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Avigma.Repository.Lib;
using API.Models.Project;
using API.Repository.Project;
using Avigma.Models;
using RestSharp;
using System.Configuration;
using System.Web;
using System.IO;

namespace API.Controllers
{
    [RoutePrefix("api/Music")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MusicController : BaseController
    {
        Log log = new Log();
        User_Admin_Master_Data User_Admin_Master_Data = new User_Admin_Master_Data();
        Get_User_Admin_Role_Data Get_User_Admin_Role_Data = new Get_User_Admin_Role_Data();
        UserMaster_Data userMaster_Data = new UserMaster_Data();
        User_Guide_Data user_Guide_Data = new User_Guide_Data();
        Diet_Tips_Data diet_Tips_Data = new Diet_Tips_Data();
        Bad_Food_Data bad_Food_Data = new Bad_Food_Data();
        Brain_Food_Data brain_Food_Data = new Brain_Food_Data();
        User_Recipes_Data user_Recipes_Data = new User_Recipes_Data();
        Menu_Master_Data Menu_Master_Data = new Menu_Master_Data();
        Menu_Role_Relation_Data Menu_Role_Relation_Data = new Menu_Role_Relation_Data();
        Grocery_List_Data grocery_List_Data = new Grocery_List_Data();
        User_Medidation_Data user_Medidation_Data = new User_Medidation_Data();
        User_Nurtition_Data user_Nurtition_Data = new User_Nurtition_Data();
        User_Yoga_Data user_Yoga_Data = new User_Yoga_Data();
        Online_Therapy_Data online_Therapy_Data = new Online_Therapy_Data();

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("AddUserMasterData")]
        public async Task<List<dynamic>> AddUserMasterData()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {



                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage(s);
                var Data = JsonConvert.DeserializeObject<UserMaster_DTO>(s);
                Data.UserID = LoggedInUserId;
                if (Data.Type != 1)
                {
                    if (Data.Type == 8)
                    {
                        Data.Type = 2;
                    }
                    else if (Data.Type != 9 && Data.Type != 4)
                    {
                        Data.User_PkeyID = LoggedInUserId;
                    }
                    else
                    {
                        Data.User_PkeyID = LoggedInUserId;
                    }

                }
                var userMasterDetails = await Task.Run(() => userMaster_Data.AddUserMaster_Data(Data));

                return userMasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserMasterData")]
        public async Task<List<dynamic>> GetUserMasterData()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {

                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage(s);
                var Data = JsonConvert.DeserializeObject<UserMaster_DTO>(s);
                Data.UserID = LoggedInUserId;
                if (Data.Type == 2)
                {
                    Data.User_PkeyID = LoggedInUserId;
                }
                else
                {
                    if (Data.Type != 1)
                    {
                        if (Data.Type == 4)
                        {
                            Data.Type = 2;
                        }
                        else
                        {
                            // Data.User_PkeyID = LoggedInUserId;
                        }

                    }
                }
                var userMasterDetails = await Task.Run(() => userMaster_Data.Get_UserMasterDetails(Data));

                return userMasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("UploadImages")]
        public async Task<List<dynamic>> UploadImages()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {

                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage(s);
                var Data = JsonConvert.DeserializeObject<ImageData>(s);
                ImageGenerator imageGenerator = new ImageGenerator();
                var ImageData = await Task.Run(() => imageGenerator.GetImagePath(Data));

                return ImageData;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("UploadDocuments")]
        public IHttpActionResult UploadDocuments()
        {
            Response res = new Response();
            res.Code = 401;
            res.Data = "";
            res.Message = "Unable to upload file";
            string BasePath = ConfigurationManager.AppSettings["DocumentPath"];
            string LocalBasePath = ConfigurationManager.AppSettings["DocumentDBPath"];

            try
            {

                var files = HttpContext.Current.Request.Files;

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    List<object> filePathList = new List<object>();
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile postedFile = files[i];
                        string filePath = string.Empty;
                        if (postedFile != null)
                        {
                            string path = System.Web.HttpContext.Current.Server.MapPath("~/UploadDocuments/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            var extension = Path.GetExtension(postedFile.FileName);
                            var FileName2 = "";
                            if (extension == null || extension == "")
                            {
                                FileName2 = DateTime.Now.Ticks + "_" + i + ".pdf";
                            }
                            else
                            {
                                FileName2 = DateTime.Now.Ticks + "_" + i + extension;
                            }
                            //var FileName2 = DateTime.Now.Ticks + extension;
                            filePath = path + FileName2;
                            postedFile.SaveAs(filePath);
                            var FileName1 = Path.GetFileName(postedFile.FileName);



                            //string FileUrl = BasePath + FileName2;
                            filePathList.Add(new { url = LocalBasePath + FileName2, name = FileName1, ext = extension });
                        }
                    }


                    res.Code = 200;
                    res.Error = "";
                    res.Data = filePathList;
                    res.Message = "Success...!";
                    return Json(res);
                }
            }
            catch (Exception ex)
            {
                res.Code = 400;
                res.Error = "";
                res.Data = "";
                res.Message = ex.Message;
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                return Json(res);
            }
            return Json("");
        }

        [HttpPost]
        [Route("ForGotPassword")]
        public async Task<List<dynamic>> ForGotPassword()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                GetSetUser getSetUser = new GetSetUser();
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------ForGotPassword Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------ForGotPassword End--------------");
                var user_Child_DTO = JsonConvert.DeserializeObject<UserLogin>(s);


                var MasterDetails = await Task.Run(() => getSetUser.GetForGetPassword(user_Child_DTO));

                return MasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetVerificationLink")]
        public async Task<List<dynamic>> GetVerificationLink()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                GetSetUser getSetUser = new GetSetUser();
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetVerificationLink Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetVerificationLink End--------------");
                var user_Child_DTO = JsonConvert.DeserializeObject<UserLogin>(s);
                user_Child_DTO.User_PkeyID = LoggedInUserId;
                user_Child_DTO.UserID = LoggedInUserId;

                var MasterDetails = await Task.Run(() => getSetUser.GetVerificationLink(user_Child_DTO));

                return MasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }
        [HttpPost]
        [Route("GetUserViryficationDetails")]
        public async Task<List<dynamic>> GetUserViryficationDetails()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                UserVerificationMaster_Data userVerificationMaster_Data = new UserVerificationMaster_Data();
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetUserViryficationDetails Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetUserViryficationDetails End--------------");
                //log.logDebugMessage(s);
                var Data = JsonConvert.DeserializeObject<UserVerificationMaster_DTO>(s);
                //Data.Type = 1;

                var Getuser = await Task.Run(() => userVerificationMaster_Data.Check_User(Data));
                return Getuser;

            }
            catch (Exception ex)
            {

                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [HttpPost]
        [Route("VerifyUserByEmail")]
        public async Task<List<dynamic>> VerifyUserByEmail()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                UserVerificationMaster_Data userVerificationMaster_Data = new UserVerificationMaster_Data();
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------VerifyUserByEmail Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------VerifyUserByEmail End--------------");

                var Data = JsonConvert.DeserializeObject<UserMaster_DTO>(s);


                var Getuser = await Task.Run(() => userMaster_Data.VerifyUserByEmail(Data));
                return Getuser;

            }
            catch (Exception ex)
            {

                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<List<dynamic>> ChangePassword()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                UserMaster_Data userMaster_Data = new UserMaster_Data();
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------ChangePassword Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------ChangePassword End--------------");
                var userMaster_DTO = JsonConvert.DeserializeObject<UserMaster_ChangePassword>(s);

                var userDetails = await Task.Run(() => userMaster_Data.ChangePassword(userMaster_DTO));
                userMaster_DTO.Type = 5;
                return userDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("ChangePasswordByEmail")]
        public async Task<List<dynamic>> ChangePasswordByEmail()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                UserMaster_Data userMaster_Data = new UserMaster_Data();
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------ChangePassword Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------ChangePassword End--------------");
                var userMaster_DTO = JsonConvert.DeserializeObject<UserMaster_ChangePassword>(s);

                var userDetails = await Task.Run(() => userMaster_Data.ChangePasswordByEmail(userMaster_DTO));
                userMaster_DTO.Type = 5;
                return userDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }

        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserAdminMaster")]
        public async Task<List<dynamic>> CreateUpdate_User_Admin_Master()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateUserAdminMaster Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateUserAdminMaster End--------------");
                var Data = JsonConvert.DeserializeObject<User_Admin_Master_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_User_Admin_Master_DataDetails = await Task.Run(() => User_Admin_Master_Data.CreateUpdate_User_Admin_Master_DataDetails(Data));

                return CreateUpdate_User_Admin_Master_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateUserAdminMaster");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserAdminMaster")]
        public async Task<List<dynamic>> Get_User_Admin_Master()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetUserAdminMaster Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetUserAdminMaster End--------------");
                var Data = JsonConvert.DeserializeObject<User_Admin_Master_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_User_Admin_MasterDetails = await Task.Run(() => User_Admin_Master_Data.Get_User_Admin_MasterDetails(Data));

                return Get_User_Admin_MasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetUserAdminMaster");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserGuide")]
        public async Task<List<dynamic>> CreateUpdate_User_Guide()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateUserGuide Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateUserGuide End--------------");
                var Data = JsonConvert.DeserializeObject<User_Guide_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_User_Guide_DataDetails = await Task.Run(() => user_Guide_Data.CreateUpdate_User_Guide_DataDetails(Data));

                return CreateUpdate_User_Guide_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateUserGuide");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserGuide")]
        public async Task<List<dynamic>> Get_User_Guide()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetUserGuide Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetUserGuide End--------------");
                var Data = JsonConvert.DeserializeObject<User_Guide_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_User_GuideDetails = await Task.Run(() => user_Guide_Data.Get_User_GuideDetails(Data));

                return Get_User_GuideDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetUserGuide");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateDietTips")]
        public async Task<List<dynamic>> CreateUpdate_Diet_Tips()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateDietTips Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateDietTips End--------------");
                var Data = JsonConvert.DeserializeObject<Diet_Tips_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_Diet_Tips_DataDetails = await Task.Run(() => diet_Tips_Data.CreateUpdate_Diet_Tips_DataDetails(Data));

                return CreateUpdate_Diet_Tips_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateDietTips");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetDietTips")]
        public async Task<List<dynamic>> Get_Diet_Tips()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetDietTips Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetDietTips End--------------");
                var Data = JsonConvert.DeserializeObject<Diet_Tips_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_Diet_TipsDetails = await Task.Run(() => diet_Tips_Data.Get_Diet_TipsDetails(Data));

                return Get_Diet_TipsDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetDietTips");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }



        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateBadFood")]
        public async Task<List<dynamic>> CreateUpdate_Bad_Food()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateBadFood Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateBadFood End--------------");
                var Data = JsonConvert.DeserializeObject<Bad_Food_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_Bad_Food_DataDetails = await Task.Run(() => bad_Food_Data.CreateUpdate_Bad_Food_DataDetails(Data));

                return CreateUpdate_Bad_Food_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateBadFood");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetBadFood")]
        public async Task<List<dynamic>> Get_Bad_Food()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetBadFood Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetBadFood End--------------");
                var Data = JsonConvert.DeserializeObject<Bad_Food_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_Bad_FoodDetails = await Task.Run(() => bad_Food_Data.Get_Bad_FoodDetails(Data));

                return Get_Bad_FoodDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetBadFood");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateBrainFood")]
        public async Task<List<dynamic>> CreateUpdate_Brain_Food()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateBrainFood Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateBrainFood End--------------");
                var Data = JsonConvert.DeserializeObject<Brain_Food_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_Brain_Food_DataDetails = await Task.Run(() => brain_Food_Data.CreateUpdate_Brain_Food_DataDetails(Data));

                return CreateUpdate_Brain_Food_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateBrainFood");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetBrainFood")]
        public async Task<List<dynamic>> Get_Brain_Food()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetBrainFood Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetBrainFood End--------------");
                var Data = JsonConvert.DeserializeObject<Brain_Food_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_Brain_FoodDetails = await Task.Run(() => brain_Food_Data.Get_Brain_FoodDetails(Data));

                return Get_Brain_FoodDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetBrainFood");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserRecipes")]
        public async Task<List<dynamic>> CreateUpdate_User_Recipes()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateUserRecipes Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateUserRecipes End--------------");
                var Data = JsonConvert.DeserializeObject<User_Recipes_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_User_Recipes_DataDetails = await Task.Run(() => user_Recipes_Data.CreateUpdate_User_Recipes_DataDetails(Data));

                return CreateUpdate_User_Recipes_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateUserRecipes");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserRecipes")]
        public async Task<List<dynamic>> Get_User_Recipes()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetUserRecipes Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetUserRecipes End--------------");
                var Data = JsonConvert.DeserializeObject<User_Recipes_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_User_RecipesDetails = await Task.Run(() => user_Recipes_Data.Get_User_RecipesDetails(Data));

                return Get_User_RecipesDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetUserRecipes");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserAdminRole")]
        public async Task<List<dynamic>> Get_User_Admin_Role()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetUserAdminRole Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetUserAdminRole End--------------");
                var Data = JsonConvert.DeserializeObject<User_Admin_Master_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_User_Admin_RoleDetails = await Task.Run(() => Get_User_Admin_Role_Data.Get_User_Admin_RoleDetails(Data));

                return Get_User_Admin_RoleDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetUserAdminRole");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateMenuMaster")]
        public async Task<List<dynamic>> CreateUpdate_Menu_Master()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateMenuMaster Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateMenuMaster End--------------");
                var Data = JsonConvert.DeserializeObject<Menu_Master_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_Menu_Master_DataDetails = await Task.Run(() => Menu_Master_Data.CreateUpdate_Menu_Master_DataDetails(Data));

                return CreateUpdate_Menu_Master_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateMenuMaster");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetMenuMaster")]
        public async Task<List<dynamic>> Get_Menu_Master()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetMenuMaster Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetMenuMaster End--------------");
                var Data = JsonConvert.DeserializeObject<Menu_Master_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_Menu_MasterDetails = await Task.Run(() => Menu_Master_Data.Get_Menu_MasterDetails(Data));

                return Get_Menu_MasterDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetMenuMaster");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateMenuRoleRelation")]
        public async Task<List<dynamic>> CreateUpdate_Menu_Role_Relation()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateMenuRoleRelation Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateMenuRoleRelation End--------------");
                var Data = JsonConvert.DeserializeObject<Menu_Role_Relation_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_Menu_Role_Relation_DataDetails = await Task.Run(() => Menu_Role_Relation_Data.CreateUpdate_Menu_Role_Relation_DataDetails(Data));

                return CreateUpdate_Menu_Role_Relation_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateMenuRoleRelation");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }
        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetMenuRoleRelation")]
        public async Task<List<dynamic>> Get_Menu_Role_Relation()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetMenuRoleRelation Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetMenuRoleRelation End--------------");
                var Data = JsonConvert.DeserializeObject<Menu_Role_Relation_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_Menu_Role_RelationDetails = await Task.Run(() => Menu_Role_Relation_Data.Get_Menu_Role_RelationDetails(Data));

                return Get_Menu_Role_RelationDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage(" GetMenuRoleRelation");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }



        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateGroceryList")]
        public async Task<List<dynamic>> CreateUpdate_Grocery_List()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateGroceryList Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateGroceryList End--------------");
                var Data = JsonConvert.DeserializeObject<Grocery_List_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_Grocery_List_DataDetails = await Task.Run(() => grocery_List_Data.CreateUpdate_Grocery_List_DataDetails(Data));

                return CreateUpdate_Grocery_List_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateGroceryList");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetGroceryList")]
        public async Task<List<dynamic>> Get_Grocery_List()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetGroceryList Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetGroceryList End--------------");
                var Data = JsonConvert.DeserializeObject<Grocery_List_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_Grocery_ListDetails = await Task.Run(() => grocery_List_Data.Get_Grocery_ListDetails(Data));

                return Get_Grocery_ListDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetGroceryList");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }


        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserMedidation")]
        public async Task<List<dynamic>> CreateUpdate_User_Medidation()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateUserMedidation Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateUserMedidation End--------------");
                var Data = JsonConvert.DeserializeObject<User_Medidation_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_User_Medidation_DataDetails = await Task.Run(() => user_Medidation_Data.CreateUpdate_User_Medidation_DataDetails(Data));

                return CreateUpdate_User_Medidation_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateUserMedidation");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserMedidation")]
        public async Task<List<dynamic>> Get_User_Medidation()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetUserMedidation Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetUserMedidation End--------------");
                var Data = JsonConvert.DeserializeObject<User_Medidation_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_User_MedidationDetails = await Task.Run(() => user_Medidation_Data.Get_User_MedidationDetails(Data));

                return Get_User_MedidationDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetUserMedidation");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserNutrition")]
        public async Task<List<dynamic>> CreateUpdate_User_Nutrition()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateUserNutrition Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateUserNutrition End--------------");
                var Data = JsonConvert.DeserializeObject<User_Nurtition_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_User_Nutrition_DataDetails = await Task.Run(() => user_Nurtition_Data.CreateUpdate_User_Nutrition_DataDetails(Data));

                return CreateUpdate_User_Nutrition_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateUserNutrition");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserNutrition")]
        public async Task<List<dynamic>> Get_User_Nutrition()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetUserNutrition Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetUserNutrition End--------------");
                var Data = JsonConvert.DeserializeObject<User_Nurtition_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_User_NutritionDetails = await Task.Run(() => user_Nurtition_Data.Get_User_NutritionDetails(Data));

                return Get_User_NutritionDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetUserNutrition");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateUserYoga")]
        public async Task<List<dynamic>> CreateUpdate_User_Yoga()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateUserYoga Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateUserYoga End--------------");
                var Data = JsonConvert.DeserializeObject<User_Yoga_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_User_Yoga_DataDetails = await Task.Run(() => user_Yoga_Data.CreateUpdate_User_Yoga_DataDetails(Data));

                return CreateUpdate_User_Yoga_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateUserYoga");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetUserYoga")]
        public async Task<List<dynamic>> Get_User_Yoga()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetUserYoga Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetUserYoga End--------------");
                var Data = JsonConvert.DeserializeObject<User_Yoga_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_User_YogaDetails = await Task.Run(() => user_Yoga_Data.Get_User_YogaDetails(Data));

                return Get_User_YogaDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetUserYoga");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("CreateUpdateOnlineTherapy")]
        public async Task<List<dynamic>> CreateUpdate_Online_Therapy()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------CreateUpdateOnlineTherapy Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------CreateUpdateOnlineTherapy End--------------");
                var Data = JsonConvert.DeserializeObject<Online__Therapy_DTO>(s);
                Data.UserID = LoggedInUserId;

                var CreateUpdate_Online_Therapy_DataDetails = await Task.Run(() => online_Therapy_Data.CreateUpdate_Online_Therapy_DataDetails(Data));

                return CreateUpdate_Online_Therapy_DataDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("CreateUpdateOnlineTherapy");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }

        [AllowAnonymous]
        [Authorize]
        [HttpPost]
        [Route("GetOnlineTherapy")]
        public async Task<List<dynamic>> Get_Online_Therapy()
        {
            List<dynamic> objdynamicobj = new List<dynamic>();

            try
            {
                System.IO.Stream body = System.Web.HttpContext.Current.Request.InputStream;
                System.Text.Encoding encoding = System.Web.HttpContext.Current.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                string s = reader.ReadToEnd();
                log.logDebugMessage("----------GetOnlineTherapy Start--------------");
                log.logDebugMessage(s);
                log.logDebugMessage("----------GetOnlineTherapy End--------------");
                var Data = JsonConvert.DeserializeObject<Online__Therapy_DTO>(s);
                Data.UserID = LoggedInUserId;

                var Get_Online_TherapyDetails = await Task.Run(() => online_Therapy_Data.Get_Online_TherapyDetails(Data));

                return Get_Online_TherapyDetails;

            }
            catch (Exception ex)
            {
                log.logErrorMessage("GetOnlineTherapy");
                log.logErrorMessage(ex.Message);
                log.logErrorMessage(ex.StackTrace);
                objdynamicobj.Add(ex.Message);
                return objdynamicobj;
            }
        }


       
       

    }
}
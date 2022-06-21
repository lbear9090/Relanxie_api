using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avigma.Models
{
    public class UserMaster_DTO
    {
        public Int64 User_PkeyID { get; set; }
        public Int64? User_PkeyID_Master { get; set; }
        public String User_Name { get; set; }
        public String User_Email { get; set; }
        public String User_Password { get; set; }
        public String User_Phone { get; set; }
        public String User_Address { get; set; }
        public String User_City { get; set; }
        public String User_Country { get; set; }
        public String User_Zip { get; set; }
        public DateTime? User_DOB { get; set; }
        public int? User_Type { get; set; }
        public int? User_Gender { get; set; }
        public String User_Image_Path { get; set; }
        public String User_Image_Base { get; set; }
        public String User_MacID { get; set; }
        public Boolean? User_IsVerified { get; set; }
        public Boolean? User_IsActive { get; set; }
        public Boolean? User_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
        public String User_latitude { get; set; }
        public String User_longitude { get; set; }
       
        public String User_Token_val { get; set; }
        public int? User_Login_Type { get; set; }
        public Boolean? User_IsActive_Prof { get; set; }
        public String User_LastName { get; set; }
        public String User_Company { get; set; }
        public String User_Stripe_CustID { get; set; }
        public String User_Stripe_SubsID { get; set; }
        public String User_Stripe_AttachID { get; set; }
        public String User_Stripe_PaymentID { get; set; }
        public Boolean? User_Ispaid { get; set; }

    }

    public class UserMaster_ChangePassword
    {
        public String User_PkeyID { get; set; }
        public String User_Password { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
        public String User_Email { get; set; }
        public int? User_Type { get; set; }

    }


   

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class User_Nurtition_DTO
    {
        public Int64 NU_PKeyID { get; set; }
        public String NU_Name { get; set; }
        public String NU_Description { get; set; }
        public String NU_Type { get; set; }
        public Boolean? NU_IsActive { get; set; }
        public Boolean? NU_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
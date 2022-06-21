using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class User_Guide_DTO
    {
        public Int64 GU_PKeyID { get; set; }
        public String GU_Name { get; set; }
        public String GU_Description { get; set; }
        public Boolean? GU_IsActive { get; set; }
        public Boolean? GU_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }

}
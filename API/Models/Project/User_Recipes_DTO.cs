using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class User_Recipes_DTO
    {
        public Int64 UR_PKeyID { get; set; }
        public String UR_Name { get; set; }
        public String UR_Description { get; set; }
        public String UR_filePath { get; set; }
        public String UR_fileName { get; set; }
        public int? UR_Types { get; set; }
        public Boolean? UR_IsActive { get; set; }
        public Boolean? UR_Desc_Show { get; set; }
     
        public Boolean? UR_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
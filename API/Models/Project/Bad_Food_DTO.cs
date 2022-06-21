using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class Bad_Food_DTO
    {
        public Int64 BF_PKeyID { get; set; }
        public String BF_Name { get; set; }
        public String BF_Description { get; set; }
        public String BF_Type { get; set; }
        public Boolean? BF_IsActive { get; set; }
        public Boolean? BF_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
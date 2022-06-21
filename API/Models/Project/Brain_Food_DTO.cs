using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class Brain_Food_DTO
    {
        public Int64 BRF_PKeyID { get; set; }
        public String BRF_Name { get; set; }
        public String BRF_Description { get; set; }
        public String BRF_Type { get; set; }
        public Boolean? BRF_IsActive { get; set; }
        public Boolean? BRF_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
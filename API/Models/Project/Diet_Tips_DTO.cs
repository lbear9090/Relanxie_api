using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class Diet_Tips_DTO
    {
        public Int64 DT_PKeyID { get; set; }
        public String DT_Name { get; set; }
        public String DT_Description { get; set; }
        public String DT_Type { get; set; }
        public Boolean? DT_IsActive { get; set; }
        public Boolean? DT_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
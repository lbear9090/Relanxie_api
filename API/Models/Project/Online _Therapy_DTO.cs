using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class Online__Therapy_DTO
    {
        public Int64 OT_PKeyID { get; set; }
        public String OT_Name { get; set; }
        public String OT_Description { get; set; }
        public String OT_Type { get; set; }
        public Boolean? OT_IsActive { get; set; }
        public Boolean? OT_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class Grocery_List_DTO
    {
        public Int64 GR_PKeyID { get; set; }
        public String GR_Name { get; set; }
        public String GR_Description { get; set; }
        public String GR_Type { get; set; }
        public Boolean? GR_IsActive { get; set; }
        public Boolean? GR_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
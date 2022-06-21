using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class User_Yoga_DTO
    {
        public Int64 YG_PKeyID { get; set; }
        public String YG_Name { get; set; }
        public String YG_Description { get; set; }
        public String YG_Type { get; set; }
        public String YG_File_Name { get; set; }
        public String YG_File_Path { get; set; }
        public String YG_File_Type { get; set; }
        public int? YG_Timer { get; set; }
        public Decimal? YG_Size { get; set; }
        public String YG_ThumbNail_Path { get; set; }
        public Boolean? YG_IsActive { get; set; }
        public Boolean? YG_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
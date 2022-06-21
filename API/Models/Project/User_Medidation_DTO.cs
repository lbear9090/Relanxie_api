using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class User_Medidation_DTO
    {
        public Int64 MD_PKeyID { get; set; }
        public String MD_Name { get; set; }
        public String MD_Description { get; set; }
        public String MD_Type { get; set; }
        public String MD_File_Name { get; set; }
        public String MD_File_Path { get; set; }
        public String MD_File_Type { get; set; }
        public Boolean? MD_IsActive { get; set; }
        public Boolean? MD_IsDelete { get; set; }
        public int? MD_Timer { get; set; }
        public Decimal? MD_Size { get; set; }
        public String MD_ThumbNail_Path { get; set; }
        public String MD_File_Name_2 { get; set; }
        public String MD_File_Path_2 { get; set; }
        public String MD_File_Type_2 { get; set; }
        public Decimal? MD_Size_2 { get; set; }
        public String MD_ThumbNail_Path_2 { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
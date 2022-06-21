using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Project
{
    public class Tyre_User_Details_DTO
    {
        public Int64 TUD_PKeyID { get; set; }
        public Int64? TUD_TUM_PKeyID { get; set; }
        public Int64? TUD_Tyre_PKeyID { get; set; }
        public int? TUD_Brand { get; set; }
        public String TUD_Size { get; set; }
        public DateTime? TUD_Installation_Date { get; set; }
        public int? TUD_Mileage { get; set; }
        public Int64? TUD_User_PkeyID { get; set; }
        public Int64? TUD_TUMM_PKeyID { get; set; }
        public Boolean? TUD_IsActive { get; set; }
        public int? TUD_Status { get; set; }
        public int? TUD_Percent { get; set; }
        public int? TUD_Position { get; set; }
        public Boolean? TUD_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
        public Int64? TUD_TOC_PkeyID { get; set; }

    }
}
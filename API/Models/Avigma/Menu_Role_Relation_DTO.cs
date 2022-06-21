﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avigma.Models
{
    public class Menu_Role_Relation_DTO
    {
        public Int64 MUR_PkeyID { get; set; }
        public Int64? MUR_MenuID { get; set; }
        public Int64? MUR_Role { get; set; }
        public Boolean? MUR_IsActive { get; set; }
        public Boolean? MUR_IsDelete { get; set; }
        public int? Type { get; set; }
        public Int64? UserID { get; set; }
    }
}
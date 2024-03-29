﻿using Perm.Model.Abstraction;
using Perm.Model.Setup;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Perm.Model.Admin
{
    /// <summary>
    /// 
    /// </summary>
    [Table(name: "PermissionDetail", Schema = "Admin")]
    public class PermissionDetailModel : ModelBase
    {
        [Key]
        public long PermissionDetailID { get; set; }
        public long PermissionID { get; set; }
        public long PageOptionID { get; set; }
        public long ParamAccessTypeID { get; set; }

        public PermissionModel Permission { get; set; }
        public ApplicationParamDetailModel ParamAccessType { get; set; }
        public PageOptionModel PageOption { get; set; }
    }
}

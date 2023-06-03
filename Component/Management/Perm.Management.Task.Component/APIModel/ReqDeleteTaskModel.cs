using Perm.Common.APIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.Management.Tasks.Module.APIModel
{
    public class ReqDeleteTaskModel : IRequestModel
    {
        public long TaskID { get; set; }
    }
}

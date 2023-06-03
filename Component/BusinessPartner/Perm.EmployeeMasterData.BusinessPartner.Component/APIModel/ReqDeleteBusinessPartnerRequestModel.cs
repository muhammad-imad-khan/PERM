using Perm.Common.APIModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perm.EmployeeMasterData.BusinessPartner.Component.APIModel
{
    public class ReqDeleteBusinessPartnerModel : IRequestModel
    {
        public long BusinessPartnerID { get; set; }
    }
}

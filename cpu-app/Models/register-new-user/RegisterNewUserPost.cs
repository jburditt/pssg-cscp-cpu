using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class RegisterNewUserPost
    {
        public string BusinessBCeID { get; set; }
        public string UserBCeID { get; set; }
        public DynamicsRegisterNewUserContactPost NewContact { get; set; }
        public DynamicsRegisterNewUserServiceProviderPost NewServiceProvider { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Cscp.Victims.Public.Models
{
    public class SignedContractPostFromPortal
    {
        public string BusinessBCeID { get; set; }
        public string UserBCeID { get; set; }
        public DynamicsDocumentPost[] DocumentCollection { get; set; }
        public DynamicsSignaturePost Signature { get; set; }
    }

    public class SignedContractPostToDynamics
    {
        public string BusinessBCeID { get; set; }
        public string UserBCeID { get; set; }
        public DynamicsDocumentPost SignedContract { get; set; }
    }
}

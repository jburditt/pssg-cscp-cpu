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
        public bool IsModificationAgreement { get; set; }
    }

    public class SignedContractPostToDynamics
    {
        public string BusinessBCeID { get; set; }
        public string UserBCeID { get; set; }
        public DynamicsDocumentPost SignedContract { get; set; }
    }

    public class DocMergeRequest
    {
        public JAGDocument[] documents { get; set; }
        public JAGOptions options { get; set; }
    }

    public class JAGDocument
    {
        public string data { get; set; }
        public int index { get; set; }

        public JAGDocument(string data, int index)
        {
            this.data = data;
            this.index = index;
        }
    }

    public class JAGOptions
    {
        public bool createToC { get { return false; } }
        public bool forcePDFAOnLoad { get { return true; } }
    }
}

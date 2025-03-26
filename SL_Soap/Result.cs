using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SL_Soap
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public bool Correct { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public Exception Exception { get; set; }
        [DataMember]
        public object Object { get; set; }
        [DataMember]
        public List<Object> Objects { get; set; }
    }
}
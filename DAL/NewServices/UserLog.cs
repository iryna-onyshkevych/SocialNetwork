using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    [DataContract]
    public class UserLog
    {
        [DataMember]
        public string Userlog { get; set; }

        public UserLog()
        {

        }
        public UserLog(string Userlog)
        {
            this.Userlog = Userlog;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enteties
{

    public class Person
    {
        [JsonProperty(PropertyName = "<id>")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "mail")]
        public string Email { get; set; }


        [JsonProperty(PropertyName = "userlog")]
        public string Userlog { get; set; }
    }
}

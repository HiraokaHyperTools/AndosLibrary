using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class Login
    {
        [JsonProperty("f")] public String f { get; set; }

        [JsonProperty("uname")] public String uname { get; set; }
    }
}

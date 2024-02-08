using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class Copy1Res
    {
        [JsonProperty("ok")] public int ok { get; set; }

        [JsonProperty("id")] public string id { get; set; }

        [JsonProperty("err")] public string err { get; set; }
    }
}

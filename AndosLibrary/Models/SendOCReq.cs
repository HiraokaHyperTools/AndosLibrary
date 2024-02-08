using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class SendOCReq
    {
        [JsonProperty("idfrm")] public string idfrm { get; set; }

        [JsonProperty("idto")] public string idto { get; set; }

        [JsonProperty("p1")] public string p1 { get; set; }

        [JsonProperty("p2")] public string p2 { get; set; }

        [JsonProperty("p3")] public string p3 { get; set; }

        [JsonProperty("body")] public string body { get; set; }

        [JsonProperty("pk")] public int pk;

        [JsonProperty("ct")] public String ct { get; set; }
    }
}

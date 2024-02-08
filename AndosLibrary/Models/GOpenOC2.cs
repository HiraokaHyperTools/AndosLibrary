using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class GOpenOC2
    {
        [JsonProperty("pk")] public int pk { get; set; }
        [JsonProperty("idto")] public string idto { get; set; }
        [JsonProperty("ct")] public string ct { get; set; }
    }
}

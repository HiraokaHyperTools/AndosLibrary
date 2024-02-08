using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class NewDocVer3Res
    {
        [JsonProperty("id")] public string id { get; set; }
        [JsonProperty("version_number")] public string version_number { get; set; }
    }
}

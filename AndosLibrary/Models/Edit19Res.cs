using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class Edit19Res
    {
        [JsonProperty("allok")] public int allok { get; set; }
        [JsonProperty("anyok")] public int anyok { get; set; }
        [JsonProperty("message")] public string message { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class AddJsRes
    {
        [JsonProperty("id")] public string id { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class Basic
    {
        [JsonProperty("f")] public String f { get; set; }
    }
}

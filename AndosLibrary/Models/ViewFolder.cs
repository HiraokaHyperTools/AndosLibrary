using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class ViewFolder
    {
        [JsonProperty("f")] public String f { get; set; }

        [JsonProperty("files")] public FFile[] files { get; set; }
    }
}

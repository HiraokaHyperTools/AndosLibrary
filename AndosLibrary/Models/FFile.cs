using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class FFile
    {
        [JsonProperty("fn")] public String fn { get; set; }
        [JsonProperty("isDir")] public bool isDir { get; set; }
        [JsonProperty("size")] public Int64 size { get; set; }
        [JsonProperty("mtime")] public Int64 mtime { get; set; }
        [JsonProperty("modt")] public String modt { get; set; }
        [JsonProperty("href")] public String href { get; set; }
        [JsonProperty("hrefPic")] public String hrefPic { get; set; }
        [JsonProperty("thumbPic")] public String thumbPic { get; set; }
        [JsonProperty("exeword")] public String exeword { get; set; }
        [JsonProperty("IsDLAllowed")] public bool IsDLAllowed { get; set; }
    }
}

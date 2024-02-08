using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class Search
    {
        [JsonProperty("f")] public String f { get; set; }
        [JsonProperty("cntTotal")] public int cntTotal { get; set; }
        [JsonProperty("cntRecords")] public int cntRecords { get; set; }
        [JsonProperty("start")] public int start { get; set; }
        [JsonProperty("needNext")] public bool needNext { get; set; }
        [JsonProperty("cntPrev")] public int cntPrev { get; set; }
        [JsonProperty("cntNext")] public int cntNext { get; set; }
        [JsonProperty("recs")] public Reco[] recs { get; set; }
    }
}

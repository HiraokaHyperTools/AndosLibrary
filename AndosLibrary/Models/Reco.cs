using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class Reco
    {
        [JsonProperty("id")] public String id { get; set; }
        [JsonProperty("fp")] public String fp { get; set; }
        [JsonProperty("fp2")] public String fp2 { get; set; }

        [JsonProperty("group_id")] public int? group_id { get; set; }
        [JsonProperty("group_only")] public bool? group_only { get; set; }
        [JsonProperty("f_individual")] public bool? f_individual { get; set; }
        [JsonProperty("f_handin")] public bool? f_handin { get; set; }
        [JsonProperty("f_secret")] public bool? f_secret { get; set; }
        [JsonProperty("userdate")] public String userdate { get; set; }
        [JsonProperty("limitdate")] public String limitdate { get; set; }
        [JsonProperty("createuserid")] public string createuserid { get; set; }
        [JsonProperty("adminuserid")] public string adminuserid { get; set; }

        [JsonProperty("campanyname")] public String campanyname { get; set; }
        [JsonProperty("title")] public String title { get; set; }
        [JsonProperty("project")] public String project { get; set; }
        [JsonProperty("usercustomitem1")] public String usercustomitem1 { get; set; }
        [JsonProperty("usercustomitem2")] public String usercustomitem2 { get; set; }
        [JsonProperty("usercustomitem3")] public String usercustomitem3 { get; set; }
        [JsonProperty("usercustomitem4")] public String usercustomitem4 { get; set; }
        [JsonProperty("usercustomitem5")] public String usercustomitem5 { get; set; }
        [JsonProperty("memo")] public String memo { get; set; }

        [JsonProperty("linkpath1")] public string linkpath1 { get; set; }
        [JsonProperty("linkpath2")] public string linkpath2 { get; set; }
        [JsonProperty("linkpath3")] public string linkpath3 { get; set; }
        [JsonProperty("linkpath4")] public string linkpath4 { get; set; }
        [JsonProperty("linkpath5")] public string linkpath5 { get; set; }
        [JsonProperty("linkpath6")] public string linkpath6 { get; set; }
        [JsonProperty("linkname1")] public string linkname1 { get; set; }
        [JsonProperty("linkname2")] public string linkname2 { get; set; }
        [JsonProperty("linkname3")] public string linkname3 { get; set; }
        [JsonProperty("linkname4")] public string linkname4 { get; set; }
        [JsonProperty("linkname5")] public string linkname5 { get; set; }
        [JsonProperty("linkname6")] public string linkname6 { get; set; }

        [JsonProperty("tget_query")] public String tget_query { get; set; }
        [JsonProperty("exeword")] public String exeword { get; set; }
        [JsonProperty("IsDLAllowed")] public bool IsDLAllowed { get; set; }
    }
}

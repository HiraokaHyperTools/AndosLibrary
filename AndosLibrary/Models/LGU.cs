using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class LGU
    {
        [JsonProperty("users")] public U[] users { get; set; }

        [JsonProperty("groups")] public G[] groups { get; set; }

        public class U
        {
            [JsonProperty("id")] public String id { get; set; }

            [JsonProperty("name")] public String name { get; set; }

            [JsonProperty("syscd")] public String syscd { get; set; }

            [JsonProperty("isa")] public int isa { get; set; }

            [JsonProperty("nop")] public int nop { get; set; }

            [JsonProperty("bg")] public int[] bg { get; set; }

            [JsonProperty("dg")] public int dg { get; set; }

            public String Id => id;
            public String Name => "" + name;
        }

        public class G
        {
            [JsonProperty("id")] public int id { get; set; }

            [JsonProperty("name")] public String name { get; set; }

            [JsonProperty("o")] public int o { get; set; }

            [JsonProperty("f1")] public String f1 { get; set; }
            [JsonProperty("f2")] public String f2 { get; set; }
            [JsonProperty("f3")] public String f3 { get; set; }
            [JsonProperty("f4")] public String f4 { get; set; }
            [JsonProperty("f5")] public String f5 { get; set; }
            [JsonProperty("f6")] public String f6 { get; set; }
            [JsonProperty("f7")] public String f7 { get; set; }
            [JsonProperty("company")] public String company { get; set; }
            [JsonProperty("userDate")] public String userDate { get; set; }
            [JsonProperty("memo")] public String memo { get; set; }

            public int Id => id;
            public String Name => "" + name;
        }

        [JsonProperty("PRO4_OC2_ATTS")] public string PRO4_OC2_ATTS { get; set; }

        public string GetAtt(int p)
        {
            var b = (PRO4_OC2_ATTS ?? "");
            if (b.Length == 0)
            {
                return null;
            }

            String[] rows = b.Replace("\r", "").Split('\n');
            if (rows.Length > p)
            {
                return rows[p];
            }
            return null;
        }
    }
}

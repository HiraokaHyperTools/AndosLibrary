using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class Edit19IdlOnly
    {
        /// <summary>
        /// 更新対象の ID リスト
        /// </summary>
        [JsonProperty("idl")] public string[] idl { get; set; }
    }
}

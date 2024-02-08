using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AndosLibrary
{
    public class Edit19
    {
        /// <summary>
        /// ポイント編集を可能にする Edit19 を構築
        /// </summary>
        public static Edit19 CreatePointEdit(
            string[] idl,
            int? group_id = null,
            bool? group_only = null,
            bool? f_individual = null,
            bool? f_handin = null,
            bool? f_secret = null,
            string userdate = null,
            string limitdate = null,
            string createuserid = null,
            string adminuserid = null,
            string campanyname = null,
            string title = null,
            string project = null,
            string usercustomitem1 = null,
            string usercustomitem2 = null,
            string usercustomitem3 = null,
            string usercustomitem4 = null,
            string usercustomitem5 = null,
            string memo = null,
            string linkpath1 = null,
            string linkpath2 = null,
            string linkpath3 = null,
            string linkpath4 = null,
            string linkpath5 = null,
            string linkpath6 = null,
            string linkname1 = null,
            string linkname2 = null,
            string linkname3 = null,
            string linkname4 = null,
            string linkname5 = null,
            string linkname6 = null
        ) => new Edit19
        {
            idl = idl,
            group_id = group_id,
            group_only = group_only,
            f_individual = f_individual,
            f_handin = f_handin,
            f_secret = f_secret,
            userdate = SwapNullAndEmpty(userdate),
            limitdate = SwapNullAndEmpty(limitdate),
            createuserid = SwapNullAndEmpty(createuserid),
            adminuserid = SwapNullAndEmpty(adminuserid),
            campanyname = SwapNullAndEmpty(campanyname),
            title = SwapNullAndEmpty(title),
            project = SwapNullAndEmpty(project),
            usercustomitem1 = SwapNullAndEmpty(usercustomitem1),
            usercustomitem2 = SwapNullAndEmpty(usercustomitem2),
            usercustomitem3 = SwapNullAndEmpty(usercustomitem3),
            usercustomitem4 = SwapNullAndEmpty(usercustomitem4),
            usercustomitem5 = SwapNullAndEmpty(usercustomitem5),
            memo = SwapNullAndEmpty(memo),
            linkpath1 = SwapNullAndEmpty(linkpath1),
            linkpath2 = SwapNullAndEmpty(linkpath2),
            linkpath3 = SwapNullAndEmpty(linkpath3),
            linkpath4 = SwapNullAndEmpty(linkpath4),
            linkpath5 = SwapNullAndEmpty(linkpath5),
            linkpath6 = SwapNullAndEmpty(linkpath6),
            linkname1 = SwapNullAndEmpty(linkname1),
            linkname2 = SwapNullAndEmpty(linkname2),
            linkname3 = SwapNullAndEmpty(linkname3),
            linkname4 = SwapNullAndEmpty(linkname4),
            linkname5 = SwapNullAndEmpty(linkname5),
            linkname6 = SwapNullAndEmpty(linkname6),
        };

        /// <summary>
        /// null と 空文字列を入れ替え
        /// </summary>
        private static string SwapNullAndEmpty(string value) => (value == null) ? "" : ((value.Length == 0) ? null : value);

        /// <summary>
        /// 更新対象の ID リスト
        /// </summary>
        [JsonProperty("idl")] public string[] idl { get; set; }
        /// <summary>
        /// グループ ID
        /// </summary>
        [JsonProperty("group_id")] public int? group_id { get; set; }

        /// <summary>
        /// null → 変更しない
        /// </summary>
        [JsonProperty("group_only")] public bool? group_only { get; set; }
        /// <summary>
        /// null → 変更しない
        /// </summary>
        [JsonProperty("f_individual")] public bool? f_individual { get; set; }
        /// <summary>
        /// null → 変更しない
        /// </summary>
        [JsonProperty("f_handin")] public bool? f_handin { get; set; }
        /// <summary>
        /// null → 変更しない
        /// </summary>
        [JsonProperty("f_secret")] public bool? f_secret { get; set; }

        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("userdate")] public string userdate { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("limitdate")] public string limitdate { get; set; }

        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("createuserid")] public string createuserid { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("adminuserid")] public string adminuserid { get; set; }

        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("campanyname")] public string campanyname { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("title")] public string title { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("project")] public string project { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("usercustomitem1")] public string usercustomitem1 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("usercustomitem2")] public string usercustomitem2 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("usercustomitem3")] public string usercustomitem3 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("usercustomitem4")] public string usercustomitem4 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("usercustomitem5")] public string usercustomitem5 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("memo")] public string memo { get; set; }

        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkpath1")] public string linkpath1 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkpath2")] public string linkpath2 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkpath3")] public string linkpath3 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkpath4")] public string linkpath4 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkpath5")] public string linkpath5 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkpath6")] public string linkpath6 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkname1")] public string linkname1 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkname2")] public string linkname2 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkname3")] public string linkname3 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkname4")] public string linkname4 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkname5")] public string linkname5 { get; set; }
        /// <summary>
        /// null → データ削除。空文字列 → 変更しない。
        /// </summary>
        [JsonProperty("linkname6")] public string linkname6 { get; set; }
    }
}

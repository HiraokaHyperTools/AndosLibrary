using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace AndosLibrary
{
    public partial class Andos
    {
        /// <summary>
        /// ログイン情報を保持
        /// </summary>
		private readonly CookieContainer _cookies = new CookieContainer();

        /// <summary>
        /// 基本 URL
        /// </summary>
        private readonly Uri _baseUri;

        public Andos(Uri baseUri)
        {
            _baseUri = baseUri;
        }

        /// <summary>
        /// ログインが成功した場合、ログイン ID を保持
        /// </summary>
		public String uname { get; set; }

        /// <summary>
        /// 検索で返すマックスの検索結果件数
        /// </summary>
        /// <remarks>
        /// - `null` = 既定値。Web 版設定に従う
        /// - `0` = ∞
        /// - `1` ～ `2147483648` = その件数を上限にする
        /// </remarks>
        public int? limitCount { get; set; }


        /// <summary>
        /// ログインします
        /// </summary>
        public void Login(String U, String P, bool apiOnly = true, String apiApp = "AndosLibrary")
        {
            var query = new
            {
                a = "login",
                m = 1,
                APIOnly = apiOnly,
                APIApp = apiApp,
            };
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_baseUri, "ando.php?" + Helper.ObjectToQueryString(query)));
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            ApplyCommon(request);

            using (var inStream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(inStream, new UTF8Encoding(false)))
                {
                    writer.Write(Helper.ObjectToQueryString(
                        new
                        {
                            ID = U,
                            Password = P,
                        }
                    ));
                }
            }
            String json;
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {
                    json = Helper.ExtractMyddAndo(new StreamReader(outStream, Encoding.UTF8).ReadToEnd());
                }
            }
            var obj = Helper.ReadJson<Login>(json);
            if (obj.f != "search")
            {
                throw new Exception("ログイン失敗!");
            }
            uname = obj.uname;
        }

        /// <summary>
        /// ログアウトします
        /// </summary>
        public void Logout()
        {
            var query = new
            {
                a = "logout",
            };
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_baseUri, "core.php?" + Helper.ObjectToQueryString(query)));
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            ApplyCommon(request);

            using (var inStream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(inStream, new UTF8Encoding(false)))
                {
                }
            }
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {
                }
            }
        }

        private void ApplyCommon(HttpWebRequest request)
        {
            request.UserAgent = $"AndosLibrary/0.1.0";
            request.Timeout = 180000;
        }

        /// <summary>
        /// 書類をキーワード検索します
        /// </summary>
        /// <param name="no1">キーワード検索のキーワード</param>
        public Search SearchDoc(String no1)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(_baseUri + "ando.php?a=search&m=1&FindWay=kw&KeywordSearch=" + Uri.EscapeDataString(no1));
            request.CookieContainer = _cookies;
            request.Method = "GET";
            ApplyCommon(request);

            String json;
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {
                    json = Helper.ExtractMyddAndo(new StreamReader(outStream, Encoding.UTF8).ReadToEnd());
                }
            }
            var obj = Helper.ReadJson<Search>(json);
            if (obj.f != "thumbnail")
            {
                throw new Exception("検索失敗!");
            }
            return obj;
        }

        /// <summary>
        /// 書類をキーワード検索します。ファイルパス `fp2` あり
        /// </summary>
        /// <param name="no1">キーワード検索のキーワード</param>
        public Search SearchDocWithfp2(String no1)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(_baseUri + "ando.php?a=search&m=1&FindWay=kw&KeywordSearch=" + Uri.EscapeDataString(no1) + "&GrabDDLink=1");
            request.CookieContainer = _cookies;
            request.Method = "GET";
            ApplyCommon(request);

            String json;
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {
                    json = Helper.ExtractMyddAndo(new StreamReader(outStream, Encoding.UTF8).ReadToEnd());
                }
            }
            var obj = Helper.ReadJson<Search>(json);
            if (obj.f != "thumbnail")
            {
                throw new Exception("検索失敗!");
            }
            return obj;
        }

        /// <summary>
        /// 書類をカスタム検索します
        /// </summary>
        public Search SearchCustom(Wcqb wcq)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(
                string.Concat(
                    _baseUri,
                    "ando.php?a=search&m=1&FindWay=c&q=" + wcq.ToString(),
                    (limitCount != null) ? "&limitCount=" + limitCount : ""
                    )
                );

            request.CookieContainer = _cookies;
            request.Method = "GET";
            ApplyCommon(request);

            String json;
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {
                    json = Helper.ExtractMyddAndo(new StreamReader(outStream, Encoding.UTF8).ReadToEnd());
                }
            }
            var obj = Helper.ReadJson<Search>(json);
            if (obj.f != "thumbnail")
            {
                throw new Exception("検索失敗!");
            }
            return obj;
        }

        /// <summary>
        /// 書類をカスタム検索します。ファイルパス `fp2` あり
        /// </summary>
        public Search SearchCustomWithfp2(Wcqb wcq)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(
                string.Concat(
                    _baseUri,
                    "ando.php?a=search&m=1&FindWay=c&q=" + wcq.ToString(),
                    (limitCount != null) ? "&limitCount=" + limitCount : "",
                    "&GrabDDLink=1"
                    )
                );

            request.CookieContainer = _cookies;
            request.Method = "GET";
            ApplyCommon(request);

            String json;
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {
                    json = Helper.ExtractMyddAndo(new StreamReader(outStream, Encoding.UTF8).ReadToEnd());
                }
            }
            var obj = Helper.ReadJson<Search>(json);
            if (obj.f != "thumbnail")
            {
                throw new Exception("検索失敗!");
            }
            return obj;
        }

        /// <summary>
        /// グループ・担当者リストを取得します
        /// </summary>
        public LGU GetGU()
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(_baseUri + "js.php?a=lgu");
            request.CookieContainer = _cookies;
            request.Method = "GET";
            ApplyCommon(request);

            using (var resp = request.GetResponse())
            {
                var obj = Helper.ReadJson<LGU>(resp.GetResponseStream());
                return obj;
            }
        }

        /// <summary>
        /// ファイルを差し替えます
        /// </summary>
        /// <param name="keepOwner">true の場合、書類の登録者を変更しない。false の場合、書類の登録者は、ログイン者へ変更する。</param>
        /// <param name="newmt">true の場合、更新日時を更新します。</param>
        public String ReplaceJs(string id, MPFDGen form, bool keepOwner, bool alwaysrename = false, bool newmt = false)
        {
            var query = new
            {
                a = "replace_js",
                id = id,
                newmt = (newmt ? 1 : 0),
                alwaysrename = (alwaysrename ? 1 : 0),
                keepowner = (keepOwner ? 1 : 0),
            };
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_baseUri, "core.php?" + Helper.ObjectToQueryString(query)));
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + form.Boundary + "";
            request.AllowWriteStreamBuffering = true;
            request.SendChunked = true;
            ApplyCommon(request);

            using (var inStream = request.GetRequestStream())
            {
                form.WriteTo(inStream);
            }
            String body;
            using (var resp = request.GetResponse())
            {
                int mrRes;
                if (!int.TryParse("" + resp.Headers["X-mrRes"], out mrRes))
                {
                    mrRes = 0;
                }
                if (mrRes >= 2)
                {
                    throw new Exception("ファイルの差し替えに失敗しました(" + mrRes + ")");
                }
                using (var outStream = resp.GetResponseStream())
                {
                    body = new StreamReader(outStream, Encoding.UTF8).ReadToEnd();
                }
            }
            return Regex.Replace(body, "\\<[^\\>]+\\>", " ");
        }

        /// <summary>
        /// ファイルを差し替えます
        /// </summary>
        /// <param name="keepOwner">true の場合、書類の登録者を変更しない。false の場合、書類の登録者は、ログイン者へ変更する。</param>
        /// <param name="newmt">true の場合、更新日時を更新します。</param>
        public String ReplaceJs(string id, string filePath, bool keepOwner, bool alwaysrename = false, bool newmt = false)
        {
            var form = new Andos.MPFDGen();
            form.FileList.Add(new Andos.MPFDGen.FileEntry { FilePath = filePath, Name = "contents", });
            return ReplaceJs(id, form, keepOwner, alwaysrename, newmt);
        }

        /// <summary>
        /// 書類 ID を作成します
        /// </summary>
		public AddJsRes AddJs(String postBody)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_baseUri, "js.php?a=anr"));
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=\"utf-8\";";
            ApplyCommon(request);

            using (var inStream = request.GetRequestStream())
            {
                new BinaryWriter(inStream).Write(Encoding.UTF8.GetBytes(postBody));
            }
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {
                    var obj = Helper.ReadJson<AddJsRes>(resp.GetResponseStream());
                    return obj;
                }
            }
        }

        /// <summary>
        /// ファイルを差し替えします
        /// </summary>
        public void ReplaceFile2(string id, string fp)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_baseUri, "js.php?a=rf2&id=" + Uri.EscapeDataString(id)));
            var contentLength = new FileInfo(fp).Length;
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/octet-stream";
            request.ContentLength = contentLength;
            request.Headers["X-FILE-NAME"] = Uri.EscapeDataString(Path.GetFileName(fp));
            request.Headers["X-FILE-SIZE"] = contentLength.ToString();
            ApplyCommon(request);

            using (var inStream = request.GetRequestStream())
            {
                using (var inFile = File.OpenRead(fp))
                {
                    byte[] bin = new byte[4000];
                    while (true)
                    {
                        int r = inFile.Read(bin, 0, bin.Length);
                        if (r < 1)
                        {
                            break;
                        }
                        inStream.Write(bin, 0, r);
                    }
                }
            }
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {

                }
            }
        }

        /// <summary>
        /// ファイルを差し替えします
        /// </summary>
		public void ReplaceFile3(string id, string fp, bool alwaysrename)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_baseUri, "js.php?a=rf2&id=" + Uri.EscapeDataString(id))
                + (alwaysrename ? "&alwaysrename=1" : "")
                );
            var contentLength = new FileInfo(fp).Length;
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/octet-stream";
            request.ContentLength = contentLength;
            request.Headers["X-FILE-NAME"] = Uri.EscapeDataString(Path.GetFileName(fp));
            request.Headers["X-FILE-SIZE"] = contentLength.ToString();
            ApplyCommon(request);

            using (var inStream = request.GetRequestStream())
            {
                using (var inFile = File.OpenRead(fp))
                {
                    byte[] bin = new byte[4000];
                    while (true)
                    {
                        int r = inFile.Read(bin, 0, bin.Length);
                        if (r < 1)
                        {
                            break;
                        }
                        inStream.Write(bin, 0, r);
                    }
                }
            }
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {

                }
            }
        }

        /// <summary>
        /// OC2 関係
        /// </summary>
		public void SetDocHook(string id, string[] idto, string yk, string ys)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(_baseUri + "js.php?a=sdh");
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8;";
            ApplyCommon(request);
            using (var inStream = request.GetRequestStream())
            {
                Helper.WriteJson(inStream, new SetDocHookReq
                {
                    id = id,
                    idto = idto,
                    yk = yk,
                    ys = ys,
                });
            }
            using (var resp = request.GetResponse())
            {

            }
        }

        private class SetDocHookReq
        {
            [JsonProperty("id")] public String id { get; set; }
            [JsonProperty("idto")] public String[] idto { get; set; }
            [JsonProperty("yk")] public String yk { get; set; }
            [JsonProperty("ys")] public String ys { get; set; }
        }

        /// <summary>
        /// 書類を削除します
        /// </summary>
        /// <param name="force">true の場合、「なんちゃってレコードロック」を無視します</param>
		public void Delete(string id, bool force)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(_baseUri + "core.php?a=deleterecord_js&id=" + Uri.EscapeDataString(id) + "&force=" + (force ? 1 : 0));
            request.CookieContainer = _cookies;
            request.Method = "GET";
            ApplyCommon(request);

            using (var resp = request.GetResponse())
            {
                int mrRes;
                if (!int.TryParse("" + resp.Headers["X-mrRes"], out mrRes))
                {
                    mrRes = 0;
                }
                if (mrRes >= 2)
                {
                    throw new Exception("削除に失敗しました(" + mrRes + ")");
                }
            }
        }

        /// <summary>
        /// TECHS 連携の検索をします
        /// </summary>
        public IDictionary<string, object>[] SearchData(string 書類, string 製番)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(_baseUri + "api/techs/searchData?書類=" + Uri.EscapeDataString(書類) + "&製番=" + Uri.EscapeDataString(製番));
            request.CookieContainer = _cookies;
            request.Method = "GET";
            ApplyCommon(request);

            using (var response = request.GetResponse())
            {
                if (("" + response.Headers["X-ErrorCode"]) != "")
                {
                    throw new Exception(""
                        + "ErrorCode: " + response.Headers["X-ErrorCode"] + "\n"
                        + "ErrorInfo: " + response.Headers["X-ErrorInfo"] + "\n"
                        );
                }
                using (var outStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(outStream))
                    {
                        var json = reader.ReadToEnd();
                        return Helper.ReadJson<IDictionary<string, object>[]>(json);
                    }
                }
            }
        }

        /// <summary>
        /// 書類の「ファイルロック」を操作します
        /// </summary>
        /// <param name="ifset">true の場合、ロックを設定します。false の場合、ロックを解除します。</param>
        /// <param name="force">true の場合、「なんちゃってレコードロック」を無視します</param>
		public SetLockRes LockFileJS(string id, bool ifset, bool force)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_baseUri, "core.php?a=filelock_js&id=" + id + "&doSet=" + (ifset ? 1 : 0) + (force ? "&force=1" : "")));
            request.CookieContainer = _cookies;
            request.Method = "GET";
            ApplyCommon(request);

            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {
                    int mrRes;
                    int isLocked;
                    return new SetLockRes
                    {
                        mrRes = (int.TryParse(resp.Headers["X-mrRes"], out mrRes) ? mrRes : -1),
                        message = new StreamReader(outStream, Encoding.UTF8).ReadToEnd(),
                        isLocked = (int.TryParse(resp.Headers["X-isLocked"], out isLocked) ? isLocked : -1),
                    };
                }
            }
        }

        public class SetLockRes
        {
            public int mrRes { get; set; }
            public String message { get; set; }
            public int isLocked { get; set; }
        }

        /// <summary>
        /// 書類属性を修正します
        /// </summary>
        /// <param name="id">書類 ID</param>
        /// <param name="fc">`campanyname` 列。null を与えた場合は変更しません。</param>
        /// <param name="f1">`title` 列。null を与えた場合は変更しません。</param>
        /// <param name="f2">`project` 列。null を与えた場合は変更しません。</param>
        /// <param name="f3">`usercustomitem1` 列。null を与えた場合は変更しません。</param>
        /// <param name="f4">`usercustomitem2` 列。null を与えた場合は変更しません。</param>
        /// <param name="f5">`usercustomitem3` 列。null を与えた場合は変更しません。</param>
        /// <param name="f6">`usercustomitem4` 列。null を与えた場合は変更しません。</param>
        /// <param name="f7">`usercustomitem5` 列。null を与えた場合は変更しません。</param>
        /// <param name="fm">`memo` 列。null を与えた場合は変更しません。</param>
        /// <param name="fd">`userdate` 列。null を与えた場合は変更しません。</param>
        /// <param name="createuserid">`createuserid` 列。null を与えた場合は変更しません。</param>
        /// <param name="force">true の場合、「なんちゃってレコードロック」を無視します</param>
        public void Edit10(string id,
            string fc = null,
            string f1 = null,
            string f2 = null,
            string f3 = null,
            string f4 = null,
            string f5 = null,
            string f6 = null,
            string f7 = null,
            string fm = null,
            string fd = null,
            string createuserid = null,
            bool force = false
        )
        {
            int magic = new Random().Next();
            var query = new
            {
                a = "modifyrecordset3_js",
                magic = magic,
                force = (force ? 1 : 0),
            };
            var request = (HttpWebRequest)HttpWebRequest.Create(_baseUri + "core.php?" + Helper.ObjectToQueryString(query));
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=utf-8;";
            ApplyCommon(request);
            var dict = new Dictionary<string, string>();
            {
                dict["alid"] = $"{id}=1";
                dict["magic"] = "" + magic;
                if (fc != null)
                {
                    dict["Vcampanyname"] = fc;
                }
                if (f1 != null)
                {
                    dict["Vtitle"] = f1;
                }
                if (f2 != null)
                {
                    dict["Vproject"] = f2;
                }
                if (f3 != null)
                {
                    dict["Vusercustomitem1"] = (f3);
                }
                if (f4 != null)
                {
                    dict["Vusercustomitem2"] = (f4);
                }
                if (f5 != null)
                {
                    dict["Vusercustomitem3"] = (f5);
                }
                if (f6 != null)
                {
                    dict["Vusercustomitem4"] = (f6);
                }
                if (f7 != null)
                {
                    dict["Vusercustomitem5"] = (f7);
                }
                if (fm != null)
                {
                    dict["Vmemo"] = (fm);
                }
                if (fd != null)
                {
                    dict["DateUser"] = (fd);
                }
                if (createuserid != null)
                {
                    dict["Vcreateuserid"] = (createuserid);
                }
            }
            using (var inStream = request.GetRequestStream())
            {
                using (StreamWriter wr = new StreamWriter(inStream))
                {
                    wr.Write(Helper.DictToQueryString(dict));
                }
            }
            using (var resp = request.GetResponse())
            {
                int mrRes;
                if (!int.TryParse("" + resp.Headers["X-mrRes"], out mrRes))
                {
                    mrRes = 0;
                }
                if (mrRes >= 2)
                {
                    throw new Exception("修正に失敗しました(" + mrRes + ")");
                }
            }
        }

        /// <summary>
        /// 書類情報を修正します。適切に利用しないと書類情報が損失します。詳細は Edit19 をどうぞ。
        /// </summary>
        /// <param name="force">true の場合、「なんちゃってレコードロック」を無視します</param>
        /// <returns></returns>
        public Edit19Res Edit19(
            Edit19 edit19,
            bool force
        )
        {
            int magic = new Random().Next();
            var request = (HttpWebRequest)HttpWebRequest.Create(_baseUri + "js.php?a=modifyrecordset4&force=" + (force ? 1 : 0));
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8;";
            ApplyCommon(request);
            using (var os = request.GetRequestStream())
            {
                Helper.WriteJson(os, edit19);
            }
            using (var resp = request.GetResponse())
            {
                int mrRes;
                if (!int.TryParse("" + resp.Headers["X-mrRes"], out mrRes))
                {
                    mrRes = 0;
                }
                if (mrRes >= 2)
                {
                    throw new Exception("修正に失敗しました(" + mrRes + ")");
                }
                using (var outStream = resp.GetResponseStream())
                {
                    var obj = Helper.ReadJson<Edit19Res>(resp.GetResponseStream());
                    return obj;
                }
            }
        }

        /// <summary>
        /// 書類を複製します
        /// </summary>
        public Copy1Res Copy1(string id)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_baseUri, "js.php?a=cpy1"));
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=\"utf-8\";";
            ApplyCommon(request);

            using (var inStream = request.GetRequestStream())
            {
                new BinaryWriter(inStream).Write(Encoding.UTF8.GetBytes("id=" + Uri.EscapeDataString(id)));
            }
            using (var resp = request.GetResponse())
            {
                using (var outStream = resp.GetResponseStream())
                {
                    var obj = Helper.ReadJson<Copy1Res>(resp.GetResponseStream());
                    return obj;
                }
            }
        }


        public T PostFormDataAndGetJson<T>(string relativeUrl, MPFDGen form)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(new Uri(_baseUri, relativeUrl));
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "multipart/form-data; boundary=" + form.Boundary + "";
            ApplyCommon(request);

            using (var os = request.GetRequestStream())
            {
                form.WriteTo(os);
            }
            String body;
            using (var response = request.GetResponse())
            {
                using (var si = response.GetResponseStream())
                {
                    body = new StreamReader(si, Encoding.UTF8).ReadToEnd();
                }
            }
            return Helper.ReadJson<T>(
                new MemoryStream(Encoding.UTF8.GetBytes(body), false)
                );
        }

        /// <summary>
        /// 新しい版を作成します。データはこちらが指示したものを挿入します。
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public NewDocVer3Res NewDocVer3(MPFDGen form)
        {
            return PostFormDataAndGetJson<NewDocVer3Res>("js.php?a=newdocver3", form);
        }

        /// <summary>
        /// 書類の更新日付を更新します
        /// </summary>
        /// <param name="force">true の場合、「なんちゃってレコードロック」を無視します</param>
        /// <returns></returns>
        public Edit19Res UpdateRecordTime(
            string id,
            bool force
        )
        {
            int magic = new Random().Next();
            var request = (HttpWebRequest)HttpWebRequest.Create(_baseUri + "js.php?a=modifyrecordset4&force=" + (force ? 1 : 0));
            request.CookieContainer = _cookies;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8;";
            ApplyCommon(request);
            using (var inStream = request.GetRequestStream())
            {
                var edit19 = new Edit19IdlOnly
                {
                    idl = new string[] { id },
                };
                Helper.WriteJson(inStream, edit19);
            }
            using (var resp = request.GetResponse())
            {
                int mrRes;
                if (!int.TryParse("" + resp.Headers["X-mrRes"], out mrRes))
                {
                    mrRes = 0;
                }
                if (mrRes >= 2)
                {
                    throw new Exception("修正に失敗しました(" + mrRes + ")");
                }
                using (var outStream = resp.GetResponseStream())
                {
                    var obj = Helper.ReadJson<Edit19Res>(resp.GetResponseStream());
                    return obj;
                }
            }
        }

        internal class Helper
        {
            internal static string ObjectToQueryString<T>(T obj)
            {
                var type = typeof(T);
                var text = "";
                foreach (var prop in type.GetProperties())
                {
                    if (text.Length != 0)
                    {
                        text += "&";
                    }
                    text += Uri.EscapeDataString(prop.Name) + "=" + Uri.EscapeDataString("" + prop.GetValue(obj, new object[0]));
                }
                return text;
            }

            internal static string DictToQueryString(IDictionary<string, string> dict)
            {
                var text = "";
                foreach (var pair in dict)
                {
                    if (text.Length != 0)
                    {
                        text += "&";
                    }
                    text += Uri.EscapeDataString(pair.Key) + "=" + Uri.EscapeDataString("" + pair.Value);
                }
                return text;
            }

            internal static string ExtractMyddAndo(string body)
            {
                int p1 = body.IndexOf("<mydd:ando>");
                int p2 = body.IndexOf("</mydd:ando>", p1);
                if (p1 < 0 || p2 < 0)
                {
                    throw new Exception();
                }
                return body.Substring(p1 + 11, p2 - p1 - 11);
            }

            internal static T ReadJson<T>(string json)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }

            internal static T ReadJson<T>(Stream stream)
            {
                return JsonSerializer.CreateDefault().Deserialize<T>(new JsonTextReader(new StreamReader(stream)));
            }

            internal static void WriteJson(Stream stream, object value)
            {
                using (var writer = new StreamWriter(stream))
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    JsonSerializer.CreateDefault().Serialize(jsonWriter, value);
                }
            }
        }
    }
}

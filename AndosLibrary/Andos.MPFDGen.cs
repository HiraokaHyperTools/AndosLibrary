using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AndosLibrary
{
    partial class Andos
    {
        /// <summary>
        /// MPFDGen = multipart form data generator
        /// </summary>
        /// <remarks>
        /// 参考: http://d.hatena.ne.jp/satox/20110726/1311665904
        /// </remarks>
        public class MPFDGen
        {
            public String Boundary { get; set; } = Guid.NewGuid().ToString("N");

            public class ValueEntry
            {
                public String Body { get; set; }
                public String Name { get; set; } = "contents";
            }
            public List<ValueEntry> ValueList { get; set; } = new List<ValueEntry>();

            public class FileEntry
            {
                public String Name { get; set; } = "contents";
                public String Boundary { get; set; } = Guid.NewGuid().ToString("N");

                public String FilePath { get; set; }
                public String Mime { get; set; } = "application/octet-stream";
            }
            public List<FileEntry> FileList { get; set; } = new List<FileEntry>();

            public void WriteTo(Stream outStream)
            {
                using (StreamWriter writer = new StreamWriter(outStream, new UTF8Encoding(false)))
                {
                    foreach (ValueEntry value in ValueList)
                    {
                        writer.WriteLine("--" + Boundary);
                        writer.WriteLine("Content-Disposition: form-data; name=\"" + value.Name + "\"");
                        writer.WriteLine();
                        writer.WriteLine(value.Body);
                    }
                    foreach (FileEntry file in FileList)
                    {
                        writer.WriteLine("--" + Boundary);
                        writer.WriteLine("Content-Disposition: form-data; name=\"" + file.Name + "\"; filename=\"" + (Path.GetFileName(file.FilePath)) + "\"");
                        writer.WriteLine("Content-Type: " + file.Mime);
                        writer.WriteLine("Content-Transfer-Encoding: binary");
                        writer.WriteLine();
                        writer.Flush();
                        using (var inStream = File.OpenRead(file.FilePath))
                        {
                            byte[] bin = new byte[4000];
                            while (true)
                            {
                                int r = inStream.Read(bin, 0, bin.Length);
                                if (r < 1)
                                {
                                    break;
                                }
                                outStream.Write(bin, 0, r);
                            }
                        }
                        writer.WriteLine(); // term line
                    }
                    writer.WriteLine("--" + Boundary + "--");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AndosLibrary
{
    public sealed class Wcqb
    {
        private readonly MemoryStream _stream = new MemoryStream();

        public Wcqb T()
        {
            _stream.WriteByte(0xCB);
            return this;
        }
        public Wcqb F()
        {
            _stream.WriteByte(0xCC);
            return this;
        }
        public Wcqb LAnd()
        {
            _stream.WriteByte(0xC0);
            return this;
        }
        public Wcqb LOr()
        {
            _stream.WriteByte(0xC1);
            return this;
        }

        public Wcqb group_id()
        {
            return Fi(50);
        }
        public Wcqb title()
        {
            return Fi(5);
        }
        public Wcqb project()
        {
            return Fi(6);
        }
        public Wcqb campanyname()
        {
            return Fi(8);
        }
        public Wcqb memo()
        {
            return Fi(9);
        }
        public Wcqb usercustomitem1()
        {
            return Fi(17);
        }
        public Wcqb usercustomitem2()
        {
            return Fi(18);
        }
        public Wcqb usercustomitem3()
        {
            return Fi(19);
        }
        public Wcqb usercustomitem4()
        {
            return Fi(20);
        }
        public Wcqb usercustomitem5()
        {
            return Fi(21);
        }

        public Wcqb linkpath1()
        {
            return Fi(29);
        }
        public Wcqb linkpath2()
        {
            return Fi(30);
        }
        public Wcqb linkpath3()
        {
            return Fi(31);
        }
        public Wcqb linkpath4()
        {
            return Fi(32);
        }
        public Wcqb linkpath5()
        {
            return Fi(33);
        }
        public Wcqb linkname1()
        {
            return Fi(23);
        }
        public Wcqb linkname2()
        {
            return Fi(24);
        }
        public Wcqb linkname3()
        {
            return Fi(25);
        }
        public Wcqb linkname4()
        {
            return Fi(26);
        }
        public Wcqb linkname5()
        {
            return Fi(27);
        }
        public Wcqb exeword()
        {
            return Fi(52);
        }


        public Wcqb id()
        {
            return Fi(0);
        }

        public Wcqb Iis()
        {
            _stream.WriteByte(0xD5);
            return this;
        }
        public Wcqb Dis()
        {
            _stream.WriteByte(0xC6);
            return this;
        }
        public Wcqb Sstartswith()
        {
            _stream.WriteByte(0xC3);
            return this;
        }
        public Wcqb Scontain()
        {
            _stream.WriteByte(0xC2);
            return this;
        }
        public Wcqb Sis()
        {
            _stream.WriteByte(0xC5);
            return this;
        }
        public Wcqb Skw()
        {
            _stream.WriteByte(0xCD);
            return this;
        }


        public Wcqb Fi(int i)
        {
            _stream.WriteByte((byte)(0x80 | i));
            return this;
        }
        public Wcqb WriteCode(byte c)
        {
            _stream.WriteByte(c);
            return this;
        }

        public Wcqb Str(String s)
        {
            byte[] bin = Encoding.UTF8.GetBytes(s);
            if (bin.Length <= 127)
            {
                _stream.WriteByte((byte)bin.Length);
                _stream.Write(bin, 0, bin.Length);
            }
            else if (!AllowLongStr)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            else
            {
                _stream.WriteByte((byte)0xDB);
                _stream.Write(BitConverter.GetBytes((uint)bin.Length), 0, 4);
                _stream.Write(bin, 0, bin.Length);
            }

            return this;
        }

        public sealed class Operator
        {
            public Operator(string display, byte code, int numArgs)
            {
                Display = display;
                Code = code;
                NumArgs = numArgs;
            }

            public string Display { get; }
            public byte Code { get; }
            public int NumArgs { get; }

            public override string ToString() => Display;
        }

        public static Operator[] GetOperators() => new Operator[] {
            new Operator("And", 0xC0, 2),
            new Operator("Or", 0xC1, 2),
            new Operator("d,is", 0xC6, 2),
            new Operator("d,less", 0xC8, 2),
            new Operator("d,greater", 0xC9, 2),
            new Operator("d,isnot", 0xCA, 2),
            new Operator("d,geq", 0xD3, 2),
            new Operator("d,leq", 0xD4, 2),
            new Operator("i,is", 0xD5, 2),
            new Operator("i,isnot", 0xD6, 2),
            new Operator("s,contain", 0xC2, 2),
            new Operator("s,startswith", 0xC3, 2),
            new Operator("s,endswith", 0xC4, 2),
            new Operator("s,is", 0xC5, 2),
            new Operator("s,geq", 0xCE, 2),
            new Operator("s,leq", 0xCF, 2),
            new Operator("s,isnot", 0xD0, 2),
            new Operator("s,notcontained", 0xD2, 2),
            new Operator("s,equals", 0xDD, 2),



            new Operator("d,between", 0xC7, 3),
            new Operator("f,between", 0xDA, 3),
            new Operator("b,is", 0xD7, 1),
            new Operator("b,isnot", 0xD8, 1),
            new Operator("s,kw", 0xCD, 1),
            new Operator("s,isempty", 0xD1, 1),
            new Operator("s,fts1", 0xD9, 1),
            new Operator("s,imagesearch", 0xDC, 1),

        };

        public sealed class Field
        {
            public Field(int index, string display)
            {
                Index = index;
                Display = display;
            }

            public int Index { get; }
            public string Display { get; }

            public override string ToString() => Display;
        }

        public static Field[] GetFields() => new Field[] {
            new Field(0, "id"),
            new Field(2, "filepath"),
            new Field(5, "title"),
            new Field(6, "project"),
            new Field(8, "campanyname"),
            new Field(9, "memo"),
            new Field(10, "f_secret"),
            new Field(13, "createuserid"),
            new Field(14, "adminuserid"),
            new Field(15, "createdate"),
            new Field(16, "modifydate"),
            new Field(17, "usercustomitem1"),
            new Field(18, "usercustomitem2"),
            new Field(19, "usercustomitem3"),
            new Field(20, "usercustomitem4"),
            new Field(21, "usercustomitem5"),
            new Field(23, "linkname1"),
            new Field(24, "linkname2"),
            new Field(25, "linkname3"),
            new Field(26, "linkname4"),
            new Field(27, "linkname5"),
            new Field(29, "linkpath1"),
            new Field(30, "linkpath2"),
            new Field(31, "linkpath3"),
            new Field(32, "linkpath4"),
            new Field(33, "linkpath5"),
            new Field(35, "modifytime"),
            new Field(43, "limitdate"),
            new Field(44, "f_handin"),
            new Field(45, "f_individual"),
            new Field(47, "userdate"),
            new Field(50, "group_id"),
            new Field(51, "group_only"),
            new Field(52, "exeword"),
            new Field(53, "lock"),

        };

        public override string ToString()
        {
            return Uri.EscapeDataString(Convert.ToBase64String((_stream.ToArray())).Replace("/", "_").Replace("+", "-"));
        }

        public bool AllowLongStr { get; set; } = false;
    }
}

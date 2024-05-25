using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ADBCommandSDK
{
    public class aapt : ConsoleCommand
    {
        public aapt() : base("aapt.exe")
        {
        }
        public readDumpAapt dump(string arg, string command)
        {
            return new readDumpAapt(base.ReadToEnd("dump " + arg + " " + command).Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
        }

    }
    public class readDumpAapt
    {
        string[] StrDump;
        public readDumpAapt(string[] str)
        {
            StrDump = str;
        }
        public string Find(string instr)
        {
            for (int i = 0; i < StrDump.LongLength; i++)
                if (StrDump[i].Contains(instr))
                {
                    if (!StrDump[i].Contains("=")) return methodsTools.getBetween(StrDump[i], "'", "'", 0).SubString;

                }
            return null;
        }

        public override string ToString()
        {

            string strtemp = StrDump[0];
            for (int i = 1; i < StrDump.LongLength; i++) strtemp += "\n" + StrDump[i];
            return strtemp;
        }
    }

}

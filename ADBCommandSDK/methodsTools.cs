using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADBCommandSDK
{
    public static class methodsTools
    {
        public static SubStringEndSubString getBetween(string strSource, string strStart, string strEnd, int indexStart)
        {
            SubStringEndSubString SSE = new SubStringEndSubString();
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, indexStart) + strStart.Length;
                SSE.EndSubString = End = strSource.IndexOf(strEnd, Start);
                SSE.SubString = strSource.Substring(Start, End - Start);
            }
            return SSE;
        }
        public static SubStringEndSubString getBetween(string strSource, string strEnd, int indexStart)
        {
            SubStringEndSubString SSE = new SubStringEndSubString();
            int End;
            if (strSource.Contains(strEnd))
            {
                SSE.EndSubString = End = strSource.IndexOf(strEnd, 0);
                SSE.SubString = strSource.Substring(0, End);
            }
            return SSE;
        }

    }
    public class SubStringEndSubString { public string SubString; public int EndSubString; }

}

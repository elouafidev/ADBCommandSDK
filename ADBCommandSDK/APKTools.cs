using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADBCommandSDK
{
    public class Icon
    {
        public bool exists;
        public int quality;
    }

    public class Apk
    {
        public Apk(string pathAPk)
        {
            this.path = pathAPk;
            aapt aaptConsole = new aapt();

        }
        string path;
        string namepackage;
        int version;
        string sdkVersion;
        string targetSdkVersion;
        string usespermission;
        string applicationlabel;
        int[] densities;
        Icon[] applicationicon;
        string[] locales;

    }

}

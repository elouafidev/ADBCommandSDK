using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADBCommandSDK
{
    public class Su
    {
        private string version;
        private bool exists;

        internal Su(Device device)
        {
            if (device.State != DeviceState.DEVICE)
            {
                this.version = null;
                this.exists = false;
                return;
            }

            ConsoleCommand adb = new ConsoleCommand("adb");

                string line = adb.ReadToEnd(device, "shell su -v").Replace(System.Environment.NewLine,"");

                if (line.Contains("not found") || line.Contains("permission denied"))
                {
                    this.version = "-1";
                    this.exists = false;
                }
                else
                {
                    this.version = line;
                    this.exists = true;
                }
            
    }

        public bool Exists { get { return this.exists; } }

        /// <summary>
        /// Gets a value indicating the version of Su on the Android device
        /// </summary>
        public string Version { get { return this.version; } }

        public override string ToString()
        {
            if (exists)
                return " Is Rooted Version :" + this.version;
            else
                return " Is Not Rooted ";
        }
    }
}

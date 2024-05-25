using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADBCommandSDK
{
    public class Device
    {
        public Device()
        {
            this.id = "";
            this.module = "";
            this.State = DeviceState.UNKNOWN;
        }
        private string id;
        private DeviceState state;
        private string module;

        private Su su;
        public Su Su
            {
            get {
                if (su == null) su = new Su(this);
                return this.su;
            }
            }
        public override string ToString()
        {
            if (this.state == DeviceState.DEVICE)
            return  this.module + "\t" + this.State + "("+this.id+")";
            else return this.ID + "\t" + this.State;
        }
        public bool Existe
        {
            get
            {
                ConsoleCommand CScmd = new ConsoleCommand("adb");
                string strReturn;
                strReturn = CScmd.ReadToEnd("devices").Replace("List of devices attached", "").Replace("* daemon not running. starting it now on port 5037 *", "").Replace("* daemon started successfully *", "");
                if (strReturn.Length > 9)
                {
                    List<string> list = new List<string>(
                                   strReturn.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries)
                                   );
                    for (int i = 0; i < list.Count; i++)
                        if (list[i].Length > 1)
                            if (id == methodsTools.getBetween(list[i], "\t", 0).SubString)
                                return true ;
                }
                return false;
            }
        }
        public string ID { get { return this.id; } internal set { this.id = value; } }
        public string Module { get { return this.module; } internal set { this.module = value; } }
        public DeviceState State { get { return this.state; } internal set { this.state = value; } }
    }
}

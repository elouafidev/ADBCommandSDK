using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobileLibrary
{
    public class Device
    {
        public string ID;
        public string module;
        public string Serial
        {
            get{
                
                return "Null";
        
            }
        }
    public class Devices
        {
            public List<Device> List = new List<Device>();
            public void ReFlashList
            {

            }
        }
    }
}

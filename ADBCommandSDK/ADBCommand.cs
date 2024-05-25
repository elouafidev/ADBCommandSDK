using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ADBCommandSDK
{

    public class ADBCommand : ConsoleCommand
    {
        public ADBCommand() : base("adb") { }
        public ADBCommand(string NameProcess) : base(NameProcess) { }
        public Device DeviceSelected;
        public List<Device> ScanDevices()
        {
            List<Device> ListDevices = new List<Device>(); ;
            string strReturn;
            strReturn = ReadToEnd("devices").Replace("List of devices attached", "").Replace("* daemon not running. starting it now on port 5037 *", "").Replace("* daemon started successfully *", "");
            if (strReturn.Length > 9)
            {
                List<string> list = new List<string>(
                               strReturn.Split(new string[] { "\r\n" },
                               StringSplitOptions.RemoveEmptyEntries));

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Length > 1)
                    {
                        Device d = new Device();
                        string temp = list[i];
                        d.ID = methodsTools.getBetween(temp,"\t", 0).SubString;
                        switch (temp.Substring(temp.IndexOf('\t')+1))
                        {
                            case "offline":
                                d.State = DeviceState.OFFLINE;
                                break;
                            case "device":
                                d.State = DeviceState.DEVICE;
                                d.Module = ReadToEnd(d, "shell getprop ro.product.model").Replace(System.Environment.NewLine, "");
                                break;
                            case "recovery":
                                d.State = DeviceState.RECOVERY;
                                break;
                            case "fastboot":
                                d.State = DeviceState.FASTBOOT;
                                break;
                            case "sideload":
                                d.State = DeviceState.SIDELOAD;
                                break;
                            case "unauthorized":
                                d.State = DeviceState.UNAUTHORIZED;
                                break;
                            default:
                                d.State = DeviceState.UNKNOWN;
                                break;
                        }

                        ListDevices.Add(d);
                    }
                }
            }
            return ListDevices;
        }


        public void RunArgument(object Arguments)
        {
            base.Run(DeviceSelected, Arguments);
        }
        public string GetProp(Device d, string Prop)
        {
            return Shell(d, "getprop "+Prop).Replace(System.Environment.NewLine, "");
        }
        public string GetProp( string Prop)
        {
            return Shell("getprop " + Prop).Replace(System.Environment.NewLine, "");
        }
        public string GetBuildNumber(Device d) {
            return GetProp(d,"ro.build.display.id");
        }
        public string GetBuildNumber()
        {
            return GetProp("ro.build.display.id");
        }
        public string GetAndroidVersion()
        {
            return GetProp("ro.build.version.release");
        }
        public string GetAndroidVersion(Device d)
        {
            return GetProp(d,"ro.build.version.release");
        }
        public string GetBrand()
        {
            return GetProp("ro.product.brand");
        }
        public string GetBrand(Device d)
        {
            return GetProp(d,"ro.product.brand");
        }
        public string GetBoard()
        {
            return GetProp("ro.product.board");
        }
        public string GetBoard(Device d)
        {
            return GetProp(d, "ro.product.board");
        }
        public string GetCPU()
        {
            return GetProp( "ro.product.cpu.abi");
        }
        public string GetCPU(Device d)
        {
            return GetProp(d, "ro.product.cpu.abi");
        }
        public string GetCPU2()
        {
            return GetProp("ro.product.cpu.abi2");
        }
        public string GetCPU2(Device d)
        {
            return GetProp(d, "ro.product.cpu.abi2");
        }
        public string GetDeviceName()
        {
            return GetProp( "ro.product.device");
        }
        public string GetDeviceName(Device d)
        {
            return GetProp(d, "ro.product.device");
        }
        public string GetFingerPrint()
        {
            return GetProp("ro.build.fingerprint").Replace("/", "\n");
        }
        public string GetFingerPrint(Device d)
        {
            return GetProp(d, "ro.build.fingerprint").Replace("/","\n");
        }
        public string Shell(Device d,String CommandShell)
        {
            return ReadToEnd(d, "shell " + CommandShell);
        }
        public string Shell( String CommandShell)
        {
            return ReadToEnd(DeviceSelected, "shell " + CommandShell);
        }
        public string ShellSu(Device d, String CommandShell)
        {
            return ReadToEnd(d, "shell su -c \"" + CommandShell+"\"");
        }
        public string ShellSu( String CommandShell)
        {
            return ReadToEnd(DeviceSelected, "shell su -c \"" + CommandShell + "\"");
        }
        public string ShellSuOrNot(String CommandShell)
        {
            if (DeviceSelected.Su.Exists) return ShellSu(CommandShell);
            else return Shell(CommandShell);
        }

        /// <summary>
        /// Mode {Normal Mode,Download Mode,Recovery Mode,FastBoot Mode  }
        /// </summary>
        /// <param name="Mode"></param>
        public bool Reboot(String Mode)
        {
            string CommandReboot="";
            switch (Mode)
            {
                case "Normal Mode":
                    CommandReboot = "reboot";
                    break;
                case "Download Mode":
                    CommandReboot = "reboot download";
                    break;
                case "Recovery Mode":
                    CommandReboot = "reboot recovery";
                    break;
                case "FastBoot Mode":
                    CommandReboot = "reboot fastboot";
                    break;
            }
            if (ShellSuOrNot(CommandReboot).Replace(System.Environment.NewLine,"") == "" && CommandReboot !="") return true;
            return false;
        }

        public string InstallAPK(string FileApk,params StateInstallApk[] SIA)
            {
            string StateIA = "";
            for (int i = 0; i < SIA.Length; i++)
                if (SIA[i] == StateInstallApk.L) StateIA += "-l ";
                else if (SIA[i] == StateInstallApk.R) StateIA += "-r ";
                else if (SIA[i] == StateInstallApk.S) StateIA += "-s ";

            return ReadToEnd(DeviceSelected, "install " + StateIA + "\""+FileApk+"\"");
            }

    }

    
    
}

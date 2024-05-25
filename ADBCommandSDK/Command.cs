using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;


namespace MobileLibrary
{

    
    public class ConsoleCommand
    {
        public delegate void StringReceivedEventHandler(string e);
        public static event StringReceivedEventHandler WritingData;
        Process ProcessStart = new Process();
        public bool StateProcess;

        StringBuilder StriBuilder = new StringBuilder();
        private object syncGate = new object();
        public ConsoleCommand(String NameProcess)
        {
            ProcessStart.StartInfo.FileName = NameProcess;
            ProcessStart.StartInfo.CreateNoWindow = true;
            ProcessStart.StartInfo.ErrorDialog = false;
            ProcessStart.StartInfo.RedirectStandardError = true;
            ProcessStart.StartInfo.RedirectStandardOutput = true;
            ProcessStart.StartInfo.RedirectStandardInput = true;
            ProcessStart.StartInfo.UseShellExecute = false;
            ProcessStart.ErrorDataReceived += (sendingProcess, errorLine) => WritingData(errorLine.Data);
            ProcessStart.OutputDataReceived += (sendingProcess, dataLine) => WritingData(dataLine.Data);
            ProcessStart.EnableRaisingEvents = true;
            ProcessStart.Exited += new EventHandler(fp_Exited);
        }
        void fp_Exited(object sender, EventArgs e)
        {
            StateProcess = false;
        }
        
        public void RunArguments(object Line)
        {
            ProcessStart.StartInfo.Arguments = Line.ToString();
            StateProcess = ProcessStart.Start();
        }
        public void BeginReadline()
        {
            ProcessStart.BeginErrorReadLine();
            ProcessStart.BeginOutputReadLine();
            ProcessStart.WaitForExit();
            ProcessStart.CancelErrorRead();
            ProcessStart.CancelOutputRead();
        }
        public string StandardOutputReadLine
        {
            get { return ProcessStart.StandardOutput.ReadLine(); }
        }

        public string WriteCommandLine
        {
            set {
                try
                {
                    ProcessStart.StandardInput.WriteLine(value);
                }
                catch { WritingData("Console Not Running..!"); }
            }
        }
        private void StandardOutputRun()
        {
            int nextChar;
            while ((nextChar = ProcessStart.StandardOutput.Read()) >= 0)
                lock (syncGate)
                    StriBuilder.Append((char)nextChar);
        }
        private void StandardErrorRun()
        {
            int nextChar;
            while ((nextChar = ProcessStart.StandardError.Read()) >= 0)
                lock (syncGate)
                    StriBuilder.Append((char)nextChar);
        }

        public string ReadToEnd()
        {
            StriBuilder.Clear();
            new Thread(StandardOutputRun) { IsBackground = true }.Start();
            new Thread(StandardErrorRun) { IsBackground = true }.Start();
            ProcessStart.WaitForExit();
            return StriBuilder.ToString();
        }  
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.IO;


namespace ADBCommandSDK
{
    public class ConsoleCommand
    {
        public static event Action<string> WritingData;
        Process ProcessStart = new Process();
        public bool StateProcess = false;

        public string NameProcess
        {
            set { try
                { ProcessStart.StartInfo.FileName = value; }
                catch (Exception ex){ WritingData(ex.Message); } }
        }

        StringBuilder StriBuilder = new StringBuilder();

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
        
        public void Run(object Arguments)
        {
            ProcessStart.StartInfo.Arguments = Arguments.ToString();
            StateProcess = ProcessStart.Start();
        }
        public virtual void Run(Device deviceSelected,object Arguments)
        {
            Run("-s " + deviceSelected.ID + " " + Arguments.ToString());
        }
        /// <summary>
        /// if State Wait if True. Process wait to end.
        /// </summary>
        /// <param name="StateWait"></param>
        public void BeginReadLine(bool StateWait)
        {
            if (StateWait)
                BeginReadLine();
            else new Thread(BeginReadLine) { IsBackground = true }.Start();
        }
        public void BeginReadLine(WaitForExitState wfes)
        {
            try {
                ProcessStart.BeginErrorReadLine();
                ProcessStart.BeginOutputReadLine();
                if (wfes == WaitForExitState.WAITFOREXIT)
                    ProcessStart.WaitForExit();
                else if (wfes == WaitForExitState.STATEPROCESS) while (StateProcess) { };
                ProcessStart.CancelErrorRead();
                ProcessStart.CancelOutputRead();
            }
            catch(Exception ex)
            {
                WritingData("#Error :"+ex.Message);
            }

        }
        public void BeginReadLine() { BeginReadLine(WaitForExitState.STATEPROCESS);}
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
    /*  private void StandardOutputRun()
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
    */
        
        private void writingdataStriBuilder(string data)
        {
            StriBuilder.AppendLine(data);
        }
        public String ReadToEnd()
        {
            StriBuilder.Clear();
            Action<string> WritingDataTemp = WritingData;
            WritingData += writingdataStriBuilder;
            BeginReadLine();
            WritingData = WritingDataTemp;
            /*
            new Thread(StandardOutputRun) { IsBackground = true }.Start();
            new Thread(StandardErrorRun) { IsBackground = true }.Start();
            ProcessStart.WaitForExit();
            */
            return StriBuilder.ToString();
        }
        public String ReadToEnd(object Line)
        {
            Run(Line);
            return ReadToEnd();
        }
        public String ReadToEnd(Device deviceSelected, object Arguments)
        {
            Run(deviceSelected,Arguments);
            return ReadToEnd();
        }
    }

}

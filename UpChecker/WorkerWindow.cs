using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.IO;
using System.Linq;
using System.Management.Automation;

namespace UpChecker
{
    public partial class WorkerWindow : Form
    {
        private List<Task> tasks = new List<Task>();
        private List<string> results = new List<string>();
        private List<string> addresses;
        private WorkerMode mode;
        private int count;
        private int done;

        public WorkerWindow(List<string> a, WorkerMode m)
        {
            InitializeComponent();
            addresses = a;
            mode = m;
        }

        public void Run()
        {
            count = addresses.Count;
            progressBar.Maximum = count;

            switch (mode)
            {
                case WorkerMode.Ping:
                    runPing();
                    break;
                case WorkerMode.Reboot:
                    runReboot();
                    break;
                case WorkerMode.Logoff:
                    runLogoff();
                    break;
                case WorkerMode.Checkfile:
                    runCheckFile();
                    break;
                case WorkerMode.Checkprocess:
                    runCheckProcess();
                    break;
                case WorkerMode.Startprocess:
                    runStartProcess();
                    break;
            }
        }

        private void runReboot()
        {
            foreach (var a in addresses)
            {
                var t = new RebootWorker(a, this);
                tasks.Add(Task.Factory.StartNew(t.Do));
            }
        }

        private void runLogoff()
        {
            foreach (var a in addresses)
            {
                var t = new LogoffWorker(a, this);
                tasks.Add(Task.Factory.StartNew(t.Do));
            }
        }

        private void runPing()
        {
            foreach (var a in addresses)
            {
                var t = new CheckLifeWorker(a, this);
                tasks.Add(Task.Factory.StartNew(t.Check));
            }
        }

        private void runCheckFile()
        {
            var dialog = new ListDialog(@"Please type directory to check for, relative to network share root.
If multiple directories are entered (one on each line) the software
will check to see if any exist.");
            var dr = dialog.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                Application.Exit();
            }

            var dirs = dialog.input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var a in addresses)
            {
                var t = new CheckFileWorker(a, dirs, this);
                tasks.Add(Task.Factory.StartNew(t.Do));
            }
        }

        private void runCheckProcess()
        {
            var dialog = new ListDialog(@"Please type process name to check for.
If multiple processes are entered (one on each line) the software
will check to see if any exist.");
            var dr = dialog.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                Application.Exit();
            }

            var procs = dialog.input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var a in addresses)
            {
                var t = new CheckProcessWorker(a, procs, this);
                tasks.Add(Task.Factory.StartNew(t.Do));
            }
        }

        private void runStartProcess()
        {
            var dialog = new ListDialog(@"Please type location of process to execute.");
            var dr = dialog.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                Application.Exit();
            }

            try
            {
                Process.Start("psexec");
            }
            catch (Exception e)
            {
                outputBox.AppendText("ERROR: Cannot find 'psexec' on path. Please install sysinternals psexec. (" + e.Message + ")");
                return;
            }

            var procs = dialog.input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList().First();

            foreach (var a in addresses)
            {
                var t = new StartProcessWorker(a, procs, this);
                tasks.Add(Task.Factory.StartNew(t.Do));
            }
        }

        private class CheckFileWorker
        {
            private string address;
            private List<string> directories;
            private WorkerWindow worker;
            public CheckFileWorker(string a, List<string> dirs, WorkerWindow w)
            {
                address = a;
                directories = dirs;
                worker = w;
            }

            public void Do()
            {
                var found = false;

                foreach (var d in directories)
                {
                    if (Directory.Exists("\\\\" + address + "\\" + d))
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    worker.update(address, "Fine");
                }
                else
                {
                    worker.update(address, "Nope - can't find directory");
                }
            }
        }

        private class StartProcessWorker
        {
            private string address;
            private string process;
            private WorkerWindow worker;
            public StartProcessWorker(string a, string p, WorkerWindow w)
            {
                address = a;
                process = p;
                worker = w;
            }

            public void Do()
            {
                Process p = new Process();
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                ps.FileName = "psexec";
                ps.UseShellExecute = false;
                ps.RedirectStandardOutput = true;
                ps.RedirectStandardError = true;
                ps.Arguments = @"\\" + address + " \"" + process + "\"";
                p.StartInfo = ps;
                p.Start();
                
                var m = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
                p.WaitForExit();
                p.Close();
                worker.update(address, m);
            }
        }

        private class CheckProcessWorker
        {
            private string address;
            private List<string> processes;
            private WorkerWindow worker;
            public CheckProcessWorker(string a, List<string> prs, WorkerWindow w)
            {
                address = a;
                processes = prs;
                worker = w;
            }

            public void Do()
            {
                var found = false;

                foreach (var p in processes)
                {
                    try
                    {
                        if (Process.GetProcessesByName(p, address).Count() != 0)
                        {
                            found = true;
                        }
                    }
                    catch (Exception e)
                    {
                        worker.update(address, "Error: " + e.Message);
                        return;
                    }
                }

                if (found)
                {
                    worker.update(address, "Fine");
                }
                else
                {
                    worker.update(address, "Nope - can't find process");
                }
            }
        }

        public void update(string a, string s)
        {
            addToProgressBar();
            done++;
            addOutputText("Address " + a + " responded: " + s + " (" + done + " of " + count + ")" + Environment.NewLine);
        }

        delegate void addOutputTextCallback(string text);

        private void addOutputText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (outputBox.InvokeRequired)
            {
                addOutputTextCallback d = new addOutputTextCallback(addOutputText);
                outputBox.Invoke(d, new object[] { text });
            }
            else
            {
                outputBox.AppendText(text);
            }
        }

        delegate void addToProgressBarCallback();

        private void addToProgressBar()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (progressBar.InvokeRequired)
            {
                addToProgressBarCallback d = new addToProgressBarCallback(addToProgressBar);
                progressBar.Invoke(d);
            }
            else
            {
                progressBar.Value++;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public class RebootWorker
        {
            private string address;
            private WorkerWindow worker;
            public RebootWorker(string a, WorkerWindow w)
            {
                address = a;
                worker = w;
            }

            public void Do()
            {
                using (PowerShell ps = PowerShell.Create())
            }
        }

        public class LogoffWorker
        {
            private string address;
            private WorkerWindow worker;
            public LogoffWorker(string a, WorkerWindow w)
            {
                address = a;
                worker = w;
            }

            public void Do()
            {
                using (PowerShell ps = PowerShell.Create())
                {
                    ps.AddScript("LOGOFF console /server:" + address);
                    var r = ps.BeginInvoke();
                    // todo callback
                }
            }
        }

        public class CheckLifeWorker
        {
            private string address;
            private WorkerWindow worker;
            public CheckLifeWorker(string a, WorkerWindow w)
            {
                address = a;
                worker = w;
            }

            public void Check()
            {
                var p = new Ping();
                //PingReply pr;
                string m;
                try
                {
                    m = p.Send(address).Status.ToString();
                }
                catch (PingException e)
                {
                    m = e.Message.ToString();
                }
                worker.update(address, m);
            }
        }
    }

    public enum WorkerMode
    {
        Ping, Reboot, Checkfile, Checkprocess, Startprocess, Logoff
    }
}

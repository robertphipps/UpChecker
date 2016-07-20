using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.IO;
using System.Linq;

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
                case WorkerMode.Checkfile:
                    runCheckFile();
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
            var dialog = new DirectoryDialog();
            var dr = dialog.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                Application.Exit();
            }

            var dirs = dialog.directory.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var a in addresses)
            {
                var t = new CheckFileWorker(a, dirs, this);
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
                Process p = new Process();
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                ps.FileName = "shutdown.exe";
                ps.UseShellExecute = false;
                ps.RedirectStandardOutput = true;
                ps.RedirectStandardError = true;
                ps.Arguments = "/r /t 5 /m \\\\" + address;
                p.StartInfo = ps;
                p.Start();
                var m = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
                p.WaitForExit();
                p.Close();
                worker.update(address, m);
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
                catch (System.Net.NetworkInformation.PingException e)
                {
                    m = e.Message.ToString();
                }
                worker.update(address, m);
            }
        }
    }

    public enum WorkerMode
    {
        Ping, Reboot, Checkfile
    }
}

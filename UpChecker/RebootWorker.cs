using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace UpChecker
{
    public partial class RebootWorker : Form
    {
        private List<Task> tasks = new List<Task>();
        private List<string> results = new List<string>();
        private List<string> addresses;

        public RebootWorker(string a)
        {
            InitializeComponent();
            addresses = a.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }

        public void Run()
        {
            var count = addresses.Count;
            progressBar.Maximum = count;
            
            foreach (var a in addresses)
            {
                var t = new doRebootWorker(a, this);
                tasks.Add(Task.Factory.StartNew(t.Do));
            }
        }

        public void update(string a, string s)
        {
            addToProgressBar();
            addOutputText("Address " + a + " responded: " + s + Environment.NewLine);
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
    }

    public class doRebootWorker
    {
        private string address;
        private RebootWorker worker;
        public doRebootWorker(string a, RebootWorker w)
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
            ps.Arguments = "/r /t 5 /m \\" + address;
            p.StartInfo = ps;
            p.Start();
            var m = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            worker.update(address,m);
        }
    }
}

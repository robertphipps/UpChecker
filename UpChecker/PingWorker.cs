using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpChecker
{
    public partial class PingWorker : Form
    {
        private List<Task> tasks = new List<Task>();
        private List<string> results = new List<string>();
        private List<string> addresses;

        public PingWorker(string a)
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
                var t = new CheckLifeWorker(a, this);
                tasks.Add(Task.Factory.StartNew(t.Check));
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

    public class CheckLifeWorker
    {
        private string address;
        private PingWorker worker;
        public CheckLifeWorker(string a, PingWorker w)
        {
            address = a;
            worker = w;
        }

        public void Check()
        {
            var p = new Ping();
            PingReply pr;
            string m;
            try
            {
                m = p.Send(address).Status.ToString();
            }
            catch (System.Net.NetworkInformation.PingException e)
            {
                m = e.Message.ToString();
            }
            worker.update(address,m);
        }
    }
}

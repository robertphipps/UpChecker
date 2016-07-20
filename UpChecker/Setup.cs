using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpChecker
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            var clients = inputBox.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (pingClientsRadio.Checked)
            {
                WorkerWindow w = new WorkerWindow(clients, WorkerMode.Ping);
                w.Show();
                w.Run();
            }
            else if (rebootClientsRadio.Checked)
            {
                WorkerWindow w = new WorkerWindow(clients, WorkerMode.Reboot);
                w.Show();
                w.Run();
            }
            else if (checkClientsRadio.Checked)
            {
                WorkerWindow w = new WorkerWindow(clients, WorkerMode.Checkfile);
                w.Show();
                w.Run();
            }
            else
            {
                MessageBox.Show("Please select a mode", "No mode selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}

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
            Worker w = new Worker(inputBox.Text);
            w.Show();
            w.Run();

        }
    }
}

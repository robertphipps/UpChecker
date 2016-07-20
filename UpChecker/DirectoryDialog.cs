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
    public partial class DirectoryDialog : Form
    {
        public string directory { get; set; }

        public DirectoryDialog()
        {
            InitializeComponent();
        }

        private void okayButton_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            directory = textBox1.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

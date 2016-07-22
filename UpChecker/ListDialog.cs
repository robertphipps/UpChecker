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
    public partial class ListDialog : Form
    {
        public string input { get; set; }

        public ListDialog(string message)
        {
            InitializeComponent();
            label1.Text = message;
        }

        private void okayButton_click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            input = textBox1.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TemplateProject.Plugins.Toast
{
    public partial class FormToast : Form
    {
        public FormToast()
        {
            InitializeComponent();
        }

        public FormToast(string title, string message) : base()
        {

        }

        public FormToast(string message) : this("", message)
        {

        }
    }
}

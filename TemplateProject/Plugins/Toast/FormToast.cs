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
        /// <summary>
        /// 设置标题
        /// </summary>
        public string Caption
        {
            set { lblCaption.Text = value; }
        }

        /// <summary>
        /// 设置内容
        /// </summary>
        public string Content
        {
            set { txtMsg.Text = value; }
        }

        public FormToast()
        {
            InitializeComponent();
        }

        public FormToast(string title, string message) : base()
        {
            lblCaption.Text = title;
            txtMsg.Text = message;
        }

        public FormToast(string message):base()
        {
            txtMsg.Text = message;
        }
    }
}

namespace TemplateProject.Plugins.Toast
{
    partial class FormToast
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.plCaption = new System.Windows.Forms.Panel();
            this.plContent = new System.Windows.Forms.Panel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.RichTextBox();
            this.plCaption.SuspendLayout();
            this.plContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // plCaption
            // 
            this.plCaption.Controls.Add(this.lblCaption);
            this.plCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCaption.Location = new System.Drawing.Point(0, 0);
            this.plCaption.Name = "plCaption";
            this.plCaption.Size = new System.Drawing.Size(370, 26);
            this.plCaption.TabIndex = 0;
            // 
            // plContent
            // 
            this.plContent.Controls.Add(this.txtMsg);
            this.plContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plContent.Location = new System.Drawing.Point(0, 26);
            this.plContent.Name = "plContent";
            this.plContent.Size = new System.Drawing.Size(370, 62);
            this.plContent.TabIndex = 2;
            // 
            // lblCaption
            // 
            this.lblCaption.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaption.Location = new System.Drawing.Point(6, 3);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(361, 23);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "提示";
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMsg
            // 
            this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsg.Location = new System.Drawing.Point(12, 7);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(346, 43);
            this.txtMsg.TabIndex = 0;
            this.txtMsg.Text = "";
            // 
            // FormToast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(370, 88);
            this.Controls.Add(this.plContent);
            this.Controls.Add(this.plCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormToast";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormToast";
            this.plCaption.ResumeLayout(false);
            this.plContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plCaption;
        private System.Windows.Forms.Panel plContent;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.RichTextBox txtMsg;
    }
}
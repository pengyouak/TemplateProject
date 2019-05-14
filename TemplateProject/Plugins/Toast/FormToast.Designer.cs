namespace WindCommon.Plugins.Toast
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
            this.txtMsg = new System.Windows.Forms.RichTextBox();
            this.lblCaption = new System.Windows.Forms.Label();
            this.plIco = new System.Windows.Forms.Panel();
            this.pbIco = new System.Windows.Forms.PictureBox();
            this.plCaption.SuspendLayout();
            this.plContent.SuspendLayout();
            this.plIco.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIco)).BeginInit();
            this.SuspendLayout();
            // 
            // plCaption
            // 
            this.plCaption.BackColor = System.Drawing.Color.White;
            this.plCaption.Controls.Add(this.lblCaption);
            this.plCaption.Controls.Add(this.plIco);
            this.plCaption.Dock = System.Windows.Forms.DockStyle.Top;
            this.plCaption.Location = new System.Drawing.Point(1, 1);
            this.plCaption.Name = "plCaption";
            this.plCaption.Padding = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.plCaption.Size = new System.Drawing.Size(369, 30);
            this.plCaption.TabIndex = 0;
            // 
            // plContent
            // 
            this.plContent.BackColor = System.Drawing.Color.White;
            this.plContent.Controls.Add(this.txtMsg);
            this.plContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plContent.Location = new System.Drawing.Point(1, 31);
            this.plContent.Name = "plContent";
            this.plContent.Size = new System.Drawing.Size(369, 80);
            this.plContent.TabIndex = 2;
            // 
            // txtMsg
            // 
            this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsg.Location = new System.Drawing.Point(24, 13);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(323, 51);
            this.txtMsg.TabIndex = 0;
            this.txtMsg.Text = "";
            // 
            // lblCaption
            // 
            this.lblCaption.BackColor = System.Drawing.Color.White;
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCaption.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaption.Location = new System.Drawing.Point(35, 3);
            this.lblCaption.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(331, 24);
            this.lblCaption.TabIndex = 2;
            this.lblCaption.Text = "提示";
            this.lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // plIco
            // 
            this.plIco.Controls.Add(this.pbIco);
            this.plIco.Dock = System.Windows.Forms.DockStyle.Left;
            this.plIco.Location = new System.Drawing.Point(5, 3);
            this.plIco.Name = "plIco";
            this.plIco.Size = new System.Drawing.Size(30, 24);
            this.plIco.TabIndex = 1;
            this.plIco.Visible = false;
            // 
            // pbIco
            // 
            this.pbIco.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbIco.Location = new System.Drawing.Point(0, 0);
            this.pbIco.Name = "pbIco";
            this.pbIco.Size = new System.Drawing.Size(24, 24);
            this.pbIco.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIco.TabIndex = 2;
            this.pbIco.TabStop = false;
            // 
            // FormToast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(371, 112);
            this.Controls.Add(this.plContent);
            this.Controls.Add(this.plCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormToast";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormToast";
            this.plCaption.ResumeLayout(false);
            this.plContent.ResumeLayout(false);
            this.plIco.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbIco)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plCaption;
        private System.Windows.Forms.Panel plContent;
        private System.Windows.Forms.RichTextBox txtMsg;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Panel plIco;
        private System.Windows.Forms.PictureBox pbIco;
    }
}
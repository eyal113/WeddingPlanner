namespace WeddingPlanner
{
    partial class Form1
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
            this.button_send = new System.Windows.Forms.Button();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.label_url = new System.Windows.Forms.Label();
            this.button_readwrite = new System.Windows.Forms.Button();
            this.button_sms = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBox_shortUrl = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox_sms = new System.Windows.Forms.RichTextBox();
            this.dataGridView_Guest = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Guest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(6, 185);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 23);
            this.button_send.TabIndex = 0;
            this.button_send.Text = "Export";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(102, 14);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(204, 20);
            this.textBox_url.TabIndex = 1;
            this.textBox_url.Text = "http://192.168.1.154:9090/sendsms";
            // 
            // label_url
            // 
            this.label_url.AutoSize = true;
            this.label_url.Location = new System.Drawing.Point(8, 17);
            this.label_url.Name = "label_url";
            this.label_url.Size = new System.Drawing.Size(92, 13);
            this.label_url.TabIndex = 2;
            this.label_url.Text = "SMS Server URL:";
            // 
            // button_readwrite
            // 
            this.button_readwrite.Location = new System.Drawing.Point(97, 185);
            this.button_readwrite.Name = "button_readwrite";
            this.button_readwrite.Size = new System.Drawing.Size(75, 23);
            this.button_readwrite.TabIndex = 3;
            this.button_readwrite.Text = "Read Excel";
            this.button_readwrite.UseVisualStyleBackColor = true;
            this.button_readwrite.Click += new System.EventHandler(this.button_readwrite_Click);
            // 
            // button_sms
            // 
            this.button_sms.Enabled = false;
            this.button_sms.Location = new System.Drawing.Point(530, 246);
            this.button_sms.Name = "button_sms";
            this.button_sms.Size = new System.Drawing.Size(75, 23);
            this.button_sms.TabIndex = 4;
            this.button_sms.Text = "Send SMS";
            this.button_sms.UseVisualStyleBackColor = true;
            this.button_sms.Click += new System.EventHandler(this.button_sms_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 246);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(512, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 5;
            // 
            // checkBox_shortUrl
            // 
            this.checkBox_shortUrl.AutoSize = true;
            this.checkBox_shortUrl.Checked = true;
            this.checkBox_shortUrl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_shortUrl.Location = new System.Drawing.Point(9, 157);
            this.checkBox_shortUrl.Name = "checkBox_shortUrl";
            this.checkBox_shortUrl.Size = new System.Drawing.Size(76, 17);
            this.checkBox_shortUrl.TabIndex = 6;
            this.checkBox_shortUrl.Text = "Short URL";
            this.checkBox_shortUrl.UseVisualStyleBackColor = true;
            this.checkBox_shortUrl.CheckedChanged += new System.EventHandler(this.checkBox_shortUrl_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(617, 240);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label_url);
            this.tabPage1.Controls.Add(this.checkBox_shortUrl);
            this.tabPage1.Controls.Add(this.textBox_url);
            this.tabPage1.Controls.Add(this.button_send);
            this.tabPage1.Controls.Add(this.button_readwrite);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(609, 214);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Invitation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.richTextBox_sms);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(609, 214);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SMS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox_sms
            // 
            this.richTextBox_sms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_sms.Location = new System.Drawing.Point(3, 3);
            this.richTextBox_sms.MaxLength = 110;
            this.richTextBox_sms.Multiline = false;
            this.richTextBox_sms.Name = "richTextBox_sms";
            this.richTextBox_sms.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.richTextBox_sms.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox_sms.Size = new System.Drawing.Size(603, 208);
            this.richTextBox_sms.TabIndex = 0;
            this.richTextBox_sms.Text = "";
            // 
            // dataGridView_Guest
            // 
            this.dataGridView_Guest.AllowUserToAddRows = false;
            this.dataGridView_Guest.AllowUserToDeleteRows = false;
            this.dataGridView_Guest.AllowUserToResizeRows = false;
            this.dataGridView_Guest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Guest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Guest.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Guest.MultiSelect = false;
            this.dataGridView_Guest.Name = "dataGridView_Guest";
            this.dataGridView_Guest.ReadOnly = true;
            this.dataGridView_Guest.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView_Guest.RowHeadersVisible = false;
            this.dataGridView_Guest.Size = new System.Drawing.Size(617, 240);
            this.dataGridView_Guest.TabIndex = 8;
            this.dataGridView_Guest.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_Guest_CellContentClick);
            this.dataGridView_Guest.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_Guest_CellFormatting);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1.Controls.Add(this.button_sms);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_Guest);
            this.splitContainer1.Size = new System.Drawing.Size(617, 562);
            this.splitContainer1.SplitterDistance = 318;
            this.splitContainer1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 562);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Wedding Planner";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Guest)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Label label_url;
        private System.Windows.Forms.Button button_readwrite;
        private System.Windows.Forms.Button button_sms;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox checkBox_shortUrl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox richTextBox_sms;
        private System.Windows.Forms.DataGridView dataGridView_Guest;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}


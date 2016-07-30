namespace AmazonAcctGenerator
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_resume = new System.Windows.Forms.Button();
            this.tabledata = new System.Windows.Forms.ComboBox();
            this.btn_edge = new System.Windows.Forms.Button();
            this.btn_sync = new System.Windows.Forms.Button();
            this.btn_create = new System.Windows.Forms.Button();
            this.btn_open = new System.Windows.Forms.Button();
            this.btn_chrome = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_upt = new System.Windows.Forms.Button();
            this.btn_onedollor = new System.Windows.Forms.Button();
            this.btn_ccyp = new System.Windows.Forms.Button();
            this.btn_fillbill = new System.Windows.Forms.Button();
            this.cardpickup = new System.Windows.Forms.ComboBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.msgpanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBox1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.listBox2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.msgpanel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(674, 452);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox1, 2);
            this.groupBox1.Controls.Add(this.btn_resume);
            this.groupBox1.Controls.Add(this.tabledata);
            this.groupBox1.Controls.Add(this.btn_edge);
            this.groupBox1.Controls.Add(this.btn_sync);
            this.groupBox1.Controls.Add(this.btn_create);
            this.groupBox1.Controls.Add(this.btn_open);
            this.groupBox1.Controls.Add(this.btn_chrome);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(670, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Function";
            // 
            // btn_resume
            // 
            this.btn_resume.Enabled = false;
            this.btn_resume.Location = new System.Drawing.Point(183, 17);
            this.btn_resume.Name = "btn_resume";
            this.btn_resume.Size = new System.Drawing.Size(62, 25);
            this.btn_resume.TabIndex = 7;
            this.btn_resume.Text = "resume";
            this.btn_resume.UseVisualStyleBackColor = true;
            this.btn_resume.Click += new System.EventHandler(this.btn_resume_Click);
            // 
            // tabledata
            // 
            this.tabledata.FormattingEnabled = true;
            this.tabledata.Items.AddRange(new object[] {
            "",
            "account",
            "review",
            "shipping",
            "card"});
            this.tabledata.Location = new System.Drawing.Point(380, 21);
            this.tabledata.Name = "tabledata";
            this.tabledata.Size = new System.Drawing.Size(70, 20);
            this.tabledata.TabIndex = 6;
            this.tabledata.SelectedIndexChanged += new System.EventHandler(this.tabledata_SelectedIndexChanged);
            // 
            // btn_edge
            // 
            this.btn_edge.Location = new System.Drawing.Point(465, 17);
            this.btn_edge.Margin = new System.Windows.Forms.Padding(2);
            this.btn_edge.Name = "btn_edge";
            this.btn_edge.Size = new System.Drawing.Size(83, 25);
            this.btn_edge.TabIndex = 5;
            this.btn_edge.Text = "Edge inPrivate";
            this.btn_edge.UseVisualStyleBackColor = true;
            this.btn_edge.Click += new System.EventHandler(this.btn_edge_Click);
            // 
            // btn_sync
            // 
            this.btn_sync.Location = new System.Drawing.Point(566, 16);
            this.btn_sync.Margin = new System.Windows.Forms.Padding(2);
            this.btn_sync.Name = "btn_sync";
            this.btn_sync.Size = new System.Drawing.Size(86, 25);
            this.btn_sync.TabIndex = 3;
            this.btn_sync.Text = "Sync Accounts";
            this.btn_sync.UseVisualStyleBackColor = true;
            this.btn_sync.Click += new System.EventHandler(this.btn_sync_Click);
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(97, 17);
            this.btn_create.Margin = new System.Windows.Forms.Padding(2);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(77, 25);
            this.btn_create.TabIndex = 2;
            this.btn_create.Text = "Start Create";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(9, 17);
            this.btn_open.Margin = new System.Windows.Forms.Padding(2);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(77, 25);
            this.btn_open.TabIndex = 1;
            this.btn_open.Text = "Open n\' Read";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // btn_chrome
            // 
            this.btn_chrome.Location = new System.Drawing.Point(260, 17);
            this.btn_chrome.Margin = new System.Windows.Forms.Padding(2);
            this.btn_chrome.Name = "btn_chrome";
            this.btn_chrome.Size = new System.Drawing.Size(104, 25);
            this.btn_chrome.TabIndex = 4;
            this.btn_chrome.Text = "Chrome inPrivate";
            this.btn_chrome.UseVisualStyleBackColor = true;
            this.btn_chrome.Click += new System.EventHandler(this.btn_chrome_Click);
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
            "prepare to go"});
            this.listBox1.Location = new System.Drawing.Point(2, 146);
            this.listBox1.Margin = new System.Windows.Forms.Padding(2);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(333, 304);
            this.listBox1.TabIndex = 2;
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Items.AddRange(new object[] {
            "finish items"});
            this.listBox2.Location = new System.Drawing.Point(339, 146);
            this.listBox2.Margin = new System.Windows.Forms.Padding(2);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(333, 304);
            this.listBox2.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.groupBox2, 2);
            this.groupBox2.Controls.Add(this.btn_upt);
            this.groupBox2.Controls.Add(this.btn_onedollor);
            this.groupBox2.Controls.Add(this.btn_ccyp);
            this.groupBox2.Controls.Add(this.btn_fillbill);
            this.groupBox2.Controls.Add(this.cardpickup);
            this.groupBox2.Controls.Add(this.btn_save);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(668, 46);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bid and Shipping";
            // 
            // btn_upt
            // 
            this.btn_upt.Location = new System.Drawing.Point(531, 13);
            this.btn_upt.Name = "btn_upt";
            this.btn_upt.Size = new System.Drawing.Size(75, 23);
            this.btn_upt.TabIndex = 10;
            this.btn_upt.Text = "upt purchase";
            this.btn_upt.UseVisualStyleBackColor = true;
            this.btn_upt.Click += new System.EventHandler(this.btn_upt_Click);
            // 
            // btn_onedollor
            // 
            this.btn_onedollor.Location = new System.Drawing.Point(206, 13);
            this.btn_onedollor.Name = "btn_onedollor";
            this.btn_onedollor.Size = new System.Drawing.Size(60, 26);
            this.btn_onedollor.TabIndex = 9;
            this.btn_onedollor.Text = "$1 page";
            this.btn_onedollor.UseVisualStyleBackColor = true;
            this.btn_onedollor.Click += new System.EventHandler(this.btn_onedollor_Click_1);
            // 
            // btn_ccyp
            // 
            this.btn_ccyp.Location = new System.Drawing.Point(6, 16);
            this.btn_ccyp.Name = "btn_ccyp";
            this.btn_ccyp.Size = new System.Drawing.Size(79, 23);
            this.btn_ccyp.TabIndex = 8;
            this.btn_ccyp.Text = "ccyp.com";
            this.btn_ccyp.UseVisualStyleBackColor = true;
            this.btn_ccyp.Click += new System.EventHandler(this.btn_ccyp_Click);
            // 
            // btn_fillbill
            // 
            this.btn_fillbill.Location = new System.Drawing.Point(464, 14);
            this.btn_fillbill.Name = "btn_fillbill";
            this.btn_fillbill.Size = new System.Drawing.Size(61, 23);
            this.btn_fillbill.TabIndex = 5;
            this.btn_fillbill.Text = "fill bill";
            this.btn_fillbill.UseVisualStyleBackColor = true;
            this.btn_fillbill.Click += new System.EventHandler(this.btn_fillbill_Click);
            // 
            // cardpickup
            // 
            this.cardpickup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cardpickup.FormattingEnabled = true;
            this.cardpickup.Location = new System.Drawing.Point(280, 16);
            this.cardpickup.Name = "cardpickup";
            this.cardpickup.Size = new System.Drawing.Size(169, 20);
            this.cardpickup.TabIndex = 6;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(96, 15);
            this.btn_save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(96, 25);
            this.btn_save.TabIndex = 7;
            this.btn_save.Text = "save contact info";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // msgpanel
            // 
            this.msgpanel.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.msgpanel, 2);
            this.msgpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgpanel.Location = new System.Drawing.Point(3, 107);
            this.msgpanel.Name = "msgpanel";
            this.msgpanel.Size = new System.Drawing.Size(668, 34);
            this.msgpanel.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 452);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Amazon Account Management";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_sync;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btn_chrome;
        private System.Windows.Forms.ComboBox cardpickup;
        private System.Windows.Forms.Button btn_fillbill;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_onedollor;
        private System.Windows.Forms.Button btn_ccyp;
        private System.Windows.Forms.Button btn_edge;
        private System.Windows.Forms.FlowLayoutPanel msgpanel;
        private System.Windows.Forms.ComboBox tabledata;
        private System.Windows.Forms.Button btn_upt;
        private System.Windows.Forms.Button btn_resume;
    }
}


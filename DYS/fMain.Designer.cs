namespace DYS
{
    partial class fMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.gbBrowser = new System.Windows.Forms.GroupBox();
            this.cbHideBrowser = new System.Windows.Forms.CheckBox();
            this.rbChrome = new System.Windows.Forms.RadioButton();
            this.rbFirefox = new System.Windows.Forms.RadioButton();
            this.gbLogin = new System.Windows.Forms.GroupBox();
            this.rbMebbis = new System.Windows.Forms.RadioButton();
            this.rbEdevlet = new System.Windows.Forms.RadioButton();
            this.gbLoginInfo = new System.Windows.Forms.GroupBox();
            this.texPass = new System.Windows.Forms.TextBox();
            this.cbRemember = new System.Windows.Forms.CheckBox();
            this.textAddTcno = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblTckn = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnOnay = new System.Windows.Forms.Button();
            this.gbProcessSummary = new System.Windows.Forms.GroupBox();
            this.lblReport = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbProcess = new System.Windows.Forms.ProgressBar();
            this.gbBrowser.SuspendLayout();
            this.gbLogin.SuspendLayout();
            this.gbLoginInfo.SuspendLayout();
            this.gbProcessSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbBrowser
            // 
            this.gbBrowser.Controls.Add(this.cbHideBrowser);
            this.gbBrowser.Controls.Add(this.rbChrome);
            this.gbBrowser.Controls.Add(this.rbFirefox);
            this.gbBrowser.Location = new System.Drawing.Point(18, 14);
            this.gbBrowser.Name = "gbBrowser";
            this.gbBrowser.Size = new System.Drawing.Size(291, 85);
            this.gbBrowser.TabIndex = 0;
            this.gbBrowser.TabStop = false;
            this.gbBrowser.Text = "Browser Seçiniz";
            // 
            // cbHideBrowser
            // 
            this.cbHideBrowser.AutoSize = true;
            this.cbHideBrowser.Location = new System.Drawing.Point(19, 57);
            this.cbHideBrowser.Name = "cbHideBrowser";
            this.cbHideBrowser.Size = new System.Drawing.Size(170, 19);
            this.cbHideBrowser.TabIndex = 2;
            this.cbHideBrowser.Text = "Firefox\'u arkaplanda çalıştır";
            this.cbHideBrowser.UseVisualStyleBackColor = true;
            this.cbHideBrowser.CheckedChanged += new System.EventHandler(this.cbHideBrowser_CheckedChanged);
            // 
            // rbChrome
            // 
            this.rbChrome.AutoSize = true;
            this.rbChrome.Checked = true;
            this.rbChrome.Location = new System.Drawing.Point(156, 22);
            this.rbChrome.Name = "rbChrome";
            this.rbChrome.Size = new System.Drawing.Size(109, 19);
            this.rbChrome.TabIndex = 1;
            this.rbChrome.TabStop = true;
            this.rbChrome.Text = "Google Chrome";
            this.rbChrome.UseVisualStyleBackColor = true;
            this.rbChrome.CheckedChanged += new System.EventHandler(this.rbChrome_CheckedChanged);
            // 
            // rbFirefox
            // 
            this.rbFirefox.AutoSize = true;
            this.rbFirefox.Location = new System.Drawing.Point(19, 22);
            this.rbFirefox.Name = "rbFirefox";
            this.rbFirefox.Size = new System.Drawing.Size(61, 19);
            this.rbFirefox.TabIndex = 0;
            this.rbFirefox.TabStop = true;
            this.rbFirefox.Text = "Firefox";
            this.rbFirefox.UseVisualStyleBackColor = true;
            // 
            // gbLogin
            // 
            this.gbLogin.Controls.Add(this.rbMebbis);
            this.gbLogin.Controls.Add(this.rbEdevlet);
            this.gbLogin.Location = new System.Drawing.Point(18, 105);
            this.gbLogin.Name = "gbLogin";
            this.gbLogin.Size = new System.Drawing.Size(291, 52);
            this.gbLogin.TabIndex = 0;
            this.gbLogin.TabStop = false;
            this.gbLogin.Text = "Giriş Yöntemi Seçiniz";
            // 
            // rbMebbis
            // 
            this.rbMebbis.AutoSize = true;
            this.rbMebbis.Location = new System.Drawing.Point(156, 22);
            this.rbMebbis.Name = "rbMebbis";
            this.rbMebbis.Size = new System.Drawing.Size(64, 19);
            this.rbMebbis.TabIndex = 4;
            this.rbMebbis.TabStop = true;
            this.rbMebbis.Text = "Mebbis";
            this.rbMebbis.UseVisualStyleBackColor = true;
            // 
            // rbEdevlet
            // 
            this.rbEdevlet.AutoSize = true;
            this.rbEdevlet.Checked = true;
            this.rbEdevlet.Location = new System.Drawing.Point(19, 22);
            this.rbEdevlet.Name = "rbEdevlet";
            this.rbEdevlet.Size = new System.Drawing.Size(69, 19);
            this.rbEdevlet.TabIndex = 3;
            this.rbEdevlet.TabStop = true;
            this.rbEdevlet.Text = "E-Devlet";
            this.rbEdevlet.UseVisualStyleBackColor = true;
            this.rbEdevlet.CheckedChanged += new System.EventHandler(this.rbEdevlet_CheckedChanged);
            // 
            // gbLoginInfo
            // 
            this.gbLoginInfo.Controls.Add(this.texPass);
            this.gbLoginInfo.Controls.Add(this.cbRemember);
            this.gbLoginInfo.Controls.Add(this.textAddTcno);
            this.gbLoginInfo.Controls.Add(this.lblPass);
            this.gbLoginInfo.Controls.Add(this.lblTckn);
            this.gbLoginInfo.Location = new System.Drawing.Point(18, 161);
            this.gbLoginInfo.Name = "gbLoginInfo";
            this.gbLoginInfo.Size = new System.Drawing.Size(291, 130);
            this.gbLoginInfo.TabIndex = 0;
            this.gbLoginInfo.TabStop = false;
            this.gbLoginInfo.Text = "E-Devlet Giriş Bilgileri";
            // 
            // texPass
            // 
            this.texPass.Location = new System.Drawing.Point(145, 54);
            this.texPass.Name = "texPass";
            this.texPass.PasswordChar = '*';
            this.texPass.Size = new System.Drawing.Size(140, 23);
            this.texPass.TabIndex = 6;
            // 
            // cbRemember
            // 
            this.cbRemember.AutoSize = true;
            this.cbRemember.Location = new System.Drawing.Point(19, 96);
            this.cbRemember.Name = "cbRemember";
            this.cbRemember.Size = new System.Drawing.Size(85, 19);
            this.cbRemember.TabIndex = 7;
            this.cbRemember.Text = "Beni hatırla";
            this.cbRemember.UseVisualStyleBackColor = true;
            // 
            // textAddTcno
            // 
            this.textAddTcno.Location = new System.Drawing.Point(145, 20);
            this.textAddTcno.MaxLength = 11;
            this.textAddTcno.Name = "textAddTcno";
            this.textAddTcno.Size = new System.Drawing.Size(140, 23);
            this.textAddTcno.TabIndex = 5;
            this.textAddTcno.TextChanged += new System.EventHandler(this.textAddTcno_TextChanged);
            this.textAddTcno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textAddTcno_KeyPress);
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(19, 58);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(92, 15);
            this.lblPass.TabIndex = 0;
            this.lblPass.Text = "E-Devlet Şifreniz";
            // 
            // lblTckn
            // 
            this.lblTckn.AutoSize = true;
            this.lblTckn.Location = new System.Drawing.Point(19, 24);
            this.lblTckn.Name = "lblTckn";
            this.lblTckn.Size = new System.Drawing.Size(117, 15);
            this.lblTckn.TabIndex = 0;
            this.lblTckn.Text = "TC Kimlik Numaranız";
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(20, 294);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(289, 47);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "label1";
            // 
            // btnOnay
            // 
            this.btnOnay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOnay.Location = new System.Drawing.Point(18, 349);
            this.btnOnay.Name = "btnOnay";
            this.btnOnay.Size = new System.Drawing.Size(291, 49);
            this.btnOnay.TabIndex = 8;
            this.btnOnay.Text = "ONAYLAMAYI BAŞLAT";
            this.btnOnay.UseVisualStyleBackColor = true;
            this.btnOnay.Click += new System.EventHandler(this.btnOnay_Click);
            // 
            // gbProcessSummary
            // 
            this.gbProcessSummary.Controls.Add(this.lblReport);
            this.gbProcessSummary.Controls.Add(this.btnCancel);
            this.gbProcessSummary.Controls.Add(this.pbProcess);
            this.gbProcessSummary.Location = new System.Drawing.Point(329, 14);
            this.gbProcessSummary.Name = "gbProcessSummary";
            this.gbProcessSummary.Size = new System.Drawing.Size(393, 392);
            this.gbProcessSummary.TabIndex = 7;
            this.gbProcessSummary.TabStop = false;
            this.gbProcessSummary.Text = "İşlem Özeti";
            // 
            // lblReport
            // 
            this.lblReport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblReport.ForeColor = System.Drawing.Color.Red;
            this.lblReport.Location = new System.Drawing.Point(18, 22);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(355, 279);
            this.lblReport.TabIndex = 9;
            this.lblReport.Text = "lblReport";
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(18, 335);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(355, 49);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "ONAYLAMA İŞLEMİNİ İPTAL ET";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbProcess
            // 
            this.pbProcess.Location = new System.Drawing.Point(18, 304);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(355, 23);
            this.pbProcess.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbProcess.TabIndex = 10;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 407);
            this.Controls.Add(this.gbProcessSummary);
            this.Controls.Add(this.btnOnay);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.gbLoginInfo);
            this.Controls.Add(this.gbLogin);
            this.Controls.Add(this.gbBrowser);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(0, 446);
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DYS OTOMATİK ONAYLAMA";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.fMain_HelpButtonClicked);
            this.Load += new System.EventHandler(this.fMain_Load);
            this.gbBrowser.ResumeLayout(false);
            this.gbBrowser.PerformLayout();
            this.gbLogin.ResumeLayout(false);
            this.gbLogin.PerformLayout();
            this.gbLoginInfo.ResumeLayout(false);
            this.gbLoginInfo.PerformLayout();
            this.gbProcessSummary.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox gbBrowser;
        private RadioButton rbChrome;
        private RadioButton rbFirefox;
        private GroupBox gbLogin;
        private RadioButton rbMebbis;
        private RadioButton rbEdevlet;
        private GroupBox gbLoginInfo;
        private TextBox texPass;
        private TextBox textAddTcno;
        private Label lblPass;
        private Label lblTckn;
        private Label lblMessage;
        private Button btnOnay;
        private CheckBox cbRemember;
        private CheckBox cbHideBrowser;
        private GroupBox gbProcessSummary;
        private Button btnCancel;
        private ProgressBar pbProcess;
        private Label lblReport;
    }
}
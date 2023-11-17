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
            gbBrowser = new GroupBox();
            cbHideBrowser = new CheckBox();
            rbChrome = new RadioButton();
            rbFirefox = new RadioButton();
            gbLogin = new GroupBox();
            rbMebbis = new RadioButton();
            rbEdevlet = new RadioButton();
            gbLoginInfo = new GroupBox();
            texPass = new TextBox();
            cbRemember = new CheckBox();
            textAddTcno = new TextBox();
            lblPass = new Label();
            lblTckn = new Label();
            lblMessage = new Label();
            btnOnay = new Button();
            gbProcessSummary = new GroupBox();
            lblReport = new Label();
            btnCancel = new Button();
            pbProcess = new ProgressBar();
            lblRemain = new Label();
            gbBrowser.SuspendLayout();
            gbLogin.SuspendLayout();
            gbLoginInfo.SuspendLayout();
            gbProcessSummary.SuspendLayout();
            SuspendLayout();
            // 
            // gbBrowser
            // 
            gbBrowser.Controls.Add(cbHideBrowser);
            gbBrowser.Controls.Add(rbChrome);
            gbBrowser.Controls.Add(rbFirefox);
            gbBrowser.Location = new Point(18, 14);
            gbBrowser.Name = "gbBrowser";
            gbBrowser.Size = new Size(291, 85);
            gbBrowser.TabIndex = 0;
            gbBrowser.TabStop = false;
            gbBrowser.Text = "Browser Seçiniz";
            // 
            // cbHideBrowser
            // 
            cbHideBrowser.AutoSize = true;
            cbHideBrowser.Location = new Point(19, 57);
            cbHideBrowser.Name = "cbHideBrowser";
            cbHideBrowser.Size = new Size(170, 19);
            cbHideBrowser.TabIndex = 2;
            cbHideBrowser.Text = "Firefox'u arkaplanda çalıştır";
            cbHideBrowser.UseVisualStyleBackColor = true;
            cbHideBrowser.CheckedChanged += cbHideBrowser_CheckedChanged;
            // 
            // rbChrome
            // 
            rbChrome.AutoSize = true;
            rbChrome.Checked = true;
            rbChrome.Location = new Point(156, 22);
            rbChrome.Name = "rbChrome";
            rbChrome.Size = new Size(109, 19);
            rbChrome.TabIndex = 1;
            rbChrome.TabStop = true;
            rbChrome.Text = "Google Chrome";
            rbChrome.UseVisualStyleBackColor = true;
            rbChrome.CheckedChanged += rbChrome_CheckedChanged;
            // 
            // rbFirefox
            // 
            rbFirefox.AutoSize = true;
            rbFirefox.Location = new Point(19, 22);
            rbFirefox.Name = "rbFirefox";
            rbFirefox.Size = new Size(61, 19);
            rbFirefox.TabIndex = 0;
            rbFirefox.TabStop = true;
            rbFirefox.Text = "Firefox";
            rbFirefox.UseVisualStyleBackColor = true;
            // 
            // gbLogin
            // 
            gbLogin.Controls.Add(rbMebbis);
            gbLogin.Controls.Add(rbEdevlet);
            gbLogin.Location = new Point(18, 105);
            gbLogin.Name = "gbLogin";
            gbLogin.Size = new Size(291, 52);
            gbLogin.TabIndex = 0;
            gbLogin.TabStop = false;
            gbLogin.Text = "Giriş Yöntemi Seçiniz";
            // 
            // rbMebbis
            // 
            rbMebbis.AutoSize = true;
            rbMebbis.Location = new Point(156, 22);
            rbMebbis.Name = "rbMebbis";
            rbMebbis.Size = new Size(64, 19);
            rbMebbis.TabIndex = 4;
            rbMebbis.TabStop = true;
            rbMebbis.Text = "Mebbis";
            rbMebbis.UseVisualStyleBackColor = true;
            // 
            // rbEdevlet
            // 
            rbEdevlet.AutoSize = true;
            rbEdevlet.Checked = true;
            rbEdevlet.Location = new Point(19, 22);
            rbEdevlet.Name = "rbEdevlet";
            rbEdevlet.Size = new Size(69, 19);
            rbEdevlet.TabIndex = 3;
            rbEdevlet.TabStop = true;
            rbEdevlet.Text = "E-Devlet";
            rbEdevlet.UseVisualStyleBackColor = true;
            rbEdevlet.CheckedChanged += rbEdevlet_CheckedChanged;
            // 
            // gbLoginInfo
            // 
            gbLoginInfo.Controls.Add(texPass);
            gbLoginInfo.Controls.Add(cbRemember);
            gbLoginInfo.Controls.Add(textAddTcno);
            gbLoginInfo.Controls.Add(lblPass);
            gbLoginInfo.Controls.Add(lblTckn);
            gbLoginInfo.Location = new Point(18, 161);
            gbLoginInfo.Name = "gbLoginInfo";
            gbLoginInfo.Size = new Size(291, 130);
            gbLoginInfo.TabIndex = 0;
            gbLoginInfo.TabStop = false;
            gbLoginInfo.Text = "E-Devlet Giriş Bilgileri";
            // 
            // texPass
            // 
            texPass.Location = new Point(145, 54);
            texPass.Name = "texPass";
            texPass.PasswordChar = '*';
            texPass.Size = new Size(140, 23);
            texPass.TabIndex = 6;
            // 
            // cbRemember
            // 
            cbRemember.AutoSize = true;
            cbRemember.Location = new Point(19, 96);
            cbRemember.Name = "cbRemember";
            cbRemember.Size = new Size(85, 19);
            cbRemember.TabIndex = 7;
            cbRemember.Text = "Beni hatırla";
            cbRemember.UseVisualStyleBackColor = true;
            // 
            // textAddTcno
            // 
            textAddTcno.Location = new Point(145, 20);
            textAddTcno.MaxLength = 11;
            textAddTcno.Name = "textAddTcno";
            textAddTcno.Size = new Size(140, 23);
            textAddTcno.TabIndex = 5;
            textAddTcno.TextChanged += textAddTcno_TextChanged;
            textAddTcno.KeyPress += textAddTcno_KeyPress;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(19, 58);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(92, 15);
            lblPass.TabIndex = 0;
            lblPass.Text = "E-Devlet Şifreniz";
            // 
            // lblTckn
            // 
            lblTckn.AutoSize = true;
            lblTckn.Location = new Point(19, 24);
            lblTckn.Name = "lblTckn";
            lblTckn.Size = new Size(117, 15);
            lblTckn.TabIndex = 0;
            lblTckn.Text = "TC Kimlik Numaranız";
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(20, 294);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(289, 47);
            lblMessage.TabIndex = 4;
            lblMessage.Text = "label1";
            // 
            // btnOnay
            // 
            btnOnay.Cursor = Cursors.Hand;
            btnOnay.Location = new Point(18, 349);
            btnOnay.Name = "btnOnay";
            btnOnay.Size = new Size(291, 49);
            btnOnay.TabIndex = 8;
            btnOnay.Text = "ONAYLAMAYI BAŞLAT";
            btnOnay.UseVisualStyleBackColor = true;
            btnOnay.Click += btnOnay_Click;
            // 
            // gbProcessSummary
            // 
            gbProcessSummary.Controls.Add(lblRemain);
            gbProcessSummary.Controls.Add(lblReport);
            gbProcessSummary.Controls.Add(btnCancel);
            gbProcessSummary.Controls.Add(pbProcess);
            gbProcessSummary.Location = new Point(329, 14);
            gbProcessSummary.Name = "gbProcessSummary";
            gbProcessSummary.Size = new Size(393, 392);
            gbProcessSummary.TabIndex = 7;
            gbProcessSummary.TabStop = false;
            gbProcessSummary.Text = "İşlem Özeti";
            // 
            // lblReport
            // 
            lblReport.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblReport.ForeColor = Color.Red;
            lblReport.Location = new Point(18, 22);
            lblReport.Name = "lblReport";
            lblReport.Size = new Size(355, 240);
            lblReport.TabIndex = 9;
            lblReport.Text = "lblReport";
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Location = new Point(18, 335);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(355, 49);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "ONAYLAMA İŞLEMİNİ İPTAL ET";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // pbProcess
            // 
            pbProcess.Location = new Point(18, 304);
            pbProcess.Name = "pbProcess";
            pbProcess.Size = new Size(355, 23);
            pbProcess.Style = ProgressBarStyle.Continuous;
            pbProcess.TabIndex = 10;
            // 
            // lblRemain
            // 
            lblRemain.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblRemain.ForeColor = Color.Red;
            lblRemain.Location = new Point(18, 275);
            lblRemain.Name = "lblRemain";
            lblRemain.Size = new Size(355, 23);
            lblRemain.TabIndex = 12;
            lblRemain.Text = "lblRemain";
            lblRemain.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // fMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 407);
            Controls.Add(gbProcessSummary);
            Controls.Add(btnOnay);
            Controls.Add(lblMessage);
            Controls.Add(gbLoginInfo);
            Controls.Add(gbLogin);
            Controls.Add(gbBrowser);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(0, 446);
            Name = "fMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DYS OTOMATİK ONAYLAMA";
            HelpButtonClicked += fMain_HelpButtonClicked;
            Load += fMain_Load;
            gbBrowser.ResumeLayout(false);
            gbBrowser.PerformLayout();
            gbLogin.ResumeLayout(false);
            gbLogin.PerformLayout();
            gbLoginInfo.ResumeLayout(false);
            gbLoginInfo.PerformLayout();
            gbProcessSummary.ResumeLayout(false);
            ResumeLayout(false);
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
        private Label lblRemain;
    }
}
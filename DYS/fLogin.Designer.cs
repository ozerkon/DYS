namespace DYS
{
    partial class fLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fLogin));
            gbLoginInfo = new GroupBox();
            texPass = new TextBox();
            textAddTcno = new TextBox();
            lblPass = new Label();
            lblTckn = new Label();
            gbCaptcha = new GroupBox();
            panel1 = new Panel();
            pbCaptcha = new PictureBox();
            btnRefreshCaptcha = new Button();
            txtCaptcha = new TextBox();
            lblCode = new Label();
            btnLogin = new Button();
            btnCancel = new Button();
            lblMessage = new Label();
            gbLoginInfo.SuspendLayout();
            gbCaptcha.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbCaptcha).BeginInit();
            SuspendLayout();
            // 
            // gbLoginInfo
            // 
            gbLoginInfo.Controls.Add(texPass);
            gbLoginInfo.Controls.Add(textAddTcno);
            gbLoginInfo.Controls.Add(lblPass);
            gbLoginInfo.Controls.Add(lblTckn);
            gbLoginInfo.Location = new Point(12, 12);
            gbLoginInfo.Name = "gbLoginInfo";
            gbLoginInfo.Size = new Size(291, 98);
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
            texPass.TabIndex = 4;
            // 
            // textAddTcno
            // 
            textAddTcno.Location = new Point(145, 20);
            textAddTcno.MaxLength = 11;
            textAddTcno.Name = "textAddTcno";
            textAddTcno.Size = new Size(140, 23);
            textAddTcno.TabIndex = 2;
            textAddTcno.TextChanged += textAddTcno_TextChanged;
            textAddTcno.KeyPress += textAddTcno_KeyPress;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(19, 58);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(92, 15);
            lblPass.TabIndex = 3;
            lblPass.Text = "E-Devlet Şifreniz";
            // 
            // lblTckn
            // 
            lblTckn.AutoSize = true;
            lblTckn.Location = new Point(19, 24);
            lblTckn.Name = "lblTckn";
            lblTckn.Size = new Size(117, 15);
            lblTckn.TabIndex = 1;
            lblTckn.Text = "TC Kimlik Numaranız";
            // 
            // gbCaptcha
            // 
            gbCaptcha.Controls.Add(panel1);
            gbCaptcha.Controls.Add(btnRefreshCaptcha);
            gbCaptcha.Controls.Add(txtCaptcha);
            gbCaptcha.Controls.Add(lblCode);
            gbCaptcha.Location = new Point(16, 119);
            gbCaptcha.Name = "gbCaptcha";
            gbCaptcha.Size = new Size(291, 181);
            gbCaptcha.TabIndex = 5;
            gbCaptcha.TabStop = false;
            gbCaptcha.Text = "Güvenlik Kodu Girişi";
            // 
            // panel1
            // 
            panel1.Controls.Add(pbCaptcha);
            panel1.Location = new Point(6, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(276, 78);
            panel1.TabIndex = 6;
            // 
            // pbCaptcha
            // 
            pbCaptcha.Location = new Point(27, 16);
            pbCaptcha.Name = "pbCaptcha";
            pbCaptcha.Size = new Size(223, 43);
            pbCaptcha.TabIndex = 0;
            pbCaptcha.TabStop = false;
            // 
            // btnRefreshCaptcha
            // 
            btnRefreshCaptcha.Image = Properties.Resources.refresh;
            btnRefreshCaptcha.ImageAlign = ContentAlignment.MiddleLeft;
            btnRefreshCaptcha.Location = new Point(20, 145);
            btnRefreshCaptcha.Name = "btnRefreshCaptcha";
            btnRefreshCaptcha.Size = new Size(261, 30);
            btnRefreshCaptcha.TabIndex = 9;
            btnRefreshCaptcha.Text = "Güvenlik Kodunu Yenile";
            btnRefreshCaptcha.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefreshCaptcha.UseVisualStyleBackColor = true;
            btnRefreshCaptcha.Click += btnRefreshCaptcha_Click;
            // 
            // txtCaptcha
            // 
            txtCaptcha.Location = new Point(145, 113);
            txtCaptcha.MaxLength = 11;
            txtCaptcha.Name = "txtCaptcha";
            txtCaptcha.Size = new Size(140, 23);
            txtCaptcha.TabIndex = 8;
            txtCaptcha.TextChanged += txtCaptcha_TextChanged;
            txtCaptcha.KeyPress += txtCaptcha_KeyPress;
            // 
            // lblCode
            // 
            lblCode.AutoSize = true;
            lblCode.Location = new Point(15, 117);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(84, 15);
            lblCode.TabIndex = 7;
            lblCode.Text = "Güvenlik Kodu";
            // 
            // btnLogin
            // 
            btnLogin.Image = Properties.Resources.login_32;
            btnLogin.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogin.Location = new Point(49, 354);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(99, 39);
            btnLogin.TabIndex = 11;
            btnLogin.Text = "GİRİŞ";
            btnLogin.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnCancel
            // 
            btnCancel.Image = Properties.Resources.cancel_32;
            btnCancel.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancel.Location = new Point(157, 354);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(99, 39);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "VAZGEÇ";
            btnCancel.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // lblMessage
            // 
            lblMessage.ForeColor = Color.Red;
            lblMessage.Location = new Point(16, 305);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(291, 46);
            lblMessage.TabIndex = 10;
            lblMessage.Text = "label1";
            // 
            // fLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(329, 404);
            ControlBox = false;
            Controls.Add(lblMessage);
            Controls.Add(btnCancel);
            Controls.Add(btnLogin);
            Controls.Add(gbCaptcha);
            Controls.Add(gbLoginInfo);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(345, 443);
            MinimizeBox = false;
            MinimumSize = new Size(345, 443);
            Name = "fLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "fLogin";
            TopMost = true;
            Load += fLogin_Load;
            gbLoginInfo.ResumeLayout(false);
            gbLoginInfo.PerformLayout();
            gbCaptcha.ResumeLayout(false);
            gbCaptcha.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbCaptcha).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbLoginInfo;
        private TextBox texPass;
        private TextBox textAddTcno;
        private Label lblPass;
        private Label lblTckn;
        private GroupBox gbCaptcha;
        private PictureBox pbCaptcha;
        private TextBox txtCaptcha;
        private Label lblCode;
        private Button btnLogin;
        private Button btnRefreshCaptcha;
        private Button btnCancel;
        private Label lblMessage;
        private Panel panel1;
    }
}
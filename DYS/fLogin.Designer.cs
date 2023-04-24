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
            this.gbLoginInfo = new System.Windows.Forms.GroupBox();
            this.texPass = new System.Windows.Forms.TextBox();
            this.textAddTcno = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblTckn = new System.Windows.Forms.Label();
            this.gbCaptcha = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbCaptcha = new System.Windows.Forms.PictureBox();
            this.btnRefreshCaptcha = new System.Windows.Forms.Button();
            this.txtCaptcha = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.gbLoginInfo.SuspendLayout();
            this.gbCaptcha.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // gbLoginInfo
            // 
            this.gbLoginInfo.Controls.Add(this.texPass);
            this.gbLoginInfo.Controls.Add(this.textAddTcno);
            this.gbLoginInfo.Controls.Add(this.lblPass);
            this.gbLoginInfo.Controls.Add(this.lblTckn);
            this.gbLoginInfo.Location = new System.Drawing.Point(12, 12);
            this.gbLoginInfo.Name = "gbLoginInfo";
            this.gbLoginInfo.Size = new System.Drawing.Size(291, 98);
            this.gbLoginInfo.TabIndex = 1;
            this.gbLoginInfo.TabStop = false;
            this.gbLoginInfo.Text = "E-Devlet Giriş Bilgileri";
            // 
            // texPass
            // 
            this.texPass.Location = new System.Drawing.Point(145, 54);
            this.texPass.Name = "texPass";
            this.texPass.PasswordChar = '*';
            this.texPass.Size = new System.Drawing.Size(140, 23);
            this.texPass.TabIndex = 3;
            // 
            // textAddTcno
            // 
            this.textAddTcno.Location = new System.Drawing.Point(145, 20);
            this.textAddTcno.MaxLength = 11;
            this.textAddTcno.Name = "textAddTcno";
            this.textAddTcno.Size = new System.Drawing.Size(140, 23);
            this.textAddTcno.TabIndex = 2;
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
            // gbCaptcha
            // 
            this.gbCaptcha.Controls.Add(this.panel1);
            this.gbCaptcha.Controls.Add(this.btnRefreshCaptcha);
            this.gbCaptcha.Controls.Add(this.txtCaptcha);
            this.gbCaptcha.Controls.Add(this.label1);
            this.gbCaptcha.Location = new System.Drawing.Point(16, 119);
            this.gbCaptcha.Name = "gbCaptcha";
            this.gbCaptcha.Size = new System.Drawing.Size(291, 181);
            this.gbCaptcha.TabIndex = 2;
            this.gbCaptcha.TabStop = false;
            this.gbCaptcha.Text = "Güvenlik Kodu Girişi";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbCaptcha);
            this.panel1.Location = new System.Drawing.Point(6, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 78);
            this.panel1.TabIndex = 4;
            // 
            // pbCaptcha
            // 
            this.pbCaptcha.Location = new System.Drawing.Point(27, 16);
            this.pbCaptcha.Name = "pbCaptcha";
            this.pbCaptcha.Size = new System.Drawing.Size(223, 43);
            this.pbCaptcha.TabIndex = 0;
            this.pbCaptcha.TabStop = false;
            // 
            // btnRefreshCaptcha
            // 
            this.btnRefreshCaptcha.Image = global::DYS.Properties.Resources.refresh;
            this.btnRefreshCaptcha.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRefreshCaptcha.Location = new System.Drawing.Point(20, 145);
            this.btnRefreshCaptcha.Name = "btnRefreshCaptcha";
            this.btnRefreshCaptcha.Size = new System.Drawing.Size(261, 30);
            this.btnRefreshCaptcha.TabIndex = 3;
            this.btnRefreshCaptcha.Text = "Güvenlik Kodunu Yenile";
            this.btnRefreshCaptcha.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefreshCaptcha.UseVisualStyleBackColor = true;
            this.btnRefreshCaptcha.Click += new System.EventHandler(this.btnRefreshCaptcha_Click);
            // 
            // txtCaptcha
            // 
            this.txtCaptcha.Location = new System.Drawing.Point(145, 113);
            this.txtCaptcha.MaxLength = 11;
            this.txtCaptcha.Name = "txtCaptcha";
            this.txtCaptcha.Size = new System.Drawing.Size(140, 23);
            this.txtCaptcha.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Güvenlik Kodu";
            // 
            // btnLogin
            // 
            this.btnLogin.Image = global::DYS.Properties.Resources.login_32;
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.Location = new System.Drawing.Point(49, 354);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(99, 39);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "GİRİŞ";
            this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::DYS.Properties.Resources.cancel_32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(157, 354);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 39);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "VAZGEÇ";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(16, 305);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(291, 46);
            this.lblMessage.TabIndex = 7;
            this.lblMessage.Text = "label1";
            // 
            // fLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(329, 404);
            this.ControlBox = false;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.gbCaptcha);
            this.Controls.Add(this.gbLoginInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(345, 443);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(345, 443);
            this.Name = "fLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fLogin";
            this.Load += new System.EventHandler(this.fLogin_Load);
            this.gbLoginInfo.ResumeLayout(false);
            this.gbLoginInfo.PerformLayout();
            this.gbCaptcha.ResumeLayout(false);
            this.gbCaptcha.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptcha)).EndInit();
            this.ResumeLayout(false);

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
        private Label label1;
        private Button btnLogin;
        private Button btnRefreshCaptcha;
        private Button btnCancel;
        private Label lblMessage;
        private Panel panel1;
    }
}
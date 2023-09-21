using DYS.Captcha;
using DYS.Helpers;
using DYS.Properties;
using Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System.Text;

namespace DYS
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
            Width= 335;
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            textAddTcno.Text = Settings.Default.tckn;
            if (Settings.Default.browser == "0") { Common.browser = "0"; rbFirefox.Checked = true; cbHideBrowser.Text = "Firefox'u arkaplanda �al��t�r"; }
            else if (Settings.Default.browser == "1") { Common.browser = "1"; rbChrome.Checked = true; cbHideBrowser.Text = "Chrome'u arkaplanda �al��t�r"; }

            if (Settings.Default.loginType == "0") { rbEdevlet.Checked = true; texPass.Text = Settings.Default.passED; }
            else if (Settings.Default.loginType == "1") { rbMebbis.Checked = true; texPass.Text = Settings.Default.passMEB; }

            cbHideBrowser.Checked = Settings.Default.hideBrowser? true: false;
            CheckForIllegalCrossThreadCalls = false;
            BringFront();

        }
        private void InitializeBgwConfirm()
        {
            Common.bgwConfirm.WorkerSupportsCancellation = true;
            Common.bgwConfirm.DoWork += new DoWorkEventHandler(BgwConfirmDoWork);
            Common.bgwConfirm.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgwConfirmComplated);
        }

        #region controls
        private void btnOnay_Click(object sender, EventArgs e)
        {
            Common.bgwConfirm = new BackgroundWorker();
            Common.mebAjandaActive = false;
            InitializeBgwConfirm();
            if (textAddTcno.Text.Trim() == "" || textAddTcno.Text.Length != 11 || !Common.rx.IsMatch(textAddTcno.Text.Trim()))
            {
                lblMessage.Text = "TC Kimlik numaras� hatal�!"; textAddTcno.Focus(); return;
            }
            else { lblMessage.Text = ""; Common.tckn = textAddTcno.Text.Trim(); }
            if (texPass.Text.Trim() == "")
            {
                lblMessage.Text = rbMebbis.Checked ? "Mebbis �ifresi girilmedi!" : "E-devlet �ifresi girilmedi"; texPass.Focus(); return;
            }
            else { lblMessage.Text = ""; Common.pass = texPass.Text.Trim(); }

            if (!Common.bgwConfirm.IsBusy)
            {
                Common.bgwConfirm.RunWorkerAsync();
            }
            ProcessStarted();


        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.cancelProcess = true;
            lblReport.Text += "\r\n�PTAL TALEB� ALINDI! L�TFEN BEKLEY�N�Z\r\n";
            btnCancel.Text = "ONAYLAMA ��LEM� �PTAL ED�L�YOR...";
            btnCancel.Enabled = false;
            if (Common.bgwConfirm.IsBusy)
            {
                Common.bgwConfirm.CancelAsync();
            }
        }
        private void rbChrome_CheckedChanged(object sender, EventArgs e)
        {
            if (rbChrome.Checked) { cbHideBrowser.Text = "Chrome'u arkaplanda �al��t�r"; Common.browser = "1"; Settings.Default.browser = "1"; }
            else { cbHideBrowser.Text = "Firefox'u arkaplanda �al��t�r"; Common.browser = "0"; Settings.Default.browser = "0"; }
            Settings.Default.Save();
        }
        private void rbEdevlet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMebbis.Checked == true)
            {
                gbLoginInfo.Text = "Mebbis Bilgileriniz";
                lblPass.Text = "Mebbis �ifreniz";
                texPass.Text = Settings.Default.passMEB;
                Common.loginType = "1";
            }
            else
            {
                gbLoginInfo.Text = "E-devlet Bilgileriniz";
                lblPass.Text = "E-devlet �ifreniz";
                texPass.Text = Settings.Default.passED;
                Common.loginType = "0";
            }
        }
        private void cbHideBrowser_CheckedChanged(object sender, EventArgs e)
        {
            Common.hideBrowser = cbHideBrowser.Checked ? true : false;
        }
        private void textAddTcno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b' || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void textAddTcno_TextChanged(object sender, EventArgs e)
        {
            if (textAddTcno.Text.Trim() == "")
            {
                lblMessage.Text = String.Empty;
                textAddTcno.Clear();
            }
        }
        #endregion

        #region bgwConfirm
        private void BgwConfirmDoWork(object? sender, DoWorkEventArgs e)
        {
            e.Cancel = false; 
            lblMessage.Text =  string.Empty;
            Common.error = true; Common.loggedIn = false; Common.cancel = false; Common.flipCount = 0;
            Common.driver = WinHelpers.GetWebDriver(Common.hideBrowser, out Common.msg);
            if(Common.driver == null && Common.msg.Contains("This version of ChromeDriver only supports Chrome version"))
            {
                lblMessage.Text = "Chrome s�r�m� g�ncel de�il. L�tfen �nce google chrome'u g�ncelleyip tekrar deneyin"; return;
            }
            Common.wait = new WebDriverWait(Common.driver, TimeSpan.FromSeconds(30));
            lblReport.Text += Common.browser == "0"? "* Firefox ba�lat�l�yor...\r\n" : "* Chrome ba�lat�l�yor...\r\n";
            Common.driver.Url = "https://mebbis.meb.gov.tr/default.aspx";
            WinHelpers.WaitForPageLoad(out Common.msg);
            if (Common.bgwConfirm.CancellationPending == true) { e.Cancel = true; return; }
            while (!Common.driver.PageSource.Contains("�ifremi Unuttum?"))
            {
                Thread.Sleep(500);
            }
            Common.driver.Manage().Window.Maximize();
            if (Common.loginType == "0")
            {
                Common.tcknTbID = "tridField";
                Common.passTbID = "egpField";
                Common.pictureCaptcha = "captchaImage";
                Common.tcknCaptcha = "captchaField";
                Common.btnLogin = "submitButton";

                IWebElement edevletlink = WinHelpers.GetElementBy("i", "edevletlink", out Common.msg);
                edevletlink.Click();
                if (!Common.driver.PageSource.Contains("temin edilebilmektedir"))
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Thread.Sleep(500);
                    }
                    if (Common.bgwConfirm.CancellationPending == true) { e.Cancel = true; return; }
                }
                int tryCount = 1;
                while (!Common.loggedIn && Common.error)
                {
                    Common.loggedIn = EdevletGiris();
                    tryCount++;
                    if (tryCount == 4 || Common.cancel) { lblMessage.Text = "Oturum a�ma ba�ar�s�z oldu."; return; }
                    if (Common.bgwConfirm.CancellationPending == true) { e.Cancel = true; return; }
                }
            }
            else
            {
                Common.tcknTbID = "txtKullaniciAd";
                Common.passTbID = "txtSifre";
                Common.pictureCaptcha = "imgKontrol";
                Common.tcknCaptcha = "txtGuvenlikKod";
                Common.btnLogin = "btnGiris";
                int tryCount = 1;
                while (!Common.loggedIn && Common.error)
                {
                    Common.loggedIn = MebbisGiris();
                    tryCount++;
                    if (tryCount == 4 || Common.cancel) { lblMessage.Text = "Oturum a�ma ba�ar�s�z oldu."; return; }
                    if (Common.bgwConfirm.CancellationPending == true) { e.Cancel = true; return; }
                }
            }
            if (!ConfirmMessages(out Common.msg)) {
                if (Common.cancelProcess) { btnCancel_Click(btnCancel, new EventArgs()); e.Cancel = true;}
                 return; 
            }
            
        }
        private void BgwConfirmComplated(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {

                lblMessage.Text = $"Onaylama s�ras�nda hata olu�tu! {e.Error.Message}";

            }
            else if (e.Cancelled)
            {
                lblMessage.Text = $"Onaylama iptal edildi!";
            }
            else
            {
                if(lblMessage.Text.Contains("Chrome s�r�m� g�ncel de�il")) { }
                else if (Common.confirmError == true)
                {
                    lblMessage.Text = Common.isManager ? $"0naylama i�lemi hatalarla tamamland�.\r\n{Common.totalMessages} adet mesajdan {Common.confirmedMessages} adedi onayland�\r\n{Common.nonConfirmedMessages} adet mesaj� DYS program� ile onaylaman�z gerekli" : $"0naylama i�lemi hatalarla tamamland�.\r\n{Common.totalMessages} adet mesajdan {Common.confirmedMessages} adedi onayland�";
                }
                else if(Common.totalMessages > 0)
                {
                    lblMessage.Text = Common.isManager? $"0naylama i�lemi hatas�z tamamland�.\r\n{Common.confirmedMessages} adet mesaj onayland�\r\n{Common.nonConfirmedMessages} adet mesaj� DYS program� ile onaylaman�z gerekli" : $"0naylama i�lemi hatas�z tamamland�.\r\n{Common.confirmedMessages} adet mesaj onayland�";

                }
                else if (Common.totalMessages == 0)
                {
                    lblMessage.Text = $"0naylanmam�� mesaj yok";
                }

            }
            Common.bgwConfirm.Dispose();
            Thread.Sleep(3000);
            ProcessFinished();
        }
        public bool ConfirmMessages(out string msg)
        {
            lblReport.Text += "* mebbis.meb.gov.tr sayfas� a��ld�.\r\n";
            msg = "";
            WinHelpers.WaitForPageLoad(out Common.msg);
            if (Common.driver.PageSource.Contains("Do�rulamas� Aktif"))
            {
                Common.mebAjandaActive = true;
                fLogin f = new fLogin(true);
                while (Common.driver.PageSource.Contains("Do�rulamas� Aktif") || Common.driver.PageSource.Contains("Do�rulama Ger�ekle�medi"))
                {
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        Common.mebAjandaText = WinHelpers.GetElementBy("i", "txtDogrulamaKodu", out Common.msg);
                        Common.mebAjandaButton = WinHelpers.GetElementBy("i", "btnDogrula", out Common.msg);
                        if (Common.mebAjandaText != null && Common.mebAjandaButton != null)
                        {
                            Common.mebAjandaText.Clear();
                            Common.mebAjandaText.SendKeys(Common.captcha);
                            Common.mebAjandaButton.Click();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "��lem iptal edildi"; Common.driver.Dispose(); return false;
                    }
                }
            }

            IWebElement? dysFlipMenu = GetDysLink();
            if (dysFlipMenu == null) { lblMessage.Text = "DYS giri� linki bulunamad�\r\nL�tfen daha sonra tekrar deneyin"; Common.driver.Dispose();  return false; }
            Actions action = new Actions(Common.driver);
            action.MoveToElement(dysFlipMenu).Perform();

            int tryCount = 0;
            Thread.Sleep(1000);
            if (Common.bgwConfirm.CancellationPending ) { return false; }
            IWebElement? dysgiris = GetLoginLink();
            if (dysgiris == null) { lblMessage.Text = "DYS giri� linki bulunamad�\r\nL�tfen daha sonra tekrar deneyin"; Common.driver.Dispose(); return false; }
            dysgiris.Click();

            

            if (Common.driver.WindowHandles.Count > 1)
            {
                Common.driver.SwitchTo().Window(Common.driver.WindowHandles[1]);
                WinHelpers.WaitForPageLoad(out Common.msg);
                if (Common.bgwConfirm.CancellationPending) { return false; }
            }
            tryCount = 0;
            lblReport.Text += "* dysweb.meb.gov.tr sekmesine ge�ildi.\r\n";
            List<IWebElement>? tree = null;

            while (tree == null)
            {
                Thread.Sleep(100);
                tree = Common.driver.FindElements(By.CssSelector("li[id^='menuForm:roller_tree:']")).ToList();
                tryCount++;
                if (tryCount == 300) { lblMessage.Text = "Sayfa 30 saniye boyunca yan�t vermedi�i i�in i�lem iptal edildi"; Common.driver.Dispose(); return false; }
                if (Common.bgwConfirm.CancellationPending) { return false; }
            }

            if (tree.Count > 9)
            {
                Common.isManager= true;
                Common.mdLink = GetMdLink();
                if (Common.mdLink == null) { lblMessage.Text = "�dareci mesajlar� listelenemdi\r\nL�tfen daha sonra tekrar deneyin"; Common.driver.Dispose(); return false; }
                Common.mdLink.Click();
                if (Common.bgwConfirm.CancellationPending) { return false; }
            }

            while (!Common.driver.PageSource.Contains("listelenmektedir"))
            {
                Thread.Sleep(100);
                if (Common.driver.PageSource.Contains("evrak bulunmamaktad�r")) { lblReport.Text += "* Onaylanmam�� mesaj yok.\r\n"; lblMessage.Text = "Onaylanmam�� mesaj yok"; return false; }
                tryCount++;
                if (tryCount == 300) { lblReport.Text += "* Sayfa 30 saniye boyunca yan�t vermedi�i i�in i�lem iptal edildi.\r\n"; lblMessage.Text = "Sayfa 30 saniye boyunca yan�t vermedi�i i�in i�lem iptal edildi"; Common.driver.Dispose(); return false; }
                if (Common.bgwConfirm.CancellationPending) { return false; }
            }
            IWebElement isler = WinHelpers.GetElementBy("i", "form:etiketFilter", out Common.msg);

            string sayi = isler.Text.Substring(23, isler.Text.Substring(23).IndexOf(" "));
            
            try
            {
                Common.totalMessages = Convert.ToInt32(sayi);
                pbProcess.Maximum = Common.totalMessages;
                lblReport.Text += $"* {Common.totalMessages} adet okunmam�� mesaj bulundu.\r\n";
                Common.ilksatir = WinHelpers.GetButtonElementBy("x", "/html/body/div[2]/div[4]/form/div[1]/div/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]", out Common.msg);
                Common.ilksatir?.Click();
                while (!Common.driver.PageSource.Contains("Gelen Evrak G�zden Ge�irme") && !Common.driver.PageSource.Contains("Evrak G�r�nt�leme"))
                {
                    Thread.Sleep(1000);
                    if (Common.bgwConfirm.CancellationPending) { return false; }
                }

                lblReport.Text += "* Onaylama i�lemi ba�lat�l�yor...\r\n";

                for (int i = 0; i < Common.totalMessages; i++)
                {
                    while (!Common.driver.PageSource.Contains("Gelen Evrak G�zden Ge�irme") && !Common.driver.PageSource.Contains("Evrak G�r�nt�leme"))
                    {
                        Thread.Sleep(100);
                        if (Common.bgwConfirm.CancellationPending) { return false; }
                    }

                    IList<IWebElement> hiddenElements = Common.driver.FindElements(By.CssSelector("ui-dialog"));
                    foreach (IWebElement item in hiddenElements)
                    {
                        Common.wait?.Until(e => ((IJavaScriptExecutor)Common.driver).ExecuteScript("arguments[0].setAttribute('style', 'display:none')", item));
                        if (Common.bgwConfirm.CancellationPending) { return false; }
                    }

                    IWebElement? iframe = null;
                    while (iframe == null)
                    {
                        iframe = WinHelpers.GetElementBy("i", "gozdenGecirmeEkraniId", out Common.msg);
                        Thread.Sleep(500);
                        if (Common.bgwConfirm.CancellationPending) { return false; }
                        if (Screen.PrimaryScreen?.Bounds.Height < 1080)  WinHelpers.ZoomOut();
                    }
                    Common.driver.SwitchTo().Frame("gozdenGecirmeEkraniId");

                    IWebElement? okudum = null;
                    if (Common.driver.PageSource.Contains("Okudum")) { okudum = WinHelpers.GetButtonElementBy("x", "//*[@id=\"formspanel:okudumBtn\"]", out Common.msg); }
                    if (okudum != null) { 
                        okudum.Click();
                        Common.confirmedMessages++;
                        pbProcess.Value = Common.confirmedMessages;
                        if (Common.bgwConfirm.CancellationPending) { return false; } }
                    else
                    {
                        IWebElement? kapat = WinHelpers.GetButtonElementBy("x", "//*[@id=\"formspanel:_08001kapat2\"]", out Common.msg);
                        if (kapat != null) { 
                            kapat.Click(); 
                            Common.nonConfirmedMessages++;
                            if (Common.bgwConfirm.CancellationPending) { return false; } 
                        }
                        continue;
                    }
                    while (!Common.driver.PageSource.Contains("Gelen Evrak G�zden Ge�irme"))
                    {
                        Thread.Sleep(1000);
                        if (Common.bgwConfirm.CancellationPending) { return false; }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Common.confirmError = true;
                msg = ex.Message;
                return false;
            }
        }
        private void ProcessStarted()
        {
            Common.totalMessages = 0;
            Common.confirmedMessages= 0;
            Common.nonConfirmedMessages= 0;
            Common.confirmError = false;
            Common.isManager = false;
            Cursor.Current = Cursors.WaitCursor;
            btnCancel.Text = "ONAYLAMA ��LEM�N� �PTAL ET";
            btnCancel.Enabled = true;
            lblReport.Text = string.Empty;
            gbBrowser.Enabled = false;
            gbLogin.Enabled = false;
            gbLoginInfo.Enabled = false;
            btnOnay.Enabled = false;
            Width = 752;
            SaveSettings();
        }
        private void ProcessFinished()
        {
            gbBrowser.Enabled = true;
            gbLogin.Enabled = true;
            gbLoginInfo.Enabled = true;
            btnOnay.Enabled = true;
            Cursor.Current = Cursors.Default;
            if(Common.driver != null) Common.driver.Dispose();
            Width = 335;
        }
        #endregion

        #region methods
        public bool EdevletGiris()
        {
            Common.tcknWE = WinHelpers.GetElementBy("i", Common.tcknTbID, out Common.msg);
            Common.tcknWE.Clear();
            Common.tcknWE.SendKeys(Common.tckn);
            Common.passWE = WinHelpers.GetElementBy("i", Common.passTbID, out Common.msg);
            Common.passWE.Clear();
            Common.passWE.SendKeys(Common.pass);
            Common.giris = WinHelpers.GetButtonElementBy("n", Common.btnLogin, out Common.msg);
            if (Common.driver.PageSource.Contains("G�venlik Kodu"))
            {
                Common.captchaWE = WinHelpers.GetElementBy("i", Common.tcknCaptcha, out Common.msg);
                Common.captchaWE.Clear();
                Common.captchaWE.SendKeys(Common.captcha);
            }

            Common.giris.Click();
            WinHelpers.WaitForPageLoad(out Common.msg);
            if (Common.driver.PageSource.Contains("Mebbis Kullan�c�n�z Bulunmamaktad�r"))
            {
                lblMessage.Text = "Mebbsis �yel�iniz yoktur"; Common.error = false; Common.loggedIn = false; return false;
            }
            if (Common.driver.PageSource.Contains("�st �ste ba�ar�s�z giri�"))
            {
                MessageBox.Show($"�st �ste ba�ar�s�z giri� denemeleri yapt���n�z i�in hesab�n�z bir saatli�ine ask�ya al�nm��t�r. L�tfen daha sonra tekrar deneyiniz ya da Mebbis �ifresi ile giri� yap�n�z.", "E-devlet giri� hatas�!"); Common.error = false; Common.loggedIn = false; return false;
            }

            if (Common.driver.PageSource.Contains("Kimlik no veya �ifre hatal�d�r") || Common.driver.PageSource.Contains("L�tfen formdaki hatal� alanlar� d�zelterek yeniden deneyiniz"))
            {
                Common.tckn = ""; Common.pass = ""; fLogin f; Common.error = true;

                if (Common.driver.PageSource.Contains("G�venlik Kodu"))
                {
                    Common.captchaType = "x";
                    Common.captchaValue = "/html/body/div[1]/main/section[2]/form/fieldset/div[3]/div/img";
                    Common.rcbType = "x";
                    Common.rcbValue = "/html/body/div[1]/main/section[2]/form/fieldset/div[3]/div/img";

                    IWebElement captchaPic = WinHelpers.GetElementBy(Common.captchaType, Common.captchaValue, out Common.msg);
                    Common.bitmap = null;
                    for (int i = 0; i < 3; i++)
                    {
                        Common.bitmap = CaptchaService.TakeScreenshot(captchaPic, out Common.msg);
                        if (Common.bitmap != null) break;
                    }

                    if (Common.bitmap == null)
                    {
                        lblMessage.Text = "G�venlik Kodu bulunamad�\r\nl�tfen daha sonra tekrar deneyin";
                        return false;
                    }
                    else
                    {
                        f = new fLogin(true);
                    }
                }
                else
                {
                    f = new fLogin(false);
                }
                if (f.ShowDialog() == DialogResult.OK) EdevletGiris();
                else { Common.error = false; Common.loggedIn = false; Common.driver.Dispose(); lblMessage.Text = "��lem iptal edildi"; Common.cancel = true; return false; }
            }
            Common.loggedIn = true;
            return true;
        }
        public bool MebbisGiris()
        {
            if (Common.driver.PageSource.Contains("Kullan�c� Ad� veya �ifre yanl��"))
            {
                fLogin f; Common.error = true;
                Common.captchaType = "i";
                Common.captchaValue = "imgKontrol";

                IWebElement captchaPic = WinHelpers.GetElementBy(Common.captchaType, Common.captchaValue, out Common.msg);
                Common.bitmap = null;
                for (int i = 0; i < 3; i++)
                {
                    Common.bitmap = CaptchaService.TakeScreenshot(captchaPic, out Common.msg);
                    if (Common.bitmap != null) break;
                }

                if (Common.bitmap == null)
                {
                    lblMessage.Text = "G�venlik Kodu bulunamad�\r\nl�tfen daha sonra tekrar deneyin";
                    return false;
                }
                else
                {
                    f = new fLogin(true);
                }
                if (f.ShowDialog() == DialogResult.OK)
                {
                    Common.captchaWE = WinHelpers.GetElementBy("i", Common.tcknCaptcha, out Common.msg);
                    Common.captchaWE.Clear();
                    Common.captchaWE.SendKeys(Common.captcha);
                }
                else { Common.error = false; Common.loggedIn = false; Common.driver.Dispose(); lblMessage.Text = "��lem iptal edildi"; Common.cancel = true; return false; }
            }
            Common.tcknWE = WinHelpers.GetElementBy("i", Common.tcknTbID, out Common.msg);
            Common.tcknWE.Clear();
            Common.tcknWE.SendKeys(Common.tckn);
            Common.passWE = WinHelpers.GetElementBy("i", Common.passTbID, out Common.msg);
            Common.passWE.Clear();
            Common.passWE.SendKeys(Common.pass);
            Common.giris = WinHelpers.GetButtonElementBy("n", Common.btnLogin, out Common.msg);
            Common.giris.Click();

            WinHelpers.WaitForPageLoad(out Common.msg);
            if (Common.driver.PageSource.Contains("G�venlik Kodunu Yanl�� Girdiniz"))
            {
                lblMessage.Text = "G�venlik kodu yanl�� girildi"; Common.error = true; Common.loggedIn = false; MebbisGiris();
            }
            else if (Common.driver.PageSource.Contains("Kullan�c� Ad� veya �ifre yanl��"))
            {
                lblMessage.Text = "Kullan�c� ad� veya �ifre yanl��"; Common.error = true; Common.loggedIn = false; MebbisGiris();
            }
            Common.loggedIn = true;
            return true;
        }
        public IWebElement? GetLoginLink()
        {
            int tryCount = 0;
            IWebElement? dysgiris = null;
            Common.dysLink = $"rptProjeler_ctl0{Common.flipCount}_rptKullanicilar_ctl00_LinkButton1";
            while (dysgiris == null)
            {
                dysgiris = WinHelpers.GetButtonElementBy("i", Common.dysLink, out Common.msg); Thread.Sleep(1000); tryCount++;
                if (tryCount == 10) { return null; }
            }

            if (!dysgiris.Text.Contains("DYS WEB"))
            {
                dysgiris = null;
                Common.dysLink = $"rptProjeler_ctl0{Common.flipCount}_rptKullanicilar_ctl01_LinkButton1";
                while (dysgiris == null)
                {
                    dysgiris = WinHelpers.GetButtonElementBy("i", Common.dysLink, out Common.msg); Thread.Sleep(1000); tryCount++;
                    if (tryCount == 10) { return null; }
                }
            }
            return dysgiris;
        }
        public IWebElement? GetMdLink()
        {
            int tryCount = 0;
            IWebElement? mdLink = null;
            while (mdLink == null)
            {
                tryCount++;
                mdLink = WinHelpers.GetElementBy("i", "menuForm:roller_tree:2", out Common.msg);
                if (mdLink.Text.Contains("M�d�r")) { return mdLink; }
                mdLink = WinHelpers.GetElementBy("i", "menuForm:roller_tree:1", out Common.msg);
                if (mdLink.Text.Contains("M�d�r")) { return mdLink; }
                mdLink = WinHelpers.GetElementBy("i", "menuForm:roller_tree:0", out Common.msg);
                if (mdLink.Text.Contains("M�d�r")) { return mdLink; }
                if (tryCount == 10) { return null; }
            }

            return mdLink;
        }
        public IWebElement? GetDysLink()
        {
            int tryCount = 0;
            IWebElement? dsyLink = null;
            List<IWebElement> flips = WinHelpers.GetElementsBy("c", "image-flip", out Common.msg);
            while (flips == null)
            {
                tryCount++;
                flips = WinHelpers.GetElementsBy("c", "image-flip", out Common.msg);
                if (tryCount == 10) { return null; }
            }
            foreach (IWebElement flip in flips)
            {
                if (flip.Text.Contains("DYS")) { dsyLink = flip; break; }
                else { Common.flipCount++; }
            }

            return dsyLink;
        }
        public void BringFront()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void SaveSettings()
        {
            Settings.Default.hideBrowser = Common.hideBrowser;
            Settings.Default.tckn = cbRemember.Checked ? Common.tckn : string.Empty;
            if (Common.loginType == "0") Settings.Default.passED = cbRemember.Checked ? Common.pass : string.Empty;
            else if (Common.loginType == "1") Settings.Default.passMEB = cbRemember.Checked ? Common.pass : string.Empty;
            Settings.Default.loginType = Common.loginType;
            Settings.Default.Save();
        }
        #endregion

        private void fMain_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            fAbout f = new fAbout();
            f.ShowDialog();
        }
    }
}
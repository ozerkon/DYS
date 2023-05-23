using DYS.Captcha;
using DYS.Helpers;
using DYS.Properties;
using Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DYS
{
    public partial class fMain : Form
    {
        string tcknTbID = "";
        string passTbID = "";
        string pictureCaptcha = "";
        string tcknCaptcha = "";
        string btnLogin = "";
        string dysLink = "";

        IWebElement tcknWE;
        IWebElement passWE;
        IWebElement giris;
        IWebElement captchaWE;

        Regex rx = new Regex(@"[0-9]{1}[0-9]{9}[02468]{1}");
        string? msg;
        WebDriverWait? wait = null;
        bool logged = false, error;

        public fMain()
        {
            InitializeComponent();
        }
        private void fMain_Load(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            textAddTcno.Text = Settings.Default.tckn;
            if (Settings.Default.browser == "0") { Common.browser = "0"; rbFirefox.Checked = true; }
            else if (Settings.Default.browser == "1") { Common.browser = "1"; rbChrome.Checked = true; }

            if (Settings.Default.login == "0") { rbEdevlet.Checked = true; texPass.Text = Settings.Default.passED; }
            else if (Settings.Default.login == "1") { rbMebbis.Checked = true; texPass.Text = Settings.Default.passMEB; }

            BringFront();

        }
        private void btnOnay_Click(object sender, EventArgs e)
        {
            error = true; logged = false;
            if (textAddTcno.Text.Trim() == "" || textAddTcno.Text.Length != 11) { 
                lblMessage.Text = "TC Kimlik numaras� hatal�!"; textAddTcno.Focus();  return; 
            }
            else { lblMessage.Text = ""; Common.tckn = textAddTcno.Text.Trim(); }
            if (texPass.Text.Trim() == "" )
            {
                 lblMessage.Text = rbMebbis.Checked?  "Mebbis �ifresi girilmedi!" : "E-devlet �ifresi girilmedi"; texPass.Focus(); return;
            }
            else {  lblMessage.Text = ""; Common.pass = texPass.Text.Trim(); }

            Common.driver = WinHelpers.GetWebDriver(out msg);
            wait = new WebDriverWait(Common.driver, TimeSpan.FromSeconds(30));
            Common.driver.Url = "https://mebbis.meb.gov.tr/default.aspx";
            WaitForPageLoad(out msg);

            while (!Common.driver.PageSource.Contains("�ifremi Unuttum?"))
            {
                Thread.Sleep(500);
            }
            Common.driver.Manage().Window.Maximize();
            if (Common.login == "0")
            {
                tcknTbID = "tridField";
                passTbID = "egpField";
                pictureCaptcha = "captchaImage";
                tcknCaptcha = "captchaField";
                btnLogin = "submitButton";

                IWebElement edevletlink = WinHelpers.GetElementBy("i", "edevletlink", out msg);
                edevletlink.Click();
                if (!Common.driver.PageSource.Contains("temin edilebilmektedir"))
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Thread.Sleep(500);
                    }
                }
                int tryCount = 1;
                while (!logged && error)
                {
                    logged = EdevletGiris();
                    tryCount++;
                    if(tryCount == 4) { lblMessage.Text = "Oturum a�ma ba�ar�s�z oldu."; return; }
                }
            }
            else
            {
                tcknTbID = "txtKullaniciAd";
                passTbID = "txtSifre";
                pictureCaptcha = "imgKontrol";
                tcknCaptcha = "txtGuvenlikKod";
                btnLogin = "btnGiris";
                int tryCount = 1;
                while (!logged && error)
                {
                    logged = MebbisGiris();
                    tryCount++;
                    if (tryCount == 4) { lblMessage.Text = "Oturum a�ma ba�ar�s�z oldu."; return; }
                }
            }
            ConfirmMessages();
            Common.driver.Dispose();
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
        private void rbChrome_CheckedChanged(object sender, EventArgs e)
        {
            if (rbChrome.Checked) { Common.browser = "1"; Settings.Default.browser = "1"; }
            else { Common.browser = "0"; Settings.Default.browser = "0"; }
            Settings.Default.Save();
        }
        private void rbEdevlet_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMebbis.Checked == true)
            {
                gbLoginInfo.Text = "Mebbis Bilgileriniz";
                lblPass.Text = "Mebbis �ifreniz";
                texPass.Text = Settings.Default.passMEB;
                Settings.Default.login = "1";
                Common.login = "1";
            }
            else
            {
                gbLoginInfo.Text = "E-devlet Bilgileriniz";
                lblPass.Text = "E-devlet �ifreniz";
                texPass.Text = Settings.Default.passED;
                Settings.Default.login = "0";
                Common.login = "0";
            }
            Settings.Default.Save();
        }
        public bool EdevletGiris()
        {
            tcknWE = WinHelpers.GetElementBy("i", tcknTbID, out msg);
            tcknWE.Clear();
            tcknWE.SendKeys(Common.tckn);
            passWE = WinHelpers.GetElementBy("i", passTbID, out msg);
            passWE.Clear();
            passWE.SendKeys(Common.pass);
            giris = WinHelpers.GetButtonElementBy("n", btnLogin, out msg);
            if(Common.driver.PageSource.Contains("G�venlik Kodu"))
            {
                captchaWE = WinHelpers.GetElementBy("i", tcknCaptcha, out msg);
                captchaWE.Clear();
                captchaWE.SendKeys(Common.captcha);
            }

            giris.Click();
            WaitForPageLoad(out msg);
            if (Common.driver.PageSource.Contains("Mebbis Kullan�c�n�z Bulunmamaktad�r"))
            {
                lblMessage.Text = "Mebbsis �yel�iniz yoktur"; error = false; logged = false; return false;
            }
            if (Common.driver.PageSource.Contains("�st �ste ba�ar�s�z giri�"))
            {
                MessageBox.Show($"�st �ste ba�ar�s�z giri� denemeleri yapt���n�z i�in hesab�n�z bir saatli�ine ask�ya al�nm��t�r. L�tfen daha sonra tekrar deneyiniz ya da Mebbis �ifresi ile giri� yap�n�z.", "E-devlet giri� hatas�!"); error = false; logged = false;  return false;
            }

            if (Common.driver.PageSource.Contains("Kimlik no veya �ifre hatal�d�r") || Common.driver.PageSource.Contains("L�tfen formdaki hatal� alanlar� d�zelterek yeniden deneyiniz"))
            {
                Common.tckn = ""; Common.pass = ""; fLogin f; error = true;
                
                if (Common.driver.PageSource.Contains("G�venlik Kodu"))
                {
                    Common.captchaType = "x";
                    Common.captchaValue = "/html/body/div[1]/main/section[2]/form/fieldset/div[3]/div/img";
                    Common.rcbType = "x";
                    Common.rcbValue = "/html/body/div[1]/main/section[2]/form/fieldset/div[3]/div/img";

                    IWebElement captchaPic = WinHelpers.GetElementBy(Common.captchaType, Common.captchaValue, out msg);
                    Common.bitmap = null;
                    for (int i = 0; i < 3; i++)
                    {
                        Common.bitmap = CaptchaService.TakeScreenshot(captchaPic, out msg);
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
                else { error = false; logged = false; Common.driver.Dispose(); lblMessage.Text = "��lem iptal edildi"; return false; }
            }
            logged = true;
            Settings.Default.passED = Common.pass;
            Settings.Default.tckn = Common.tckn;
            Settings.Default.Save();
            return true;
        }
        public bool MebbisGiris()
        {
            if (Common.driver.PageSource.Contains("Kullan�c� Ad� veya �ifre yanl��"))
            {
                fLogin f; error = true;
                Common.captchaType = "i";
                Common.captchaValue = "imgKontrol";

                IWebElement captchaPic = WinHelpers.GetElementBy(Common.captchaType, Common.captchaValue, out msg);
                Common.bitmap = null;
                for (int i = 0; i < 3; i++)
                {
                    Common.bitmap = CaptchaService.TakeScreenshot(captchaPic, out msg);
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
                if (f.ShowDialog() == DialogResult.OK) {
                    captchaWE = WinHelpers.GetElementBy("i", tcknCaptcha, out msg);
                    captchaWE.Clear();
                    captchaWE.SendKeys(Common.captcha);
                }
                else { error = false; logged = false; Common.driver.Dispose(); lblMessage.Text = "��lem iptal edildi"; return false; }
            }
            tcknWE = WinHelpers.GetElementBy("i", tcknTbID, out msg);
            tcknWE.Clear();
            tcknWE.SendKeys(Common.tckn);
            passWE = WinHelpers.GetElementBy("i", passTbID, out msg);
            passWE.Clear();
            passWE.SendKeys(Common.pass);
            giris = WinHelpers.GetButtonElementBy("n", btnLogin, out msg);
            giris.Click();
            
            WaitForPageLoad(out msg);
            if (Common.driver.PageSource.Contains("G�venlik Kodunu Yanl�� Girdiniz"))
            {
                lblMessage.Text = "G�venlik kodu yanl�� girildi"; error = true; logged = false; MebbisGiris();
            }else if(Common.driver.PageSource.Contains("Kullan�c� Ad� veya �ifre yanl��"))
            {
                lblMessage.Text = "Kullan�c� ad� veya �ifre yanl��"; error = true; logged = false; MebbisGiris();
            }
            logged= true;
            Settings.Default.passMEB = Common.pass;
            Settings.Default.tckn = Common.tckn;
            Settings.Default.Save();
            return true;
        }
        public IWebElement? GetLoginLink() {
            int tryCount = 0;
            IWebElement? dysgiris = null;
            dysLink = "rptProjeler_ctl02_rptKullanicilar_ctl00_LinkButton1";
            while (dysgiris == null)
            {
                dysgiris = WinHelpers.GetButtonElementBy("i", dysLink, out msg); Thread.Sleep(1000); tryCount++;
                if (tryCount == 10) { return null; }
            }

            if (!dysgiris.Text.Contains("DYS WEB"))
            {
                dysgiris = null;
                dysLink = "rptProjeler_ctl02_rptKullanicilar_ctl01_LinkButton1";
                while (dysgiris == null)
                {
                    dysgiris = WinHelpers.GetButtonElementBy("i", dysLink, out msg); Thread.Sleep(1000); tryCount++;
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
                mdLink = WinHelpers.GetElementBy("i", "menuForm:roller_tree:2", out msg);
                if (mdLink.Text.Contains("M�d�r")){ return mdLink;   }
                mdLink = WinHelpers.GetElementBy("i", "menuForm:roller_tree:1", out msg);
                if (mdLink.Text.Contains("M�d�r")) { return mdLink; }
                mdLink = WinHelpers.GetElementBy("i", "menuForm:roller_tree:0", out msg);
                if (mdLink.Text.Contains("M�d�r")) { return mdLink; }
                if (tryCount == 10) { return null; }
            }

            return mdLink;
        }
        public void ConfirmMessages()
        {
            List<IWebElement> flips = WinHelpers.GetElementsBy("c", "image-flip", out msg);
            Actions action = new Actions(Common.driver);
            action.MoveToElement(flips[2]).Perform();

            int tryCount = 0;
            Thread.Sleep(1000);

            IWebElement? dysgiris = GetLoginLink();
            if(dysgiris == null) { lblMessage.Text = "DYS giri� linki bulunamad�\r\nL�tfen daha sonra tekrar deneyin"; Common.driver.Dispose(); return; }
            dysgiris.Click();

            if (Common.driver.WindowHandles.Count > 1)
            {
                Common.driver.SwitchTo().Window(Common.driver.WindowHandles[1]);
                WinHelpers.WaitForPageLoad(out msg);
            }
            tryCount = 0;

            List<IWebElement>? tree = null;

            while (tree == null)
            {
                Thread.Sleep(100);
                tree = Common.driver.FindElements(By.CssSelector("li[id^='menuForm:roller_tree:']")).ToList();
                tryCount++;
                if (tryCount == 300) { lblMessage.Text = "Sayfa 30 saniye boyunca yan�t vermedi�i i�in i�lem iptal edildi"; Common.driver.Dispose(); return; }
            }

            if(tree.Count > 9)
            {
                IWebElement? mdLink = GetMdLink();
                if (mdLink == null) { lblMessage.Text = "DYS giri� linki bulunamad�\r\nL�tfen daha sonra tekrar deneyin"; Common.driver.Dispose(); return; }
                mdLink.Click();
            }

            while (!Common.driver.PageSource.Contains("listelenmektedir"))
            {
                Thread.Sleep(100);
                if (Common.driver.PageSource.Contains("evrak bulunmamaktad�r")) { lblMessage.Text = "Onaylanmam�� mesaj yok"; return; }
                tryCount++;
                if(tryCount== 300) { lblMessage.Text = "Sayfa 30 saniye boyunca yan�t vermedi�i i�in i�lem iptal edildi"; Common.driver.Dispose(); return; }
            }
            IWebElement isler = WinHelpers.GetElementBy("i", "form:etiketFilter", out msg);

            string sayi = isler.Text.Substring(23, isler.Text.Substring(23).IndexOf(" "));
            int okunmamis = 0;
            try
            {
                okunmamis = Convert.ToInt32(sayi);
                IWebElement ilksatir = WinHelpers.GetButtonElementBy("x", "/html/body/div[2]/div[4]/form/div[1]/div/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]", out msg);
                ilksatir.Click();
                while (!Common.driver.PageSource.Contains("Gelen Evrak G�zden Ge�irme") && !Common.driver.PageSource.Contains("Evrak G�r�nt�leme"))
                {
                    Thread.Sleep(1000);
                }
                for (int i = 0; i < okunmamis; i++)
                {
                    while (!Common.driver.PageSource.Contains("Gelen Evrak G�zden Ge�irme") && !Common.driver.PageSource.Contains("Evrak G�r�nt�leme"))
                    {
                        Thread.Sleep(100);
                    }

                    IList<IWebElement> hiddenElements = Common.driver.FindElements(By.CssSelector("ui-dialog"));
                    foreach (IWebElement item in hiddenElements)
                    {
                        wait.Until(e => ((IJavaScriptExecutor)Common.driver).ExecuteScript("arguments[0].setAttribute('style', 'display:none')", item));
                    }

                    IWebElement? iframe = null;
                    while (iframe == null)
                    {
                        iframe = WinHelpers.GetElementBy("i", "gozdenGecirmeEkraniId", out msg);
                        Thread.Sleep(1000);
                    }
                    Common.driver.SwitchTo().Frame("gozdenGecirmeEkraniId");
                    IWebElement? okudum = WinHelpers.GetButtonElementBy("x", "//*[@id=\"formspanel:okudumBtn\"]", out msg);
                    if(okudum != null)
                    okudum.Click();
                    else
                    {
                        IWebElement? tamam = WinHelpers.GetButtonElementBy("x", "//*[@id=\"formspanel:_08001kapat2\"]", out msg);
                        tamam.Click();
                        continue;
                    }

                    while (!Common.driver.PageSource.Contains("Gelen Evrak G�zden Ge�irme"))
                    {
                        Thread.Sleep(1000);
                    }
                }
                lblMessage.Text= "B�t�n mesajlar onayland�";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HATA: {ex.Message}");
            }
        }
        public void WaitForPageLoad(out string msg)
        {
            msg = "";
            try
            {
                wait.Until(e => ((IJavaScriptExecutor)Common.driver).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
        }
        public void BringFront()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
    }
}
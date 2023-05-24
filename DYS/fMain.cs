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
        bool cancel = false;
        int flipCount = 0;

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
            error = true; logged = false; cancel = false; flipCount = 0;
            if (textAddTcno.Text.Trim() == "" || textAddTcno.Text.Length != 11)
            {
                lblMessage.Text = "TC Kimlik numarasý hatalý!"; textAddTcno.Focus(); return;
            }
            else { lblMessage.Text = ""; Common.tckn = textAddTcno.Text.Trim(); }
            if (texPass.Text.Trim() == "")
            {
                lblMessage.Text = rbMebbis.Checked ? "Mebbis þifresi girilmedi!" : "E-devlet þifresi girilmedi"; texPass.Focus(); return;
            }
            else { lblMessage.Text = ""; Common.pass = texPass.Text.Trim(); }

            Common.driver = WinHelpers.GetWebDriver(out msg);
            wait = new WebDriverWait(Common.driver, TimeSpan.FromSeconds(30));
            Common.driver.Url = "https://mebbis.meb.gov.tr/default.aspx";
            WaitForPageLoad(out msg);

            while (!Common.driver.PageSource.Contains("Þifremi Unuttum?"))
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
                    if (tryCount == 4 || cancel) { lblMessage.Text = "Oturum açma baþarýsýz oldu."; return; }
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
                    if (tryCount == 4 || cancel) { lblMessage.Text = "Oturum açma baþarýsýz oldu."; return; }
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
                lblPass.Text = "Mebbis Þifreniz";
                texPass.Text = Settings.Default.passMEB;
                Settings.Default.login = "1";
                Common.login = "1";
            }
            else
            {
                gbLoginInfo.Text = "E-devlet Bilgileriniz";
                lblPass.Text = "E-devlet Þifreniz";
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
            if (Common.driver.PageSource.Contains("Güvenlik Kodu"))
            {
                captchaWE = WinHelpers.GetElementBy("i", tcknCaptcha, out msg);
                captchaWE.Clear();
                captchaWE.SendKeys(Common.captcha);
            }

            giris.Click();
            WaitForPageLoad(out msg);
            if (Common.driver.PageSource.Contains("Mebbis Kullanýcýnýz Bulunmamaktadýr"))
            {
                lblMessage.Text = "Mebbsis üyelðiniz yoktur"; error = false; logged = false; return false;
            }
            if (Common.driver.PageSource.Contains("Üst üste baþarýsýz giriþ"))
            {
                MessageBox.Show($"Üst üste baþarýsýz giriþ denemeleri yaptýðýnýz için hesabýnýz bir saatliðine askýya alýnmýþtýr. Lütfen daha sonra tekrar deneyiniz ya da Mebbis þifresi ile giriþ yapýnýz.", "E-devlet giriþ hatasý!"); error = false; logged = false; return false;
            }

            if (Common.driver.PageSource.Contains("Kimlik no veya þifre hatalýdýr") || Common.driver.PageSource.Contains("Lütfen formdaki hatalý alanlarý düzelterek yeniden deneyiniz"))
            {
                Common.tckn = ""; Common.pass = ""; fLogin f; error = true;

                if (Common.driver.PageSource.Contains("Güvenlik Kodu"))
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
                        lblMessage.Text = "Güvenlik Kodu bulunamadý\r\nlütfen daha sonra tekrar deneyin";
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
                else { error = false; logged = false; Common.driver.Dispose(); lblMessage.Text = "Ýþlem iptal edildi"; cancel = true; return false; }
            }
            logged = true;
            Settings.Default.passED = cbRemember.Checked ? Common.pass : string.Empty;
            Settings.Default.tckn = cbRemember.Checked ? Common.tckn : string.Empty;
            Settings.Default.Save();
            return true;
        }
        public bool MebbisGiris()
        {
            if (Common.driver.PageSource.Contains("Kullanýcý Adý veya Þifre yanlýþ"))
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
                    lblMessage.Text = "Güvenlik Kodu bulunamadý\r\nlütfen daha sonra tekrar deneyin";
                    return false;
                }
                else
                {
                    f = new fLogin(true);
                }
                if (f.ShowDialog() == DialogResult.OK)
                {
                    captchaWE = WinHelpers.GetElementBy("i", tcknCaptcha, out msg);
                    captchaWE.Clear();
                    captchaWE.SendKeys(Common.captcha);
                }
                else { error = false; logged = false; Common.driver.Dispose(); lblMessage.Text = "Ýþlem iptal edildi"; cancel = true; return false; }
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
            if (Common.driver.PageSource.Contains("Güvenlik Kodunu Yanlýþ Girdiniz"))
            {
                lblMessage.Text = "Güvenlik kodu yanlýþ girildi"; error = true; logged = false; MebbisGiris();
            }
            else if (Common.driver.PageSource.Contains("Kullanýcý Adý veya Þifre yanlýþ"))
            {
                lblMessage.Text = "Kullanýcý adý veya þifre yanlýþ"; error = true; logged = false; MebbisGiris();
            }
            logged = true;
            Settings.Default.passMEB = cbRemember.Checked ? Common.pass : string.Empty;
            Settings.Default.tckn = cbRemember.Checked ? Common.tckn : string.Empty;
            Settings.Default.Save();
            return true;
        }
        public IWebElement? GetLoginLink()
        {
            int tryCount = 0;
            IWebElement? dysgiris = null;
            dysLink = $"rptProjeler_ctl0{flipCount}_rptKullanicilar_ctl00_LinkButton1";
            while (dysgiris == null)
            {
                dysgiris = WinHelpers.GetButtonElementBy("i", dysLink, out msg); Thread.Sleep(1000); tryCount++;
                if (tryCount == 10) { return null; }
            }

            if (!dysgiris.Text.Contains("DYS WEB"))
            {
                dysgiris = null;
                dysLink = $"rptProjeler_ctl0{flipCount}_rptKullanicilar_ctl01_LinkButton1";
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
                if (mdLink.Text.Contains("Müdür")) { return mdLink; }
                mdLink = WinHelpers.GetElementBy("i", "menuForm:roller_tree:1", out msg);
                if (mdLink.Text.Contains("Müdür")) { return mdLink; }
                mdLink = WinHelpers.GetElementBy("i", "menuForm:roller_tree:0", out msg);
                if (mdLink.Text.Contains("Müdür")) { return mdLink; }
                if (tryCount == 10) { return null; }
            }

            return mdLink;
        }
        public IWebElement? GetDysLink()
        {
            int tryCount = 0;
            IWebElement? dsyLink = null;
            List<IWebElement> flips = WinHelpers.GetElementsBy("c", "image-flip", out msg);
            while (flips == null)
            {
                tryCount++;
                flips = WinHelpers.GetElementsBy("c", "image-flip", out msg);
                if (tryCount == 10) { return null; }
            }
            foreach (IWebElement flip in flips)
            {
                if (flip.Text.Contains("DYS")) { dsyLink = flip; break; }
                else { flipCount++; }
            }

            return dsyLink;
        }
        public void ConfirmMessages()
        {
            IWebElement? dysFlipMenu = GetDysLink();
            if (dysFlipMenu == null) { lblMessage.Text = "DYS giriþ linki bulunamadý\r\nLütfen daha sonra tekrar deneyin"; Common.driver.Dispose(); return; }
            Actions action = new Actions(Common.driver);
            action.MoveToElement(dysFlipMenu).Perform();

            int tryCount = 0;
            Thread.Sleep(1000);

            IWebElement? dysgiris = GetLoginLink();
            if (dysgiris == null) { lblMessage.Text = "DYS giriþ linki bulunamadý\r\nLütfen daha sonra tekrar deneyin"; Common.driver.Dispose(); return; }
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
                if (tryCount == 300) { lblMessage.Text = "Sayfa 30 saniye boyunca yanýt vermediði için iþlem iptal edildi"; Common.driver.Dispose(); return; }
            }

            if (tree.Count > 9)
            {
                IWebElement? mdLink = GetMdLink();
                if (mdLink == null) { lblMessage.Text = "Ýdareci mesajlarý listelenemdi\r\nLütfen daha sonra tekrar deneyin"; Common.driver.Dispose(); return; }
                mdLink.Click();
            }

            while (!Common.driver.PageSource.Contains("listelenmektedir"))
            {
                Thread.Sleep(100);
                if (Common.driver.PageSource.Contains("evrak bulunmamaktadýr")) { lblMessage.Text = "Onaylanmamýþ mesaj yok"; return; }
                tryCount++;
                if (tryCount == 300) { lblMessage.Text = "Sayfa 30 saniye boyunca yanýt vermediði için iþlem iptal edildi"; Common.driver.Dispose(); return; }
            }
            IWebElement isler = WinHelpers.GetElementBy("i", "form:etiketFilter", out msg);

            string sayi = isler.Text.Substring(23, isler.Text.Substring(23).IndexOf(" "));
            int okunmamis = 0;
            try
            {
                okunmamis = Convert.ToInt32(sayi);
                IWebElement ilksatir = WinHelpers.GetButtonElementBy("x", "/html/body/div[2]/div[4]/form/div[1]/div/div/div[2]/div/div[2]/table/tbody/tr[1]/td[6]", out msg);
                ilksatir.Click();
                while (!Common.driver.PageSource.Contains("Gelen Evrak Gözden Geçirme") && !Common.driver.PageSource.Contains("Evrak Görüntüleme"))
                {
                    Thread.Sleep(1000);
                }
                for (int i = 0; i < okunmamis; i++)
                {
                    while (!Common.driver.PageSource.Contains("Gelen Evrak Gözden Geçirme") && !Common.driver.PageSource.Contains("Evrak Görüntüleme"))
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
                        Thread.Sleep(500);
                    }
                    Common.driver.SwitchTo().Frame("gozdenGecirmeEkraniId");

                    IWebElement? okudum = null;
                    if (Common.driver.PageSource.Contains("Okudum")) { okudum = WinHelpers.GetButtonElementBy("x", "//*[@id=\"formspanel:okudumBtn\"]", out msg); }
                    if (okudum != null)
                        okudum.Click();
                    else
                    {
                        IWebElement? kapat = WinHelpers.GetButtonElementBy("x", "//*[@id=\"formspanel:_08001kapat2\"]", out msg);
                        if (kapat != null)
                            kapat.Click();
                        continue;
                    }

                    while (!Common.driver.PageSource.Contains("Gelen Evrak Gözden Geçirme"))
                    {
                        Thread.Sleep(1000);
                    }
                }
                lblMessage.Text = "Bütün mesajlar onaylandý";
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
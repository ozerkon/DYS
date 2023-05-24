using DYS.Captcha;
using DYS.Helpers;
using Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DYS
{
    public partial class fLogin : Form
    {
        private bool hasCaptcha = false;
        public IWebElement weCaptchaImg { get; set; }
        public IWebElement clickElement { get; set; }
        
        public fLogin(bool _hasCaptcha = false)
        {
            InitializeComponent();
            hasCaptcha = _hasCaptcha;
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            Text = Common.loginType == "1" ? "Mebbis giriş bilgileri" : "E-devlet giriş bilgileri";
            lblPass.Text = Common.loginType == "1" ? "Mebbis şifreniz" : "E-devlet şifreniz";
            btnRefreshCaptcha.Enabled = Common.loginType == "1" ? false : true;
            lblMessage.Text = "";
            gbCaptcha.Enabled = hasCaptcha;
            pbCaptcha.Image = Common.bitmap == null ?  null : Common.bitmap;
            if(hasCaptcha && Common.bitmap != null) CenterImage();

            if (Common.mebAjandaActive)
            {
                gbLoginInfo.Enabled = false;
                btnRefreshCaptcha.Enabled = false;
                gbCaptcha.Text = "Meb Ajanda Doğrulama Kodu Girişi";
                lblCode.Text = "Doğrulama Kodu";
                txtCaptcha.Focus();
                txtCaptcha.SelectAll();
            }
            else { gbCaptcha.Text = "Güvenlik Kodu Girişi"; lblCode.Text = "Güvenlik Kodu"; }
            
        }
        private void CenterImage()
        {
            pbCaptcha.Left = (panel1.Width - Common.bitmap.Width) / 2;
            pbCaptcha.Top = (panel1.Height - Common.bitmap.Height) / 2;

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Common.mebAjandaActive && (textAddTcno.Text.Trim() == "" || textAddTcno.Text.Length != 11) )
            {
                lblMessage.Text = "TC Kimlik numarası hatalı!"; textAddTcno.Focus(); return;
            }
            else { lblMessage.Text = ""; Common.tckn = textAddTcno.Text.Trim(); }
            if (!Common.mebAjandaActive && texPass.Text.Trim() == "")
            {
                lblMessage.Text = Common.loginType == "0" ? "Mebbis şifresi girilmedi!" : "E-devlet şifresi girilmedi"; texPass.Focus(); return;
            }
            else { lblMessage.Text = ""; Common.pass = texPass.Text.Trim(); }

            if (hasCaptcha && txtCaptcha.Text.Trim() == "")
            {
                lblMessage.Text = "Kod girilmedi!"; txtCaptcha.Focus(); return;
            }
            else { lblMessage.Text = ""; Common.captcha = txtCaptcha.Text.Trim(); }
            DialogResult = DialogResult.OK;
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

        private void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            string msg = ""; string srcBefore = String.Empty; string srcAfter = String.Empty;
            string imgType;
            string imgValue;
            lblMessage.Text = string.Empty;
            try
            {
                imgType = Common.captchaType;
                imgValue = Common.captchaValue;
                if (pbCaptcha.Image != null)
                {
                    pbCaptcha.Image.Dispose();
                }
                Common.driver.Manage().Window.Maximize();
                srcBefore = GetImageSrc(imgValue, imgType, out msg);
                ClickWebElement(Common.rcbValue, out msg, Common.rcbType);
                while (srcBefore == GetImageSrc(imgValue, imgType, out msg))
                {
                    Thread.Sleep(100);
                }
                srcAfter = GetImageSrc(imgValue, imgType, out msg);
                
                if (srcAfter != String.Empty && srcBefore != String.Empty && srcAfter != srcBefore)
                {
                    WaitForImageLoad(imgValue, out msg, imgType);
                    if (GetCaptchaPicture(imgType, imgValue, out msg))
                    {
                        pbCaptcha.Image = CaptchaService.TakeScreenshot(weCaptchaImg, out msg);
                        if (pbCaptcha.Image != null && pbCaptcha.Image.Size != null && pbCaptcha.Image.Width > 0 && pbCaptcha.Image.Height > 0)
                        {
                            pbCaptcha.Width = pbCaptcha.Image.Width;
                            pbCaptcha.Height = pbCaptcha.Image.Height;
                            pbCaptcha.Left = (340 - pbCaptcha.Width) / 2;
                            pbCaptcha.Top = (100 - pbCaptcha.Height) / 2;
                            pbCaptcha.Refresh();
                            BringFront();
                            txtCaptcha.Focus();
                            txtCaptcha.SelectAll();
                        }
                    }
                }
                else { lblMessage.Text = "Kod yenilenemedi!"; }
                Common.driver.Manage().Window.Minimize();
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                lblMessage.Text = $"Kod yenilenemedi! Hata:{msg}";
            }
        }
        public void BringFront()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        public string GetImageSrc(string element, string type, out string msg)
        {
            msg = "";
            WebDriverWait wait = new WebDriverWait(Common.driver, TimeSpan.FromSeconds(Common.maxWait));
            try
            {
                switch (type)
                {
                    case "x":
                        weCaptchaImg = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(element)));
                        break;
                    case "t":
                        weCaptchaImg = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName(element)));
                        break;
                    case "c":
                        weCaptchaImg = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName(element)));
                        break;
                    case "s":
                        weCaptchaImg = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(element)));
                        break;
                    case "n":
                        weCaptchaImg = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name(element)));
                        break;
                    case "i":
                        weCaptchaImg = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(element)));
                        break;
                    default:
                        return String.Empty;
                }
                return weCaptchaImg.GetAttribute("src");
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                return String.Empty;
            }
        }
        public void ClickWebElement(string element, out string msg, string method = "XPath")
        {
            msg = "";
            try
            {
                switch (method)
                {
                    case "i":
                        clickElement = Common.driver.FindElement(By.Id($"{element}"));
                        break;
                    case "c":
                        clickElement = Common.driver.FindElement(By.ClassName($"{element}"));
                        break;
                    case "s":
                        clickElement = Common.driver.FindElement(By.CssSelector($"{element}"));
                        break;
                    case "t":
                        clickElement = Common.driver.FindElement(By.TagName($"{element}"));
                        break;
                    case "l":
                        clickElement = Common.driver.FindElement(By.LinkText($"{element}"));
                        break;
                    case "x":
                        clickElement = Common.driver.FindElement(By.XPath($"{element}"));
                        break;
                    default:
                        clickElement = Common.driver.FindElement(By.XPath($"{element}"));
                        break;
                }
                clickElement.Click();
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
        }
        public bool GetCaptchaPicture(string t, string tv, out string msg)
        {
            try
            {
                weCaptchaImg = WinHelpers.GetElementBy(t, tv, out msg);
                if (weCaptchaImg != null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
            return false;
        }
        public void WaitForImageLoad(string element, out string msg, string method = "XPath")
        {
            msg = "";
            IWebElement Image = null;
            try
            {
                IWebElement e = WinHelpers.GetElementBy(method, element, out msg);
                bool ImagePresent = (Boolean)((IJavaScriptExecutor)Common.driver).ExecuteScript("return arguments[0].complete && typeof arguments[0].naturalWidth != \"undefined\" && arguments[0].naturalWidth > 0", e);
                if (ImagePresent == true)
                {
                    return;
                }

                else

                {
                    // You can increase or decrease the loop times based on tested application
                    for (int i = 0; i < 25; i++)
                    {
                        System.Threading.Thread.Sleep(1000);
                        Image = Common.driver.FindElement(By.XPath("xpath of container which is contained the image"));
                        ImagePresent = (Boolean)((IJavaScriptExecutor)Common.driver).ExecuteScript("return arguments[0].complete && typeof arguments[0].naturalWidth != \"undefined\" && arguments[0].naturalWidth > 0", e);

                        if (ImagePresent == true)
                        {
                            break;
                        }
                    }
                }
            }
            catch (NoSuchElementException) { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Common.cancelProcess = true;
        }
    }
}

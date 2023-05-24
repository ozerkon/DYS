using Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Diagnostics;
using System.Text;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace DYS.Helpers
{

    public class WinHelpers
    {
        private static IWebDriver? driver;
        private static WebDriverWait? wait;
        public static IWebDriver GetWebDriver(bool hideBrowser, out string msg)
        {
            msg = ""; IWebDriver? driver = null;
            switch (Common.browser)
            {
                case "0":
                    driver = SetFirefoxOptionsForDownload(hideBrowser);
                    break;
                case "1":
                    driver = SetChromeOptionsForDownload(hideBrowser);
                    break;
            }
            return driver;
        }
        public static IWebDriver SetFirefoxOptionsForDownload(bool hideBrowser)
        {
            //IWebDriver driver;
            FirefoxOptions firefoxOptions;
            FirefoxProfile firefoxProfile = new FirefoxProfile();
            FirefoxDriverService fDriverService;
            firefoxOptions = new FirefoxOptions();

            string driverLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;

            firefoxProfile.SetPreference("browser.download.folderList", 2);
            firefoxProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/pdf");
            firefoxProfile.SetPreference("plugin.scan.plid.all", false);
            firefoxProfile.SetPreference("plugin.scan.Acrobat", "99.0");
            //firefoxProfile.SetPreference("browser.download.manager.showWhenStarting", false);
            firefoxProfile.SetPreference("pdfjs.disabled", true);

            //firefoxProfile.SetPreference("browser.helperApps.alwaysAsk.force", false);

            firefoxProfile.SetPreference("browser.download.manager.showAlertOnComplete", false);
            firefoxProfile.SetPreference("pdfjs.enabledCache.state", false);
            firefoxProfile.DeleteAfterUse = true;

            firefoxOptions.Profile = firefoxProfile;
            if (hideBrowser)
            {
                fDriverService = FirefoxDriverService.CreateDefaultService(Application.StartupPath);
                fDriverService.HideCommandPromptWindow = true;
                firefoxOptions.AddArgument("-headless");
                int sw = Screen.PrimaryScreen.Bounds.Width;
                firefoxOptions.AddArgument($"--width={sw}");
                int sh = Screen.PrimaryScreen.Bounds.Height;
                firefoxOptions.AddArgument($"--height={sh}");
            }
            try
            {
                driverLocation = new DriverManager().SetUpDriver(new FirefoxConfig());
                driverLocation = driverLocation.Replace(@"\geckodriver.exe", "");
                fDriverService = FirefoxDriverService.CreateDefaultService(driverLocation);
                fDriverService.HideCommandPromptWindow = true;
                driver = new FirefoxDriver(fDriverService, firefoxOptions);

            }
            catch
            {
                fDriverService = FirefoxDriverService.CreateDefaultService(driverLocation);
                fDriverService.HideCommandPromptWindow = true;
                driver = new FirefoxDriver(fDriverService, firefoxOptions);
            }

            return driver;
        }
        public static IWebDriver SetChromeOptionsForDownload(bool hideBrowser)
        {
            string msg = "";
            ChromeOptions chromeOptions;
            ChromeDriverService cDriverService;

            chromeOptions = new ChromeOptions();
            string driverLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;

            chromeOptions.AddExcludedArgument("enable-automation");
            //chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            chromeOptions.AddUserProfilePreference("intl.accept_languages", "tr");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

            if (hideBrowser)
            {
                chromeOptions.AddArgument("headless");
                int sw = Screen.PrimaryScreen.Bounds.Width;
                int sh = Screen.PrimaryScreen.Bounds.Height;
                chromeOptions.AddArguments($"window-size={sw},{sh}");
            }
            try
            {
                driverLocation = new DriverManager().SetUpDriver(new ChromeConfig());
                driverLocation = driverLocation.Replace(@"\chromedriver.exe", "");
                cDriverService = ChromeDriverService.CreateDefaultService(driverLocation);
                cDriverService.HideCommandPromptWindow = true;
                driver = new ChromeDriver(cDriverService, chromeOptions);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                cDriverService = ChromeDriverService.CreateDefaultService(driverLocation);
                driver = new ChromeDriver(cDriverService, chromeOptions);
            }
            return driver;

        }
        public static IWebElement? element { get; set; }
        public static bool isAlertPresent(out string msg)
        {
            msg = "";
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IAlert alert1 = wait.Until(ExpectedConditions.AlertIsPresent());

                if (alert1 != null)
                {
                    IAlert alert = driver.SwitchTo().Alert();
                    alert.Accept();
                    return true;
                }
                return false;

            }
            catch (NoAlertPresentException ex)
            {
                msg = $"Alert yok {ex}"; return false;
            }
        }
        public static IWebElement GetElementBy(string t, string tv, out string msg)
        {
            msg = "";
            try
            {
                WebDriverWait wait = GetWait();
                switch (t)
                {
                    case "n":
                        element = wait.Until(e => e.FindElement(By.Name(tv)));
                        break;
                    case "i":
                        element = wait.Until(e => e.FindElement(By.Id(tv)));
                        break;
                    case "x":
                        element = wait.Until(e => e.FindElement(By.XPath(tv)));
                        break;
                    case "c":
                        element = wait.Until(e => e.FindElement(By.CssSelector(tv)));
                        break;
                    case "t":
                        element = wait.Until(e => e.FindElement(By.LinkText(tv)));
                        break;
                    case "cl":
                        element = wait.Until(e => e.FindElement(By.ClassName(tv)));
                        break;
                    case "tn":
                        element = wait.Until(e => e.FindElement(By.TagName(tv)));
                        break;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                return null;
            }
            return element;
        }
        public static List<IWebElement> GetElementsBy(string t, string tv, out string msg)
        {
            msg = "";
            List<IWebElement> elements = new List<IWebElement>();
            try
            {
                WebDriverWait wait = GetWait();
                if (!ExecuteScript(t, tv)) { wait.Until(e => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete")); }


                elements = wait.Until(e => e.FindElements(By.ClassName(tv))).ToList();

            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                return null;
            }
            return elements;
        }
        public static IWebElement? GetButtonElementBy(string t, string tv, out string msg)
        {
            msg = "";
            try
            {
                WebDriverWait wait = GetWait();
                wait.Until(e => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                switch (t)
                {
                    case "n":
                        element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name(tv)));
                        break;
                    case "i":
                        element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id(tv)));
                        break;
                    case "x":
                        element = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(tv)));
                        break;
                    case "c":
                        element = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(tv)));
                        break;
                    case "t":
                        element = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(tv)));
                        break;
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                return null;
            }
            return element;
        }
        public static string GetTableFromPage(string t, string tv, int sr, int lr, out string msg)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                IWebElement table = GetElementBy(t, tv, out msg);

                if (table == null) { msg = "Tablo bulunamadı"; return null; }

                List<IWebElement> trs = table.FindElements(By.TagName("tr")).ToList();
                if (trs == null || trs.Count == 0) { msg = "Tabloda satır bulunamadı"; return null; }
                if (trs.Count == sr - 1) { msg = "Tabloda veri bulunamadı"; return "boş"; }

                int count = 1;
                foreach (IWebElement tr in trs)
                {
                    if (count < sr) { count++; continue; }

                    List<IWebElement> tds = tr.FindElements(By.TagName("td")).ToList();
                    foreach (IWebElement td in tds)
                    {
                        sb.Append($"{td.Text}\t");
                    }
                    sb.Append($"\r\n");
                    if (count == trs.Count - lr) { break; }
                    count++;
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                return null;
            }
        }
        public static bool IsPageLoad(string term, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (driver.PageSource.Contains(term))
                {
                    return true;
                }
                Thread.Sleep(1000);
            }
            return false;
        }
        public static void WaitForPageLoad(out string msg)
        {
            msg = "";
            try
            {
                WebDriverWait wait = GetWait();
                wait.Until(e => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
        }
        public static void WaitForElement(string type, string element, out string msg)
        {
            msg = "";
            WebDriverWait wait = GetWait();
            try
            {
                if (type == "i")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy((By.Id(element))));
                }
                else if (type == "x")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy((By.XPath(element))));
                }
                else if (type == "n")
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy((By.Name(element))));
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
        }
        public static void JustMouseHover(string type, string tv, out string msg)
        {
            msg = "";
            WebDriverWait wait = GetWait();
            IWebElement menuItem = null;
            Actions action = new Actions(driver);

            try
            {
                switch (type)
                {
                    case "i":
                        menuItem = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(tv)));
                        break;
                    case "x":
                        menuItem = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(tv)));
                        break;
                    case "n":
                        menuItem = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name(tv)));
                        break;
                    case "c":
                        menuItem = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector(tv)));
                        break;
                    case "t":
                        menuItem = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText(tv)));
                        break;
                }
                action.MoveToElement(menuItem).Perform();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
        }
        public static void JustMouseHover(IWebElement element, out string msg)
        {
            msg = "";
            Actions action = new Actions(driver);
            try
            {
                action.MoveToElement(element).Perform();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
        }
        public static void MoveMouse(int x, int y, out string msg)
        {
            msg = "";
            try
            {
                Actions action = new Actions(driver);
                action.MoveByOffset(x, y).Perform();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
        }
        public static void KillDriver(out string msg)
        {
            msg = "";
            try
            {

                foreach (var process in Process.GetProcessesByName("geckodriver.exe"))
                {
                    process.Kill();
                }
            }
            catch (Exception)
            {
                msg = "geckodriver.exe bulunamadı";
            }

        }
        public static WebDriverWait GetWait()
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(Models.Common.maxWait));
        }
        public static bool ExecuteScript(string t, string tv)
        {
            wait = GetWait();
            string selector = "";
            string command =
                $"function waitForElm(selector) {{" +
                    $"return new Promise(resolve => {{" +
                        $"if (document.querySelector(selector)){{" +
                            $"return resolve(document.querySelector(selector));" +
                        $"}}" +
                    $"}});" +
                $"}}";
            switch (t)
            {
                case "x":
                    selector = tv.Replace("'", "\\'");
                    command = $"function checkIfElemExists(selector) {{" +
                                    $"return new Promise(resolve => {{" +
                                        $"var clickButton = document.evaluate (selector, document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;" +
                                            $"if (clickButton == null) {{ window.requestAnimationFrame(checkIfElemExists); }}" +
                                            $"else {{ return resolve(clickButton); }}" +
                                    $"}})" +
                               $"}}" +
                               $"return checkIfElemExists('{selector}').then((elm) => {{return 'ready'}});";
                    break;
                case "i":
                    selector = $"#{tv}";
                    command += $"return waitForElm('{selector}').then((elm) => {{return 'ready'}});";
                    break;
                case "c":
                    selector = $".{tv}";
                    command += $"return waitForElm('{selector}').then((elm) => {{return 'ready'}});";
                    break;
                case "tn":
                    selector = tv;
                    command += $"return waitForElm('{selector}').then((elm) => {{return 'ready'}});";
                    break;
                case "n":
                    selector = tv;
                    command += $"return waitForElm('[name=\"{selector}\"]').then((elm) => {{return 'ready'}});";
                    break;
            }
            try
            {
                return wait.Until(e => ((IJavaScriptExecutor)driver).ExecuteScript(command).Equals("ready"));
            }
            catch (Exception)
            {
                return false;
            }


        }
        public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
        {
            int place = Source.LastIndexOf(Find);

            if (place == -1)
                return Source;

            return Source.Remove(place, Find.Length).Insert(place, Replace);
        }
    }
}



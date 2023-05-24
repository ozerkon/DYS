
using Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DYS.Captcha
{
    public static class CaptchaService 
    {
        public static IWebElement captchaPic { get; set; }
        public static string captchaFilePath = Path.GetTempPath();
        public static string LastCaptchaPath = Path.GetTempPath();
        public static Bitmap? TakeScreenshot(IWebElement element, out string msg)
        {
            msg = "";
            try
            {
                if (element == null) return null;
                TryCreateFolder(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\SgkAsistan\Temp", false);
                string fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
                captchaFilePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\SgkAsistan\Temp\" + fileName;
                LastCaptchaPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\SgkAsistan\Temp\" + fileName;
                byte[] byteArray = ((ITakesScreenshot)Common.driver).GetScreenshot().AsByteArray;
                Bitmap screenshot = new Bitmap(new MemoryStream(byteArray));
                Rectangle croppedImage = new Rectangle(element.Location.X, element.Location.Y, element.Size.Width, element.Size.Height);
                screenshot = screenshot.Clone(croppedImage, screenshot.PixelFormat);
                screenshot.Save(captchaFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                Bitmap bit = new Bitmap(captchaFilePath);
                Common.imageWidth = element.Size.Width;
                return bit;
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
                return null;
            }
        }

        public static void RefreshCaptchaImage(PictureBox pb, string type, string value, out string msg)
        {
            msg = "";
            try
            {
                WebDriverWait wait = new WebDriverWait(Common.driver, TimeSpan.FromSeconds(30)); 
                if (!ExecuteScript(type, value)) wait.Until(e => ((IJavaScriptExecutor)Common.driver).ExecuteScript("return document.readyState").Equals("complete"));
                switch (type)
                {
                    case "x":
                        captchaPic = wait.Until(e => e.FindElement(By.XPath(value)));
                        break;
                    case "i":
                        captchaPic = wait.Until(e => e.FindElement(By.Id(value)));
                        break;
                    case "n":
                        captchaPic = wait.Until(e => e.FindElement(By.Name(value)));
                        break;
                    case "s":
                        captchaPic = wait.Until(e => e.FindElement(By.CssSelector(value)));
                        break;
                    case "c":
                        captchaPic = wait.Until(e => e.FindElement(By.ClassName(value)));
                        break;
                    case "t":
                        captchaPic = wait.Until(e => e.FindElement(By.TagName(value)));
                        break;
                }
                pb.Image = TakeScreenshot(captchaPic, out msg);
            }
            catch (Exception ex)
            {
                msg = ex.Message.ToString();
            }
        }
        public static bool TryCreateFolder(string folderToCreate, bool clearExistingFiles)
        {
            int tryCount = 0;
            bool folderCreated = false;
            bool folderIsEmpty = false;
            while (tryCount++ < 10)
            {
                if (System.IO.Directory.Exists(folderToCreate) == false)
                {
                    System.IO.Directory.CreateDirectory(folderToCreate);
                }
                else
                {
                    folderCreated = true;
                    break;
                }

            }
            tryCount = 0;
            if (folderCreated == false)
            {
                return false;
            }
            if (clearExistingFiles)
            {
                folderIsEmpty = false;
                while (tryCount++ < 10)
                {
                    int fileCount = 0;
                    System.IO.DirectoryInfo di = new DirectoryInfo(folderToCreate);
                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                        fileCount++;
                    }
                    if (fileCount == 0)
                    {
                        folderIsEmpty = true;
                        break;
                    }
                }
                return folderCreated && folderIsEmpty;
            }

            return folderCreated;
        }
        public static bool ExecuteScript(string t, string tv)
        {
            WebDriverWait wait = new WebDriverWait(Common.driver, TimeSpan.FromSeconds(30));
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
                case "cl":
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
                return wait.Until(e => ((IJavaScriptExecutor)Common.driver).ExecuteScript(command).Equals("ready"));
            }
            catch (Exception)
            {
                return false;
            }


        }
    }

}

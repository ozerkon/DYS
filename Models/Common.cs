using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Models
{
    public static class Common
    {
        public static string browser { get; set; } = "0"; // 0: firefox, 1: chrome
        public static string loginType { get; set; } = "0"; // 0: edevlet, 1: mebbis
        public static string tckn { get; set; } = ""; 
        public static string pass { get; set; } = ""; 
        public static string captcha { get; set; } = ""; 
        public static string captchaType { get; set; } = ""; 
        public static string captchaValue { get; set; } = ""; 
        public static string rcbValue { get; set; } = ""; 
        public static string rcbType { get; set; } = "";

        public static int imageWidth { get; set; }
        public static int maxWait { get; set; } = 20;
        public static Bitmap? bitmap { get; set; }
        public static IWebDriver driver { get; set; }

        public static string tcknTbID = "";
        public static string passTbID = "";
        public static string pictureCaptcha = "";
        public static string tcknCaptcha = "";
        public static string btnLogin = "";
        public static string dysLink = "";
        public static bool cancel = false;
        public static int flipCount = 0;

        public static IWebElement? tcknWE;
        public static IWebElement? passWE;
        public static IWebElement? giris;
        public static IWebElement? captchaWE;
        public static IWebElement? mdLink;
        public static IWebElement? ilksatir;

        public static bool mebAjandaActive = false;
        public static IWebElement? firstInput;
        public static IWebElement? secondInput ;
        public static IWebElement? thirdInput ;
        public static IWebElement? fourthInput ;
        public static IWebElement? fifthInput;
        public static IWebElement? sixthInput;
        public static IWebElement? mebAjandaDontAsk;
        public static IWebElement? mebAjandaButton;


        public static Regex rx = new Regex(@"[0-9]{1}[0-9]{9}[02468]{1}");
        public static string? msg;
        public static WebDriverWait? wait = null;
        public static bool loggedIn = false; 
        public static bool error;
        public static BackgroundWorker bgwConfirm;
        public static bool cancelProcess;
        public static bool hideBrowser;
        public static bool confirmError;
        public static bool isManager;

        public static int tryCount = 0;
        public static string mesaj = "";

        public static int totalMessages = 0;
        public static int confirmedMessages = 0;
        public static int nonConfirmedMessages = 0;
    }
}

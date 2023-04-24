using OpenQA.Selenium;
using System.Net;

namespace Models
{
    public static class Common
    {
        public static string browser { get; set; } = "0"; // 0: firefox, 1: chrome
        public static string login { get; set; } = "0"; // 0: firefox, 1: chrome
        public static string tckn { get; set; } = ""; // 0: firefox, 1: chrome
        public static string pass { get; set; } = ""; // 0: firefox, 1: chrome
        public static string captcha { get; set; } = ""; // 0: firefox, 1: chrome
        public static string captchaType { get; set; } = ""; 
        public static string captchaValue { get; set; } = ""; 
        public static string rcbValue { get; set; } = ""; 
        public static string rcbType { get; set; } = "";

        public static int imageWidth { get; set; }
        public static int maxWait { get; set; } = 20;
        public static Bitmap? bitmap { get; set; }
        public static IWebDriver? driver { get; set; }
    }
}

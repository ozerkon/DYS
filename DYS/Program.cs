namespace DYS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            SetPoints("3999,58");
            ApplicationConfiguration.Initialize();
            Application.Run(new fMain());
        }

        public static string SetPoints(string source)
        {
            source = source.Trim();
            source = "123.954,65";
            
            int pointPos = source.IndexOf(".");
            int commaPos = source.IndexOf(",");

            if (commaPos > 0 && commaPos > pointPos && pointPos > 0) // xx.xxx,yy
            {
                source = source.Replace("-,", "-0,").Replace("-.", "-0.").Replace(".", "").Replace(",", ".").Replace(" ", "");
            }
            else if (commaPos > 0 && commaPos > pointPos && pointPos == -1) // xxx,yy
            {
                source = source.Replace("-,", "-0,").Replace("-.", "-0.").Replace(".", "").Replace(",", ".").Replace(" ", "");
            }
            else if (pointPos > 0 && pointPos > commaPos && commaPos > 0 ) // xx,xxx.yy
            {
                source = source.Replace("-,", "-0,").Replace("-.", "-0.").Replace(",", "").Replace(" ", "");
            }
            else if (pointPos > 0 && pointPos > commaPos && commaPos == - 1) // xxx.yy
            {
                source = source.Replace("-,", "-0,").Replace("-.", "-0.").Replace(",", "").Replace(" ", "");
            }
            else if (commaPos == pointPos && commaPos == -1) //  xxxx 
            {
                source += ".00";
            }
            else if (commaPos == 0 && pointPos == -1) // ,xx
            {
                source = source.Replace(",", "0.");
            }
            else if (pointPos == 0 && commaPos == -1) // .xx
            {
                source = source.Replace(".", "0.");
            }
            int lenth = source.Length;
            pointPos = source.IndexOf(".");
            if (pointPos == lenth - 2)
            {
                source += "0";
            }
            return source;
        }
    }
}
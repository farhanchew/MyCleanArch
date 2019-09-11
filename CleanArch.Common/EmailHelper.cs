using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CleanArch.Common
{
    public class EmailHelper
    {
        public static string ConvertEmailHtmlToString(string emailHtmlRelativePath)
        {
            string actualPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, emailHtmlRelativePath);
            return File.ReadAllText(actualPath, Encoding.UTF8);
        }
    }
}

using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Linq;

namespace CleanArch.Common
{

    public class Helpers
    {
        private static readonly string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public static string JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
        }

        public static string GetConnectionString(string connectionStringName)
        {
            IConfigurationRoot configuration = GetConfiguration();

            var connectionString = configuration.GetConnectionString(connectionStringName);
            return connectionString;
        }
        public static string GetHostUrl
        {
            get
            {
                IConfigurationRoot configuration = GetConfiguration();
                return configuration["Data:HostUrl"];
            }
        }

        //public static (string,string) GetEmailSender()
        //{
        //    IConfigurationRoot configuration = GetConfiguration();

        //    var username = configuration["EmailSender:Username"];
        //    var password = configuration["EmailSender:Password"];
        //    return (username, password);
        //}
        public static (string fromEmail, List<string> tos) GetEmailSender()
        {
            IConfigurationRoot configuration = GetConfiguration();
            var from = configuration["EmailSender:FromEmail"];
            var to = configuration["EmailSender:ToEmail"];
            List<string> multipleTo = new List<string>();
            if (!string.IsNullOrEmpty(to))
            {
                multipleTo = to.Split(";").Select(x => x.Trim()).ToList();
            }
            return (from, multipleTo);
        }

        public static string GenerateRandomStr(int size)
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            string input = ";:abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Range(0, size).Select(x => input[rand.Next(0, input.Length)]).ToArray());
        }

        public static string GetServiceKey(string keyName)
        {
            IConfigurationRoot configuration = GetConfiguration();
            var connectionString = configuration["ServiceKeys:" + keyName];
            return connectionString;
        }

        public static string GetSetting(string settingName)
        {
            IConfigurationRoot configuration = GetConfiguration();
            var connectionString = configuration[settingName];
            return connectionString;
        }

        private static IConfigurationRoot GetConfiguration()
        {
            string basePath = Directory.GetCurrentDirectory();

#if DEBUG //Debug folder will be as below, on appservice all will be in same folder
            basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}CleanArch.API", Path.DirectorySeparatorChar);
#endif

            var configuration = new ConfigurationBuilder()
               .SetBasePath(basePath)
               .AddJsonFile("appsettings.json")
               .AddJsonFile($"appsettings.Local.json", optional: true)
               .AddJsonFile($"appsettings.{ GetEnvironment() }.json", optional: true)
               .AddEnvironmentVariables()
               .Build();
            return configuration;
        }

        public static string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable(AspNetCoreEnvironment);
        }

        public static bool IsProduction
        {
            get
            {
                return Environment.GetEnvironmentVariable(AspNetCoreEnvironment).Equals("Production");
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        public static bool IsLegalAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }

            return age >= 18;
        }
    }
}

using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using IRAT.Inject.Model;
using System.Reflection;
using System.ComponentModel;
using System.Web;
using static IRAT.Inject.Business._Enums;

namespace IRAT.Inject.Business
{
    public class _Helper
    {

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }
            return value.ToString();
        }

        public static string MD5Encode(string s)
        {
            byte[] array = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(s));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string Sha5121Encode(string password)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] message = UE.GetBytes(password);
            SHA512Managed hashString = new SHA512Managed();
            string hexNumber = "";
            byte[] hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hexNumber += String.Format("{0:x2}", x);
            }
            return hexNumber;
        }

        public static string GuId(GuId guid)
        {
            string str = "";
            if (guid == _Enums.GuId.small)
            {
                str = Guid.NewGuid().ToString();
            }
            else
            {
                str = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
            }
            return str;
        }

        public static string GeneratePassword(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public static string GeneratePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public static string GetIPAddress()
        {
            string VisitorsIPAddress = string.Empty;
            try
            {
                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    VisitorsIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                {
                    VisitorsIPAddress = HttpContext.Current.Request.UserHostAddress;
                }
            }
            catch { }
            return VisitorsIPAddress;
        }

        public static string RandomGenerator
        {
            get
            {
                var random = new Random();
                string[] colors = { "success", "danger", "warning", "primary", "info", "dark", "secondary" };
                int r = random.Next(colors.Length);
                return ((string)colors[r]);
            }
        }

        public static string RandomColors
        {
            get
            {
                string[] colors = { "#4FC1E9", "#FE424D", "#1AA6B7", "#967ADC", "#48cfad", "#7c69ef", "#d9e2ef", "#df4759", "#42ba96", "#ffc107", "#467fd0", "#4FC1E9", "#161c2d", "#c6d3e6", "#abbcd5", "#869ab8", "#506690", "#384c74", "#467fd0", "#69d2f1", "#6610f2", "#7c69ef", "#e83e8c", "#df4759", "#fd9644", "#ffc107", "#42ba96", "#20c997", "#17a2b8" };
                Random rnd = new Random();
                int r = rnd.Next(colors.Length);
                return ((string)colors[r]);
            }
        }

        public static string GetError(Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder(ex.Message);
            while (ex.InnerException != null)
            {
                stringBuilder.AppendLine().Append(ex.InnerException.Message);
                ex = ex.InnerException;
            }
            return stringBuilder.ToString();
        }
    }
}

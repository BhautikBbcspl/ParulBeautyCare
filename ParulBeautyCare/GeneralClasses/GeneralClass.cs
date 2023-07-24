using ParulBeautyCareViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ParulBeautyCare.GeneralClasses
{
    public abstract class GeneralClass : Controller
    {

        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }

        public LoginViewModel LoggedUserDetails
        {
            get
            {
                LoginViewModel lm = new LoginViewModel();
                HttpCookie reqCookies = Request.Cookies["LoginMaster"];
                if (reqCookies != null)
                {
                    lm.CompanyCode = reqCookies["CompanyCode"].ToString();
                    lm.RoleId = Convert.ToInt32(reqCookies["RoleId"]);
                    lm.UserName = reqCookies["UserName"].ToString();
                    return lm;
                }
                else
                {
                    return lm;
                }
            }
        }

        public class generalFunctions
        {
            #region ==>GeneralFunctions


            public static string Encrypt(string toEncrypt, bool useHashing)
            {
                byte[] keyArray;
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
                string key = "bbcspl";

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }

            public static string Decrypt(string cipherString, bool useHashing)
            {
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);

                string key = "bbcspl";

                if (useHashing)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();
                }
                else
                    keyArray = UTF8Encoding.UTF8.GetBytes(key);

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                tdes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            public static string getTimeZoneDatetimedb()
            {
                var info = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTimeOffset localServerTime = DateTimeOffset.Now;
                DateTimeOffset gatime = TimeZoneInfo.ConvertTime(localServerTime, info);
                string gatimes = gatime.ToString("yyyy-MM-dd HH:mm:ss");
                return gatimes.Replace('.', ':');
            }
            public static string getTimeZoneTime()
            {
                var info = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTimeOffset localServerTime = DateTimeOffset.Now;
                DateTimeOffset gatime = TimeZoneInfo.ConvertTime(localServerTime, info);
                string gatimes = gatime.ToString("HH:mm:ss");
                return gatimes.Replace('.', ':');
            }
            public static string getDate()
            {
                var info = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTimeOffset localServerTime = DateTimeOffset.Now;
                DateTimeOffset gatime = TimeZoneInfo.ConvertTime(localServerTime, info);
                return gatime.ToString("yyyy-MM-dd");
            }


            public static string dateconvert(string strdate)
            {
                string str = "";
                str = strdate.Split('-').ElementAt(2) + "-" + strdate.Split('-').ElementAt(1) + "-" + strdate.Split('-').ElementAt(0);
                return str;
            }

            //public static string DateConvert(string strDate)
            //{
            //    string[] dateParts = strDate.Split('/');
            //    string convertedDate = dateParts[2] + "-" + dateParts[1] + "-" + dateParts[2];
            //    return convertedDate;
            //}

            //convert into dd-MM-YYYY
            public static string ShortDateConvert(string strDate)
            {
                string[] parts = strDate.Split(' '); // Split the string at the space
                if (parts.Length >= 1)
                {
                    strDate = parts[0]; // The date part is the first element in the parts array
                }
                return strDate;
            }

            //Convert Date Time into 2023-07-17 16:12:59.000 formate 
            //public static string DateTimeConvert(string dateString)
            //{

            //    DateTime parsedDate = DateTime.ParseExact(dateString, "dd-MM-yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
            //    string formattedDate = parsedDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //    return formattedDate;
            //}
            public static string DateTimeConvert(string dateString)
            {
                string[] formats = new string[]
                {
        "dd-MM-yyyy HH:mm:ss",
        "dd-MM-yyyy hh:mm:ss tt",
        "MM-dd-yyyy HH:mm:ss",
        "MM-dd-yyyy hh:mm:ss tt",
        "yyyy-MM-dd HH:mm:ss",
        "yyyy-MM-dd hh:mm:ss tt",
        "MM/dd/yyyy HH:mm:ss",
        "MM/dd/yyyy hh:mm:ss tt",
        "yyyy/MM/dd HH:mm:ss",
        "yyyy/MM/dd hh:mm:ss tt",
        "dd MMM yyyy HH:mm:ss",
        "dd MMM yyyy hh:mm:ss tt",
        "MMM dd, yyyy HH:mm:ss",
        "MMM dd, yyyy hh:mm:ss tt",
        "yyyy MMM dd HH:mm:ss",
        "yyyy MMM dd hh:mm:ss tt",
                    // Add more formats here if needed.
                };

                if (DateTime.TryParseExact(dateString, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
                {
                    return parsedDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                }
                else
                {
                    // If the input dateString is not in any of the specified formats, handle the error as needed.
                    // For simplicity, we can return an empty string or an error message.
                    return string.Empty; // or throw an exception or return an error message.
                }
            }


            public static string GetFiscalYear(DateTime date)
            {
                if (date.Month <= 12)
                {
                    return int.Parse(date.ToString("yyyy")).ToString();
                }
                else
                {
                    return date.Year.ToString() + "-" + (int.Parse(date.ToString("yyyy")) + 1).ToString();
                }
                //if (date.Month < 4)
                //{
                //    return (int.Parse(date.ToString("yyyy")) - 1).ToString() + "-" + date.ToString("yyyy");
                //}
                //else
                //{
                //    return date.Year.ToString() + "-" + (int.Parse(date.ToString("yyyy")) + 1).ToString();
                //}
            }

            public static String getCommon(String AbsoluteUri)
            {
                string page = "";
                try
                {
                    page = AbsoluteUri;
                    char[] delimiterChars = { '/' };
                    string[] link = page.Split(delimiterChars);


                    int length = link.Length;
                    var ab = link[length - 2].ToString() + "/" + link[length - 1].ToString();
                    page = ab;
                }
                catch (Exception ex)
                { ex.Message.ToString(); }
                return page;
            }
            #endregion

        }


    }
}
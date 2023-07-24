using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ParulBeautyCareAPI.Models.Logs
{
    public class iNotifyLogger
    {
        public enum LogType
        {
            /// <summary>
            /// Log Type : Information
            /// </summary>
            Information,
            /// <summary>
            /// Log Type : Exception
            /// </summary>
            Exception,
            /// <summary>
            /// Log Type : Warning
            /// </summary>
            Warning
        }
        #region Members
        /// <summary>
        /// _LogFileName : Log File Name
        /// </summary>
        string _LogFileName = "iNotify.log";

        //string _LogFolder = "";
        /// <summary>
        /// Log Information
        /// </summary>
        bool logInformation = false;
        /// <summary>
        /// Log Exception
        /// </summary>
        bool logException = false;
        /// <summary>
        /// Log Warning
        /// </summary>
        bool logWarning = false;
        #endregion

        #region Object Construction
        /// <summary>
        /// CommunicationLogger : Non-Default Constructor
        /// </summary>
        public iNotifyLogger()
        {
            logException = true;
            logInformation = true;
            logWarning = true;
        }

        public iNotifyLogger(string FileName)
        {
            this._LogFileName = FileName;
            logException = true;
            logInformation = true;
            logWarning = true;
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Log Message
        /// </summary>
        /// <param name="classname">Module Name</param>
        /// <param name="method">Method Name</param>
        /// <param name="message">Message</param>
        /// <param name="msgType">Message Type (Ref ConfigurationFiles\\ExpenseManagerConfigurationSettings.xml for behaviour)</param>
        public void LogMessage(string classname, string method, string message, LogType msgType)
        {
            StreamWriter strW = null;
            try
            {
                bool isLoggingRequested = false;
                switch (msgType)
                {
                    case LogType.Exception:
                        if (logException)
                        {
                            isLoggingRequested = true;
                        }
                        break;
                    case LogType.Information:
                        if (logInformation)
                        {
                            isLoggingRequested = true;
                        }
                        break;
                    case LogType.Warning:
                        if (logWarning)
                        {
                            isLoggingRequested = true;
                        }
                        break;
                }
                if (isLoggingRequested)
                {
                    //string logFile = iNotifyUtility.GetInstallPath() + "\\Logs\\" + DateTime.Now.ToString("yyyyMMdd") + "_" + _LogFileName;
                    string logFile = HttpContext.Current.Server.MapPath("~/Models/Logs/") + _LogFileName + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                    string callingAssembly = Assembly.GetCallingAssembly().ToString();
                    strW = new StreamWriter(logFile, true);
                    strW.WriteLine(DateTime.Now + " $$$ " + Convert.ToString(msgType) + " $$$ " + classname + " $$$ " + method + " $$$ " + message);
                }
            }
            catch { }
            finally
            {
                try
                {
                    if (strW != null)
                    {
                        strW.Close();
                        strW = null;
                    }
                }
                catch { }
            }
        }
        #endregion
    }
}
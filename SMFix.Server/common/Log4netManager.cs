using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telewave.Utility;

namespace SMFix.Server
{
    public class Log
    {

        /// <summary>
        /// DEBUG日志输出
        /// </summary>
        /// <param name="Description">日志信息内容</param>
        public static void WriteDebug(string Description)
        {
            LoggerManger.Debug(Description);
        }

        /// <summary>
        /// Error日志输出
        /// </summary>
        /// <param name="Description">日志信息描述</param>
        /// <param name="exception">异常信息</param>
        public static void WriteError(string Description, Exception exception)
        {
            LoggerManger.Error(Description, exception);
        }

        /// <summary>
        /// Fatal日志输出
        /// </summary>
        /// <param name="Description">日志信息描述</param>
        /// <param name="exception">异常信息</param>
        public static void WriteFatal(string Description, Exception exception)
        {
            LoggerManger.Fatal(Description, exception);
        }

        /// <summary>
        /// Info日志输出
        /// </summary>
        /// <param name="Description">日志信息内容</param>
        public static void WriteInfo(string Description)
        {
            LoggerManger.Info(Description);
        }

        /// <summary>
        /// Warn日志输出
        /// </summary>
        /// <param name="Description">日志信息描述</param>
        /// <param name="exception">异常信息</param>
        public static void WriteWarn(string Description, Exception exception)
        {
            LoggerManger.Warn(Description, exception);
        }
    }
}

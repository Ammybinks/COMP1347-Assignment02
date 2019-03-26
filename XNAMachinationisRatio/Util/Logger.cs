using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAMachinationisRatio.Util
{
    /// <summary>
    /// Class responsible for logging on File.
    /// </summary>
    public class Logger
    {
        
        /// <summary>
        /// Log file name
        /// </summary>
        private const String FILE_NAME = @"./XNAMachinationisRatio.log";
        /// <summary>
        /// StreamWriter assigned to log file
        /// </summary>
        //#PILL# Output on file
        private StreamWriter mLogFile;

        private Logger() {
            mLogFile = File.CreateText(FILE_NAME);
            mLogFile.AutoFlush = true;
        }
        /*#PILL# Singleton*/
        private static Logger mSingletonInstance = new Logger();
        
        
        public static Logger getLogger()
        {
            return mSingletonInstance;
        }
        /// <summary>
        /// Log a message on File
        /// </summary>
        /// <param name="pMessage">Message to Log</param>
        public void Log(String pMessage)
        {
            mLogFile.WriteLine(DateTime.Now.Hour +":"+DateTime.Now.Minute + ":" + DateTime.Now.Second + ":" + DateTime.Now.Millisecond + " - " + pMessage);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;

namespace BankingSystemApp.Log
{
    public class RecordLog
    {

        public RecordLog(string logFileNameInput)
        {
            logFileName = logFileNameInput;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;

        }

        bool isBusyChecking = false;
        string logFileName;
        long upLimitSize = 1024000000;//100M
        Timer timer;

        public string LogFileName
        {
            get
            {
                logFileName = workingDirectory + "\\" + logFileName + ".txt";
                return logFileName;
            }
            set { logFileName = value; }
        }
        string workingDirectory = System.IO.Directory.GetCurrentDirectory();


        public bool WriteLogFile(string content)
        {
            bool successed;
            try
            {
                string logFilePathFileName = workingDirectory + "\\" + logFileName + ".txt";
                content = DateTime.Now.ToString("\r\n" + "yyyy/MM/dd hh:mm:ss ") + content;
                File.AppendAllText(logFileName, content, Encoding.UTF8);
                successed = true;
            }
            catch
            {
                successed = false;
            }
            return successed;
        }


        public bool WriteLogFile(List<string> contentList)
        {
            bool result = true;
            List<bool> succeedList = new List<bool>();
            bool tmpSucceed;
            foreach (string tmpContent in contentList)
            {
                tmpSucceed = WriteLogFile(tmpContent);
                succeedList.Add(tmpSucceed);
            }
            foreach (bool tmpSuccess in succeedList)
            {
                if (tmpSuccess == false)
                    result = false;
            }
            return result;
        }

        private void timer_Elapsed(object sender, EventArgs e)
        {
            if (!isBusyChecking)
            { 
                CheckLogFileSize();
            }
        }

        public bool CheckLogFileSize()
        {
            isBusyChecking = true;
            if (File.Exists(logFileName))
            {
                FileInfo fileInfo = new FileInfo(logFileName);
                if (fileInfo.Length > upLimitSize)
                {
                    File.Delete(logFileName);
                    isBusyChecking = false;
                    return true;
                }
            }
            isBusyChecking = false;
            return false;
        }
    }


    /// <summary>
    /// Singleton
    /// </summary>
    public class RecordLogProvider
    {
        private static RecordLog instance;
        private static readonly object syncRoot = new object();
        public static RecordLog GetInstance(string logFileNameInput)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new RecordLog(logFileNameInput);
                    }
                }
            }
            return instance;
        }
    }
}
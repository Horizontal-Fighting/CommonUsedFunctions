using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvFileProcessor
{
    public class FilesReader
    {
        //fields
        string folderStr;
        FileReader[] fileReaderArr;
        BackgroundWorker bg;
        Int64 processedRowNumber;

        public long ProcessedRowNumber
        {
            get
            {
                return processedRowNumber;
            }
        }

        //with parameter
        public delegate void LineReadedEventHandler(object sender, LineReadedEventArgs e);
        public event LineReadedEventHandler LineReaded;

        //no parameter
        public event EventHandler FilesReadCompleted;

        //constructor
        public FilesReader(string _folderStr)
        {
            folderStr = _folderStr;
            fileReaderArr = GetFileReaderArr(_folderStr);
            processedRowNumber = 0;
        }


        #region private
        //private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    //do nothing
        //}

        //private void Bg_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    BeginReadLine();
        //}


        private FileReader[] GetFileReaderArr(string _folderStr)
        {
            try
            {
                string[] files = Directory.GetFiles(folderStr, "*.csv");

                FileReader[] resultArr = new FileReader[files.Length];
                for (int i = 0; i < resultArr.Length; i++)
                {
                    resultArr[i] = new FileReader(files[i]);
                }

                return resultArr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        #endregion


        /// <summary>
        /// return line with Biggest FileSize line by line.
        /// Time consumption method;
        /// </summary>
        public void BeginReadLine()
        {
            //1 read 1 line in each file
            foreach (var tmpReader in fileReaderArr)
            {
                tmpReader.ReadOneLine();
            }

            while (fileReaderArr.Any(r => r.CurLine != null))
            {
                //2 find line with biggest file size
                var fileReaderWithBiggestFileSize = fileReaderArr.Select(r => r)
                    .OrderByDescending(r => r.CurLineFileSize)
                    .FirstOrDefault();
                string lineWithBiggestFileSize = fileReaderWithBiggestFileSize.CurLine;
                var lineReadedE = new LineReadedEventArgs();
                lineReadedE.CurLineStr = lineWithBiggestFileSize;
                LineReaded(this, lineReadedE);
                processedRowNumber++;

                //3 fileReaderWithBiggestFileSize read 1 more line;
                fileReaderWithBiggestFileSize.ReadOneLine();
            }

            FilesReadCompleted(this, new EventArgs());
        }

    }

    public class LineReadedEventArgs : EventArgs
    {
        string curLineStr;
        public string CurLineStr
        {
            get
            {
                return curLineStr;
            }

            set
            {
                curLineStr = value;
            }
        }
    }


    public class FilesReaderProvider
    {
        private static FilesReader instance;
        private static readonly object syncRoot = new object();
        public static FilesReader GetInstance(string _folderStr)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new FilesReader(_folderStr);
                    }
                }
            }
            return instance;
        }
    }


}

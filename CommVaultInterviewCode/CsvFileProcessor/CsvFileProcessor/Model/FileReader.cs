using CsvFileProcessor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvFileProcessor
{
    public class FileReader
    {
        string fileName;
        StreamReader fileStreamReader;
        String curLine;
        Int64 curLineFileSize;
        Int64 curIndex = 0;

        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

        public StreamReader FileStreamReader
        {
            get
            {
                return fileStreamReader;
            }

            set
            {
                fileStreamReader = value;
            }
        }

        public string CurLine
        {
            get
            {
                return curLine;
            }

            set
            {
                curLine = value;
            }
        }

        public long CurLineFileSize
        {
            get
            {
                return curLineFileSize;
            }

            set
            {
                curLineFileSize = value;
            }
        }

        public FileReader(string _fullFileName)
        {
            if (File.Exists(_fullFileName))
            {
                fileName = _fullFileName;
                fileStreamReader = new StreamReader(fileName);
            }
            else
                throw new  FileNotFoundException(_fullFileName);
            
        }


        /// <summary>
        /// read one line until end (return null;)
        /// </summary>
        /// <returns></returns>
        public string ReadOneLine()
        {
            if (curIndex == 0 || !string.IsNullOrEmpty(curLine))
            {
                curLine = fileStreamReader.ReadLine();
                if (!string.IsNullOrEmpty(curLine))
                {
                    this.curIndex++;
                    FileRecord fr = new FileRecord();
                    this.curLineFileSize = fr.GetSize(curLine);
                    return this.curLine;
                }
                return null;
            }
            else
                return null;
        }

        

    }



    

}

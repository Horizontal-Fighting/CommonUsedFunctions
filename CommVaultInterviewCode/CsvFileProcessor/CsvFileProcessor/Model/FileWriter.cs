using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvFileProcessor.Model
{
    public class FileWriter
    {
        string fileName = string.Empty;
        StreamWriter sw;
        public FileWriter(string _fullFileName)
        {
            fileName = _fullFileName;
            sw = File.AppendText(fileName);
        }

        public void AppendLine(String lineStr)
        {
             sw.WriteLine(lineStr);
        }


        #region 析构函数
        //显示实现Dispose接口，避免同时出现Dispose方法和自定义命名方法(Close)
        public void Dispose()
        {
            //释放所有资源
            Dispose(true);
            //避免重复调用Finalize函数
            GC.SuppressFinalize(this);
        }

        //内部Dispose方法，真正试试资源释放工作
        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    // Release managed resources
                    sw.Close();
                    sw.Dispose();
                    // this.Dispose();
                }
                // Release unmanaged resources
                m_disposed = true;
            }
        }

        //在Finalize函数中调用内部的Dispose方法
        ~FileWriter()
        {
            //被自动回收是仅释放托管资源，不释放非托管资源
            Dispose(false);
        }

        private bool m_disposed;
        #endregion

    }
}

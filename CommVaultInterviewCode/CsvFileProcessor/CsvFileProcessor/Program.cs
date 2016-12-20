using CsvFileProcessor.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvFileProcessor
{
    class Program
    {
        static string outputFN;
        static string inputFolder;
        static Stopwatch sw;
        static FileWriter fw;

        static void Main(string[] args)
        {
            sw = new Stopwatch();
            sw.Start();
            outputFN = @"C:\Users\rong.fan\Desktop\CommVaultInterviewCode\OutPut\result.csv";
            inputFolder = @"C:\Users\rong.fan\Desktop\CommVaultInterviewCode\allcores";
            fw = new FileWriter(outputFN);

            FilesReader filesReader = FilesReaderProvider.GetInstance(inputFolder);
            filesReader.LineReaded += FilesReader_LineReaded; ;
            filesReader.FilesReadCompleted += FilesReader_FilesReadCompleted;
            filesReader.BeginReadLine();
        }

        private static void FilesReader_LineReaded(object sender, LineReadedEventArgs e)
        {
            var filesReader = (FilesReader)sender;
            Console.WriteLine(filesReader.ProcessedRowNumber);
            fw.AppendLine(e.CurLineStr);
        }

        private static void FilesReader_FilesReadCompleted(object sender, EventArgs e)
        {
            sw.Stop();
            Console.WriteLine("Done, time consumed(s): "+ sw.Elapsed.TotalSeconds);
            Console.ReadKey();
        }

       
    }
}

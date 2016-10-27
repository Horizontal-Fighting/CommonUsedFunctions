/*Created by:Rong Fan
 *email:rong.fan1031@gmail.com
 *Desc: using async and await to implement async operation
 *Desc: I get a better performance now
 *Dt: 2016-9-12
 *Version:.NET 4.5
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Await_Async2
{
    class Program
    {
        //创建计时器; create a timer
        private static readonly Stopwatch Watch = new Stopwatch();

        static void Main(string[] args)
        {
            //启动计时器; start timer
            Watch.Start();

            const string url1 = "http://www.cnblogs.com/";
            const string url2 = "http://www.cnblogs.com/liqingwen/";

            //两次调用 CountCharacters 方法（下载某网站内容，并统计字符的个数）
            //evoke CountCharacters 2 times
            Task<int> result1 = CountCharacters(1, url1);
            Task<int> result2 = CountCharacters(2, url2);

            //三次调用 ExtraOperation 方法（主要是通过拼接字符串达到耗时操作）
            //invoke ExtraOperation 3 times, time cosuming operation
            for (var i = 0; i < 3; i++)
            {
                ExtraOperation(i + 1);
            }

            //控制台输出; output
            Console.WriteLine(url1+ "word number：" + result1);
            Console.WriteLine(url2+ "word number：" + result2);

            Console.ReadKey();
        }


   

        /// <summary>
        /// 统计字符个数; count word number
        /// </summary>
        /// <param name="id"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        private static async Task<int> CountCharacters(int id, string address)
        {
            var wc = new WebClient();
            Console.WriteLine("begin id ="+ id+":" +Watch.ElapsedMilliseconds+" ms");

            var result = await wc.DownloadStringTaskAsync(address);
            Console.WriteLine("finished id=" + id+ ":" +Watch.ElapsedMilliseconds+" ms");

            return result.Length;
        }

         /// <summary>
        /// 额外操作; extra operation
        /// </summary>
        /// <param name="id"></param>
        private static void ExtraOperation(int id)
        {
            //这里是通过拼接字符串进行一些相对耗时的操作
            //time consuming operation
            var s = "";
            for (var i = 0; i < 6000; i++)
            {
                s += i;
            }
            Console.WriteLine("id ="+id+ " ExtraOperation finidhed："+ Watch.ElapsedMilliseconds +"ms");
        }

    }
}

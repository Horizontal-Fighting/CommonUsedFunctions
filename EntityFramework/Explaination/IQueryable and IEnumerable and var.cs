/*Created by:Rong Fan
 *email:rong.fan1031@gmail.com
 *Desc: difference between IQueryable and IEnumerable and var
 *Dt: 2016-10-3
 * Version:.NET 4.5
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> ints = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                ints.Add(i);
            }

            // 延时加载，在数据库中筛选；
            //lazy loading， filter from database 
            IQueryable<int> query1 = (from c in ints
                                   where c > 50
                                   select c).AsQueryable();
            var res1 = query1.Where(r => r > 50);

            // 立即加载，同时，在内存中筛选
            // filter in memory
            IEnumerable<int> query2 = from c in ints
                        where c > 50
                        select c;
            var res2 = query2.Where(r => r > 50);

            // 默认是IEnumerable
            // default is IEnumerable
            var query3 = from c in ints
                                      where c > 50
                                      select c;
            var res3 = query3.Where(r => r > 50);

        }
    }
}

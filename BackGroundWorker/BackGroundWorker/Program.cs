/*Created by:Rong Fan
 *email:rong.fan1031@gmail.com
 *Desc: using BackGroundWorker to implement multiple threads
 *Dt: 2016-9-1
 * Version:.NET 4.0
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BackGroundWorker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

/* Created by:Rong Fan
 * email:rong.fan1031@gmail.com
 * Desc: sigleton design pattern
 * Dt: 2016-9-5
 * Version:.NET 2.0
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public class Singleton
    {

    }


    /// <summary>
    /// Singleton
    /// </summary>
    public class SingletonProvider
    {
        private static Singleton instance;
        private static readonly object syncRoot = new object();
        public static Singleton GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public class A
    {
    }

    /// <summary>
    /// 单例模式;Singleton
    /// </summary>
    public class AProvider
    {
        private static A instance;
        private static readonly object syncRoot = new object();
        public static A GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new A();
                    }
                }
            }
            return instance;
        }
    }
}

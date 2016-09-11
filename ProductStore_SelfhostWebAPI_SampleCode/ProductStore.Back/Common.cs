using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OwinSelfhostSample
{
    public class Common
    {
        public static int Port = 9000;

        public static string BaseAddress = "http://localhost:" + Common.Port + "/";

        /// <summary>
        /// Whether port is occupied
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool IsPortOccupied(int port)
        {
            if (port < 0 || port > 65535)
                throw new IndexOutOfRangeException("port is out of range.");

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var ipEndPoints = ipProperties.GetActiveTcpListeners().ToList();
            return ipEndPoints.Any(r=>r.Port==port);
        }

    }
}

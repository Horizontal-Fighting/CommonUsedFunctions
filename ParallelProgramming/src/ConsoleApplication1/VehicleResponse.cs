/*Created by:Rong Fan
 *email:rong.fan1031@gmail.com
 *Desc: using Parallel programming to optimize performance
 *Desc: 
 *Dt: 2016-9-30
 *Version:.NET 4.5
 */
using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class MyVehicleResponse: VehicleAnswer
    {
        int dealerId;

        public int DealerId
        {
            get
            {
                return dealerId;
            }

            set
            {
                dealerId = value;
            }
        }
    }
}

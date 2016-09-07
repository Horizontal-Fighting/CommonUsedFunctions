/* Created by:Rong Fan
 * email:rong.fan1031@gmail.com
 * Desc: sigleton design pattern
 * Dt: 2016-9-6
 * Version:.NET 2.0
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class Product
    {
        int id;
        string name;
        decimal pricePerDay;
        DateTime startDateTime, endDateTime;

        public DateTime EndDateTime
        {
            get { return endDateTime; }
            set { endDateTime = value; }
        }

        public DateTime StartDateTime
        {
            get { return startDateTime; }
            set { startDateTime = value; }
        }
        public decimal PricePerDay
        {
            get { return pricePerDay; }
            set { pricePerDay = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

}

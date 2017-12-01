using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class Commision:BaseEntity
    {
        public Commisions Commisions { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Year
        {
            get
            {
                year = (EndDate - StartDate).TotalDays / 365.0;
                return year;
            }
        }

        public decimal Amount { get; set; }

        public CurrencyType CurrencyType { get; set; }

        private double year;
    }

    public class Commisions : BaseEntity
    {
        public Account Account { get; set; }
        public List<Commision> CommisionList { get; set; }
        public Commision CurrentYearCommision { get; set; }
    }
}

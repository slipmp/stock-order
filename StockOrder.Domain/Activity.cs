using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrder.Domain
{
    public class Activity
    {
        public ActivityType Type { get; set; }
        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public Stock StockItem { get; set; }
    }
}

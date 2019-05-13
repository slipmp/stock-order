using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrder.Domain
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Exchange { get; set; }
    }

    public class StockHolding
    {
        public Stock Stock { get; set; }
        public Int64 Quantity { get; set; }
        
        public override string ToString()
        {
            return $"{this.Stock.Symbol}:{this.Stock.Exchange} - Quantity: {this.Quantity}";
        }
    }
}

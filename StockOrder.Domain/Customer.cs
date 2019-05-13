using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrder.Domain
{
    public class Customer
    {
        public IList<StockHolding> StockHoldings;
        public double TotalBalance;

        public Customer()
        {
            StockHoldings = new List<StockHolding>();
        }

        public StockHolding DoesCustomerOwnStock(Stock stock)
        {
            var result = this.StockHoldings.Where(
                x =>
                x.Stock.Symbol.ToLower() == stock.Symbol.ToLower() &&
                x.Stock.Exchange.ToLower() == stock.Exchange.ToLower()).FirstOrDefault();

            return result;
        }
    }
}

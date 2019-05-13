using StockOrder.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockOrder.Core
{
    public class CustomerService
    {
        public void CalculateBalance(Customer customer, IList<Activity> listActivity)
        {
            foreach (var activity in listActivity)
            {
                switch (activity.Type)
                {
                    case ActivityType.Deposit:
                    case ActivityType.Withdraw:
                        customer.TotalBalance = customer.TotalBalance + activity.Amount;
                        break;
                    case ActivityType.Buy:
                        PerformStockTransaction(customer, activity);
                        break;

                    case ActivityType.Sell:
                        PerformStockTransaction(customer, activity);
                        break;
                }
            }
        }

        private void PerformStockTransaction(Customer customer, Activity activity)
        {
            var stockHolding = AddOrReturnStockHolding(customer, activity);

            //Activity amount has the final amount, calculated according to amount of bought stocks
            if (activity.Type == ActivityType.Buy)
            { 
                //Reference type :)
                stockHolding.Quantity = stockHolding.Quantity + Convert.ToInt64(activity.Quantity);
                customer.TotalBalance = customer.TotalBalance - activity.Amount;
            }
            else if(activity.Type==ActivityType.Sell)
            { 
                stockHolding.Quantity = stockHolding.Quantity - Convert.ToInt64(activity.Quantity);
                customer.TotalBalance = customer.TotalBalance + activity.Amount;
            }                
        }

        private StockHolding AddOrReturnStockHolding(Customer customer, Activity activity)
        {
            var stockHolding = customer.DoesCustomerOwnStock(activity.StockItem);

            if (stockHolding == null)
            {
                var stock = new Stock { Exchange = activity.StockItem.Exchange, Symbol = activity.StockItem.Symbol };
                var newStockHolding = new StockHolding { Stock = stock};

                customer.StockHoldings.Add(newStockHolding);
                return newStockHolding;
            }
            return stockHolding;
        }
    }
}

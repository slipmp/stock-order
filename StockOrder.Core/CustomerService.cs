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
                        BuyStock(customer, activity);
                        break;

                    case ActivityType.Sell:
                        SellStock(customer, activity);
                        break;
                }
            }
        }

        private void BuyStock(Customer customer, Activity activity)
        {
            var stockHolding = customer.DoesCustomerOwnStock(activity.StockItem);

            //Customer owns stock
            if (stockHolding != null)
            {
                //Reference type :)
                stockHolding.Quantity = stockHolding.Quantity + Convert.ToInt64(activity.Quantity);
                //Activity amount has the final amount, calculated according to amount of bought stocks
                customer.TotalBalance = customer.TotalBalance + activity.Amount;
                return;
            }
            var stock = new Stock { Exchange = activity.StockItem.Exchange, Symbol = activity.StockItem.Symbol };
            var newStockHolding = new StockHolding { Stock = stock, Quantity = activity.Quantity };
            customer.TotalBalance = customer.TotalBalance - activity.Amount;

            customer.StockHoldings.Add(newStockHolding);
        }

        private void SellStock(Customer customer, Activity activity)
        {
            var stockHolding = customer.DoesCustomerOwnStock(activity.StockItem);

            //Customer owns stock
            if (stockHolding != null)
            {
                //Reference type :)
                stockHolding.Quantity = stockHolding.Quantity - Convert.ToInt64(activity.Quantity);
                //Activity amount has the final amount, calculated according to amount of bought stocks
                customer.TotalBalance = customer.TotalBalance + activity.Amount;
                return;
            }
            var stock = new Stock { Exchange = activity.StockItem.Exchange, Symbol = activity.StockItem.Symbol };
            var newStockHolding = new StockHolding { Stock = stock, Quantity = activity.Quantity };
            customer.TotalBalance = customer.TotalBalance + activity.Amount;

            customer.StockHoldings.Add(newStockHolding);
        }
    }
}

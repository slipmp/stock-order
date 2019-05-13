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
                        //Withdraw is stored as a negative value
                        customer.TotalBalance = customer.TotalBalance + activity.Amount;
                        break;
                    case ActivityType.Buy:
                    case ActivityType.Sell:
                        PerformStockTransaction(customer, activity);
                        break;
                }
            }

            //Since the Activities order does not matter, I am not validating per individual activity
            //rather, I am only validating balance at the end
            if (customer.TotalBalance < 0)
                throw new InvalidOperationException($"Customer cannot have negative balance after all transactions performed. Total Balance is:{customer.TotalBalance}");
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
                //I am not validating negative balance on Stocks
                //Reasons:
                //1 - It was not mentioned such use case
                //2 - Perhaps we should consider short selling somehow? By having negative balance?
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

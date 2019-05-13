using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrder.Core;
using StockOrder.Domain;

namespace StockOrder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var costumer = new Customer();
            var customerService = new CustomerService();

            var listActivity = ReturnListActivityForTesting();

            customerService.CalculateBalance(costumer, listActivity);

            foreach(var stockHolding in costumer.StockHoldings)
            {
                //Output logic located on ToString()
                System.Console.WriteLine(stockHolding);
            }

            //Expected balance will be:
            //ABC.CA = 5
            //ABC.US = 3
            //Amount: $78
            System.Console.WriteLine($"Balance: ${costumer.TotalBalance}");
            System.Console.ReadKey();
        }

        private static IList<Activity> ReturnListActivityForTesting()
        {
            var listActivity = new List<Activity>
            {
                new Activity { Type = ActivityType.Deposit, Date = new DateTime(2019, 1, 1), Amount = 200 },
                new Activity { Type = ActivityType.Withdraw, Date = new DateTime(2019, 1, 1), Amount = -50 },

                //Stock initially valued as $10 per stock
                new Activity { Type = ActivityType.Buy, Date = new DateTime(2019, 1, 1), Amount = 100,
                    StockItem =new Stock{Exchange="CA",Symbol="ABC" },Quantity=10 },

                //Sold Stock as $15 - therefore having some profit
                //I don't remember if Amount when selling was a negative value
                new Activity { Type = ActivityType.Sell, Date = new DateTime(2019, 1, 1), Amount = 75,
                    StockItem =new Stock{Exchange="CA",Symbol="ABC" },Quantity=5 },

                //Stock initially valued as $13 per stock
                new Activity { Type = ActivityType.Buy, Date = new DateTime(2019, 1, 1), Amount = 91,
                    StockItem =new Stock{Exchange="US",Symbol="ABC" },Quantity=7 },

                //Stold stock as $11 - therefore having losses
                new Activity { Type = ActivityType.Sell, Date = new DateTime(2019, 1, 1), Amount = 44,
                    StockItem =new Stock{Exchange="US",Symbol="ABC" },Quantity=4 },

            };

            return listActivity;
        }
    }
}

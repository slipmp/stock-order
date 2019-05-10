using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockOrder.Domain;

namespace StockOrder.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var listActivity = new List<Activity>
            {
                new Activity { Type = ActivityType.Deposit, Date = new DateTime(2019, 1, 1), Amount = 200 },
                new Activity { Type = ActivityType.Withdraw, Date = new DateTime(2019, 1, 1), Amount = -50 },

                new Activity { Type = ActivityType.Buy, Date = new DateTime(2019, 1, 1), Amount = 200,
                    StockItem =new Stock{Exchange="CA",Symbol="ABC" } },
                new Activity { Type = ActivityType.Sell, Date = new DateTime(2019, 1, 1), Amount = 200 },
                new Activity { Type = ActivityType.Buy, Date = new DateTime(2019, 1, 1), Amount = 200 },
                new Activity { Type = ActivityType.Sell, Date = new DateTime(2019, 1, 1), Amount = 200 }
            };

            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            System.Console.WriteLine("Hello World!");
            System.Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}

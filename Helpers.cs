using System;
using System.Collections.Generic;

namespace Advantage.Api
{
    //contains subroutines to help generate randomised values for matching fields outlined in DataSeed class

    public class Helpers
    {
        // Generates a random number for an index used for picking a string values from a list.
        private static Random _rand = new Random();

        internal static string GetRandom(IList<string> items)
        {
            return items[_rand.Next(items.Count)];
        }

        internal static string MakeUniqueCustomerName(List<string> names)
        {
            // Helps avoid getting trapped in a recrsuve loop whereby MakeUniqueCustomerName calls itself.
            var maxNames = bizPrefix.Count * bizSuffix.Count;
            if (names.Count >= maxNames)
            {
                throw new System.InvalidOperationException("Maximum number of unique names exceeded");
            }
            //picks random prefix and suffix's from arrays to generate a business name
            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bizSuffix);
            // brute force check tmake sure randomised name is unique
            var bizName = prefix + " " + suffix;
            if (names.Contains(bizName))
            {
                //if the list of names contains bizName the the method is called recursively to produce another random name.
                MakeUniqueCustomerName(names);
            }
            return bizName;
        }

        internal static string MakeCustomerEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com";
        }

        internal static string GetRandomState()
        {
            return GetRandom(usStates);
        }

        internal static Decimal GetRandomOrderTotal()
        {
            // generates a random order between a 100 and a thousand.
            return _rand.Next(100, 1000);
        }

        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now; //to make sure orders can't beplaced in the future.
            var start = end.AddDays(-90); // to make sure order placed within the last 90 days to limit an infinite past.
            // To create a time window in which a new order could be placed.
            // The time is generated in a random number of minutes which are added to a random start time within the last 90 days.
            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);
            return start + newSpan;
        }

        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            // calculating the time paased since the order was placed.
            var timePassed = now - orderPlaced;

            if (timePassed < minLeadTime)
            {
                return null;
            }
            // generates a random number between 7 & 14 days and adds the days onto the orderplaced.    
            return orderPlaced.AddDays(_rand.Next(7, 14));

        }





        // Private Static Lists below Here
        private static readonly List<string> bizPrefix = new List<string>()
        {
            "ABC",
            "XYZ",
            "Acme",
            "MainSt",
            "Ready",
            "Magic",
            "Fluent",
            "Peak",
            "Forward",
            "Enterprise",
            "Sales"
        };

        private static readonly List<string> bizSuffix = new List<string>()
        {
            "Co",
            "Corp",
            "Holdings",
            "Corporation",
            "Movers",
            "Cleaners",
            "Bakery",
            "Apparel",
            "Rentals",
            "Storage",
            "Transit",
            "Logistics"
        };
        internal static readonly List<string> usStates = new List<string>()
        {
            "AK", "AL","AZ",  "AR", "CA", "CO", "CT", "DE", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
        };

    }

}
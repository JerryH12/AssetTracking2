using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracking2
{
    internal class AssetsBLL
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Office { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PriceInUSD { get; set; }
        public double LocalPrice { get; set; }
        public string Currency { get; set; }

        private Dictionary<string, string> countryCode = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Sweden", "SE" }, {"USA", "US" }, {"India", "IN" }
        };

        private Dictionary<string, double> exchangeRates = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
        {
            { "SEK", 10.50 }, {"INR", 83.04 }
        };

        [Flags]
        public enum OfficeType
        {
            sweden = 0,
            india = 1,
            usa = 2
        }
        public AssetsBLL(string office, DateTime purchaseDate, double price)
        {
            try
            {
                if (!Enum.IsDefined(typeof(OfficeType), office.ToLower()))
                {
                    throw new ArgumentException($"\nError: {office} is not a valid office!\n");
                }

                Office = office;
                PurchaseDate = purchaseDate;
                PriceInUSD = price;
                Currency = GetCurrencySymbol(office);
                LocalPrice = GetLocalPrice(Currency);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private double GetLocalPrice(string currency)
        {
            double localPrice;

            switch (currency)
            {
                case "SEK":
                    localPrice = exchangeRates["SEK"] * PriceInUSD;
                    break;
                case "INR":
                    localPrice = exchangeRates["INR"] * PriceInUSD;
                    break;
                default:
                    localPrice = PriceInUSD;
                    break;
            }
            return localPrice;
        }

        private string GetCurrencySymbol(string country)
        {
            var ri = new RegionInfo(countryCode[country]);
            return ri.ISOCurrencySymbol;
        }

        public virtual int GetRemainingLife()
        {
            TimeSpan span = DateTime.Now.Subtract(PurchaseDate);
            int remainingDays = 365 * 3 - span.Days;
            return remainingDays;
        }

        public virtual void Print(int index=-1)
        {
            string number = (index >= 0) ? index+"." : "";

            int remainingLife = GetRemainingLife();

            if (remainingLife > 0)
            {
                Console.Write(number); // An index number for selecting items.

                if (remainingLife < 90)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (remainingLife < 180)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.WriteLine(Type.PadRight(12) + Brand.PadRight(12) + Model.PadRight(12) + Office.PadRight(10) + PurchaseDate.ToString("yy/MM/dd").PadRight(15) + PriceInUSD.ToString().PadRight(15) + Currency.PadRight(12) + LocalPrice.ToString());
                Console.ResetColor();
            }
        }
    }
}

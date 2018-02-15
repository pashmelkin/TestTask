using System;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace TestTask
{
    public class PricingRule
    {
        public JobPrices Prices;
        private IDeal CurrentDeal = null;

        public bool IsDealExists()
        {
            return true;
        }

        public IDeal GetCurrentDeal()
        {
            return CurrentDeal;
        }

        public PricingRule(CompanyDeal deal)
        {
            try
            {
                using (var r = new StreamReader("JobPrices.json"))
                {
                    var json = r.ReadToEnd();
                    var settings = JsonConvert.DeserializeObject<Settings>(json);
                    Prices = settings.JobPrices;
                    CurrentDeal = deal ?? new CompanyDeal();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: Cannot get the prices for ads: {ex}");
            }
        }

    }
    
}
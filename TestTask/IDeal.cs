using System;
using System.IO;
using Newtonsoft.Json;

namespace TestTask
{
    public interface IDeal
    {
        double ClassicDeal(int numJobs);
        double StandOutDeal(int numJobs);
        double PremiumDeal(int numJobs);
    }

    public class CompanyDeal : IDeal
    {
        public string AdvertiserName;
        public JobPrices StandardPrices;
        public JobPrices DiscountPrices = null;

        public CompanyDeal()
        {
            using (var r = new StreamReader("JobPrices.json"))
            {
                var json = r.ReadToEnd();
                var settings = JsonConvert.DeserializeObject<Settings>(json);
                this.StandardPrices = settings.JobPrices;
            }
        }
        public virtual double ClassicDeal(int numJobs)
        {
            return numJobs * StandardPrices.Classic;
        }

        public virtual double PremiumDeal(int numJobs)
        {
            return numJobs * StandardPrices.Premium;
        }

        public virtual double StandOutDeal(int numJobs)
        {
            return numJobs * StandardPrices.Standout;
        }
    }

    public class AppleDeal : CompanyDeal
    {
        public AppleDeal()
        {
            this.AdvertiserName = "Apple";
            using (var r = new StreamReader("AppleJobPrices.json"))
            {
                var json = r.ReadToEnd();
                var settings = JsonConvert.DeserializeObject<Settings>(json);
                this.DiscountPrices = settings.JobPrices;
            }
        }
        public override double StandOutDeal(int numJobs)
        {
            if (DiscountPrices?.Standout == null)
            {
                Console.WriteLine($"Error:  {this.AdvertiserName}: no setup for Standout price");
                return numJobs * StandardPrices.Standout;
            }
            return numJobs * DiscountPrices.Standout;
        }
    }

    public class NikeDeal : CompanyDeal
    {
        private readonly int DISCOUNT_PREMIUM_JOB_NUMBER = 4;
        public NikeDeal()
        {
            this.AdvertiserName = "Nike";
            using (var r = new StreamReader("NikeJobPrices.json"))
            {
                var json = r.ReadToEnd();
                var settings = JsonConvert.DeserializeObject<Settings>(json);
                this.DiscountPrices = settings.JobPrices;
            }
        }

        public override double PremiumDeal(int numJobs)
        {
            if (DiscountPrices?.Premium == null || numJobs < DISCOUNT_PREMIUM_JOB_NUMBER)
            {
                if (numJobs >= DISCOUNT_PREMIUM_JOB_NUMBER)
                {
                    Console.WriteLine($"Error:  {this.AdvertiserName}: no setup for Premium price");
                }
                return numJobs * StandardPrices.Premium;
            }
            return numJobs * DiscountPrices.Premium;
        }

    }

    public class FordDeal : CompanyDeal
    {
        private readonly int DISCOUNT_PREMIUM_JOB_NUMBER = 3;

        public FordDeal()
        {
            this.AdvertiserName = "Ford";
            using (var r = new StreamReader("FordJobPrices.json"))
            {
                var json = r.ReadToEnd();
                var settings = JsonConvert.DeserializeObject<Settings>(json);
                this.DiscountPrices = settings.JobPrices;
            }
        }
        public override double ClassicDeal(int numJobs)
        {
            return CurrentDeals.FiveForFourClassicAds(numJobs) * StandardPrices.Classic;
        }

        public override double StandOutDeal(int numJobs)
        {
            if (DiscountPrices?.Standout == null)
            {
                Console.WriteLine($"Error: {this.AdvertiserName}: no setup for Standout price");
                return numJobs * StandardPrices.Standout;
            }
            return numJobs * DiscountPrices.Standout;
        }
        public override double PremiumDeal(int numJobs)
        {
            if (DiscountPrices?.Premium == null || numJobs < DISCOUNT_PREMIUM_JOB_NUMBER)
            {
                if (numJobs >= DISCOUNT_PREMIUM_JOB_NUMBER)
                {
                    Console.WriteLine($"Error: {this.AdvertiserName}: no setup for Premium price");
                }
                return numJobs * StandardPrices.Premium;
            }
            return numJobs * DiscountPrices.Premium;
        }
    }

    public class UnilevierDeal : CompanyDeal
    {
      
        public UnilevierDeal()
        {
            this.AdvertiserName = "Uniliever";
        }

        public override double ClassicDeal(int numJobs)
        {
            return CurrentDeals.ThreeForTwoClassicAds(numJobs) * StandardPrices.Classic;
        }
  
    }

    public class CurrentDeals 
    {
        public static int ThreeForTwoClassicAds(int numClassicAds)
        {
            int i = numClassicAds/3;
            var rest = numClassicAds % 3;
            return i * 2 + rest;
        }
        public static int FiveForFourClassicAds(int numClassicAds)
        {
            int i = numClassicAds / 5;
            var rest = numClassicAds % 5;
            return i * 4 + rest;
        }
        public int StandOutDiscount()
        {
            return 4;
        }
    }
}
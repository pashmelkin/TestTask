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
        public JobPrices StandardPrices;
        public JobPrices DiscountPrices = null;

        public CompanyDeal(string pricesFileName = "JobPrices.json")
        {
            using (var r = new StreamReader(pricesFileName))
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
        public AppleDeal(string appleFielName) : base()
        {
            using (var r = new StreamReader(appleFielName))
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
                Console.WriteLine($"Error: no setup for Standout price");
                return numJobs * StandardPrices.Standout;
            }
            return numJobs * DiscountPrices.Standout;
        }
    }

    public class NikeDeal : CompanyDeal
    {
        private readonly int DISCOUNT_PREMIUM_JOB_NUMBER = 4;
        public NikeDeal(string nikeFileName)
        {
            using (var r = new StreamReader(nikeFileName))
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
                    Console.WriteLine($"Error: no setup for Premium price");
                }
                return numJobs * StandardPrices.Premium;
            }
            return numJobs * DiscountPrices.Premium;
        }

    }

    public class FordDeal : CompanyDeal
    {
        private readonly int DISCOUNT_PREMIUM_JOB_NUMBER = 3;

        public FordDeal(string fordFileName)
        {
            using (var r = new StreamReader(fordFileName))
            {
                var json = r.ReadToEnd();
                var settings = JsonConvert.DeserializeObject<Settings>(json);
                this.DiscountPrices = settings.JobPrices;
            }
        }
        public override double ClassicDeal(int numJobs)
        {
            int i = numJobs / 5;
            var rest = numJobs % 5;
            return (i * 4 + rest) * StandardPrices.Classic;
        }

        public override double StandOutDeal(int numJobs)
        {
            if (DiscountPrices?.Standout == null)
            {
                Console.WriteLine($"Error: no setup for Standout price");
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
                    Console.WriteLine($"Error: no setup for Premium price");
                }
                return numJobs * StandardPrices.Premium;
            }
            return numJobs * DiscountPrices.Premium;
        }
    }

    public class UnilevierDeal : CompanyDeal
    {
            
        public override double ClassicDeal(int numJobs)
        {
            int i = numJobs / 3;
            var rest = numJobs % 3;
            return (i * 2 + rest) * StandardPrices.Classic;
        }
  
    }
 
}
using System;
using System.IO;
using Newtonsoft.Json;
using TestTask;

namespace TestTask
{
    public interface IStrategy
    {
        double GetPrice(int numJobs);
    }

    public abstract class Strategy : IStrategy
    {
        public double StandardPrice;
        public double DiscountedPrice;

        public virtual double GetPrice(int numJobs)
        {
            return numJobs * StandardPrice;
        }
        protected double ReadPrice(string pricesFileName, JobTypes priceType)
        {
            using (var r = new StreamReader(pricesFileName))
            {
                var json = r.ReadToEnd();
                var settings = JsonConvert.DeserializeObject<Settings>(json);
                switch (priceType)
                {
                    case JobTypes.Classic:
                        return settings.JobPrices.Classic;
                    case JobTypes.Standout:
                        return settings.JobPrices.Standout;
                    case JobTypes.Premium:
                        return settings.JobPrices.Premium;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(priceType), priceType, null);
                }

            }
        }
    }


    public class ClassicJobStrategy : Strategy
    {
        public  ClassicJobStrategy(string pricesFileName = "JobPrices.json")
        {
            this.StandardPrice = ReadPrice(pricesFileName, JobTypes.Classic);
        }
    }

    public class StandOutJobStrategy : Strategy
    {
        public StandOutJobStrategy(string pricesFileName = "JobPrices.json")
        {
            this.StandardPrice = ReadPrice(pricesFileName, JobTypes.Standout);
        }
    }

    public class PremiumJobStrategy : Strategy
    {
        public PremiumJobStrategy(string pricesFileName = "JobPrices.json")
        {
            this.StandardPrice = ReadPrice(pricesFileName, JobTypes.Premium);
        }
    }


    public  class AppleStandoutStrategy : StandOutJobStrategy
    {
        public AppleStandoutStrategy(string pricesFileName = "AppleJobPrices.json")
        {
            this.DiscountedPrice = ReadPrice(pricesFileName, JobTypes.Standout);
        }
        public override double GetPrice(int numJobs)
        {
            if (DiscountedPrice == 0.0)
            {
                Console.WriteLine("Error: no setup for DiscountedPrice price");
                return numJobs * StandardPrice;
            }
            return numJobs * DiscountedPrice;
        }       
    }

    public class NikePremiumStrategy : PremiumJobStrategy
    {
        private readonly int DISCOUNT_PREMIUM_JOB_NUMBER = 4;

        public NikePremiumStrategy(string pricesFileName = "NikeJobPrices.json")
        {
            this.DiscountedPrice = ReadPrice(pricesFileName, JobTypes.Premium);
        }

        public override double GetPrice(int numJobs)
        {
            if (DiscountedPrice == 0.0 || numJobs < DISCOUNT_PREMIUM_JOB_NUMBER)
            {
                if (numJobs >= DISCOUNT_PREMIUM_JOB_NUMBER)
                {
                    Console.WriteLine("Error: no setup for DiscountedPrice price");
                }
                return numJobs * StandardPrice;
            }
            return numJobs * DiscountedPrice;
        }
    }

    public class FordClassicStrategy : ClassicJobStrategy
    {

        public override double GetPrice(int numJobs)
        {
            var i = numJobs / 5;
            var rest = numJobs % 5;
            return (i * 4 + rest) * StandardPrice;
        }
    }
    public class FordStandOutStrategy : StandOutJobStrategy
    {
        public FordStandOutStrategy(string pricesFileName = "FordJobPrices.json")
        {
            this.DiscountedPrice = ReadPrice(pricesFileName, JobTypes.Standout);
        }
        public override double GetPrice(int numJobs)
        {
            if (DiscountedPrice == 0.0)
            {
                Console.WriteLine("Error: no setup for DiscountedPrice price");
                return numJobs * StandardPrice;
            }
            return numJobs * DiscountedPrice;
        }
    }
    public class FordPremiumStrategy : PremiumJobStrategy
    {
        private readonly int DISCOUNT_PREMIUM_JOB_NUMBER = 3;

        public FordPremiumStrategy(string pricesFileName = "FordJobPrices.json")
        {
            this.DiscountedPrice = ReadPrice(pricesFileName, JobTypes.Premium);
        }

        public override double GetPrice(int numJobs)
        {
            if (DiscountedPrice == 0.0 || numJobs < DISCOUNT_PREMIUM_JOB_NUMBER)
            {
                if (numJobs >= DISCOUNT_PREMIUM_JOB_NUMBER)
                {
                    Console.WriteLine("Error: no setup for DiscountedPrice price");
                }
                return numJobs * StandardPrice;
            }
            return numJobs * DiscountedPrice;
        }
    }

    public class UnilevierClassicStrategy : ClassicJobStrategy
    {

        public override double GetPrice(int numJobs)
        {
            var i = numJobs / 3;
            var rest = numJobs % 3;
            return (i * 2 + rest) * StandardPrice;
        }  
    } 
}
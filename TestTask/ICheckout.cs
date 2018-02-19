using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TestTask
{
    interface ICheckout
    {
        bool Add(Job jobItem);
        bool Remove(Job jobItem);
        double Total();

    }

    public class Triple<T, X, Y>
    {
        public T ClassicNum { get; set; }
        public X StandOutNum { get; set; }
        public Y PremiumNum  { get; set; }
    }

    public class Checkout: ICheckout
    {
        private IDeal deal;
        private Triple<int, int, int> shoppingCart = new Triple<int, int, int>();
        public Checkout(IDeal deal)
        {
            this.deal = deal;
        }

        /// <summary>
        ///Add the job item into the cart
        /// </summary>
        /// <param name="jobItem"></param>
        /// <returns>true if succesfull, false otherwise</returns>
        public bool Add(Job jobItem)
        {
            switch (jobItem.JobType)
            {
               case JobTypes.Classic:
                   shoppingCart.ClassicNum++;
                   break;
                case JobTypes.Standout:
                    shoppingCart.StandOutNum++;
                    break;
                case JobTypes.Premium:
                    shoppingCart.PremiumNum++;
                    break;
                default:
                    Console.WriteLine($"Error: Unsupported Job type: {jobItem.JobType}");
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Remove the job item, this one is nice to have, not required
        /// </summary>
        /// <param name="jobItem"></param>
        /// <returns></returns>
        public bool Remove(Job jobItem)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Calculate the total expences of the cart
        /// </summary>
        /// <returns>
        /// the total expense or -1 if error occurs
        /// </returns>
        public double Total()
        {
            var classicTotal = deal.ClassicDeal(shoppingCart.ClassicNum);
            var standoutTotal = deal.StandOutDeal(shoppingCart.StandOutNum);
            var premiumTotal = deal.PremiumDeal(shoppingCart.PremiumNum);

            return classicTotal + standoutTotal + premiumTotal;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using TestTask;

namespace TestTaskUnitTests
{
  
    public class CheckoutTests 
    {      

        [Fact]
        public void ClassicCheckout()
        {
            var co = new Checkout(new CompanyDeal());
            var classicJob = new Job(JobTypes.Classic);

            var result = co.Add(classicJob);
            result.Should().Be(true);
            var total = co.Total();

            total.Should().Be(269.99);
        }
        [Fact]
        public void Uniliever2ClassicCheckout()
        {           
            var co = new Checkout(new UnilevierDeal());
            var classicJob = new Job(JobTypes.Classic);

            co.Add(classicJob);
            co.Add(classicJob);

            var total = co.Total();

            total.Should().Be(269.99 * 2);
        }
        [Fact]
        public void Uniliever3ClassicCheckout()
        {
            var co = new Checkout(new UnilevierDeal());

            var classicJob = new Job(JobTypes.Classic);

            co.Add(classicJob);
            co.Add(classicJob);
            co.Add(classicJob);
            var total = co.Total();

            total.Should().Be(269.99 * 2);
        }
        [Fact]
        public void Uniliever4ClassicCheckout()
        {
            var co = new Checkout(new UnilevierDeal());

            var classicJob = new Job(JobTypes.Classic);

            co.Add(classicJob);
            co.Add(classicJob);
            co.Add(classicJob);
            co.Add(classicJob);
            var total = co.Total();

            total.Should().Be(269.99 * 3);
        }

        [Fact]
        public void StandoutCheckout()
        {         
            var co = new Checkout(new CompanyDeal());
            var standoutJob = new Job(JobTypes.Standout);

            var result = co.Add(standoutJob);
            result.Should().Be(true);
            var total = co.Total();

            total.Should().Be(322.99);
        }
        [Fact]
        public void PremiumCheckout()
        {
            var co = new Checkout(new CompanyDeal());

            var premiumJob = new Job(JobTypes.Premium);

            var result = co.Add(premiumJob);
            result.Should().Be(true);
            var total = co.Total();

            total.Should().Be(394.99);
        }

        [Fact]
        public void AppleCheckout()
        {
           
            var co = new Checkout(new AppleDeal("AppleJobPrices.json"));

            co.Add(new Job(JobTypes.Classic));
            co.Add(new Job(JobTypes.Standout));
            var total = co.Total();

            total.Should().Be(269.99  + 299.99);
        }
        [Fact]
        public void Nike3PremiumCheckout()
        {        
            var co = new Checkout(new NikeDeal("NikeJobPrices.json"));

            co.Add(new Job(JobTypes.Premium));
            co.Add(new Job(JobTypes.Premium));
            co.Add(new Job(JobTypes.Premium));
            var total = co.Total();

            total.Should().Be(394.99* 3);
        }

        [Fact]
        public void Nike5PremiumCheckout()
        {
            var co = new Checkout(new NikeDeal("NikeJobPrices.json"));

            co.Add(new Job(JobTypes.Premium));
            co.Add(new Job(JobTypes.Premium));
            co.Add(new Job(JobTypes.Premium));
            co.Add(new Job(JobTypes.Premium));
            co.Add(new Job(JobTypes.Premium));

            var total = co.Total();

            total.Should().Be(379.99 * 5);
        }

        [Fact]
        public void Ford3PremiumCheckout()
        {
            var co = new Checkout(new FordDeal("FordJobPrices.json"));


            co.Add(new Job(JobTypes.Classic));
            co.Add(new Job(JobTypes.Classic));
            co.Add(new Job(JobTypes.Classic));
            co.Add(new Job(JobTypes.Classic));
            co.Add(new Job(JobTypes.Classic));

            co.Add(new Job(JobTypes.Standout));

            co.Add(new Job(JobTypes.Premium));
            co.Add(new Job(JobTypes.Premium));
            co.Add(new Job(JobTypes.Premium));

            var total = co.Total();

            total.Should().Be(269.99 *4 + 309.99 + 389.99 * 3);
        }
    }
}

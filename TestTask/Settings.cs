using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TestTask
{
    public class PriviligedCustomers
    {
        public List<string> Names { get; set; }
    }

    public class JobPrices
    {
        public double Classic { get; set; }
        public double Standout { get; set; }
        public double Premium { get; set; }
    }
    public class Settings
    {
        public JobPrices JobPrices { get; set; }
        public PriviligedCustomers PriviligedCustomers { get; set; }
    }
}

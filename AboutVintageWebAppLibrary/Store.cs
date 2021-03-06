﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AboutVintageWebAppLibrary;

namespace AboutVintageBlazorWebApp.Data
{
    public class Store
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string StoreName { get; set; }
        public float RevenueCash { get; set; }
        public float RevenueMobilePay { get; set; }
        public float RevenueCreditCard { get; set; }
        public int CustomerCount { get; set; }
    }
}

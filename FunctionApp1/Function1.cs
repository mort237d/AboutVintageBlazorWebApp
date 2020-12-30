using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfDataAccessLibrary.DataAccess;
using EfDataAccessLibrary.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopifySharp;
using ShopifySharp.Filters;
using ShopifySharp.Lists;
using TradeGeckoLibrary;
using Order = ShopifySharp.Order;
using Variant = EfDataAccessLibrary.Models.Variant;

namespace FunctionApp1
{
    public static class Function1
    {
        private static readonly string shopifyUrlDk = "https://aboutvintage-dk.myshopify.com";
        private static readonly string accessTokenDk = "";
        private static readonly string shopifyUrlNyg = "https://av-cph-airport.myshopify.com";
        private static readonly string accessTokenNyg = "";
        private static readonly string shopifyUrlEu = "https://aboutvintage-eu.myshopify.com";
        private static readonly string accessTokenEu = "";
        private static readonly string shopifyUrlKr = "https://aboutvintage-kr.myshopify.com";
        private static readonly string accessTokenKr = "";
        private static readonly string shopifyUrlWw = "https://aboutvintage.myshopify.com";
        private static readonly string accessTokenWw = "";
        private static readonly string shopifyUrlJp = "https://aboutvintage-jp.myshopify.com";
        private static readonly string accessTokenJp = "";

        private const string AccessTokenTG = "Bearer ";

        private static DateTime _minDate;
        private static List<Order> allOrders;

        [FunctionName("Function1")]
        public static async Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            OrderComplaintContext db = new OrderComplaintContext(new DbContextOptionsBuilder<OrderComplaintContext>().UseSqlServer(SqlConnection).Options);

            _minDate = DateTime.Now.AddYears(-5);

            allOrders = new List<Order>();

            Task.WaitAll(
                GetShopifyOrders(shopifyUrlDk, accessTokenDk),
                GetShopifyOrders(shopifyUrlNyg, accessTokenNyg),
                GetShopifyOrders(shopifyUrlEu, accessTokenEu),
                GetShopifyOrders(shopifyUrlKr, accessTokenKr),
                GetShopifyOrders(shopifyUrlWw, accessTokenWw),
                GetShopifyOrders(shopifyUrlJp, accessTokenJp));

            List<TradeGeckoLibrary.Variant> variants = TradeGecko.GetAllTradeGeckoData<TradeGeckoLibrary.Variant>(AccessTokenTG, "https://api.tradegecko.com/variants", "?limit=250");
            List<Composition> compositions = TradeGecko.GetAllTradeGeckoData<Composition>(AccessTokenTG, "https://api.tradegecko.com/variants/composition", $"?limit=250");

            List<OrderComplaint> tempOrderComplaints = new List<OrderComplaint>();

            foreach (Order order in allOrders)
            {
                OrderComplaint tempOrderComplaint = new OrderComplaint();
                tempOrderComplaint.OrderNumber = order.Name;
                tempOrderComplaint.CreatedDate = order.CreatedAt.Value.DateTime;

                foreach (LineItem lineItem in order.LineItems)
                {
                    TradeGeckoLibrary.Variant variant = variants.SingleOrDefault(v => !string.IsNullOrEmpty(v.Sku) && v.Sku.Equals(lineItem.SKU));

                    if (variant != null)
                    {
                        if (variant.Composite)
                        {
                            var lineItemCompositions = compositions.FindAll(c => c.Bundle_sku.Equals(lineItem.SKU));

                            foreach (Composition composition in lineItemCompositions)
                            {
                                TradeGeckoLibrary.Variant variantFromBundle = variants.SingleOrDefault(v => !string.IsNullOrEmpty(v.Sku) && v.Sku.Equals(composition.Component_sku));

                                if (!variantFromBundle.Hs_code.Equals("4420101900"))
                                {
                                    var tempVariant = new Variant
                                    {
                                        Title = variantFromBundle.Product_name,
                                        SKU = variantFromBundle.Sku
                                    };

                                    if (variantFromBundle.Product_type.ToLower().Equals("strap"))
                                    {
                                        if (!string.IsNullOrEmpty(variantFromBundle.Opt1)) tempVariant.Title += " " + variantFromBundle.Opt1;
                                        if (!string.IsNullOrEmpty(variantFromBundle.Opt2)) tempVariant.Title += " " + variantFromBundle.Opt2;
                                        if (!string.IsNullOrEmpty(variantFromBundle.Opt3)) tempVariant.Title += " " + variantFromBundle.Opt3;
                                    }

                                    tempOrderComplaint.Variants.Add(tempVariant);
                                }
                            }
                        }
                        else
                        {
                            tempOrderComplaint.Variants.Add(new Variant
                            {
                                Title = lineItem.Name,
                                SKU = lineItem.SKU
                            });
                        }
                    }
                }

                if (order.Customer != null)
                    if (order.Customer.DefaultAddress != null)
                        tempOrderComplaint.Customerer = new EfDataAccessLibrary.Models.Customer
                        {
                            Address = new EfDataAccessLibrary.Models.Address
                            {
                                StreetAddress = order.Customer.DefaultAddress.Address1,
                                City = order.Customer.DefaultAddress.City,
                                Country = order.Customer.DefaultAddress.Country,
                                ZipCode = order.Customer.DefaultAddress.Zip
                            },
                            Email = order.Customer.Email,
                            FirstName = order.Customer.FirstName,
                            LastName = order.Customer.LastName,
                            PhoneNumber = order.Customer.DefaultAddress.Phone
                        };

                tempOrderComplaints.Add(tempOrderComplaint);
            }

            await Task.Run(() => db.AddRange(tempOrderComplaints));
            await Task.Run(() => db.SaveChanges());

            var OrderComplaints = await Task.Run(() => db.OrderComplaints
                .Include(c => c.Customerer)
                .Include(v => v.Variants)
                .Include(a => a.Customerer.Address)
                .ToList());
        }

        private static async Task GetShopifyOrders(string url, string token)
        {
            Console.WriteLine($"{url} started");

            var allOrdersForCurrentShop = new List<Order>();
            var service = new OrderService(url, token);
            ListResult<Order> page = await service.ListAsync(new OrderListFilter
            {
                Limit = 250,
                UpdatedAtMin = _minDate,
                Status = "any",

            });

            while (true)
            {
                allOrdersForCurrentShop.AddRange(page.Items);

                Console.WriteLine($"{url} --> Amount of orders: {allOrdersForCurrentShop.Count}");

                if (!page.HasNextPage)
                {
                    break;
                }

                page = await service.ListAsync(page.GetNextPageFilter());
            }

            Console.WriteLine($"{url} ended");

            allOrders.AddRange(allOrdersForCurrentShop);
        }
    }
}

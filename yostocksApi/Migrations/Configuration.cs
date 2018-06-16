namespace yostocksApi.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using yostocksApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<yostocksApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(yostocksApi.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            /*
             
            var stockList = new List<Stock>();
            DateTime CurrentDateTime = Convert.ToDateTime(DateTime.Now);
            string CurrentDate = CurrentDateTime.ToLongDateString();
            string CurrentTime = CurrentDateTime.ToLongTimeString();


            Stock amazon = new Stock()
            {
                Brand = "Amazon",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/amazon.png"
            };
            stockList.Add(amazon);

            Stock apple = new Stock()
            {
                Brand = "Apple",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 12123.51,
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/apple.png"
            };
            stockList.Add(apple);

            Stock audi = new Stock()
            {
                Brand = "Audi",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/audi.png"
            };
            stockList.Add(audi);

            Stock bing = new Stock()
            {
                Brand = "Bing",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/bing.png"
            };
            stockList.Add(bing);

            Stock bmw = new Stock()
            {
                Brand = "BMW",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/bmw.png"
            };
            stockList.Add(bmw);

            Stock cocacola = new Stock()
            {
                Brand = "Coca Cola",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/cocacola.png"
            };
            stockList.Add(cocacola);

            Stock disney = new Stock()
            {
                Brand = "Disney",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/disney.png"
            };
            stockList.Add(disney);
            Stock easports = new Stock()
            {
                Brand = "Ea Sports",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/easports.png"
            };
            stockList.Add(easports);

            Stock facebook = new Stock()
            {
                Brand = "Facebook",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
               
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/facebook.png"
            };
            stockList.Add(facebook);

            Stock ford = new Stock()
            {
                Brand = "Ford",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/ford.png"
            };
            stockList.Add(ford);

            Stock google = new Stock()
            {
                Brand = "Google",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/google.png"
            };
            stockList.Add(google);

            Stock hm = new Stock()
            {
                Brand = "H & M",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
               
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/hm.png"
            };
            stockList.Add(hm);

            Stock ibm = new Stock()
            {
                Brand = "IBM",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/ibm.png"
            };
            stockList.Add(ibm);

            Stock lockheed = new Stock()
            {
                Brand = "Lockheed",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
               
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/lockheed.png"
            };
            stockList.Add(lockheed);

            Stock mcdonalds = new Stock()
            {
                Brand = "MC Donalds",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
               
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/mcdonalds.png"
            };
            stockList.Add(mcdonalds);

            Stock microsoft = new Stock()
            {
                Brand = "Microsoft",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
               
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/microsoft.png"
            };
            stockList.Add(microsoft);

            Stock netflix = new Stock()
            {
                Brand = "Netflix",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
               
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/netflix.png"
            };
            stockList.Add(netflix);

            Stock nike = new Stock()
            {
                Brand = "Nike",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/nike.png"
            };
            stockList.Add(nike);

            Stock nintendo = new Stock()
            {
                Brand = "IBM",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/nintendo.png"
            };
            stockList.Add(nintendo);

            Stock northropgrummanm = new Stock()
            {
                Brand = "Northrop Grummanm",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/northropgrummanm.png"
            };
            stockList.Add(northropgrummanm);

            Stock pepsi = new Stock()
            {
                Brand = "Samsung",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/samsung.png"
            };
            stockList.Add(pepsi);

            Stock starbuckscoffe = new Stock()
            {
                Brand = "Starbucks Coffe",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/starbuckscoffe.png"
            };
            stockList.Add(starbuckscoffe);

            Stock tesla = new Stock()
            {
                Brand = "Tesla",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/tesla.png"
            };
            stockList.Add(tesla);

            Stock victoriassecret = new Stock()
            {
                Brand = "Victoria's Secret",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/victoriassecret.png"
            };
            stockList.Add(victoriassecret);

            Stock visa = new Stock()
            {
                Brand = "Visa",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/visa.png"
            };
            stockList.Add(visa);

            Stock walmart = new Stock()
            {
                Brand = "Walmart",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/walmart.png"
            };
            stockList.Add(walmart);


            Stock zara = new Stock()
            {
                Brand = "Zara",
                RemainingPercentage = 100.0,
                PriceWhenPurchased = 6003.21,
                DateGenerated = CurrentDate,
                TimeGenerated = CurrentTime,
                LogoImagePath = "~/Content/Brands-Logo/zara.png"
            };
            stockList.Add(zara);


            //Adding all Stocks to the DB table
            
            foreach (var item in stockList)
            {
                context.Stocks.Add(item);
            }
            context.SaveChanges();

            */
        }
    }
}

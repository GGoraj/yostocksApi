using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


    namespace yostocksApi.Models
    {
        public class BuyStockFragmentBindingModel
        {
            [Required]
            public int YostocksUserId { get; set; }
            [Required]
            public string Brand { get; set; }
            [Required]
            public string Currency { get; set; }
            [Required]
            public double CurrentStockPrice { get; set; } //are we selling the stock by the price visible to custommer - or to server - or to the broker
            [Required]

            public double RequiredAmount { get; set; }

            [Required]
            public DateTime Date { get; set; }
            [Required]
            public DateTime Time { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string Country { get; set; }


        }

        public class SellStockFragmentBindingModel
        {
            [Required]
            public string UserId { get; set; }
            [Required]
            public string Brand { get; set; }
            [Required]
            public string CurrentStockPrice { get; set; }
            [Required]
            public string SellingPercentAmount { get; set; }

        }
    
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace yostocksApi.Models
{
    public class Stock
    {
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public double RemainingPercentage { get; set; }
        [Required]
        public double PriceWhenPurchased { get; set; }
        //[Required]
        //public string CurrencyOfPurchase { get; set; }
        [Required]
        public string DateGenerated { get; set; }
        [Required]
        public string TimeGenerated { get; set; }
        [Required]
        public string LogoImagePath { get; set; }

        //[NotMapped]
        //public string StockFragmentId { get; set; }

        //[NotMapped]
        //public byte[] ImageArray { get; set; }

        //navigation properties

        //one Stock can have many fragments that belong to different users
        public virtual ICollection<Fragment> Fragments { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace yostocksApi.Models
{
    public class FragmentResponseModel
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public double PercentValue { get; set; } 
    }
}
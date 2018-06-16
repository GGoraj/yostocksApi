using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using yostocksApi.Models;


namespace yostocksApi.Controllers
{
    public class StockTransactionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Stocks
        [HttpGet]
        public IQueryable<Stock> GetStocks()
        {
            // mobile app requests all stacks
            // to display images, stock names and description
            //in ListView, so the user can choose stock to invest in
            return db.Stocks;
        }


        //POST
        [Route("api/StockTransactions/BuyStockFragment")]
        public IHttpActionResult BuyStockFragment([FromBody] BuyStockFragmentBindingModel buyFragmentModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // calculate Percent Value of user request
            double requestedPercentage =(buyFragmentModel.RequiredAmount / buyFragmentModel.CurrentStockPrice) * 100;
            string stringRequestedPercentage = String.Format("{0:0.##}", requestedPercentage);
            requestedPercentage = Convert.ToDouble(stringRequestedPercentage);
            
            String time = DateTime.Now.ToLongTimeString();
            String date = DateTime.Now.ToLongDateString();

            
            

        while (true) {

            Stock stock = db.Stocks
                    .Where(b => b.Brand == buyFragmentModel.Brand)
                    .Where(a => a.RemainingPercentage > 0)
                    .FirstOrDefault();

            //checking if stock wasn't found
            if (stock.Equals(null))
            {
                return Ok("Error - No such stock available");
            }

            double remainingStockPercent = stock.RemainingPercentage;

            //if stock.RemainingPercentage is enough > requested
            if (remainingStockPercent > requestedPercentage)
            {
                
                Fragment fragment = new Fragment
                {
                    YostocksUserId = buyFragmentModel.YostocksUserId,
                    StockId = stock.Id,
                    PercentValue = requestedPercentage
                };

                db.Fragments.Add(fragment);
                    
                //trim remaining percentage before adding to Stock column
                double remainingStockPercentage = remainingStockPercent - requestedPercentage;
                string trimmedStockPercentage = String.Format("{0:0.##}", remainingStockPercentage);
                stock.RemainingPercentage = Convert.ToDouble(trimmedStockPercentage);
                   

                db.SaveChanges(); //should it be async?? issue: how to manage multiple buy requests of the same stock
                return StatusCode(HttpStatusCode.Accepted);
                
            }
            
            //if stock.RemainingPercentage is just enough == requested
            //then create new stock of required brand - to fill the vacancy after the fragmented stock.
            if(remainingStockPercent == requestedPercentage)
            {
                Fragment fragment = new Fragment
                {
                    YostocksUserId = buyFragmentModel.YostocksUserId,
                    StockId = stock.Id,
                    PercentValue = requestedPercentage
                };
                db.Fragments.Add(fragment);

                //create new stock of the same brand
                Stock newStock = new Stock()
                {
                    Brand = buyFragmentModel.Brand,
                    RemainingPercentage = 100,
                    PriceWhenPurchased = 6300.3434,
                    DateGenerated = date,
                    TimeGenerated = time,
                    LogoImagePath = stock.LogoImagePath

                };
                db.Stocks.Add(newStock);
                db.SaveChanges();
                return StatusCode(HttpStatusCode.Created);
            }
            //if stock.RemainingPercentage is NOT ENOUGH
            if(remainingStockPercent < requestedPercentage)
            {
                
                Fragment fragment = new Fragment
                {
                    YostocksUserId = buyFragmentModel.YostocksUserId,
                    StockId = stock.Id,
                    PercentValue = remainingStockPercent
                };
                db.Fragments.Add(fragment);

                //set remaining stock %
                stock.RemainingPercentage = 0;

                //calculate new required percent value
                requestedPercentage = Math.Abs(remainingStockPercent - requestedPercentage);

                //trim this value to 2 places from coma
                string trimmedStockPercentage = String.Format("{0:0.##}", requestedPercentage);

                //double value of new request
                requestedPercentage = Convert.ToDouble(trimmedStockPercentage);
               

                //create new stock of the same brand
                Stock newStock = new Stock()
                {
                    Brand = buyFragmentModel.Brand,
                    RemainingPercentage = 100,
                    PriceWhenPurchased = 6300.3434,
                    DateGenerated = date,
                    TimeGenerated = time,
                    LogoImagePath = stock.LogoImagePath

                };
                db.Stocks.Add(newStock);
                db.SaveChanges();

                //if its true, request can return
                if(requestedPercentage == 0)
                {
                    StatusCode(HttpStatusCode.Created);
                }
                //While(true) - stil true
            }
        }   
          
     }

        
            
        
        // GET: api/Stocks/5
        [ResponseType(typeof(Stock))]
        public async Task<IHttpActionResult> GetStock(int id)
        {
            Stock stock = await db.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }
        /*
       // PUT: api/Stocks/5
       [ResponseType(typeof(void))]
       public async Task<IHttpActionResult> PutStock(int id, StockModel stock)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }

           if (id != stock.Id)
           {
               return BadRequest();
           }

           db.Entry(stock).State = EntityState.Modified;

           try
           {
               await db.SaveChangesAsync();
           }
           catch (DbUpdateConcurrencyException)
           {
               if (!StockExists(id))
               {
                   return NotFound();
               }
               else
               {
                   throw;
               }
           }

           return StatusCode(HttpStatusCode.NoContent);
       }

       // POST: api/Stocks
       [ResponseType(typeof(StockModel))]
       public async Task<IHttpActionResult> PostStock(StockModel stock)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }

           db.Stocks.Add(stock);
           await db.SaveChangesAsync();

           return CreatedAtRoute("DefaultApi", new { id = stock.Id }, stock);
       }

       // DELETE: api/Stocks/5
       [ResponseType(typeof(StockModel))]
       public async Task<IHttpActionResult> DeleteStock(int id)
       {
           StockModel stock = await db.Stocks.FindAsync(id);
           if (stock == null)
           {
               return NotFound();
           }

           db.Stocks.Remove(stock);
           await db.SaveChangesAsync();

           return Ok(stock);
       }

       protected override void Dispose(bool disposing)
       {
           if (disposing)
           {
               db.Dispose();
           }
           base.Dispose(disposing);
       }

       private bool StockExists(int id)
       {
           return db.Stocks.Count(e => e.Id == id) > 0;
       }
       */

    
    }
}
/*
{"YostocksUserId":"1", "Brand":"Amazon", "Currency":"DKK", "CurrentStockPrice":624.56, "RequiredAmount":0,
"Date":"11-06-2018", "Time":"22:54:00", "City":"Copenhagen", "Country":"Denmark"}

    */

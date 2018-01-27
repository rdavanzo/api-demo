using System;
using System.Linq;
using ApiDemo.Models;
using ApiDemo.Persistence;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ApiDemo.Controllers
{
    [Route("api/costing")]
    public class CostingController : Controller 
    {
        private readonly DefaultDbContext _dbContext;
        public CostingController(DefaultDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        public void GetCost()
        {
            System.Console.WriteLine("Getting cost...");

            var movementItemAggregates = 
                from a in _dbContext.MovementItemAggregates
                orderby a.Date
                select a;
            
            foreach (var aggregate in movementItemAggregates)
            {
                var invoiceItems = 
                    from i in _dbContext.Invoices.Include(i => i.InvoiceItems)
                    from ii in i.InvoiceItems
                    where ii.Upc == aggregate.Upc
                    orderby i.Date
                    select ii;

                var quantity = aggregate.Quantity;
                var aggregateCost = 0m;
                foreach (var item in invoiceItems)
                {
                    var lineItemQuantity = item.Quantity;
                    var lineItemCost = item.Cost;

                    if (lineItemQuantity == quantity)
                    {
                        aggregateCost += lineItemCost;
                        break;
                    }

                    var unitCost = lineItemCost / lineItemQuantity;
                    if (lineItemQuantity > quantity)
                    {
                        aggregateCost += unitCost * quantity;
                        break;
                    }
                    if (lineItemQuantity < quantity)
                    {
                        aggregateCost += unitCost * quantity;
                        quantity -= lineItemQuantity;
                    }
                }

                aggregate.Cost = aggregateCost;
            }

            _dbContext.SaveChanges();
        }
    }
}

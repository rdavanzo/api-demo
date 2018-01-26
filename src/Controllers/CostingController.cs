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

            var movementItemAggregates = _dbContext
                .MovementItemAggregates
                .OrderBy(m => m.Date);

            foreach (var aggregate in movementItemAggregates)
            {
                var invoiceItems = _dbContext.Invoices
                    .Include(i => i.InvoiceItems)
                    .OrderBy(i => i.Date)
                    .SelectMany(i => i.InvoiceItems)
                    .Where(i => i.Upc == aggregate.Upc);

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

using ApiDemo.Models;
using System.Linq;
using System.Collections.Generic;
using System;
using Bogus;

namespace ApiDemo.Persistence
{

    public static class DbInitializer
    {
        public static void Initialize(DefaultDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            CreateFakeData(context);

            context.SaveChanges();
        }

        private static void CreateFakeData(DefaultDbContext context)
        {
            var invoiceFactory = new Faker<Invoice>();
            invoiceFactory.RuleFor(i => i.Date, f => f.Date.Past());
            var invoices = new List<Invoice>();
            for (var i = 0; i < 5; i++)
                invoices.Add(invoiceFactory.Generate());

            var faker = new Faker();
            var upcs = new List<string>();
            for (var i = 0; i < 10; i++)
                upcs.Add(faker.Random.ReplaceNumbers("#############"));

            var invoiceItemFactory = new Faker<InvoiceItem>();
            invoiceItemFactory.RuleFor(i => i.Upc, f => f.Random.ArrayElement(upcs.ToArray()))
                .RuleFor(i => i.Quantity, f => f.Random.Number(min: 1, max: 20))
                .RuleFor(i => i.Cost, f => Math.Round(f.Random.Decimal(max: 100), 2))
                .RuleFor(i => i.Invoice, f => f.Random.ArrayElement(invoices.ToArray()));

            for (var i = 0; i < 100; i++)
            {
                var item = invoiceItemFactory.Generate();
                item.Invoice.InvoiceItems.Add(item);
            }

            context.Invoices.AddRange(invoices);

            var movementItemAggregateFactory = new Faker<MovementItemAggregate>();
            movementItemAggregateFactory.RuleFor(i => i.Date, DateTime.Today)
                .RuleFor(i => i.Quantity, f => f.Random.Number(min: 1, max: 20))
                .RuleFor(i => i.Retail, f => Math.Round(f.Random.Decimal(min: 1, max: 200), 2))
                .RuleFor(i => i.Upc, f => f.Random.ArrayElement(invoices.SelectMany(inv => inv.InvoiceItems).Select(itm => itm.Upc).ToArray()));

            for (var i = 0; i < 10; i++)
                context.MovementItemAggregates.Add(movementItemAggregateFactory.Generate());
        }
    }
}
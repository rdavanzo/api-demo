using System;
using JsonApiDotNetCore.Models;

namespace ApiDemo.Models
{
    public class MovementItemAggregate : Identifiable
    {
        public DateTime Date { get; set; }
        public string Upc { get; set; }
        public int Quantity { get; set; }
        public decimal Retail { get; set; }
        public decimal Cost { get; set; }
    }
}
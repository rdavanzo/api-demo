using System;

namespace ApiDemo.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public Invoice Invoice { get; set; }
        public string Upc { get; set; }
        public int Quantity { get; set; }
        public int QuantitySold { get; set; }
        public decimal Cost { get; set; }
    }
}
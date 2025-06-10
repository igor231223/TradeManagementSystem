using System;

namespace TradeManagementSystem.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Supplier { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int ShelfLife { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}

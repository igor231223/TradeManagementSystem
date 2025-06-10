using System;
using System.Collections.Generic;

namespace TradeManagementSystem.Models
{
    public class Order
    {
        public string Client { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}

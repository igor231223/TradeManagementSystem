using System;
using System.Collections.Generic;

namespace TradeManagementSystem.Models
{
    public class Invoice
    {
        public string Supplier { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
    }
}

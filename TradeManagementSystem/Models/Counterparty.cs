using System;

namespace TradeManagementSystem.Models
{
    public class Counterparty
    {
        public string TypeView { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string DirectorName { get; set; }
        public string LegalAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string TaxNumber { get; set; }
    }
}

using System;

namespace NorthwindApi.Entities
{
    public class Invoice
    {
        private decimal totalPrice;

        public Invoice(string customerName, decimal extendedPrice, decimal freightPrice)
        {
            totalPrice = extendedPrice + freightPrice;
            CustomerName = customerName;
        }

        public string CustomerName { get; set; }
        public decimal TotalPrice => totalPrice;
    }
}

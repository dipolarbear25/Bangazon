using System;
namespace Bangazon.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int SellerId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public int QuantityInStock { get; set; }
        public string Description { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime TimePosted { get; set; }
    }
}
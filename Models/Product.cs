namespace Bangazon.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int sellerId { get; set; }
        public string productName { get; set; }
        public string category { get; set; }
        public string quantity { get; set; }
        public string description { get; set; }
        public decimal pricePerUnit { get; set; }
        public DateTime timePosted { get; set; }
    }
}
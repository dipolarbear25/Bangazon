using System;
namespace Bangazon.Models
{
	public class Order
	{
		public int ID { get; set; }
		public int UserId { get; set; }
		public bool OrderIsOpen { get; set; }
		public DateTime OrderDate { get; set; }
		public int PaymentType { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
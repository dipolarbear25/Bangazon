using System;
namespace Bangazon.Models
{
	public class Orders
	{
		public int ID { get; set; }
		public string? userId { get; set; }
		public bool orderStatus { get; set; }
		public DateTime orderDate { get; set; }
		public string paymentTypeId { get; set; }
	}
}
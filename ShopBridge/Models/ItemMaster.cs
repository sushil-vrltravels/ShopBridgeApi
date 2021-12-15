using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Models
{
    public class ItemMaster
    {
		[Key]
		public int ItemId { get; set; }

		[Required(ErrorMessage = "Item Name Required")]
		[MinLength(3, ErrorMessage = "Item Should be Atleast 3 Characters")]
		[MaxLength(50, ErrorMessage = "Item Should not be more than 50 Characters")]
		public string ItemName { get; set; }

		[Required]
		[MinLength(3, ErrorMessage = "Description Should be Atleast 3 Characters")]
		public string ItemDescription { get; set; }

		[Required]
		[Range(1, Double.MaxValue, ErrorMessage = "Item Price Should be More than Rs.1")]
		public decimal ItemPrice { get; set; }
		 
	}
}

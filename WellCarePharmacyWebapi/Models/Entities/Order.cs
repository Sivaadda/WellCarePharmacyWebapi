using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellCarePharmacyWebapi.Models.Entities.Base;

namespace WellCarePharmacyWebapi.Models.Entities
{
    public class Order:BaseEntity
    {
       
        [Required]
        public int TotalQuantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public int UsersId { get; set; }
        [ForeignKey("UsersId")]
        public virtual User Users { get; set; }


        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }

    public class ProductOrder
    {
        [Key]
        public int ProductOrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}


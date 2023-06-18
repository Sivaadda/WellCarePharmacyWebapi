using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellCarePharmacyWebapi.Models.Entities;

namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class OrderRespond
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TotalQuantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public int UsersId { get; set; }
        [ForeignKey("UsersId")]
        public virtual User Users { get; set; }

        [Required]
        public IList<ProductOrderRespond> Products { get; set; }


    }

    public class ProductOrderRespond
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

    }

}

    



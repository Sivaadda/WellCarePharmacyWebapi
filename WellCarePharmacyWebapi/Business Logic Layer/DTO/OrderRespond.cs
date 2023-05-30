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
        public int Quantity { get; set; }

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

        [Required]
        public decimal Price { get; set; }

        [MaxLength(100)]
        public string Descripition { get; set; }

        public decimal Discount { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; }

    }




}

    



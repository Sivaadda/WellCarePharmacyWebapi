
using System.ComponentModel.DataAnnotations;

namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class ProductRespond
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string Descripition { get; set; }

        public decimal Discount { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class ProductRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }

        [Required]
        
        public float Price { get; set; }

        [MaxLength(100)]
        public string Descripition { get; set; }

        
        public decimal Discount { get; set; }

        [Required]
        [MaxLength(30)]
        public string Status { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}

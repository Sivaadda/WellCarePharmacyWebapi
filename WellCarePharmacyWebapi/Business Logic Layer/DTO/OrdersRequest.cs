using System.ComponentModel.DataAnnotations;
using WellCarePharmacyWebapi.Models.Entities;

namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class OrdersRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Users Users { get; set; }

        [Required]
        public virtual ICollection<Products> Products { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Range(18, 2)]
        public decimal TotalPrice { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class OrdersRespond
    {
        

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [ForeignKey("User")]
        public int UsersId { get; set; }


        [ForeignKey("Product")]
        public int ProductId { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace WellCarePharmacyWebapi.Business_Logic_Layer.DTO
{
    public class LoginRequest
    {

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
    }
}

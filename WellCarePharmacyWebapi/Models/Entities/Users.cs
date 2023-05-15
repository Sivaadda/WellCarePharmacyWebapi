using System.ComponentModel.DataAnnotations;
using WellCarePharmacyWebapi.Models.Entities.Base;

namespace WellCarePharmacyWebapi.Models.Entities
{
    public class Users:BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Password { get; set; }

        [Required]
        [MaxLength(15)]
        public int PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        [Required]
        public Role Role { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}

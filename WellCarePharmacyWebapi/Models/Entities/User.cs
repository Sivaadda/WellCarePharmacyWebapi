using System.ComponentModel.DataAnnotations;
using WellCarePharmacyWebapi.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellCarePharmacyWebapi.Models.Entities
{
    public class User:BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required ]
        [EmailAddress(ErrorMessage = "Enter emailaddress")]
        public string Email { get; set; }

        [Required ]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="The Password must be at least 8 characters long.")]
        public string Password { get; set; }

        
        [Phone(ErrorMessage = "Enter valid phone number.")]
        public string  PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; } = DateTime.Now;


        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Roles { get; set; }
               
    }
}
    
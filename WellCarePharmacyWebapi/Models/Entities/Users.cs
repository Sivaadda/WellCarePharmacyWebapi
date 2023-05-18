using System.ComponentModel.DataAnnotations;
using WellCarePharmacyWebapi.Models.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

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


        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Roles { get; set; }
       

        
    }
}
    

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hostman.Database.Model
{
    public class Authentication
    {
        [Key]
        public uint Id { get; set; }

        public uint UserId { get; set; }

        [StringLength(255)]
        public string Issuer { get; set; }

        [StringLength(255)]
        public string Subject { get; set; }

        [ForeignKey(nameof(Authentication.UserId))]
        [Required]
        public User User { get; set; }
    }
}

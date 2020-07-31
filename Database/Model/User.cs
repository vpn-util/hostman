using System.ComponentModel.DataAnnotations;

namespace Hostman.Database.Model
{
    public class User
    {
        [Key]
        public uint Id { get; set; }

        [MinLength(3)]
        [MaxLength(16)]
        [Required]
        public string Nickname { get; set; }

        public bool Enabled { get; set; }
    }
}
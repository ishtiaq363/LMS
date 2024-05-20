using LMS.Utility;
using System.ComponentModel.DataAnnotations;

namespace LMSWeb.Models
{
    public class MessageViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DBConstants.MaxEmailLength)]
        public string Email { get; set; }

        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string Subject { get; set; }

        [Required]
        [StringLength(DBConstants.MaxUrlLength)]
        public string Msg { get; set; }

    }
}

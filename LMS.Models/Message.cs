using LMS.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    [Table(nameof(Message))]
    public class Message
    {
        [Key]
        public Guid Id {get; set;}

        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        [Column(nameof(Message.Name))]
        public string Name { get; set; }

        [Required]
        [StringLength(DBConstants.MaxEmailLength)]
        [Column(nameof (Message.Email))]
        public string Email { get; set; }

        [Required]
        [Column(nameof(Message.Subject))]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string Subject { get; set; }

        [Required]
        [StringLength(DBConstants.MaxUrlLength)]
        [Column(nameof(Message.Msg))]
        public string Msg { get; set; }


    }
}

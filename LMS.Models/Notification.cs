using LMS.Utility;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    [Table(nameof(Notification))]
    public class Notification
    {
        [Key]
        [Column(nameof(Id))]
        public Guid Id { get; set; }

        [Required]
        [Column(nameof(Notification.Title))]
        [StringLength(DBConstants.MaxDescriptionLength)]
        public string Title { get; set; }

        [Required]
        [Column(nameof(Notification.Content))]
        [StringLength(DBConstants.MaxAddressLineLength)]
        public string Content { get; set; }

        [Required]
        [Column(nameof(Notification.DateCreated))]
        public DateTime DateCreated { get; set; }

        [Required]
        [Column(nameof(Notification.IsRead))]
        [DefaultValue(false)]
        public bool IsRead { get; set; }

        [DefaultValue(false)]
        [Column(nameof(Notification.IsDeleted))]
        public bool? IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Notification.BatchId))]
        [Column(nameof(Notification.BatchId))]
        public Guid BatchId { get; set; }
        public virtual Batch Batch { get; set; }

    }
}

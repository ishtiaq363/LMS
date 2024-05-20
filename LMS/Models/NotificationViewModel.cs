using LMS.Models;
using LMS.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSWeb.Models
{
    public class NotificationViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? BatchName { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public bool? IsRead { get; set; }

        public bool? IsDeleted { get; set; }

        public Guid BatchId { get; set; }
        public Batch? Batch { get; set; }

    }
}

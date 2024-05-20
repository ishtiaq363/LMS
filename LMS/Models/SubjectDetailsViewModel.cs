using LMS.Models;
using LMS.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMSWeb.Models
{
    public class SubjectDetailsViewModel
    {
       
        public Guid Id { get; set; }

        [Required]
        public Guid SubjectId { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        public string ResourceType { get; set; }

        
        [StringLength(DBConstants.MaxUrlLength)]
        public string? ResourceUrl { get; set; }
        public Subject? Subject { get;  set; }
    }
}

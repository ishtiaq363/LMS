using LMS.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSWeb.Models
{
    public class SubjectViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        public string Level { get; set; }

        [Required]
        [StringLength(DBConstants.MaxStatusLength)]
        public string Status { get; set; }

        [StringLength(DBConstants.MaxNotesLength)]
        public string? ResourceScope { get; set; }

    }
}

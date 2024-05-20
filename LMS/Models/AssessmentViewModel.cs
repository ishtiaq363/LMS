using LMS.Models;
using LMS.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSWeb.Models
{
    public class AssessmentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Type Of Assessment")]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string Name { get; set; }

    }
}

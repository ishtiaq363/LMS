using LMS.Models;
using LMS.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSWeb.Models
{
    public class BatchViewModel
    {

        public Guid Id { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [DefaultValue("")]
        public string CourseTitle {get; set;}

        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string Name { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        [StringLength(DBConstants.MaxEnumStringLength)]
        public string Status { get; set; }

    
        [StringLength(DBConstants.MaxDescriptionLength)]
        public string Description { get; set; }
               
       
    }
}

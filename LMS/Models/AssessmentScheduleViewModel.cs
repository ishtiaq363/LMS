using LMS.Models;
using LMS.Utility;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSWeb.Models
{
    public class AssessmentScheduleViewModel
    {

        public Guid Id { get; set; }
        [Required]
        public Guid BatchId { get; set; }
        public string? BatchName { get; set; }
        [Required]
        public Guid SubjectId { get; set; }

        public string? SubjectName { get; set; }
        [Required]
        [DisplayName("please provide Assessment Type")]
        public Guid AssessmentId { get; set; }
        [StringLength(DBConstants.MaxUrlLength)]
        public string? AssessmentName { get; set; }
        
        public string? AssessmentSource { get; set; }
        public DateOnly AssessmentDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }


        [StringLength(DBConstants.MaxResult)]
        public string TotalMarks { get; set; }

        [StringLength(DBConstants.MaxResult)]
        public string Passingmarks { get; set; }
        [StringLength(DBConstants.MaxEnumStringLength)]
        public string Status { get; set; }
    }
}



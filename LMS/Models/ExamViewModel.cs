using LMS.Models;
using LMS.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSWeb.Models
{
    public class ExamViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid BatchId { get; set; }
        public string? BatchName {  get; set; } 

        [Required]
        public Guid StudentId { get; set; }
        public string? StudentName { get; set; }
        [Required]
        [StringLength(DBConstants.MaxRollNoLength)]
        public String ExamType { get; set; }


        public string? AssessmentSource { get; set; }


        public DateOnly? AssessmentDate { get; set; }


        public TimeOnly? StartTime { get; set; }

        public TimeOnly? EndTime { get; set; }
        
        public string? TotalMarks { get; set; }

        public string? Passingmarks { get; set; }

        [DefaultValue(false)]
        public bool? IsDeleted { get; set; }

        [StringLength(DBConstants.MaxEnumStringLength)]
        public string? Status { get; set; }

        public string? UploadPaper { get; set; }

        public string? Result { get; set; }

        public string? ObtainedMarks { get; set; }

        public Batch? Batch { get; set; }

        public Student? Student { get; set; }
    }
}

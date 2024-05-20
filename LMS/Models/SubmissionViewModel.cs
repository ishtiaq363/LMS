using LMS.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSWeb.Models
{
    public class SubmissionViewModel
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string? FullName { get; set; }
        public string? BatchName { get; set; }
        public string? AssessmentName { get; set; }
        public Guid AssessmentScheduleId { get; set; }
        public string SubmissionUrl { get; set; }
        public float? Marks { get; set; }
        public string Status { get; set; }

        public Student? Student { get; set; }

        public AssessmentSchedule? AssessmentSchedule { get; set; }
    }
}

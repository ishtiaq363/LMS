using LMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMSWeb.Models
{
    public class CourseSubjectViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        public string? CourseName { get; set; }

        [Required]
        public Guid SubjectId { get; set; }

        public string? SubjectName { get; set; }
    }
}

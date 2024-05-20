using LMS.Models;
using LMS.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSWeb.Models
{
    public class BatchStudentViewModel
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public string? StudentName { get; set; }

        public Guid BatchId { get; set; }

        public string? BatchName { get; set; }

        [DisplayName("Enrollment No")]
        [StringLength(DBConstants.MaxStatusLength)]
        public string EnrollmentNo { get; set; }

        [Required]
        [DisplayName("Enrollment Date")]
        public DateOnly EnrollmentDate { get; set; }


        [StringLength(DBConstants.MaxStatusLength)]
        public string Status { get; set; }

        public DateOnly? CompletionDate { get; set; }

        [StringLength(DBConstants.MaxDescriptionLength)]
        public string Remarks { get; set; }
        public Student? Student { get; set; }


        public Batch? Batch { get; set; }

    }
}

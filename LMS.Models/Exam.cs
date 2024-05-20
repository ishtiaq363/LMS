using LMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMS.Models
{
    [Table(nameof(Exam))]
    public  class Exam
    {
        [Key]
        [Column(nameof(Exam.Id))]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(Exam.BatchId))]
        [Column(nameof(Exam.BatchId))]
        public Guid BatchId { get; set; }

        [ForeignKey(nameof(StudentDetail.StudentId))]
        [Column(nameof(StudentDetail.StudentId))]
        public Guid StudentId { get; set; }

        [Required]
        [Column(nameof(Exam.ExamType))]
        [StringLength(DBConstants.MaxRollNoLength)]
        public String? ExamType { get; set; }
        
       
        [Column(nameof(Exam.AssessmentSource))]
        [StringLength(DBConstants.MaxUrlLength)]
        public string? AssessmentSource { get; set; }

        
        [Column(nameof(Exam.AssessmentDate))]
        public DateOnly? AssessmentDate { get; set; }

        
        [Column(nameof(Exam.StartTime))]
        public TimeOnly? StartTime { get; set; }

        
        [Column(nameof(Exam.EndTime))]
        public TimeOnly? EndTime { get; set; }

        
        [Column(nameof(Exam.TotalMarks))]
        [StringLength(DBConstants.MaxResult)]
        public string? TotalMarks { get; set; }

        
        [StringLength(DBConstants.MaxResult)]
        [Column(nameof(Exam.Passingmarks))]
        public string? Passingmarks { get; set; }

        [DefaultValue(false)]
        [Column(nameof(Exam.IsDeleted))]
        public bool? IsDeleted { get; set; }

        [Required]
        [Column(nameof(Exam.Status))]
        [StringLength(DBConstants.MaxEnumStringLength)]
        public string? Status { get; set; }

       
        [Column(nameof(Exam.UploadPaper))]
        [StringLength(DBConstants.MaxUrlLength)]
        public string? UploadPaper { get; set; }

        [Column(nameof(Exam.Result))]
        [StringLength(DBConstants.MaxRollNoLength)]
        public string? Result { get; set; }

        [Column(nameof(Exam.ObtainedMarks))]
        [StringLength(DBConstants.MaxRollNoLength)]
        public string? ObtainedMarks { get; set; }

        [JsonIgnore]
        public Batch Batch { get; set; }

        [JsonIgnore]
        public Student Student { get; set; }

    }
}

using LMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMS.Models;

[Table(nameof(Batch))]
public  class Batch
{
    [Key]
    [Column(nameof(Batch.Id))]
    public Guid Id { get; set; }

    [Required]
    [Column(nameof(Batch.CourseId))]
    public Guid CourseId { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    [Column(nameof(Batch.Name))]
    public string Name { get; set; }

    [Required]
    [Column(nameof(Batch.StartDate))]
    public DateOnly StartDate { get; set; }

    [Required]
    [Column(nameof(Batch.EndDate))]
    public DateOnly EndDate { get; set; }

    [Required]
    [Column(nameof(Batch.Capacity))]
    public int Capacity { get; set; }

    [Required]
    [Column(nameof(Batch.Status))]
    [StringLength(DBConstants.MaxEnumStringLength)]
    public string Status { get; set; }

    [Column(nameof(Batch.Description))]
    [StringLength(DBConstants.MaxDescriptionLength)]
    public string Description { get; set; }

    [DefaultValue(false)]
    [Column(nameof(Batch.IsDeleted))]
    public bool? IsDeleted { get; set; }

    [Column(nameof(Batch.CreatedAt))]
    public DateTime? CreatedAt { get; set; }

    [Column(nameof(Batch.UpdatedAt))]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey(nameof(Batch.CourseId))]
    [InverseProperty(nameof(Course.Batches))]
    [JsonIgnore]
    public Course Course { get; set; }
    
    public virtual List<AssessmentSchedule> AssessmentSchedules { get; set; }

    public virtual List<BatchStudent> CourseStudents { get; set; }

    public virtual List<BatchFee> BatchFees { get; set; }

    public virtual List<Payment> Payments { get; set; }

    public virtual List<Exam> Exams { get; set; }
}




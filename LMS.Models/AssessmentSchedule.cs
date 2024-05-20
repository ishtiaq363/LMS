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

[Table(nameof(AssessmentSchedule))]
public class AssessmentSchedule
{
    [Key]
    [Column(nameof(AssessmentSchedule.Id))]
    public Guid Id { get; set; }

    [Required]
    [ForeignKey(nameof(AssessmentSchedule.BatchId))]
    [Column(nameof(AssessmentSchedule.BatchId))]
    public Guid BatchId { get; set; }

    [Required]
    [ForeignKey(nameof(AssessmentSchedule.SubjectId))]
    [Column(nameof(AssessmentSchedule.SubjectId))]
    public Guid SubjectId { get; set; }

    [Required]
    [ForeignKey(nameof(AssessmentSchedule.AssessmentId))]
    [Column(nameof(AssessmentSchedule.AssessmentId))]
    public Guid AssessmentId { get; set; }

    [Required]
    [Column(nameof(AssessmentSchedule.AssessmentSource))]
    [StringLength(DBConstants.MaxUrlLength)]
    public string AssessmentSource { get; set; }

    [Required]
    [Column(nameof(AssessmentSchedule.AssessmentDate))]
    public DateOnly AssessmentDate { get; set; }

    [Required]
    [Column(nameof(AssessmentSchedule.StartTime))]
    public TimeOnly StartTime { get; set; }

    [Required]
    [Column(nameof(AssessmentSchedule.EndTime))]
    public TimeOnly EndTime { get; set; }

    [Required]
    [Column(nameof(AssessmentSchedule.TotalMarks))]
    [StringLength(DBConstants.MaxResult)]
    public string TotalMarks { get; set; }

    [Required]
    [StringLength(DBConstants.MaxResult)]
    [Column(nameof(AssessmentSchedule.Passingmarks))]
    public string Passingmarks { get; set; }

    [DefaultValue(false)]
    [Column(nameof(AssessmentSchedule.IsDeleted))]
    public bool? IsDeleted { get; set; }

    [Required]
    [Column(nameof(AssessmentSchedule.Status))]
    [StringLength(DBConstants.MaxEnumStringLength)]
    public string Status { get; set; }

    [JsonIgnore]
    public  Batch Batch { get; set; }

    [JsonIgnore]
    public Subject Subject { get; set; }

    [JsonIgnore]
    public Assessment Assessment { get; set; }

    public virtual List<Submission> Submissions { get; set; }
}

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

[Table(nameof(Submission))]
public class Submission
{
    [Key]
    [Column(nameof(Submission.Id))]
    public Guid Id { get; set; }

    [Column(nameof(Submission.StudentId))]
    [ForeignKey(nameof(Submission.StudentId))]
    public Guid StudentId { get; set; }

    [Column(nameof(Submission.AssessmentScheduleId))]
    [ForeignKey(nameof(Submission.AssessmentScheduleId))]
    public Guid AssessmentScheduleId { get; set; }

    [Required]
    [StringLength(DBConstants.MaxUrlLength)]
    [Column(nameof(Submission.SubmissionUrl))]
    public string SubmissionUrl { get; set; }

    [Required]
    [StringLength(DBConstants.MaxStatusLength)]
    [Column(nameof(Submission.Status))]
    public string Status { get; set; }

    
    [Column(nameof(Submission.Marks))]
    public float? Marks { get; set; }

    [DefaultValue(false)]
    [Column(nameof(Submission.IsDeleted))]
    public bool? IsDeleted { get; set; }

    [JsonIgnore]
    public virtual Student Student { get; set; }

    [JsonIgnore]
    public  AssessmentSchedule AssessmentSchedule { get; set; }
}

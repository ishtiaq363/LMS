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
[Table(nameof(BatchStudent))]
public class BatchStudent
{
    [Key]
    [Column(nameof(BatchStudent.Id))]
    public Guid Id { get; set; }

    [ForeignKey(nameof(BatchStudent.StudentId))]
    [Column(nameof(BatchStudent.StudentId))]
    public Guid StudentId { get; set; }

    [ForeignKey(nameof(BatchStudent.BatchId))]
    [Column(nameof(BatchStudent.BatchId))]
    public Guid BatchId { get; set; }

    [Column(nameof(BatchStudent.EnrollmentNo))]
    [StringLength(DBConstants.MaxStatusLength)]
    public string EnrollmentNo { get; set; }

    [Required]
    [Column(nameof(BatchStudent.EnrollmentDate))]
    public DateOnly EnrollmentDate { get; set; }

    [Column(nameof(BatchStudent.Status))]
    [StringLength(DBConstants.MaxStatusLength)]
    public string Status { get; set; }

    [Column(nameof(BatchStudent.CompletionDate))]
    public DateOnly? CompletionDate { get; set; }



    [Column(nameof(BatchStudent.Remarks))]
    [StringLength(DBConstants.MaxDescriptionLength)]
    public string Remarks { get; set; }

    [DefaultValue(false)]
    [Column(nameof(BatchStudent.IsDeleted))]
    public bool? IsDeleted { get; set; }

    [JsonIgnore]
    public virtual Student Student { get; set; }

    [JsonIgnore]
    public virtual Batch Batch { get; set; }
   
}

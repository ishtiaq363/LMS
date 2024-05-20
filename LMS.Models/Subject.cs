using LMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models;
[Table(nameof(Subject))]
public class Subject
{
    [Key]
    [Column(nameof(Subject.Id))]
    public Guid Id { get; set; }
        
    [Required]
    [Column(nameof(Subject.Name))]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    public string Name { get; set; }

    [Column(nameof(Subject.Level))]
    [StringLength(DBConstants.MaxLevelLength)]
    public string Level { get; set; }

    [Column(nameof(Subject.Status))]
    [StringLength(DBConstants.MaxStatusLength)]
    public string Status { get; set; }

    [Column(nameof(Subject.ResourceScope))]
    [StringLength(DBConstants.MaxNotesLength)]
    public string? ResourceScope {get; set;}

    [Column(nameof(Subject.CreatedAt))]
    public DateTime? CreatedAt { get; set; }

    [Column(nameof(Subject.UpdatedAt))]
    public DateTime? UpdatedAt { get; set; }

    [DefaultValue(false)]
    [Column(nameof(Subject.IsDeleted))]
    public bool? IsDeleted { get; set; }

    public virtual List<CourseSubject> CourseSubjects { get; set; }
    [InverseProperty(nameof(SubjectDetail.Subject))]
    public virtual List<SubjectDetail> SubjectDetails { get; set; }

    public virtual SubjectOutline SubjectOutline { get; set; }
}

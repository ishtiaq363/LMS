using LMS.Utility;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models;
[Table(nameof(Course))]
public class Course
{
    [Key]
    [Column(nameof(Course.Id))]
    public Guid Id { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    [Column(nameof(Course.Title))]
    public string Title { get; set; }

    [Required,StringLength(DBConstants.MaxDescriptionLength)]
    [Column(nameof(Course.Description))]
    public string Description { get; set; }

    [Required]
    [Column(nameof(Course.Duration))]
    [DisplayName("Duration: (Months)")]
    public int Duration { get; set; }

    [Required]
    [Column(nameof(Course.Price))]
    [DisplayName("Fee: per month")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDescriptionLength)]
    [Column(nameof(Course.Prerequisites))]
    public string Prerequisites { get; set; }

    [Required]
    [StringLength(DBConstants.MaxLevelLength)]
    [Column(nameof(Course.Level))]
    public string Level { get; set; }

    [Required]
    [Column(nameof(Course.Status))]
    [StringLength(DBConstants.MaxStatusLength)]
    public string Status { get; set; }

    [Column(nameof(Course.CreatedAt))]
    public DateTime? CreatedAt { get; set; }

    [Column(nameof(Course.UpdatedAt))]
    public DateTime? UpdatedAt { get; set; }

    [DefaultValue(false)]
    [Column(nameof(Course.IsDeleted))]
    public bool? IsDeleted { get; set; }

    public virtual List<Batch> Batches { get; set; }
       
    public virtual List<CourseSubject> CourseSubjects { get; set; }
}

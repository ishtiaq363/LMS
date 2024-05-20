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

[Table(nameof(SubjectDetail))]
public class SubjectDetail
{
    [Key]
    [Column(nameof(SubjectDetail.Id))]
    public Guid Id { get; set; }

    [Required]
    [Column(nameof(SubjectDetail.SubjectId))] // Rename the column to avoid conflict
    public Guid SubjectId { get; set; }

    [StringLength(DBConstants.MaxLevelLength)]
    [Column(nameof(SubjectDetail.ResourceType))]
    public string ResourceType { get; set; }

    [StringLength(DBConstants.MaxUrlLength)]
    [Column(nameof(SubjectDetail.ResourceUrl))] // Map to a unique column name
    public string ResourceUrl { get; set; }

    [DefaultValue(false)]
    [Column(nameof(SubjectDetail.IsDeleted))]
    public bool? IsDeleted { get; set; }

    [ForeignKey(nameof(SubjectDetail.SubjectId))]
    [InverseProperty(nameof(Subject.SubjectDetails))]
    [JsonIgnore]
    public virtual  Subject Subject { get; set; }

   
}





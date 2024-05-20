using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMS.Models;

[Table(nameof(CourseSubject))]
public  class CourseSubject
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(CourseSubject.CourseId))]
    public Guid CourseId { get; set; }
   
    [ForeignKey(nameof(CourseSubject.SubjectId))]
    public Guid SubjectId { get; set; }

    [DefaultValue(false)]
    [Column(nameof(CourseSubject.IsDeleted))]
    public bool? IsDeleted { get; set; }

    [JsonIgnore]
    public virtual Course crouse { get; set; }


    [JsonIgnore]
    public virtual Subject Subject { get; set; }


}

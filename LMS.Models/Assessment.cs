using LMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models;

[Table(nameof(Assessment))]
public class Assessment
{
    [Key]
    [Column(nameof(Assessment.Id))]
    public Guid Id { get; set; }

    [Required]
    [Column(nameof(Assessment.Name))]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    public string Name { get; set; }

    [DefaultValue(false)]
    [Column(nameof(Assessment.IsDeleted))]
    public bool? IsDeleted { get; set; }

    public virtual List<AssessmentSchedule> AssessmentSchedules { get; set; }
}

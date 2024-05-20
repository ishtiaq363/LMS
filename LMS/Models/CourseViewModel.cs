using LMS.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSWeb.Models;

public class CourseViewModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    public string Title { get; set; }


    [Required, StringLength(DBConstants.MaxDescriptionLength)]
    public string Description { get; set; }

    [Required]
    [DisplayName("Duration: (Months)")]
    public int Duration { get; set; }

    [Required]
    [DisplayName("Fee: per month")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDescriptionLength)]
    public string Prerequisites { get; set; }

    [Required]
    [DisplayName("Select the Level")]
    [StringLength(DBConstants.MaxLevelLength)]
    public string Level { get; set; }

    [Required]
    [DisplayName("Select the Status")]
    [StringLength(DBConstants.MaxStatusLength)]
    public string Status { get; set; }

}


using LMS.Utility;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace LMS.Models;

[Table(nameof(Student))]
public class Student
{
    [Key]
    [Column(nameof(Student.Id))]
    public Guid Id { get; set; }


    [DisplayName("Name")]
    [Required]
    [Column(nameof(Student.FullName))]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    public string FullName { get; set; }

    [DisplayName("Father's Name")]
    [Required]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    [Column(nameof(Student.FatherName))]
    public string FatherName { get; set; }

    [Required]
    [StringLength(DBConstants.MaxRollNoLength)]
    [Column(nameof(Student.RegistrationNo))]    
    public string RegistrationNo { get; set; }

    [Required]
    [StringLength(DBConstants.MaxEmailLength)]
    [Column(nameof(Student.Email))]
    public string Email { get; set; }

    [Required]
    [StringLength(DBConstants.MaxRollNoLength)]
    [Column(nameof(Student.Password))]
    public string Password { get; set; }

    [Required]
    [StringLength(DBConstants.MaxPhoneLength)]
    [Column(nameof(Student.MobileNo))]
    public string MobileNo { get; set; }

    
    [StringLength(DBConstants.MaxUrlLength)]
    [Column(nameof(Student.ImageURL))]
    public string ImageURL { get; set; }

    [Required]
    [Column(nameof(Student.DOB))]
    public DateOnly DOB { get; set; }

    [DefaultValue("Male")]
    [AllowedValues(["Male", "Female", "Transgender"])]
    [Column(nameof(Student.Gender))]
    public string Gender { get; set; }

    [Required]
    [StringLength(DBConstants.MaxAddressLineLength)]
    [Column(nameof(Student.Address))]
    public string Address { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    [Column(nameof(Student.City))]
    public string City { get; set; }

    // [DefaultValue("Registered")]
    [StringLength(DBConstants.MaxStatusLength)]
    [Column(nameof(Student.Status))]
    public string? Status { get; set; }
    
    [Column(nameof(Student.CreatedAt))]
    public DateTime? CreatedAt { get; set; }

    [Column(nameof(Student.UpdatedAt))]
    public DateTime? UpdatedAt { get; set; }

    [DefaultValue(false)]
    [Column(nameof(Student.IsDeleted))]
    public bool? IsDeleted { get; set; }

    [JsonIgnore]
    public virtual List<BatchStudent> CourseStudents { get; set; }

    [JsonIgnore]
    public virtual List<Payment> Payments { get; set; }

    [JsonIgnore]
    public virtual StudentDetail StudentDetail { get; set; }
    [JsonIgnore]
    public virtual List<Submission> Submissions { get; set; }

    [JsonIgnore]
    public virtual List<Exam> Exams { get; set; }

}

using LMS.Models;
using LMS.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSWeb.Models
{
    public class StudentDetailViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid StudentId { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        public string Religon { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(DBConstants.MaxCodeLength)]
        public string Province { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        public string Phone { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        public string Country { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        public string Nationality { get; set; }

        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string GuardianName { get; set; }

        [Required]
        [StringLength(DBConstants.MaxPhoneLength)]
        public string GuardianContactNo { get; set; }

        [Required]
        [StringLength(DBConstants.MaxPhoneLength)]
        public string EmergencyContactNo { get; set; }

        [Required]
        [StringLength(DBConstants.MaxEmailLength)]
        public string PreviousQualification { get; set; }

        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string InstituteName { get; set; }

        [Required]
        [StringLength(DBConstants.MaxYearLength)]
        public string PassingYear { get; set; }

        [Required]
        [StringLength(DBConstants.MaxResult)]
        public string MarksAverage { get; set; }

        public string? MyEmail { get; set; }
        public string? MyPassword { get; set; }
        public string? MyRePassword { get; set; }

        [DefaultValue(false)]
        public bool? IsDeleted { get; set; }

        public Student? Student { get; set; }
    }
}

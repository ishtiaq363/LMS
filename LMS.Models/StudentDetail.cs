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

namespace LMS.Models
{
    [Table(nameof(StudentDetail))]
    public class StudentDetail
    {
        [Key]
        [Column(nameof(StudentDetail.Id))]
        public Guid Id { get; set; }

        [ForeignKey(nameof(StudentDetail.StudentId))]
        [Column(nameof(StudentDetail.StudentId))]
        public Guid StudentId { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        [Column(nameof(StudentDetail.Religon))]
        public string Religon { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        [Column(nameof(StudentDetail.PostalCode))]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(DBConstants.MaxCodeLength)]
        [Column(nameof(StudentDetail.Province))]
        public string Province { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        [Column(nameof(StudentDetail.Phone))]
        public string Phone { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        [Column(nameof(StudentDetail.Country))]
        public string Country { get; set; }

        [Required]
        [StringLength(DBConstants.MaxLevelLength)]
        [Column(nameof(StudentDetail.Nationality))]
        public string Nationality { get; set; }

        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        [Column(nameof(StudentDetail.GuardianName))]
        public string GuardianName { get; set; }

        [Required]
        [StringLength(DBConstants.MaxPhoneLength)]
        [Column(nameof(StudentDetail.GuardianContactNo))]
        public string GuardianContactNo { get; set; }

        [Required]
        [StringLength(DBConstants.MaxPhoneLength)]
        [Column(nameof(StudentDetail.EmergencyContactNo))]
        public string EmergencyContactNo { get; set; }

        [Required]
        [StringLength(DBConstants.MaxEmailLength)]
        [Column(nameof(StudentDetail.PreviousQualification))]
        public string PreviousQualification { get; set; }

        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        [Column(nameof(StudentDetail.InstituteName))]
        public string InstituteName { get; set; }

        [Required]
        [StringLength(DBConstants.MaxYearLength)]
        [Column(nameof(StudentDetail.PassingYear))]
        public string PassingYear { get; set; }

        [Required]
        [StringLength(DBConstants.MaxResult)]
        [Column(nameof(StudentDetail.MarksAverage))]
        public string MarksAverage { get; set; }

        [DefaultValue(false)]
        [Column(nameof(StudentDetail.IsDeleted))]
        public bool? IsDeleted { get; set; }

        [JsonIgnore]
        public virtual Student Student { get; set; }
    }
}

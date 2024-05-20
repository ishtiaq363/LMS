using LMS.Models;
using LMS.Utility;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSWeb.Models
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }


        [DisplayName("Full Name")]
        [Required]       
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string FullName { get; set; }

        [DisplayName("Father's Name")]
        [Required]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string FatherName { get; set; }

        [Required]
        [DisplayName("Registration No.")]
        [StringLength(DBConstants.MaxRollNoLength)]
        public string RegistrationNo { get; set; }

        [Required]
        [DisplayName("E-mail")]
        [StringLength(DBConstants.MaxEmailLength)]
        public string Email { get; set; }

        [Required]
        [DisplayName("Generate Password")]
        [StringLength(DBConstants.MaxRollNoLength)]
        public string Password { get; set; }

        [Required]
        [DisplayName("Mobile No")]
        [StringLength(DBConstants.MaxPhoneLength)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(DBConstants.MaxUrlLength)]
        public string? ImageURL { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        public DateOnly DOB { get; set; }

        [DefaultValue("Male")]
        [AllowedValues(["Male", "Female", "Transgender"])]
        public string Gender { get; set; }

        [Required]
        [DisplayName("Address")]
        [StringLength(DBConstants.MaxAddressLineLength)]
        public string Address { get; set; }

        [Required]
        [DisplayName("City")]
        [StringLength(DBConstants.MaxDisplayNameLength)]
        public string City { get; set; }

        [StringLength(DBConstants.MaxRollNoLength)]
        public string? RePassword { get; set; }

        [Required]
        [DefaultValue("Registered")]
        [DisplayName("Current Status")]
        [StringLength(DBConstants.MaxStatusLength)]
        public string? Status { get; set; }
                
    }
}

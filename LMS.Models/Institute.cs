using LMS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Models;

[Table(nameof(Institute))]
public class Institute
{
    [Key]
    [Column(nameof(Institute.Id))]
    public Guid Id { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    [Column(nameof(Institute.Name))]
    public string Name { get; set; }

    [Required]
    [StringLength(DBConstants.MaxAddressLineLength)]
    [Column(nameof(Institute.Address))]
    public string Address { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    [Column (nameof(Institute.City))]
    public string City { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    [Column(nameof(Institute.State))]
    public string State { get; set; }

    [Required]
    [StringLength(DBConstants.MaxZipCodeLength)]
    [Column(nameof(Institute.ZipCode))]
    public string ZipCode { get; set; }

    [Required]
    [StringLength(DBConstants.MaxDisplayNameLength)]
    [Column(nameof(Institute.Country))]
    public string Country { get; set; }

    [Column(nameof(Institute.FoundedDate))]
    public DateTime? FoundedDate { get; set; }

    [Column(nameof(Institute))]
    public string ImageUrl { get; set; }

}

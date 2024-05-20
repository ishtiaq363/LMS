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

[Table(nameof(BatchFee))]
public class BatchFee
{
    [Key]
    [Column(nameof(BatchFee.Id))]
    public Guid Id { get; set; }

    [Required]
    [Column(nameof(BatchFee.BatchId))]
    public Guid BatchId { get; set; }

    [Required]
    [Column(nameof(BatchFee.FeeAmount), TypeName = "decimal(18, 2)")]
    [DisplayName("Fee Amount:")]
    public decimal FeeAmount { get; set; }

    [Required]
    [Column(nameof(BatchFee.EffectiveDate))]
    [DisplayName("Effective Date")]
    public DateTime EffectiveDate { get; set; }

    [DefaultValue(false)]
    [Column(nameof(BatchFee.IsDeleted))]
    public bool? IsDeleted { get; set; }

    [ForeignKey(nameof(BatchFee.BatchId))]

    [JsonIgnore]
    public virtual Batch Batch { get; set; }
    [JsonIgnore]
    public virtual List<Payment> Payments { get; set; } = new List<Payment>();

}


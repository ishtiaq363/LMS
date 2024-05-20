using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using LMS.Utility;
using System.ComponentModel;

namespace LMS.Models
{
    [Table(nameof(Payment))]
    public class Payment
    {
        [Key]
        [Column(nameof(Payment.Id))]
        public Guid Id{ get; set; }

        [Required]
        [Column(nameof(Payment.StudentId))]
        public Guid StudentId { get; set; }

        [Required]
        [Column(nameof(Payment.BatchId))]
        public Guid BatchId { get; set; }

        [Required]
        [Column(nameof(Payment.PaymentAmount), TypeName = "decimal(18, 2)")]
        public decimal PaymentAmount { get; set; }

        [Required]
        [Column(nameof(Payment.PaymentDate))]
        public DateTime PaymentDate { get; set; }

        [Required]
        [StringLength(DBConstants.MaxStatusLength)]
        [Column(nameof(Payment.Status))]
        public string? Status { get; set; }

        [Required]
        [StringLength(DBConstants.MaxUrlLength)]
        [Column(nameof(Payment.ReceiptUrl))]
        public string ReceiptUrl { get; set; }

        [DefaultValue(false)]
        [Column(nameof(Payment.IsDeleted))]
        public bool? IsDeleted { get; set; }

        //navigational properties
        [ForeignKey(nameof(Payment.StudentId))]

        [JsonIgnore]
        public virtual Student Student { get; set; }

        [ForeignKey(nameof(Payment.BatchId))]
        [JsonIgnore]
        public virtual Batch Batch { get; set; }
    }
}

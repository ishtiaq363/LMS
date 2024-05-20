using LMS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMSWeb.Models
{
    public class BatchFeeViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid BatchId { get; set; }

        [Required]
        [DisplayName("Fee Amount(Per Monthe)")]
        public decimal FeeAmount { get; set; }

        [Required]
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }

        [DefaultValue("")]
        public string? batchList { get; set; }
       // public Guid BatchId { get; internal set; }
    }
}

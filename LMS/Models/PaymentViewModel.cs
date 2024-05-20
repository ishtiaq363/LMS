using LMS.Models;
using LMS.Utility;
using System.ComponentModel.DataAnnotations;

namespace LMSWeb.Models
{
    public class PaymentViewModel
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public string? StudentName { get; set; }

        public Guid BatchId { get; set; }

        public string? BatchName { get; set; }

        public decimal PaymentAmount { get; set; }

        public DateTime PaymentDate { get; set; }

        [StringLength(DBConstants.MaxStatusLength)]
        public string Status { get; set; }

        [StringLength(DBConstants.MaxUrlLength)]
        public string? ReceiptUrl { get; set; }

        public bool? IsDeleted { get; set; }
        
        public virtual Student? Student { get; set; }

        public virtual Batch? Batch { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InvoicingSystem.Models
{
    public partial class InvoiceItem
    {
        public int Id { get; set; }
        public int? InvoiceId { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }
    }
}

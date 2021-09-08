using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace InvoicingSystem.Models
{
    public partial class Invoice
    {
        public int Id { get; set; }
        public string Client { get; set; }
        [Display(Name = "Amount")]
        public decimal? InvoiceAmount { get; set; }
        [Display(Name = "Amount + Vat")]
        public decimal? InvoiceAmountPlusVat { get; set; }
        [Display(Name = "Vat %")]
        public decimal? VatRate { get; set; }
        [Display(Name = "Status")]
        public string InvoiceStatus { get; set; }
        [Display(Name = "Date")]
        public DateTime? InvoiceDate { get; set; }
        [Display(Name = "Created At")]
        public DateTime? CreatedAt { get; set; }
    }
}

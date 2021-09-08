using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InvoicingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoicingSystem.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoicingSystemContext _invoicingSystemContext;
        const int pageSize = 5;

        public InvoicesController(InvoicingSystemContext invoicingSystemContext)
        {
            _invoicingSystemContext = invoicingSystemContext;
        }

        // GET: Invoices
        public ActionResult Index(int page, [FromQuery] string errorMsg)
        {
            int totalPages = _invoicingSystemContext.Invoices.Count() / pageSize;
            var data = this._invoicingSystemContext.Invoices.Skip(page * pageSize).Take(pageSize).ToList();
            ViewBag.MaxPage = totalPages;
            ViewBag.Page = page;
            ViewBag.InvoicesErrorMsg = errorMsg;

            return View(data);
        }

        // GET: Invoices/ExportCSVTransactions
        public ActionResult ExportCSVTransactions()
        {
            try
            {
                List<Invoice> invoices = _invoicingSystemContext.Invoices.ToList();
                string delimiter = ",";
                StringBuilder sb = new StringBuilder();
                string[] newLine = { "Invoice ID", "Company Name", "Invoice Amount" };
                sb.AppendLine(string.Join(delimiter, newLine));
                for (int index = 0; index < invoices.Count; index++)
                {
                    newLine = new string[] { invoices[index].Id.ToString(), invoices[index].Client, invoices[index].InvoiceAmount.ToString().Replace(',', '.') };
                    sb.AppendLine(string.Join(delimiter, newLine));
                }

                return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "Transactions.csv");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return RedirectToAction("Index", new { errorMsg = ViewBag.InvoicesErrorMsg });
        }

        // GET: Invoices/ExportCustomerReport/5
        public ActionResult ExportCustomerReport(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    Invoice customerInvoice = _invoicingSystemContext.Invoices
                       .Where(x => x.Id == id).First();
                    if (customerInvoice is Invoice)
                    {
                        string delimiter = ",";
                        StringBuilder sb = new StringBuilder();
                        string[] newLine = { "Company Name", "Total Amount Invoiced", "Total Amount Paid", "Total Amount Outstanding" };
                        sb.AppendLine(string.Join(delimiter, newLine));
                        newLine = new string[] { customerInvoice.Client, customerInvoice.InvoiceAmountPlusVat.ToString().Replace(',', '.'), customerInvoice.InvoiceStatus == "paid" ? customerInvoice.InvoiceAmountPlusVat.ToString().Replace(',', '.') : "0", customerInvoice.InvoiceStatus == "unpaid" ? customerInvoice.InvoiceAmountPlusVat.ToString().Replace(',', '.') : "0" };
                        sb.AppendLine(string.Join(delimiter, newLine));

                        return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "CustomerReport - " + customerInvoice.Client + ".csv");
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return RedirectToAction("Index", new { errorMsg = ViewBag.InvoicesErrorMsg });
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    List<InvoiceItem> invoiceItems = _invoicingSystemContext.InvoiceItems.Where(x => x.InvoiceId == id).ToList();
                    if (invoiceItems == null)
                    {
                        return View();
                    }
                    return View(invoiceItems);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return RedirectToAction("Index", new { errorMsg = ViewBag.InvoicesErrorMsg });
        }

        // POST: Invoices/SetStatus/5
        [HttpPost]
        public ActionResult SetStatus(string status, int id)
        {
            try
            {
                bool isSet = false;

                Invoice invoice = _invoicingSystemContext.Invoices.Where(x => x.Id == id).FirstOrDefault();
                if (invoice is Invoice)
                {
                    invoice.InvoiceStatus = status;
                    _invoicingSystemContext.Entry(invoice).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _invoicingSystemContext.SaveChanges();
                    isSet = false;
                }

                if (!isSet)
                {
                    throw new ArgumentNullException();
                }

                return Json(new { success = true, msg = "Successful operation", invoiceStatus = invoice.InvoiceStatus });

            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return RedirectToAction("Index", new { errorMsg = ViewBag.InvoicesErrorMsg });
        }

        private void HandleException(Exception ex)
        {
            ViewBag.InvoicesErrorMsg = $"There was an error {ex.GetType().Name} - {ex.Message}";
        }
    }
}
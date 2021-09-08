using InvoicingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace XTestController
{
    public class InvoiceSeedFixture : IDisposable
    {
        public static IConfigurationRoot config { get; private set; }
        public InvoicingSystemContext InvoiceContext { get; private set; } = new InvoicingSystemContext();

        public InvoiceSeedFixture()
        {
            var options = new DbContextOptionsBuilder<InvoicingSystemContext>()
           .UseInMemoryDatabase("assignment_db")
           .Options;

            InvoiceContext = new InvoicingSystemContext(options, config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                    .Build());
            InvoiceContext.Invoices.Add(new Invoice
            {
                Id = 1,
                Client = "test",
                InvoiceAmount = 111
            });
            InvoiceContext.Invoices.Add(new Invoice {
                Id = 2,
                Client = "test 2",
                InvoiceAmount = 222
            });
            InvoiceContext.SaveChanges();
        }

        public void Dispose()
        {
            InvoiceContext.Dispose();
        }
    }
}

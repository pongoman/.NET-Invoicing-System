using InvoicingSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XTestController
{
    public class InvoicesControllerUnitTest : IClassFixture<InvoiceSeedFixture>
    {
        readonly InvoiceSeedFixture fixture;

        public InvoicesControllerUnitTest(InvoiceSeedFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void Index_ReturnsAViewResult()
        {
            using (var context = fixture.InvoiceContext)
            {
                var controller = new InvoicesController(context);

                var result = controller.Index(0, "");

                Assert.IsType<ViewResult>(result);
            }
        }
    }
}

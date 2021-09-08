using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace InvoicingSystem.Models
{
    public partial class InvoicingSystemContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public InvoicingSystemContext()
        {
        }

        public InvoicingSystemContext(DbContextOptions<InvoicingSystemContext> options, IConfiguration configuration)
        : base(options)
        {
            Configuration = configuration;
        }

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                ///#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql(Configuration.GetConnectionString("InvoiceSystem"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.4-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("invoices");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Client)
                    .HasMaxLength(255)
                    .HasColumnName("client");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.InvoiceAmount)
                    .HasPrecision(15, 5)
                    .HasColumnName("invoice_amount");

                entity.Property(e => e.InvoiceAmountPlusVat)
                    .HasPrecision(15, 5)
                    .HasColumnName("invoice_amount_plus_vat");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("date")
                    .HasColumnName("invoice_date");

                entity.Property(e => e.InvoiceStatus)
                    .HasColumnType("enum('paid','unpaid')")
                    .HasColumnName("invoice_status");

                entity.Property(e => e.VatRate)
                    .HasPrecision(15, 5)
                    .HasColumnName("vat_rate");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.ToTable("invoice_items");

                entity.HasCharSet("latin1")
                    .UseCollation("latin1_swedish_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(15, 5)
                    .HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp")
                    .HasColumnName("created_at");

                entity.Property(e => e.InvoiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("invoice_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

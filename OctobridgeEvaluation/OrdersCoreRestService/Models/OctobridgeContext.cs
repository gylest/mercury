using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OctobridgeCoreRestService.Models
{
    public partial class OctobridgeContext : DbContext
    {
        public OctobridgeContext()
        {
        }

        public OctobridgeContext(DbContextOptions<OctobridgeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<CodedValue> CodedValues { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderAudit> OrderAudits { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.ToTable("Attachment");

                entity.Property(e => e.Filedata).IsRequired();

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Filetype)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RecordCreated).HasDefaultValueSql("(sysdatetime())");
            });

            modelBuilder.Entity<CodedValue>(entity =>
            {
                entity.HasKey(e => new { e.GroupName, e.Value });

                entity.ToTable("CodedValue");

                entity.Property(e => e.GroupName).HasMaxLength(20);

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(200);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.AddressLine2).HasMaxLength(60);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.RecordCreated).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.RecordModified).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.FreightAmount).HasColumnType("money");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RecordCreated).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.RecordModified).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.SubTotal).HasColumnType("money");

                entity.Property(e => e.TotalDue).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");
            });

            modelBuilder.Entity<OrderAudit>(entity =>
            {
                entity.HasKey(e => e.AuditId);

                entity.ToTable("OrderAudit");

                entity.Property(e => e.AuditType)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.FreightAmount).HasColumnType("money");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SubTotal).HasColumnType("money");

                entity.Property(e => e.TotalDue).HasColumnType("money");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(sysdatetime())");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("OrderDetail");

                entity.Property(e => e.LineId).ValueGeneratedOnAdd();

                entity.Property(e => e.RecordCreated).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.RecordModified).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order_Id");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Order");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Payment");

                entity.HasIndex(e => e.TransactionId, "UN_Payment_TransactionId")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CardNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CardType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ExpiryDate)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Gateway)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.RecordCreated).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.RecordModified).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Order");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RecordCreated).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.RecordModified).HasDefaultValueSql("(sysdatetime())");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategory_Product");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RecordCreated).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.RecordModified).HasDefaultValueSql("(sysdatetime())");
            });

            // Exclude AttachmentInfo and User from being mapped to the database
            modelBuilder.Ignore<AttachmentInfo>();
            modelBuilder.Ignore<User>();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

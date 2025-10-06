using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace TestDatabaseNUnit.Models
{
    public partial class OctobridgeContext : DbContext
    {
        private string connectionString;

        public OctobridgeContext()
        {
            var cb = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = cb.GetConnectionString("OctobridgeDatabaseConnection");
        }

        public OctobridgeContext(DbContextOptions<OctobridgeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<CodedValue> CodedValue { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderAudit> OrderAudit { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Attachment>(entity =>
            {
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

                entity.Property(e => e.GroupName).HasMaxLength(20);

                entity.Property(e => e.Value).HasMaxLength(100);

                entity.Property(e => e.Description).HasMaxLength(200);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
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
                entity.Property(e => e.FreightAmount).HasColumnType("money");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RecordCreated).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.RecordModified).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.SubTotal).HasColumnType("money");

                entity.Property(e => e.TotalDue).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer");

               // let EF Core know that the target table has a trigger
               entity.ToTable(tb => tb.HasTrigger("SomeTrigger"));
            });

            modelBuilder.Entity<OrderAudit>(entity =>
            {
                entity.HasKey(e => e.AuditId);

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
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategory_Product");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RecordCreated).HasDefaultValueSql("(sysdatetime())");

                entity.Property(e => e.RecordModified).HasDefaultValueSql("(sysdatetime())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

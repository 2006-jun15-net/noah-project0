using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreApp.DataAccess.Model
{
    public partial class _2006StoreApplicationContext : DbContext
    {
        public _2006StoreApplicationContext()
        {
        }

        public _2006StoreApplicationContext(DbContextOptions<_2006StoreApplicationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsInStores> ProductsInStores { get; set; }
        public virtual DbSet<ProductsOfOrders> ProductsOfOrders { get; set; }
        public virtual DbSet<Stores> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:2006noahfuntanilla.database.windows.net,1433;Initial Catalog=2006StoreApplication;Persist Security Info=False;User ID=nfuntanilla;Password=nFunAzure1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK__Customer__A4AE64D88B25FA17");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__Orders__C3905BCFCB277E0B");

                entity.Property(e => e.OrderDate).HasDefaultValueSql("(sysutcdatetime())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_CustomerId");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__B40CC6CD4E31DBD9");

                entity.Property(e => e.ProductId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ProductsInStores>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId })
                    .HasName("PK_ProductsInStores_StoreId_ProductId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsInStores)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_PinS_Products_ProductId");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ProductsInStores)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_PinS_Stores_StoreId");
            });

            modelBuilder.Entity<ProductsOfOrders>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_ProductsOfOrder_OrderId_ProductId");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductsOfOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_PofO_Orders_OrderId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductsOfOrders)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_PofO_Products_ProductId");
            });

            modelBuilder.Entity<Stores>(entity =>
            {
                entity.HasKey(e => e.StoreId)
                    .HasName("PK__Stores__3B82F101BBDD1620");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stores_OrderId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

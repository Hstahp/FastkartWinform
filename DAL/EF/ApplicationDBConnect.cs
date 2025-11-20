using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.EF
{
    public partial class ApplicationDBConnect : DbContext
    {
        public ApplicationDBConnect()
            : base("name=ApplicationDBConnect")
        {
        }

        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<BlogCategories> BlogCategories { get; set; }
        public virtual DbSet<BlogPosts> BlogPosts { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartItem> CartItem { get; set; }
        public virtual DbSet<Functions> Functions { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Permissions> Permissions { get; set; }
        public virtual DbSet<PermissionTypes> PermissionTypes { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductSubCategory> ProductSubCategory { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<StockStatus> StockStatus { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogCategories>()
                .HasMany(e => e.BlogPosts)
                .WithRequired(e => e.BlogCategories)
                .HasForeignKey(e => e.CategoryUid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Functions>()
                .HasMany(e => e.Permissions)
                .WithRequired(e => e.Functions)
                .HasForeignKey(e => e.FunctionId);

            modelBuilder.Entity<PermissionTypes>()
                .HasMany(e => e.Permissions)
                .WithRequired(e => e.PermissionTypes)
                .HasForeignKey(e => e.PermissionTypeId);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.CartItem)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderItem)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.ProductSubCategory)
                .WithRequired(e => e.ProductCategory)
                .HasForeignKey(e => e.CategoryUid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductSubCategory>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.ProductSubCategory)
                .HasForeignKey(e => e.SubCategoryUid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Permissions)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.RoleUid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StockStatus>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.StockStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Unit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.OtpCode)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.BlogPosts)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.AuthorUid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Cart)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserUid);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserUid);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Wishlist)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserUid);
        }
    }
}

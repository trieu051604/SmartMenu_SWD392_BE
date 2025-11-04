using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartMenu.Domain.Entities;

namespace SmartMenu.Infrastructure.Data;

public partial class SmartMenuDbContext : DbContext
{
    public SmartMenuDbContext()
    {
    }

    public SmartMenuDbContext(DbContextOptions<SmartMenuDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<InventorySyncLog> InventorySyncLogs { get; set; }

    public virtual DbSet<MenuTemplate> MenuTemplates { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<PlaylistItem> PlaylistItems { get; set; }

    public virtual DbSet<PosPriceSyncLog> PosPriceSyncLogs { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPriceVersion> ProductPriceVersions { get; set; }

    public virtual DbSet<ProductStock> ProductStocks { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreUser> StoreUsers { get; set; }

    public virtual DbSet<TemplateInstance> TemplateInstances { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=SmartMenuDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__AuditLog__A17F239822EC341C");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.EntityType).HasMaxLength(100);
            entity.Property(e => e.OccurredAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("success");

            entity.HasOne(d => d.ActorUser).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.ActorUserId)
                .HasConstraintName("FK_AuditLogs_User");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BFC2D362C");

            entity.HasIndex(e => e.CategoryName, "UQ__Categori__8517B2E01CB8EF77").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK__Devices__49E123115742C4AA");

            entity.HasIndex(e => e.Identifier, "UQ__Devices__821FB019133F69C8").IsUnique();

            entity.Property(e => e.Identifier).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("offline");

            entity.HasOne(d => d.Store).WithMany(p => p.Devices)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Devices_Stores");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorId).HasName("PK__ErrorLog__35856A2AA970834C");

            entity.Property(e => e.Component).HasMaxLength(100);
            entity.Property(e => e.OccurredAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Severity).HasMaxLength(20);
        });

        modelBuilder.Entity<InventorySyncLog>(entity =>
        {
            entity.HasKey(e => e.SyncId).HasName("PK__Inventor__7E50DEC6A3D88DB1");

            entity.Property(e => e.ErrorMessage).HasMaxLength(500);
            entity.Property(e => e.ProcessedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Result).HasMaxLength(20);

            entity.HasOne(d => d.Product).WithMany(p => p.InventorySyncLogs)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_InvSync_Product");
        });

        modelBuilder.Entity<MenuTemplate>(entity =>
        {
            entity.HasKey(e => e.TemplateId).HasName("PK__MenuTemp__F87ADD276CF965A9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.MenuTemplates)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MenuTemplates_User");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38E11CE235");

            entity.Property(e => e.Amount).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValue("VND")
                .IsFixedLength();
            entity.Property(e => e.ExternalRef).HasMaxLength(100);
            entity.Property(e => e.Method).HasMaxLength(50);
            entity.Property(e => e.OrderId).HasMaxLength(64);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("initiated");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId).HasName("PK__Playlist__B30167A07A7682DC");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("draft");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Playlists_User");
        });

        modelBuilder.Entity<PlaylistItem>(entity =>
        {
            entity.HasKey(e => e.PlaylistItemId).HasName("PK__Playlist__1910CEADDFCA5B34");

            entity.HasIndex(e => new { e.PlaylistId, e.DisplayOrder }, "UQ_Playlist_Order").IsUnique();

            entity.Property(e => e.DurationSeconds).HasDefaultValue(10);

            entity.HasOne(d => d.Playlist).WithMany(p => p.PlaylistItems)
                .HasForeignKey(d => d.PlaylistId)
                .HasConstraintName("FK_PI_Playlist");

            entity.HasOne(d => d.Product).WithMany(p => p.PlaylistItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_PI_Product");

            entity.HasOne(d => d.TemplateInstance).WithMany(p => p.PlaylistItems)
                .HasForeignKey(d => d.TemplateInstanceId)
                .HasConstraintName("FK_PI_TemplateInstance");
        });

        modelBuilder.Entity<PosPriceSyncLog>(entity =>
        {
            entity.HasKey(e => e.SyncId).HasName("PK__PosPrice__7E50DEC6A7D561B4");

            entity.Property(e => e.ErrorMessage).HasMaxLength(500);
            entity.Property(e => e.ProcessedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Result).HasMaxLength(20);

            entity.HasOne(d => d.Product).WithMany(p => p.PosPriceSyncLogs)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_PosSync_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD51EFF7A3");

            entity.HasIndex(e => e.Sku, "UQ__Products__CA1ECF0DEE4B6DCE").IsUnique();

            entity.Property(e => e.Availability)
                .HasMaxLength(20)
                .HasDefaultValue("available");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Sku)
                .HasMaxLength(64)
                .HasColumnName("SKU");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Category");

            entity.HasMany(d => d.Promotions).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductPromotion",
                    r => r.HasOne<Promotion>().WithMany()
                        .HasForeignKey("PromotionId")
                        .HasConstraintName("FK_PP_Promo"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_PP_Product"),
                    j =>
                    {
                        j.HasKey("ProductId", "PromotionId").HasName("PK__ProductP__51208431EB70CD6A");
                        j.ToTable("ProductPromotions");
                    });
        });

        modelBuilder.Entity<ProductPriceVersion>(entity =>
        {
            entity.HasKey(e => e.PriceVersionId).HasName("PK__ProductP__BFA73867D8939E39");

            entity.HasIndex(e => new { e.ProductId, e.EffectiveFrom, e.EffectiveTo }, "IX_PPV_ProductTime");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValue("VND")
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Source)
                .HasMaxLength(20)
                .HasDefaultValue("manual");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductPriceVersions)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_PPV_User");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPriceVersions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_PPV_Product");
        });

        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__ProductS__B40CC6CDFB580CE6");

            entity.ToTable("ProductStock");

            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("in_stock");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Product).WithOne(p => p.ProductStock)
                .HasForeignKey<ProductStock>(d => d.ProductId)
                .HasConstraintName("FK_ProductStock_Product");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PK__Promotio__52C42FCF370CD647");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.DiscountType)
                .HasMaxLength(20)
                .HasDefaultValue("amount");
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A59C7FB97");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B616031E4DC05").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__9C8A5B4968C9CB56");

            entity.HasIndex(e => new { e.DeviceId, e.StartTime, e.EndTime, e.Priority }, "IX_Schedules_DeviceTime");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Recurrence).HasMaxLength(255);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Schedules_User");

            entity.HasOne(d => d.Device).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.DeviceId)
                .HasConstraintName("FK_Schedules_Device");

            entity.HasOne(d => d.Playlist).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.PlaylistId)
                .HasConstraintName("FK_Schedules_Playlist");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F101EA1BA0AD");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.StoreName).HasMaxLength(150);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Manager).WithMany(p => p.Stores)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_Stores_Manager");
        });

        modelBuilder.Entity<StoreUser>(entity =>
        {
            entity.HasKey(e => e.StoreUserId).HasName("PK__StoreUse__F769D91D1F1A0631");

            entity.HasIndex(e => new { e.StoreId, e.UserId }, "UQ_StoreUser").IsUnique();

            entity.HasOne(d => d.Store).WithMany(p => p.StoreUsers)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_StoreUsers_Store");

            entity.HasOne(d => d.User).WithMany(p => p.StoreUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_StoreUsers_User");
        });

        modelBuilder.Entity<TemplateInstance>(entity =>
        {
            entity.HasKey(e => e.TemplateInstanceId).HasName("PK__Template__F20561FFB2A545D1");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.TemplateInstances)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TemplateInstances_User");

            entity.HasOne(d => d.Template).WithMany(p => p.TemplateInstances)
                .HasForeignKey(d => d.TemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TemplateInstances_Template");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C0CF06111");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534ABA70512").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

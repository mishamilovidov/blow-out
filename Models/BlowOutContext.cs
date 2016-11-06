using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace BlowOut.Models
{
	public partial class BlowOutContext : DbContext
	{
    	public BlowOutContext(DbContextOptions<BlowOutContext> options)
        	: base(options)
    	{ }
    	protected override void OnModelCreating(ModelBuilder modelBuilder)
    	{
        	modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("Customers_PK");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CustCity).HasMaxLength(50);

                entity.Property(e => e.CustEmail).HasMaxLength(50);

                entity.Property(e => e.CustFirstName).HasMaxLength(50);

                entity.Property(e => e.CustLastName).HasMaxLength(50);

                entity.Property(e => e.CustPhone).HasMaxLength(22);

                entity.Property(e => e.CustState).HasMaxLength(50);

                entity.Property(e => e.CustStreetAddress).HasMaxLength(50);
            });

            modelBuilder.Entity<InstrumentPictures>(entity =>
            {
                entity.HasKey(e => e.InstrumentPictureId)
                    .HasName("InstrumentPictureID");

                entity.ToTable("Instrument_Pictures");

                entity.Property(e => e.InstrumentPictureId)
                    .HasColumnName("InstrumentPictureID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.InstrumentPicture).HasColumnType("text");
            });

            modelBuilder.Entity<Instruments>(entity =>
            {
                entity.HasKey(e => e.InstrumentId)
                    .HasName("Instruments_PK");

                entity.Property(e => e.InstrumentId)
                    .HasColumnName("InstrumentID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.InstrumentName).HasMaxLength(50);

                entity.Property(e => e.InstrumentPictureId).HasColumnName("InstrumentPictureID");

                entity.Property(e => e.InstrumentPrice).HasDefaultValueSql("0");

                entity.Property(e => e.RentalTypeId).HasColumnName("RentalTypeID");

                entity.HasOne(d => d.InstrumentPicture)
                    .WithMany(p => p.Instruments)
                    .HasForeignKey(d => d.InstrumentPictureId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Instruments_FK01");

                entity.HasOne(d => d.RentalType)
                    .WithMany(p => p.Instruments)
                    .HasForeignKey(d => d.RentalTypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("Instruments_FK00");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderNumber)
                    .HasName("Orders_PK");

                entity.Property(e => e.OrderNumber).HasDefaultValueSql("0");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.InstrumentId)
                    .HasColumnName("InstrumentID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Orders_FK00");

                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.InstrumentId)
                    .HasConstraintName("Orders_FK01");
            });

            modelBuilder.Entity<RentalTypes>(entity =>
            {
                entity.HasKey(e => e.RentalTypeId)
                    .HasName("RentalTypeID");

                entity.ToTable("Rental_Types");

                entity.Property(e => e.RentalTypeId)
                    .HasColumnName("RentalTypeID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.RentalType).HasMaxLength(10);
            });
    	}
		public virtual DbSet<Customers> Customers { get; set; }
		public virtual DbSet<Orders> Orders { get; set; }
    	public virtual DbSet<InstrumentPictures> InstrumentPictures { get; set; }
    	public virtual DbSet<Instruments> Instruments { get; set; }
    	public virtual DbSet<RentalTypes> RentalTypes { get; set; }
		public virtual DbSet<RentalInfo> RentalInfo { get; set; }
        public virtual DbSet<Customers> customer { get; set; }
        public virtual DbSet<Orders> order { get; set; }
	}
}

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
    	public virtual DbSet<InstrumentPictures> InstrumentPictures { get; set; }
    	public virtual DbSet<Instruments> Instruments { get; set; }
    	public virtual DbSet<RentalTypes> RentalTypes { get; set; }
		public virtual DbSet<RentalInfo> RentalInfo { get; set; }
	}
}

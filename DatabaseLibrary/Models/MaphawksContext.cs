using System;
using DatabaseLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseLibrary.Models
{
    public partial class MaphawksContext : DbContext
    {
        public MaphawksContext()
        {
        }

        public MaphawksContext(DbContextOptions<MaphawksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contacts> Contacts { get; set; }
        public virtual DbSet<DailyHours> DailyHours { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<SpecialQualities> SpecialQualities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacts>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__Contacts__E7FEA47797FCCECC");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.WebAddress)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithOne(p => p.Contact)
                    .HasForeignKey<Contacts>(d => d.LocationId)
                    .HasConstraintName("FK__Contacts__Locati__50D0E6F9");
            });

            modelBuilder.Entity<DailyHours>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__DailyHou__E7FEA477DD2C4905");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtfriClose)
                    .HasColumnName("HoursDTFriClose")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtfriOpen)
                    .HasColumnName("HoursDTFriOpen")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtmonClose)
                    .HasColumnName("HoursDTMonClose")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtmonOpen)
                    .HasColumnName("HoursDTMonOpen")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtsatClose)
                    .HasColumnName("HoursDTSatClose")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtsatOpen)
                    .HasColumnName("HoursDTSatOpen")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtsunClose)
                    .HasColumnName("HoursDTSunClose")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtsunOpen)
                    .HasColumnName("HoursDTSunOpen")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtthuClose)
                    .HasColumnName("HoursDTThuClose")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtthuOpen)
                    .HasColumnName("HoursDTThuOpen")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDttueClose)
                    .HasColumnName("HoursDTTueClose")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDttueOpen)
                    .HasColumnName("HoursDTTueOpen")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtwedClose)
                    .HasColumnName("HoursDTWedClose")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursDtwedOpen)
                    .HasColumnName("HoursDTWedOpen")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursFriClose)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursFriOpen)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursMonClose)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursMonOpen)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursSatClose)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursSatOpen)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursSunClose)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursSunOpen)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursThuClose)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursThuOpen)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursTueClose)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursTueOpen)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursWedClose)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.HoursWedOpen)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithOne(p => p.DailyHours)
                    .HasForeignKey<DailyHours>(d => d.LocationId)
                    .HasConstraintName("FK__DailyHour__Locat__5689C04F");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__Location__E7FEA477FF3BFBB2");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CoopLocationId)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.County)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Hours)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.LocationType)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Point)
                .HasComputedColumnSql("([geography]::Point([Latitude],[Longitude], (4326)))");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RetailOutlet)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpecialQualities>(entity =>
            {
                entity.HasKey(e => e.LocationId)
                    .HasName("PK__SpecialQ__E7FEA477004DE703");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.AcceptCash)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.AcceptDeposit)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Access)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.AccessNotes)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Cashless)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CoinStar)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.DriveThruOnly)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.EnvelopeRequired)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.HandicapAccess)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.InstallationType)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.InstantDebitCardReplacement)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LimitedTransactions)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.MemberConsultant)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.MilitaryIdRequired)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.OnMilitaryBase)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.OnPremise)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.PartnerCreditUnion)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RestrictedAccess)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SelfServiceDevice)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SelfServiceOnly)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Surcharge)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.TellerServices)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e._24hourExpressBox)
                    .HasColumnName("24HourExpressBox")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithOne(p => p.SpecialQualities)
                    .HasForeignKey<SpecialQualities>(d => d.LocationId)
                    .HasConstraintName("FK__SpecialQu__Locat__53AD53A4");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HuisAppotheek.Domain.DAL
{
    public partial class huisapotheekContext : DbContext
    {
        public huisapotheekContext()
        {
        }

        public huisapotheekContext(DbContextOptions<huisapotheekContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dokter> Dokter { get; set; }
        public virtual DbSet<Medicijn> Medicijn { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Persoonlijkeapotheek> Persoonlijkeapotheek { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=MAXIM\\SQLEXPRESSAPPDEV;Initial Catalog=huisapotheek;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dokter>(entity =>
            {
                entity.ToTable("dokter");

                entity.Property(e => e.Dokterid).HasColumnName("dokterid");

                entity.Property(e => e.Achternaam)
                    .IsRequired()
                    .HasColumnName("achternaam")
                    .HasMaxLength(50);

                entity.Property(e => e.Bus)
                    .HasColumnName("bus")
                    .HasMaxLength(4);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Huisnummer)
                    .HasColumnName("huisnummer")
                    .HasMaxLength(4);

                entity.Property(e => e.Land)
                    .HasColumnName("land")
                    .HasMaxLength(30);

                entity.Property(e => e.Mobiel)
                    .HasColumnName("mobiel")
                    .HasMaxLength(20);

                entity.Property(e => e.Postcode)
                    .HasColumnName("postcode")
                    .HasMaxLength(15);

                entity.Property(e => e.Reservatieurl)
                    .HasColumnName("reservatieurl")
                    .HasMaxLength(256);

                entity.Property(e => e.Stad)
                    .HasColumnName("stad")
                    .HasMaxLength(30);

                entity.Property(e => e.Straat)
                    .IsRequired()
                    .HasColumnName("straat")
                    .HasMaxLength(80);

                entity.Property(e => e.Telefoon)
                    .HasColumnName("telefoon")
                    .HasMaxLength(20);

                entity.Property(e => e.Voornaam)
                    .IsRequired()
                    .HasColumnName("voornaam")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Medicijn>(entity =>
            {
                entity.ToTable("medicijn");

                entity.Property(e => e.Medicijnid).HasColumnName("medicijnid");

                entity.Property(e => e.Bijsluiter)
                    .HasColumnName("bijsluiter")
                    .HasMaxLength(256);

                entity.Property(e => e.Dokterid).HasColumnName("dokterid");

                entity.Property(e => e.Groep)
                    .IsRequired()
                    .HasColumnName("groep")
                    .HasMaxLength(50);

                entity.Property(e => e.OpVoorschrift).HasColumnName("opVoorschrift");

                entity.Property(e => e.Postcode)
                    .HasColumnName("postcode")
                    .HasMaxLength(15);

                entity.Property(e => e.UrlBijsluiter)
                    .HasColumnName("urlBijsluiter")
                    .HasMaxLength(256);

                entity.Property(e => e.Vervaldatum)
                    .HasColumnName("vervaldatum")
                    .HasColumnType("date");

                entity.Property(e => e.Volledigenaam)
                    .IsRequired()
                    .HasColumnName("volledigenaam")
                    .HasMaxLength(150);

                entity.HasOne(d => d.Dokter)
                    .WithMany(p => p.Medicijn)
                    .HasForeignKey(d => d.Dokterid)
                    .HasConstraintName("FK__medicijn__dokter__5CD6CB2B");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("patient");

                entity.Property(e => e.Patientid).HasColumnName("patientid");

                entity.Property(e => e.Achternaam)
                    .IsRequired()
                    .HasColumnName("achternaam")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.Geboortedatumdatum)
                    .HasColumnName("geboortedatumdatum")
                    .HasColumnType("date");

                entity.Property(e => e.Voornaam)
                    .IsRequired()
                    .HasColumnName("voornaam")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Persoonlijkeapotheek>(entity =>
            {
                entity.HasKey(e => e.Apotheekid)
                    .HasName("PK__persoonl__243EA1F0BEEB3801");

                entity.ToTable("persoonlijkeapotheek");

                entity.Property(e => e.Apotheekid).HasColumnName("apotheekid");

                entity.Property(e => e.ActiefIngenomen).HasColumnName("actiefIngenomen");

                entity.Property(e => e.Dosering)
                    .IsRequired()
                    .HasColumnName("dosering")
                    .HasMaxLength(50);

                entity.Property(e => e.Groep)
                    .IsRequired()
                    .HasColumnName("groep")
                    .HasMaxLength(50);

                entity.Property(e => e.InApotheek).HasColumnName("inApotheek");

                entity.Property(e => e.Medicijnid).HasColumnName("medicijnid");

                entity.Property(e => e.Opmerkingen)
                    .HasColumnName("opmerkingen")
                    .HasMaxLength(500);

                entity.Property(e => e.Patientid).HasColumnName("patientid");

                entity.HasOne(d => d.Medicijn)
                    .WithMany(p => p.Persoonlijkeapotheek)
                    .HasForeignKey(d => d.Medicijnid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__persoonli__medic__70DDC3D8");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Persoonlijkeapotheek)
                    .HasForeignKey(d => d.Patientid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__persoonli__patie__6FE99F9F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

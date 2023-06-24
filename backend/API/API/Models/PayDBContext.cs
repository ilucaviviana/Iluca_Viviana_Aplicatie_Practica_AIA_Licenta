using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace API.Models
{
    public partial class PayDBContext : DbContext
    {
        public PayDBContext()
        {
        }

        public PayDBContext(DbContextOptions<PayDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartament> Apartament { get; set; }
        public virtual DbSet<Apometre> Apometre { get; set; }
        public virtual DbSet<Bloc> Bloc { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<Judet> Judet { get; set; }
        public virtual DbSet<Locatii> Locatii { get; set; }
        public virtual DbSet<Oras> Oras { get; set; }
        public virtual DbSet<Strada> Strada { get; set; }
        public virtual DbSet<Tarife> Tarife { get; set; }
        public virtual DbSet<Utilizator> Utilizator { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=PayDB; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartament>(entity =>
            {
                entity.HasKey(e => e.Idapartament)
                    .HasName("PK__Apartame__E10A290A1020E9EA");

                entity.Property(e => e.Idapartament).HasColumnName("IDApartament");
            });

            modelBuilder.Entity<Apometre>(entity =>
            {
                entity.HasKey(e => e.IdApometre)
                    .HasName("PK__Apometre__12F610C948212894");

                entity.Property(e => e.Ap1)
                    .HasColumnName("ap1")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ap2)
                    .HasColumnName("ap2")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ap3)
                    .HasColumnName("ap3")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ap4)
                    .HasColumnName("ap4")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TransmitereData).HasColumnType("date");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Apometre)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apometre__Iduser__01142BA1");
            });

            modelBuilder.Entity<Bloc>(entity =>
            {
                entity.HasKey(e => e.Idbloc)
                    .HasName("PK__Bloc__3CFB8BB234155D77");

                entity.Property(e => e.Idbloc).HasColumnName("IDBloc");

                entity.Property(e => e.NumarBloc)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("PK__Factura__50E7BAF157512CD6");

                entity.Property(e => e.AdminUsage).HasColumnType("decimal(20, 0)");

                entity.Property(e => e.ApaUsage).HasColumnType("decimal(20, 0)");

                entity.Property(e => e.CuratenieUsage).HasColumnType("decimal(20, 0)");

                entity.Property(e => e.IdUtilizator).HasColumnName("Id_utilizator");

                entity.Property(e => e.RetimUsage).HasColumnType("decimal(20, 0)");

                entity.Property(e => e.TotalAdmin).HasColumnType("decimal(20, 0)");

                entity.Property(e => e.TotalApa).HasColumnType("decimal(20, 0)");

                entity.Property(e => e.TotalCuratenie).HasColumnType("decimal(20, 0)");

                entity.Property(e => e.TotalRetim).HasColumnType("decimal(20, 0)");

                entity.Property(e => e.TransmitereData).HasColumnType("date");

                entity.HasOne(d => d.IdUtilizatorNavigation)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.IdUtilizator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Factura__Id_util__1EA48E88");
            });

            modelBuilder.Entity<Judet>(entity =>
            {
                entity.HasKey(e => e.Idjudet)
                    .HasName("PK__Judet__D2AAE0672BBB57F3");

                entity.Property(e => e.Idjudet).HasColumnName("IDJudet");

                entity.Property(e => e.NumeJudet)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Locatii>(entity =>
            {
                entity.HasKey(e => e.IdLocatii)
                    .HasName("PK__Locatii__9A1EDBC7C8E60F77");

                entity.Property(e => e.Apartament).HasColumnName("apartament");

                entity.Property(e => e.Bloc)
                    .HasColumnName("bloc")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Judet)
                    .HasColumnName("judet")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nrapometre).HasColumnName("nrapometre");

                entity.Property(e => e.Nrlocatari).HasColumnName("nrlocatari");

                entity.Property(e => e.Oras)
                    .HasColumnName("oras")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Strada)
                    .HasColumnName("strada")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Locatii)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Locatii__IdUser__787EE5A0");
            });

            modelBuilder.Entity<Oras>(entity =>
            {
                entity.HasKey(e => e.Idoras)
                    .HasName("PK__Oras__BA6D4AB46994EE18");

                entity.Property(e => e.Idoras).HasColumnName("IDOras");

                entity.Property(e => e.NumeOras)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Strada>(entity =>
            {
                entity.HasKey(e => e.Idstrada)
                    .HasName("PK__Strada__A66FDE6924C83BA8");

                entity.Property(e => e.Idstrada).HasColumnName("IDStrada");

                entity.Property(e => e.NumeStrada)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tarife>(entity =>
            {
                entity.HasKey(e => e.IdTarife)
                    .HasName("PK__Tarife__78F1A91178F6604D");

                entity.Property(e => e.PretAdmin)
                    .HasColumnName("pret_admin")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PretApa)
                    .HasColumnName("pret_apa")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PretCuratenie)
                    .HasColumnName("pret_curatenie")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PretRetim)
                    .HasColumnName("pret_retim")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TransmitereData).HasColumnType("date");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Tarife)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tarife__Id__160F4887");
            });

            modelBuilder.Entity<Utilizator>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cnp)
                    .HasColumnName("cnp")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Parola)
                    .HasColumnName("parola")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Prenume)
                    .HasColumnName("prenume")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Telefon)
                    .HasColumnName("telefon")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tip).HasColumnName("tip");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

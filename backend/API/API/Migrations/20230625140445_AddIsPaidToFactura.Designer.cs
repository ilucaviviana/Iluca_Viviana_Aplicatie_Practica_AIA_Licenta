﻿// <auto-generated />
using System;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(PayDBContext))]
    [Migration("20230625140445_AddIsPaidToFactura")]
    partial class AddIsPaidToFactura
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.Apartament", b =>
                {
                    b.Property<int>("Idapartament")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDApartament")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NumarApartament")
                        .HasColumnType("int");

                    b.HasKey("Idapartament")
                        .HasName("PK__Apartame__E10A290A1020E9EA");

                    b.ToTable("Apartament");
                });

            modelBuilder.Entity("API.Models.Apometre", b =>
                {
                    b.Property<int>("IdApometre")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ap1")
                        .HasColumnName("ap1")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("Ap2")
                        .HasColumnName("ap2")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("Ap3")
                        .HasColumnName("ap3")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("Ap4")
                        .HasColumnName("ap4")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<int>("Iduser")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TransmitereData")
                        .HasColumnType("date");

                    b.HasKey("IdApometre")
                        .HasName("PK__Apometre__12F610C948212894");

                    b.HasIndex("Iduser");

                    b.ToTable("Apometre");
                });

            modelBuilder.Entity("API.Models.Bloc", b =>
                {
                    b.Property<int>("Idbloc")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDBloc")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NumarBloc")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.HasKey("Idbloc")
                        .HasName("PK__Bloc__3CFB8BB234155D77");

                    b.ToTable("Bloc");
                });

            modelBuilder.Entity("API.Models.Factura", b =>
                {
                    b.Property<int>("IdFactura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("AdminUsage")
                        .HasColumnType("decimal(20, 0)");

                    b.Property<decimal?>("ApaUsage")
                        .HasColumnType("decimal(20, 0)");

                    b.Property<decimal?>("CuratenieUsage")
                        .HasColumnType("decimal(20, 0)");

                    b.Property<int>("IdUtilizator")
                        .HasColumnName("Id_utilizator")
                        .HasColumnType("int");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<decimal?>("RetimUsage")
                        .HasColumnType("decimal(20, 0)");

                    b.Property<decimal?>("TotalAdmin")
                        .HasColumnType("decimal(20, 0)");

                    b.Property<decimal?>("TotalApa")
                        .HasColumnType("decimal(20, 0)");

                    b.Property<decimal?>("TotalCuratenie")
                        .HasColumnType("decimal(20, 0)");

                    b.Property<decimal?>("TotalRetim")
                        .HasColumnType("decimal(20, 0)");

                    b.Property<DateTime?>("TransmitereData")
                        .HasColumnType("date");

                    b.HasKey("IdFactura")
                        .HasName("PK__Factura__50E7BAF157512CD6");

                    b.HasIndex("IdUtilizator");

                    b.ToTable("Factura");
                });

            modelBuilder.Entity("API.Models.Judet", b =>
                {
                    b.Property<int>("Idjudet")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDJudet")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NumeJudet")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Idjudet")
                        .HasName("PK__Judet__D2AAE0672BBB57F3");

                    b.ToTable("Judet");
                });

            modelBuilder.Entity("API.Models.Locatii", b =>
                {
                    b.Property<int>("IdLocatii")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Apartament")
                        .HasColumnName("apartament")
                        .HasColumnType("int");

                    b.Property<string>("Bloc")
                        .HasColumnName("bloc")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("Judet")
                        .HasColumnName("judet")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int?>("Nrapometre")
                        .HasColumnName("nrapometre")
                        .HasColumnType("int");

                    b.Property<int?>("Nrlocatari")
                        .HasColumnName("nrlocatari")
                        .HasColumnType("int");

                    b.Property<string>("Oras")
                        .HasColumnName("oras")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Strada")
                        .HasColumnName("strada")
                        .HasColumnType("varchar(60)")
                        .HasMaxLength(60)
                        .IsUnicode(false);

                    b.HasKey("IdLocatii")
                        .HasName("PK__Locatii__9A1EDBC7C8E60F77");

                    b.HasIndex("IdUser");

                    b.ToTable("Locatii");
                });

            modelBuilder.Entity("API.Models.Oras", b =>
                {
                    b.Property<int>("Idoras")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDOras")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NumeOras")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Idoras")
                        .HasName("PK__Oras__BA6D4AB46994EE18");

                    b.ToTable("Oras");
                });

            modelBuilder.Entity("API.Models.Strada", b =>
                {
                    b.Property<int>("Idstrada")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IDStrada")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NumeStrada")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Idstrada")
                        .HasName("PK__Strada__A66FDE6924C83BA8");

                    b.ToTable("Strada");
                });

            modelBuilder.Entity("API.Models.Tarife", b =>
                {
                    b.Property<int>("IdTarife")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("PretAdmin")
                        .HasColumnName("pret_admin")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("PretApa")
                        .HasColumnName("pret_apa")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("PretCuratenie")
                        .HasColumnName("pret_curatenie")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<string>("PretRetim")
                        .HasColumnName("pret_retim")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<DateTime?>("TransmitereData")
                        .HasColumnType("date");

                    b.HasKey("IdTarife")
                        .HasName("PK__Tarife__78F1A91178F6604D");

                    b.HasIndex("Id");

                    b.ToTable("Tarife");
                });

            modelBuilder.Entity("API.Models.Utilizator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cnp")
                        .HasColumnName("cnp")
                        .HasColumnType("varchar(13)")
                        .HasMaxLength(13)
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Nume")
                        .HasColumnName("nume")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Parola")
                        .HasColumnName("parola")
                        .HasColumnType("varchar(14)")
                        .HasMaxLength(14)
                        .IsUnicode(false);

                    b.Property<string>("Prenume")
                        .HasColumnName("prenume")
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Telefon")
                        .HasColumnName("telefon")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10)
                        .IsUnicode(false);

                    b.Property<int?>("Tip")
                        .HasColumnName("tip")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Utilizator");
                });

            modelBuilder.Entity("API.Models.Apometre", b =>
                {
                    b.HasOne("API.Models.Utilizator", "IduserNavigation")
                        .WithMany("Apometre")
                        .HasForeignKey("Iduser")
                        .HasConstraintName("FK__Apometre__Iduser__01142BA1")
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.Factura", b =>
                {
                    b.HasOne("API.Models.Utilizator", "IdUtilizatorNavigation")
                        .WithMany("Factura")
                        .HasForeignKey("IdUtilizator")
                        .HasConstraintName("FK__Factura__Id_util__1EA48E88")
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.Locatii", b =>
                {
                    b.HasOne("API.Models.Utilizator", "IdUserNavigation")
                        .WithMany("Locatii")
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK__Locatii__IdUser__787EE5A0")
                        .IsRequired();
                });

            modelBuilder.Entity("API.Models.Tarife", b =>
                {
                    b.HasOne("API.Models.Utilizator", "IdNavigation")
                        .WithMany("Tarife")
                        .HasForeignKey("Id")
                        .HasConstraintName("FK__Tarife__Id__160F4887")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

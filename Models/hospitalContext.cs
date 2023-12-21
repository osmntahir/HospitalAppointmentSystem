using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HospitalAppointmentSystem.Models
{
    public partial class hospitalContext : DbContext
    {
        public hospitalContext()
        {
        }

        public hospitalContext(DbContextOptions<hospitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CalismaGun> CalismaGuns { get; set; } = null!;
        public virtual DbSet<Doktor> Doktors { get; set; } = null!;
        public virtual DbSet<Kullanici> Hasta { get; set; } = null!;
        public virtual DbSet<Poliklinik> Polikliniks { get; set; } = null!;
        public virtual DbSet<Randevu> Randevus { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalismaGun>(entity =>
            {
                entity.ToTable("CalismaGun");

                entity.Property(e => e.CalismaGunId)
                    .ValueGeneratedNever()
                    .HasColumnName("CalismaGunID");

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.Property(e => e.Gun)
                    .HasColumnType("date")
                    .HasColumnName("GUN");

                entity.HasOne(d => d.Doktor)
                    .WithMany(p => p.CalismaGuns)
                    .HasForeignKey(d => d.DoktorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__CalismaGu__Dokto__1A34DF26");
            });

            modelBuilder.Entity<Doktor>(entity =>
            {
                entity.ToTable("Doktor");

                entity.Property(e => e.DoktorId)
                    .ValueGeneratedNever()
                    .HasColumnName("DoktorID");

                entity.Property(e => e.Adi).HasMaxLength(255);

                entity.Property(e => e.Adres).HasMaxLength(500);

                entity.Property(e => e.Brans).HasMaxLength(255);

                entity.Property(e => e.Cinsiyet).HasMaxLength(10);

                entity.Property(e => e.Maas).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mail).HasMaxLength(255);

                entity.Property(e => e.PoliklinikId).HasColumnName("PoliklinikID");

                entity.Property(e => e.Soyadi).HasMaxLength(255);

                entity.Property(e => e.Telefon).HasMaxLength(20);

                entity.HasOne(d => d.Poliklinik)
                    .WithMany(p => p.Doktors)
                    .HasForeignKey(d => d.PoliklinikId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Doktor__Poliklin__1758727B");
            });

            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.HasKey(e => e.KullaniciId)
                    .HasName("PK__Hasta__114C5CABA617346D");

                entity.Property(e => e.KullaniciId)
                    .ValueGeneratedNever()
                    .HasColumnName("HastaID");

                entity.Property(e => e.Adi).HasMaxLength(255);

                entity.Property(e => e.Cinsiyet).HasMaxLength(10);

                entity.Property(e => e.DogumTarihi).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.KullaniciRol)
                    .HasMaxLength(2)
                    .HasDefaultValueSql("('U')");

                entity.Property(e => e.Sifre)
                    .HasMaxLength(50)
                    .HasColumnName("sifre");

                entity.Property(e => e.Soyadi).HasMaxLength(255);

                entity.Property(e => e.TelefonNumarasi).HasMaxLength(20);
            });

            modelBuilder.Entity<Poliklinik>(entity =>
            {
                entity.ToTable("Poliklinik");

                entity.Property(e => e.PoliklinikId)
                    .ValueGeneratedNever()
                    .HasColumnName("PoliklinikID");

                entity.Property(e => e.Adi).HasMaxLength(255);
            });

            modelBuilder.Entity<Randevu>(entity =>
            {
                entity.ToTable("Randevu");

                entity.Property(e => e.RandevuId)
                    .ValueGeneratedNever()
                    .HasColumnName("RandevuID");

                entity.Property(e => e.Aciklama).HasMaxLength(500);

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.Property(e => e.RandevuTarihiSaat).HasColumnType("datetime");

                entity.HasOne(d => d.Doktor)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.DoktorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Randevu__DoktorI__1EF99443");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

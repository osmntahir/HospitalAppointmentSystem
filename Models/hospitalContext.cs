﻿using System;
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
        public virtual DbSet<Kullanici> Kullanicis { get; set; } = null!;
        public virtual DbSet<Poliklinik> Polikliniks { get; set; } = null!;
        public virtual DbSet<Randevu> Randevus { get; set; } = null!;
        public virtual DbSet<Saatler> Saatlers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-39QEI7I; Database=hospital; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalismaGun>(entity =>
            {
                entity.ToTable("CalismaGun");

                entity.Property(e => e.CalismaGunId).HasColumnName("CalismaGunID");

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.Property(e => e.Gun)
                    .HasColumnType("date")
                    .HasColumnName("GUN");

                entity.HasOne(d => d.Doktor)
                    .WithMany(p => p.CalismaGuns)
                    .HasForeignKey(d => d.DoktorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__CalismaGu__Dokto__6EE06CCD");
            });

            modelBuilder.Entity<Doktor>(entity =>
            {
                entity.ToTable("Doktor");

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

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
                    .HasConstraintName("FK__Doktor__Poliklin__6C040022");
            });

            modelBuilder.Entity<Kullanici>(entity =>
            {
                entity.ToTable("Kullanici");

                entity.Property(e => e.Adi).HasMaxLength(255);

                entity.Property(e => e.Cinsiyet).HasMaxLength(10);

                entity.Property(e => e.DogumTarihi).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.KullaniciRole)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('U')")
                    .IsFixedLength();

                entity.Property(e => e.Sifre)
                    .HasMaxLength(50)
                    .HasColumnName("sifre");

                entity.Property(e => e.Soyadi).HasMaxLength(255);

                entity.Property(e => e.TelefonNumarasi).HasMaxLength(20);
            });

            modelBuilder.Entity<Poliklinik>(entity =>
            {
                entity.ToTable("Poliklinik");

                entity.Property(e => e.PoliklinikId).HasColumnName("PoliklinikID");

                entity.Property(e => e.Adi).HasMaxLength(255);
            });

            modelBuilder.Entity<Randevu>(entity =>
            {
                entity.ToTable("Randevu");

                entity.Property(e => e.RandevuId).HasColumnName("RandevuID");

                entity.Property(e => e.Aciklama).HasMaxLength(500);

                entity.Property(e => e.CalismaGunId).HasColumnName("CalismaGunID");

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.Property(e => e.PoliklinikId).HasColumnName("PoliklinikID");

                entity.Property(e => e.SaatId).HasColumnName("SaatID");

                entity.HasOne(d => d.CalismaGun)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.CalismaGunId)
                    .HasConstraintName("FK_Randevu_CalismaGun");

                entity.HasOne(d => d.Doktor)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.DoktorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Randevu__DoktorI__01F34141");

                entity.HasOne(d => d.Kullanici)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.KullaniciId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Randevu__Kullani__02E7657A");

                entity.HasOne(d => d.Poliklinik)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.PoliklinikId)
                    .HasConstraintName("FK__Randevu__Polikli__03DB89B3");

                entity.HasOne(d => d.Saat)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.SaatId)
                    .HasConstraintName("FK_Randevu_Saatler");
            });

            modelBuilder.Entity<Saatler>(entity =>
            {
                entity.HasKey(e => e.SaatId)
                    .HasName("PK__Saatler__A82183CFE697CE3C");

                entity.ToTable("Saatler");

                entity.Property(e => e.SaatId).HasColumnName("SaatID");

                entity.Property(e => e.CalismaGunId).HasColumnName("CalismaGunID");

                entity.Property(e => e.Secilebilir)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CalismaGun)
                    .WithMany(p => p.Saatlers)
                    .HasForeignKey(d => d.CalismaGunId)
                    .HasConstraintName("FK__Saatler__Calisma__71BCD978");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

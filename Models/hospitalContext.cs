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
                    .HasConstraintName("FK__CalismaGu__Dokto__43F60EC8");
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
                    .HasConstraintName("FK__Doktor__Poliklin__4119A21D");
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

                entity.Property(e => e.PoliklinikId)
                    .ValueGeneratedNever()
                    .HasColumnName("PoliklinikID");

                entity.Property(e => e.Adi).HasMaxLength(255);
            });

            modelBuilder.Entity<Randevu>(entity =>
            {
                entity.ToTable("Randevu");

                entity.Property(e => e.RandevuId).HasColumnName("RandevuID");

                entity.Property(e => e.Aciklama).HasMaxLength(500);

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.Property(e => e.PoliklinikId).HasColumnName("PoliklinikID");

                entity.Property(e => e.RandevuTarihiSaat).HasColumnType("datetime");

                entity.HasOne(d => d.Doktor)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.DoktorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Randevu__DoktorI__505BE5AD");

                entity.HasOne(d => d.Kullanici)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.KullaniciId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Randevu__Kullani__515009E6");

                entity.HasOne(d => d.Poliklinik)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.PoliklinikId)
                    .HasConstraintName("FK__Randevu__Polikli__52442E1F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

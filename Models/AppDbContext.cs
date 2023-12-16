using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HospitalAppointmentSystem.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bolum> Bolums { get; set; } = null!;
        public virtual DbSet<CalismaGun> CalismaGuns { get; set; } = null!;
        public virtual DbSet<Doktor> Doktors { get; set; } = null!;
        public virtual DbSet<Hastum> Hasta { get; set; } = null!;
        public virtual DbSet<Poliklinik> Polikliniks { get; set; } = null!;
        public virtual DbSet<Randevu> Randevus { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-39QEI7I;Database=hospital;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bolum>(entity =>
            {
                entity.ToTable("Bolum");

                entity.Property(e => e.BolumId)
                    .ValueGeneratedNever()
                    .HasColumnName("BolumID");

                entity.Property(e => e.BolumAdi).HasMaxLength(255);
            });

            modelBuilder.Entity<CalismaGun>(entity =>
            {
                entity.ToTable("CalismaGun");

                entity.Property(e => e.CalismaGunId).HasColumnName("CalismaGunID");

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.HasOne(d => d.Doktor)
                    .WithMany(p => p.CalismaGuns)
                    .HasForeignKey(d => d.DoktorId)
                    .HasConstraintName("FK__CalismaGu__Dokto__03F0984C");
            });

            modelBuilder.Entity<Doktor>(entity =>
            {
                entity.ToTable("Doktor");

                entity.Property(e => e.DoktorId)
                    .ValueGeneratedNever()
                    .HasColumnName("DoktorID");

                entity.Property(e => e.BolumId).HasColumnName("BolumID");

                entity.Property(e => e.DoktorAdi).HasMaxLength(255);

                entity.Property(e => e.PoliklinikId).HasColumnName("PoliklinikID");

                entity.Property(e => e.UzmanlikAlani).HasMaxLength(255);

                entity.HasOne(d => d.Bolum)
                    .WithMany(p => p.Doktors)
                    .HasForeignKey(d => d.BolumId)
                    .HasConstraintName("FK__Doktor__BolumID__6A30C649");

                entity.HasOne(d => d.Poliklinik)
                    .WithMany(p => p.Doktors)
                    .HasForeignKey(d => d.PoliklinikId)
                    .HasConstraintName("FK__Doktor__Poliklin__6B24EA82");
            });

            modelBuilder.Entity<Hastum>(entity =>
            {
                entity.HasKey(e => e.HastaId)
                    .HasName("PK__Hasta__114C5CAB1CF64D3B");

                entity.Property(e => e.HastaId)
                    .ValueGeneratedNever()
                    .HasColumnName("HastaID");

                entity.Property(e => e.Ad).HasMaxLength(255);

                entity.Property(e => e.Cinsiyet).HasMaxLength(10);

                entity.Property(e => e.DogumTarihi).HasColumnType("date");

                entity.Property(e => e.Soyad).HasMaxLength(255);

                entity.Property(e => e.TelefonNumarasi).HasMaxLength(20);
            });

            modelBuilder.Entity<Poliklinik>(entity =>
            {
                entity.ToTable("Poliklinik");

                entity.Property(e => e.PoliklinikId)
                    .ValueGeneratedNever()
                    .HasColumnName("PoliklinikID");

                entity.Property(e => e.BolumId).HasColumnName("BolumID");

                entity.Property(e => e.PoliklinikAdi).HasMaxLength(255);

                entity.HasOne(d => d.Bolum)
                    .WithMany(p => p.Polikliniks)
                    .HasForeignKey(d => d.BolumId)
                    .HasConstraintName("FK__Poliklini__Bolum__6754599E");
            });

            modelBuilder.Entity<Randevu>(entity =>
            {
                entity.ToTable("Randevu");

                entity.Property(e => e.RandevuId)
                    .ValueGeneratedNever()
                    .HasColumnName("RandevuID");

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.Property(e => e.HastaId).HasColumnName("HastaID");

                entity.Property(e => e.RandevuTarihiSaat).HasColumnType("datetime");

                entity.HasOne(d => d.Doktor)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.DoktorId)
                    .HasConstraintName("FK__Randevu__DoktorI__72C60C4A");

                entity.HasOne(d => d.Hasta)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.HastaId)
                    .HasConstraintName("FK__Randevu__HastaID__73BA3083");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

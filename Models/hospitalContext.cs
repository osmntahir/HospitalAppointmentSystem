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

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Bolum> Bolums { get; set; } = null!;
        public virtual DbSet<CalismaGun> CalismaGuns { get; set; } = null!;
        public virtual DbSet<Doktor> Doktors { get; set; } = null!;
        public virtual DbSet<HastaLogin> HastaLogins { get; set; } = null!;
        public virtual DbSet<Hastum> Hasta { get; set; } = null!;
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
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(255);
            });

            modelBuilder.Entity<Bolum>(entity =>
            {
                entity.ToTable("Bolum");

                entity.Property(e => e.BolumId).HasColumnName("BolumID");

                entity.Property(e => e.Aciklama).HasMaxLength(500);

                entity.Property(e => e.BolumAdi).HasMaxLength(255);
            });

            modelBuilder.Entity<CalismaGun>(entity =>
            {
                entity.ToTable("CalismaGun");

                entity.Property(e => e.CalismaGunId).HasColumnName("CalismaGunID");

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.Property(e => e.Gun)
                    .HasColumnType("date")
                    .HasColumnName("GUN");
            });

            modelBuilder.Entity<Doktor>(entity =>
            {
                entity.ToTable("Doktor");

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.Property(e => e.Adres).HasMaxLength(500);

                entity.Property(e => e.Brans).HasMaxLength(255);

                entity.Property(e => e.Cinsiyet).HasMaxLength(10);

                entity.Property(e => e.DoktorAdi).HasMaxLength(255);

                entity.Property(e => e.Maas).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mail).HasMaxLength(255);

                entity.Property(e => e.PoliklinikId).HasColumnName("PoliklinikID");

                entity.Property(e => e.Soyadi).HasMaxLength(255);

                entity.Property(e => e.Telefon).HasMaxLength(20);

                entity.HasOne(d => d.Poliklinik)
                    .WithMany(p => p.Doktors)
                    .HasForeignKey(d => d.PoliklinikId)
                    .HasConstraintName("FK__Doktor__Poliklin__24285DB4");
            });

            modelBuilder.Entity<HastaLogin>(entity =>
            {
                entity.HasKey(e => e.HastaId)
                    .HasName("PK__HastaLog__114C5CABCD24457C");

                entity.ToTable("HastaLogin");

                entity.Property(e => e.HastaId)
                    .ValueGeneratedNever()
                    .HasColumnName("HastaID");

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.HasOne(d => d.Hasta)
                    .WithOne(p => p.HastaLogin)
                    .HasForeignKey<HastaLogin>(d => d.HastaId)
                    .HasConstraintName("FK_HastaLogin_Hasta");
            });

            modelBuilder.Entity<Hastum>(entity =>
            {
                entity.HasKey(e => e.HastaId)
                    .HasName("PK__Hasta__114C5CAB075BC7E4");

                entity.Property(e => e.HastaId).HasColumnName("HastaID");

                entity.Property(e => e.Ad).HasMaxLength(255);

                entity.Property(e => e.Cinsiyet).HasMaxLength(10);

                entity.Property(e => e.DogumTarihi).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Soyad).HasMaxLength(255);

                entity.Property(e => e.TelefonNumarasi).HasMaxLength(20);
            });

            modelBuilder.Entity<Poliklinik>(entity =>
            {
                entity.ToTable("Poliklinik");

                entity.Property(e => e.PoliklinikId).HasColumnName("PoliklinikID");

                entity.Property(e => e.BolumId).HasColumnName("BolumID");

                entity.Property(e => e.PoliklinikAdi).HasMaxLength(255);

                entity.Property(e => e.Telefon).HasMaxLength(20);

                entity.HasOne(d => d.Bolum)
                    .WithMany(p => p.Polikliniks)
                    .HasForeignKey(d => d.BolumId)
                    .HasConstraintName("FK__Poliklini__Bolum__0A688BB1");
            });

            modelBuilder.Entity<Randevu>(entity =>
            {
                entity.ToTable("Randevu");

                entity.Property(e => e.RandevuId).HasColumnName("RandevuID");

                entity.Property(e => e.Aciklama).HasMaxLength(500);

                entity.Property(e => e.DoktorId).HasColumnName("DoktorID");

                entity.Property(e => e.HastaId).HasColumnName("HastaID");

                entity.Property(e => e.RandevuTarihiSaat).HasColumnType("datetime");

                entity.HasOne(d => d.Hasta)
                    .WithMany(p => p.Randevus)
                    .HasForeignKey(d => d.HastaId)
                    .HasConstraintName("FK__Randevu__HastaID__19AACF41");
            });

            modelBuilder.Entity<Saatler>(entity =>
            {
                entity.HasKey(e => e.SaatId)
                    .HasName("PK__Saatler__A82183CF0AB8A1CD");

                entity.ToTable("Saatler");

                entity.Property(e => e.SaatId).HasColumnName("SaatID");

                entity.Property(e => e.CalismaGunId).HasColumnName("CalismaGunID");

                entity.Property(e => e.Secilebilir)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CalismaGun)
                    .WithMany(p => p.Saatlers)
                    .HasForeignKey(d => d.CalismaGunId)
                    .HasConstraintName("FK__Saatler__Calisma__12FDD1B2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

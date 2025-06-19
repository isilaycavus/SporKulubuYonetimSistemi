using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SporKulubuYonetimSistemi.Models;

public partial class SporKulubuContext : DbContext
{
    public SporKulubuContext()
    {
    }

    public SporKulubuContext(DbContextOptions<SporKulubuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branslar> Branslars { get; set; }

    public virtual DbSet<Duyurular> Duyurulars { get; set; }

    public virtual DbSet<GirisKayitlari> GirisKayitlaris { get; set; }

    public virtual DbSet<Kartlar> Kartlars { get; set; }

    public virtual DbSet<KullaniciBran> KullaniciBrans { get; set; }

    public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }

    public virtual DbSet<Odemeler> Odemelers { get; set; }

    public virtual DbSet<Roller> Rollers { get; set; }

    public virtual DbSet<UyelikPlanlari> UyelikPlanlaris { get; set; }

    public virtual DbSet<Uyelikler> Uyeliklers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=SporKulubuDB;Username=postgres;Password=Asdqwe");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branslar>(entity =>
        {
            entity.HasKey(e => e.BransId).HasName("branslar_pkey");

            entity.ToTable("branslar");

            entity.Property(e => e.BransId).HasColumnName("brans_id");
            entity.Property(e => e.BransAdi)
                .HasMaxLength(50)
                .HasColumnName("brans_adi");
        });

        modelBuilder.Entity<Duyurular>(entity =>
        {
            entity.HasKey(e => e.DuyuruId).HasName("duyurular_pkey");

            entity.ToTable("duyurular");

            entity.Property(e => e.DuyuruId).HasColumnName("duyuru_id");
            entity.Property(e => e.Baslik)
                .HasMaxLength(100)
                .HasColumnName("baslik");
            entity.Property(e => e.Icerik).HasColumnName("icerik");
            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");
            entity.Property(e => e.Tarih)
                .HasDefaultValueSql("now()")
                .HasColumnName("tarih");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Duyurulars)
                .HasForeignKey(d => d.KullaniciId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("duyurular_kullanici_id_fkey");
        });

        modelBuilder.Entity<GirisKayitlari>(entity =>
        {
            entity.HasKey(e => e.KayitId).HasName("giris_kayitlari_pkey");

            entity.ToTable("giris_kayitlari");

            entity.Property(e => e.KayitId).HasColumnName("kayit_id");
            entity.Property(e => e.GirisTarihi)
                .HasDefaultValueSql("now()")
                .HasColumnName("giris_tarihi");
            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.GirisKayitlaris)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("giris_kayitlari_kullanici_id_fkey");
        });

        modelBuilder.Entity<Kartlar>(entity =>
        {
            entity.HasKey(e => e.KartId).HasName("kartlar_pkey");

            entity.ToTable("kartlar");

            entity.Property(e => e.KartId).HasColumnName("kart_id");
            entity.Property(e => e.KartNumarasi)
                .HasMaxLength(20)
                .HasColumnName("kart_numarasi");
            entity.Property(e => e.KayitTarihi)
                .HasDefaultValueSql("now()")
                .HasColumnName("kayit_tarihi");
            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Kartlars)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("kartlar_kullanici_id_fkey");
        });

        modelBuilder.Entity<KullaniciBran>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("kullanici_brans_pkey");

            entity.ToTable("kullanici_brans");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BransId).HasColumnName("brans_id");
            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");

            entity.HasOne(d => d.Brans).WithMany(p => p.KullaniciBrans)
                .HasForeignKey(d => d.BransId)
                .HasConstraintName("kullanici_brans_brans_id_fkey");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.KullaniciBrans)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("kullanici_brans_kullanici_id_fkey");
        });

        modelBuilder.Entity<Kullanicilar>(entity =>
        {
            entity.HasKey(e => e.KullaniciId).HasName("kullanicilar_pkey");

            entity.ToTable("kullanicilar");

            entity.HasIndex(e => e.Email, "kullanicilar_email_key").IsUnique();

            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");
            entity.Property(e => e.AdSoyad)
                .HasMaxLength(100)
                .HasColumnName("ad_soyad");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.Sifre).HasColumnName("sifre");

            entity.HasOne(d => d.Rol).WithMany(p => p.Kullanicilars)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("kullanicilar_rol_id_fkey");
        });

        modelBuilder.Entity<Odemeler>(entity =>
        {
            entity.HasKey(e => e.OdemeId).HasName("odemeler_pkey");

            entity.ToTable("odemeler");

            entity.Property(e => e.OdemeId).HasColumnName("odeme_id");
            entity.Property(e => e.KartId).HasColumnName("kart_id");
            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");
            entity.Property(e => e.OdemeTarihi)
                .HasDefaultValueSql("now()")
                .HasColumnName("odeme_tarihi");
            entity.Property(e => e.Tutar)
                .HasPrecision(10, 2)
                .HasColumnName("tutar");
            entity.Property(e => e.UyelikId).HasColumnName("uyelik_id");

            entity.HasOne(d => d.Kart).WithMany(p => p.Odemelers)
                .HasForeignKey(d => d.KartId)
                .HasConstraintName("odemeler_kart_id_fkey");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Odemelers)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("odemeler_kullanici_id_fkey");

            entity.HasOne(d => d.Uyelik).WithMany(p => p.Odemelers)
                .HasForeignKey(d => d.UyelikId)
                .HasConstraintName("odemeler_uyelik_id_fkey");
        });

        modelBuilder.Entity<Roller>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("roller_pkey");

            entity.ToTable("roller");

            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.RolAdi)
                .HasMaxLength(50)
                .HasColumnName("rol_adi");
        });

        modelBuilder.Entity<UyelikPlanlari>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("uyelik_planlari_pkey");

            entity.ToTable("uyelik_planlari");

            entity.HasIndex(e => e.PlanAdi, "uyelik_planlari_plan_adi_key").IsUnique();

            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.GirisUcreti)
                .HasPrecision(10, 2)
                .HasColumnName("giris_ucreti");
            entity.Property(e => e.PlanAdi)
                .HasMaxLength(50)
                .HasColumnName("plan_adi");
            entity.Property(e => e.ToplamGirisHakki).HasColumnName("toplam_giris_hakki");
        });

        modelBuilder.Entity<Uyelikler>(entity =>
        {
            entity.HasKey(e => e.UyelikId).HasName("uyelikler_pkey");

            entity.ToTable("uyelikler");

            entity.Property(e => e.UyelikId).HasColumnName("uyelik_id");
            entity.Property(e => e.Bakiye)
                .HasPrecision(10, 2)
                .HasColumnName("bakiye");
            entity.Property(e => e.BaslangicTarihi).HasColumnName("baslangic_tarihi");
            entity.Property(e => e.BitisTarihi).HasColumnName("bitis_tarihi");
            entity.Property(e => e.KalanGirisHakki).HasColumnName("kalan_giris_hakki");
            entity.Property(e => e.KullaniciId).HasColumnName("kullanici_id");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.Uyeliklers)
                .HasForeignKey(d => d.KullaniciId)
                .HasConstraintName("uyelikler_kullanici_id_fkey");

            entity.HasOne(d => d.Plan).WithMany(p => p.Uyeliklers)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("uyelikler_plan_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

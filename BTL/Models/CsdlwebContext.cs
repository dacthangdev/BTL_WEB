using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTL.Models;

public partial class CsdlwebContext : DbContext
{
    public CsdlwebContext()
    {
    }

    public CsdlwebContext(DbContextOptions<CsdlwebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BanAn> BanAns { get; set; }

    public virtual DbSet<ChitietHdb> ChitietHdbs { get; set; }

    public virtual DbSet<ChitietHdn> ChitietHdns { get; set; }

    public virtual DbSet<ChitietHdx> ChitietHdxes { get; set; }

    public virtual DbSet<ChitietTtb> ChitietTtbs { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<DatBan> DatBans { get; set; }

    public virtual DbSet<HangHoa> HangHoas { get; set; }

    public virtual DbSet<HoaDonBan> HoaDonBans { get; set; }

    public virtual DbSet<HoaDonNhap> HoaDonNhaps { get; set; }

    public virtual DbSet<HoaDonXuat> HoaDonXuats { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<LoaiMonAn> LoaiMonAns { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhongQl> PhongQls { get; set; }

    public virtual DbSet<Tang> Tangs { get; set; }

    public virtual DbSet<TinhTrang> TinhTrangs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DARKMOON\\DACTHANG;Initial Catalog=CSDLWeb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BanAn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BanAn__3214EC273AAA1F1E");

            entity.ToTable("BanAn");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdTang).HasColumnName("ID_Tang");
            entity.Property(e => e.Slghe).HasColumnName("SLGhe");

            entity.HasOne(d => d.IdTangNavigation).WithMany(p => p.BanAns)
                .HasForeignKey(d => d.IdTang)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BanAn__ID_Tang__3B75D760");
        });

        modelBuilder.Entity<ChitietHdb>(entity =>
        {
            entity.HasKey(e => new { e.IdMenu, e.IdHdb }).HasName("PK__ChitietH__DDC0A6C89A6B6075");

            entity.ToTable("ChitietHDB");

            entity.Property(e => e.IdMenu).HasColumnName("ID_Menu");
            entity.Property(e => e.IdHdb).HasColumnName("ID_HDB");
            entity.Property(e => e.Sl).HasColumnName("SL");

            entity.HasOne(d => d.IdHdbNavigation).WithMany(p => p.ChitietHdbs)
                .HasForeignKey(d => d.IdHdb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietHD__ID_HD__59063A47");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.ChitietHdbs)
                .HasForeignKey(d => d.IdMenu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietHD__ID_Me__5812160E");
        });

        modelBuilder.Entity<ChitietHdn>(entity =>
        {
            entity.HasKey(e => new { e.IdHh, e.IdHdn }).HasName("PK__ChitietH__E98A33FE043DD2AB");

            entity.ToTable("ChitietHDN");

            entity.Property(e => e.IdHh).HasColumnName("ID_HH");
            entity.Property(e => e.IdHdn).HasColumnName("ID_HDN");
            entity.Property(e => e.Sl).HasColumnName("SL");

            entity.HasOne(d => d.IdHdnNavigation).WithMany(p => p.ChitietHdns)
                .HasForeignKey(d => d.IdHdn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietHD__ID_HD__4CA06362");

            entity.HasOne(d => d.IdHhNavigation).WithMany(p => p.ChitietHdns)
                .HasForeignKey(d => d.IdHh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietHD__ID_HH__4BAC3F29");
        });

        modelBuilder.Entity<ChitietHdx>(entity =>
        {
            entity.HasKey(e => new { e.IdHh, e.IdHdx }).HasName("PK__ChitietH__898A33FDD88DB869");

            entity.ToTable("ChitietHDX");

            entity.Property(e => e.IdHh).HasColumnName("ID_HH");
            entity.Property(e => e.IdHdx).HasColumnName("ID_HDX");
            entity.Property(e => e.Sl).HasColumnName("SL");

            entity.HasOne(d => d.IdHdxNavigation).WithMany(p => p.ChitietHdxes)
                .HasForeignKey(d => d.IdHdx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietHD__ID_HD__5070F446");

            entity.HasOne(d => d.IdHhNavigation).WithMany(p => p.ChitietHdxes)
                .HasForeignKey(d => d.IdHh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietHD__ID_HH__4F7CD00D");
        });

        modelBuilder.Entity<ChitietTtb>(entity =>
        {
            entity.HasKey(e => new { e.IdBan, e.IdTt }).HasName("PK__ChitietT__2C9DBFE35383CF9F");

            entity.ToTable("ChitietTTB");

            entity.Property(e => e.IdBan).HasColumnName("ID_Ban");
            entity.Property(e => e.IdTt).HasColumnName("ID_TT");
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");

            entity.HasOne(d => d.IdBanNavigation).WithMany(p => p.ChitietTtbs)
                .HasForeignKey(d => d.IdBan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietTT__ID_Ba__47DBAE45");

            entity.HasOne(d => d.IdTtNavigation).WithMany(p => p.ChitietTtbs)
                .HasForeignKey(d => d.IdTt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChitietTT__ID_TT__48CFD27E");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChucVu__3214EC27FC237059");

            entity.ToTable("ChucVu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Hsl).HasColumnName("HSL");
            entity.Property(e => e.TenCv)
                .HasMaxLength(50)
                .HasColumnName("TenCV");
        });

        modelBuilder.Entity<DatBan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DatBan__3214EC270B87AB30");
            entity.ToTable("DatBan");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Sl)
            .HasMaxLength(10)
            .HasColumnName("Sl");
            entity.Property(e => e.NgayDat).HasColumnType("date");
            entity.Property(e => e.Name_KH)
               .HasMaxLength(50)
               .HasColumnName("Name_KH");
            entity.Property(e => e.Email)
               .HasMaxLength(50)
               .HasColumnName("Email");
            entity.Property(e => e.Phone)
               .HasMaxLength(50)
               .HasColumnName("Phone");
            entity.Property(e => e.Gio_Nhan)
               .HasMaxLength(50)
               .HasColumnName("Gio_Nhan");

        });

        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HangHoa__3214EC273040F75A");

            entity.ToTable("HangHoa");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdNhaCc).HasColumnName("ID_NhaCC");
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");
            entity.Property(e => e.Sl).HasColumnName("SL");
            entity.Property(e => e.TenHh)
                .HasMaxLength(50)
                .HasColumnName("TenHH");

            entity.HasOne(d => d.IdNhaCcNavigation).WithMany(p => p.HangHoas)
                .HasForeignKey(d => d.IdNhaCc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HangHoa__ID_NhaC__2E1BDC42");
        });

        modelBuilder.Entity<HoaDonBan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HoaDonBa__3214EC272AB878E1");

            entity.ToTable("HoaDonBan");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdBan).HasColumnName("ID_Ban");
            entity.Property(e => e.IdKh).HasColumnName("ID_KH");
            entity.Property(e => e.IdNv).HasColumnName("ID_NV");
            entity.Property(e => e.NgayXuat).HasColumnType("datetime");
            entity.Property(e => e.SoHd)
                .HasMaxLength(50)
                .HasColumnName("SoHD");

            entity.HasOne(d => d.IdBanNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.IdBan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDonBan__ID_Ba__5535A963");

            entity.HasOne(d => d.IdKhNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.IdKh)
                .HasConstraintName("FK__HoaDonBan__ID_KH__5441852A");

            entity.HasOne(d => d.IdNvNavigation).WithMany(p => p.HoaDonBans)
                .HasForeignKey(d => d.IdNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDonBan__ID_NV__534D60F1");
        });

        modelBuilder.Entity<HoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HoaDonNh__3214EC27CA99849A");

            entity.ToTable("HoaDonNhap");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdNv).HasColumnName("ID_NV");
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");

            entity.HasOne(d => d.IdNvNavigation).WithMany(p => p.HoaDonNhaps)
                .HasForeignKey(d => d.IdNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDonNha__ID_NV__3E52440B");
        });

        modelBuilder.Entity<HoaDonXuat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HoaDonXu__3214EC27CEB1A22E");

            entity.ToTable("HoaDonXuat");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdNv).HasColumnName("ID_NV");
            entity.Property(e => e.NgayXuat).HasColumnType("datetime");

            entity.HasOne(d => d.IdNvNavigation).WithMany(p => p.HoaDonXuats)
                .HasForeignKey(d => d.IdNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HoaDonXua__ID_NV__412EB0B6");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KhachHan__3214EC27336EA07E");

            entity.ToTable("KhachHang");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.Sdt)
                .HasMaxLength(50)
                .HasColumnName("SDT");
        });

        modelBuilder.Entity<LoaiMonAn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoaiMonA__3214EC277D095F78");

            entity.ToTable("LoaiMonAn");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TenLoai).HasMaxLength(50);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menu__3214EC2719F4317D");

            entity.ToTable("Menu");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Anh).HasMaxLength(50);
            entity.Property(e => e.IdLoai).HasColumnName("ID_Loai");
            entity.Property(e => e.TenMon).HasMaxLength(50);

            entity.HasOne(d => d.IdLoaiNavigation).WithMany(p => p.Menus)
                .HasForeignKey(d => d.IdLoai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Menu__ID_Loai__38996AB5");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NhaCungC__3214EC27AAA67FA1");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(50)
                .HasColumnName("TenNCC");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NhanVien__3214EC271DE883C7");

            entity.ToTable("NhanVien");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdCv).HasColumnName("ID_CV");
            entity.Property(e => e.IdPhong).HasColumnName("ID_Phong");
            entity.Property(e => e.LuongCb).HasColumnName("LuongCB");
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");

            entity.HasOne(d => d.IdCvNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.IdCv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__ID_CV__35BCFE0A");

            entity.HasOne(d => d.IdPhongNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.IdPhong)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NhanVien__ID_Pho__34C8D9D1");
        });

        modelBuilder.Entity<PhongQl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhongQL__3214EC275D9E75CB");

            entity.ToTable("PhongQL");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TenPhong).HasMaxLength(50);
        });

        modelBuilder.Entity<Tang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tang__3214EC2751379587");

            entity.ToTable("Tang");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TenTang).HasMaxLength(50);
        });

        modelBuilder.Entity<TinhTrang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TinhTran__3214EC27967DD40B");

            entity.ToTable("TinhTrang");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TrangThat).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

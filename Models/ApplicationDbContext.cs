using Microsoft.EntityFrameworkCore;

using AutoAPI;

using coreWeb.Models.Entities;


namespace coreWeb.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //identity
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionRole> PermissionRole { get; set; }
        
        //DichVu
        public DbSet<DichVu> DichVu { get; set; }

        //States
        public DbSet<States> States { get; set; }

        //Status
        public DbSet<Status> Status { get; set; }

        //NhanSu
        public DbSet<NhanSu> NhanSu { get; set; }

        //YeuCau
        public DbSet<YeuCau> YeuCau { get; set; }

        //Jira
        public DbSet<Jira> Jira { get; set; }

        //DonViYeuCau
        public DbSet<DonViYeuCau> DonViYeuCau { get; set; }

        //Comment
        public DbSet<Comment> Comment { get; set; }

        //Quanlydichvu
        public DbSet<QuanLyDichVu> QuanLyDichVu { get; set; }

        //Quanlydichvu
        public DbSet<LoaiYeuCau> LoaiYeuCau { get; set; }

        public DbSet<DichVuTheoUnit> DichVuTheoUnit { get; set; }

        public DbSet<LoaiYeuCauNew> LoaiYeuCauNew { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Writers;
using System.Reflection.Metadata.Ecma335;
using TA_PROJECT_CyclecountAPI.Model.LICC;
using TA_PROJECT_CyclecountAPI.Model.Lx17;
using TA_PROJECT_CyclecountAPI.Model.User;

namespace TA_PROJECT_CyclecountAPI.DAL
{
    public class CyclecountContext : DbContext
    {
        public DbSet<Lx17> Lx17 { get; set; }
        public DbSet<Lx17Log> Lx17Log { get; set; }
        public DbSet<MatClass> MatClass { get; set; }
        public DbSet<MatPrice> MatPrice { get; set; }
        public DbSet<LICC> LICC { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<DeptModel> Depts { get; set; }
        public DbSet<UserDeptModel> UserDepts { get; set; }

        public CyclecountContext(DbContextOptions<CyclecountContext> opt)  : base(opt)
        {
               
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lx17>(ent =>
            {
                ent.ToTable("Lx17");
                ent.HasKey(prop => prop.Id);
                ent.Property(prop => prop.Id).HasColumnType("int").ValueGeneratedOnAdd();
                ent.HasOne(x => x.Plant).WithMany().HasForeignKey(x => x.PlantID).OnDelete(DeleteBehavior.Cascade);
                ent.HasOne(x => x.Counter).WithMany().HasForeignKey(x => x.Idusup).OnDelete(DeleteBehavior.Restrict);
                ent.HasOne(x => x.InsertedBy).WithMany().HasForeignKey(x => x.Idusin).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Lx17Log>(ent =>
            {
                ent.ToTable("Lx17Log");
                ent.HasKey(prop => prop.Id);
                ent.Property(prop => prop.Id).HasColumnType("int").ValueGeneratedOnAdd();
                ent.HasOne(x => x.ReferencedLx17).WithMany().HasForeignKey(x => x.Lx17Id).OnDelete(DeleteBehavior.NoAction);
                ent.HasOne(x => x.Plant).WithMany().HasForeignKey(x => x.PlantID).OnDelete(DeleteBehavior.Cascade);
                ent.HasOne(x => x.Counter).WithMany().HasForeignKey(x => x.Idusup).OnDelete(DeleteBehavior.Restrict);
                ent.HasOne(x => x.InsertedBy).WithMany().HasForeignKey(x => x.Idusin).OnDelete(DeleteBehavior.Restrict);
                ent.HasOne(x => x.Logged_User).WithMany().HasForeignKey(x => x.Logged_UserId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<MatClass>(ent =>
            {
                ent.ToTable("MatClass");
                ent.HasKey(prop => prop.id);
                ent.Property(prop => prop.id).HasColumnType("int").ValueGeneratedOnAdd();
                ent.Property(prop => prop.fmr).HasColumnType("nvarchar(48)");
                ent.Property(prop => prop.abcin).HasColumnType("nvarchar(12)");
                ent.Property(prop => prop.matnr).HasColumnType("Nvarchar(128)");
                ent.HasOne(x => x.Plant).WithMany().HasForeignKey(x => x.PlantId);
                ent.HasOne(x => x.Created_By).WithMany().HasForeignKey(x => x.idusin).OnDelete(DeleteBehavior.Restrict);
                ent.HasOne(x => x.Updated_By).WithMany().HasForeignKey(x => x.idusup).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<MatPrice>(ent =>
            {
                ent.ToTable("MatPrice");
                ent.HasKey(prop => prop.id);
                ent.Property(prop => prop.id).HasColumnType("int").ValueGeneratedOnAdd();
                ent.HasOne(x => x.Plant).WithMany().HasForeignKey(x => x.PlantId).OnDelete(DeleteBehavior.Cascade);
                ent.HasOne(x => x.Created_By).WithMany().HasForeignKey(x => x.idusin).OnDelete(DeleteBehavior.Restrict);
                ent.HasOne(x => x.Updated_By).WithMany().HasForeignKey(x => x.idusup).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<LICC>(ent =>
            {
                ent.ToTable("LICC");
                ent.HasKey(prop => prop.id);
                ent.Property(prop => prop.id).HasColumnType("int").ValueGeneratedOnAdd();
                ent.HasOne(x => x.Plant).WithMany().HasForeignKey(x => x.PlantId);
                ent.HasOne(x => x.InsertedBy).WithMany().HasForeignKey(x => x.Idusin).OnDelete(DeleteBehavior.Restrict);
                ent.HasOne(x => x.UpdatedBy).WithMany().HasForeignKey(x => x.Idusup).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserModel>(ent =>
            {
                ent.ToTable("Users");
                ent.HasKey(prop => prop.Id);
                ent.Property(prop => prop.Id).HasColumnType("int").ValueGeneratedOnAdd();
                ent.Property(prop => prop.SESAID).HasColumnType("Nvarchar(48)");
                ent.Property(prop => prop.Name).HasColumnType("Nvarchar(128)");
                ent.Property(prop => prop.Username).HasColumnType("Nvarchar(128)");
                ent.Property(prop => prop.Level).HasColumnType("nvarchar(64)");
                ent.Property(prop => prop.Password).HasColumnType("Nvarchar(128)");
                ent.HasMany(x => x.Depts).WithOne(x => x.User).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                ent.HasOne(x => x.DefaultDept).WithMany().HasForeignKey(x => x.DefaultDeptId).OnDelete(DeleteBehavior.NoAction);
                ent.HasOne(x => x.Role).WithMany().HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<DeptModel>(ent =>
            {
                ent.ToTable("Depts");
                ent.HasKey(prop => prop.Id);
                ent.Property(prop => prop.Id).HasColumnType("int").ValueGeneratedOnAdd();
                ent.Property(prop => prop.DeptName).HasColumnType("nvarchar(64)");
                ent.Property(prop => prop.LGNUM).HasColumnType("nvarchar(12)");
                ent.Property(prop => prop.Mltp).HasColumnType("int");
                ent.Property(prop => prop.WERKS).HasColumnType("nvarchar(12)");
                
            });
            modelBuilder.Entity<UserDeptModel>(ent =>
            {
                ent.ToTable("UserDept");
                ent.HasKey(prop => new { prop.UserId, prop.DeptId });

                ent.Property(prop => prop.UserId).HasColumnType("int");
                ent.Property(prop => prop.DeptId).HasColumnType("int");
//                ent.HasOne(x => x.User).WithMany(x => x.Depts).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                ent.HasOne(x => x.Dept).WithMany().HasForeignKey(x => x.DeptId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<RoleModel>(ent =>
            {
                ent.ToTable("Role");
                ent.HasKey(prop => prop.Id);
                ent.Property(x => x.Id).HasColumnType("int").ValueGeneratedOnAdd();
                ent.Property(x => x.RoleName).HasColumnType("nvarchar(48)");
            });
        }
    }
}

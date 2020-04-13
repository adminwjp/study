using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Company.Domain.Core;
using Company.Domain.Mapp;
using Microsoft.Extensions.Logging;

namespace Company.Domain
{
    public class CompanyDbContext:DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory
       = LoggerFactory.Create(builder => { builder.AddConsole();  });
        public CompanyDbContext(DbContextOptions options):base(options)
        {
           // Database.Migrate();
           //  base.Database.EnsureCreated();
        }
        public DbSet<WorkCategoryInfo>  WorkCategories { get; set; }
        public DbSet<SocialInfo> Socials { get; set; }
        public DbSet<TeamSourceInfo> TeamSourceInfos { get; set; }
        public DbSet<TeamInfo> Teams { get; set; }
        public DbSet<AboutInfo> Abouts { get; set; }
        public DbSet<NavInfo> NavInfos { get; set; }
        public DbSet<CompanyInfo> Companies { get; set; }
        public DbSet<MainInfo> Mains { get; set; }
        public DbSet<WorkInfo> Works { get; set; }
        public DbSet<CategoryInfo> Categories { get; set; }
        public DbSet<BasicCategoryInfo> BasicCategories { get; set; }
        public DbSet<ImageInfo> Images { get; set; }
        public DbSet<ServiceInfo> Services { get; set; }
        public DbSet<SkillInfo> Skills { get; set; }
        public DbSet<MediaInfo> Medias { get; set; }
        public DbSet<BrandInfo> Brands { get; set; }
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<AdminInfo> Admins { get; set; }
        public DbSet<AdminRoleInfo> Roles { get; set; }
        public DbSet<ThemeInfo> Themes { get; set; }
        public DbSet<TestimonialPersonInfo> TestimonialPersons { get; set; }
        public DbSet<SkinInfo> Skins { get; set; }
        public DbSet<MenuInfo> Menus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory); // Warning: Do not create a new ILoggerFactory instance each time
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new ThemeMapp());
            base.OnModelCreating(modelBuilder);
        }
    }
}

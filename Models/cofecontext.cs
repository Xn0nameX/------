using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Курсач.Models
{
    public class cofecontext : DbContext
    {
        public cofecontext(DbContextOptions<cofecontext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        // DbSet'ы для моделей данных
        public DbSet<reservation> reservations { get; set; }
        public DbSet<menuitemcategory> menuitemcategories { get; set; }
        public DbSet<payment> payments { get; set; }
        public DbSet<Userr> users { get; set; }
        public DbSet<Tables> tables { get; set; }
        public DbSet<Menuitem> menuitems { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Orderitem> orderitems { get; set; }
        public DbSet<Orderstatus> orderstatuses { get; set; }

        public DbSet<UserRole> userroles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql("Host=localhost;Port=5432;Database=kyrs;Username=postgres;Password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Userr>()
            .HasMany(u => u.Tables)
            .WithOne(t => t.ReservedByUser)
            .HasForeignKey(t => t.ReservedByUserId);


            modelBuilder.Entity<Userr>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.Userr)
            .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Orders>()
            .HasMany(o => o.Orderitem)
            .WithOne(oi => oi.Orders)
            .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Menuitem>()
            .HasMany(mi => mi.Orderitem)
            .WithOne(oi => oi.Menuitem)
            .HasForeignKey(oi => oi.MenuItemId);

            modelBuilder.Entity<Orders>()
            .HasOne(o => o.Orderstatus)
            .WithMany(os => os.Orders)
            .HasForeignKey(o => o.StatusId);

            modelBuilder.Entity<Userr>()
            .HasOne(u => u.UserRole)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);


            modelBuilder.Entity<reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId);


            modelBuilder.Entity<menuitemcategory>()
                .HasMany(c => c.Menuitems)
                .WithOne(m => m.menuitemcategory)
                .HasForeignKey(m => m.MenuItemId);


            modelBuilder.Entity<Menuitem>()
            .HasOne(mi => mi.menuitemcategory)
            .WithMany(c => c.Menuitems)
            .HasForeignKey(mi => mi.CategoryId);


            modelBuilder.Entity<Orders>()
                .HasOne(o => o.payment)
                .WithOne(p => p.order)
                .HasForeignKey<payment>(p => p.PaymentId);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { RoleId = 1, RoleName = "Пользователь" },
                 new UserRole { RoleId = 2, RoleName = "Админ" },
                 new UserRole { RoleId = 3, RoleName = "Сотрудник" }
            );
            modelBuilder.Entity<menuitemcategory>().HasData(
                new menuitemcategory { CategoryId = 1, CategoryName = "Напитки" },
                new menuitemcategory { CategoryId = 2, CategoryName = "Десерты" }
            );
            modelBuilder.Entity<Menuitem>().HasData(
                new Menuitem { MenuItemId = 1, ItemName = "Кофе Латте", CategoryId = 1, Description = "Нежный латте с великолепным ароматом", Price = 99, Availability = true },
                new Menuitem { MenuItemId = 2, ItemName = "Капучино", CategoryId = 1, Description = "Ароматный капучино с пышной пенкой", Price = 89, Availability = true },
                new Menuitem { MenuItemId = 3, ItemName = "Американо", CategoryId = 1, Description = "Крепкий американо с насыщенным вкусом", Price = 69, Availability = true },
                new Menuitem { MenuItemId = 4, ItemName = "Эспрессо", CategoryId = 1, Description = "Классическое эспрессо для настоящих ценителей", Price = 89, Availability = true },
                new Menuitem { MenuItemId = 5, ItemName = "Мокко", CategoryId = 1, Description = "Восхитительный мокко с добавлением шоколада", Price = 100, Availability = true },
                new Menuitem { MenuItemId = 6, ItemName = "Чизкейк", CategoryId = 2, Description = "Нежный чизкейк с сочными ягодами", Price = 120, Availability = true },
                new Menuitem { MenuItemId = 7, ItemName = "Круассан с лососем", CategoryId = 2, Description = "Ароматный круассан с копчёным лососем", Price = 100, Availability = true },
                new Menuitem { MenuItemId = 8, ItemName = "Фруктовый салат", CategoryId = 2, Description = "Свежий фруктовый салат с мятным соусом", Price = 150, Availability = true },
                new Menuitem { MenuItemId = 9, ItemName = "Панкейки с кленовым сиропом", CategoryId = 2, Description = "Панкейки с кленовым сиропом", Price = 200, Availability = true }
            );

            modelBuilder.Entity<Userr>().HasData(
                new Userr
                {
                    UserId = 1,
                    PhoneNumber = "89999999999",
                    Username = "admin",
                    Password = "123",
                    FirstName = "Daniel",
                    LastName = "Met",
                    RoleId = 2, 
                    DOB = "01/01/1990", 
                    CreatedAt = DateTime.Now
                },
                new Userr
                {
                    UserId = 2,
                    PhoneNumber = "89771484774",
                    Username = "sotryd",
                    Password = "123",
                    FirstName = "Max",
                    LastName = "Met",
                    RoleId = 3,
                    DOB = "01/01/1990",
                    CreatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<Orderstatus>().HasData(
                new Orderstatus { StatusId = 1, StatusName = "В обработке" },
                new Orderstatus { StatusId = 2, StatusName = "Готовиться" },
                new Orderstatus { StatusId = 3, StatusName = "Готов" },
                new Orderstatus { StatusId = 4, StatusName = "Возврат" }
            );

            modelBuilder.Entity<Tables>().HasData(
                new Tables { TableId = 1, TableNumber = 1, Capacity = 4, IsReserved = false, ReservedByUserId = 1 },
                new Tables { TableId = 2, TableNumber = 2, Capacity = 4, IsReserved = false, ReservedByUserId = 1 },
                new Tables { TableId = 3, TableNumber = 3, Capacity = 4, IsReserved = false, ReservedByUserId = 1 }
            );






        }
    }

}
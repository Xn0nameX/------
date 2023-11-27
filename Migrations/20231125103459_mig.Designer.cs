﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Курсач.Models;

#nullable disable

namespace Курсач.Migrations
{
    [DbContext(typeof(cofecontext))]
    [Migration("20231125103459_mig")]
    partial class mig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Курсач.Models.Menuitem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MenuItemId"));

                    b.Property<bool>("Availability")
                        .HasColumnType("boolean");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("MenuItemId");

                    b.HasIndex("CategoryId");

                    b.ToTable("menuitems");

                    b.HasData(
                        new
                        {
                            MenuItemId = 1,
                            Availability = true,
                            CategoryId = 1,
                            Description = "Нежный латте с великолепным ароматом",
                            ItemName = "Кофе Латте",
                            Price = 99m
                        },
                        new
                        {
                            MenuItemId = 2,
                            Availability = true,
                            CategoryId = 1,
                            Description = "Ароматный капучино с пышной пенкой",
                            ItemName = "Капучино",
                            Price = 89m
                        },
                        new
                        {
                            MenuItemId = 3,
                            Availability = true,
                            CategoryId = 1,
                            Description = "Крепкий американо с насыщенным вкусом",
                            ItemName = "Американо",
                            Price = 69m
                        },
                        new
                        {
                            MenuItemId = 4,
                            Availability = true,
                            CategoryId = 1,
                            Description = "Классическое эспрессо для настоящих ценителей",
                            ItemName = "Эспрессо",
                            Price = 89m
                        },
                        new
                        {
                            MenuItemId = 5,
                            Availability = true,
                            CategoryId = 1,
                            Description = "Восхитительный мокко с добавлением шоколада",
                            ItemName = "Мокко",
                            Price = 100m
                        },
                        new
                        {
                            MenuItemId = 6,
                            Availability = true,
                            CategoryId = 2,
                            Description = "Нежный чизкейк с сочными ягодами",
                            ItemName = "Чизкейк",
                            Price = 120m
                        },
                        new
                        {
                            MenuItemId = 7,
                            Availability = true,
                            CategoryId = 2,
                            Description = "Ароматный круассан с копчёным лососем",
                            ItemName = "Круассан с лососем",
                            Price = 100m
                        },
                        new
                        {
                            MenuItemId = 8,
                            Availability = true,
                            CategoryId = 2,
                            Description = "Свежий фруктовый салат с мятным соусом",
                            ItemName = "Фруктовый салат",
                            Price = 150m
                        },
                        new
                        {
                            MenuItemId = 9,
                            Availability = true,
                            CategoryId = 2,
                            Description = "Панкейки с кленовым сиропом",
                            ItemName = "Панкейки с кленовым сиропом",
                            Price = 200m
                        });
                });

            modelBuilder.Entity("Курсач.Models.Orderitem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderItemId"));

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("ItemPrice")
                        .HasColumnType("numeric");

                    b.Property<int>("MenuItemId")
                        .HasColumnType("integer");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderItemId");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("orderitems");
                });

            modelBuilder.Entity("Курсач.Models.Orders", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("Курсач.Models.Orderstatus", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("StatusId");

                    b.ToTable("orderstatuses");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            StatusName = "В обработке"
                        },
                        new
                        {
                            StatusId = 2,
                            StatusName = "Готовиться"
                        },
                        new
                        {
                            StatusId = 3,
                            StatusName = "Готов"
                        },
                        new
                        {
                            StatusId = 4,
                            StatusName = "Возврат"
                        });
                });

            modelBuilder.Entity("Курсач.Models.Tables", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TableId"));

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("boolean");

                    b.Property<int?>("ReservedByUserId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int>("TableNumber")
                        .HasColumnType("integer");

                    b.HasKey("TableId");

                    b.HasIndex("ReservedByUserId");

                    b.ToTable("tables");

                    b.HasData(
                        new
                        {
                            TableId = 1,
                            Capacity = 4,
                            IsReserved = false,
                            ReservedByUserId = 1,
                            TableNumber = 1
                        },
                        new
                        {
                            TableId = 2,
                            Capacity = 4,
                            IsReserved = false,
                            ReservedByUserId = 1,
                            TableNumber = 2
                        },
                        new
                        {
                            TableId = 3,
                            Capacity = 4,
                            IsReserved = false,
                            ReservedByUserId = 1,
                            TableNumber = 3
                        });
                });

            modelBuilder.Entity("Курсач.Models.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("userroles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Пользователь"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "Админ"
                        },
                        new
                        {
                            RoleId = 3,
                            RoleName = "Сотрудник"
                        });
                });

            modelBuilder.Entity("Курсач.Models.Userr", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DOB")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            CreatedAt = new DateTime(2023, 11, 25, 13, 34, 59, 575, DateTimeKind.Local).AddTicks(7850),
                            DOB = "01/01/1990",
                            FirstName = "Daniel",
                            LastName = "Met",
                            Password = "123",
                            PhoneNumber = "89999999999",
                            RoleId = 2,
                            Username = "admin"
                        },
                        new
                        {
                            UserId = 2,
                            CreatedAt = new DateTime(2023, 11, 25, 13, 34, 59, 575, DateTimeKind.Local).AddTicks(7858),
                            DOB = "01/01/1990",
                            FirstName = "Max",
                            LastName = "Met",
                            Password = "123",
                            PhoneNumber = "89771484774",
                            RoleId = 3,
                            Username = "sotryd"
                        });
                });

            modelBuilder.Entity("Курсач.Models.menuitemcategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategoryId");

                    b.ToTable("menuitemcategories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Напитки"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Десерты"
                        });
                });

            modelBuilder.Entity("Курсач.Models.payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PaymentDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("PaymentId");

                    b.ToTable("payments");
                });

            modelBuilder.Entity("Курсач.Models.reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ReservationId"));

                    b.Property<string>("CustomerFirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerLastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("ReservationDateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("TableId")
                        .HasColumnType("integer");

                    b.Property<int>("TablesTableId")
                        .HasColumnType("integer");

                    b.HasKey("ReservationId");

                    b.HasIndex("TableId");

                    b.HasIndex("TablesTableId");

                    b.ToTable("reservations");
                });

            modelBuilder.Entity("Курсач.Models.Menuitem", b =>
                {
                    b.HasOne("Курсач.Models.menuitemcategory", "menuitemcategory")
                        .WithMany("Menuitems")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("menuitemcategory");
                });

            modelBuilder.Entity("Курсач.Models.Orderitem", b =>
                {
                    b.HasOne("Курсач.Models.Menuitem", "Menuitem")
                        .WithMany("Orderitem")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Курсач.Models.Orders", "Orders")
                        .WithMany("Orderitem")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menuitem");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Курсач.Models.Orders", b =>
                {
                    b.HasOne("Курсач.Models.Orderstatus", "Orderstatus")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Курсач.Models.Userr", "Userr")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orderstatus");

                    b.Navigation("Userr");
                });

            modelBuilder.Entity("Курсач.Models.Tables", b =>
                {
                    b.HasOne("Курсач.Models.Userr", "ReservedByUser")
                        .WithMany("Tables")
                        .HasForeignKey("ReservedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReservedByUser");
                });

            modelBuilder.Entity("Курсач.Models.Userr", b =>
                {
                    b.HasOne("Курсач.Models.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Курсач.Models.payment", b =>
                {
                    b.HasOne("Курсач.Models.Orders", "order")
                        .WithOne("payment")
                        .HasForeignKey("Курсач.Models.payment", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order");
                });

            modelBuilder.Entity("Курсач.Models.reservation", b =>
                {
                    b.HasOne("Курсач.Models.Tables", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Курсач.Models.Tables", "Tables")
                        .WithMany()
                        .HasForeignKey("TablesTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Курсач.Models.Menuitem", b =>
                {
                    b.Navigation("Orderitem");
                });

            modelBuilder.Entity("Курсач.Models.Orders", b =>
                {
                    b.Navigation("Orderitem");

                    b.Navigation("payment")
                        .IsRequired();
                });

            modelBuilder.Entity("Курсач.Models.Orderstatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Курсач.Models.Tables", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Курсач.Models.UserRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Курсач.Models.Userr", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Курсач.Models.menuitemcategory", b =>
                {
                    b.Navigation("Menuitems");
                });
#pragma warning restore 612, 618
        }
    }
}

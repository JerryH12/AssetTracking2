﻿// <auto-generated />
using System;
using AssetTracking2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssetTracking2.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240404163456_AddRelationships")]
    partial class AddRelationships
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AssetTracking2.Assets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Office")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PriceInUSD")
                        .HasColumnType("float");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Assets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "MacBook",
                            Office = "Sweden",
                            PriceInUSD = 1200.0,
                            PurchaseDate = new DateTime(2021, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Samsung",
                            Office = "USA",
                            PriceInUSD = 200.0,
                            PurchaseDate = new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Dell",
                            Office = "India",
                            PriceInUSD = 599.0,
                            PurchaseDate = new DateTime(2014, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Motorolla",
                            Office = "India",
                            PriceInUSD = 699.0,
                            PurchaseDate = new DateTime(2020, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Lenovo",
                            Office = "Sweden",
                            PriceInUSD = 599.0,
                            PurchaseDate = new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("AssetTracking2.LaptopComputers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetsId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssetsId");

                    b.ToTable("LaptopComputers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssetsId = 1,
                            Model = "SD",
                            Type = "Computer"
                        },
                        new
                        {
                            Id = 3,
                            AssetsId = 3,
                            Model = "ThinkPad",
                            Type = "Computer"
                        },
                        new
                        {
                            Id = 4,
                            AssetsId = 5,
                            Model = "IdeaPad",
                            Type = "Computer"
                        });
                });

            modelBuilder.Entity("AssetTracking2.MobilePhones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetsId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssetsId");

                    b.ToTable("MobilePhones");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssetsId = 2,
                            Model = "SD",
                            Type = "Mobile"
                        },
                        new
                        {
                            Id = 2,
                            AssetsId = 4,
                            Model = "iPhone14",
                            Type = "Mobile"
                        });
                });

            modelBuilder.Entity("AssetTracking2.LaptopComputers", b =>
                {
                    b.HasOne("AssetTracking2.Assets", "Assets")
                        .WithMany("LaptopComputerList")
                        .HasForeignKey("AssetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assets");
                });

            modelBuilder.Entity("AssetTracking2.MobilePhones", b =>
                {
                    b.HasOne("AssetTracking2.Assets", "Assets")
                        .WithMany("MobilePhoneList")
                        .HasForeignKey("AssetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assets");
                });

            modelBuilder.Entity("AssetTracking2.Assets", b =>
                {
                    b.Navigation("LaptopComputerList");

                    b.Navigation("MobilePhoneList");
                });
#pragma warning restore 612, 618
        }
    }
}

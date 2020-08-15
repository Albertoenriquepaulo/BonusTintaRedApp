﻿// <auto-generated />
using BonusApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BonusApp.Migrations
{
    [DbContext(typeof(BonusAppDbContext))]
    [Migration("20200815104224_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7");

            modelBuilder.Entity("BonusApp.Data.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Client");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "TRCtester@tcr.com"
                        });
                });

            modelBuilder.Entity("BonusApp.Data.ClientCoupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CouponId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpentPages")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("CouponId");

                    b.ToTable("ClientCoupon");
                });

            modelBuilder.Entity("BonusApp.Data.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Pages")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Coupon");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "BYN100",
                            Pages = 100
                        },
                        new
                        {
                            Id = 2,
                            Name = "BYN200",
                            Pages = 200
                        },
                        new
                        {
                            Id = 3,
                            Name = "BYN500",
                            Pages = 500
                        },
                        new
                        {
                            Id = 4,
                            Name = "COLOR50",
                            Pages = 50
                        },
                        new
                        {
                            Id = 5,
                            Name = "COLOR100",
                            Pages = 100
                        },
                        new
                        {
                            Id = 6,
                            Name = "COLOR200",
                            Pages = 200
                        });
                });

            modelBuilder.Entity("BonusApp.Data.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientCouponId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CouponId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("SpentPages")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClientCouponId");

                    b.HasIndex("ClientId");

                    b.HasIndex("CouponId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("BonusApp.Data.ClientCoupon", b =>
                {
                    b.HasOne("BonusApp.Data.Client", "Client")
                        .WithMany("ClientCoupons")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BonusApp.Data.Coupon", "Coupon")
                        .WithMany("ClientCoupons")
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BonusApp.Data.Transaction", b =>
                {
                    b.HasOne("BonusApp.Data.ClientCoupon", "ClientCoupon")
                        .WithMany("Transactions")
                        .HasForeignKey("ClientCouponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BonusApp.Data.Client", "Client")
                        .WithMany("Transactions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BonusApp.Data.Coupon", "Coupon")
                        .WithMany("Transactions")
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
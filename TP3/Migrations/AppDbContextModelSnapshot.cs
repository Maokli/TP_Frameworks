﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TP3.Models;

#nullable disable

namespace TP4.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("CustomerMovie", b =>
                {
                    b.Property<int>("CustomersId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoviesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CustomersId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CustomerMovie");
                });

            modelBuilder.Entity("TP3.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("membershiptypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("membershiptypeId");

                    b.ToTable("custumors");
                });

            modelBuilder.Entity("TP3.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GenreName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 54,
                            GenreName = "GenreFromJsonFile1"
                        },
                        new
                        {
                            Id = 45,
                            GenreName = "GenreFromJsonFile2"
                        });
                });

            modelBuilder.Entity("TP3.Models.Membershiptype", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("DiscountRate")
                        .HasColumnType("REAL");

                    b.Property<int>("DurationInMonth")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<float>("SignUpFee")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Membershiptypes");
                });

            modelBuilder.Entity("TP3.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GenresId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("MovieAdded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Photo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GenresId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("CustomerMovie", b =>
                {
                    b.HasOne("TP3.Models.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TP3.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TP3.Models.Customer", b =>
                {
                    b.HasOne("TP3.Models.Membershiptype", "Membershiptypes")
                        .WithMany("Customers")
                        .HasForeignKey("membershiptypeId");

                    b.Navigation("Membershiptypes");
                });

            modelBuilder.Entity("TP3.Models.Movie", b =>
                {
                    b.HasOne("TP3.Models.Genre", "Genres")
                        .WithMany("Movies")
                        .HasForeignKey("GenresId");

                    b.Navigation("Genres");
                });

            modelBuilder.Entity("TP3.Models.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("TP3.Models.Membershiptype", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}

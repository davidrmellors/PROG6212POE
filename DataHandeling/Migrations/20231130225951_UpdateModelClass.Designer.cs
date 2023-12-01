﻿// <auto-generated />
using System;
using DataHandeling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataHandeling.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20231130225951_UpdateModelClass")]
    partial class UpdateModelClass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataHandeling.Models.Module", b =>
                {
                    b.Property<int>("ModuleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModuleID"), 1L, 1);

                    b.Property<double>("ClassHoursPerWeek")
                        .HasColumnType("float");

                    b.Property<string>("ModuleCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModuleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfCredits")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfWeeks")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StudyDay")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ModuleID");

                    b.HasIndex("Username");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("DataHandeling.Models.StudyHour", b =>
                {
                    b.Property<int>("StudyHoursID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudyHoursID"), 1L, 1);

                    b.Property<int>("ModuleID")
                        .HasColumnType("int");

                    b.Property<double>("RemainingHours")
                        .HasColumnType("float");

                    b.Property<int>("WeekNumber")
                        .HasColumnType("int");

                    b.HasKey("StudyHoursID");

                    b.HasIndex("ModuleID");

                    b.ToTable("StudyHours");
                });

            modelBuilder.Entity("DataHandeling.Models.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataHandeling.Models.Module", b =>
                {
                    b.HasOne("DataHandeling.Models.User", "User")
                        .WithMany("Modules")
                        .HasForeignKey("Username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataHandeling.Models.StudyHour", b =>
                {
                    b.HasOne("DataHandeling.Models.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("DataHandeling.Models.User", b =>
                {
                    b.Navigation("Modules");
                });
#pragma warning restore 612, 618
        }
    }
}

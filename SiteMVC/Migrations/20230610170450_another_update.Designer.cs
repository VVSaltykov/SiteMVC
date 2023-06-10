﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SiteMVC;

#nullable disable

namespace SiteMVC.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230610170450_another_update")]
    partial class another_update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ClassUsers", b =>
                {
                    b.Property<int>("ClassesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("ClassesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("ClassUsers");
                });

            modelBuilder.Entity("SiteMVC.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SiteMVC.Models.Cabinet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Cabinets");
                });

            modelBuilder.Entity("SiteMVC.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Name")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("SiteMVC.Models.HomeWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("ClassID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LessonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassID");

                    b.HasIndex("LessonId");

                    b.ToTable("HomeWorks");
                });

            modelBuilder.Entity("SiteMVC.Models.Journal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int?>("LessonID")
                        .HasColumnType("int");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("WorkType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LessonID");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserID");

                    b.ToTable("Journals");
                });

            modelBuilder.Entity("SiteMVC.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CabinetId")
                        .HasColumnType("int");

                    b.Property<int?>("ClassID")
                        .HasColumnType("int");

                    b.Property<int?>("LessonNumber")
                        .HasColumnType("int");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("WeekDay")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CabinetId");

                    b.HasIndex("ClassID");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserID");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("SiteMVC.Models.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SiteMVC.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SiteMVC.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("ClassId")
                        .HasColumnType("int");

                    b.Property<string>("FIO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClassUsers", b =>
                {
                    b.HasOne("SiteMVC.Models.Class", null)
                        .WithMany()
                        .HasForeignKey("ClassesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SiteMVC.Models.Users", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SiteMVC.Models.HomeWork", b =>
                {
                    b.HasOne("SiteMVC.Models.Class", "Class")
                        .WithMany("HomeWorks")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SiteMVC.Models.Lesson", "Lesson")
                        .WithMany("HomeWorks")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Class");

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("SiteMVC.Models.Journal", b =>
                {
                    b.HasOne("SiteMVC.Models.Lesson", "Lesson")
                        .WithMany("Journals")
                        .HasForeignKey("LessonID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SiteMVC.Models.Subject", "Subject")
                        .WithMany("Journals")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SiteMVC.Models.Users", "User")
                        .WithMany("Journals")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Lesson");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SiteMVC.Models.Lesson", b =>
                {
                    b.HasOne("SiteMVC.Models.Cabinet", "Cabinet")
                        .WithMany("Lessons")
                        .HasForeignKey("CabinetId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SiteMVC.Models.Class", "Class")
                        .WithMany("Lessons")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SiteMVC.Models.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SiteMVC.Models.Users", "Users")
                        .WithMany("Lessons")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Cabinet");

                    b.Navigation("Class");

                    b.Navigation("Subject");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SiteMVC.Models.Users", b =>
                {
                    b.HasOne("SiteMVC.Models.Account", "Account")
                        .WithMany("Users")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("SiteMVC.Models.Roles", "Roles")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Account");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("SiteMVC.Models.Account", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SiteMVC.Models.Cabinet", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("SiteMVC.Models.Class", b =>
                {
                    b.Navigation("HomeWorks");

                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("SiteMVC.Models.Lesson", b =>
                {
                    b.Navigation("HomeWorks");

                    b.Navigation("Journals");
                });

            modelBuilder.Entity("SiteMVC.Models.Roles", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("SiteMVC.Models.Subject", b =>
                {
                    b.Navigation("Journals");

                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("SiteMVC.Models.Users", b =>
                {
                    b.Navigation("Journals");

                    b.Navigation("Lessons");
                });
#pragma warning restore 612, 618
        }
    }
}

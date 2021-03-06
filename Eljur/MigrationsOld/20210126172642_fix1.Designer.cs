﻿// <auto-generated />
using System;
using Eljur.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20210126172642_fix1")]
    partial class fix1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("EducationDepartmentEducationLevel", b =>
                {
                    b.Property<int>("EducationDepartmentsId")
                        .HasColumnType("integer");

                    b.Property<int>("EducationLevelsId")
                        .HasColumnType("integer");

                    b.HasKey("EducationDepartmentsId", "EducationLevelsId");

                    b.HasIndex("EducationLevelsId");

                    b.ToTable("EducationDepartmentEducationLevel");
                });

            modelBuilder.Entity("Eljur.Context.Tables.EducationDepartment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EducationDepartments");
                });

            modelBuilder.Entity("Eljur.Context.Tables.EducationLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EducationLevels");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("SpecializationId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("Eljur.Context.Tables.GroupVisit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<int?>("SemesterId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("integer");

                    b.Property<int?>("ThemeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SemesterId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("ThemeId");

                    b.ToTable("GroupVisit");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("EducationDepartmentId")
                        .HasColumnType("integer");

                    b.Property<int?>("EducationLevelId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EducationDepartmentId");

                    b.HasIndex("EducationLevelId");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("FIO")
                        .HasColumnType("text");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Eljur.Context.Tables.StudentVisit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("GroupVisitId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TypeVisit")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupVisitId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentVisit");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("Attestation")
                        .HasColumnType("integer");

                    b.Property<double>("AttestationHours")
                        .HasColumnType("double precision");

                    b.Property<int?>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("SemesterId")
                        .HasColumnType("integer");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SemesterId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("FIO")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<double>("AllowedHours")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("SemesterId")
                        .HasColumnType("integer");

                    b.Property<int?>("SubjectId")
                        .HasColumnType("integer");

                    b.Property<int?>("ThemeGroupId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SemesterId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("ThemeGroupId");

                    b.ToTable("Theme");
                });

            modelBuilder.Entity("Eljur.Context.Tables.ThemeGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<double>("UsedHours")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("ThemeGroup");
                });

            modelBuilder.Entity("Eljur.Context.Tables.ThemeVisit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int?>("GroupVisitId")
                        .HasColumnType("integer");

                    b.Property<double>("HoursPerVisit")
                        .HasColumnType("double precision");

                    b.Property<int?>("ThemeId")
                        .HasColumnType("integer");

                    b.Property<int>("TypeSubject")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupVisitId");

                    b.HasIndex("ThemeId");

                    b.ToTable("ThemeVisit");
                });

            modelBuilder.Entity("Eljur.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("EducationDepartmentEducationLevel", b =>
                {
                    b.HasOne("Eljur.Context.Tables.EducationDepartment", null)
                        .WithMany()
                        .HasForeignKey("EducationDepartmentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eljur.Context.Tables.EducationLevel", null)
                        .WithMany()
                        .HasForeignKey("EducationLevelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Eljur.Context.Tables.Group", b =>
                {
                    b.HasOne("Eljur.Context.Tables.Specialization", "Specialization")
                        .WithMany("Groups")
                        .HasForeignKey("SpecializationId");

                    b.Navigation("Specialization");
                });

            modelBuilder.Entity("Eljur.Context.Tables.GroupVisit", b =>
                {
                    b.HasOne("Eljur.Context.Tables.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("Eljur.Context.Tables.Semester", null)
                        .WithMany("GroupVisits")
                        .HasForeignKey("SemesterId");

                    b.HasOne("Eljur.Context.Tables.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");

                    b.HasOne("Eljur.Context.Tables.Theme", null)
                        .WithMany("Visits")
                        .HasForeignKey("ThemeId");

                    b.Navigation("Group");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Semester", b =>
                {
                    b.HasOne("Eljur.Context.Tables.Group", "Group")
                        .WithMany("Semesters")
                        .HasForeignKey("GroupId");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Specialization", b =>
                {
                    b.HasOne("Eljur.Context.Tables.EducationDepartment", "EducationDepartment")
                        .WithMany("Specializations")
                        .HasForeignKey("EducationDepartmentId");

                    b.HasOne("Eljur.Context.Tables.EducationLevel", "EducationLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelId");

                    b.Navigation("EducationDepartment");

                    b.Navigation("EducationLevel");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Student", b =>
                {
                    b.HasOne("Eljur.Context.Tables.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Eljur.Context.Tables.StudentVisit", b =>
                {
                    b.HasOne("Eljur.Context.Tables.GroupVisit", "GroupVisit")
                        .WithMany("StudentVisits")
                        .HasForeignKey("GroupVisitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eljur.Context.Tables.Student", "Student")
                        .WithMany("StudentVisits")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eljur.Context.Tables.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId");

                    b.Navigation("GroupVisit");

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Subject", b =>
                {
                    b.HasOne("Eljur.Context.Tables.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("Eljur.Context.Tables.Semester", "Semester")
                        .WithMany("Subjects")
                        .HasForeignKey("SemesterId");

                    b.HasOne("Eljur.Context.Tables.Teacher", "Teacher")
                        .WithMany("Subjects")
                        .HasForeignKey("TeacherId");

                    b.Navigation("Group");

                    b.Navigation("Semester");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Theme", b =>
                {
                    b.HasOne("Eljur.Context.Tables.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId");

                    b.HasOne("Eljur.Context.Tables.Subject", "Subject")
                        .WithMany("Themes")
                        .HasForeignKey("SubjectId");

                    b.HasOne("Eljur.Context.Tables.ThemeGroup", "ThemeGroup")
                        .WithMany()
                        .HasForeignKey("ThemeGroupId");

                    b.Navigation("Semester");

                    b.Navigation("Subject");

                    b.Navigation("ThemeGroup");
                });

            modelBuilder.Entity("Eljur.Context.Tables.ThemeVisit", b =>
                {
                    b.HasOne("Eljur.Context.Tables.GroupVisit", null)
                        .WithMany("ThemeVisits")
                        .HasForeignKey("GroupVisitId");

                    b.HasOne("Eljur.Context.Tables.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Eljur.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Eljur.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Eljur.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Eljur.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Eljur.Context.Tables.EducationDepartment", b =>
                {
                    b.Navigation("Specializations");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Group", b =>
                {
                    b.Navigation("Semesters");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Eljur.Context.Tables.GroupVisit", b =>
                {
                    b.Navigation("StudentVisits");

                    b.Navigation("ThemeVisits");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Semester", b =>
                {
                    b.Navigation("GroupVisits");

                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Specialization", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Student", b =>
                {
                    b.Navigation("StudentVisits");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Subject", b =>
                {
                    b.Navigation("Themes");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Teacher", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("Eljur.Context.Tables.Theme", b =>
                {
                    b.Navigation("Visits");
                });
#pragma warning restore 612, 618
        }
    }
}

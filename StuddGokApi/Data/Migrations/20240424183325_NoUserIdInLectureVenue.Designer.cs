﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StuddGokApi.Data;

#nullable disable

namespace StuddGokApi.Data.Migrations
{
    [DbContext(typeof(StuddGokDbContext))]
    [Migration("20240424183325_NoUserIdInLectureVenue")]
    partial class NoUserIdInLectureVenue
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("StuddGokApi.Models.Alert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Links")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Seen")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Alerts");
                });

            modelBuilder.Entity("StuddGokApi.Models.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseImplementationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Mandatory")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CourseImplementationId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("StuddGokApi.Models.AssignmentResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("UserId");

                    b.ToTable("AssignmentResults");
                });

            modelBuilder.Entity("StuddGokApi.Models.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.HasIndex("UserId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("StuddGokApi.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("DiplomaCours")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ExamCours")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Points")
                        .HasColumnType("decimal(65,30)");

                    b.Property<bool>("TeachCours")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StuddGokApi.Models.CourseImplementation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EndSemester")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EndYear")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseImplementations");
                });

            modelBuilder.Entity("StuddGokApi.Models.Exam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CourseImplementationId")
                        .HasColumnType("int");

                    b.Property<decimal>("DurationHours")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("PeriodEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("PeriodStart")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CourseImplementationId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("StuddGokApi.Models.ExamGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("UserId");

                    b.ToTable("ExamGroups");
                });

            modelBuilder.Entity("StuddGokApi.Models.ExamImplementation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("VenueId");

                    b.ToTable("ExamImplementations");
                });

            modelBuilder.Entity("StuddGokApi.Models.ExamResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("UserId");

                    b.ToTable("ExamResults");
                });

            modelBuilder.Entity("StuddGokApi.Models.Lecture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseImplementationId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CourseImplementationId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("StuddGokApi.Models.LectureVenue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.HasIndex("UserId");

                    b.HasIndex("VenueId");

                    b.ToTable("LectureVenues");
                });

            modelBuilder.Entity("StuddGokApi.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("StuddGokApi.Models.ProgramCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseImplementationId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramImplementationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseImplementationId");

                    b.HasIndex("ProgramImplementationId");

                    b.ToTable("ProgramCourses");
                });

            modelBuilder.Entity("StuddGokApi.Models.ProgramImplementation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EndSemester")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EndYear")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("StudyProgramId")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudyProgramId");

                    b.ToTable("ProgramImplementations");
                });

            modelBuilder.Entity("StuddGokApi.Models.ProgramLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramImplementationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProgramImplementationId");

                    b.ToTable("ProgramLocations");
                });

            modelBuilder.Entity("StuddGokApi.Models.StudentProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProgramImplementationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramImplementationId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentPrograms");
                });

            modelBuilder.Entity("StuddGokApi.Models.StudyProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NUS_code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Points")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("StudyPrograms");
                });

            modelBuilder.Entity("StuddGokApi.Models.TeacherCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CourseImplementationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseImplementationId");

                    b.HasIndex("UserId");

                    b.ToTable("TeacherCourses");
                });

            modelBuilder.Entity("StuddGokApi.Models.TeacherProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ProgramImplementationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramImplementationId");

                    b.HasIndex("UserId");

                    b.ToTable("TeacherPrograms");
                });

            modelBuilder.Entity("StuddGokApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email3")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GokstadEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StuddGokApi.Models.UserExamImplementation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExamImplementationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamImplementationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserExamImplementations");
                });

            modelBuilder.Entity("StuddGokApi.Models.Venue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PostCode")
                        .HasColumnType("int");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("StuddGokApi.Models.Alert", b =>
                {
                    b.HasOne("StuddGokApi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StuddGokApi.Models.Assignment", b =>
                {
                    b.HasOne("StuddGokApi.Models.CourseImplementation", "CourseImplementation")
                        .WithMany("Assignments")
                        .HasForeignKey("CourseImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseImplementation");
                });

            modelBuilder.Entity("StuddGokApi.Models.AssignmentResult", b =>
                {
                    b.HasOne("StuddGokApi.Models.Assignment", "Assignment")
                        .WithMany("AssignmentResults")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.User", "User")
                        .WithMany("AssignmentResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StuddGokApi.Models.Attendance", b =>
                {
                    b.HasOne("StuddGokApi.Models.Lecture", "Lecture")
                        .WithMany("Attendances")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.User", "User")
                        .WithMany("Attendances")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StuddGokApi.Models.CourseImplementation", b =>
                {
                    b.HasOne("StuddGokApi.Models.Course", "Course")
                        .WithMany("CoursesImplementations")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("StuddGokApi.Models.Exam", b =>
                {
                    b.HasOne("StuddGokApi.Models.CourseImplementation", "CourseImplementation")
                        .WithMany("Exams")
                        .HasForeignKey("CourseImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseImplementation");
                });

            modelBuilder.Entity("StuddGokApi.Models.ExamGroup", b =>
                {
                    b.HasOne("StuddGokApi.Models.Exam", "Exam")
                        .WithMany("ExamGroups")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.User", "User")
                        .WithMany("ExamGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StuddGokApi.Models.ExamImplementation", b =>
                {
                    b.HasOne("StuddGokApi.Models.Exam", "Exam")
                        .WithMany("ExamImplementation")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.Venue", "Venue")
                        .WithMany("ExamImplementations")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("StuddGokApi.Models.ExamResult", b =>
                {
                    b.HasOne("StuddGokApi.Models.Exam", "Exam")
                        .WithMany("ExamResults")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StuddGokApi.Models.Lecture", b =>
                {
                    b.HasOne("StuddGokApi.Models.CourseImplementation", "CourseImplementation")
                        .WithMany("Lectures")
                        .HasForeignKey("CourseImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseImplementation");
                });

            modelBuilder.Entity("StuddGokApi.Models.LectureVenue", b =>
                {
                    b.HasOne("StuddGokApi.Models.Lecture", "Lecture")
                        .WithMany("LectureVenues")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.User", null)
                        .WithMany("LectureVenues")
                        .HasForeignKey("UserId");

                    b.HasOne("StuddGokApi.Models.Venue", "Venue")
                        .WithMany("LectureVenues")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("StuddGokApi.Models.ProgramCourse", b =>
                {
                    b.HasOne("StuddGokApi.Models.CourseImplementation", "CourseImplementation")
                        .WithMany("ProgramCourses")
                        .HasForeignKey("CourseImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.ProgramImplementation", "ProgramImplementation")
                        .WithMany("ProgramCourses")
                        .HasForeignKey("ProgramImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseImplementation");

                    b.Navigation("ProgramImplementation");
                });

            modelBuilder.Entity("StuddGokApi.Models.ProgramImplementation", b =>
                {
                    b.HasOne("StuddGokApi.Models.StudyProgram", "StudyProgram")
                        .WithMany("ProgramImplementations")
                        .HasForeignKey("StudyProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudyProgram");
                });

            modelBuilder.Entity("StuddGokApi.Models.ProgramLocation", b =>
                {
                    b.HasOne("StuddGokApi.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.ProgramImplementation", "ProgramImplementation")
                        .WithMany("ProgramLocations")
                        .HasForeignKey("ProgramImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("ProgramImplementation");
                });

            modelBuilder.Entity("StuddGokApi.Models.StudentProgram", b =>
                {
                    b.HasOne("StuddGokApi.Models.ProgramImplementation", "ProgramImplementation")
                        .WithMany("StudentPrograms")
                        .HasForeignKey("ProgramImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.User", "User")
                        .WithMany("StudentPrograms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProgramImplementation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StuddGokApi.Models.TeacherCourse", b =>
                {
                    b.HasOne("StuddGokApi.Models.CourseImplementation", "CourseImplementation")
                        .WithMany("TeacherCourses")
                        .HasForeignKey("CourseImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.User", "User")
                        .WithMany("TeacherCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseImplementation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StuddGokApi.Models.TeacherProgram", b =>
                {
                    b.HasOne("StuddGokApi.Models.ProgramImplementation", "ProgramImplementation")
                        .WithMany("TeacherPrograms")
                        .HasForeignKey("ProgramImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.User", "User")
                        .WithMany("TeacherPrograms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProgramImplementation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StuddGokApi.Models.UserExamImplementation", b =>
                {
                    b.HasOne("StuddGokApi.Models.ExamImplementation", "ExamImplementation")
                        .WithMany("UserExamImplementation")
                        .HasForeignKey("ExamImplementationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StuddGokApi.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExamImplementation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StuddGokApi.Models.Venue", b =>
                {
                    b.HasOne("StuddGokApi.Models.Location", "Location")
                        .WithMany("Venues")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("StuddGokApi.Models.Assignment", b =>
                {
                    b.Navigation("AssignmentResults");
                });

            modelBuilder.Entity("StuddGokApi.Models.Course", b =>
                {
                    b.Navigation("CoursesImplementations");
                });

            modelBuilder.Entity("StuddGokApi.Models.CourseImplementation", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("Exams");

                    b.Navigation("Lectures");

                    b.Navigation("ProgramCourses");

                    b.Navigation("TeacherCourses");
                });

            modelBuilder.Entity("StuddGokApi.Models.Exam", b =>
                {
                    b.Navigation("ExamGroups");

                    b.Navigation("ExamImplementation");

                    b.Navigation("ExamResults");
                });

            modelBuilder.Entity("StuddGokApi.Models.ExamImplementation", b =>
                {
                    b.Navigation("UserExamImplementation");
                });

            modelBuilder.Entity("StuddGokApi.Models.Lecture", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("LectureVenues");
                });

            modelBuilder.Entity("StuddGokApi.Models.Location", b =>
                {
                    b.Navigation("Venues");
                });

            modelBuilder.Entity("StuddGokApi.Models.ProgramImplementation", b =>
                {
                    b.Navigation("ProgramCourses");

                    b.Navigation("ProgramLocations");

                    b.Navigation("StudentPrograms");

                    b.Navigation("TeacherPrograms");
                });

            modelBuilder.Entity("StuddGokApi.Models.StudyProgram", b =>
                {
                    b.Navigation("ProgramImplementations");
                });

            modelBuilder.Entity("StuddGokApi.Models.User", b =>
                {
                    b.Navigation("AssignmentResults");

                    b.Navigation("Attendances");

                    b.Navigation("ExamGroups");

                    b.Navigation("LectureVenues");

                    b.Navigation("StudentPrograms");

                    b.Navigation("TeacherCourses");

                    b.Navigation("TeacherPrograms");
                });

            modelBuilder.Entity("StuddGokApi.Models.Venue", b =>
                {
                    b.Navigation("ExamImplementations");

                    b.Navigation("LectureVenues");
                });
#pragma warning restore 612, 618
        }
    }
}
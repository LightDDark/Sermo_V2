﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(WebApplication1Context))]
    partial class WebApplication1ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("LogUser", b =>
                {
                    b.Property<string>("LogsstringId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UsersName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("LogsstringId", "UsersName");

                    b.HasIndex("UsersName");

                    b.ToTable("LogUser");
                });

            modelBuilder.Entity("Sermo_WAPI_Trial2.Log", b =>
                {
                    b.Property<string>("stringId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("stringId");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Sermo_WAPI_Trial2.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LogstringId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorName");

                    b.HasIndex("LogstringId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Sermo_WAPI_Trial2.User", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Name");

                    b.HasIndex("UserName");

                    b.ToTable("User");
                });

            modelBuilder.Entity("LogUser", b =>
                {
                    b.HasOne("Sermo_WAPI_Trial2.Log", null)
                        .WithMany()
                        .HasForeignKey("LogsstringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sermo_WAPI_Trial2.User", null)
                        .WithMany()
                        .HasForeignKey("UsersName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sermo_WAPI_Trial2.Message", b =>
                {
                    b.HasOne("Sermo_WAPI_Trial2.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sermo_WAPI_Trial2.Log", "Log")
                        .WithMany("Messages")
                        .HasForeignKey("LogstringId");

                    b.Navigation("Author");

                    b.Navigation("Log");
                });

            modelBuilder.Entity("Sermo_WAPI_Trial2.User", b =>
                {
                    b.HasOne("Sermo_WAPI_Trial2.User", null)
                        .WithMany("Contacts")
                        .HasForeignKey("UserName");
                });

            modelBuilder.Entity("Sermo_WAPI_Trial2.Log", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Sermo_WAPI_Trial2.User", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}

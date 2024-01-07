﻿// <auto-generated />
using System;
using Di2P1G3.Authentication.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Di2P1G3.Authentication.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210920141432_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.AccessToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdUser");

                    b.HasIndex("UserId1");

                    b.ToTable("AccessTokens");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.ClientApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ClientApplications");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AccessTokenId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdAccessToken")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AccessTokenId1");

                    b.HasIndex("IdAccessToken");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password_salt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.UserApplication", b =>
                {
                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdClientApplication")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("IdUser", "IdClientApplication");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("IdClientApplication");

                    b.HasIndex("UserId");

                    b.ToTable("UserApplications");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.AccessToken", b =>
                {
                    b.HasOne("Di2P1G3.Authentication.SharedKernel.User", null)
                        .WithMany("Tokens")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Di2P1G3.Authentication.SharedKernel.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.RefreshToken", b =>
                {
                    b.HasOne("Di2P1G3.Authentication.SharedKernel.AccessToken", "AccessToken")
                        .WithMany()
                        .HasForeignKey("AccessTokenId1");

                    b.HasOne("Di2P1G3.Authentication.SharedKernel.AccessToken", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("IdAccessToken")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccessToken");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.UserApplication", b =>
                {
                    b.HasOne("Di2P1G3.Authentication.SharedKernel.ClientApplication", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");

                    b.HasOne("Di2P1G3.Authentication.SharedKernel.User", null)
                        .WithMany("Applications")
                        .HasForeignKey("IdClientApplication")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Di2P1G3.Authentication.SharedKernel.ClientApplication", null)
                        .WithMany("Users")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Di2P1G3.Authentication.SharedKernel.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Application");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.AccessToken", b =>
                {
                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.ClientApplication", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Di2P1G3.Authentication.SharedKernel.User", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
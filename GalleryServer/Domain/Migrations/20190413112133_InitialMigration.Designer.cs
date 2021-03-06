﻿// <auto-generated />
using System;
using Domain.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20190413112133_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Models.ImageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Models.Models.RoleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("Models.Models.ScoreModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.ToTable("Scores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Value = 1
                        },
                        new
                        {
                            Id = 2,
                            Value = 2
                        },
                        new
                        {
                            Id = 3,
                            Value = 3
                        },
                        new
                        {
                            Id = 4,
                            Value = 4
                        },
                        new
                        {
                            Id = 5,
                            Value = 5
                        },
                        new
                        {
                            Id = 6,
                            Value = 6
                        },
                        new
                        {
                            Id = 7,
                            Value = 7
                        },
                        new
                        {
                            Id = 8,
                            Value = 8
                        },
                        new
                        {
                            Id = 9,
                            Value = 9
                        },
                        new
                        {
                            Id = 10,
                            Value = 10
                        });
                });

            modelBuilder.Entity("Models.Models.TagModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Models.Models.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password");

                    b.Property<int>("RoleId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=",
                            RoleId = 1,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Password = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=",
                            RoleId = 2,
                            UserName = "user"
                        });
                });

            modelBuilder.Entity("Models.Models.UserToImageScore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId");

                    b.Property<int>("ScoreId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("ScoreId");

                    b.HasIndex("UserId");

                    b.ToTable("UserToImageScores");
                });

            modelBuilder.Entity("Models.Models.UserToImageTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageId");

                    b.Property<int>("TagId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("UserToImageTags");
                });

            modelBuilder.Entity("Models.Models.UserModel", b =>
                {
                    b.HasOne("Models.Models.RoleModel", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.UserToImageScore", b =>
                {
                    b.HasOne("Models.Models.ImageModel", "Image")
                        .WithMany("UserToImageScores")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.ScoreModel", "Score")
                        .WithMany("UserToImageScores")
                        .HasForeignKey("ScoreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.UserModel", "User")
                        .WithMany("UserToImageScores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Models.UserToImageTag", b =>
                {
                    b.HasOne("Models.Models.ImageModel", "Image")
                        .WithMany("UserToImageTags")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.TagModel", "Tag")
                        .WithMany("UserToImageTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Models.UserModel", "User")
                        .WithMany("UserToImageTags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}

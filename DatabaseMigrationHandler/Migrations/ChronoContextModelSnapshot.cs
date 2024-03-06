﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

#nullable disable

namespace DatabaseMigrationHandler.Migrations
{
    [DbContext(typeof(ChronoContext))]
    partial class ChronoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.1.24081.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Repository.Models.TimeManagement.TimeRegistration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("TopicId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("TimeRegistrations");
                });

            modelBuilder.Entity("Repository.Models.TimeManagement.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Repository.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Changed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("HashAlgorithmType")
                        .HasColumnType("integer");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("Salt")
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Repository.Models.TimeManagement.TimeRegistration", b =>
                {
                    b.HasOne("Repository.Models.TimeManagement.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.HasOne("Repository.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}

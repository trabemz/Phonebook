﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Phonebook.Models;

namespace Phonebook.Migrations
{
    [DbContext(typeof(PhonebookContext))]
    [Migration("20200918132318_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Phonebook.Models.Location.TerritorialUnit", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Locations");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TerritorialUnit");
                });

            modelBuilder.Entity("Phonebook.Models.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("MicrodistrictId")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("Phonebook.Models.Location.District", b =>
                {
                    b.HasBaseType("Phonebook.Models.Location.TerritorialUnit");

                    b.HasDiscriminator().HasValue("District");
                });

            modelBuilder.Entity("Phonebook.Models.Location.Microdistrict", b =>
                {
                    b.HasBaseType("Phonebook.Models.Location.TerritorialUnit");

                    b.Property<int?>("DistrictId")
                        .HasColumnType("int");

                    b.HasIndex("DistrictId");

                    b.HasDiscriminator().HasValue("Microdistrict");
                });

            modelBuilder.Entity("Phonebook.Models.Location.Microdistrict", b =>
                {
                    b.HasOne("Phonebook.Models.Location.District", "District")
                        .WithMany("Microdistricts")
                        .HasForeignKey("DistrictId");
                });
#pragma warning restore 612, 618
        }
    }
}

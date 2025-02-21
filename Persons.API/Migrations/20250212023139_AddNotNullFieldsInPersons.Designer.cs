﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persons.API.Database;

#nullable disable

namespace Persons.API.Migrations
{
    [DbContext(typeof(PersonsDbContext))]
    [Migration("20250212023139_AddNotNullFieldsInPersons")]
    partial class AddNotNullFieldsInPersons
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("Persons.API.Database.Entities.PersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_date");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_by");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("dni");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_by");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("persons");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Customers.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Customers.Data.Migrations
{
    [DbContext(typeof(CustomersContext))]
    partial class CustomersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Customers.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Customers.Domain.Entities.Customer", b =>
                {
                    b.OwnsOne("SharedBase.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("CustomerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("First")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.Property<string>("Middle")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.Property<string>("Sur")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

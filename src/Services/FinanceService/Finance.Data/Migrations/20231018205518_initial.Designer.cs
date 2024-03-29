﻿// <auto-generated />
using System;
using Finance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Finance.Data.Migrations
{
    [DbContext(typeof(FinanceContext))]
    [Migration("20231018205518_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Finance.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountBearerType")
                        .HasColumnType("int");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Finance.Domain.Entities.Ledger", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Narration")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactingUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TxnRefrence")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TxnTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Ledgers");
                });

            modelBuilder.Entity("Finance.Domain.Entities.Account", b =>
                {
                    b.OwnsOne("SharedBase.ValueObjects.Money", "AccountTransactionLimit", b1 =>
                        {
                            b1.Property<Guid>("AccountId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,4)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("nvarchar(6)");

                            b1.Property<DateTime>("Time")
                                .HasColumnType("datetime2");

                            b1.HasKey("AccountId");

                            b1.ToTable("Accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");
                        });

                    b.Navigation("AccountTransactionLimit")
                        .IsRequired();
                });

            modelBuilder.Entity("Finance.Domain.Entities.Ledger", b =>
                {
                    b.HasOne("Finance.Domain.Entities.Account", "Account")
                        .WithMany("Ledgers")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SharedBase.ValueObjects.Money", "Credit", b1 =>
                        {
                            b1.Property<Guid>("LedgerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,4)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("nvarchar(6)");

                            b1.Property<DateTime>("Time")
                                .HasColumnType("datetime2");

                            b1.HasKey("LedgerId");

                            b1.ToTable("Ledgers");

                            b1.WithOwner()
                                .HasForeignKey("LedgerId");
                        });

                    b.OwnsOne("SharedBase.ValueObjects.Money", "Debit", b1 =>
                        {
                            b1.Property<Guid>("LedgerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("decimal(18,4)");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasMaxLength(6)
                                .HasColumnType("nvarchar(6)");

                            b1.Property<DateTime>("Time")
                                .HasColumnType("datetime2");

                            b1.HasKey("LedgerId");

                            b1.ToTable("Ledgers");

                            b1.WithOwner()
                                .HasForeignKey("LedgerId");
                        });

                    b.Navigation("Account");

                    b.Navigation("Credit")
                        .IsRequired();

                    b.Navigation("Debit")
                        .IsRequired();
                });

            modelBuilder.Entity("Finance.Domain.Entities.Account", b =>
                {
                    b.Navigation("Ledgers");
                });
#pragma warning restore 612, 618
        }
    }
}

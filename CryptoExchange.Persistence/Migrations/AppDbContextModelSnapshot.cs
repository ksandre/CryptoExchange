﻿// <auto-generated />
using System;
using CryptoExchange.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CryptoExchange.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CryptoExchange.Domain.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2024, 1, 27, 1, 21, 18, 462, DateTimeKind.Utc).AddTicks(5535),
                            DateModified = new DateTime(2024, 1, 27, 1, 21, 18, 462, DateTimeKind.Utc).AddTicks(5573),
                            Name = "USDT"
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(2024, 1, 27, 1, 21, 18, 462, DateTimeKind.Utc).AddTicks(5576),
                            DateModified = new DateTime(2024, 1, 27, 1, 21, 18, 462, DateTimeKind.Utc).AddTicks(5577),
                            Name = "BTC"
                        });
                });

            modelBuilder.Entity("CryptoExchange.Domain.ExchangeRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<bool?>("Approved")
                        .HasColumnType("boolean");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("boolean");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<int>("CurrencyTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("RequestingCustomerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("ExchangeRequests");
                });

            modelBuilder.Entity("CryptoExchange.Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("CurrencyId")
                        .HasColumnType("integer");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CryptoExchange.Domain.ExchangeRequest", b =>
                {
                    b.HasOne("CryptoExchange.Domain.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("CryptoExchange.Domain.Order", b =>
                {
                    b.HasOne("CryptoExchange.Domain.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartRefund.Infra.Context;

#nullable disable

namespace SmartRefund.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240316204408_createDatabase")]
    partial class createDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("SmartRefund.Domain.Models.Event", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("HashCode")
                        .HasColumnType("TEXT");

                    b.Property<uint?>("ReceiptEventSourceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptEventSourceId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("SmartRefund.Domain.Models.InternalReceipt", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<uint>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UniqueHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("InternalReceipt");
                });

            modelBuilder.Entity("SmartRefund.Domain.Models.RawVisionReceipt", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<uint>("InternalReceiptId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IsReceipt")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsTranslated")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Total")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UniqueHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InternalReceiptId");

                    b.ToTable("RawVisionReceipt");
                });

            modelBuilder.Entity("SmartRefund.Domain.Models.ReceiptEventSource", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentStatus")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("InternalReceiptId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("RawVisionReceiptId")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("TranslatedVisionReceiptId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UniqueHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InternalReceiptId");

                    b.HasIndex("RawVisionReceiptId");

                    b.HasIndex("TranslatedVisionReceiptId");

                    b.ToTable("ReceiptEventSource");
                });

            modelBuilder.Entity("SmartRefund.Domain.Models.TranslatedVisionReceipt", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsReceipt")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("RawVisionReceiptId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Total")
                        .HasColumnType("TEXT");

                    b.Property<string>("UniqueHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RawVisionReceiptId");

                    b.ToTable("TranslatedVisionReceipt");
                });

            modelBuilder.Entity("SmartRefund.Domain.Models.Event", b =>
                {
                    b.HasOne("SmartRefund.Domain.Models.ReceiptEventSource", null)
                        .WithMany("Events")
                        .HasForeignKey("ReceiptEventSourceId");
                });

            modelBuilder.Entity("SmartRefund.Domain.Models.RawVisionReceipt", b =>
                {
                    b.HasOne("SmartRefund.Domain.Models.InternalReceipt", "InternalReceipt")
                        .WithMany()
                        .HasForeignKey("InternalReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InternalReceipt");
                });

            modelBuilder.Entity("SmartRefund.Domain.Models.ReceiptEventSource", b =>
                {
                    b.HasOne("SmartRefund.Domain.Models.InternalReceipt", "InternalReceipt")
                        .WithMany()
                        .HasForeignKey("InternalReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartRefund.Domain.Models.RawVisionReceipt", "RawVisionReceipt")
                        .WithMany()
                        .HasForeignKey("RawVisionReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartRefund.Domain.Models.TranslatedVisionReceipt", "TranslatedVisionReceipt")
                        .WithMany()
                        .HasForeignKey("TranslatedVisionReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InternalReceipt");

                    b.Navigation("RawVisionReceipt");

                    b.Navigation("TranslatedVisionReceipt");
                });

            modelBuilder.Entity("SmartRefund.Domain.Models.TranslatedVisionReceipt", b =>
                {
                    b.HasOne("SmartRefund.Domain.Models.RawVisionReceipt", "RawVisionReceipt")
                        .WithMany()
                        .HasForeignKey("RawVisionReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RawVisionReceipt");
                });

            modelBuilder.Entity("SmartRefund.Domain.Models.ReceiptEventSource", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}

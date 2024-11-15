﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Supply.Repository;

#nullable disable

namespace Supply.Api.Migrations
{
    [DbContext(typeof(SupplyDbContext))]
    [Migration("20240211141931_AllUpdated1")]
    partial class AllUpdated1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Supply.Repository.Model.Category", b =>
                {
                    b.Property<string>("Category_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Category_Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Supply.Repository.Model.Order", b =>
                {
                    b.Property<int>("Order_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Order_Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tracking_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Order_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Supply.Repository.Model.Product", b =>
                {
                    b.Property<string>("Product_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("User_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Product_Id");

                    b.HasIndex("Category_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Supply.Repository.Model.ProductOrderList", b =>
                {
                    b.Property<int>("Product_Order_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Product_Order_Id"));

                    b.Property<int>("Order_Id")
                        .HasColumnType("int");

                    b.Property<string>("Product_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Stock_Quantity")
                        .HasColumnType("int");

                    b.HasKey("Product_Order_Id");

                    b.HasIndex("Order_Id");

                    b.HasIndex("Product_Id");

                    b.ToTable("ProductOrdersList");
                });

            modelBuilder.Entity("Supply.Repository.Model.Supplier", b =>
                {
                    b.Property<string>("User_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("User_Id");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("Supply.Repository.Model.TransactionalOutbox", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Messaggio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tabella")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransactionalOutbox", (string)null);
                });

            modelBuilder.Entity("Supply.Repository.Model.Order", b =>
                {
                    b.HasOne("Supply.Repository.Model.Supplier", "Supplier")
                        .WithMany("Orders")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Supply.Repository.Model.Product", b =>
                {
                    b.HasOne("Supply.Repository.Model.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Supply.Repository.Model.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Supply.Repository.Model.ProductOrderList", b =>
                {
                    b.HasOne("Supply.Repository.Model.Order", "Order")
                        .WithMany("Products")
                        .HasForeignKey("Order_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Supply.Repository.Model.Product", "Product")
                        .WithMany("ProductOrderList")
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Supply.Repository.Model.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Supply.Repository.Model.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Supply.Repository.Model.Product", b =>
                {
                    b.Navigation("ProductOrderList");
                });

            modelBuilder.Entity("Supply.Repository.Model.Supplier", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}

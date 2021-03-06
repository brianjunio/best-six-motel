﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Database;

namespace Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180420015922_update")]
    partial class update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.5");

            modelBuilder.Entity("Database.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("CompanyName");

                    b.Property<string>("Owner");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("CompanyID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Database.Customer", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastEdit");

                    b.Property<string>("LastName");

                    b.Property<string>("PaymentInfo");

                    b.Property<string>("PhoneNo");

                    b.Property<int?>("RoomNo");

                    b.Property<string>("State");

                    b.Property<string>("Street");

                    b.Property<string>("Zip");

                    b.Property<int>("lastRoom");

                    b.HasKey("ID");

                    b.HasIndex("RoomNo")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Database.Employee", b =>
                {
                    b.Property<string>("Username")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmpType");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("Username");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Database.Room", b =>
                {
                    b.Property<int>("RoomNo")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BedType");

                    b.Property<DateTime>("Checkin");

                    b.Property<DateTime>("Checkout");

                    b.Property<double>("Left");

                    b.Property<string>("Legend");

                    b.Property<int>("NoOfBeds");

                    b.Property<double>("Price");

                    b.Property<string>("Smoking");

                    b.Property<double>("Top");

                    b.HasKey("RoomNo");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Database.Transactions", b =>
                {
                    b.Property<int>("TrNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateModified");

                    b.Property<string>("ID");

                    b.Property<int>("RoomNo");

                    b.HasKey("TrNumber");

                    b.HasIndex("ID");

                    b.HasIndex("RoomNo");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Database.Customer", b =>
                {
                    b.HasOne("Database.Room", "Room")
                        .WithOne("Customer")
                        .HasForeignKey("Database.Customer", "RoomNo");
                });

            modelBuilder.Entity("Database.Transactions", b =>
                {
                    b.HasOne("Database.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("ID");

                    b.HasOne("Database.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomNo")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web_API.Services;
using WebShop;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(ItemsContext))]
    [Migration("20220531143419_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Web_API.Models.Contact", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Last")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Server")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Web_API.Models.Message", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Created")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Sent")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("WebApi.Models.Conversation", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.HasIndex("LastId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("WebApi.Models.Conversation", b =>
                {
                    b.HasOne("Web_API.Models.Message", "Last")
                        .WithMany()
                        .HasForeignKey("LastId");

                    b.Navigation("Last");
                });
#pragma warning restore 612, 618
        }
    }
}

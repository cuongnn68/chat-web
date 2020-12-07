﻿// <auto-generated />
using System;
using DiscordRipoff.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DiscordRipoff.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20201206092747_AddDefaultValueRoleRoomUser")]
    partial class AddDefaultValueRoleRoomUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DiscordRipoff.Entities.JWTBlacklist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("JWT")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeExpired")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Blacklist");
                });

            modelBuilder.Entity("DiscordRipoff.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DayCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("DiscordRipoff.Entities.RoomMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomUserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoomUserId");

                    b.ToTable("RoomMessage");
                });

            modelBuilder.Entity("DiscordRipoff.Entities.RoomUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeJoin")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("RoomUsers");
                });

            modelBuilder.Entity("DiscordRipoff.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DiscordRipoff.Entities.RoomMessage", b =>
                {
                    b.HasOne("DiscordRipoff.Entities.RoomUser", "RoomUser")
                        .WithMany("RoomMessages")
                        .HasForeignKey("RoomUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomUser");
                });

            modelBuilder.Entity("DiscordRipoff.Entities.RoomUser", b =>
                {
                    b.HasOne("DiscordRipoff.Entities.Room", "Room")
                        .WithMany("RoomUsers")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DiscordRipoff.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DiscordRipoff.Entities.Room", b =>
                {
                    b.Navigation("RoomUsers");
                });

            modelBuilder.Entity("DiscordRipoff.Entities.RoomUser", b =>
                {
                    b.Navigation("RoomMessages");
                });
#pragma warning restore 612, 618
        }
    }
}

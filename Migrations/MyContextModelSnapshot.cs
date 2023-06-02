﻿// <auto-generated />
using System;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace flashcards.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("flashcards.Models.FlashCardDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Expression")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("OppositeExpression")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("StackId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Expression");

                    b.HasIndex("StackId");

                    b.ToTable("FLASHCARDS", (string)null);
                });

            modelBuilder.Entity("flashcards.Models.Stacks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("STACKS", (string)null);
                });

            modelBuilder.Entity("flashcards.Models.Study", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FlashCardId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HaveSucess")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("StackId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FlashCardId");

                    b.HasIndex("StackId");

                    b.ToTable("STUDIES", (string)null);
                });

            modelBuilder.Entity("flashcards.Models.FlashCardDb", b =>
                {
                    b.HasOne("flashcards.Models.Stacks", "Stack")
                        .WithMany("FlashCards")
                        .HasForeignKey("StackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stack");
                });

            modelBuilder.Entity("flashcards.Models.Study", b =>
                {
                    b.HasOne("flashcards.Models.FlashCardDb", "FlashCard")
                        .WithMany("Studies")
                        .HasForeignKey("FlashCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("flashcards.Models.Stacks", "Stack")
                        .WithMany("Studies")
                        .HasForeignKey("StackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FlashCard");

                    b.Navigation("Stack");
                });

            modelBuilder.Entity("flashcards.Models.FlashCardDb", b =>
                {
                    b.Navigation("Studies");
                });

            modelBuilder.Entity("flashcards.Models.Stacks", b =>
                {
                    b.Navigation("FlashCards");

                    b.Navigation("Studies");
                });
#pragma warning restore 612, 618
        }
    }
}
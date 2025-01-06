﻿// <auto-generated />
using DAOEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAOEF.Migrations
{
    [DbContext(typeof(DAOSqlite))]
    [Migration("20250105170818_UpdateProducerTable")]
    partial class UpdateProducerTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("DAOEF.BO.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("DiskSpace")
                        .HasColumnType("REAL");

                    b.Property<int>("Genre")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("ProducerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProducerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("DAOEF.BO.Producer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Continent")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EstYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("DAOEF.BO.Game", b =>
                {
                    b.HasOne("DAOEF.BO.Producer", "Producer")
                        .WithMany("Games")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("DAOEF.BO.Producer", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
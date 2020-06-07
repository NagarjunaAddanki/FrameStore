﻿// <auto-generated />
using System;
using FrameStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FrameStore.Data.Migrations
{
    [DbContext(typeof(FrameStoreContext))]
    partial class FrameStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FrameStore.Data.Brand", b =>
                {
                    b.Property<Guid>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("FrameStore.Data.Collection", b =>
                {
                    b.Property<Guid>("CollectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CollectionId");

                    b.HasIndex("BrandId");

                    b.ToTable("Collections");
                });

            modelBuilder.Entity("FrameStore.Data.Frame", b =>
                {
                    b.Property<Guid>("FrameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Bridge")
                        .HasColumnType("float");

                    b.Property<string>("FrameColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(75)")
                        .HasMaxLength(75);

                    b.Property<double>("Horizontal")
                        .HasColumnType("float");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("StyleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Vertical")
                        .HasColumnType("float");

                    b.HasKey("FrameId");

                    b.HasIndex("StyleId");

                    b.ToTable("Frames");
                });

            modelBuilder.Entity("FrameStore.Data.FrameMaterial", b =>
                {
                    b.Property<Guid>("FrameMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FrameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaterialId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FrameMaterialId");

                    b.HasIndex("FrameId");

                    b.HasIndex("MaterialId");

                    b.ToTable("FrameMaterials");
                });

            modelBuilder.Entity("FrameStore.Data.FrameTracingRadii", b =>
                {
                    b.Property<Guid>("FrameTracingRadiusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FrameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Radius")
                        .HasColumnType("float");

                    b.HasKey("FrameTracingRadiusId");

                    b.HasIndex("FrameId");

                    b.ToTable("FrameTracingRadii");
                });

            modelBuilder.Entity("FrameStore.Data.Material", b =>
                {
                    b.Property<Guid>("MaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(75)")
                        .HasMaxLength(75);

                    b.HasKey("MaterialId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("FrameStore.Data.Style", b =>
                {
                    b.Property<Guid>("StyleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CollectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("StyleId");

                    b.HasIndex("CollectionId");

                    b.ToTable("Styles");
                });

            modelBuilder.Entity("FrameStore.Data.Collection", b =>
                {
                    b.HasOne("FrameStore.Data.Brand", "Brand")
                        .WithMany("Collections")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FrameStore.Data.Frame", b =>
                {
                    b.HasOne("FrameStore.Data.Style", "Style")
                        .WithMany("Frames")
                        .HasForeignKey("StyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FrameStore.Data.FrameMaterial", b =>
                {
                    b.HasOne("FrameStore.Data.Frame", "Frame")
                        .WithMany("Materials")
                        .HasForeignKey("FrameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FrameStore.Data.Material", "Material")
                        .WithMany()
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FrameStore.Data.FrameTracingRadii", b =>
                {
                    b.HasOne("FrameStore.Data.Frame", "Frame")
                        .WithMany("TracingRadii")
                        .HasForeignKey("FrameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FrameStore.Data.Style", b =>
                {
                    b.HasOne("FrameStore.Data.Collection", "Collection")
                        .WithMany("Styles")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
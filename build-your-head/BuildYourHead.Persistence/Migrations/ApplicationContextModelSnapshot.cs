﻿// <auto-generated />
using BuildYourHead.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuildYourHead.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BuildYourHead.Persistence.Entities.ImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("Content");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Path");

                    b.HasKey("Id");

                    b.ToTable("Images", (string)null);
                });

            modelBuilder.Entity("BuildYourHead.Persistence.Entities.ProductEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<double>("Carbohydrates")
                        .HasColumnType("double")
                        .HasColumnName("Carbohydrates");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("Description");

                    b.Property<double>("Fats")
                        .HasColumnType("double")
                        .HasColumnName("Fats");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Name");

                    b.Property<double>("Nutrition")
                        .HasColumnType("double")
                        .HasColumnName("Nutrition");

                    b.Property<double>("Proteins")
                        .HasColumnType("double")
                        .HasColumnName("Proteins");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("BuildYourHead.Persistence.Entities.ProductImageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ImagePath");

                    b.Property<bool>("IsPrimary")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("IsPrimary");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage", (string)null);
                });

            modelBuilder.Entity("BuildYourHead.Persistence.Entities.RecipeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("Description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Recipe", (string)null);
                });

            modelBuilder.Entity("BuildYourHead.Persistence.Entities.RecipeProductEntity", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int")
                        .HasColumnName("RecipeId");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductId");

                    b.HasKey("RecipeId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("RecipeProduct", (string)null);
                });

            modelBuilder.Entity("BuildYourHead.Persistence.Entities.ProductImageEntity", b =>
                {
                    b.HasOne("BuildYourHead.Persistence.Entities.ProductEntity", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BuildYourHead.Persistence.Entities.RecipeProductEntity", b =>
                {
                    b.HasOne("BuildYourHead.Persistence.Entities.ProductEntity", "Product")
                        .WithMany("RecipeProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuildYourHead.Persistence.Entities.RecipeEntity", "Recipe")
                        .WithMany("RecipeProducts")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("BuildYourHead.Persistence.Entities.ProductEntity", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("RecipeProducts");
                });

            modelBuilder.Entity("BuildYourHead.Persistence.Entities.RecipeEntity", b =>
                {
                    b.Navigation("RecipeProducts");
                });
#pragma warning restore 612, 618
        }
    }
}

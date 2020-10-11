﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SlingshotApplication.Context;

namespace SlingshotApplication.Migrations
{
    [DbContext(typeof(SlingshotContext))]
    [Migration("20201010191840_Initial-Create")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SlingshotApplication.Models.Edge", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SourceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TargetId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SourceId");

                    b.HasIndex("TargetId");

                    b.ToTable("Edges");
                });

            modelBuilder.Entity("SlingshotApplication.Models.Node", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("posX")
                        .HasColumnType("real");

                    b.Property<float>("posY")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Nodes");
                });

            modelBuilder.Entity("SlingshotApplication.Models.Edge", b =>
                {
                    b.HasOne("SlingshotApplication.Models.Node", "Source")
                        .WithMany()
                        .HasForeignKey("SourceId");

                    b.HasOne("SlingshotApplication.Models.Node", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId");
                });
#pragma warning restore 612, 618
        }
    }
}

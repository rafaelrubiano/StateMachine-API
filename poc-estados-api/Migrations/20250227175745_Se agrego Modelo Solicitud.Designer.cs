﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using poc_estados_api.Data;

#nullable disable

namespace poc_estados_api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250227175745_Se agrego Modelo Solicitud")]
    partial class SeagregoModeloSolicitud
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("poc_estados_api.Models.Accion", b =>
                {
                    b.Property<int>("IdAccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAccion"));

                    b.Property<DateTime?>("Creado")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modificado")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModificadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAccion");

                    b.ToTable("Acciones");
                });

            modelBuilder.Entity("poc_estados_api.Models.AccionEstado", b =>
                {
                    b.Property<int>("IdAccion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAccion"));

                    b.Property<DateTime?>("Creado")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GeneraEvento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdEstadoDesde")
                        .HasColumnType("int");

                    b.Property<int>("IdEstadoHasta")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificado")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModificadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAccion");

                    b.ToTable("AccionesEstado");
                });

            modelBuilder.Entity("poc_estados_api.Models.Estado", b =>
                {
                    b.Property<int>("IdEstado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEstado"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Creado")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescripcionDiagrama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EsFinal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Modificado")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModificadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short?>("Orden")
                        .HasColumnType("smallint");

                    b.HasKey("IdEstado");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("poc_estados_api.Models.Evento", b =>
                {
                    b.Property<int>("IdEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEvento"));

                    b.Property<DateTime?>("Creado")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdAccion")
                        .HasColumnType("int");

                    b.Property<long>("IdComun")
                        .HasColumnType("bigint");

                    b.Property<int>("IdEstadoDesde")
                        .HasColumnType("int");

                    b.Property<int>("IdEstadoHasta")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Modificado")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModificadoPor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEvento");

                    b.ToTable("Eventos");
                });

            modelBuilder.Entity("poc_estados_api.Models.Solicitud", b =>
                {
                    b.Property<int>("IdSolicitud")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSolicitud"));

                    b.Property<DateTime?>("FechaHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("UsuarioNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioRed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSolicitud");

                    b.ToTable("Solicitudes");
                });
#pragma warning restore 612, 618
        }
    }
}

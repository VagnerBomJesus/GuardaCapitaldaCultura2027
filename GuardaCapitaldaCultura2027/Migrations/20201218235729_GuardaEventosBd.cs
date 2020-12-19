﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GuardaCapitaldaCultura2027.Migrations
{
    public partial class GuardaEventosBd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    ContactoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Sobrenome = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Assunto = table.Column<string>(maxLength: 100, nullable: false),
                    Mensagem = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.ContactoId);
                });

            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    EventosId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(maxLength: 500, nullable: false),
                    Data_realizacao = table.Column<DateTime>(nullable: false),
                    Lotacao_max = table.Column<int>(nullable: false),
                    Lotacao_Ocupada = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.EventosId);
                });

            migrationBuilder.CreateTable(
                name: "LugarEventos",
                columns: table => new
                {
                    LugarEventoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocupado = table.Column<bool>(nullable: false),
                    NumeroCadeira = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LugarEventos", x => x.LugarEventoId);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    MunicipioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    ImagemNome = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.MunicipioId);
                });

            migrationBuilder.CreateTable(
                name: "Turistas",
                columns: table => new
                {
                    TuristaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 20, nullable: false),
                    Sobrenome = table.Column<string>(maxLength: 50, nullable: false),
                    Contacto = table.Column<string>(maxLength: 15, nullable: true),
                    NIF = table.Column<string>(maxLength: 15, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turistas", x => x.TuristaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "LugarEventos");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Turistas");
        }
    }
}

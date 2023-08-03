using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TA_PROJECT_CyclecountAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptName = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    WERKS = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    LGNUM = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    Mltp = table.Column<int>(type: "int", nullable: false),
                    Idusin = table.Column<int>(type: "int", nullable: false),
                    Idusup = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SESAID = table.Column<string>(type: "Nvarchar(48)", nullable: false),
                    Name = table.Column<string>(type: "Nvarchar(128)", nullable: false),
                    Username = table.Column<string>(type: "Nvarchar(128)", nullable: false),
                    Password = table.Column<string>(type: "Nvarchar(128)", nullable: false),
                    DefaultDeptId = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(64)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Last_Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Depts_DefaultDeptId",
                        column: x => x.DefaultDeptId,
                        principalTable: "Depts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LICC",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LGNUM = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    LGTYP = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    LGPLA = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    IDATU = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    WERKS = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    MATNR = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    NIDAT = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    TTEXT = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    ABCIN = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    SALK3 = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    AGEING_DAYS = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    LQNUM = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    Idusin = table.Column<int>(type: "int", nullable: true),
                    Idusup = table.Column<int>(type: "int", nullable: true),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    Ymd8in = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ymd8up = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LICC", x => x.id);
                    table.ForeignKey(
                        name: "FK_LICC_Depts_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Depts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LICC_Users_Idusin",
                        column: x => x.Idusin,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LICC_Users_Idusup",
                        column: x => x.Idusup,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lx17",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mandt = table.Column<int>(type: "int", nullable: true),
                    Lgnum = table.Column<int>(type: "int", nullable: true),
                    Ivnum = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Ivpos = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lqnum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Nanum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Istat = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Nvers = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Iseit = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lgtyp = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lgpla = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Plpos = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Matnr = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Werks = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Charg = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    Letyp = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Anzle = table.Column<double>(type: "float", nullable: true),
                    Menge = table.Column<double>(type: "float", nullable: true),
                    Meins = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Menga = table.Column<double>(type: "float", nullable: true),
                    Altme = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Umrez = table.Column<int>(type: "int", nullable: true),
                    Umren = table.Column<int>(type: "int", nullable: true),
                    Bestq = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Sobkz = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    Sonum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Wdatu = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Gesme = table.Column<double>(type: "float", nullable: true),
                    Idatu = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Kzinv = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Irnum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Tanum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Tapos = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lenum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Vfdat = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lgort = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Uname = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Quinv = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Idqua = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Dbgez = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: true),
                    Lvorm = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Maktx = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Kreuz = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Pagno = table.Column<int>(type: "int", nullable: true),
                    Linno = table.Column<int>(type: "int", nullable: true),
                    Bemer = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Abwei = table.Column<double>(type: "float", nullable: true),
                    Vzchn = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Ausbu = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Bwkey = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Bukrs = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Waers = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Vprsv = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    Verpr = table.Column<double>(type: "float", nullable: true),
                    Stprs = table.Column<double>(type: "float", nullable: true),
                    Peinh = table.Column<int>(type: "int", nullable: true),
                    Cwert = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Dwert = table.Column<double>(type: "float", nullable: true),
                    Noval = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lsonr = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Cntstatus = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    Statustype = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    Idusin = table.Column<int>(type: "int", nullable: true),
                    Idusup = table.Column<int>(type: "int", nullable: true),
                    PlantID = table.Column<int>(type: "int", nullable: false),
                    FromLICC = table.Column<bool>(type: "bit", nullable: false),
                    Ymd8in = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ymd8up = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lx17", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lx17_Depts_PlantID",
                        column: x => x.PlantID,
                        principalTable: "Depts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lx17_Users_Idusin",
                        column: x => x.Idusin,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lx17_Users_Idusup",
                        column: x => x.Idusup,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatClass",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    matnr = table.Column<string>(type: "Nvarchar(128)", nullable: false),
                    abcin = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    fmr = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    idusin = table.Column<int>(type: "int", nullable: true),
                    idusup = table.Column<int>(type: "int", nullable: true),
                    ymd8in = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ymd8up = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatClass", x => x.id);
                    table.ForeignKey(
                        name: "FK_MatClass_Depts_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Depts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatClass_Users_idusin",
                        column: x => x.idusin,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatClass_Users_idusup",
                        column: x => x.idusup,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatPrice",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    matnr = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    matdesc = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    ms = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    uom = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    type = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    pgr = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    mrpc = table.Column<string>(type: "nvarchar(48)", nullable: true),
                    price = table.Column<double>(type: "float", nullable: false),
                    cur = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    per = table.Column<double>(type: "float", nullable: false),
                    idusin = table.Column<int>(type: "int", nullable: true),
                    idusup = table.Column<int>(type: "int", nullable: true),
                    ymd8in = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ymd8up = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatPrice", x => x.id);
                    table.ForeignKey(
                        name: "FK_MatPrice_Depts_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Depts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatPrice_Users_idusin",
                        column: x => x.idusin,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatPrice_Users_idusup",
                        column: x => x.idusup,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDept",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DeptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDept", x => new { x.UserId, x.DeptId });
                    table.ForeignKey(
                        name: "FK_UserDept_Depts_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Depts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDept_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lx17Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mandt = table.Column<int>(type: "int", nullable: true),
                    Lgnum = table.Column<int>(type: "int", nullable: true),
                    Ivnum = table.Column<string>(type: "nvarchar(128)", nullable: true),
                    Ivpos = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lqnum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Nanum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Istat = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Nvers = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Iseit = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lgtyp = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lgpla = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Plpos = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Matnr = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Werks = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Charg = table.Column<string>(type: "nvarchar(16)", nullable: false),
                    Letyp = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Anzle = table.Column<double>(type: "float", nullable: true),
                    Menge = table.Column<double>(type: "float", nullable: true),
                    Meins = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Menga = table.Column<double>(type: "float", nullable: true),
                    Altme = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Umrez = table.Column<int>(type: "int", nullable: true),
                    Umren = table.Column<int>(type: "int", nullable: true),
                    Bestq = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Sobkz = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    Sonum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Wdatu = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Gesme = table.Column<double>(type: "float", nullable: true),
                    Idatu = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Kzinv = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Irnum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Tanum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Tapos = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lenum = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Vfdat = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lgort = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Uname = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Quinv = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Idqua = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Dbgez = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: true),
                    Lvorm = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Maktx = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Kreuz = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Pagno = table.Column<int>(type: "int", nullable: true),
                    Linno = table.Column<int>(type: "int", nullable: true),
                    Bemer = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Abwei = table.Column<double>(type: "float", nullable: true),
                    Vzchn = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Ausbu = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Bwkey = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Bukrs = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Waers = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Vprsv = table.Column<string>(type: "nvarchar(12)", nullable: false),
                    Verpr = table.Column<double>(type: "float", nullable: true),
                    Stprs = table.Column<double>(type: "float", nullable: true),
                    Peinh = table.Column<int>(type: "int", nullable: true),
                    Cwert = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Dwert = table.Column<double>(type: "float", nullable: true),
                    Noval = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Lsonr = table.Column<string>(type: "nvarchar(128)", nullable: false),
                    Cntstatus = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    Statustype = table.Column<string>(type: "nvarchar(48)", nullable: false),
                    Idusin = table.Column<int>(type: "int", nullable: true),
                    Idusup = table.Column<int>(type: "int", nullable: true),
                    Ymd8Log = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Logged_UserId = table.Column<int>(type: "int", nullable: true),
                    PlantID = table.Column<int>(type: "int", nullable: false),
                    Lx17Id = table.Column<int>(type: "int", nullable: true),
                    FromLICC = table.Column<bool>(type: "bit", nullable: false),
                    Ymd8in = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ymd8up = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lx17Log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lx17Log_Depts_PlantID",
                        column: x => x.PlantID,
                        principalTable: "Depts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lx17Log_Lx17_Lx17Id",
                        column: x => x.Lx17Id,
                        principalTable: "Lx17",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lx17Log_Users_Idusin",
                        column: x => x.Idusin,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lx17Log_Users_Idusup",
                        column: x => x.Idusup,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lx17Log_Users_Logged_UserId",
                        column: x => x.Logged_UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LICC_Idusin",
                table: "LICC",
                column: "Idusin");

            migrationBuilder.CreateIndex(
                name: "IX_LICC_Idusup",
                table: "LICC",
                column: "Idusup");

            migrationBuilder.CreateIndex(
                name: "IX_LICC_PlantId",
                table: "LICC",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Lx17_Idusin",
                table: "Lx17",
                column: "Idusin");

            migrationBuilder.CreateIndex(
                name: "IX_Lx17_Idusup",
                table: "Lx17",
                column: "Idusup");

            migrationBuilder.CreateIndex(
                name: "IX_Lx17_PlantID",
                table: "Lx17",
                column: "PlantID");

            migrationBuilder.CreateIndex(
                name: "IX_Lx17Log_Idusin",
                table: "Lx17Log",
                column: "Idusin");

            migrationBuilder.CreateIndex(
                name: "IX_Lx17Log_Idusup",
                table: "Lx17Log",
                column: "Idusup");

            migrationBuilder.CreateIndex(
                name: "IX_Lx17Log_Logged_UserId",
                table: "Lx17Log",
                column: "Logged_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lx17Log_Lx17Id",
                table: "Lx17Log",
                column: "Lx17Id");

            migrationBuilder.CreateIndex(
                name: "IX_Lx17Log_PlantID",
                table: "Lx17Log",
                column: "PlantID");

            migrationBuilder.CreateIndex(
                name: "IX_MatClass_idusin",
                table: "MatClass",
                column: "idusin");

            migrationBuilder.CreateIndex(
                name: "IX_MatClass_idusup",
                table: "MatClass",
                column: "idusup");

            migrationBuilder.CreateIndex(
                name: "IX_MatClass_PlantId",
                table: "MatClass",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_MatPrice_idusin",
                table: "MatPrice",
                column: "idusin");

            migrationBuilder.CreateIndex(
                name: "IX_MatPrice_idusup",
                table: "MatPrice",
                column: "idusup");

            migrationBuilder.CreateIndex(
                name: "IX_MatPrice_PlantId",
                table: "MatPrice",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDept_DeptId",
                table: "UserDept",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DefaultDeptId",
                table: "Users",
                column: "DefaultDeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LICC");

            migrationBuilder.DropTable(
                name: "Lx17Log");

            migrationBuilder.DropTable(
                name: "MatClass");

            migrationBuilder.DropTable(
                name: "MatPrice");

            migrationBuilder.DropTable(
                name: "UserDept");

            migrationBuilder.DropTable(
                name: "Lx17");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Depts");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}

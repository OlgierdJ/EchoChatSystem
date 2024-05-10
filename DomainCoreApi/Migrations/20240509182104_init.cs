using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountActivityStatus",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IconColor = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountActivityStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountCustomStatus",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomMessage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCustomStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BugReportReason",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugReportReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomStatusReportReason",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomStatusReportReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackReportReason",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackReportReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendship", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessageReportReason",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReportReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileReportReason",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileReportReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Theme",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordSetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatPinboard",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatPinboard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatPinboard_Chat_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    TimeLastLogon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", maxLength: 256, nullable: true),
                    ActivityStatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    CustomStatusId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountActivityStatus_ActivityStatusId",
                        column: x => x.ActivityStatusId,
                        principalTable: "AccountActivityStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Account_AccountCustomStatus_CustomStatusId",
                        column: x => x.CustomStatusId,
                        principalTable: "AccountCustomStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Account_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SecurityCredentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityCredentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityCredentials_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountBlock",
                columns: table => new
                {
                    BlockerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    BlockedId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeBlocked = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBlock", x => new { x.BlockerId, x.BlockedId });
                    table.ForeignKey(
                        name: "FK_AccountBlock_Account_BlockedId",
                        column: x => x.BlockedId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountBlock_Account_BlockerId",
                        column: x => x.BlockerId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountConnection",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ExternalProvider = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    InternalProvider = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ExternalToken = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    InternalToken = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountConnection_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMute", x => new { x.MuterId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_AccountMute_Account_MuterId",
                        column: x => x.MuterId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountMute_Account_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountNickname",
                columns: table => new
                {
                    AuthorId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNickname", x => new { x.AuthorId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_AccountNickname_Account_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountNickname_Account_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountNote",
                columns: table => new
                {
                    AuthorId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNote", x => new { x.AuthorId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_AccountNote_Account_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountNote_Account_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountProfile",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AvatarFileURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    BannerColor = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    About = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountProfile_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.RoleId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_AccountRole_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountSession",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SessionToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TimeStarted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeStopped = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSession", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSession_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSettings_Account_Id",
                        column: x => x.Id,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountSettings_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountSoundboardMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSoundboardMute", x => new { x.MuterId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_AccountSoundboardMute_Account_MuterId",
                        column: x => x.MuterId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountSoundboardMute_Account_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountViolation",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    IssuerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountViolation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountViolation_Account_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountViolation_Account_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BugReport",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ReporterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BugReport_Account_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatInvite",
                columns: table => new
                {
                    InviteCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    TotalUses = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    InviterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatInvite", x => x.InviteCode);
                    table.ForeignKey(
                        name: "FK_ChatInvite_Account_InviterId",
                        column: x => x.InviterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatInvite_Chat_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    AuthorId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MessageHolderId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ParentId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Account_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessage_ChatMessage_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Chat_MessageHolderId",
                        column: x => x.MessageHolderId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMute", x => new { x.MuterId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ChatMute_Account_MuterId",
                        column: x => x.MuterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMute_Chat_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatParticipancy",
                columns: table => new
                {
                    ParticipantId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeJoined = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatParticipancy", x => new { x.ParticipantId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ChatParticipancy_Account_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatParticipancy_Chat_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackReport",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ReporterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeedbackReport_Account_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FriendshipParticipancy",
                columns: table => new
                {
                    ParticipantId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeJoined = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipParticipancy", x => new { x.ParticipantId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_FriendshipParticipancy_Account_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendshipParticipancy_Friendship_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Friendship",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FriendSuggestion",
                columns: table => new
                {
                    ReceiverId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SuggestionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Declined = table.Column<bool>(type: "bit", nullable: false),
                    TimeSuggested = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendSuggestion", x => new { x.ReceiverId, x.SuggestionId });
                    table.ForeignKey(
                        name: "FK_FriendSuggestion_Account_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FriendSuggestion_Account_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OutgoingFriendRequest",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutgoingFriendRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutgoingFriendRequest_Account_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportedCustomStatus",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CustomMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportedCustomStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportedCustomStatus_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportedMessage",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    AuthorId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportedMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportedMessage_Account_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportedProfile",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarFileURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BannerColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportedProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportedProfile_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessibilitySettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SaturationPercent = table.Column<byte>(type: "tinyint", nullable: false),
                    ApplySaturationToCustomColors = table.Column<bool>(type: "bit", nullable: false),
                    AlwaysUnderlineLinks = table.Column<bool>(type: "bit", nullable: false),
                    RoleColorMode = table.Column<int>(type: "int", nullable: false),
                    SyncProfileThemes = table.Column<bool>(type: "bit", nullable: false),
                    SyncContrastSettings = table.Column<bool>(type: "bit", nullable: false),
                    SyncReducedMotionWithPC = table.Column<bool>(type: "bit", nullable: false),
                    ReducedMotion = table.Column<bool>(type: "bit", nullable: false),
                    AutoPlayGIFsOnAppFocus = table.Column<bool>(type: "bit", nullable: false),
                    PlayAnimatedEmojis = table.Column<bool>(type: "bit", nullable: false),
                    StickerAnimationMode = table.Column<int>(type: "int", nullable: false),
                    ShowSendMessageButton = table.Column<bool>(type: "bit", nullable: false),
                    UseLegacyChatInput = table.Column<bool>(type: "bit", nullable: false),
                    AllowTextToSpeech = table.Column<bool>(type: "bit", nullable: false),
                    TextToSpeechRate = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibilitySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessibilitySettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivitySettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DisplayCurrentActivityAsAStatusMessage = table.Column<bool>(type: "bit", nullable: false),
                    ShareActivityStatusOnLargeServerJoin = table.Column<bool>(type: "bit", nullable: false),
                    AllowFriendsToJoinGame = table.Column<bool>(type: "bit", nullable: false),
                    AllowVoiceChannelParticipantsToJoinGame = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivitySettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvancedSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DeveloperMode = table.Column<bool>(type: "bit", nullable: false),
                    HardwareAcceleration = table.Column<bool>(type: "bit", nullable: false),
                    AutoNavigateServerHome = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvancedSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppearanceSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ThemeId = table.Column<long>(type: "bigint", nullable: false),
                    InAppIcon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DarkSideBar = table.Column<bool>(type: "bit", nullable: false),
                    MessageDisplayMode = table.Column<int>(type: "int", nullable: false),
                    ShowAvatarsInCompactMode = table.Column<bool>(type: "bit", nullable: false),
                    PixelChatFontScale = table.Column<byte>(type: "tinyint", nullable: false),
                    PixelSpaceBetweenMessageGroupsScale = table.Column<byte>(type: "tinyint", nullable: false),
                    ZoomLevel = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppearanceSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppearanceSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppearanceSettings_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingInformation",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingInformation_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FriendRequestSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Everyone = table.Column<bool>(type: "bit", nullable: false),
                    FriendsOfFriends = table.Column<bool>(type: "bit", nullable: false),
                    ServerMembers = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequestSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequestSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeybindSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeybindSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeybindSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    FocusModeEnabled = table.Column<bool>(type: "bit", nullable: false),
                    DesktopNotification = table.Column<bool>(type: "bit", nullable: false),
                    UnreadMessageBadge = table.Column<bool>(type: "bit", nullable: false),
                    TaskbarFlashing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivacySettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DMFromFriends = table.Column<int>(type: "int", nullable: false),
                    DMFromUnknownUsers = table.Column<int>(type: "int", nullable: false),
                    DMFromServerChatroom = table.Column<int>(type: "int", nullable: false),
                    DMSpamFilter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivacySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrivacySettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoundboardSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SoundboardVolume = table.Column<byte>(type: "tinyint", nullable: false),
                    Soundboard = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoundboardSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoundboardSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamerModeSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    StreamerMode = table.Column<bool>(type: "bit", nullable: false),
                    AutomaticallyEnableAndDisableIfStreaming = table.Column<bool>(type: "bit", nullable: false),
                    HidePersonalInformation = table.Column<bool>(type: "bit", nullable: false),
                    HideInviteLinks = table.Column<bool>(type: "bit", nullable: false),
                    DisableSounds = table.Column<bool>(type: "bit", nullable: false),
                    DisableNotifications = table.Column<bool>(type: "bit", nullable: false),
                    HideEchoWindowFromScreenCapture = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamerModeSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreamerModeSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AlwaysPreviewVideo = table.Column<bool>(type: "bit", nullable: false),
                    CameraDevice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoiceSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    InputDevice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputDevice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputVolume = table.Column<byte>(type: "tinyint", nullable: false),
                    OutputVolume = table.Column<byte>(type: "tinyint", nullable: false),
                    InputMode = table.Column<int>(type: "int", nullable: false),
                    AutomaticallyDetermineInputSensitivity = table.Column<bool>(type: "bit", nullable: false),
                    InputSensitivity = table.Column<byte>(type: "tinyint", nullable: false),
                    EchoCancellation = table.Column<bool>(type: "bit", nullable: false),
                    NoiseSuppression = table.Column<int>(type: "int", nullable: false),
                    AdvancedVoiceActivity = table.Column<bool>(type: "bit", nullable: false),
                    AutomaticGainControl = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoiceSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountViolationAppeal",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViolationId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountViolationAppeal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountViolationAppeal_AccountViolation_ViolationId",
                        column: x => x.ViolationId,
                        principalTable: "AccountViolation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BugReportBugReportReason",
                columns: table => new
                {
                    ReasonsId = table.Column<byte>(type: "tinyint", nullable: false),
                    ReportsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugReportBugReportReason", x => new { x.ReasonsId, x.ReportsId });
                    table.ForeignKey(
                        name: "FK_BugReportBugReportReason_BugReportReason_ReasonsId",
                        column: x => x.ReasonsId,
                        principalTable: "BugReportReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugReportBugReportReason_BugReport_ReportsId",
                        column: x => x.ReportsId,
                        principalTable: "BugReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatAccountMessageTracker",
                columns: table => new
                {
                    OwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CoOwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatAccountMessageTracker", x => new { x.OwnerId, x.CoOwnerId });
                    table.ForeignKey(
                        name: "FK_ChatAccountMessageTracker_Account_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatAccountMessageTracker_ChatMessage_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatAccountMessageTracker_Chat_CoOwnerId",
                        column: x => x.CoOwnerId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessageAttachment",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessageAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessageAttachment_ChatMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessagePin",
                columns: table => new
                {
                    PinboardId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MessageId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessagePin", x => new { x.PinboardId, x.MessageId });
                    table.ForeignKey(
                        name: "FK_ChatMessagePin_ChatMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ChatMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessagePin_ChatPinboard_PinboardId",
                        column: x => x.PinboardId,
                        principalTable: "ChatPinboard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeedbackReportFeedbackReportReason",
                columns: table => new
                {
                    ReasonsId = table.Column<byte>(type: "tinyint", nullable: false),
                    ReportsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackReportFeedbackReportReason", x => new { x.ReasonsId, x.ReportsId });
                    table.ForeignKey(
                        name: "FK_FeedbackReportFeedbackReportReason_FeedbackReportReason_ReasonsId",
                        column: x => x.ReasonsId,
                        principalTable: "FeedbackReportReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackReportFeedbackReportReason_FeedbackReport_ReportsId",
                        column: x => x.ReportsId,
                        principalTable: "FeedbackReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomingFriendRequest",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderRequestId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ReceiverId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingFriendRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomingFriendRequest_Account_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomingFriendRequest_OutgoingFriendRequest_SenderRequestId",
                        column: x => x.SenderRequestId,
                        principalTable: "OutgoingFriendRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomStatusReport",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ReporterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ViolationId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomStatusReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomStatusReport_AccountViolation_ViolationId",
                        column: x => x.ViolationId,
                        principalTable: "AccountViolation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomStatusReport_Account_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomStatusReport_ReportedCustomStatus_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ReportedCustomStatus",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageReport",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ReporterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ViolationId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReport_AccountViolation_ViolationId",
                        column: x => x.ViolationId,
                        principalTable: "AccountViolation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageReport_Account_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReport_ReportedMessage_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ReportedMessage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportedMessageAttachment",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportedMessageAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportedMessageAttachment_ReportedMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ReportedMessage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProfileReport",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeSent = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ReporterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ViolationId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileReport_AccountViolation_ViolationId",
                        column: x => x.ViolationId,
                        principalTable: "AccountViolation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfileReport_Account_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileReport_ReportedProfile_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ReportedProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillingInformationId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_BillingInformation_BillingInformationId",
                        column: x => x.BillingInformationId,
                        principalTable: "BillingInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountViolationAppealReview",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppealId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ReviewerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    IsDenied = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountViolationAppealReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountViolationAppealReview_AccountViolationAppeal_AppealId",
                        column: x => x.AppealId,
                        principalTable: "AccountViolationAppeal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountViolationAppealReview_Account_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomStatusReportCustomStatusReportReason",
                columns: table => new
                {
                    ReasonsId = table.Column<byte>(type: "tinyint", nullable: false),
                    ReportsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomStatusReportCustomStatusReportReason", x => new { x.ReasonsId, x.ReportsId });
                    table.ForeignKey(
                        name: "FK_CustomStatusReportCustomStatusReportReason_CustomStatusReportReason_ReasonsId",
                        column: x => x.ReasonsId,
                        principalTable: "CustomStatusReportReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomStatusReportCustomStatusReportReason_CustomStatusReport_ReportsId",
                        column: x => x.ReportsId,
                        principalTable: "CustomStatusReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageReportMessageReportReason",
                columns: table => new
                {
                    ReasonsId = table.Column<byte>(type: "tinyint", nullable: false),
                    ReportsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReportMessageReportReason", x => new { x.ReasonsId, x.ReportsId });
                    table.ForeignKey(
                        name: "FK_MessageReportMessageReportReason_MessageReportReason_ReasonsId",
                        column: x => x.ReasonsId,
                        principalTable: "MessageReportReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageReportMessageReportReason_MessageReport_ReportsId",
                        column: x => x.ReportsId,
                        principalTable: "MessageReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfileReportProfileReportReason",
                columns: table => new
                {
                    ReasonsId = table.Column<byte>(type: "tinyint", nullable: false),
                    ReportsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileReportProfileReportReason", x => new { x.ReasonsId, x.ReportsId });
                    table.ForeignKey(
                        name: "FK_ProfileReportProfileReportReason_ProfileReportReason_ReasonsId",
                        column: x => x.ReasonsId,
                        principalTable: "ProfileReportReason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfileReportProfileReportReason_ProfileReport_ReportsId",
                        column: x => x.ReportsId,
                        principalTable: "ProfileReport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ActivityStatusId",
                table: "Account",
                column: "ActivityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_CustomStatusId",
                table: "Account",
                column: "CustomStatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBlock_BlockedId",
                table: "AccountBlock",
                column: "BlockedId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConnection_AccountId",
                table: "AccountConnection",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountMute_SubjectId",
                table: "AccountMute",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountNickname_SubjectId",
                table: "AccountNickname",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountNote_SubjectId",
                table: "AccountNote",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountProfile_AccountId",
                table: "AccountProfile",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_AccountId",
                table: "AccountRole",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSession_AccountId",
                table: "AccountSession",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSettings_LanguageId",
                table: "AccountSettings",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSoundboardMute_SubjectId",
                table: "AccountSoundboardMute",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountViolation_IssuerId",
                table: "AccountViolation",
                column: "IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountViolation_SubjectId",
                table: "AccountViolation",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountViolationAppeal_ViolationId",
                table: "AccountViolationAppeal",
                column: "ViolationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountViolationAppealReview_AppealId",
                table: "AccountViolationAppealReview",
                column: "AppealId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountViolationAppealReview_ReviewerId",
                table: "AccountViolationAppealReview",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_AppearanceSettings_ThemeId",
                table: "AppearanceSettings",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_BugReport_ReporterId",
                table: "BugReport",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_BugReportBugReportReason_ReportsId",
                table: "BugReportBugReportReason",
                column: "ReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatAccountMessageTracker_CoOwnerId",
                table: "ChatAccountMessageTracker",
                column: "CoOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatAccountMessageTracker_SubjectId",
                table: "ChatAccountMessageTracker",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatInvite_InviterId",
                table: "ChatInvite",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatInvite_SubjectId",
                table: "ChatInvite",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_AuthorId",
                table: "ChatMessage",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_MessageHolderId",
                table: "ChatMessage",
                column: "MessageHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ParentId",
                table: "ChatMessage",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessageAttachment_MessageId",
                table: "ChatMessageAttachment",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessagePin_MessageId",
                table: "ChatMessagePin",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMute_SubjectId",
                table: "ChatMute",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatParticipancy_SubjectId",
                table: "ChatParticipancy",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatPinboard_OwnerId",
                table: "ChatPinboard",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomStatusReport_ReporterId",
                table: "CustomStatusReport",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomStatusReport_SubjectId",
                table: "CustomStatusReport",
                column: "SubjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomStatusReport_ViolationId",
                table: "CustomStatusReport",
                column: "ViolationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomStatusReportCustomStatusReportReason_ReportsId",
                table: "CustomStatusReportCustomStatusReportReason",
                column: "ReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackReport_ReporterId",
                table: "FeedbackReport",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackReportFeedbackReportReason_ReportsId",
                table: "FeedbackReportFeedbackReportReason",
                column: "ReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipParticipancy_SubjectId",
                table: "FriendshipParticipancy",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendSuggestion_ReceiverId_SuggestionId",
                table: "FriendSuggestion",
                columns: new[] { "ReceiverId", "SuggestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendSuggestion_SuggestionId",
                table: "FriendSuggestion",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomingFriendRequest_ReceiverId",
                table: "IncomingFriendRequest",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomingFriendRequest_SenderRequestId",
                table: "IncomingFriendRequest",
                column: "SenderRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageReport_ReporterId",
                table: "MessageReport",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReport_SubjectId",
                table: "MessageReport",
                column: "SubjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageReport_ViolationId",
                table: "MessageReport",
                column: "ViolationId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReportMessageReportReason_ReportsId",
                table: "MessageReportMessageReportReason",
                column: "ReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_OutgoingFriendRequest_SenderId",
                table: "OutgoingFriendRequest",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_BillingInformationId",
                table: "PaymentMethod",
                column: "BillingInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileReport_ReporterId",
                table: "ProfileReport",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileReport_SubjectId",
                table: "ProfileReport",
                column: "SubjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileReport_ViolationId",
                table: "ProfileReport",
                column: "ViolationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileReportProfileReportReason_ReportsId",
                table: "ProfileReportProfileReportReason",
                column: "ReportsId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportedCustomStatus_AccountId",
                table: "ReportedCustomStatus",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportedMessage_AuthorId",
                table: "ReportedMessage",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportedMessageAttachment_MessageId",
                table: "ReportedMessageAttachment",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportedProfile_AccountId",
                table: "ReportedProfile",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityCredentials_UserId",
                table: "SecurityCredentials",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessibilitySettings");

            migrationBuilder.DropTable(
                name: "AccountBlock");

            migrationBuilder.DropTable(
                name: "AccountConnection");

            migrationBuilder.DropTable(
                name: "AccountMute");

            migrationBuilder.DropTable(
                name: "AccountNickname");

            migrationBuilder.DropTable(
                name: "AccountNote");

            migrationBuilder.DropTable(
                name: "AccountProfile");

            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "AccountSession");

            migrationBuilder.DropTable(
                name: "AccountSoundboardMute");

            migrationBuilder.DropTable(
                name: "AccountViolationAppealReview");

            migrationBuilder.DropTable(
                name: "ActivitySettings");

            migrationBuilder.DropTable(
                name: "AdvancedSettings");

            migrationBuilder.DropTable(
                name: "AppearanceSettings");

            migrationBuilder.DropTable(
                name: "BugReportBugReportReason");

            migrationBuilder.DropTable(
                name: "ChatAccountMessageTracker");

            migrationBuilder.DropTable(
                name: "ChatInvite");

            migrationBuilder.DropTable(
                name: "ChatMessageAttachment");

            migrationBuilder.DropTable(
                name: "ChatMessagePin");

            migrationBuilder.DropTable(
                name: "ChatMute");

            migrationBuilder.DropTable(
                name: "ChatParticipancy");

            migrationBuilder.DropTable(
                name: "ChatSettings");

            migrationBuilder.DropTable(
                name: "CustomStatusReportCustomStatusReportReason");

            migrationBuilder.DropTable(
                name: "FeedbackReportFeedbackReportReason");

            migrationBuilder.DropTable(
                name: "FriendRequestSettings");

            migrationBuilder.DropTable(
                name: "FriendshipParticipancy");

            migrationBuilder.DropTable(
                name: "FriendSuggestion");

            migrationBuilder.DropTable(
                name: "IncomingFriendRequest");

            migrationBuilder.DropTable(
                name: "KeybindSettings");

            migrationBuilder.DropTable(
                name: "MessageReportMessageReportReason");

            migrationBuilder.DropTable(
                name: "NotificationSettings");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "PrivacySettings");

            migrationBuilder.DropTable(
                name: "ProfileReportProfileReportReason");

            migrationBuilder.DropTable(
                name: "ReportedMessageAttachment");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "SecurityCredentials");

            migrationBuilder.DropTable(
                name: "SoundboardSettings");

            migrationBuilder.DropTable(
                name: "StreamerModeSettings");

            migrationBuilder.DropTable(
                name: "VideoSettings");

            migrationBuilder.DropTable(
                name: "VoiceSettings");

            migrationBuilder.DropTable(
                name: "AccountViolationAppeal");

            migrationBuilder.DropTable(
                name: "Theme");

            migrationBuilder.DropTable(
                name: "BugReportReason");

            migrationBuilder.DropTable(
                name: "BugReport");

            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "ChatPinboard");

            migrationBuilder.DropTable(
                name: "CustomStatusReportReason");

            migrationBuilder.DropTable(
                name: "CustomStatusReport");

            migrationBuilder.DropTable(
                name: "FeedbackReportReason");

            migrationBuilder.DropTable(
                name: "FeedbackReport");

            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.DropTable(
                name: "OutgoingFriendRequest");

            migrationBuilder.DropTable(
                name: "MessageReportReason");

            migrationBuilder.DropTable(
                name: "MessageReport");

            migrationBuilder.DropTable(
                name: "BillingInformation");

            migrationBuilder.DropTable(
                name: "ProfileReportReason");

            migrationBuilder.DropTable(
                name: "ProfileReport");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "ReportedCustomStatus");

            migrationBuilder.DropTable(
                name: "ReportedMessage");

            migrationBuilder.DropTable(
                name: "AccountSettings");

            migrationBuilder.DropTable(
                name: "AccountViolation");

            migrationBuilder.DropTable(
                name: "ReportedProfile");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "AccountActivityStatus");

            migrationBuilder.DropTable(
                name: "AccountCustomStatus");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

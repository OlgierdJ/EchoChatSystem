using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DomainCoreApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcceptedCurrency",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcceptedCurrency", x => x.Id);
                });

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
                name: "ApplicationKeybind",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationKeybind", x => x.Id);
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
                name: "Connection",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlatformName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlatformIcon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizeEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthenticationEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizationEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenCheckEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenRefreshEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeReturnEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
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
                name: "PaymentType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentType", x => x.Id);
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
                name: "Server",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Server", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerPermissionCategory",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerPermissionCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerRegion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionServerURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerRegion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServerRole",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Importance = table.Column<int>(type: "int", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplaySeperatelyFromOnlineMembers = table.Column<bool>(type: "bit", nullable: false),
                    AllowAnyoneToMention = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTransactionRefund",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeRefunded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTransactionRefund", x => x.Id);
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
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
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
                name: "SubscriptionTransactionGroup",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    ToBePaidAmount = table.Column<double>(type: "float", nullable: false),
                    Fulfilled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTransactionGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionTransactionGroup_AcceptedCurrency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "AcceptedCurrency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.Id);
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
                name: "SubscriptionPlan",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostEUR = table.Column<double>(type: "float", nullable: false),
                    PaymentPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlan_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServerChannelCategory",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Importance = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerChannelCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerChannelCategory_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerPermission",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<byte>(type: "tinyint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerPermission_ServerPermissionCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ServerPermissionCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    UserId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ActivityStatusId = table.Column<byte>(type: "tinyint", nullable: false),
                    CustomStatusId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountActivityStatus_ActivityStatusId",
                        column: x => x.ActivityStatusId,
                        principalTable: "AccountActivityStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_AccountCustomStatus_CustomStatusId",
                        column: x => x.CustomStatusId,
                        principalTable: "AccountCustomStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Account_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecurityCredentials",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                name: "SubscriptionPlanActivePeriod",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionPlanId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeStarts = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeStops = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsTemporarilyDisabled = table.Column<bool>(type: "bit", nullable: false),
                    PercentageSale = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanActivePeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanActivePeriod_SubscriptionPlan_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerChannelCategoryRole",
                columns: table => new
                {
                    ChannelCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerChannelCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerChannelCategoryRole", x => new { x.ChannelCategoryId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryRole_ServerChannelCategory_ChannelCategoryId",
                        column: x => x.ChannelCategoryId,
                        principalTable: "ServerChannelCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryRole_ServerChannelCategory_ServerChannelCategoryId",
                        column: x => x.ServerChannelCategoryId,
                        principalTable: "ServerChannelCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryRole_ServerRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ServerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannel",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderWeight = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlowMode = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsAgeRestricted = table.Column<bool>(type: "bit", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerTextChannel_ServerChannelCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ServerChannelCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerTextChannel_Server_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerVoiceChannel",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderWeight = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    BitRate = table.Column<long>(type: "bigint", nullable: false),
                    VideoQuality = table.Column<int>(type: "int", nullable: false),
                    UserLimit = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SlowMode = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsAgeRestricted = table.Column<bool>(type: "bit", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerVoiceChannel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannel_ServerChannelCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ServerChannelCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannel_ServerRegion_RegionId",
                        column: x => x.RegionId,
                        principalTable: "ServerRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannel_Server_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerChannelCategoryPermission",
                columns: table => new
                {
                    ChannelCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerChannelCategoryPermission", x => new { x.ChannelCategoryId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryPermission_ServerChannelCategory_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerChannelCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryPermission_ServerPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerRolePermission",
                columns: table => new
                {
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerRolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ServerRolePermission_ServerPermission_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerRolePermission_ServerRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ServerRole",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountConnection",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ConnectionId = table.Column<long>(type: "bigint", nullable: false),
                    AuthorizeResponseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizeClientId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizeState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorizeCodeChallenge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExternalRefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InternalRefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountConnection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountConnection_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountConnection_Connection_ConnectionId",
                        column: x => x.ConnectionId,
                        principalTable: "Connection",
                        principalColumn: "Id");
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountMute_Account_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountNickname_Account_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountNote_Account_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountProfile",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
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
                        name: "FK_AccountProfile_Account_Id",
                        column: x => x.Id,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.RoleId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_AccountRole_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountServerFolder",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Importance = table.Column<int>(type: "int", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountServerFolder", x => new { x.Id, x.Name });
                    table.ForeignKey(
                        name: "FK_AccountServerFolder_Account_Id",
                        column: x => x.Id,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountServerMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountServerMute", x => new { x.SubjectId, x.MuterId });
                    table.ForeignKey(
                        name: "FK_AccountServerMute_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountServerMute_Account_MuterId",
                        column: x => x.MuterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountServerMute_Server_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Server",
                        principalColumn: "Id");
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
                    TimeStarted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountSettings_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountSoundboardMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "AccountVideoMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountVideoMute", x => new { x.SubjectId, x.MuterId });
                    table.ForeignKey(
                        name: "FK_AccountVideoMute_Account_MuterId",
                        column: x => x.MuterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountVideoMute_Account_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id");
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
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ChatId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMute_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatMute_Chat_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Chat",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatParticipancy_Chat_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Chat",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FriendshipParticipancy_Friendship_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Friendship",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
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
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerAuditLog",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeLogged = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerAuditLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerAuditLog_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerAuditLog_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerBan",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeExpired = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeKeepMessagesBefore = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerBan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerBan_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerBan_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerEmote",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploaderId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerEmote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerEmote_Account_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerEmote_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerEvent",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreatorId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventFrequency = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerEvent_Account_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerEvent_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerChannelCategoryRolePermission",
                columns: table => new
                {
                    ChannelCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: true),
                    ServerChannelCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerChannelCategoryRolePermission", x => new { x.ChannelCategoryId, x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryRolePermission_ServerChannelCategoryRole_ChannelCategoryId_RoleId",
                        columns: x => new { x.ChannelCategoryId, x.RoleId },
                        principalTable: "ServerChannelCategoryRole",
                        principalColumns: new[] { "ChannelCategoryId", "RoleId" });
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryRolePermission_ServerChannelCategory_ChannelCategoryId",
                        column: x => x.ChannelCategoryId,
                        principalTable: "ServerChannelCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryRolePermission_ServerChannelCategory_ServerChannelCategoryId",
                        column: x => x.ServerChannelCategoryId,
                        principalTable: "ServerChannelCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryRolePermission_ServerPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryRolePermission_ServerRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ServerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountServerTextChannelMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountServerTextChannelMute", x => new { x.SubjectId, x.MuterId });
                    table.ForeignKey(
                        name: "FK_AccountServerTextChannelMute_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountServerTextChannelMute_Account_MuterId",
                        column: x => x.MuterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountServerTextChannelMute_ServerTextChannel_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerInvite",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    InviterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    InviteCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    TotalUses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerInvite", x => new { x.SubjectId, x.InviterId });
                    table.ForeignKey(
                        name: "FK_ServerInvite_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerInvite_Account_InviterId",
                        column: x => x.InviterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerInvite_ServerTextChannel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerInvite_Server_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelMessage",
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
                    table.PrimaryKey("PK_ServerTextChannelMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMessage_Account_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMessage_ServerTextChannelMessage_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ServerTextChannelMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMessage_ServerTextChannel_MessageHolderId",
                        column: x => x.MessageHolderId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelPermission",
                columns: table => new
                {
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelPermission", x => new { x.ChannelId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelPermission_ServerPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerTextChannelPermission_ServerTextChannel_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelPinboard",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelPinboard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelPinboard_ServerTextChannel_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelRole",
                columns: table => new
                {
                    ChannelCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelRole", x => new { x.ChannelCategoryId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelRole_ServerRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ServerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelRole_ServerTextChannel_ChannelCategoryId",
                        column: x => x.ChannelCategoryId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerWebhook",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TextChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebhookEndpointURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerWebhook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerWebhook_ServerTextChannel_TextChannelId",
                        column: x => x.TextChannelId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerWebhook_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountServerVoiceChannelMute",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MuterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    TimeMuted = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountServerVoiceChannelMute", x => new { x.SubjectId, x.MuterId });
                    table.ForeignKey(
                        name: "FK_AccountServerVoiceChannelMute_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountServerVoiceChannelMute_Account_MuterId",
                        column: x => x.MuterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountServerVoiceChannelMute_ServerVoiceChannel_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ServerVoiceChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: false),
                    InactiveChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    SystemMessagesChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    SendRandomWelcomeMessageWhenSomeoneJoins = table.Column<bool>(type: "bit", nullable: false),
                    PromptMembersToReplyWithASticker = table.Column<bool>(type: "bit", nullable: false),
                    SendHelpfulTipsForServerSetup = table.Column<bool>(type: "bit", nullable: false),
                    DefaultNotificationSettingsMode = table.Column<int>(type: "int", nullable: false),
                    ServerImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServerInviteBackgroundUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShowMembersInChannelList = table.Column<bool>(type: "bit", nullable: false),
                    VerificationLevelMode = table.Column<int>(type: "int", nullable: false),
                    Require2FAForModeratorActions = table.Column<bool>(type: "bit", nullable: false),
                    ExplicitImageFilterMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerSettings_ServerRegion_RegionId",
                        column: x => x.RegionId,
                        principalTable: "ServerRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerSettings_ServerTextChannel_Id",
                        column: x => x.Id,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerSettings_ServerVoiceChannel_Id",
                        column: x => x.Id,
                        principalTable: "ServerVoiceChannel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerSettings_Server_Id",
                        column: x => x.Id,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerVoiceChannelPermission",
                columns: table => new
                {
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerVoiceChannelPermission", x => new { x.ChannelId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelPermission_ServerPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelPermission_ServerVoiceChannel_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerVoiceChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerVoiceChannelRole",
                columns: table => new
                {
                    ChannelCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerVoiceChannelRole", x => new { x.ChannelCategoryId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelRole_ServerRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ServerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelRole_ServerVoiceChannel_ChannelCategoryId",
                        column: x => x.ChannelCategoryId,
                        principalTable: "ServerVoiceChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerVoiceInvite",
                columns: table => new
                {
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    InviterId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    GuestInvite = table.Column<bool>(type: "bit", nullable: false),
                    InviteCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimesUsed = table.Column<int>(type: "int", nullable: false),
                    TotalUses = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerVoiceInvite", x => new { x.SubjectId, x.InviterId });
                    table.ForeignKey(
                        name: "FK_ServerVoiceInvite_Account_InviterId",
                        column: x => x.InviterId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerVoiceInvite_ServerVoiceChannel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerVoiceChannel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerVoiceInvite_Server_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Server",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerProfile",
                columns: table => new
                {
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    FolderId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    FolderName = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    KickFromServerOnVoiceLeave = table.Column<bool>(type: "bit", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeJoined = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    JoinMethod = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Unknown")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerProfile", x => new { x.AccountId, x.ServerId });
                    table.ForeignKey(
                        name: "FK_ServerProfile_AccountServerFolder_FolderId_FolderName",
                        columns: x => new { x.FolderId, x.FolderName },
                        principalTable: "AccountServerFolder",
                        principalColumns: new[] { "Id", "Name" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerProfile_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerProfile_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccessibilitySettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SaturationPercent = table.Column<byte>(type: "tinyint", maxLength: 100, nullable: false),
                    ApplySaturationToCustomColors = table.Column<bool>(type: "bit", nullable: false),
                    AlwaysUnderlineLinks = table.Column<bool>(type: "bit", nullable: false),
                    RoleColorMode = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SyncProfileThemes = table.Column<bool>(type: "bit", nullable: false),
                    SyncContrastSettings = table.Column<bool>(type: "bit", nullable: false),
                    SyncReducedMotionWithPC = table.Column<bool>(type: "bit", nullable: false),
                    ReducedMotion = table.Column<bool>(type: "bit", nullable: false),
                    AutoPlayGIFsOnAppFocus = table.Column<bool>(type: "bit", nullable: false),
                    PlayAnimatedEmojis = table.Column<bool>(type: "bit", nullable: false),
                    StickerAnimationMode = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ShowSendMessageButton = table.Column<bool>(type: "bit", nullable: false),
                    UseLegacyChatInput = table.Column<bool>(type: "bit", nullable: false),
                    AllowTextToSpeech = table.Column<bool>(type: "bit", nullable: false),
                    TextToSpeechRate = table.Column<byte>(type: "tinyint", maxLength: 40, nullable: false, defaultValue: (byte)4)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessibilitySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessibilitySettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdvancedSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DeveloperMode = table.Column<bool>(type: "bit", nullable: false),
                    UseHardwareAccelerationToMakeEchoSmoother = table.Column<bool>(type: "bit", nullable: false),
                    AutoNavigateServerHome = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvancedSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppearanceSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ThemeId = table.Column<long>(type: "bigint", nullable: false),
                    InAppIcon = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppearanceSettings_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChatSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    DisplayVideosAndImagesFromLink = table.Column<bool>(type: "bit", nullable: false),
                    DisplayDirectVideosAndImagesUploads = table.Column<bool>(type: "bit", nullable: false),
                    LoadImageDescriptionsWithImages = table.Column<bool>(type: "bit", nullable: false),
                    PreviewEmbedsAndWebsiteLinks = table.Column<bool>(type: "bit", nullable: false),
                    ShowEmoteReactionsOnMessages = table.Column<bool>(type: "bit", nullable: false),
                    AutoConvertEmoticonsToEmojis = table.Column<bool>(type: "bit", nullable: false),
                    EnableStickerSuggestions = table.Column<bool>(type: "bit", nullable: false),
                    StickersInAutocomplete = table.Column<bool>(type: "bit", nullable: false),
                    PreviewEmojisAndMarkdownWhilstTyping = table.Column<bool>(type: "bit", nullable: false),
                    OpenThreadsInSplitView = table.Column<bool>(type: "bit", nullable: false),
                    ContentSpoilerMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FriendRequestSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountSettingsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Everyone = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FriendsOfFriends = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ServerMembers = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequestSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendRequestSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FriendRequestSettings_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameOverlaySettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EnableGameOverlay = table.Column<bool>(type: "bit", nullable: false),
                    ToggleOverlayLockKeybind = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Shift + L"),
                    AvatarSizeMode = table.Column<int>(type: "int", nullable: false),
                    DisplayNamesMode = table.Column<int>(type: "int", nullable: false),
                    DisplayUsersMode = table.Column<int>(type: "int", nullable: false),
                    OverlayNotificationsPlacement = table.Column<int>(type: "int", nullable: false),
                    ShowTextChatNotifications = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameOverlaySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameOverlaySettings_AccountSettings_Id",
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EnableDesktopNotifications = table.Column<bool>(type: "bit", nullable: false),
                    EnableUnreadMessageBadge = table.Column<bool>(type: "bit", nullable: false),
                    EnableTaskbarFlashing = table.Column<bool>(type: "bit", nullable: false),
                    PushNotificationInactiveTimeoutInMinutes = table.Column<byte>(type: "tinyint", nullable: false),
                    TextToSpeechNotificationMode = table.Column<int>(type: "int", nullable: false),
                    FocusModeEnabled = table.Column<bool>(type: "bit", nullable: false),
                    EnableSameChannelNotifications = table.Column<bool>(type: "bit", nullable: false),
                    DisableAllNotificationSounds = table.Column<bool>(type: "bit", nullable: false),
                    AllowMessageNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowDeafenNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowUndeafenNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowMuteNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowUnmuteNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowVoiceDisconnectedNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowPTTActivateNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowPTTDeactivateNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowUserJoinNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowUserLeaveNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowUserMovedNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowOutgoingRingNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowIncomingRingNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowStreamStartedNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowStreamStoppedNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowViewerJoinNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowViewerLeaveNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowActivityStartNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowActivityEndNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowActivityUserJoinNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowActivityUserLeaveNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    AllowInvitedToSpeakNotificationSound = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveCommunicationEmails = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveSocialEmails = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveAnnouncementAndUpdateEmails = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveTipEmails = table.Column<bool>(type: "bit", nullable: false),
                    ReceiveRecommendationEmails = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PrivacySettings_Account_Id",
                        column: x => x.Id,
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SoundboardSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SoundboardVolume = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoundboardSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoundboardSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StreamerModeSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    EnableStreamerMode = table.Column<bool>(type: "bit", nullable: false),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VideoSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AlwaysPreviewVideo = table.Column<bool>(type: "bit", nullable: false),
                    CameraDevice = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoBackground = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseOpenH264VideoCodec = table.Column<bool>(type: "bit", nullable: false),
                    EnableHardwareAccelerationForVideo = table.Column<bool>(type: "bit", nullable: false),
                    EnableForceQualityOfServicePacketPrio = table.Column<bool>(type: "bit", nullable: false),
                    UseDDLInjectionToCaptureScreen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id");
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
                    AutomaticGainControl = table.Column<bool>(type: "bit", nullable: false),
                    Attenuation = table.Column<byte>(type: "tinyint", nullable: false),
                    LowerVolumeOfOtherApplicationsWhenISpeak = table.Column<bool>(type: "bit", nullable: false),
                    LowerVolumeOfOtherApplicationsWhenOthersSpeak = table.Column<bool>(type: "bit", nullable: false),
                    AudioSubSystemMode = table.Column<int>(type: "int", nullable: false),
                    ShowWarningWhenNoMicInputDetected = table.Column<bool>(type: "bit", nullable: false),
                    EnableDiagnosticAudioRecording = table.Column<bool>(type: "bit", nullable: false),
                    EnableVoiceDebugLogging = table.Column<bool>(type: "bit", nullable: false),
                    MuteSelf = table.Column<bool>(type: "bit", nullable: false),
                    DeafenSelf = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoiceSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoiceSettings_AccountSettings_Id",
                        column: x => x.Id,
                        principalTable: "AccountSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WindowSettings",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    OpenEchoOnPCStartup = table.Column<bool>(type: "bit", nullable: false),
                    StartMinimized = table.Column<bool>(type: "bit", nullable: false),
                    MinimizeOnClose = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindowSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WindowSettings_AccountSettings_Id",
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
                        principalColumn: "Id");
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
                        principalColumn: "Id");
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
                        principalColumn: "Id");
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
                        principalColumn: "Id");
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CustomStatusReport",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
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
                        name: "FK_CustomStatusReport_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CustomStatusReport_Account_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Account",
                        principalColumn: "Id");
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
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
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
                        name: "FK_MessageReport_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageReport_Account_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Account",
                        principalColumn: "Id");
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
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
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
                        name: "FK_ProfileReport_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfileReport_Account_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProfileReport_ReportedProfile_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ReportedProfile",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerSoundboardSound",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    UploaderId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AssociatedEmoteId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoundFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerSoundboardSound", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerSoundboardSound_Account_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerSoundboardSound_ServerEmote_AssociatedEmoteId",
                        column: x => x.AssociatedEmoteId,
                        principalTable: "ServerEmote",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerSoundboardSound_Server_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Server",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelAccountMessageTracker",
                columns: table => new
                {
                    OwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CoOwnerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubjectId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelAccountMessageTracker", x => new { x.OwnerId, x.CoOwnerId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelAccountMessageTracker_Account_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Account",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerTextChannelAccountMessageTracker_ServerTextChannelMessage_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "ServerTextChannelMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelAccountMessageTracker_ServerTextChannel_CoOwnerId",
                        column: x => x.CoOwnerId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelMessageAttachment",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    FileURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelMessageAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMessageAttachment_ServerTextChannelMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ServerTextChannelMessage",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelMessagePin",
                columns: table => new
                {
                    PinboardId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    MessageId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelMessagePin", x => new { x.PinboardId, x.MessageId });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMessagePin_ServerTextChannelMessage_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ServerTextChannelMessage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMessagePin_ServerTextChannelPinboard_PinboardId",
                        column: x => x.PinboardId,
                        principalTable: "ServerTextChannelPinboard",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelRolePermission",
                columns: table => new
                {
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelRolePermission", x => new { x.ChannelId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelRolePermission_ServerPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerTextChannelRolePermission_ServerRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ServerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelRolePermission_ServerTextChannelRole_ChannelId_RoleId",
                        columns: x => new { x.ChannelId, x.RoleId },
                        principalTable: "ServerTextChannelRole",
                        principalColumns: new[] { "ChannelCategoryId", "RoleId" });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelRolePermission_ServerTextChannel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerVoiceChannelRolePermission",
                columns: table => new
                {
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerVoiceChannelRolePermission", x => new { x.ChannelId, x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelRolePermission_ServerPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelRolePermission_ServerRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ServerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelRolePermission_ServerVoiceChannelRole_ChannelId_RoleId",
                        columns: x => new { x.ChannelId, x.RoleId },
                        principalTable: "ServerVoiceChannelRole",
                        principalColumns: new[] { "ChannelCategoryId", "RoleId" });
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelRolePermission_ServerVoiceChannel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerVoiceChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerChannelCategoryMemberSettings",
                columns: table => new
                {
                    ChannelCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerChannelCategoryMemberSettings", x => new { x.AccountId, x.ChannelCategoryId });
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryMemberSettings_ServerChannelCategory_ChannelCategoryId",
                        column: x => x.ChannelCategoryId,
                        principalTable: "ServerChannelCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryMemberSettings_ServerPermission_ChannelCategoryId",
                        column: x => x.ChannelCategoryId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryMemberSettings_ServerProfile_AccountId_ServerId",
                        columns: x => new { x.AccountId, x.ServerId },
                        principalTable: "ServerProfile",
                        principalColumns: new[] { "AccountId", "ServerId" });
                });

            migrationBuilder.CreateTable(
                name: "ServerProfileServerRole",
                columns: table => new
                {
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    RoleId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TimeGranted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerProfileServerRole", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ServerProfileServerRole_ServerProfile_AccountId_ServerId",
                        columns: x => new { x.AccountId, x.ServerId },
                        principalTable: "ServerProfile",
                        principalColumns: new[] { "AccountId", "ServerId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerProfileServerRole_ServerRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ServerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelMemberSettings",
                columns: table => new
                {
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelMemberSettings", x => new { x.ChannelId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMemberSettings_ServerPermission_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMemberSettings_ServerProfile_AccountId_ServerId",
                        columns: x => new { x.AccountId, x.ServerId },
                        principalTable: "ServerProfile",
                        principalColumns: new[] { "AccountId", "ServerId" });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMemberSettings_ServerTextChannel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerVoiceChannelMemberSettings",
                columns: table => new
                {
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerVoiceChannelMemberSettings", x => new { x.ChannelId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelMemberSettings_ServerPermission_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelMemberSettings_ServerProfile_AccountId_ServerId",
                        columns: x => new { x.AccountId, x.ServerId },
                        principalTable: "ServerProfile",
                        principalColumns: new[] { "AccountId", "ServerId" });
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelMemberSettings_ServerVoiceChannel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerVoiceChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PaymentTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TimeAdded = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDefaultPaymentMethod = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_BillingInformation_Id",
                        column: x => x.Id,
                        principalTable: "BillingInformation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PaymentMethod_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_PaymentType_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillingInformationId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    SubscriptionPlanId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    TimeSubscribed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeCancelled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MyCostEUR = table.Column<double>(type: "float", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    AgreeToEchosWithdrawalRight = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_AcceptedCurrency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "AcceptedCurrency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscription_BillingInformation_BillingInformationId",
                        column: x => x.BillingInformationId,
                        principalTable: "BillingInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscription_SubscriptionPlan_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Keybind",
                columns: table => new
                {
                    KeybindSettingsId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ApplicationKeybindId = table.Column<byte>(type: "tinyint", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keybind", x => new { x.KeybindSettingsId, x.ApplicationKeybindId });
                    table.ForeignKey(
                        name: "FK_Keybind_ApplicationKeybind_ApplicationKeybindId",
                        column: x => x.ApplicationKeybindId,
                        principalTable: "ApplicationKeybind",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Keybind_KeybindSettings_KeybindSettingsId",
                        column: x => x.KeybindSettingsId,
                        principalTable: "KeybindSettings",
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
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountViolationAppealReview_Account_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "Account",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "ServerChannelCategoryMemberPermission",
                columns: table => new
                {
                    ChannelCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerChannelCategoryMemberPermission", x => new { x.ChannelCategoryId, x.AccountId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryMemberPermission_ServerChannelCategoryMemberSettings_AccountId_ChannelCategoryId",
                        columns: x => new { x.AccountId, x.ChannelCategoryId },
                        principalTable: "ServerChannelCategoryMemberSettings",
                        principalColumns: new[] { "AccountId", "ChannelCategoryId" });
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryMemberPermission_ServerChannelCategory_ChannelCategoryId",
                        column: x => x.ChannelCategoryId,
                        principalTable: "ServerChannelCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryMemberPermission_ServerPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerChannelCategoryMemberPermission_ServerProfile_AccountId_ServerId",
                        columns: x => new { x.AccountId, x.ServerId },
                        principalTable: "ServerProfile",
                        principalColumns: new[] { "AccountId", "ServerId" });
                });

            migrationBuilder.CreateTable(
                name: "ServerTextChannelMemberPermission",
                columns: table => new
                {
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerTextChannelMemberPermission", x => new { x.ChannelId, x.AccountId });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMemberPermission_ServerPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMemberPermission_ServerProfile_AccountId_ServerId",
                        columns: x => new { x.AccountId, x.ServerId },
                        principalTable: "ServerProfile",
                        principalColumns: new[] { "AccountId", "ServerId" });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMemberPermission_ServerTextChannelMemberSettings_ChannelId_AccountId",
                        columns: x => new { x.ChannelId, x.AccountId },
                        principalTable: "ServerTextChannelMemberSettings",
                        principalColumns: new[] { "ChannelId", "AccountId" });
                    table.ForeignKey(
                        name: "FK_ServerTextChannelMemberPermission_ServerTextChannel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerTextChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServerVoiceChannelMemberPermission",
                columns: table => new
                {
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AccountId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    PermissionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ServerId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerVoiceChannelMemberPermission", x => new { x.ChannelId, x.AccountId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelMemberPermission_ServerPermission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "ServerPermission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelMemberPermission_ServerProfile_AccountId_ServerId",
                        columns: x => new { x.AccountId, x.ServerId },
                        principalTable: "ServerProfile",
                        principalColumns: new[] { "AccountId", "ServerId" });
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelMemberPermission_ServerVoiceChannelMemberSettings_ChannelId_AccountId",
                        columns: x => new { x.ChannelId, x.AccountId },
                        principalTable: "ServerVoiceChannelMemberSettings",
                        principalColumns: new[] { "ChannelId", "AccountId" });
                    table.ForeignKey(
                        name: "FK_ServerVoiceChannelMemberPermission_ServerVoiceChannel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ServerVoiceChannel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionActivePeriod",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubcriptionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TransactionGroupId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ExpirationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimePaused = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionActivePeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionActivePeriod_SubscriptionTransactionGroup_TransactionGroupId",
                        column: x => x.TransactionGroupId,
                        principalTable: "SubscriptionTransactionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionActivePeriod_Subscription_SubcriptionId",
                        column: x => x.SubcriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionTransaction",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TransactionGroupId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionTypeId = table.Column<long>(type: "bigint", nullable: false),
                    SubscriptionId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ChargedAmount = table.Column<double>(type: "float", nullable: false),
                    TimePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExternalTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionTransaction_AcceptedCurrency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "AcceptedCurrency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionTransaction_PaymentType_TransactionTypeId",
                        column: x => x.TransactionTypeId,
                        principalTable: "PaymentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionTransaction_SubscriptionTransactionGroup_TransactionGroupId",
                        column: x => x.TransactionGroupId,
                        principalTable: "SubscriptionTransactionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionTransaction_SubscriptionTransactionRefund_Id",
                        column: x => x.Id,
                        principalTable: "SubscriptionTransactionRefund",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionTransaction_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AcceptedCurrency",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "dkk" },
                    { 2L, "eur" },
                    { 3L, "usd" }
                });

            migrationBuilder.InsertData(
                table: "AccountActivityStatus",
                columns: new[] { "Id", "Description", "Icon", "IconColor", "Name" },
                values: new object[,]
                {
                    { (byte)1, "", "Icons.Material.Filled.Circle", "Success", "Online" },
                    { (byte)2, "", "Icons.Material.Filled.Brightness2", "Warning", "Away" },
                    { (byte)3, "You will not receive any desktop notifications.", "Icons.Material.Filled.RemoveCircle", "Error", "Busy" },
                    { (byte)4, "", "Icons.Material.Filled.StopCircle", "Dark", "Offline" },
                    { (byte)5, "You will not appear online, but have full access to all of Echo.", "Icons.Material.Filled.StopCircle", "Dark", "Invisible" }
                });

            migrationBuilder.InsertData(
                table: "ApplicationKeybind",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { (byte)1, "mutes self if unmuted and unmutes self if muted", "mute / unmute self" },
                    { (byte)2, "Edit a message if you have the permission", "Edit message" },
                    { (byte)3, "Pin a message in a chat", "Pin message" },
                    { (byte)4, "make a reply to a message in a chat", "Reply" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Danmark" },
                    { 2L, "Sverige" },
                    { 3L, "Noreg" },
                    { 4L, "Deutschland" },
                    { 5L, "United Kingdom" },
                    { 6L, "La France" },
                    { 7L, "华人(Chinese)" },
                    { 8L, "日本(Japan)" },
                    { 9L, "남한(south korea)" },
                    { 10L, "United States of America" }
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Id", "LanguageCode", "Name" },
                values: new object[,]
                {
                    { 1L, "DK", "Danmark" },
                    { 2L, "Sv", "Sverige" },
                    { 3L, "No", "Noreg" },
                    { 4L, "De", "Deutschland" },
                    { 5L, "en-gb", "United Kingdom" },
                    { 6L, "Fr", "La France" },
                    { 7L, "zh-CN", "中国(China)" },
                    { 8L, "Jp", "日本(Japan)" },
                    { 9L, "ko", "남한(south korea)" },
                    { 10L, "en-us", "United States of America" }
                });

            migrationBuilder.InsertData(
                table: "PaymentType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "PayPal" },
                    { 2L, "MobilePay" },
                    { 3L, "PaysafeCard" },
                    { 4L, "Visa" },
                    { 5L, "MasterCard" },
                    { 6L, "Google Pay" },
                    { 7L, "Apple Pay" },
                    { 8L, "Stripe" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1m, "View_Server" },
                    { 2m, "Send_Message" },
                    { 3m, "Add_Friend" },
                    { 4m, "Join_Voice" },
                    { 5m, "Delete_Account" },
                    { 6m, "Create_Server" },
                    { 7m, "Create_Chats" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1m, "User" },
                    { 2m, "Admin" },
                    { 3m, "Moderator" },
                    { 4m, "System_Owner" },
                    { 5m, "Premium_Sonar" },
                    { 6m, "Premium_Sonar_Plus" }
                });

            migrationBuilder.InsertData(
                table: "ServerPermission",
                columns: new[] { "Id", "CategoryId", "Description", "Name" },
                values: new object[,]
                {
                    { 1m, null, "Allows the role to see the public channels that are not private. It’s normal to grant this permission to almost all roles, but the channel/category permission settings usually void it.", "View Channels" },
                    { 2m, null, "Allows the role to access the channel settings of all channels it can see. Granting this permission can be extremely dangerous since deleted channels are not recoverable.", "Manage Channels" },
                    { 3m, null, "Allows the role to create, edit, and remove all the roles that are below itself in the hierarchy. Users with this role can also add and remove roles to/from members. Granting this permission can be extremely dangerous since deleted roles are not recoverable, and ill-intended users can grant dangerous permissions to others.", "Manage Roles" },
                    { 4m, null, "Allows the role to access the Emoji, Stickers, and Soundboard sections of the Server Settings. Users with this role can add expressions. Granting this permission can be dangerous since there isn’t an approval system for added expressions.", "Create Expressions" },
                    { 5m, null, "Allows the role to access the Emoji, Stickers, and Soundboard sections of the Server Settings. Users with this role can edit and remove all expressions. Granting this permission can be dangerous since deleted expressions are not recoverable.", "Manage Expressions" },
                    { 6m, null, "Allows the role to view the Audit Log section of the Server Settings. While the section doesn’t allow users to take any action, it can be dangerous to grant since there could be sensitive information in Audit Logs.", "View Audit Log" },
                    { 7m, null, "Allows the role to view the Server Insights section of Server Settings. While Server Insights contains a lot of important information, there’s no harm in sharing them unless you don’t want to.", "View Server Insights" },
                    { 8m, null, "Allows the role to add, edit, and remove webhooks. Granting this permission can be extremely dangerous since webhooks can bypass AutoMod, bots, and other moderation systems in place, allowing users to tag @everyone, post unwanted content, and similar ill-intended actions limitlessly and fast.", "Manage Webhooks" },
                    { 9m, null, "Allows the role to adjust the server settings like name, icon, default settings, add bots, and change AutoMod rules. Granting this can be extremely dangerous since while server name, icon, and default settings can be easily fixed, removed AutoMod rules are not recoverable, and ill-intended bots can nuke (delete all the channels, ban all the members, etc.) your server.", "Manage Server" },
                    { 10m, null, "Allows the role to create custom invites for the server. It’s normal to grant this to all roles unless you want to have only certain invites available. Be sure to provide your invite links in the server for people to copy and paste if you don’t want to grant this permission.", "Create Invite" },
                    { 11m, null, "Allows the users with permission to change their own nicknames on your server. It’s a normal permission to grant.", "Change Nickname" },
                    { 12m, null, "Allows the role to change the nicknames of other members. Granting this permission can be dangerous since ill-intended users might vandalize others’ profiles by changing their names.", "Manage Nicknames" },
                    { 13m, null, "Allows the role to kick members that are below them in the user/role hierarchy by using the integrated /kick command or via the right-click menu. Granting this permission can be extremely dangerous since it allows the users to remove others from the server (kicked users can rejoin,) but it’s not the most dangerous part of it. Discord has a “pruning” feature - a feature that allows you to kick all the members that haven’t been in Discord in the last 7 or 30 days with no/selected roles. Pruning is a common vandalism method that can remove most users of a server. Preventing the prune vandalism is as simple as not granting the Kick Members permission.", "Kick Members" },
                    { 14m, null, "Allows the role to ban members that are below them in the user/role hierarchy by using the integrated /ban command or via the right-click menu. Granting this permission can be extremely dangerous since it allows the users to ban every single user that is below them in the hierarchy, and banned users cannot rejoin the server, even with other accounts, since all bans are IP bans. Bans can be manually removed via the Bans section of the Server Settings.", "Ban Members" },
                    { 15m, null, "Allows the role to timeout other users via the right-click menu. Users who are timed out cannot send messages in any channel or speak in voice channels. Granting this permission can be dangerous since it allows users to prevent others from interacting with the community.", "Timeout Members" },
                    { 16m, null, "Allows the role to send messages in channels they can see. It’s normal to grant this permission to almost all roles, but it is usually voided by the channel/category permission settings.", "Send Messages" },
                    { 17m, null, "Allows the role to send messages in threads they can see. It’s normal to grant this permission to almost all roles, but it is usually voided by the channel/category permission settings.", "Send Messages in Threads" },
                    { 18m, null, "Allows the role to create public threads in channels they can see. Although Discord has a limit of 1000 for active threads (no limit on inactive), allowing users to create threads can make moderation a bit harder.", "Create Public Threads" },
                    { 19m, null, "Allows the role to create private threads in channels they can see. The only way to see a private thread is to be mentioned in the thread or have the Manage Threads permission.", "Create Private Threads" },
                    { 20m, null, "Allows the role to display embedded content for the links they send. A common misconception about this permission is that it allows or disallows users to send links. There are a few ways to disallow users from sending links, but this permission is not it. It only manages the embedded content (marked red in the image below) of a link.", "Embed Links" },
                    { 21m, null, "Allows the role to attach files with any extension to the channels where they can send messages in. While this permission is normal to grant to every user in servers with a few thousand members, it can be mildly dangerous in situations where there are tens of thousands of members and a fast chat where moderation is also mildly difficult. Being able to attach files means they can literally attach any file, including malicious ones.", "Attach Files" },
                    { 22m, null, "Allows the role to add reactions to messages they can see. When disallowed, users can still add reactions to the reactions that are already present.", "Add Reactions" },
                    { 23m, null, "Allows the role to use emojis from other servers. It is usually granted to all users on most servers, just like the Use External Stickers permission. Don’t grant this permission if you want to ensure that no one uses ill-intended emojis on your server.", "Use External Emoji" },
                    { 24m, null, "Allows the role to use stickers from other servers. It is usually granted to all users on most servers, just like the Use External Emoji permission. Don’t grant this permission if you want to ensure that no one uses ill-intended stickers on your server.", "Use External Stickers" },
                    { 25m, null, "Allows the role to mention @everyone, @here, and all the roles even if their “Allow anyone to @mention this role” option is turned off. Granting this permission can be extremely dangerous since it allows users to spam mention everyone in the server and makes way for Mention Raids (multiple users joining the server and spam mentioning multiple users or even everyone).", "Mention @everyone, @here, and All Roles" },
                    { 26m, null, "Allows the role to delete and pin messages they can see. Granting this permission can be very dangerous since it allows users to delete multiple messages of other users, potentially deleting every single message in the server.", "Manage Messages" },
                    { 27m, null, "Allows the role to edit, close, and delete threads. Granting this permission can be very dangerous since it gives full control over threads, potentially deleting all of them.", "Manage Threads" },
                    { 28m, null, "Allows the role to see every message sent in text channels. When disallowed, users only see messages when they’re online and in a text channel. It’s normal to grant this permission to everyone.", "Read Message History" },
                    { 29m, null, "Allows the user to use the /tts command, which triggers a text-to-speech player to read out the provided message to everyone who’s viewing the channel. Granting this permission can be mildly dangerous since a device reading an unwanted message out loud can be risky.", "Send Text-to-Speech Messages" },
                    { 30m, null, "Allows the permission to use application commands such as slash commands and right-click menu buttons. It’s normal to grant this permission to everyone since most commands and application functions are public-intended; users won’t be able to use a command that isn’t public (only available to staff).", "Use Application Commands" },
                    { 31m, null, "Allows the permission to send voice messages to the channels they can see using mobile devices. Discord introduced the voice message feature in April 2024. Granting this permission can be mildly dangerous since there’s currently no automatic moderation on voice messages, and ill-intended users can send unwanted voice messages.", "Send Voice Messages" },
                    { 32m, null, "Allows the permission to join voice channels they can see. It’s normal to grant this permission to everyone. One common reason not to grant this permission is to block newcomers from joining voice channels, preventing a potential voice raid. The system most servers use in this case is once the user spends a certain amount of time, they’ll get a new role (via a bot or manually) that has Connect permission.", "Connect" },
                    { 33m, null, "Allows the permission to speak in voice channels. If a user doesn’t have this permission, they will be muted upon joining a voice channel. There are two ways they can talk: they get the Speak permission, or a user with Mute Members permission unmutes them. It’s normal to grant this permission to everyone.", "Speak" },
                    { 34m, null, "Allows the role to turn on their camera and screen share in voice channels. While it’s normal to grant this permission to everyone, it can be mildly dangerous since there’s no automatic moderation system for video calls and screen sharing, allowing ill-intended users to display unwanted content.", "Video" },
                    { 35m, null, "Allows the role to use the Activities feature. Activities are games and apps (like YouTube Watch Together, Blazing 8s, Gartic Phone, etc.) that are integrated into voice channels. It’s normal to grant this permission to everyone.", "Use Activities" },
                    { 36m, null, "Allows the role to use sounds from the Soundboard in voice channels. Granting this permission can be mildly dangerous since users can disturb other members by playing or spamming loud or unwanted sounds in voice channels.", "Use Soundboard" },
                    { 37m, null, "Allows the role to use soundboards of other servers in voice channels. Granting this permission can be mildly dangerous since other servers might have ill-intended sounds.", "Use External Sounds" },
                    { 38m, null, "Allows the role to speak without Push-to-talk. Users who don’t have this permission will have to use push-to-talk to speak in voice channels.", "Use Voice Activity" },
                    { 39m, null, "Allows the role to use the “Push to Talk (Priority)” keybind, which lowers the other users’ voice channel volume when pressed, thus allowing the user to be easily heard. While this permission isn’t risky to grant, usually only staff roles are granted.", "Priority Speaker" },
                    { 40m, null, "Allows the role to mute other users in voice channels so they won’t be able to speak. It’s a common misconception that this permission allows users to mute others in the sense that they won’t be able to send messages; this is not the case. Users need the Timeout Members permission to mute others (prevent them from sending messages.) Granting this permission can be dangerous since it allows users to prevent others from speaking in voice channels.", "Mute Members" },
                    { 41m, null, "Allows the role to deafen other users in voice channels so they won’t be able to hear other users. Deafened users can still speak. Granting this permission can be dangerous since it allows users to prevent others from hearing others in voice channels.", "Deafen Members" },
                    { 42m, null, "Allows the role to move members between voice channels. The user with the permission can also join voice channels even if they’re at full capacity. They can also move members into voice channels that are at full capacity. Granting this permission can be dangerous since it allows users to move each other between voice channels, potentially disturbing conversations.", "Move Members" },
                    { 43m, null, "Allows the role to adjust voice channel status. Granting this permission is mildly dangerous since users can put unwanted content in the status.", "Set Voice Channel Status" },
                    { 44m, null, "Allows the role to request to speak in stage channels. Members who request to speak can be approved or denied by moderators. It’s normal to grant this permission to everyone.", "Request to Speak" },
                    { 45m, null, "Allows the role to create events. Granting this permission is dangerous since users can flood the server with all kinds of events.", "Create Events" },
                    { 46m, null, "Allows the role to edit and delete all events. Granting this permission is dangerous since users with the role can disturb the server's events.", "Manage Events" },
                    { 47m, null, "Members with this permission will have every permission and will also bypass channel specific permissions or restrictions (for example, these members would get access to all private channels) **This is a dangerous permission to grant.", "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "ServerRegion",
                columns: new[] { "Id", "Icon", "Name", "RegionServerURL" },
                values: new object[,]
                {
                    { 1L, "https://upload.wikimedia.org/wikipedia/commons/4/4a/Brazilian_flag_icon_round.svg", "Mr Worldwide", "https://echo.chat/rtc/brazil/rtchub" },
                    { 2L, "https://en.wikipedia.org/wiki/St._Peter%27s_Basilica#/media/File:Basilica_di_San_Pietro_in_Vaticano_September_2015-1a.jpg", "holy pop", "https://echo.chat/rtc/vatikanet/rtchub" }
                });

            migrationBuilder.InsertData(
                table: "Theme",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Dark" },
                    { 2L, "Light" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_ActivityStatusId",
                table: "Account",
                column: "ActivityStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_CustomStatusId",
                table: "Account",
                column: "CustomStatusId",
                unique: true,
                filter: "[CustomStatusId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Account_Name",
                table: "Account",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountBlock_BlockedId",
                table: "AccountBlock",
                column: "BlockedId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConnection_AccountId",
                table: "AccountConnection",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConnection_ConnectionId",
                table: "AccountConnection",
                column: "ConnectionId");

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
                name: "IX_AccountRole_AccountId",
                table: "AccountRole",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerMute_AccountId",
                table: "AccountServerMute",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerMute_MuterId",
                table: "AccountServerMute",
                column: "MuterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerTextChannelMute_AccountId",
                table: "AccountServerTextChannelMute",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerTextChannelMute_MuterId",
                table: "AccountServerTextChannelMute",
                column: "MuterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerVoiceChannelMute_AccountId",
                table: "AccountServerVoiceChannelMute",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountServerVoiceChannelMute_MuterId",
                table: "AccountServerVoiceChannelMute",
                column: "MuterId");

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
                name: "IX_AccountVideoMute_MuterId",
                table: "AccountVideoMute",
                column: "MuterId");

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
                name: "IX_ApplicationKeybind_Name",
                table: "ApplicationKeybind",
                column: "Name",
                unique: true);

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
                name: "IX_ChatMute_ChatId",
                table: "ChatMute",
                column: "ChatId");

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
                name: "IX_CustomStatusReport_AccountId",
                table: "CustomStatusReport",
                column: "AccountId");

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
                name: "IX_FriendRequestSettings_AccountId",
                table: "FriendRequestSettings",
                column: "AccountId",
                unique: true);

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
                name: "IX_Keybind_Action",
                table: "Keybind",
                column: "Action",
                unique: true,
                filter: "[Action] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Keybind_ApplicationKeybindId",
                table: "Keybind",
                column: "ApplicationKeybindId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReport_AccountId",
                table: "MessageReport",
                column: "AccountId");

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
                name: "IX_PaymentMethod_CountryId",
                table: "PaymentMethod",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_PaymentTypeId",
                table: "PaymentMethod",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileReport_AccountId",
                table: "ProfileReport",
                column: "AccountId");

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
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityCredentials_UserId",
                table: "SecurityCredentials",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerAuditLog_AccountId",
                table: "ServerAuditLog",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerAuditLog_ServerId",
                table: "ServerAuditLog",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerBan_AccountId",
                table: "ServerBan",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerBan_ServerId",
                table: "ServerBan",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategory_ServerId",
                table: "ServerChannelCategory",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryMemberPermission_AccountId_ChannelCategoryId",
                table: "ServerChannelCategoryMemberPermission",
                columns: new[] { "AccountId", "ChannelCategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryMemberPermission_AccountId_ServerId",
                table: "ServerChannelCategoryMemberPermission",
                columns: new[] { "AccountId", "ServerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryMemberPermission_PermissionId",
                table: "ServerChannelCategoryMemberPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryMemberSettings_AccountId_ServerId",
                table: "ServerChannelCategoryMemberSettings",
                columns: new[] { "AccountId", "ServerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryMemberSettings_ChannelCategoryId",
                table: "ServerChannelCategoryMemberSettings",
                column: "ChannelCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryPermission_PermissionId",
                table: "ServerChannelCategoryPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryRole_RoleId",
                table: "ServerChannelCategoryRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryRole_ServerChannelCategoryId",
                table: "ServerChannelCategoryRole",
                column: "ServerChannelCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryRolePermission_PermissionId",
                table: "ServerChannelCategoryRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryRolePermission_RoleId",
                table: "ServerChannelCategoryRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerChannelCategoryRolePermission_ServerChannelCategoryId",
                table: "ServerChannelCategoryRolePermission",
                column: "ServerChannelCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerEmote_ServerId",
                table: "ServerEmote",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerEmote_UploaderId",
                table: "ServerEmote",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerEvent_CreatorId",
                table: "ServerEvent",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerEvent_ServerId",
                table: "ServerEvent",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerInvite_AccountId",
                table: "ServerInvite",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerInvite_ChannelId",
                table: "ServerInvite",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerInvite_InviterId",
                table: "ServerInvite",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerPermission_CategoryId",
                table: "ServerPermission",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfile_FolderId_FolderName",
                table: "ServerProfile",
                columns: new[] { "FolderId", "FolderName" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfile_ServerId",
                table: "ServerProfile",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfileServerRole_AccountId_ServerId",
                table: "ServerProfileServerRole",
                columns: new[] { "AccountId", "ServerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerProfileServerRole_RoleId",
                table: "ServerProfileServerRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSettings_RegionId",
                table: "ServerSettings",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSoundboardSound_AssociatedEmoteId",
                table: "ServerSoundboardSound",
                column: "AssociatedEmoteId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSoundboardSound_ServerId",
                table: "ServerSoundboardSound",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSoundboardSound_UploaderId",
                table: "ServerSoundboardSound",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannel_CategoryId",
                table: "ServerTextChannel",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannel_OwnerId",
                table: "ServerTextChannel",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelAccountMessageTracker_CoOwnerId",
                table: "ServerTextChannelAccountMessageTracker",
                column: "CoOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelAccountMessageTracker_SubjectId",
                table: "ServerTextChannelAccountMessageTracker",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelMemberPermission_AccountId_ServerId",
                table: "ServerTextChannelMemberPermission",
                columns: new[] { "AccountId", "ServerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelMemberPermission_PermissionId",
                table: "ServerTextChannelMemberPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelMemberSettings_AccountId_ServerId",
                table: "ServerTextChannelMemberSettings",
                columns: new[] { "AccountId", "ServerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelMessage_AuthorId",
                table: "ServerTextChannelMessage",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelMessage_MessageHolderId",
                table: "ServerTextChannelMessage",
                column: "MessageHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelMessage_ParentId",
                table: "ServerTextChannelMessage",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelMessageAttachment_MessageId",
                table: "ServerTextChannelMessageAttachment",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelMessagePin_MessageId",
                table: "ServerTextChannelMessagePin",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelPermission_PermissionId",
                table: "ServerTextChannelPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelPinboard_OwnerId",
                table: "ServerTextChannelPinboard",
                column: "OwnerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelRole_RoleId",
                table: "ServerTextChannelRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelRolePermission_PermissionId",
                table: "ServerTextChannelRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerTextChannelRolePermission_RoleId",
                table: "ServerTextChannelRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannel_CategoryId",
                table: "ServerVoiceChannel",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannel_OwnerId",
                table: "ServerVoiceChannel",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannel_RegionId",
                table: "ServerVoiceChannel",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannelMemberPermission_AccountId_ServerId",
                table: "ServerVoiceChannelMemberPermission",
                columns: new[] { "AccountId", "ServerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannelMemberPermission_PermissionId",
                table: "ServerVoiceChannelMemberPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannelMemberSettings_AccountId_ServerId",
                table: "ServerVoiceChannelMemberSettings",
                columns: new[] { "AccountId", "ServerId" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannelPermission_PermissionId",
                table: "ServerVoiceChannelPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannelRole_RoleId",
                table: "ServerVoiceChannelRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannelRolePermission_PermissionId",
                table: "ServerVoiceChannelRolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceChannelRolePermission_RoleId",
                table: "ServerVoiceChannelRolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceInvite_ChannelId",
                table: "ServerVoiceInvite",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerVoiceInvite_InviterId",
                table: "ServerVoiceInvite",
                column: "InviterId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerWebhook_ServerId",
                table: "ServerWebhook",
                column: "ServerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerWebhook_TextChannelId",
                table: "ServerWebhook",
                column: "TextChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_BillingInformationId",
                table: "Subscription",
                column: "BillingInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_CurrencyId",
                table: "Subscription",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_SubscriptionPlanId",
                table: "Subscription",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionActivePeriod_SubcriptionId",
                table: "SubscriptionActivePeriod",
                column: "SubcriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionActivePeriod_TransactionGroupId",
                table: "SubscriptionActivePeriod",
                column: "TransactionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlan_RoleId",
                table: "SubscriptionPlan",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanActivePeriod_SubscriptionPlanId",
                table: "SubscriptionPlanActivePeriod",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTransaction_CurrencyId",
                table: "SubscriptionTransaction",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTransaction_SubscriptionId",
                table: "SubscriptionTransaction",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTransaction_TransactionGroupId",
                table: "SubscriptionTransaction",
                column: "TransactionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTransaction_TransactionTypeId",
                table: "SubscriptionTransaction",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionTransactionGroup_CurrencyId",
                table: "SubscriptionTransactionGroup",
                column: "CurrencyId");
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
                name: "AccountServerMute");

            migrationBuilder.DropTable(
                name: "AccountServerTextChannelMute");

            migrationBuilder.DropTable(
                name: "AccountServerVoiceChannelMute");

            migrationBuilder.DropTable(
                name: "AccountSession");

            migrationBuilder.DropTable(
                name: "AccountSoundboardMute");

            migrationBuilder.DropTable(
                name: "AccountVideoMute");

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
                name: "GameOverlaySettings");

            migrationBuilder.DropTable(
                name: "IncomingFriendRequest");

            migrationBuilder.DropTable(
                name: "Keybind");

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
                name: "ServerAuditLog");

            migrationBuilder.DropTable(
                name: "ServerBan");

            migrationBuilder.DropTable(
                name: "ServerChannelCategoryMemberPermission");

            migrationBuilder.DropTable(
                name: "ServerChannelCategoryPermission");

            migrationBuilder.DropTable(
                name: "ServerChannelCategoryRolePermission");

            migrationBuilder.DropTable(
                name: "ServerEvent");

            migrationBuilder.DropTable(
                name: "ServerInvite");

            migrationBuilder.DropTable(
                name: "ServerProfileServerRole");

            migrationBuilder.DropTable(
                name: "ServerRolePermission");

            migrationBuilder.DropTable(
                name: "ServerSettings");

            migrationBuilder.DropTable(
                name: "ServerSoundboardSound");

            migrationBuilder.DropTable(
                name: "ServerTextChannelAccountMessageTracker");

            migrationBuilder.DropTable(
                name: "ServerTextChannelMemberPermission");

            migrationBuilder.DropTable(
                name: "ServerTextChannelMessageAttachment");

            migrationBuilder.DropTable(
                name: "ServerTextChannelMessagePin");

            migrationBuilder.DropTable(
                name: "ServerTextChannelPermission");

            migrationBuilder.DropTable(
                name: "ServerTextChannelRolePermission");

            migrationBuilder.DropTable(
                name: "ServerVoiceChannelMemberPermission");

            migrationBuilder.DropTable(
                name: "ServerVoiceChannelPermission");

            migrationBuilder.DropTable(
                name: "ServerVoiceChannelRolePermission");

            migrationBuilder.DropTable(
                name: "ServerVoiceInvite");

            migrationBuilder.DropTable(
                name: "ServerWebhook");

            migrationBuilder.DropTable(
                name: "SoundboardSettings");

            migrationBuilder.DropTable(
                name: "StreamerModeSettings");

            migrationBuilder.DropTable(
                name: "SubscriptionActivePeriod");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanActivePeriod");

            migrationBuilder.DropTable(
                name: "SubscriptionTransaction");

            migrationBuilder.DropTable(
                name: "VideoSettings");

            migrationBuilder.DropTable(
                name: "VoiceSettings");

            migrationBuilder.DropTable(
                name: "WindowSettings");

            migrationBuilder.DropTable(
                name: "Connection");

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
                name: "ApplicationKeybind");

            migrationBuilder.DropTable(
                name: "KeybindSettings");

            migrationBuilder.DropTable(
                name: "MessageReportReason");

            migrationBuilder.DropTable(
                name: "MessageReport");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "ProfileReportReason");

            migrationBuilder.DropTable(
                name: "ProfileReport");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "ServerChannelCategoryMemberSettings");

            migrationBuilder.DropTable(
                name: "ServerChannelCategoryRole");

            migrationBuilder.DropTable(
                name: "ServerEmote");

            migrationBuilder.DropTable(
                name: "ServerTextChannelMemberSettings");

            migrationBuilder.DropTable(
                name: "ServerTextChannelMessage");

            migrationBuilder.DropTable(
                name: "ServerTextChannelPinboard");

            migrationBuilder.DropTable(
                name: "ServerTextChannelRole");

            migrationBuilder.DropTable(
                name: "ServerVoiceChannelMemberSettings");

            migrationBuilder.DropTable(
                name: "ServerVoiceChannelRole");

            migrationBuilder.DropTable(
                name: "PaymentType");

            migrationBuilder.DropTable(
                name: "SubscriptionTransactionGroup");

            migrationBuilder.DropTable(
                name: "SubscriptionTransactionRefund");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "ReportedCustomStatus");

            migrationBuilder.DropTable(
                name: "ReportedMessage");

            migrationBuilder.DropTable(
                name: "AccountViolation");

            migrationBuilder.DropTable(
                name: "ReportedProfile");

            migrationBuilder.DropTable(
                name: "ServerTextChannel");

            migrationBuilder.DropTable(
                name: "ServerPermission");

            migrationBuilder.DropTable(
                name: "ServerProfile");

            migrationBuilder.DropTable(
                name: "ServerRole");

            migrationBuilder.DropTable(
                name: "ServerVoiceChannel");

            migrationBuilder.DropTable(
                name: "AcceptedCurrency");

            migrationBuilder.DropTable(
                name: "BillingInformation");

            migrationBuilder.DropTable(
                name: "SubscriptionPlan");

            migrationBuilder.DropTable(
                name: "ServerPermissionCategory");

            migrationBuilder.DropTable(
                name: "AccountServerFolder");

            migrationBuilder.DropTable(
                name: "ServerChannelCategory");

            migrationBuilder.DropTable(
                name: "ServerRegion");

            migrationBuilder.DropTable(
                name: "AccountSettings");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Server");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "AccountActivityStatus");

            migrationBuilder.DropTable(
                name: "AccountCustomStatus");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

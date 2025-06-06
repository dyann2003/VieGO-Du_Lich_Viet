using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class updateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountCodes",
                columns: table => new
                {
                    DiscountCodeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UsedQuantity = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodes", x => x.DiscountCodeID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    PaymentMethodID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__DC31C1F3DA96D595", x => x.PaymentMethodID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE1A8B036B87", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Password = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    RoleId = table.Column<int>(type: "integer", nullable: true),
                    Gender = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserType = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACA952F6BB", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__Users__RoleId__3E52440B",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "ServiceProviders",
                columns: table => new
                {
                    ProviderID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    ProviderType = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ServiceP__B54C689D5CD7CFFC", x => x.ProviderID);
                    table.ForeignKey(
                        name: "FK__ServicePr__UserI__440B1D61",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "TourGuides",
                columns: table => new
                {
                    GuideID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: true),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    GuideType = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TourGuid__E77EE03E436CB6D4", x => x.GuideID);
                    table.ForeignKey(
                        name: "FK__TourGuide__UserI__4AB81AF0",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    TourID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourCode = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    ServiceProviderID = table.Column<int>(type: "integer", nullable: false),
                    TourName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Duration = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    GroupSizeMin = table.Column<int>(type: "integer", nullable: true),
                    GroupSizeMax = table.Column<int>(type: "integer", nullable: true),
                    TourType = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "character varying(10)", unicode: false, maxLength: 10, nullable: false),
                    DepartureCity = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: true),
                    Destination = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Policies = table.Column<string>(type: "text", nullable: true),
                    FeaturedImageURL = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tours__604CEA103FD60ACF", x => x.TourID);
                    table.ForeignKey(
                        name: "FK__Tours__ServicePr__5629CD9C",
                        column: x => x.ServiceProviderID,
                        principalTable: "ServiceProviders",
                        principalColumn: "ProviderID");
                });

            migrationBuilder.CreateTable(
                name: "Itineraries",
                columns: table => new
                {
                    ItineraryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourID = table.Column<int>(type: "integer", nullable: false),
                    DayNumber = table.Column<int>(type: "integer", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Location = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Itinerar__361216A6434CD361", x => x.ItineraryID);
                    table.ForeignKey(
                        name: "FK__Itinerari__TourI__66603565",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateTable(
                name: "TourExclusions",
                columns: table => new
                {
                    ExclusionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourID = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TourExcl__2703AF95C66C02AC", x => x.ExclusionID);
                    table.ForeignKey(
                        name: "FK__TourExclu__TourI__6EF57B66",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateTable(
                name: "TourHighlights",
                columns: table => new
                {
                    HighlightID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourID = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TourHigh__B11CEDD069534D93", x => x.HighlightID);
                    table.ForeignKey(
                        name: "FK__TourHighl__TourI__693CA210",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateTable(
                name: "TourInclusions",
                columns: table => new
                {
                    InclusionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourID = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TourIncl__0486AEBF9957F28C", x => x.InclusionID);
                    table.ForeignKey(
                        name: "FK__TourInclu__TourI__6C190EBB",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateTable(
                name: "TourSchedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourID = table.Column<int>(type: "integer", nullable: false),
                    DepartureDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    AvailableSlots = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    MeetingPoint = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TourSche__9C8A5B690191ADF8", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK__TourSched__TourI__59FA5E80",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourID = table.Column<int>(type: "integer", nullable: false),
                    ScheduleID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    BookingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TravelStartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TravelEndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    NumAdults = table.Column<int>(type: "integer", nullable: false),
                    NumChildren = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    SpecialRequests = table.Column<string>(type: "text", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    BookingStatus = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    PaymentStatus = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false),
                    PaymentMethodID = table.Column<int>(type: "integer", nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DiscountCodeID = table.Column<int>(type: "integer", nullable: true),
                    PaymentNotes = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__73951ACDA720ACAE", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_DiscountCodes_DiscountCodeID",
                        column: x => x.DiscountCodeID,
                        principalTable: "DiscountCodes",
                        principalColumn: "DiscountCodeID");
                    table.ForeignKey(
                        name: "FK__Bookings__Paymen__628FA481",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "PaymentMethodID");
                    table.ForeignKey(
                        name: "FK__Bookings__Schedu__60A75C0F",
                        column: x => x.ScheduleID,
                        principalTable: "TourSchedules",
                        principalColumn: "ScheduleID");
                    table.ForeignKey(
                        name: "FK__Bookings__TourID__5FB337D6",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                    table.ForeignKey(
                        name: "FK__Bookings__UserID__619B8048",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "TourAssignments",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourID = table.Column<int>(type: "integer", nullable: false),
                    ScheduleID = table.Column<int>(type: "integer", nullable: false),
                    GuideID = table.Column<int>(type: "integer", nullable: false),
                    AssignmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TourAssi__32499E57890AFDC1", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK__TourAssig__Guide__7A672E12",
                        column: x => x.GuideID,
                        principalTable: "TourGuides",
                        principalColumn: "GuideID");
                    table.ForeignKey(
                        name: "FK__TourAssig__Sched__797309D9",
                        column: x => x.ScheduleID,
                        principalTable: "TourSchedules",
                        principalColumn: "ScheduleID");
                    table.ForeignKey(
                        name: "FK__TourAssig__TourI__787EE5A0",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TourID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    BookingID = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    ReviewDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Reviews__74BC79AE519CED32", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK__Reviews__Booking__74AE54BC",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID");
                    table.ForeignKey(
                        name: "FK__Reviews__TourID__72C60C4A",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                    table.ForeignKey(
                        name: "FK__Reviews__UserID__73BA3083",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "TourAttendees",
                columns: table => new
                {
                    AttendeeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookingID = table.Column<int>(type: "integer", nullable: false),
                    ScheduleID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: true),
                    FullName = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true),
                    AttendanceStatus = table.Column<string>(type: "character varying(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "Absent")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TourAtte__1844012862DF1396", x => x.AttendeeID);
                    table.ForeignKey(
                        name: "FK__TourAtten__Booki__04E4BC85",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID");
                    table.ForeignKey(
                        name: "FK__TourAtten__Sched__05D8E0BE",
                        column: x => x.ScheduleID,
                        principalTable: "TourSchedules",
                        principalColumn: "ScheduleID");
                    table.ForeignKey(
                        name: "FK__TourAtten__UserI__06CD04F7",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "VoucherUsage",
                columns: table => new
                {
                    UsageID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DiscountCodeID = table.Column<int>(type: "integer", nullable: false),
                    BookingID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    TourID = table.Column<int>(type: "integer", nullable: false),
                    UsageDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VoucherU__29B197C03EFF77A3", x => x.UsageID);
                    table.ForeignKey(
                        name: "FK_VoucherUsage_DiscountCodes_DiscountCodeID",
                        column: x => x.DiscountCodeID,
                        principalTable: "DiscountCodes",
                        principalColumn: "DiscountCodeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__VoucherUs__Booki__7E37BEF6",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID");
                    table.ForeignKey(
                        name: "FK__VoucherUs__TourI__00200768",
                        column: x => x.TourID,
                        principalTable: "Tours",
                        principalColumn: "TourID");
                    table.ForeignKey(
                        name: "FK__VoucherUs__UserI__7F2BE32F",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookingStatus",
                table: "Bookings",
                column: "BookingStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DiscountCodeID",
                table: "Bookings",
                column: "DiscountCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PaymentMethodID",
                table: "Bookings",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ScheduleID",
                table: "Bookings",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TourID",
                table: "Bookings",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserID",
                table: "Bookings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Itineraries_TourID",
                table: "Itineraries",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingID",
                table: "Reviews",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_TourID",
                table: "Reviews",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserID",
                table: "Reviews",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__8A2B61604B73FECF",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_UserID",
                table: "ServiceProviders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__ServiceP__A9D105345E1479E4",
                table: "ServiceProviders",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourAssignments_GuideID",
                table: "TourAssignments",
                column: "GuideID");

            migrationBuilder.CreateIndex(
                name: "IX_TourAssignments_ScheduleID",
                table: "TourAssignments",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_TourAssignments_Status",
                table: "TourAssignments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TourAssignments_TourID",
                table: "TourAssignments",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_TourAttendees_AttendanceStatus",
                table: "TourAttendees",
                column: "AttendanceStatus");

            migrationBuilder.CreateIndex(
                name: "IX_TourAttendees_BookingID",
                table: "TourAttendees",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_TourAttendees_ScheduleID",
                table: "TourAttendees",
                column: "ScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_TourAttendees_UserID",
                table: "TourAttendees",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TourExclusions_TourID",
                table: "TourExclusions",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_UserID",
                table: "TourGuides",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "UQ__TourGuid__A9D1053484EADB1F",
                table: "TourGuides",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourHighlights_TourID",
                table: "TourHighlights",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_TourInclusions_TourID",
                table: "TourInclusions",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_ServiceProviderID",
                table: "Tours",
                column: "ServiceProviderID");

            migrationBuilder.CreateIndex(
                name: "UQ__Tours__1982F8D0366BFBCC",
                table: "Tours",
                column: "TourCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourSchedules_DepartureDate",
                table: "TourSchedules",
                column: "DepartureDate");

            migrationBuilder.CreateIndex(
                name: "IX_TourSchedules_TourID",
                table: "TourSchedules",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534396A980C",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsage_BookingID",
                table: "VoucherUsage",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsage_DiscountCodeID",
                table: "VoucherUsage",
                column: "DiscountCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsage_TourID",
                table: "VoucherUsage",
                column: "TourID");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherUsage_UserID",
                table: "VoucherUsage",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itineraries");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "TourAssignments");

            migrationBuilder.DropTable(
                name: "TourAttendees");

            migrationBuilder.DropTable(
                name: "TourExclusions");

            migrationBuilder.DropTable(
                name: "TourHighlights");

            migrationBuilder.DropTable(
                name: "TourInclusions");

            migrationBuilder.DropTable(
                name: "VoucherUsage");

            migrationBuilder.DropTable(
                name: "TourGuides");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "DiscountCodes");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "TourSchedules");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "ServiceProviders");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

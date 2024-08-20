using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClarityImplementation.API.Migrations
{
    /// <inheritdoc />
    public partial class initialsetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessEmployeeCommunications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessEmployeeCommunications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActiveEmployeeRunoutPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveEmployeeRunoutPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "api_case_contact_change_requests",
                columns: table => new
                {
                    case_contact_change_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    case_contact_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    case_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sf_contact_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    sf_case_owner_user_id = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    sf_task_id = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    version_no = table.Column<int>(type: "int", nullable: true),
                    contact_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    organization_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    organization_state = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    organization_zip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    contact_sub_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    contact_title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    contact_first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    contact_last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    contact_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    contact_is_cons_ben_contact = table.Column<bool>(type: "bit", nullable: false),
                    contact_is_cobra_contact = table.Column<bool>(type: "bit", nullable: false),
                    contact_is_ben_admin_contact = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    employer_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    case_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    case_sub_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    requested_by_email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_case_contact_change_requests", x => x.case_contact_change_id);
                });

            migrationBuilder.CreateTable(
                name: "api_cases",
                columns: table => new
                {
                    case_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    case_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_ready_for_processing = table.Column<bool>(type: "bit", nullable: true),
                    version_no = table.Column<int>(type: "int", nullable: true),
                    case_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_sub_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_division_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_case_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_case_owner_user_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status_last_completed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    sf_account_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_invite_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_curr_page_no = table.Column<int>(type: "int", nullable: true),
                    wizard_curr_step_no = table.Column<int>(type: "int", nullable: true),
                    cobra_submission_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_submission_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_upload_reminder_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step_6_entry_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    kickoff_call_submission_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step0_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step1_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step2_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step3_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step4_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step5_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step6_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step7_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step8_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step9_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    step10_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_entry_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    legal_business_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dba = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street_physical_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    street_line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    web_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    incorporation_date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    laws_of_state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    healthcare_carrier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    healthcare_plan_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dental_care_carrier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dental_care_plan_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vision_care_carrier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    vision_care_plan_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    implementation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    implementation_plan_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_plan_open_enrollment_start_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cons_ben_plan_open_enrollment_end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cons_ben_plan_enrollment_method_employees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_plan_enrollment_data_transmission_to_clarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_plan_estimated_date_enrollment_data_delivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_plan_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_plan_employee_class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_plan_first_payroll_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cons_ben_plan_waiting_period = table.Column<int>(type: "int", nullable: true),
                    cons_ben_plan_parking_transit_waiting_period = table.Column<int>(type: "int", nullable: true),
                    cons_ben_plan_qualified_dependents = table.Column<int>(type: "int", nullable: true),
                    cobra_plan_open_enrollment_start_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cobra_plan_open_enrollment_end_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cobra_plan_enrollment_method_employees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cobra_plan_enrollment_data_transmission_to_clarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cobra_plan_estimated_date_enrollment_data_delivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cobra_plan_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cobra_plan_employee_class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cobra_plan_first_payroll_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cobra_plan_waiting_period = table.Column<int>(type: "int", nullable: true),
                    cobra_plan_parking_transit_waiting_period = table.Column<int>(type: "int", nullable: true),
                    cobra_plan_qualified_dependents = table.Column<int>(type: "int", nullable: true),
                    cons_ben_has_rol_plan = table.Column<bool>(type: "bit", nullable: true),
                    cons_ben_number_of_benefit_eligible_employees = table.Column<int>(type: "int", nullable: true),
                    cons_ben_number_of_participating_employees = table.Column<int>(type: "int", nullable: true),
                    cons_ben_plan_enrollment_template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_plan_enrollment_template_tpa_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_plan_payroll_group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cons_ben_funding_method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    portal_case_edit_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_new_task_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_used_for_testing = table.Column<bool>(type: "bit", nullable: true),
                    form_initial_summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_user_sumbission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_final_summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_updated_from = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status_last_cancelled_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_rollout_error = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    payroll_provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_initial_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_user_sumbission_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_final_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    member_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    member_first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    member_last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_new_task_id_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_new_task_id_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_new_task_id_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_new_task_id_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    form_pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sales_rep_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    client_annual_revenue = table.Column<int>(type: "int", nullable: true),
                    crm_user_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_owner_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_owner_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    funding_submission_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_implementation_contact_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_implementation_contact_first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_implementation_contact_last_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_cases", x => x.case_id);
                });

            migrationBuilder.CreateTable(
                name: "api_file_uploads",
                columns: table => new
                {
                    file_upload_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    case_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uploaded_by_user_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    platform_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    platform_template_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    checker_results = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    upload_results = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    upload_errors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_new_task_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_case_owner_user_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_uploaded_file_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_sub_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_file_uploads", x => x.file_upload_id);
                });

            migrationBuilder.CreateTable(
                name: "api_notification_logs",
                columns: table => new
                {
                    notification_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    destination_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notification_source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notification_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notification_category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notification_sub_category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notification_content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notification_sent_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notification_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_owner_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notification_label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_task_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mailbox_uid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mailbox_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mailbox_folder_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_notification_logs", x => x.notification_id);
                });

            migrationBuilder.CreateTable(
                name: "BenefitPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BenefitPlanTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitPlanTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrokerRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyTotalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompanyDivision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAffiliatedCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KickoffStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KickoffUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalendlyUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseOwnerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseOwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_case_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_implementation_contact_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_implementation_contact_first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_implementation_contact_last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    TotalProgress = table.Column<int>(type: "int", nullable: false),
                    TotalPlanProgress = table.Column<int>(type: "int", nullable: false),
                    PendingProgress = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 8, 20, 13, 10, 32, 616, DateTimeKind.Local).AddTicks(5540)),
                    CompanyProfileStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BenefitStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntroStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BenefitReviewStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraReviewStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundingReviewStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reminder1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reminder1SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reminder2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reminder2SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reminder3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reminder3SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadEnrollEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadEnrollEmailDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadEnrollReminder1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadEnrollReminder1Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadEnrollReminder2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadEnrollReminder2Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadEnrollReminder3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadEnrollReminder3Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEmailSent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusUpdateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalesRepName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientAnnualRevenue = table.Column<int>(type: "int", nullable: true),
                    LastRolloutError = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUsedForTesting = table.Column<bool>(type: "bit", nullable: true),
                    IsReadyForProcessing = table.Column<bool>(type: "bit", nullable: true),
                    AdminStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrmUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalFundingProgress = table.Column<int>(type: "int", nullable: true),
                    ImplementationPlans = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactResponsibilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactResponsibilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataTransmissionMethods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataTransmissionMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EBPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EligibilityDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EligibilityDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Isname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Isemail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementationContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CaseOwnerFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseOwnerLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseOwnerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfReminders = table.Column<int>(type: "int", nullable: false),
                    NoOfDays = table.Column<int>(type: "int", nullable: false),
                    EmailSentdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaseID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployerEntityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerEntityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnrolledEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolledEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HRAReimbursableExpenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRAReimbursableExpenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncorporationStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncorporationStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InformationProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InformationProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KickoffUserDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalendlyUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCalenderConnected = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousEventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DaysSincePreviousEvent = table.Column<int>(type: "int", nullable: false),
                    TotalEventsInLast30Days = table.Column<int>(type: "int", nullable: false),
                    LifeTimeEvents = table.Column<int>(type: "int", nullable: false),
                    VideoConferrencingIntegration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CalendlyAPIKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KickoffUserDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayrollProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayrollVendorNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollVendorNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayScheduleFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayScheduleFrequencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RunoutPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunoutPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TerminatedEmployeeCoverageEndPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminatedEmployeeCoverageEndPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TerminatedEmployeeRunoutPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminatedEmployeeRunoutPeriods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffiliatedCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AffiliateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerEntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EINNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCompanyDivision = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeBenefitsFundingFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAFundingFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitConsumerBenefitFundingForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFeeForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPaymentForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitConsumerBenefitFunding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCreditCobraPremiumRemittance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliatedCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliatedCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "api_case_contacts",
                columns: table => new
                {
                    case_contact_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    case_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_contact_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    version_no = table.Column<int>(type: "int", nullable: true),
                    contact_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organization_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organization_state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organization_zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_sub_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_is_cons_ben_contact = table.Column<int>(type: "int", nullable: true),
                    contact_is_cobra_contact = table.Column<int>(type: "int", nullable: true),
                    contact_is_implementation_contact = table.Column<int>(type: "int", nullable: true),
                    contact_is_ben_admin_contact = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_email_org = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    benefit_plan_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_is_participant_contact = table.Column<int>(type: "int", nullable: true),
                    is_invited = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_case_collaborator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_primary_contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_case_contacts", x => x.case_contact_id);
                    table.ForeignKey(
                        name: "FK_api_case_contacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BrokerContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrokerageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryContactChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_contact_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_contact_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInvite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactIsImplementationContact = table.Column<int>(type: "int", nullable: true),
                    IsPrimaryContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrokerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrokerContacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendlyEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    assigned_to = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    event_type_uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    event_type_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    event_start_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    event_end_time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    invitee_uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    invitee_full_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    invitee_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    answer_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    answer_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendlyEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendlyEvents_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAFundings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsRequiredSeparateRemittance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCreditCobraPremiumRemittance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraDefaultFundingFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPremiumProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrokerFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrokerLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrokerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCobraACHFormChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraSignatureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraSignatureTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraSignatureDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    TotalFundingProgress = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAFundings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAFundings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsOpenEnrollment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAPlans_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sf_contact_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    case_contact_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInvite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactIsImplementationContact = table.Column<int>(type: "int", nullable: true),
                    IsPrimaryContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DBA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EINNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementationPlanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerEntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibleEmployees = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDetails_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDivisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeBenefitsFundingFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAFundingFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitConsumerBenefitFundingForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFeeForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPaymentForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitConsumerBenefitFunding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCreditCobraPremiumRemittance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDivisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDivisions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyStatuses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsFileUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnrollmentTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsFileUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsFileUploads_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsFundings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsClarityRequiredSeparateAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsClientRequiredSeparateAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundingModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundingDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnAccountFundingDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsACHFormChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    TotalFundingProgress = table.Column<int>(type: "int", nullable: false),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForBenefitFunding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForBenefitFunding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForBenefitFunding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForClarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForClarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForClarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitConsumerBenefitFundingForClarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFeeForClarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPaymentForClarity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitConsumerBenefitFundingForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFeeForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPaymentForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitConsumerBenefitFunding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientFundingFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsFundings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsFundings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsMidYearPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPriorYearPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSamePlanEligibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsPlans_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageMetaDataFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldMetaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tooltip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Regex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageMetaDataFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageMetaDataFields_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffiliatedCompanyDivisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeBenefitsFundingFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAFundingFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitConsumerBenefitFundingForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFeeForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPaymentForClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitConsumerBenefitFunding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCreditCobraPremiumRemittance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebitMonthlyAdministrationFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBrokerorPartnerPayment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForCreditCobra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNameForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountNumberForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankRoutingNumberForAdminFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliatedCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliatedCompanyDivisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliatedCompanyDivisions_AffiliatedCompanies_AffiliatedCompanyId",
                        column: x => x.AffiliatedCompanyId,
                        principalTable: "AffiliatedCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffiliatedCompanyFundingFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadedFileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliatedCompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliatedCompanyFundingFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliatedCompanyFundingFiles_AffiliatedCompanies_AffiliatedCompanyId",
                        column: x => x.AffiliatedCompanyId,
                        principalTable: "AffiliatedCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAFundingFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CobraPremiumProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraUploadedFileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraFundingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAFundingFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAFundingFiles_COBRAFundings_CobraFundingId",
                        column: x => x.CobraFundingId,
                        principalTable: "COBRAFundings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRADentalPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eight34ContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubGroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRenewalDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDivisionSpecific = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabilityExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRADentalPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRADentalPlans_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAEAPPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eight34ContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubGroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRenewalDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDivisionSpecific = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabilityExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAEAPPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAEAPPlans_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAFSAPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eight34ContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubGroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRenewalDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDivisionSpecific = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabilityExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAFSAPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAFSAPlans_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CobraGeneralInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsChange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClarityEventReceiptionMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentBenefitAdministrationPlatform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherBenefitAdminPlatform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentPayrollPlatform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherPayrollPlatform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobraGeneralInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobraGeneralInformations_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAHRAPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eight34ContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubGroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRenewalDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDivisionSpecific = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabilityExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAHRAPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAHRAPlans_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAInsurancePlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eight34ContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubGroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRenewalDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDivisionSpecific = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabilityExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAInsurancePlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAInsurancePlans_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAMedicalPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eight34ContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubGroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRenewalDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDivisionSpecific = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabilityExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAMedicalPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAMedicalPlans_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cobraOpenEnrollmentManagements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpenEnrollmentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfDaysOffered = table.Column<int>(type: "int", nullable: false),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cobraOpenEnrollmentManagements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cobraOpenEnrollmentManagements_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAVisionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eight34ContactEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncorporationState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubGroupNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRenewalDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDivisionSpecific = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DivisionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisabilityExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanRateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRateEndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAVisionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAVisionPlans_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentAndEligibilityContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BenefitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BenefitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentAndEligibilityContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentAndEligibilityContacts_COBRAPlans_COBRAPlanId",
                        column: x => x.COBRAPlanId,
                        principalTable: "COBRAPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDivisionFundingFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadedFileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyDivisionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDivisionFundingFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDivisionFundingFiles_CompanyDivisions_CompanyDivisionId",
                        column: x => x.CompanyDivisionId,
                        principalTable: "CompanyDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CobraBenefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    EmployeeBenefitsFileUploadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobraBenefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobraBenefits_EmployeeBenefitsFileUploads_EmployeeBenefitsFileUploadId",
                        column: x => x.EmployeeBenefitsFileUploadId,
                        principalTable: "EmployeeBenefitsFileUploads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    EmployeeBenefitsFileUploadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefits_EmployeeBenefitsFileUploads_EmployeeBenefitsFileUploadId",
                        column: x => x.EmployeeBenefitsFileUploadId,
                        principalTable: "EmployeeBenefitsFileUploads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsFundingFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundingModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedFileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsFundingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsFundingFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsFundingFiles_EmployeeBenefitsFundings_EmployeeBenefitsFundingId",
                        column: x => x.EmployeeBenefitsFundingId,
                        principalTable: "EmployeeBenefitsFundings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsEnrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataTransmissionMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsEnrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsEnrollments_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsFSAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsHSA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOfferLPFSA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFSAChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLPFSAChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStandardPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCustomizePlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinAnnualElectionAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAnnualElectionAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveEmployeeRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherOptionForActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherOptionForTerminated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminatedEmployeeRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminatedEmployeeCoverageEndPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowCarryOver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowGracePeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RunOutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsFSAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsFSAs_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsGeneralInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InformationProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformationProviderVendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformationProviderVendorContactFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformationProviderVendorContactLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InformationProviderVendorContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayrollProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayrollProviderVendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayrollProviderOtherVendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayrollProviderFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayrollProviderLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayrollProviderContactEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutopostOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContributionManagerOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientDepositOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VendorDepositOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsGeneralInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsGeneralInformations_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsHRAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HRAType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveEmployeeRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminatedEmployeeRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminatedDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReimbursableExpenses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherExpenses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeChild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeSpouse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Family = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProRated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmployeeResponsible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleEmployee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleEmployeeChild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleEmployeeSpouse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHRAEmployeeResponsible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HRAResponsibleEmployee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HRAResponsibleEmployeeChild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HRAResponsibleEmployeeSpouse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HRAResponsibleFamily = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumReimbursement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsParticipantResponsible = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HRAClaimPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HRAUnusedFund = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxRolloverAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnusedFundsPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHRAorFSA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPlanForClarityBenefitCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClarityBenefitCardType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDependentCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDependentCardOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalPlanDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsHRAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsHRAs_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsHSAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsOfferHSA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrolledEmployee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBulkTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPayrollAdvance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepaymentAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFullBalanceAdvance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmployerContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCardIssued = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSpouseOrDependent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsHSAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsHSAs_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsSmartRides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsParkingChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTransitChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkingStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransitStartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkingPreTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransitPreTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsParkingPostTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTransitPostTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkingOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransitOption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParkingRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherOptionForParking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherOptionForTransit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransitRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsParkingAddPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTransitAddPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsSmartRides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsSmartRides_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MidYearPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidYearPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MidYearPlans_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayScheduleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayScheduleFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPaycheckDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayScheduleTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayScheduleTypes_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Group = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibilityDefinition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false),
                    OtherOptionEligibilityDefinition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanTypes_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriorYearPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorYearPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriorYearPlans_EmployeeBenefitsPlans_EmployeeBenefitsPlanId",
                        column: x => x.EmployeeBenefitsPlanId,
                        principalTable: "EmployeeBenefitsPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AffiliatedCompanyDivisionFundingFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadedFileURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliatedCompanyDivisionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliatedCompanyDivisionFundingFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliatedCompanyDivisionFundingFiles_AffiliatedCompanyDivisions_AffiliatedCompanyDivisionId",
                        column: x => x.AffiliatedCompanyDivisionId,
                        principalTable: "AffiliatedCompanyDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DentalCoverageRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DentalPlanId = table.Column<int>(type: "int", nullable: false),
                    COBRADentalPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DentalCoverageRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DentalCoverageRates_COBRADentalPlans_COBRADentalPlanId",
                        column: x => x.COBRADentalPlanId,
                        principalTable: "COBRADentalPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EAPCoverageRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EAPPlanId = table.Column<int>(type: "int", nullable: false),
                    COBRAEAPPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EAPCoverageRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EAPCoverageRates_COBRAEAPPlans_COBRAEAPPlanId",
                        column: x => x.COBRAEAPPlanId,
                        principalTable: "COBRAEAPPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FSACoverageRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FSAPlanId = table.Column<int>(type: "int", nullable: false),
                    COBRAFSAPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FSACoverageRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FSACoverageRates_COBRAFSAPlans_COBRAFSAPlanId",
                        column: x => x.COBRAFSAPlanId,
                        principalTable: "COBRAFSAPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HRACoverageRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HRAPlanId = table.Column<int>(type: "int", nullable: false),
                    COBRAHRAPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRACoverageRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HRACoverageRates_COBRAHRAPlans_COBRAHRAPlanId",
                        column: x => x.COBRAHRAPlanId,
                        principalTable: "COBRAHRAPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePlanCoverageRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsurancePlanId = table.Column<int>(type: "int", nullable: false),
                    COBRAInsurancePlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePlanCoverageRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsurancePlanCoverageRates_COBRAInsurancePlans_COBRAInsurancePlanId",
                        column: x => x.COBRAInsurancePlanId,
                        principalTable: "COBRAInsurancePlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MedicalPlanCoverageRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COBRAMedicalPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalPlanCoverageRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalPlanCoverageRates_COBRAMedicalPlans_COBRAMedicalPlanId",
                        column: x => x.COBRAMedicalPlanId,
                        principalTable: "COBRAMedicalPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisionCoverageRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverageLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FutureRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisionPlanId = table.Column<int>(type: "int", nullable: false),
                    COBRAVisionPlanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisionCoverageRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisionCoverageRates_COBRAVisionPlans_COBRAVisionPlanId",
                        column: x => x.COBRAVisionPlanId,
                        principalTable: "COBRAVisionPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "COBRABrokerContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollmentAndEligibilityContactId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRABrokerContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRABrokerContacts_EnrollmentAndEligibilityContacts_EnrollmentAndEligibilityContactId",
                        column: x => x.EnrollmentAndEligibilityContactId,
                        principalTable: "EnrollmentAndEligibilityContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COBRAClientContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollmentAndEligibilityContactId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COBRAClientContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_COBRAClientContacts_EnrollmentAndEligibilityContacts_EnrollmentAndEligibilityContactId",
                        column: x => x.EnrollmentAndEligibilityContactId,
                        principalTable: "EnrollmentAndEligibilityContacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CobraFileUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CobraBenefitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobraFileUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobraFileUploads_CobraBenefits_CobraBenefitId",
                        column: x => x.CobraBenefitId,
                        principalTable: "CobraBenefits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsFileUploadItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsFileUploadItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsFileUploadItems_EmployeeBenefits_EmployeeBenefitId",
                        column: x => x.EmployeeBenefitId,
                        principalTable: "EmployeeBenefits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsDCAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDCAChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStandardPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCustomizePlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinAnnualElectionAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAnnualElectionAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveEmployeeRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminatedEmployeeRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherOptionForActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherOptionForTerminated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminatedEmployeeCoverageEndPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowGracePeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    RunOutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsFSAId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsDCAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsDCAs_EmployeeBenefitsFSAs_EmployeeBenefitsFSAId",
                        column: x => x.EmployeeBenefitsFSAId,
                        principalTable: "EmployeeBenefitsFSAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBenefitsLPFSAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsLPFSAChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStandardPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTestStandardPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCustomizePlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinAnnualElectionAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAnnualElectionAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveEmployeeRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminatedEmployeeRunoutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherOptionForActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherOptionForTerminated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminatedEmployeeCoverageEndPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowCarryOver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTestAllowCarryOver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowGracePeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTestAllowGracePeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    RunOutPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsFSAId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBenefitsLPFSAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBenefitsLPFSAs_EmployeeBenefitsFSAs_EmployeeBenefitsFSAId",
                        column: x => x.EmployeeBenefitsFSAId,
                        principalTable: "EmployeeBenefitsFSAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployerContributionGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EligibilityContigent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SingleContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeSpouseContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeChildContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyContribution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeBenefitsHSAId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerContributionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerContributionGroups_EmployeeBenefitsHSAs_EmployeeBenefitsHSAId",
                        column: x => x.EmployeeBenefitsHSAId,
                        principalTable: "EmployeeBenefitsHSAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CompanyId",
                table: "Addresses",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatedCompanies_CompanyId",
                table: "AffiliatedCompanies",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatedCompanyDivisionFundingFiles_AffiliatedCompanyDivisionId",
                table: "AffiliatedCompanyDivisionFundingFiles",
                column: "AffiliatedCompanyDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatedCompanyDivisions_AffiliatedCompanyId",
                table: "AffiliatedCompanyDivisions",
                column: "AffiliatedCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AffiliatedCompanyFundingFiles_AffiliatedCompanyId",
                table: "AffiliatedCompanyFundingFiles",
                column: "AffiliatedCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_api_case_contacts_CompanyId",
                table: "api_case_contacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_BrokerContacts_CompanyId",
                table: "BrokerContacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendlyEvents_CompanyId",
                table: "CalendlyEvents",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CobraBenefits_EmployeeBenefitsFileUploadId",
                table: "CobraBenefits",
                column: "EmployeeBenefitsFileUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_COBRABrokerContacts_EnrollmentAndEligibilityContactId",
                table: "COBRABrokerContacts",
                column: "EnrollmentAndEligibilityContactId");

            migrationBuilder.CreateIndex(
                name: "IX_COBRAClientContacts_EnrollmentAndEligibilityContactId",
                table: "COBRAClientContacts",
                column: "EnrollmentAndEligibilityContactId");

            migrationBuilder.CreateIndex(
                name: "IX_COBRADentalPlans_COBRAPlanId",
                table: "COBRADentalPlans",
                column: "COBRAPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_COBRAEAPPlans_COBRAPlanId",
                table: "COBRAEAPPlans",
                column: "COBRAPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CobraFileUploads_CobraBenefitId",
                table: "CobraFileUploads",
                column: "CobraBenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_COBRAFSAPlans_COBRAPlanId",
                table: "COBRAFSAPlans",
                column: "COBRAPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_COBRAFundingFiles_CobraFundingId",
                table: "COBRAFundingFiles",
                column: "CobraFundingId");

            migrationBuilder.CreateIndex(
                name: "IX_COBRAFundings_CompanyId",
                table: "COBRAFundings",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CobraGeneralInformations_COBRAPlanId",
                table: "CobraGeneralInformations",
                column: "COBRAPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COBRAHRAPlans_COBRAPlanId",
                table: "COBRAHRAPlans",
                column: "COBRAPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_COBRAInsurancePlans_COBRAPlanId",
                table: "COBRAInsurancePlans",
                column: "COBRAPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_COBRAMedicalPlans_COBRAPlanId",
                table: "COBRAMedicalPlans",
                column: "COBRAPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_cobraOpenEnrollmentManagements_COBRAPlanId",
                table: "cobraOpenEnrollmentManagements",
                column: "COBRAPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COBRAPlans_CompanyId",
                table: "COBRAPlans",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COBRAVisionPlans_COBRAPlanId",
                table: "COBRAVisionPlans",
                column: "COBRAPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContacts_CompanyId",
                table: "CompanyContacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDetails_CompanyId",
                table: "CompanyDetails",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDivisionFundingFiles_CompanyDivisionId",
                table: "CompanyDivisionFundingFiles",
                column: "CompanyDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDivisions_CompanyId",
                table: "CompanyDivisions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyStatuses_CompanyId",
                table: "CompanyStatuses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DentalCoverageRates_COBRADentalPlanId",
                table: "DentalCoverageRates",
                column: "COBRADentalPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EAPCoverageRates_COBRAEAPPlanId",
                table: "EAPCoverageRates",
                column: "COBRAEAPPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefits_EmployeeBenefitsFileUploadId",
                table: "EmployeeBenefits",
                column: "EmployeeBenefitsFileUploadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsDCAs_EmployeeBenefitsFSAId",
                table: "EmployeeBenefitsDCAs",
                column: "EmployeeBenefitsFSAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsEnrollments_EmployeeBenefitsPlanId",
                table: "EmployeeBenefitsEnrollments",
                column: "EmployeeBenefitsPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsFileUploadItems_EmployeeBenefitId",
                table: "EmployeeBenefitsFileUploadItems",
                column: "EmployeeBenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsFileUploads_CompanyId",
                table: "EmployeeBenefitsFileUploads",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsFSAs_EmployeeBenefitsPlanId",
                table: "EmployeeBenefitsFSAs",
                column: "EmployeeBenefitsPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsFundingFiles_EmployeeBenefitsFundingId",
                table: "EmployeeBenefitsFundingFiles",
                column: "EmployeeBenefitsFundingId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsFundings_CompanyId",
                table: "EmployeeBenefitsFundings",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsGeneralInformations_EmployeeBenefitsPlanId",
                table: "EmployeeBenefitsGeneralInformations",
                column: "EmployeeBenefitsPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsHRAs_EmployeeBenefitsPlanId",
                table: "EmployeeBenefitsHRAs",
                column: "EmployeeBenefitsPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsHSAs_EmployeeBenefitsPlanId",
                table: "EmployeeBenefitsHSAs",
                column: "EmployeeBenefitsPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsLPFSAs_EmployeeBenefitsFSAId",
                table: "EmployeeBenefitsLPFSAs",
                column: "EmployeeBenefitsFSAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsPlans_CompanyId",
                table: "EmployeeBenefitsPlans",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBenefitsSmartRides_EmployeeBenefitsPlanId",
                table: "EmployeeBenefitsSmartRides",
                column: "EmployeeBenefitsPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerContributionGroups_EmployeeBenefitsHSAId",
                table: "EmployerContributionGroups",
                column: "EmployeeBenefitsHSAId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentAndEligibilityContacts_COBRAPlanId",
                table: "EnrollmentAndEligibilityContacts",
                column: "COBRAPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FSACoverageRates_COBRAFSAPlanId",
                table: "FSACoverageRates",
                column: "COBRAFSAPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_HRACoverageRates_COBRAHRAPlanId",
                table: "HRACoverageRates",
                column: "COBRAHRAPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePlanCoverageRates_COBRAInsurancePlanId",
                table: "InsurancePlanCoverageRates",
                column: "COBRAInsurancePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalPlanCoverageRates_COBRAMedicalPlanId",
                table: "MedicalPlanCoverageRates",
                column: "COBRAMedicalPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MidYearPlans_EmployeeBenefitsPlanId",
                table: "MidYearPlans",
                column: "EmployeeBenefitsPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PageMetaDataFields_PageId",
                table: "PageMetaDataFields",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PayScheduleTypes_EmployeeBenefitsPlanId",
                table: "PayScheduleTypes",
                column: "EmployeeBenefitsPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTypes_EmployeeBenefitsPlanId",
                table: "PlanTypes",
                column: "EmployeeBenefitsPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorYearPlans_EmployeeBenefitsPlanId",
                table: "PriorYearPlans",
                column: "EmployeeBenefitsPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_VisionCoverageRates_COBRAVisionPlanId",
                table: "VisionCoverageRates",
                column: "COBRAVisionPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessEmployeeCommunications");

            migrationBuilder.DropTable(
                name: "ActiveEmployeeRunoutPeriods");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AffiliatedCompanyDivisionFundingFiles");

            migrationBuilder.DropTable(
                name: "AffiliatedCompanyFundingFiles");

            migrationBuilder.DropTable(
                name: "api_case_contact_change_requests");

            migrationBuilder.DropTable(
                name: "api_case_contacts");

            migrationBuilder.DropTable(
                name: "api_cases");

            migrationBuilder.DropTable(
                name: "api_file_uploads");

            migrationBuilder.DropTable(
                name: "api_notification_logs");

            migrationBuilder.DropTable(
                name: "BenefitPlans");

            migrationBuilder.DropTable(
                name: "BenefitPlanTypes");

            migrationBuilder.DropTable(
                name: "BrokerContacts");

            migrationBuilder.DropTable(
                name: "BrokerRoles");

            migrationBuilder.DropTable(
                name: "CalendlyEvents");

            migrationBuilder.DropTable(
                name: "COBRABrokerContacts");

            migrationBuilder.DropTable(
                name: "COBRAClientContacts");

            migrationBuilder.DropTable(
                name: "CobraFileUploads");

            migrationBuilder.DropTable(
                name: "COBRAFundingFiles");

            migrationBuilder.DropTable(
                name: "CobraGeneralInformations");

            migrationBuilder.DropTable(
                name: "cobraOpenEnrollmentManagements");

            migrationBuilder.DropTable(
                name: "CompanyContacts");

            migrationBuilder.DropTable(
                name: "CompanyDetails");

            migrationBuilder.DropTable(
                name: "CompanyDivisionFundingFiles");

            migrationBuilder.DropTable(
                name: "CompanyStatuses");

            migrationBuilder.DropTable(
                name: "ContactResponsibilities");

            migrationBuilder.DropTable(
                name: "ContactRoles");

            migrationBuilder.DropTable(
                name: "DataTransmissionMethods");

            migrationBuilder.DropTable(
                name: "DentalCoverageRates");

            migrationBuilder.DropTable(
                name: "EAPCoverageRates");

            migrationBuilder.DropTable(
                name: "EBPlans");

            migrationBuilder.DropTable(
                name: "EligibilityDefinitions");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsDCAs");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsEnrollments");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsFileUploadItems");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsFundingFiles");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsGeneralInformations");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsHRAs");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsLPFSAs");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsSmartRides");

            migrationBuilder.DropTable(
                name: "EmployeeGroups");

            migrationBuilder.DropTable(
                name: "EmployerContributionGroups");

            migrationBuilder.DropTable(
                name: "EmployerEntityTypes");

            migrationBuilder.DropTable(
                name: "EnrolledEmployees");

            migrationBuilder.DropTable(
                name: "FSACoverageRates");

            migrationBuilder.DropTable(
                name: "HRACoverageRates");

            migrationBuilder.DropTable(
                name: "HRAReimbursableExpenses");

            migrationBuilder.DropTable(
                name: "IncorporationStates");

            migrationBuilder.DropTable(
                name: "InformationProviders");

            migrationBuilder.DropTable(
                name: "InsurancePlanCoverageRates");

            migrationBuilder.DropTable(
                name: "KickoffUserDatas");

            migrationBuilder.DropTable(
                name: "MedicalPlanCoverageRates");

            migrationBuilder.DropTable(
                name: "MidYearPlans");

            migrationBuilder.DropTable(
                name: "PageMetaDataFields");

            migrationBuilder.DropTable(
                name: "PayrollProviders");

            migrationBuilder.DropTable(
                name: "PayrollVendorNames");

            migrationBuilder.DropTable(
                name: "PayScheduleFrequencies");

            migrationBuilder.DropTable(
                name: "PayScheduleTypes");

            migrationBuilder.DropTable(
                name: "PlanTypes");

            migrationBuilder.DropTable(
                name: "PriorYearPlans");

            migrationBuilder.DropTable(
                name: "RunoutPeriods");

            migrationBuilder.DropTable(
                name: "TerminatedEmployeeCoverageEndPeriods");

            migrationBuilder.DropTable(
                name: "TerminatedEmployeeRunoutPeriods");

            migrationBuilder.DropTable(
                name: "VisionCoverageRates");

            migrationBuilder.DropTable(
                name: "AffiliatedCompanyDivisions");

            migrationBuilder.DropTable(
                name: "EnrollmentAndEligibilityContacts");

            migrationBuilder.DropTable(
                name: "CobraBenefits");

            migrationBuilder.DropTable(
                name: "COBRAFundings");

            migrationBuilder.DropTable(
                name: "CompanyDivisions");

            migrationBuilder.DropTable(
                name: "COBRADentalPlans");

            migrationBuilder.DropTable(
                name: "COBRAEAPPlans");

            migrationBuilder.DropTable(
                name: "EmployeeBenefits");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsFundings");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsFSAs");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsHSAs");

            migrationBuilder.DropTable(
                name: "COBRAFSAPlans");

            migrationBuilder.DropTable(
                name: "COBRAHRAPlans");

            migrationBuilder.DropTable(
                name: "COBRAInsurancePlans");

            migrationBuilder.DropTable(
                name: "COBRAMedicalPlans");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "COBRAVisionPlans");

            migrationBuilder.DropTable(
                name: "AffiliatedCompanies");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsFileUploads");

            migrationBuilder.DropTable(
                name: "EmployeeBenefitsPlans");

            migrationBuilder.DropTable(
                name: "COBRAPlans");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}

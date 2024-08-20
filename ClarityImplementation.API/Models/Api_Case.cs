using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClarityImplementation.API.Models
{
    [Table("api_cases")]
    public class Api_Case
    {
        [Key]
        [Column("case_id")]
        public string CaseId { get; set; }
        [Column("case_status")]
        public string? CaseStatus { get; set; } = "New";
        [Column("is_ready_for_processing")]
        [DefaultValue(false)]
        
        public bool? IsReadyForProcessing { get; set; } = false; // Assuming an int meant to represent a boolean, 0 = false
        [Column("version_no")]
        public int? VersionNo { get; set; } = 0;
        [Column("case_type")]
        public string? CaseType { get; set; }
        [Column("case_sub_type")]
        public string? CaseSubType { get; set; }
        [Column("employer_id")]
        public string? EmployerId { get; set; }
        [Column("employer_name")]
        public string? EmployerName { get; set; }
        [Column("employer_division_name")]
        public string? EmployerDivisionName { get; set; }
        [Column("sf_case_id")]
        public string? SfCaseId { get; set; }
        [Column("sf_case_owner_user_id")]
        public string? SfCaseOwnerUserId { get; set; }
        [Column("status_last_completed_at")]
        public DateTime? StatusLastCompletedAt { get; set; }
        [Column("sf_account_no")]
        public string? SfAccountNo { get; set; }
        [Column("form_invite_token")]
        public string? FormInviteToken { get; set; }
        [Column("form_curr_page_no")]
        public int? FormCurrPageNo { get; set; } = 1;
        [Column("wizard_curr_step_no")]
        public int? WizardCurrStepNo { get; set; } = 1;
        [Column("cobra_submission_status")]
        public string? CobraSubmissionStatus { get; set; }
        [Column("cons_ben_submission_status")]
        public string? ConsBenSubmissionStatus { get; set; }
        [Column("file_upload_reminder_status")]
        public string? FileUploadReminderStatus { get; set; }
        [Column("step_6_entry_date")]
        public DateTime? Step6EntryDate { get; set; }
        [Column("kickoff_call_submission_status")]
        public string? KickoffCallSubmissionStatus { get; set; }
        [Column("step0_status")]
        public string? Step0Status { get; set; }
        [Column("step1_status")]
        public string? Step1Status { get; set; }
        [Column("step2_status")]
        public string? Step2Status { get; set; }
        [Column("step3_status")]
        public string? Step3Status { get; set; }
        [Column("step4_status")]
        public string? Step4Status { get; set; }
        [Column("step5_status")]
        public string? Step5Status { get; set; }
        [Column("step6_status")]
        public string? Step6Status { get; set; }
        [Column("step7_status")]
        public string? Step7Status { get; set; }
        [Column("step8_status")]
        public string? Step8Status { get; set; }
        [Column("step9_status")]
        public string? Step9Status { get; set; }
        [Column("step10_status")]
        public string? Step10Status { get; set; }
        [Column("form_type")]
        public string? FormType { get; set; } = "Formidable Forms";
        [Column("form_entry_id")]
        public string? FormEntryId { get; set; }
        [Column("legal_business_name")]
        public string? LegalBusinessName { get; set; }
        [Column("dba")]
        public string? Dba { get; set; }
        [Column("street_physical_address")]
        public string? StreetPhysicalAddress { get; set; }
        [Column("street_line2")]
        public string? StreetLine2 { get; set; }
        [Column("city")]
        public string? City { get; set; }
        [Column("state")]
        public string? State { get; set; }
        [Column("zip")]
        public string? Zip { get; set; }
        [Column("phone")]
        public string? Phone { get; set; }
        [Column("fax")]
        public string? Fax { get; set; }
        [Column("web_address")]
        public string? WebAddress { get; set; }
        [Column("incorporation_date")]
        public string? IncorporationDate { get; set; } // Assuming string?, might require conversion to DateTime
        [Column("laws_of_state")]
        public string? LawsOfState { get; set; }
        [Column("healthcare_carrier")]
        public string? HealthcareCarrier { get; set; }
        [Column("healthcare_plan_type")]
        public string? HealthcarePlanType { get; set; }
        [Column("dental_care_carrier")]
        public string? DentalCareCarrier { get; set; }
        [Column("dental_care_plan_type")]
        public string? DentalCarePlanType { get; set; }
        [Column("vision_care_carrier")]
        public string? VisionCareCarrier { get; set; }
        [Column("vision_care_plan_type")]
        public string? VisionCarePlanType { get; set; }
        [Column("implementation_date")]
        public DateTime ImplementationDate { get; set; }
        [Column("implementation_plan_type")]
        public string? ImplementationPlanType { get; set; }
        [Column("cons_ben_plan_open_enrollment_start_date")]
        public DateTime? ConsBenPlanOpenEnrollmentStartDate { get; set; }
        [Column("cons_ben_plan_open_enrollment_end_date")]
        public DateTime? ConsBenPlanOpenEnrollmentEndDate { get; set; }
        [Column("cons_ben_plan_enrollment_method_employees")]
        public string? ConsBenPlanEnrollmentMethodEmployees { get; set; }
        [Column("cons_ben_plan_enrollment_data_transmission_to_clarity")]
        public string? ConsBenPlanEnrollmentDataTransmissionToClarity { get; set; }
        [Column("cons_ben_plan_estimated_date_enrollment_data_delivery")]
        public string? ConsBenPlanEstimatedDateEnrollmentDataDelivery { get; set; }
        [Column("cons_ben_plan_type")]
        public string? ConsBenPlanType { get; set; }
        [Column("cons_ben_plan_employee_class")]
        public string? ConsBenPlanEmployeeClass { get; set; }
        [Column("cons_ben_plan_first_payroll_date")]
        public DateTime? ConsBenPlanFirstPayrollDate { get; set; }
        [Column("cons_ben_plan_waiting_period")]
        public int? ConsBenPlanWaitingPeriod { get; set; }
        [Column("cons_ben_plan_parking_transit_waiting_period")]
        public int? ConsBenPlanParkingTransitWaitingPeriod { get; set; }
        [Column("cons_ben_plan_qualified_dependents")]
        public int? ConsBenPlanQualifiedDependents { get; set; }
        [Column("cobra_plan_open_enrollment_start_date")]
        public DateTime? CobraPlanOpenEnrollmentStartDate { get; set; }
        [Column("cobra_plan_open_enrollment_end_date")]
        public DateTime? CobraPlanOpenEnrollmentEndDate { get; set; }
        [Column("cobra_plan_enrollment_method_employees")]
        public string? CobraPlanEnrollmentMethodEmployees { get; set; }
        [Column("cobra_plan_enrollment_data_transmission_to_clarity")]
        public string? CobraPlanEnrollmentDataTransmissionToClarity { get; set; }
        [Column("cobra_plan_estimated_date_enrollment_data_delivery")]
        public string? CobraPlanEstimatedDateEnrollmentDataDelivery { get; set; }
        [Column("cobra_plan_type")]
        public string? CobraPlanType { get; set; }
        [Column("cobra_plan_employee_class")]
        public string? CobraPlanEmployeeClass { get; set; }
        [Column("cobra_plan_first_payroll_date")]
        public DateTime? CobraPlanFirstPayrollDate { get; set; }
        [Column("cobra_plan_waiting_period")]
        public int? CobraPlanWaitingPeriod { get; set; }
        [Column("cobra_plan_parking_transit_waiting_period")]
        public int? CobraPlanParkingTransitWaitingPeriod { get; set; }
        [Column("cobra_plan_qualified_dependents")]
        public int? CobraPlanQualifiedDependents { get; set; }
        [Column("cons_ben_has_rol_plan")]
        
        public bool? ConsBenHasRolPlan { get; set; } = false; // Assuming an int meant to represent a boolean, 0 = false
        [Column("cons_ben_number_of_benefit_eligible_employees")]
        public int? ConsBenNumberOfBenefitEligibleEmployees { get; set; }
        [Column("cons_ben_number_of_participating_employees")]
        public int? ConsBenNumberOfParticipatingEmployees { get; set; }
        [Column("cons_ben_plan_enrollment_template")]
        public string? ConsBenPlanEnrollmentTemplate { get; set; }
        [Column("cons_ben_plan_enrollment_template_tpa_name")]
        public string? ConsBenPlanEnrollmentTemplateTpaName { get; set; }
        [Column("cons_ben_plan_payroll_group")]
        public string? ConsBenPlanPayrollGroup { get; set; }
        [Column("cons_ben_funding_method")]
        public string? ConsBenFundingMethod { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Column("created_by")]
        public string? CreatedBy { get; set; } = "CURRENT_USER";
        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        [Column("updated_by")]
        public string? UpdatedBy { get; set; } = "CURRENT_USER";
        [Column("portal_case_edit_url")]
        public string? PortalCaseEditUrl { get; set; }
        [Column("sf_new_task_id")]
        public string? SfNewTaskId { get; set; }
        [Column("is_used_for_testing")]
        
        public bool? IsUsedForTesting { get; set; } = false; // Assuming an int meant to represent a boolean, 0 = false
        [Column("form_initial_summary")]
        public string? FormInitialSummary { get; set; }
        [Column("form_user_sumbission")]
        public string? FormUserSubmission { get; set; }
        [Column("form_final_summary")]
        public string? FormFinalSummary { get; set; }
        [Column("last_updated_from")]
        public string? LastUpdatedFrom { get; set; }
        [Column("status_last_cancelled_at")]
        public DateTime? StatusLastCancelledAt { get; set; }
        [Column("last_rollout_error")]
        public string? LastRolloutError { get; set; }
        [Column("payroll_provider")]
        public string? PayrollProvider { get; set; }
        [Column("form_initial_json")]
        public string? FormInitialJson { get; set; }
        [Column("form_user_sumbission_json")]
        public string? FormUserSubmissionJson { get; set; }
        [Column("form_final_json")]
        public string? FormFinalJson { get; set; }
        [Column("member_email")]
        public string? MemberEmail { get; set; }
        [Column("member_first_name")]
        public string? MemberFirstName { get; set; }
        [Column("member_last_name")]
        public string? MemberLastName { get; set; }
        [Column("sf_new_task_id_2")]
        public string? SfNewTaskId2 { get; set; }
        [Column("sf_new_task_id_3")]
        public string? SfNewTaskId3 { get; set; }
        [Column("sf_new_task_id_4")]
        public string? SfNewTaskId4 { get; set; }
        [Column("sf_new_task_id_5")]
        public string? SfNewTaskId5 { get; set; }
        [Column("form_pin")]
        public string? FormPin { get; set; } = "X";
        [Column("sales_rep_name")]
        public string? SalesRepName { get; set; }
        [Column("client_annual_revenue")]
        public int? ClientAnnualRevenue { get; set; }
        [Column("crm_user_id")]
        public string? CrmUserId { get; set; }
        [Column("case_owner_name")]
        public string? Case_Owner_Name { get; set; }
        [Column("case_owner_email")]
        public string? Case_Owner_Email { get; set; }
        [Column("funding_submission_status")]
        public string? Funding_Submission_Status { get; set; }
        [Column("case_implementation_contact_email")]
        public string? Case_Implementation_Contact_Email { get; set; }
        [Column("case_implementation_contact_first_name")]
        public string? Case_Implementation_Contact_First_Name { get; set; }
        [Column("case_implementation_contact_last_name")]
        public string? Case_Implementation_Contact_Last_Name { get; set; }
    }
}

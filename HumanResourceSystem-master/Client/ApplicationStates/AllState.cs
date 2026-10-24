namespace Client.ApplicationStates
{
    public class AllState
    {
        // Scope action
        public Action? Action { get; set; }
        public Action? GeneralDepartmentAction { get; set; }
        public Action? DepartmentAction { get; set; }
        public Action? BranchAction { get; set; }
        public Action? CountryAction { get; set; }
        public Action? CityAction { get; set; }
        public Action? TownAction { get; set; }
        public Action? UserAction { get; set; }
        public Action? HealthAction { get; set; }
        public Action? OvertimeAction { get; set; }
        public Action? OvertimeTypeAction { get; set; }
        public Action? SanctionAction { get; set; }
        public Action? SanctionTypeAction { get; set; }
        public Action? VacationAction { get; set; }
        public Action? VacationTypeAction { get; set; }
        public Action? RecruitmentAction { get; set; }
        public Action? CandidateAction { get; set; }
        public Action? EmployeeAction { get; set; }
        public Action? BonusAction { get; set; }
        public Action? InstructorAction { get; set; }
        public Action? TrainingProgramAction { get; set; }
        public Action? PeriodicEvaluationAction { get; set; }
        public Action? PeerEvaluationAction { get; set; }
        public Action? StatisticalAction { get; set; }
        public Action? ProfileAction { get; set; }
        public Action? WorkProfileAction { get; set; }
        public Action? EmployeeTransferAction { get; set; }

        // General Department
        public bool ShowGeneralDepartment { get; set; }
        public void GeneralDepartmentClicked()
        {
            ResetAllDepartments();
            ShowGeneralDepartment = true;
            GeneralDepartmentAction?.Invoke();
            Action?.Invoke();
        }

        // Department
        public bool ShowDepartment { get; set; }
        public void DepartmentClicked()
        {
            ResetAllDepartments();
            ShowDepartment = true;
            DepartmentAction?.Invoke();
            Action?.Invoke();
        }

        // Branch
        public bool ShowBranch { get; set; }
        public void BranchClicked()
        {
            ResetAllDepartments();
            ShowBranch = true;
            BranchAction?.Invoke();
            Action?.Invoke();
        }

        // Country
        public bool ShowCountry { get; set; }
        public void CountryClicked()
        {
            ResetAllDepartments();
            ShowCountry = true;
            CountryAction?.Invoke();
            Action?.Invoke();
        }

        // City
        public bool ShowCity { get; set; }
        public void CityClicked()
        {
            ResetAllDepartments();
            ShowCity = true;
            CityAction?.Invoke();
            Action?.Invoke();
        }

        // Town
        public bool ShowTown { get; set; }
        public void TownClicked()
        {
            ResetAllDepartments();
            ShowTown = true;
            TownAction?.Invoke();
            Action?.Invoke();
        }

        // User
        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            ResetAllDepartments();
            ShowUser = true;
            UserAction?.Invoke();
            Action?.Invoke();
        }

        // Doctor
        public bool ShowHealth { get; set; }
        public void HealthClicked()
        {
            ResetAllDepartments();
            ShowHealth = true;
            HealthAction?.Invoke();
            Action?.Invoke();
        }

        // Overtime
        public bool ShowOvertime { get; set; }
        public void OvertimeClicked()
        {
            ResetAllDepartments();
            ShowOvertime = true;
            OvertimeAction?.Invoke();
            Action?.Invoke();
        }

        // Overtime Type
        public bool ShowOvertimeType { get; set; }
        public void OvertimeTypeClicked()
        {
            ResetAllDepartments();
            ShowOvertimeType = true;
            OvertimeTypeAction?.Invoke();
            Action?.Invoke();
        }

        // Sanction
        public bool ShowSanction { get; set; }
        public void SanctionClicked()
        {
            ResetAllDepartments();
            ShowSanction = true;
            SanctionAction?.Invoke();
            Action?.Invoke();
        }

        // Sanction Type
        public bool ShowSanctionType { get; set; }
        public void SanctionTypeClicked()
        {
            ResetAllDepartments();
            ShowSanctionType = true;
            SanctionTypeAction?.Invoke();
            Action?.Invoke();
        }

        // Vacation
        public bool ShowVacation { get; set; }
        public void VacationClicked()
        {
            ResetAllDepartments();
            ShowVacation = true;
            VacationAction?.Invoke();
            Action?.Invoke();
        }

        // Vacation Type
        public bool ShowVacationType { get; set; }
        public void VacationTypeClicked()
        {
            ResetAllDepartments();
            ShowVacationType = true;
            VacationTypeAction?.Invoke();
            Action?.Invoke();
        }

        // Recruitment
        public bool ShowRecruitment { get; set; }
        public void RecruitmentClicked()
        {
            ResetAllDepartments();
            ShowRecruitment = true;
            RecruitmentAction?.Invoke();
            Action?.Invoke();
        }

        // Candidate
        public bool ShowCandidate { get; set; }
        public void CandidateClicked()
        {
            ResetAllDepartments();
            ShowCandidate = true;
            CandidateAction?.Invoke();
            Action?.Invoke();
        }

        // Instructor
        public bool ShowInstructor { get; set; }
        public void InstructorClicked()
        {
            ResetAllDepartments();
            ShowInstructor = true;
            InstructorAction?.Invoke();
            Action?.Invoke();
        }

        // TrainingProgram
        public bool ShowTrainingProgram { get; set; }
        public void TrainingProgramClicked()
        {
            ResetAllDepartments();
            ShowTrainingProgram = true;
            TrainingProgramAction?.Invoke();
            Action?.Invoke();
        }

        // Periodic Evaluation
        public bool ShowPeriodicEvaluation { get; set; }
        public void PeriodicEvaluationClicked()
        {
            ResetAllDepartments();
            ShowPeriodicEvaluation = true;
            Action?.Invoke();
        }

        // Peer Evaluation
        public bool ShowPeerEvaluation { get; set; }
        public void PeerEvaluationClicked()
        {
            ResetAllDepartments();
            ShowPeerEvaluation = true;
            PeriodicEvaluationAction?.Invoke();
            Action?.Invoke();
        }

        // Statistical
        public bool ShowStatistical { get; set; }
        public void StatisticalClicked()
        {
            ResetAllDepartments();
            ShowStatistical = true;
            StatisticalAction?.Invoke();
            Action?.Invoke();
        }

        // Employee
        public bool ShowEmployee { get; set; }
        public void EmployeeClicked()
        {
            ResetAllDepartments();
            ShowEmployee = true;
            EmployeeAction?.Invoke();
            Action?.Invoke();
        }

        // Bonus
        public bool ShowBonus { get; set; }
        public void BonusClicked()
        {
            ResetAllDepartments();
            ShowBonus = true;
            BonusAction?.Invoke();
            Action?.Invoke();
        }

        // Profile
        public bool ShowProfile { get; set; } = true;
        public void ProfileClicked()
        {
            ResetAllDepartments();
            ShowProfile = true;
            ProfileAction?.Invoke();
            Action?.Invoke();
        }

        // Profile
        public bool ShowWorkProfile { get; set; }
        public void WorkProfileClicked()
        {
            ResetAllDepartments();
            ShowWorkProfile = true;
            WorkProfileAction?.Invoke();
            Action?.Invoke();
        }

        // EmployeeTransfer
        public bool ShowEmployeeTransfer { get; set; }
        public void EmployeeTransferClicked()
        {
            ResetAllDepartments();
            ShowEmployeeTransfer = true;
            EmployeeTransferAction?.Invoke();
            Action?.Invoke();
        }

        private void ResetAllDepartments()
        {
            ShowGeneralDepartment = false;
            ShowDepartment = false;
            ShowBranch = false;
            ShowCountry = false;
            ShowCity = false;
            ShowTown = false;
            ShowUser = false;
            ShowHealth = false;
            ShowOvertime = false;
            ShowOvertimeType = false;
            ShowSanction = false;
            ShowSanctionType = false;
            ShowVacation = false;
            ShowVacationType = false;
            ShowRecruitment = false;
            ShowCandidate = false;
            ShowEmployee = false;
            ShowBonus = false;
            ShowInstructor = false;
            ShowTrainingProgram = false;
            ShowPeriodicEvaluation = false;
            ShowPeerEvaluation = false;
            ShowStatistical = false;
            ShowProfile = false;
            ShowWorkProfile = false;
            ShowEmployeeTransfer = false;
        }
    }
}

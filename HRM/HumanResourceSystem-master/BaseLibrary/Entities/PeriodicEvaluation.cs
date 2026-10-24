
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibrary.Entities
{
    public class PeriodicEvaluation : BaseEntity
    {
        public int AssessorId { get; set; }
        [Required]
        public DateTime EvaluationDate { get; set; }

        [Required]
        public int TechnicalSkillsScore { get; set; } // Điểm kỹ năng kỹ thuật

        [Required]
        public int CommunicationSkillsScore { get; set; } // Điểm kỹ năng giao tiếp

        [Required]
        public int TeamworkSkillsScore { get; set; } // Điểm kỹ năng làm việc nhóm

        [Required]
        public int ProblemSolvingSkillsScore { get; set; } // Điểm kỹ năng giải quyết vấn đề

        [Required]
        public int WorkEthicScore { get; set; } // Điểm thái độ làm việc

        public string? Comments { get; set; }

        // Many to one relationship with Employee
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }

    }
}

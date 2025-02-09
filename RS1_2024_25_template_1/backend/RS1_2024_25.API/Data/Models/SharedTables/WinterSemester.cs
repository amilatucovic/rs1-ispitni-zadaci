using Microsoft.AspNetCore.Routing.Constraints;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul1_Auth;
using RS1_2024_25.API.Data.Models.TenantSpecificTables.Modul2_Basic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models.SharedTables
{
    public class WinterSemester
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(StudentId))]
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        [ForeignKey(nameof(AcademicYearId))]
        public int AcademicYearId { get; set; }
        public AcademicYear? AcademicYear { get; set; }
        [ForeignKey(nameof(RecordedById))]
        public int RecordedById { get; set; }
        public MyAppUser? RecordedBy { get; set; }

        public DateTime Date { get; set; }
        public int YearOfStudy { get; set; }
        public float Tuition { get; set; }
        public bool Renewal { get; set; }
    }
}

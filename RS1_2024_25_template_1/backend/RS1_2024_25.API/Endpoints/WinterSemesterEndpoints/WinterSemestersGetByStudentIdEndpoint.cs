using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Helper.Api;

namespace RS1_2024_25.API.Endpoints.WinterSemesterEndpoints
{
    [Route("semesters")]
    [ApiController]
    public class WinterSemestersGetByStudentIdEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync.WithRequest<int>
        .WithActionResult<SemestersResponse[]>
    {
        [HttpGet("{id}")]
        public async override Task<ActionResult<SemestersResponse[]>> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var student=await db.StudentsAll.FindAsync(id,cancellationToken);
            if(student == null)
            {
                return BadRequest("Nema studenta!");
            }

            return await db.WinterSemesters.Where(x=>x.StudentId==id)
                                           .Select(x=>new SemestersResponse
                                           {
                                               Id = x.Id,
                                               StudentId = x.StudentId,
                                               RecordedBy=x.RecordedBy.FirstName.ToLower(),
                                               Date=x.Date.ToString("yyyy-MM-dd"),
                                               Renewal=x.Renewal,
                                               Tuition=x.Tuition,
                                               AcademicYear=x.AcademicYear.Description.Substring(x.AcademicYear.Description.LastIndexOf(' ')+1),
                                               YearOfStudy=x.YearOfStudy
                                           }).ToArrayAsync(cancellationToken);

            throw new NotImplementedException();
        }
    }

    public class SemestersResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string RecordedBy { get; set; }
        public string Date {  get; set; }
        public bool Renewal {  get; set; }
        public float Tuition { get; set; }
        public string AcademicYear { get; set; }
        public int YearOfStudy { get; set; }

    }
}

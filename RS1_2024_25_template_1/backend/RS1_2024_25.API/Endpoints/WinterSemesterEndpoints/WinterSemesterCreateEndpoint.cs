using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Data.Models.SharedTables;
using RS1_2024_25.API.Helper.Api;
using RS1_2024_25.API.Services;

namespace RS1_2024_25.API.Endpoints.WinterSemesterEndpoints
{
    [Route("semesters")]
    [ApiController]
    public class WinterSemesterCreateEndpoint(ApplicationDbContext db, IMyAuthService myAuthService) : MyEndpointBaseAsync.WithRequest<SemesterRequest>.WithActionResult<SemestersResponse>
    {
        [HttpPost]
        public async override Task<ActionResult<SemestersResponse>> HandleAsync(SemesterRequest request, CancellationToken cancellationToken = default)
        {
            MyAuthInfo myAuthInfo = myAuthService.GetAuthInfoFromRequest();
            if (!myAuthInfo.IsLoggedIn)
            {
                return Unauthorized();
            }

            var student=await db.StudentsAll.FindAsync(request.StudentId,cancellationToken);
            if (student == null)
            {
                return BadRequest("Nema studenta");
            }
            var academicYear=await db.AcademicYears.FindAsync(request.AcademicYearId,cancellationToken);
            if (academicYear == null)
            {
                return BadRequest("Nema akademske godine");
            }
            var recordedBy=await db.MyAppUsersAll.FindAsync(request.RecordedById,cancellationToken);
            if(recordedBy == null)
            {
                return BadRequest("Nema recorded by-a");
            }

            bool obnova=await db.WinterSemesters
                .Where(x=>x.StudentId==request.StudentId && x.YearOfStudy==request.YearOfStudy)
                .FirstOrDefaultAsync(cancellationToken)!=null;

            var result = new WinterSemester
            {
                StudentId = request.StudentId,
                Student=student,
                AcademicYearId = request.AcademicYearId,
                AcademicYear =academicYear,
                RecordedById = request.RecordedById,
                RecordedBy =recordedBy,
                Date=request.Date,
                YearOfStudy = request.YearOfStudy,
                Renewal=obnova,
                Tuition=obnova?400:1800
            };

            await db.WinterSemesters.AddAsync(result);
            await db.SaveChangesAsync(cancellationToken);

            return Ok(new SemestersResponse{
                Id=result.Id,
                StudentId=result.StudentId,
                RecordedBy=result.RecordedBy.FirstName.ToLower(),
                Date=result.Date.ToString("yyyy-MM-dd"),
                Renewal = result.Renewal,
                Tuition=result.Tuition,
                AcademicYear=result.AcademicYear.Description.Substring(result.AcademicYear.Description.LastIndexOf(' ')+1),
                YearOfStudy=result.YearOfStudy
            });
        }
    }

    public class SemesterRequest
    {
        public int StudentId { get; set; }
        public DateTime Date {  get; set; }
        public int YearOfStudy { get; set; }
        public int AcademicYearId { get; set; }
        public int RecordedById { get; set; }
    }
}

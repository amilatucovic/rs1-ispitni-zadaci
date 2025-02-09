using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RS1_2024_25.API.Data;
using RS1_2024_25.API.Helper.Api;

namespace RS1_2024_25.API.Endpoints.AcademicYearEndpoints
{
    [Route("academic-years")]
    [ApiController]
    public class AcademicYearGetAllEndpoint(ApplicationDbContext db) : MyEndpointBaseAsync.WithoutRequest.WithActionResult<AcademicYearResponse[]>
    {
        [HttpGet]
        public async override Task<ActionResult<AcademicYearResponse[]>> HandleAsync(CancellationToken cancellationToken = default)
        {
            return await db.AcademicYears.Select(x=>new AcademicYearResponse
            {
                Id=x.ID,
                Description=x.Description.Substring(x.Description.LastIndexOf(' ')+1)

            }).ToArrayAsync(cancellationToken);
        }
    }

    public class AcademicYearResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}

using FluentValidation;

namespace RS1_2024_25.API.Endpoints.WinterSemesterEndpoints
{
    public class WinterSemesterCreateValidator:AbstractValidator<SemesterRequest>
    {
        public WinterSemesterCreateValidator()
        {
            RuleFor(x => x.YearOfStudy)
                .NotEmpty().WithMessage("Godina studija je obavezna")
                .GreaterThan(0).WithMessage("Godina studija mora biti veca od 0")
                .LessThan(6).WithMessage("Godina studija mora biti manja od 6");
        }
    }
}

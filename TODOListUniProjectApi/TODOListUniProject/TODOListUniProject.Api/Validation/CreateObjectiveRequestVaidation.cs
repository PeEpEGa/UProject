using FluentValidation;
using TODOListUniProject.Contracts.Http;

namespace TODOListUniProject.Api.Validation;

public class CreateObjectiveRequestVaidation : AbstractValidator<CreateObjectiveRequest>
{
    public CreateObjectiveRequestVaidation()
    {
        RuleFor(x => x.Title).NotNull().Length(1,200);
    }
}
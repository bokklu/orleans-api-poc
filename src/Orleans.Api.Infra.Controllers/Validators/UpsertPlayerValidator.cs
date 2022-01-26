using FluentValidation;
using Orleans.Api.Models.Input;

namespace Orleans.Api.Infra.Controllers.Validators
{
    public class UpsertPlayerValidator : AbstractValidator<Player>
    {
        public UpsertPlayerValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Level).IsInEnum();
        }
    }
}

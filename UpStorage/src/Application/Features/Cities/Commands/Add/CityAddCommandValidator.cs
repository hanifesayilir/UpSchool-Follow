using Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Cities.Commands.Add
{
    public class CityAddCommandValidator: AbstractValidator<CityAddCommand>
    {
        private readonly IApplicationDbContext _applicationDContext;
        public CityAddCommandValidator(IApplicationDbContext applicationDbContext)
        {
            _applicationDContext = applicationDbContext;
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.CountryId).NotEmpty();

            RuleFor(x => x.CountryId)
                .MustAsync(DoesCountryExistAsync)
                .WithMessage("The selected country does not exist");

            RuleFor(x => x.Name)
                .MustAsync((command, name, cancellationToken) =>
                {
                    return _applicationDContext.Cities.AllAsync(x => x.Name.ToLower() == name.ToLower(), cancellationToken);
                });

          /* a sample for non synchronuous
            RuleFor(x => x.CountryIds)
                .Must(IsCountryIdsListValid)
                .WithMessage("Please select at least two countries.");*/

        }

        private Task<bool> DoesCountryExistAsync(int countryId, CancellationToken cancellationToken)
        {
            return _applicationDContext.Countries.AnyAsync(x => x.Id == countryId, cancellationToken);
           
        }

        /*private bool IsCountryIdsListValid(List<Guid> countryIds)
        {
            if (countryIds is null || !countryIds.Any() || countryIds.Count < 2) return false;

            return true;
        }*/
    }
}

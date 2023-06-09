﻿using Application.Common.Interfaces;
using Application.Common.Localizations;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.Cities.Commands.Add
{
    public class CityAddCommandHandler : IRequestHandler<CityAddCommand, Response<int>>
    {

        private readonly IApplicationDbContext _applicationDbContext;

        private readonly IStringLocalizer<CommonLocalizations> _localizer;

        public CityAddCommandHandler(IApplicationDbContext applicationDbContext, IStringLocalizer<CommonLocalizations> localizer)
        {
            _applicationDbContext = applicationDbContext;
            _localizer = localizer;
        }


        public async Task<Response<int>> Handle(CityAddCommand request, CancellationToken cancellationToken)
        {
          /* if(!await _applicationDbContext.Countries.AnyAsync(x => x.Id == request.CountryId, cancellationToken))
            {
                throw new ArgumentNullException(nameof(request.CountryId));
            }

            if (await _applicationDbContext.Cities.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower(), cancellationToken))
            {
                throw new ArgumentNullException(nameof(request.Name));
            }*/

           /* var commandValidator = new CityAddCommandValidator(_applicationDbContext);

            var result = await commandValidator.ValidateAsync(request, cancellationToken);

            if(!result.IsValid)
            {
                throw new NullReferenceException("Fluent validation works!");
            }*/

            var city = new City()
            {
                Name = request.Name,
                CountryId = request.CountryId,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                CreatedOn = DateTimeOffset.Now,
                CreatedByUserId = null,
                IsDeleted = false,
            };

            await _applicationDbContext.Cities.AddAsync(city, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<int>(_localizer[CommonLocalizationKeys.City.Added, city.Name], city.Id);
        }
    }
}

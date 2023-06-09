﻿using Domain.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Cities.Commands.Add
{
    public class CityAddCommand: IRequest<Response<int>>
    {

        public string Name { get; set; }
        [Required]
        public int CountryId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

       // public List<Guid> CountryIds { get; set; }
    }
}

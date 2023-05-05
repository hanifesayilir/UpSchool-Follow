﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Countries.Queries.GetAll
{
    public class CountriesGetAllDto
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public CountriesGetAllDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

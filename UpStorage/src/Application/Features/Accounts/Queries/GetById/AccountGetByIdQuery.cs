﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Accounts.Queries.GetById
{
    public class AccountGetByIdQuery : IRequest<AccountGetByIdDto>
    {
        public Guid Id { get; set; }
        public AccountGetByIdQuery(Guid id)
        {
            Id = id;    
        }
    }
}

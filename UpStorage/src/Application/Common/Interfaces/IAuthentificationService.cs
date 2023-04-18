﻿using Application.Common.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IAuthentificationService
    {
        Task<string> CreateUserAsync(CreateUserDto createUserDto,CancellationToken cancellationToken);

        Task<string> GenerateEmailActivationTokenAsync(string userId, CancellationToken cancellationToken);

        Task<bool> CheckIfUserExists(string email, CancellationToken cancellationToken);
    }
}

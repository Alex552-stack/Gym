using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Entities;
using API.Dtos;
using Microsoft.AspNetCore.Identity;

namespace API.Services;

public class AccountService(UserManager<AppUser> userManager)
{
    private readonly UserManager<AppUser> _userManager = userManager;
    public async Task<AppUser> GetCurrentUser(string? username)
    {
        if(username is null)
            return null;

        var user = await _userManager.FindByNameAsync(username);

        return user;
    }
}
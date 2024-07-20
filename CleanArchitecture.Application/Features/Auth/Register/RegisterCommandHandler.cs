using AutoMapper;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nlabs.Result;

namespace CleanArchitecture.Application.Features.Auth.Register;

internal sealed class RegisterCommandHandler
    (
        UserManager<AppUser> userManager,
        IMapper mapper
    ) : IRequestHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userIsExists = await userManager.Users.AnyAsync(p => p.UserName == request.UserName || p.Email == request.Email, cancellationToken);
        if (userIsExists)
        {
            return Result<string>.Failure("There is already a user with this information!");
        };

        var user = mapper.Map<AppUser>(request);
        user.EmailConfirmed = true;

        if (request.RePassword != request.Password)
        {
            return Result<string>.Failure("Parolalar eşleşmiyor!");
        }

        var identityResult = await userManager.CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
        {
            return Result<string>.Failure(identityResult.Errors.Select(s => s.Description).ToList());
        }



        return "Your registration was successful";
    }
}

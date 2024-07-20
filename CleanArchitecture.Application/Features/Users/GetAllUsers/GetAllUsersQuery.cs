using MediatR;

namespace CleanArchitecture.Application.Features.Users.GetAllUsers;
public sealed record GetAllUsersQuery : IRequest<List<UserResponse>>;

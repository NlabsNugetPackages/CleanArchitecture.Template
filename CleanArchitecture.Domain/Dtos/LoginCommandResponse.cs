namespace CleanArchitecture.Domain.Dtos;
public sealed record LoginCommandResponse(
    string Token,
    string RefreshToken,
    DateTime RefreshTokenExpires);
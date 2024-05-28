using FastFood.Domain.Entities;

namespace FastFood.Application.Services.Authentication;

public record AuthenticationResult(
    User user,
    string Token
);
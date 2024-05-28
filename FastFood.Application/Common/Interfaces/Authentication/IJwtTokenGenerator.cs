using FastFood.Domain.Entities;

namespace FastFood.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
using FastFood.Application.Common.Errors;
using FastFood.Application.Common.Interfaces.Authentication;
using FastFood.Application.Common.Interfaces.Persistence;
using FastFood.Domain.Entities;
using ErrorOr;
using FastFood.Domain.Common.Errors;

namespace FastFood.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if (_userRepository.GetByUserEmail(email) != null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if (_userRepository.GetByUserEmail(email) is not User user)
        {
            return Errors.User.DuplicateEmail;
        }
        if (user.Password != password)
        {
            return Errors.Authentication.InvaildCredential;
        }
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token
        );
    }
}
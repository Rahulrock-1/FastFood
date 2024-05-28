using FastFood.Application.Services.Authentication;
using FastFood.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using FastFood.Api.Controllers;

namespace FastFood.Api.Controller;

[Route("api/auth")]
//[ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> registerResult = _authenticationService.Register
        (request.FirstName,
         request.LastName,
         request.Email,
         request.Password);

        return registerResult.Match(
           authResult => Ok(MapAuthResult(authResult)),
           errors => Problem(errors));

    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
                        authResult.user.Id,
                        authResult.user.FirstName,
                        authResult.user.LastName,
                        authResult.user.Email,
                        authResult.Token);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> loginResult = _authenticationService.Login(request.Email, request.Password);
        return loginResult.Match(
           authResult => Ok(MapAuthResult(authResult)),
           errors => Problem(errors));
    }
}
namespace FastFood.Contracts.Authentication;

public record LoginRequest(
    string Email,
    string Password
);
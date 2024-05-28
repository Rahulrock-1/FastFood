using FastFood.Application.Common.Interfaces.Persistence;
using FastFood.Domain.Entities;

namespace FastFood.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly List<User> _user = new();

    public void Add(User user)
    {
        _user.Add(user);
    }

    public User? GetByUserEmail(string email)
    {
        return _user.SingleOrDefault(u => u.Email == email);
    }
}
using FastFood.Domain.Entities;
namespace FastFood.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetByUserEmail(string email);
    void Add(User user);
}

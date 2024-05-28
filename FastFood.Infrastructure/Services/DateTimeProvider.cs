using FastFood.Application.Common.Interfaces.Services;

namespace FastFood.Infrastructure.Services;

public class DateTimeProvider : IDateTieProviders
{
    public DateTime UtcNow => DateTime.UtcNow;
}
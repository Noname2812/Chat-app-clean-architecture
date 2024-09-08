using System.Data;

namespace ChatApp.Domain.Abstractions.Dappers
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}

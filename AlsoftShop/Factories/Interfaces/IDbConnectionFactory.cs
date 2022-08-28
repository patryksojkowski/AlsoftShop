using System.Data;

namespace AlsoftShop.Factories.Interfaces
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection();
    }
}

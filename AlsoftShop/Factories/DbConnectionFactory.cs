using AlsoftShop.Factories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlsoftShop.Factories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private string cs;

        public DbConnectionFactory()
        {
            // todo read cs from app.settings
            cs = "Server=.;Database=AlsoftShopDatabase;Trusted_Connection=true;TrustServerCertificate=True";
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(cs);
        }
    }
}

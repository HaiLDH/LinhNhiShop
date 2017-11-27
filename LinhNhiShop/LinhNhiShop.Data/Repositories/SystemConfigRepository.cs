using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Model.Models;

namespace LinhNhiShop.Data.Repositories
{
    public interface ISystemConfigRepository : IRepository<SystemConfig>
    {

    }


    public class SystemConfigRepository : RepositoryBase<SystemConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

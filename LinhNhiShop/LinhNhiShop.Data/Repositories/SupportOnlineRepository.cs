using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Model.Models;

namespace LinhNhiShop.Data.Repositories
{
    public interface ISupportOnlineRepository
    {

    }



    public class SupportOnlineRepository : RepositoryBase<SupportOnline>, ISupportOnlineRepository
    {
        public SupportOnlineRepository(DbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

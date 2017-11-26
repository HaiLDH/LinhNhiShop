using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Model.Models;

namespace LinhNhiShop.Data.Repositories
{
    public interface IPageRepository
    {

    }



    public class PageRepository : RepositoryBase<Page>, IPageRepository
    {
        public PageRepository(DbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

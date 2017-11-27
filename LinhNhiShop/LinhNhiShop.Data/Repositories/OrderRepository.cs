using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Model.Models;

namespace LinhNhiShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {

    }


    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

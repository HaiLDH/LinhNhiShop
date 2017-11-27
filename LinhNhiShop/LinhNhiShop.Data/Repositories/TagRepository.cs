using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Model.Models;

namespace LinhNhiShop.Data.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {

    }


    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

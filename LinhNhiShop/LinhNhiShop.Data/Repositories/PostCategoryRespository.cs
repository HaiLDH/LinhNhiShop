using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Model.Models;

namespace LinhNhiShop.Data.Repositories
{
    public interface IPostCategoryRespository : IRepository<PostCategory>
    {

    }
    public class PostCategoryRespository : RepositoryBase<PostCategory>, IPostCategoryRespository
    {
        public PostCategoryRespository(DbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}

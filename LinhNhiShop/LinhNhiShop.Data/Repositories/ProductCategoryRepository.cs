using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace LinhNhiShop.Data.Repositories
{
    //add new method no in RepositoryBase
    public interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }

    //inheritance from RepositoryBase
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(x => x.Alias == alias);
        }
    }
}

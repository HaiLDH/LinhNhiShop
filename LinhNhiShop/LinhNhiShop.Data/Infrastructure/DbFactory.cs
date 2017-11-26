namespace LinhNhiShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private LinhNhiShopDbContext dbContext;

        public LinhNhiShopDbContext Init()
        {
            return dbContext ?? (dbContext = new LinhNhiShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace LYC.Helpers
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
        IRepository<T> Repository<T>() where T : class;
    }

    public class UnitOfWork<U> : IUnitOfWork where U : class
    {
        #region Private
        private DbContext DbContext { get; set; }
        public IConfiguration Configuration { get; }

        private Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        #endregion

        public UnitOfWork(IConfiguration configuration, U context)
        {
            Configuration = configuration;
            DbContext = context as DbContext;
        }

        #region Public FUN/Properties
        public void Save()
        {
            DbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repo = new Repository<T>(DbContext);
            repositories.Add(typeof(T), repo);
            return repo;
        }
        #endregion 
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Models;
using Talabat.Core.Repositories;

namespace Talabat.Repository.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repo = new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(type, repo);
            }
                   // HashTable -> return object -> need Casting
            return _repositories[type] as IGenericRepository<TEntity>;

        }

        public async Task<int> Complete()
            => await _dbContext.SaveChangesAsync();


        public async ValueTask DisposeAsync()
            => await _dbContext.DisposeAsync();
        
    }
}

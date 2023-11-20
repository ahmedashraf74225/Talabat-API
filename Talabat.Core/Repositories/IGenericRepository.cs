using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Models;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // static
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);


        // dynamic
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);

        Task<int> GetCountWithSpecAsync(ISpecification<T> spec);


        Task Add(T entity);

        void Update(T entity);
        void Delete(T entity);


    }
}

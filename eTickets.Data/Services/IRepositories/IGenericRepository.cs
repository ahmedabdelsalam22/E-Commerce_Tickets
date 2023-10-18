using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Data.Services.IRepositories
{
    public interface IGenericRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAllAsync(bool tracked = true , string[] includes=null);
        Task<T> GetAsync(Expression<Func<T,bool>>? filter=null , bool tracked=true, string[] includes = null);
        Task Create(T entity);
        Task Delete(T entity);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FiscalFacil.Database
{
    public interface IDatabase<T> where T : class, new()
    {
        Task<List<T>> Get();
        Task<T> Get(int id);
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> Delete(T entity);
    }
}

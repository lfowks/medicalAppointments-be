using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEDAPP.Repository
{
    public interface IRepository
    {
        Task<List<T>> FindAll<T>() where T : class;
        Task<T> FindById<T>(int id) where T : class;
        List<T> FindByCondition<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class;
       
        T FindSingleByCondition<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class;


        Task CreateAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
    }
}

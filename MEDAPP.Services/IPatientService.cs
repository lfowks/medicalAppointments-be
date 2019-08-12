using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MEDAPP.Models;

namespace MEDAPP.Services
{
    public interface IPatientService
    {
        Task<List<Patient>> FindAll<T>();
        List<Patient> FindByCondition<T>(System.Linq.Expressions.Expression<Func<Patient, bool>> expression);


        Task<Patient> FindById<T>(int id);
        Task CreateAsync<T>(Patient entity);
        Task UpdateAsync<T>(Patient entity);
        Task DeleteAsync<T>(Patient entity);
    }
}

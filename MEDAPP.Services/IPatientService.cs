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
        Task<Patient> FindById<T>(long id);
        Task CreateAsync<T>(Patient entity);
        Task UpdateAsync<T>(Patient entity);
        Task DeleteAsync<T>(Patient entity);
    }
}

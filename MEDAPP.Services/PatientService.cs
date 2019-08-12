using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MEDAPP.Models;

namespace MEDAPP.Services
{
    public class PatientService : IPatientService
    {
        private readonly Repository.IRepository _myRepo;
      

        public PatientService(Repository.IRepository repo)
        {
            _myRepo = repo;
        }

        public Task CreateAsync<T>(Patient entity)
        {
            return _myRepo.CreateAsync(entity);
        }

        public Task UpdateAsync<T>(Patient entity)
        {
            return _myRepo.UpdateAsync(entity);
        }

        public Task DeleteAsync<T>(Patient entity)
        {
           return _myRepo.DeleteAsync(entity);
        }

        public Task<List<Patient>> FindAll<T>()
        {
            return _myRepo.FindAll<Patient>();
        }

        public Task<Patient> FindById<T>(int id)
        {
            return _myRepo.FindById<Patient>(id);
        }

        public List<Patient> FindByCondition<T>(Expression<Func<Patient, bool>> expression)
        {
            return _myRepo.FindByCondition(expression);
        }

    }
}

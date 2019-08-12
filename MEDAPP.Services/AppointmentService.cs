using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MEDAPP.Models;

namespace MEDAPP.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly Repository.IRepository _myRepo;


        public AppointmentService(Repository.IRepository repo)
        {
            _myRepo = repo;
        }

        public Task CreateAsync<T>(Appointment entity)
        {
            return _myRepo.CreateAsync(entity);
        }

        public Task UpdateAsync<T>(Appointment entity)
        {
            return _myRepo.UpdateAsync(entity);
        }

        public Task DeleteAsync<T>(Appointment entity)
        {
            return _myRepo.DeleteAsync(entity);
        }

        public Task<List<Appointment>> FindAll<T>()
        {
            return _myRepo.FindAll<Appointment>();
        }


        public Task<List<AppointmentCategory>> FindAllCategories<T>()
        {
            return _myRepo.FindAll<AppointmentCategory>();
        }

        public Task<Appointment> FindById<T>(int id)
        {
            return _myRepo.FindById<Appointment>(id);
        }


        public List<Appointment> FindByCondition<T>(Expression<Func<Appointment, bool>> expression)
        {
            return  _myRepo.FindByCondition(expression);
        }


        /// <summary>
        /// This method validates that a Patient only has 1 appointment in a day.
        /// </summary>
        /// <param name="entity">Appointment Object</param>
        /// <param name="patient">Patient Object</param>
        /// <returns>Result with Success(True) or Success(False) with Errors and Messages</returns>
        public  bool ValidateOneAppointmentPatient(Appointment entity, List<Appointment> listAppointmentsPatient)
        {
            List<Appointment> listAppointments = listAppointmentsPatient.FindAll(
                x =>
                    x.Date.DayOfYear == entity.Date.DayOfYear
            );

            if (listAppointments == null || listAppointments.Count == 0) return true;

            return false;
        }


        public bool ValidateCancelationDate(Appointment entity, DateTime dateCancelation)
        {
            if ((entity.Date - dateCancelation.Date).TotalDays >= 1) return true;

            return false;
        }

        public ResultEntity ResultBuilder(object entity, bool hasError, string succesMessage = "", string errorMessage = "")
        {
            return ResultEntity.ResultBuilder(entity, true, succesMessage, errorMessage);
        }
    }
}

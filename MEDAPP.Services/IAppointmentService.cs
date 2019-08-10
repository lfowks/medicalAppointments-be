using MEDAPP.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MEDAPP.Services
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> FindAll<T>();
        Task<Appointment> FindById<T>(int id);
        List<Appointment> FindByCondition<T>(System.Linq.Expressions.Expression<Func<Appointment, bool>> expression);

        Task CreateAsync<T>(Appointment entity);
        Task UpdateAsync<T>(Appointment entity);
        Task DeleteAsync<T>(Appointment entity);

        bool ValidateCancelationDate(Appointment entity, DateTime dateCancelation);
        bool ValidateOneAppointmentPatient(Appointment entity, Patient patient);

        ResultEntity ResultBuilder(object entity, bool hasError, string succesMessage = "", string errorMessage = "");


    }
}

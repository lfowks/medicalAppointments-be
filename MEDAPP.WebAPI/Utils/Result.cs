using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEDAPP.WebAPI.Utils
{
    public struct Error
    {
        public int CodError { get; set; }
        public string Message { get; set; }
    }

   

    public class Result
    {
        public const string ERROR = "An error has ocurred";
        public const string SUCCESS = "The process was successful";

        public int Status { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public object CurrentObject { get; set; }

        public ICollection<Error> Errors { get; set; }

        public static Result ResultBuilder(object entity, bool hasError, string succesMessage = "" , string errorMessage = "")
        {
            return new Result
            {
                CurrentObject = entity,
                Message = hasError ? (errorMessage.Equals("") ? ERROR: errorMessage) : (succesMessage.Equals("") ? SUCCESS: succesMessage),
                Status = hasError ? 404 : 200,
                Success = !hasError
                
            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MEDAPP.Models
{
    public struct Error
    {
        public int CodError { get; set; }
        public string Message { get; set; }
    }


    public class ResultEntity
    {
        public const string ERROR = "An error has ocurred";
        public const string SUCCESS = "The process was successful";
        
        public string Message { get; set; }
        public bool Success { get; set; }

        public object CurrentObject { get; set; }

        public ICollection<Error> Errors { get; set; }

        public static ResultEntity ResultBuilder(object entity, bool hasError, string succesMessage = "", string errorMessage = "")
        {
            return new ResultEntity
            {
                CurrentObject = entity,
                Message = hasError ? (errorMessage.Equals("") ? ERROR : errorMessage) : (succesMessage.Equals("") ? SUCCESS : succesMessage),
                Success = !hasError

            };
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahibinden.Business.Concrete.Services
{
    public class ResultWrapperService<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ResultWrapperService()
        {
            Errors = new List<string>();
        }
        public static ResultWrapperService<T> SuccessResult(T data, string message = null)
        {
            return new ResultWrapperService<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }
        public static ResultWrapperService<T> FailureResult(string message, List<string> errors = null)
        {
            return new ResultWrapperService<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
}


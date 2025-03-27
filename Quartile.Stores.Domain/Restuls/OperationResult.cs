using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quartile.Stores.Domain.Restuls
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public static OperationResult CreateSuccess(string message = "")
        {
            return new OperationResult { Success = true, Message = message };
        }

        public static OperationResult CreateFailure(string message = "")
        {
            return new OperationResult { Success = false, Message = message };
        }
    }

    public class OperationResult<T> : OperationResult
    {
        public T? Data { get; set; }

        public static OperationResult<T> CreateSuccess(T data, string message = "")
        {
            return new OperationResult<T> { Data = data, Success = true, Message = message };
        }

        public static new OperationResult<T> CreateFailure(string message = "")
        {
            return new OperationResult<T> { Success = false, Message = message };
        }
    }
}

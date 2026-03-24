namespace User_Registration_System.Shared.Respones
{
    public interface IRequestResponse
    {
        bool IsSuccess { get; }
    }


    public class RequestRespone<T> : IRequestResponse
    {
        public bool IsSuccess { get; private set; }
        public T Data { get; private set; } = default!;
        public int StatusCode { get; private set; }
        public string Message { get; private set; } = string.Empty;


        private RequestRespone(T data, string message, int statusCode, bool isSuccess)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
            IsSuccess = isSuccess;
        }

        private RequestRespone(string message, int statusCode, bool isSuccess)
        {
            Message = message;
            StatusCode = statusCode;
            IsSuccess = isSuccess;
        }

        public static RequestRespone<T> Success(T data, string message = "Success", int statusCode = 200)
        {
            return new RequestRespone<T>(data, message, statusCode, true);
        }

        public static RequestRespone<T> Failure(string message, int statusCode = 400)
        {
            return new RequestRespone<T>(message, statusCode, false);
        }

    }
}

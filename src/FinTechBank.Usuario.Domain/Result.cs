using System.Net;

namespace FinTechBank.Usuario.Domain
{
    //Esta clase se creó para manejar las peticiones y sus resultados, se crea clase generica.
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T Value { get; set; }
        public string Error { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public static Result<T> Success(T value) => new() { IsSuccess = true, StatusCode = (int)HttpStatusCode.OK, Value = value };
        public static Result<T> Failure(string error) => new() { IsSuccess = false, StatusCode = (int)HttpStatusCode.BadRequest, Error = error };
        public static Result<T> Exception(string error) => new() { IsSuccess = false, StatusCode = (int)HttpStatusCode.InternalServerError, Error = error };
        public static Result<T> UnAuthorized(string error) => new() { IsSuccess = false, StatusCode = (int)HttpStatusCode.Unauthorized };
    }
}
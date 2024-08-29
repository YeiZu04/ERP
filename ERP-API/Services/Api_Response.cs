namespace ERP_API.Services.Api_Response
{
    public enum ErrorCode
    {

        UserAlreadyExists,
        InvalidInput,
        NotFound,
        errorDataBase,
        registerError,
        GeneralError
    }
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public ErrorCode? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

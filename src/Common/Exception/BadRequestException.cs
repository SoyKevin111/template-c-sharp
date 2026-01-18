namespace templatebase.src.Common.Exception
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message) : base(message, StatusCodes.Status400BadRequest) { }
    }
}
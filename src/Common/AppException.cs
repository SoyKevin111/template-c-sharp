namespace templatebase.src.Common
{
    public abstract class AppException : System.Exception
    {
        public int StatusCode { get; }
        protected AppException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
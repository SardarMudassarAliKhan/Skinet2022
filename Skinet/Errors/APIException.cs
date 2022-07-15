namespace Skinet.Errors
{
    public class APIException : APIResponce
    {
        public APIException(int StatusCode, string message = null,string details=null) : base(StatusCode, message)
        {
            details = Details;
        }
        public string Details { get; set; }

    }
}

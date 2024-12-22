namespace Zeeyo.Service.Exceptions;

public class ZeeyoException : Exception
{
    public int statusCode;
    public ZeeyoException(int Code, string Message) : base(Message)
    {
        statusCode = Code;
    }
}

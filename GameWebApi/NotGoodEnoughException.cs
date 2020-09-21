using System;

public class NotGoodEnoughException : Exception
{
    public NotGoodEnoughException()
    {
    }

    public NotGoodEnoughException(string message)
        : base(message)
    {
    }

    public NotGoodEnoughException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
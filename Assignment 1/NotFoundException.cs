using System;
public class NotFoundException : Exception {
        public NotFoundException()
    {
        Console.WriteLine("Not Found");
    }

    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string message, Exception inner)
        : base(message, inner)
    {
        Console.WriteLine(message + inner);
    }
}
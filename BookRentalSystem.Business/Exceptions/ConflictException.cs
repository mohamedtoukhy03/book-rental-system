using System;

namespace BookRentalSystem.Business.Exceptions
{
    public class ConflictException(string message) : Exception(message)
    {
    }
}

using System;

namespace BookRentalSystem.Business.Exceptions
{
    public class UnauthorizedException(string message) : Exception(message)
    {
    }
}

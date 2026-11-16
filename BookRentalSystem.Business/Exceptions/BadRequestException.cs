using System;

namespace BookRentalSystem.Business.Exceptions
{
    public class BadRequestException(string message) : Exception(message)
    {
    }
}

using System;

namespace BookRentalSystem.Business.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}

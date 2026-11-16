using System;

namespace BookRentalSystem.Business.Exceptions
{
    public class ForbiddenException(string message) : Exception(message)
    {
    }
}

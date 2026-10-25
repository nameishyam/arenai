namespace Server.Application.Exceptions
{
    public class NotFoundException()
        : Exception($"user with the given details does not exist");
}
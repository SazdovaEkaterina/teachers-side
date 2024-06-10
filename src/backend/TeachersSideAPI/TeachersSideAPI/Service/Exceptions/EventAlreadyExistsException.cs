namespace TeachersSideAPI.Service.Exceptions;

public class EventAlreadyExistsException : Exception
{
    public EventAlreadyExistsException(string message)
        : base(message)
    {
    }
}
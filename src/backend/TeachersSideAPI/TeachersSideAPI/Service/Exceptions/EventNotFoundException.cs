namespace TeachersSideAPI.Service.Exceptions;
public class EventNotFoundException : Exception
{
    public EventNotFoundException(string message)
        : base(message)
    {
    }
}
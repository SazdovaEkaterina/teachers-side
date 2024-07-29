namespace TeachersSideAPI.Service.Exceptions;

public class ForumNotFoundException : Exception
{
    public ForumNotFoundException(string message)
        : base(message)
    {
    }
}
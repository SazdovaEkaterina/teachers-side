namespace TeachersSideAPI.Service.Exceptions;

public class BadFileExtensionException : Exception
{
    public BadFileExtensionException(string message)
        : base(message)
    {
    }
}
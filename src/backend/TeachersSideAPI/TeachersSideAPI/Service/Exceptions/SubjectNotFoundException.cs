namespace TeachersSideAPI.Service.Exceptions;
public class SubjectNotFoundException : Exception
{
    public SubjectNotFoundException(string message)
        : base(message)
    {
    }
}
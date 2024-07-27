using TeachersSideAPI.Domain.Models;

namespace TeachersSideAPI.Persistence.Repositories;

public interface IForumRepository
{
    Task<Forum?> FindByIdAsync(int id);
}
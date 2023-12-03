using TeachersSideAPI.Persistence.Repositories;

namespace TeachersSideAPI.Service.Implementation;

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _materialRepository;

    public MaterialService(IMaterialRepository materialRepository)
    {
        _materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TeachersSideAPI.Domain.DTO;
using TeachersSideAPI.Domain.Enums;
using TeachersSideAPI.Domain.Models;
using TeachersSideAPI.Persistence.Repositories;
using TeachersSideAPI.Service.Exceptions;

namespace TeachersSideAPI.Service.Implementation;

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _materialRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<Teacher> _userManager;
    private readonly ISubjectRepository _subjectRepository;

    public MaterialService(IMaterialRepository materialRepository,
        IMapper mapper,
        UserManager<Teacher> userManager,
        ISubjectRepository subjectRepository)
    {
        _materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
        _mapper = mapper;
        _userManager = userManager;
        _subjectRepository = subjectRepository ?? throw new ArgumentNullException(nameof(subjectRepository));
    }

    public async Task<IEnumerable<MaterialDto>> GetAllAsync()
    {
        IEnumerable<Material> events = await _materialRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<MaterialDto>>(events);
    }

    public async Task<MaterialDto?> GetAsync(int id)
    {
        var evt = await _materialRepository.GetAsync(id);
        
        return evt != null ? _mapper.Map<MaterialDto>(evt) : null;
    }

    public async Task<bool> SaveAsync(MaterialDto materialDto)
    {
        var material = _mapper.Map<Material>(materialDto);
        material.Creator = await _userManager.FindByEmailAsync(materialDto.Creator.Email)
                           ?? throw new UserNotFoundException($"User with email {material.Creator.Email} not found");
        material.Subject = await _subjectRepository.GetBySubjectNameAndCategoryAsync(materialDto.Subject.Name, materialDto.Subject.Category)
                           ?? throw new SubjectNotFoundException($"Subject {materialDto.Subject.Name} not found");
        material.DateCreated = DateTime.UtcNow;
        return await _materialRepository.SaveAsync(material);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var material = await _materialRepository.GetAsync(id);

        if (material == null)
            return false;
        
        return await _materialRepository.DeleteAsync(material);
    }

    public async Task<bool> EditAsync(int id, MaterialDto materialDto)
    {
        var material = await _materialRepository.GetAsync(id);

        if (material == null)
            return false;
        
        material.Creator = await _userManager.FindByEmailAsync(materialDto.Creator.Email)
                           ?? throw new UserNotFoundException($"User with email {material.Creator.Email} not found");
        material.Subject = await _subjectRepository.GetBySubjectNameAndCategoryAsync(materialDto.Subject.Name, materialDto.Subject.Category)
                           ?? throw new SubjectNotFoundException($"Subject {materialDto.Subject.Name}  not found");
        material.FilePath = materialDto.FilePath;
        material.FileTitle = materialDto.FileTitle;
        material.FileType = materialDto.FileType;

        return await _materialRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<MaterialDto>> GetAllBySubjectAndCategoryAsync(string subjectName, string category)
    {
        IEnumerable<Material> materials = await _materialRepository.GetAllBySubjectNameAndCategoryAsync(subjectName, Enum.Parse<Category>(category));
        return _mapper.Map<IEnumerable<MaterialDto>>(materials);
    }
}
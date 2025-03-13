using TaiLieuWebsiteBackend.DTOs;

namespace TaiLieuWebsiteBackend.Services.IServices
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseDto>> GetAllExercisesAsync();
        Task<ExerciseDto> GetExerciseByIdAsync(int id);
        Task<ExerciseDto> AddExerciseAsync(ExerciseDto exerciseDto);
        Task<ExerciseDto> UpdateExerciseAsync(ExerciseDto exerciseDto);
        Task DeleteExerciseAsync(int id);
    }
}

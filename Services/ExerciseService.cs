using AutoMapper;
using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Repositories.IRepositories;
using TaiLieuWebsiteBackend.Services.IServices;

namespace TaiLieuWebsiteBackend.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllExercisesAsync()
        {
            var exercises = await _exerciseRepository.GetAllExercisesAsync();
            return exercises.Select(e => new ExerciseDto
            {
                Id = e.exercise_id,
                difficulty = e.difficulty,
                Title = e.title,
                Description = e.description,
                category_id = e.category_id,
                UploadedBy = e.uploaded_by,
                CreatedAt = e.CreatedAt,
                UpdatedAt = e.UpdatedAt
            }).ToList();
        }
        public async Task<ExerciseDto> GetExerciseByIdAsync(int id)
        {
            var exercise = await _exerciseRepository.GetExerciseByIdAsync(id);
            if (exercise == null)
            {
                return null;
            }

            return new ExerciseDto
            {
                Id = exercise.exercise_id,
                difficulty = exercise.difficulty,
                Title = exercise.title,
                Description = exercise.description,
                category_id = exercise.category_id,
                UploadedBy = exercise.uploaded_by,
                CreatedAt = exercise.CreatedAt,
                UpdatedAt = exercise.UpdatedAt
            };
        }
        public async Task<ExerciseDto> AddExerciseAsync(ExerciseDto exerciseDto)
        {
            var exercise = new Exercise
            {
                difficulty = exerciseDto.difficulty,
                title = exerciseDto.Title,
                description = exerciseDto.Description,
                category_id = exerciseDto.category_id,
                uploaded_by = exerciseDto.UploadedBy,
                CreatedAt = exerciseDto.CreatedAt,
                UpdatedAt = exerciseDto.UpdatedAt
            };

            var addedExercise = await _exerciseRepository.AddExerciseAsync(exercise);
            exerciseDto.Id = addedExercise.exercise_id;
            return exerciseDto;
        }
        public async Task<ExerciseDto> UpdateExerciseAsync(ExerciseDto exerciseDto)
        {
            var exercise = await _exerciseRepository.GetExerciseByIdAsync(exerciseDto.Id);
            if (exercise == null)
            {
                return null;
            }

            exercise.difficulty = exerciseDto.difficulty;
            exercise.title = exerciseDto.Title;
            exercise.description = exerciseDto.Description;
            exercise.category_id = exercise.category_id;
            exercise.uploaded_by = exerciseDto.UploadedBy;
            exercise.CreatedAt = exerciseDto.CreatedAt;
            exercise.UpdatedAt = exerciseDto.UpdatedAt;

            await _exerciseRepository.UpdateExerciseAsync(exercise);
            return exerciseDto;
        }
        public async Task DeleteExerciseAsync(int id)
        {
            await _exerciseRepository.DeleteExerciseAsync(id);
        }
    }
}

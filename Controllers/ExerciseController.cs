using Microsoft.AspNetCore.Mvc;
using TaiLieuWebsiteBackend.DTOs;
using TaiLieuWebsiteBackend.Services.IServices;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = await _exerciseService.GetAllExercisesAsync();
            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseById(int id)
        {
            var exercise = await _exerciseService.GetExerciseByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise([FromBody] ExerciseDto exerciseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newExercise = await _exerciseService.AddExerciseAsync(exerciseDto);
            return CreatedAtAction(nameof(GetExerciseById), new { id = newExercise.Id }, newExercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] ExerciseDto exerciseDto)
        {
            if (id != exerciseDto.Id)
            {
                return BadRequest();
            }

            var updatedExercise = await _exerciseService.UpdateExerciseAsync(exerciseDto);
            if (updatedExercise == null)
            {
                return NotFound();
            }
            return Ok(updatedExercise);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            await _exerciseService.DeleteExerciseAsync(id);
            return NoContent();
        }
    }
}


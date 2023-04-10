using BuberBreakfast.Contracts;
using BuberBreakfast.Converters;
using BuberBreakfast.Services;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers
{
    [ApiController]
    [Route("/breakfasts")]
    public class BreakfastController: ControllerBase
    {
        private readonly IBreakfastService breakfastService;

        public BreakfastController(IBreakfastService breakfastService)
        {
            this.breakfastService = breakfastService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBreakfast(Breakfast request)
        {
            var breakfastEntity = BreakfastConverter.ToModel(request);

            var createdEntity = this.breakfastService.CreateBreakfast(breakfastEntity);

            var response = BreakfastConverter.ToContract(createdEntity);

            await Task.Delay(1000);

            return CreatedAtAction(nameof(GetBreakfast), new { id = breakfastEntity.Id }, response);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetBreakfast(Guid id)
        {
            var breakfastEntity = this.breakfastService.GetBreakfast(id);

            if (breakfastEntity == null)
                return NotFound();

            var breakfast = BreakfastConverter.ToContract(breakfastEntity);

            return Ok(breakfast);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpsertBreakfast(Guid id, Breakfast request)
        {
            var breakfastEntity = BreakfastConverter.ToModel(request);
                
            breakfastEntity = this.breakfastService.UpsertBreakfast(id, breakfastEntity, out var isCreation);

            var response = BreakfastConverter.ToContract(breakfastEntity);

            if (isCreation)
                return CreatedAtAction(nameof(GetBreakfast), new { id = response.Id }, response);

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteBreakfast(Guid id)
        {
            this.breakfastService.DeleteBreakfast(id);
            return NoContent();
        }
    }
}

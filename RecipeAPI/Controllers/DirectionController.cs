using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Dto;
using RecipeAPI.Interfaces;
using RecipeAPI.Models;
using RecipeAPI.Repository;

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController:Controller
    {
        private readonly IDirectionRepository _directionsRepository;
        private readonly IMapper _mapper;

        public DirectionController(IDirectionRepository directionsRepository, IMapper mapper)
        {
            _directionsRepository = directionsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Directions>))]
        public IActionResult GetDirections()
        {
            var directions = _mapper.Map<List<DirectionDto>>(_directionsRepository.GetDirections());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(directions);
        }

        [HttpGet("{directionid}")]
        [ProducesResponseType(200, Type = typeof(Directions))]
        [ProducesResponseType(400)]
        public IActionResult GetDirection(int directionid)
        {
            if (!_directionsRepository.HasDirections(directionid))
                return NotFound();

            var direction = _mapper.Map<DirectionDto>(_directionsRepository.GetDirection(directionid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(direction);
        }

        [HttpGet("recipes/{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Directions))]
        [ProducesResponseType(400)]
        public IActionResult GetDirectionsByRecipe(int recipeId)
        {

            var directions = _mapper.Map<List<DirectionDto>>(_directionsRepository.GetDirectionsByRecipe(recipeId));

            if (directions.Count == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(directions);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDirection([FromBody] DirectionDto directionCreate)
        {
            if (directionCreate == null)
                return BadRequest(ModelState);

            var direction = _directionsRepository.GetDirections()
                .Where(c => c.Instruction.Trim().ToUpper() == directionCreate.Instruction.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (direction != null)
            {
                ModelState.AddModelError("", "Direction already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var directionMap = _mapper.Map<Directions>(directionCreate);

            if (!_directionsRepository.CreateDirection(directionMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }

            return Ok("Successfully created");
        }

        [HttpPut("{directionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDirection(int directionId, [FromBody] DirectionDto updateDirection)
        {
            if (updateDirection == null)
                return BadRequest(ModelState);

            if (directionId != updateDirection.Id)
                return BadRequest(ModelState);

            if (!_directionsRepository.HasDirections(directionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeMap = _mapper.Map<Directions>(updateDirection);

            if (!_directionsRepository.UpdateDirection(recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }

            return Ok("Successfully update");
        }

        [HttpDelete("{directionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDirection(int directionId)
        {
            if (!_directionsRepository.HasDirections(directionId))
            {
                return NotFound();
            }

            var directionToDelete = _directionsRepository.GetDirection(directionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_directionsRepository.DeleteDirection(directionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting direction");
            }

            return NoContent();
        }
    }
}

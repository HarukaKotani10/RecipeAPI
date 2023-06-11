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

        [HttpPut("{directionid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDirection(int directionid, [FromBody] DirectionDto updateDirection)
        {
            if (updateDirection == null)
                return BadRequest(ModelState);

            if (directionid != updateDirection.Id)
                return BadRequest(ModelState);

            if (!_directionsRepository.HasDirections(directionid))
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
    }
}

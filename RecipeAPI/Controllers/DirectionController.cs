using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RecipeAPI.Dto;
using RecipeAPI.Interfaces;
using RecipeAPI.Models;

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController:Controller
    {
        private readonly IDirectionsRepository _directionsRepository;
        private readonly IMapper _mapper;

        public DirectionController(IDirectionsRepository directionsRepository, IMapper mapper)
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
    }
}

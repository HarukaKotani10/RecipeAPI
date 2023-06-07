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
    public class IngredientController : Controller
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public IngredientController(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ingredients>))]
        public IActionResult GetIngredients()
        {
            var ingredients = _mapper.Map<List<IngredientDto>>(_ingredientRepository.GetIngredients());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ingredients);
        }

        [HttpGet("{ingredientid}")]
        [ProducesResponseType(200, Type = typeof(Ingredients))]
        [ProducesResponseType(400)]
        public IActionResult GetIngredient(int ingredientid)
        {
            if (!_ingredientRepository.HasIngredient(ingredientid))
                return NotFound();

            var ingredient = _mapper.Map<IngredientDto>(_ingredientRepository.GetIngredient(ingredientid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ingredient);
        }

    }
}

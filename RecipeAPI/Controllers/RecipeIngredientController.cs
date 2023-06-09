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
    public class RecipeIngredientController : Controller
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly IMapper _mapper;
        public RecipeIngredientController(IRecipeIngredientRepository recipeIngredientRepository, IMapper mapper)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<RecipeIngredient>))]
        public IActionResult GetRecipeIngredients()
        {
            var recipeIngredients = _mapper.Map<List<RecipeIngredientDto>>(_recipeIngredientRepository.GetRecipeIngredients());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipeIngredients);
        }

        [HttpGet("{recipeid}, {ingredientid}")]
        [ProducesResponseType(200, Type = typeof(RecipeIngredient))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeIngredient(int recipeid, int ingredientid)
        {
            if (!_recipeIngredientRepository.HasRecipeIngredient(recipeid, ingredientid))
                return NotFound();

            var recipeIngredient = _mapper.Map<RecipeIngredient>(_recipeIngredientRepository.GetRecipeIngredient(recipeid, ingredientid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipeIngredient);
        }

    }
}

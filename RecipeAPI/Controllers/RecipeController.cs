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
    public class RecipeController : Controller
    {

        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        public RecipeController(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipes>))]
        public IActionResult GetRecipes()
        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetRecipes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipes);
        }

        [HttpGet("{recipeid}")]
        [ProducesResponseType(200, Type = typeof(Recipes))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int recipeid)
        {
            if (!_recipeRepository.HasRecipe(recipeid))
                return NotFound();

            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(recipeid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipe);
        }
    }
}

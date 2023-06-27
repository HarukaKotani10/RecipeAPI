using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeAPI.Dto;
using RecipeAPI.Interfaces;
using RecipeAPI.Models;
using RecipeAPI.Repository;
using System.Diagnostics.Metrics;

namespace RecipeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {

        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        public RecipeController(IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
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


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipe([FromQuery] List<int> ingIds, [FromQuery] List<string> amounts, [FromQuery] RecipeDto recipeCreate)
        {
            if (recipeCreate == null)
                return BadRequest(ModelState);

            var existingRecipe = _recipeRepository.GetRecipes()
                .FirstOrDefault(c => c.Name.Trim().ToUpper() == recipeCreate.Name.TrimEnd().ToUpper());

            if (existingRecipe != null)
            {
                ModelState.AddModelError("", "Recipe already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeMap = _mapper.Map<Recipes>(recipeCreate);

            if (ingIds.Count != amounts.Count)
            {
                ModelState.AddModelError("", "The number of ingredient IDs does not match the number of amounts");
                return BadRequest(ModelState);
            }

            for (int i = 0; i < ingIds.Count; i++)
            {
                int ingId = ingIds[i];
                string amount = amounts[i];

                var ingredient = _ingredientRepository.GetIngredient(ingId);
                if (ingredient == null)
                {
                    ModelState.AddModelError("", $"Ingredient with ID {ingId} does not exist");
                    return StatusCode(404, ModelState);
                }

                var recipeIngredient = new RecipeIngredient
                {
                    Recipe = recipeMap,
                    Ingredient = ingredient,
                    Amount = amount
                };

                recipeMap.RecipeIngredients.Add(recipeIngredient);
            }

            if (!_recipeRepository.CreateRecipe(recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{recipeid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRecipe(int recipeid, [FromBody] RecipeDto updateRecipe)
        {
            if (updateRecipe == null)
                return BadRequest(ModelState);

            if (recipeid != updateRecipe.Id)
                return BadRequest(ModelState);

            if (!_recipeRepository.HasRecipe(recipeid))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeMap = _mapper.Map<Recipes>(updateRecipe);

            if (!_recipeRepository.UpdateRecipe(recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }

            return Ok("Successfully update");
        }

        [HttpDelete("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRecipe(int recipeId)
        {
            if (!_recipeRepository.HasRecipe(recipeId))
            {
                return NotFound();
            }

            var recipeToDelete = _recipeRepository.GetRecipe(recipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_recipeRepository.DeleteRecipe(recipeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting direction");
            }

            return NoContent();
        }
    }
}

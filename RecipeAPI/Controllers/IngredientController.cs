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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateIngredient([FromBody] IngredientDto ingredientCreate)
        {
            if (ingredientCreate == null)
                return BadRequest(ModelState);

            var ingredient = _ingredientRepository.GetIngredients()
                .Where(c => c.Name.Trim().ToUpper() == ingredientCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (ingredient != null)
            {
                ModelState.AddModelError("", "Ingredient already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredientMap = _mapper.Map<Ingredients>(ingredientCreate);

            if (!_ingredientRepository.CreateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }

            return Ok("Successfully created");
        }

        [HttpPut("{ingredientId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int ingredientId, [FromBody] IngredientDto updateIngredient)
        {
            if (updateIngredient == null)
                return BadRequest(ModelState);

            if (ingredientId != updateIngredient.Id)
                return BadRequest(ModelState);

            if (!_ingredientRepository.HasIngredient(ingredientId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ingredientMap = _mapper.Map<Ingredients>(updateIngredient);

            if (!_ingredientRepository.UpdateIngredient(ingredientMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
            }

            return Ok("Successfully update");
        }

        [HttpDelete("{ingredientId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteIngredient(int ingredientId)
        {
            if (!_ingredientRepository.HasIngredient(ingredientId))
            {
                return NotFound();
            }

            var ingredientToDelete = _ingredientRepository.GetIngredient(ingredientId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ingredientRepository.DeleteIngredientn(ingredientToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ingredient");
            }

            return NoContent();
        }

    }
}

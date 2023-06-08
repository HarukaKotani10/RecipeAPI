using AutoMapper;
using RecipeAPI.Dto;
using RecipeAPI.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Directions, DirectionDto>();
            CreateMap<Ingredients, IngredientDto>();
            CreateMap<Recipes, RecipeDto>();
        }
    }
}

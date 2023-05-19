using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RecipeAPI.Models;

namespace RecipeAPI.Data
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {

            if (!DbContextExtensions.TableExists(dataContext, "Ingredients"))
            {
                // Add ingredients
                var ingredients = new List<Ingredients>
                {
                    new Ingredients { Id = 1, Name = "Flour", Calories = 364, Carbohydrates = 76.3, Protein = 10.3, Fat = 1.2 },
                    new Ingredients { Id = 2, Name = "Sugar", Calories = 387, Carbohydrates = 99.5, Protein = 0, Fat = 0 },
                    new Ingredients { Id = 3, Name = "Eggs", Calories = 155, Carbohydrates = 1.1, Protein = 12.6, Fat = 10.6 },
                    new Ingredients { Id = 4, Name = "Butter", Calories = 717, Carbohydrates = 0.1, Protein = 0.9, Fat = 81.4 },
                    new Ingredients { Id = 5, Name = "Milk", Calories = 42, Carbohydrates = 5.2, Protein = 3.4, Fat = 1.0 },
                    new Ingredients { Id = 6, Name = "Blueberries", Calories = 57, Carbohydrates = 14.5, Protein = 0.7, Fat = 0.3 }
                };
                dataContext.Ingredients.AddRange(ingredients);
                dataContext.SaveChanges();

                if (!DbContextExtensions.TableExists(dataContext, "Ingredients"))
                {
                    // Add recipes
                    var recipes = new List<Recipes>
                    {
                    new Recipes
                    {
                        Name = "Chocolate Cake",
                        Description = "A rich and decadent chocolate cake.",
                        ImageUrl = "https://www.example.com/images/chocolate-cake.jpg",
                        RecipeIngredients = new List<RecipeIngredient>
                        {
                            new RecipeIngredient { IngredientId = 1, Amount = "2 cups" },
                            new RecipeIngredient { IngredientId = 2, Amount = "1 1/2 cups" },
                            new RecipeIngredient { IngredientId = 3, Amount = "3" },
                            new RecipeIngredient { IngredientId = 4, Amount = "1/2 cup" },
                            new RecipeIngredient { IngredientId = 5, Amount = "1 cup" }
                        },
                        Directions = new List<Directions>
                        {
                            new Directions { StepNumber = 1, Instruction = "Preheat oven to 350°F (175°C)." },
                            new Directions { StepNumber = 2, Instruction = "Mix flour and sugar together in a bowl." },
                            new Directions { StepNumber = 3, Instruction = "Beat in eggs one at a time." },
                            new Directions { StepNumber = 4, Instruction = "Melt butter in a small saucepan." },
                            new Directions { StepNumber = 5, Instruction = "Stir in milk and melted butter until smooth." },
                            new Directions { StepNumber = 6, Instruction = "Pour mixture into greased baking pan." },
                            new Directions { StepNumber = 7, Instruction = "Bake for 30-35 minutes." },
                            new Directions { StepNumber = 8, Instruction = "Let cool before serving." }
                        }
                    },
                    new Recipes
                    {
                        Name = "Blueberry Muffins",
                        Description = "Delicious muffins bursting with fresh blueberries.",
                        ImageUrl = "https://www.example.com/images/blueberry-muffins.jpg",
                        RecipeIngredients = new List<RecipeIngredient>
                        {
                            new RecipeIngredient { IngredientId = 1, Amount = "2 cups" },
                            new RecipeIngredient { IngredientId = 2, Amount = "1/2 cup" },
                            new RecipeIngredient { IngredientId = 3, Amount = "2" },
                            new RecipeIngredient { IngredientId = 4, Amount = "1/4 cup" },
                            new RecipeIngredient { IngredientId = 5, Amount = "1/2 cup" },
                            new RecipeIngredient { IngredientId = 6, Amount = "1 cup" }
                        },
                        Directions = new List<Directions>
                        {
                            new Directions { StepNumber = 1, Instruction = "Preheat oven to 375°F (190°C)." },
                            new Directions { StepNumber = 2, Instruction = "Mix flour, sugar, and baking powder together in a bowl." },
                            new Directions { StepNumber = 3, Instruction = "Beat in eggs." },
                            new Directions { StepNumber = 4, Instruction = "Stir in melted butter and milk until just combined." },
                            new Directions { StepNumber = 5, Instruction = "Fold in blueberries." },
                            new Directions { StepNumber = 6, Instruction = "Divide batter evenly into a greased muffin tin." },
                            new Directions { StepNumber = 7, Instruction = "Bake for 20-25 minutes or until a toothpick inserted in the center comes out clean." },
                            new Directions { StepNumber = 8, Instruction = "Let cool in the tin for 5 minutes before transferring to a wire rack to cool completely." }
                        }
                    }
                };

                    dataContext.Recipes.AddRange(recipes);
                    dataContext.SaveChanges();
                }
            }
        }
    }
}


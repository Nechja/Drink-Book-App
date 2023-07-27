using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKDataLoader
{
    public class IngredientTypeBuilder
    {
        public List <Ingredient> Ingredients = new List <Ingredient> ();
        public List <IngredientTypeDataModel> IngredientTypes = new List<IngredientTypeDataModel> ();

        public IngredientTypeBuilder(List<Ingredient> ingredients)
        {
            Ingredients = ingredients;
        }

        public void build()
        {
            foreach(Ingredient ingredient in Ingredients)
            {
                IngredientTypeDataModel Type = new IngredientTypeDataModel();
                if(IngredientTypes.FirstOrDefault(i => i.Name.ToLower() == ingredient.Type.ToLower()) == null)
                {
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    Type.Name = textInfo.ToTitleCase(ingredient.Type.ToLower());
                    IngredientTypes.Add (Type);
                }
            }
            foreach(var t in IngredientTypes)
            {
                Console.WriteLine(t.Name);
            }
        }

        public void load()
        {
            DrinkDBContext drinkDBContext = new DrinkDBContext();
            foreach (var t in IngredientTypes)
            {
                if(drinkDBContext.IngredientTypes.FirstOrDefault(i => i.Name.ToLower() == t.Name.ToLower()) == null)
                {
                    drinkDBContext.Add(t);
                    drinkDBContext.SaveChanges();
                }
            }

        }

    }
}

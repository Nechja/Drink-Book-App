using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKDataLoader
{
    public class IngredientBuilder
    {
        public List<Ingredient> Ingredients = new List<Ingredient>();
        public List<IngredientDataModel> IngredientData = new List<IngredientDataModel>();
        public IngredientBuilder(List<Ingredient> ingredients)
        {
            Ingredients = ingredients;


        }

        public void build()
        {
            
            foreach(Ingredient ingredient in Ingredients)
            {
                IngredientDataModel model = new IngredientDataModel();
                if (IngredientData.FirstOrDefault(i => i.Name.ToLower() == ingredient.Name.ToLower()) == null)
                {
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    IngredientTypeDataModel type = new IngredientTypeDataModel();

                    model.Name = textInfo.ToTitleCase(ingredient.Name);
                    type.Name = ingredient.Type;
                    model.IngredientType = type;
             
                    IngredientData.Add(model);
                }
            }


            foreach(var i in IngredientData)
            {
                Console.WriteLine(i.Name);
            }
        }

        public void load()
        {
            DrinkDBContext drinkDBContext = new DrinkDBContext();
            foreach (var t in IngredientData)
            {
                if (drinkDBContext.Ingredients.FirstOrDefault(i => i.Name.ToLower() == t.Name.ToLower()) == null)
                {
                    t.IngredientType = drinkDBContext.IngredientTypes.FirstOrDefault(i => i.Name.ToLower() == t.IngredientType.Name.ToLower())!;
                    drinkDBContext.Add(t);
                    drinkDBContext.SaveChanges();
                }
            }

        }


    }
}

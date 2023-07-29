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
                var ty = ingredient.Type.ToLower();
                ty = ty.Trim();
                switch (ty)
                {
                    case ".5":
                    case "":
                        ty = "borked";
                        break;
                    case "mixer/wine":
                    case "mixer wine":
                    
                    case "wine":

                        ty = "wine";
                        break;
                    case "aperitif wine":
                        ty = "Apéritif";
                        break;
                    case "mixxer":
                    case "cream":
                        ty = "mixer";
                        break;
                    case "liqueru":
                    case "liquer":
                        ty = "liqueur";
                        break;
                    case "spray":
                        ty = "absinthe";
                        break;
                    case "aquavit":
                        ty = "akvavit";
                        break;
                    case "powder mix":
                    case "sparkle dust":
                        ty = "powder";
                        break;
                    case "energy drink syrup":
                        ty = "syrup";
                        break;
                    case "whiskey (or moonshine)":
                        ty = "moonshine";
                        break;
                    case "energy drink":
                        ty = "mixer";
                        break;
                    case "ice":
                    case "sugar cube":
                    case "egg white":
                    case "candy":
                    case "puree mix":
                    case "vinegar":
                    case "food color":
                    case "vegetable":
                        ty = "bar supplies";
                        break;
                    case "herb":
                    case "herbs":
                    case "salt":
                    case "sugar":
                    case "seasoning":
                        ty = "spice";
                        break;
                    case "cyrup":
                        ty = "syrup";
                        break;
                    case "sours":
                        ty = "mixer";
                        break;
                    case "vermouth":
                        ty = "fortified wine";
                        break;
                    case "hard cider":
                        ty = "cider";
                        break;
                }
                if (IngredientTypes.FirstOrDefault(i => i.Name.ToLower() == ty.ToLower()) == null)
                {



                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    Type.Name = textInfo.ToTitleCase(ty.ToLower());
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


                if (drinkDBContext.IngredientTypes.FirstOrDefault(i => i.Name.ToLower() == t.Name.ToLower()) == null)
                {
                    drinkDBContext.Add(t);
                    drinkDBContext.SaveChanges();
                }
            }

        }

    }
}

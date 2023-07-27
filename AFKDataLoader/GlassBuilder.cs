using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace AFKDataLoader
{
    internal class GlassBuilder
    {
        List<Drink> drinks = new List<Drink>();
        List<GlassDataModel> models = new List<GlassDataModel>();

        public GlassBuilder(List<Drink> drinks)
        {
            this.drinks = drinks;
        }

        public void build()
        {
            foreach (Drink drink in drinks)
            {

                var glass = drink.Glassware.ToLower();
                int size; // = Int32.TryParse(Regex.Match(glass, @"\d+").Value);
                glass = Regex.Replace(glass, @"[\d-]", string.Empty);
                glass = glass.Replace("oz", string.Empty);

                glass = glass.Trim();

                if (String.IsNullOrEmpty(glass)) glass = "pounder";

                switch (glass)
                {
                    case "sealable container large enough to fit more than g of water":
                    case "sealable container large enough to fit more than ml":
                        glass = "Sealable Container";
                        break;
                    case "heat proof container":
                    case "heat proof sealable container":
                        glass = "Heat Proof Container";
                        break;
                    case "tall":
                    case "preferred size":
                    case "jar or pint glass":
                    case "pint glass":
                    case "dropshot":
                        glass = "pounder";
                        break;
                    case "collns":
                        glass = "collins";
                        break;
                    case "layered shot":
                        glass = "shot";
                        break;
                    case "bucket or lowball":
                    case "bucket or globe":
                    case "dirty drop shot, bucket":
                        glass = "bucket";
                        break;
                    case "globe or small whiskey glass":
                    case "stemless wine glass/globe":
                    case "wine glass or globe":
                    case "wine or globe":
                    case "globe or bucket":
                    case "small wine":
                        glass = "globe";
                        break;
                    case "hurricane/  glass":
                        glass = "hurricane";
                        break;
                    case "l glass":
                        glass = "One Liter Mug";
                        break;
                    case "small wineglass or martini":
                    case "small wine or martini":
                    case "martini":
                    case "coup or martini":
                        glass = "martini shell";
                        break;
                    case "large mason jar or pitcher":
                        glass = "large mason jar";
                        break;
                    case "rocks/whiskey glass":
                        glass = "rocks";
                        break;
                    case "large wine/goblet":
                        glass = "goblet";
                        break;
                    case "double shot or rocks":
                        glass = "rocks";
                        break;
                    case "jar":
                        glass = "mason jar";
                        break;



                    default:
                        break;
                }
                glass = glass.ToLower();
                glass = glass.Trim();

                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

                glass = textInfo.ToTitleCase(glass);


                if (models.FirstOrDefault(i => i.Name == glass) == null)
                {
                    GlassDataModel model = new GlassDataModel();
                    model.Name = glass;
                    models.Add(model);
                }



            }

            foreach (GlassDataModel model in models)
            {
                Console.WriteLine(model.Name);
            }
        }

        public void load()
        {
            DrinkDBContext drinkDBContext = new DrinkDBContext();
            foreach (var t in models)
            {
                if (drinkDBContext.Glasses.FirstOrDefault(i => i.Name.ToLower() == t.Name.ToLower()) == null)
                {
                    drinkDBContext.Add(t);
                    drinkDBContext.SaveChanges();
                }
            }

        }
    }
}


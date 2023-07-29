using DataAccess.Context;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKDataLoader
{
    internal class IceBuilder
    {
        List<Drink> drinks = new List<Drink>();
        List<IceDataModel> models = new List<IceDataModel>();

        public IceBuilder(List<Drink> drinks)
        {
            this.drinks = drinks;
        }

        public void build()
        {
            foreach (Drink drink in drinks)
            {

                var ice = drink.Ice.ToLower();
                ice = ice.Trim();
                switch (ice)
                {
                    case "yes":
                    case "yes ":
                    case "lots":
                    case string a when drink.Ice.Contains("yes"):
                        ice = "/w ice";
                        break;
                    case "light":
                    case "light ice":
                    case "little bit of ice":
                    case "half ice":
                        ice = "/w light ice";
                        break;

                    default:
                        break;
                }
                if (ice.Contains("no")) continue;
                if (ice.Contains("yes")) continue;
                if (ice.Contains(".5")) continue;
                if (ice.Contains("rim")) continue;
                if (ice.Contains("blender")) continue;
                if (string.IsNullOrEmpty(ice)) continue;

                if (models.FirstOrDefault(i => i.Value == ice) == null)
                {
                    IceDataModel model = new IceDataModel();
                    model.Value = ice;
                    models.Add(model);
                }



            }

            foreach (IceDataModel model in models)
            {
                Console.WriteLine(model.Value);
            }
        }

        public void load()
        {
            DrinkDBContext drinkDBContext = new DrinkDBContext();
            foreach (var t in models)
            {
                if (drinkDBContext.IceTypes.FirstOrDefault(i => i.Value.ToLower() == t.Value.ToLower()) == null)
                {
                    drinkDBContext.Add(t);
                    drinkDBContext.SaveChanges();
                }
            }

        }
    }
}


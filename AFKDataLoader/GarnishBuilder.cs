using DataAccess.Context;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKDataLoader
{
    internal class GarnishBuilder
    {
        List<Drink> drinks = new List<Drink>();
        List<GarnishDataModel> models = new List<GarnishDataModel>();

        public GarnishBuilder(List<Drink> drinks)
        {
            this.drinks = drinks;
        }

        public void build()
        {


            foreach (Drink drink in drinks)
            {
                if (!drink.Garnish.Contains("rim"))
                {
                    var garnish = drink.Garnish.ToLower();
                    if (models.FirstOrDefault(i => i.Value == garnish) == null)
                    {
                        GarnishDataModel model = new GarnishDataModel();
                        model.Value = garnish;
                        models.Add(model);
                    }
                }

            }

            foreach (GarnishDataModel model in models)
            {
                Console.WriteLine(model.Value);
            }
        }

        public void load()
        {
            DrinkDBContext drinkDBContext = new DrinkDBContext();
            foreach (var t in models)
            {
                if (drinkDBContext.GarnishTypes.FirstOrDefault(i => i.Value.ToLower() == t.Value.ToLower()) == null)
                {
                    drinkDBContext.Add(t);
                    drinkDBContext.SaveChanges();
                }
            }

        }
    }
}


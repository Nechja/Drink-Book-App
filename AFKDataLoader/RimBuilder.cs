using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AFKDataLoader
{
    public class RimBuilder
    {
        List<Drink> drinks = new List<Drink>();
        List<RimDataModel> models = new List<RimDataModel>();
        public List<GarnishDataModel> garnishes = new List<GarnishDataModel>();

        public RimBuilder(List<Drink> drinks)
        {
            this.drinks = drinks;
        }

        public void build()
        {
            foreach (Drink drink in drinks)
            {
                if (drink.Garnish.Contains("rim"))
                {

                    var rim = drink.Garnish.ToLower();
                    switch (rim)
                    {
                        case "salt/pepper rim and pickled jalapeno":
                            rim = "salt & pepper rim";
                            garnishes.Add(new GarnishDataModel() { Value = "pickled jalapeno" });
                            break;
                        case "smoked salt rim, lemon & lime wedge, pepperoncini & green olive skewer":
                            rim = "smoked salt rim";
                            garnishes.Add(new GarnishDataModel() { Value = "lemon wedge" });
                            garnishes.Add(new GarnishDataModel() { Value = "lime wedge" });
                            garnishes.Add(new GarnishDataModel() { Value = "pepperoncini" });
                            garnishes.Add(new GarnishDataModel() { Value = "green olive" });
                            break;
                        case "":
                            rim = "derpinteno";
                            break;

                    }


                    if (models.FirstOrDefault(i => i.Value == rim) == null)
                    {
                        RimDataModel model = new RimDataModel();
                        model.Value = rim;
                        models.Add(model);
                    }
                }
                
            }

            foreach (RimDataModel model in models)
            {
                Console.WriteLine(model.Value);
            }
        }

        public void load()
        {
            DrinkDBContext drinkDBContext = new DrinkDBContext();
            foreach (var t in models)
            {
                if (drinkDBContext.RimTypes.FirstOrDefault(i => i.Value.ToLower() == t.Value.ToLower()) == null)
                {
                    drinkDBContext.Add(t);
                    drinkDBContext.SaveChanges();
                }
            }

        }
    }
}

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

        public GarnishBuilder(List<Drink> drinks, List<GarnishDataModel> garnishes)
        {
            this.drinks = drinks;
            this.models = garnishes;
        }

        public void build()
        {


            foreach (Drink drink in drinks)
            {
                if (String.IsNullOrEmpty(drink.Name)) continue;


                if (!drink.Garnish.Contains("rim"))
                {
                    List<String> toadd = new List<String>();



                    var garnish = drink.Garnish.ToLower();
                    garnish = garnish.Trim();

                    switch (garnish)
                    {
                        case "orange peel, sugar cube":
                            garnish = "orange peel";
                            toadd.Add("sugar cube");
                            break;
                        case "2-4 gummy worms":
                            garnish = "gummy worms";
                            break;
                        case "zested lemon and lime":
                            garnish = "lemon zest";
                            toadd.Add("lime");
                            break;
                        case "orange peel & sage leaf":
                            garnish = "orange peel";
                            toadd.Add("sage leaf");
                            break;
                        case "whipped cream, cherry":
                            garnish = "whipped cream";
                            toadd.Add("cherry");
                            break;
                        case "two cherries, two sugar cubes":
                            garnish = "cherry";
                            toadd.Add("sugar cube");
                            break;
                        case "gummy worms, 3 cherries":
                            garnish = "gummy worms";
                            toadd.Add("cherry");
                            break;
                        case "green olive, lime wedge optional":
                            garnish = "green olive";
                            break;
                        case "lime and mint":
                            garnish = "lime";
                            toadd.Add("mint");
                            break;
                        case "sliced wheel of lemon & black olive":
                        case "wheel of lemon and slice of black olive":
                            garnish = "wheel of lemon";
                            toadd.Add("black olive");
                            break;
                        case "lemon wedge (2 cal)":
                        case "lemon wedge":
                            garnish = "lemon";
                            break;
                        case "lime wedge":
                            garnish = "lime";
                            break;
                        case "grapefruit wedge":
                            garnish = "grapefruite";
                            break;



                    }


                    if (models.FirstOrDefault(i => i.Value == garnish) == null)
                    {
                        GarnishDataModel model = new GarnishDataModel();
                        model.Value = garnish;
                        models.Add(model);
                    }
                    foreach(string a in toadd)
                    {
                        if (models.FirstOrDefault(i => i.Value == garnish) == null)
                        {
                            GarnishDataModel model = new GarnishDataModel();
                            model.Value = garnish;
                            models.Add(model);
                        }
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


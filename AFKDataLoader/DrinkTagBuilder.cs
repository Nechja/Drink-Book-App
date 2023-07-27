using DataAccess.Context;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFKDataLoader
{
    public class DrinkTagBuilder
    {
        public List<Drink> drinks = new List<Drink>();
        public List<DrinkTagDataModel> tagdata = new List<DrinkTagDataModel>();

        public DrinkTagBuilder(List<Drink> drinks)
        { 
            this.drinks = drinks;
        }

        public void build()
        {
            foreach(Drink drink in drinks)
            {
                if (drink.Tags == null) continue;
                List<String> tags = new List<String>();
                tags = drink.Tags.TagList;
                foreach(var tag in tags)
                {
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    var worktag = tag.Trim();
                    if (worktag.Length > 3) worktag = textInfo.ToTitleCase(worktag.ToLower());
                    else worktag = worktag.ToUpper();
                    worktag = worktag.Replace("Afk", "AFK");

                    if (tagdata.FirstOrDefault(i => i.Value.ToLower() == worktag.ToLower()) == null)
                    {
                        var t = new DrinkTagDataModel();
                        t.Value = worktag;
                        tagdata.Add(t);
                    }

                }
            }
            tagdata = tagdata.OrderBy(x => x.Value).ToList();
            foreach(var tag in tagdata)
            {
                Console.WriteLine(tag.Value);
            }
        }

        public void load()
        {
            DrinkDBContext drinkDBContext = new DrinkDBContext();
            foreach (var t in tagdata)
            {
                if (drinkDBContext.DrinkTags.FirstOrDefault(i => i.Value.ToLower() == t.Value.ToLower()) == null)
                {
                    t.TagType = drinkDBContext.DrinkTagTypes.FirstOrDefault(i => i.Id == 9)!;
                    drinkDBContext.Add(t);
                    drinkDBContext.SaveChanges();
                }
            }

        }

    }
}

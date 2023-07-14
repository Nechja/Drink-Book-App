using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Drink_Book_App.Data
{
    public class DashBoardDataTools
    {
        [Inject]
        public DrinkRepository repo { get; set; }



        public List<(int count,string name)>  IngredientTypesCounter = new List<(int count, string name)> ();

        public DashBoardDataTools() 
        {
            var ingredientTypes = repo.GetIngredientTypes();
            foreach(var t in ingredientTypes)
            {
                int count = 0;
                foreach(var i in t.Ingredients)
                {
                    count++;
                }
                IngredientTypesCounter.Add((count, t.Name));
            }
        }

    }
}

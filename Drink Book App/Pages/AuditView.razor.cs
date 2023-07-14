using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Pages
{
    public partial class AuditView
    {

        [Inject]
        public DrinkRepository repo { get; set; }

        private List<DrinkDataModel> drinks { get; set; } = new();



        private string searchString { get; set; }


        protected override void OnInitialized()
        {
            UpdateData();
        }

        protected private void UpdateData()
        {
            drinks.Clear();
            searchString = string.Empty;

            drinks = repo.GetDrinksAudit();
      

        }

        public void DeleteDrink(int id)
        {
            repo.DeleteDrink(id);
            UpdateData();
        }

        private void UndoDel(int id)
        {
            repo.UndoSoftDeleteDrink(id);
            UpdateData();
        }

        private Func<DrinkDataModel, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            if (x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        };
    }
}

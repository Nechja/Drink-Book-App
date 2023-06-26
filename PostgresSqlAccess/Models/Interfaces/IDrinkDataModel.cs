namespace DataAccess.Models.Interfaces
{
    public interface IDrinkDataModel
    {
        string? Garnish { get; set; }
        string? Ice { get; set; }
        int Id { get; set; }
        Uri? Image { get; set; }
        DrinkDataModel? Mod { get; set; }
        string Name { get; set; }
        string? Notes { get; set; }
    }
}
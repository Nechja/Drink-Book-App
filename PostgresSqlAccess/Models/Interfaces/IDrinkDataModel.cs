namespace DataAccess.Models.Interfaces
{
    public interface IDrinkDataModel
    {
        int Id { get; set; }
        Uri? Image { get; set; }

        string Name { get; set; }
        string? Notes { get; set; }
    }
}
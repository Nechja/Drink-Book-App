namespace DataAccess.Models.Interfaces
{
    public interface IGlassDataModel
    {
        int Id { get; set; }
        string Name { get; set; }
        float? Oz { get; set; }

        Uri? Image { get; set; }
    }
}
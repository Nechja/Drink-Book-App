namespace DataAccess.Models.Interfaces
{
    public interface IGlassDataModel
    {
        int Id { get; set; }
        int Name { get; set; }
        int? Oz { get; set; }

        Uri? Image { get; set; }
    }
}
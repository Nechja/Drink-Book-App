namespace DataAccess.Models.Interfaces
{
    public interface IIngredientDataModel
    {
        int Id { get; set; }
        IngredientTypeDataModel IngredientType { get; set; }
    }
}
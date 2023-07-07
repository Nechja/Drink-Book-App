namespace DataAccess.Models.Interfaces
{
    public interface IInstructionDataModel
    {
        int Id { get; set; }
        int? Oz { get; set; }
        string? Special { get; set; }
		public string? DropShopOptions { get; set; }
	}
}
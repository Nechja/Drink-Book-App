namespace DataAccess.Models.Interfaces
{
    public interface IInstructionDataModel
    {
        int Id { get; set; }
        float? Oz { get; set; }
        string? Special { get; set; }

		public int? DisplayWeight { get; set; }
	}
}
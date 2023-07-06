namespace DataAccess.Models.Interfaces
{
    public interface IFlagDataModel
    {
        int id { get; set; }
        string Name { get; set; }

		public string? OpeningStatement { get; set; }

		public string? ClosingStatment { get; set; }

		public string? InlineStatement { get; set; }
	}
}
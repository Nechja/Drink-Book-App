using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

namespace Drink_Book_App.Components.DrinkAddEdit.Tags
{
	public partial class Tagging
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		private string _tagtext { get; set; } = string.Empty;

		[CascadingParameter]
		public List<TagDisplayModel> Tags { get; set; } = new List<TagDisplayModel>();

		public List<TagDisplayModel> TagsAuto { get; set; } = new List<TagDisplayModel>();
		private List<TagDisplayModel> TagsToFilter = new List<TagDisplayModel>();

		[Parameter]
		public string TagType { get; set; }

		protected override void OnInitialized()
		{
			switch (TagType)
			{
				case $"{nameof(IngredientDisplayModel)}":
					var tagsdata = repo.GetIngredientTags();
					if (tagsdata.Count == 0) break;
					foreach (var tag in tagsdata)
					{
						TagsToFilter.Add(new TagDisplayModel(tag));
					}
					break;
				default:
					return;
			}
		}


		public void HandleChange(ChangeEventArgs e)
		{
			if (_tagtext.Length <= 2) return;
			if (TagsToFilter.Count == 0) return;
			TagsAuto.Clear();
			foreach (var tag in TagsToFilter)
			{
				if (tag.Value.ToLower().Contains(_tagtext.ToLower()))
				{
					TagsAuto.Add(new TagDisplayModel(tag));
				}
				
			}
			
			

		}
	}
}

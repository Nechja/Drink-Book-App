using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using MudBlazor;
using System.Security.Cryptography.Xml;
using System.Timers;

namespace Drink_Book_App.Components.DrinkAddEdit.Tags
{
	public partial class Tagging
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		public string? TagText 
		{ get 
			{
				return _tagText;
			} 
			set
			{ 
				_tagText = value;
				StateHasChanged();
			} 
		}

		private string? _tagText;

		private TagDisplayModel _tagdisplay { get; set; } = new TagDisplayModel();


		[CascadingParameter]
		public List<TagDisplayModel> Tags { get; set; } = new List<TagDisplayModel>();
		[Parameter]
		public EventCallback<List<TagDisplayModel>> OnSelectTag { get; set; }

		public List<TagDisplayModel> TagsAuto { get; set; } = new List<TagDisplayModel>();
		private List<string> _tags = new List<string>();

		[Parameter]
		public string TagType { get; set; }

		protected override void OnInitialized()
		{

			switch (TagType)
			{
				case $"{nameof(IngredientDisplayModel)}":
					var t = repo.GetIngredientTags();
					if (t.Count == 0) break;
					foreach (var tag in t)
					{
						TagsAuto.Add(new TagDisplayModel(tag));
						_tags.Add(tag.Value.ToLower());
					}
					break;
				default:
					return;
			}
		}

		private async Task EnterText(KeyboardEventArgs e) 
		{
			if (string.IsNullOrEmpty(TagText)) return;
			if(e.Code == "Enter" || e.Code == "NumpadEnter" || e.Code == "Comma") 
			{
				if(TagsAuto.FirstOrDefault(t => t.Value.ToLower() == TagText.ToLower()) is null)
				{
					TagsAuto.Add(new TagDisplayModel() { Value = TagText });
					Tags.Add(new TagDisplayModel() { Value = TagText });
					await OnSelectTag.InvokeAsync(Tags);
				}
				else
				{
					if(Tags.FirstOrDefault(t => t.Value.ToLower() == TagText.ToLower()) is null)
					{
						Tags.Add(TagsAuto.FirstOrDefault(t => t.Value.ToLower() == TagText.ToLower()));
						await OnSelectTag.InvokeAsync(Tags);
					}
					
				}
				TagText = null;
			}
		}

		protected private void Closed(MudChip chip)
		{
			if (chip is null) return;
			var toremove = Tags.FirstOrDefault(t => t.Value.ToLower() == chip.Text.ToLower());
			if (toremove != null) { Tags.Remove(toremove); }
			StateHasChanged();
		}

		private async Task<IEnumerable<string>> TagSearch(string value)
		{
			await Task.Delay(5);
			if (string.IsNullOrEmpty(value)) return new string[0];
			return _tags.Distinct().Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
		}

	}
}

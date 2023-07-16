using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;


namespace Drink_Book_App.Components.DrinkAddEdit.Tags
{
    public partial class TagEditDeleteTabel
    {
        [CascadingParameter]
        public List<TagDisplayModel> Types { get; set; } = new List<TagDisplayModel>();
        [Parameter]
        public EventCallback<TagDisplayModel> OnEdit { get; set; }

        [Parameter]
        public EventCallback<TagDisplayModel> OnDelete { get; set; }

		protected async Task EventCallbackOnEdit(TagDisplayModel m)
		{
			OnEdit.InvokeAsync(m);
		}

		protected async Task EventCallbackOnDelete(TagDisplayModel m)
		{
			OnDelete.InvokeAsync(m);
		}


	}
}

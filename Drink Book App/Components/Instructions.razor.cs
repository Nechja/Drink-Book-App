using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components
{
    public partial class Instructions
    {
        [Parameter]
        public List<InstructionDisplayModel> instructions { get; set; } = default!;

        protected override void OnInitialized()
        {
            var m = instructions;
        }
    }
}

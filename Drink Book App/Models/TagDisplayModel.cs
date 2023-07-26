using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Drink_Book_App.Models
{
    public class TagDisplayModel : DisplayDeleteProtection, ITagDataModel
    {
        public int Id { get; set; }
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
        public string Value { get; set; }

        public DrinkTagType TagType { get; set; } = new DrinkTagType();



        public TagDisplayModel(ITagDataModel tagDataModel)
        {
            if (tagDataModel == null) return;
            Id = tagDataModel.Id;
            Value = tagDataModel.Value;
            if(tagDataModel is DrinkTagDataModel)
            {
                var d = tagDataModel as DrinkTagDataModel;
                TagType = d.TagType;
            }
        }

        public TagDisplayModel() { }

        public IngredientTagDataModel IngredientTagDataModel 
        {
            get 
            { 
                return new IngredientTagDataModel() 
                { 
                    Id = Id,
                    Value = Value
                }; 
            }
        }

		public InstructionTagDataModel InstructionTagDataModel
		{
			get
			{
				return new InstructionTagDataModel()
				{
					Id = Id,
					Value = Value
				};
			}
		}

        public DrinkTagDataModel DrinkTagDataModel
        {
            get
            {

				return new DrinkTagDataModel()
				{
					Id = Id,
					Value = Value,
					TagType = TagType

				};


            }
        }

        public GarnishDataModel GarnishDataModel
        {
            get
            {
                return new GarnishDataModel()
                {
                    Id = Id,
                    Value = Value
                };
            }
        }

        public IceDataModel IceDataModel
        {
            get
            {
                return new IceDataModel()
                {
                    Id = Id,
                    Value = Value
                };
            }
        }

		public RimDataModel RimDataModel
		{
			get
			{
				return new RimDataModel()
				{
					Id = Id,
					Value = Value
				};
			}

		}

		public override string ToString()
		{
            if (String.IsNullOrEmpty(Value)) return String.Empty;
			return Value.ToString();
		}
	}
}

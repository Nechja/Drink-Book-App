using Drink_Book_App.Components.DrinkAddEdit.Instruction;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;

namespace Drink_Book_App.Components.DrinkCard
{
    public partial class Instructions
    {
        [Parameter]
        public List<InstructionDisplayModel> instructions { get; set; } = default!;
        List<(FlagDisplayModel decoration, List<InstructionDisplayModel> items)> FlagingItems { get; set; } = default!;
        public List<InstructionDisplayModel> NoFlagInstructions { get; set; } = new List<InstructionDisplayModel> { };
        protected override void OnInitialized()
        {
            if (instructions == null) return;
            var m = instructions;
            FlagingItems = ListFormater();
        }


        protected private List<(FlagDisplayModel decoration, List<InstructionDisplayModel> instructions)> ListFormater()
        {
            var OnlyFlags = instructions.ToList();
            foreach ( var instruction in instructions)
            {
                if(instruction.Flag.Name is null)
                {
                    OnlyFlags.Remove(instruction);
                    NoFlagInstructions.Add(instruction);
                }  
                    
                
            }
            List<List<InstructionDisplayModel>> FormatedFlags = FlagFormater(OnlyFlags, new List<List<InstructionDisplayModel>>());
            List<(FlagDisplayModel, List<InstructionDisplayModel>)> MudFormated = new List<(FlagDisplayModel, List<InstructionDisplayModel>)>();

            foreach (var list in FormatedFlags)
            {
                List<InstructionDisplayModel> FormatedOutput = new List<InstructionDisplayModel>();
                FlagDisplayModel Decor = new FlagDisplayModel();
                var flag = list[0].Flag;
                Decor = flag;
                foreach (var item in list)
                {
                    FormatedOutput.Add(item);
                }
                var tup = (Decor, FormatedOutput);
                MudFormated.Add(tup);
            }

            return MudFormated;
        }

        protected private List<List<InstructionDisplayModel>> FlagFormater(List<InstructionDisplayModel> flaginjest, List<List<InstructionDisplayModel>> flagsgoing)
        {
            if(flaginjest.Any())
            {
                List<InstructionDisplayModel> SortingList = new List<InstructionDisplayModel>();
                var flag = flaginjest[0].Flag; //flag we are working on
                var flagsworking = flaginjest.ToList();
                foreach ( var instruction in flagsworking)
                {
                    if(instruction.Flag == flag) 
                    {
                        SortingList.Add(instruction);
                        flaginjest.Remove(instruction);
                    }
                }
                flagsgoing.Add(SortingList);
                flagsgoing = FlagFormater(flaginjest, flagsgoing);
                return flagsgoing;
            }
            else
            {
                return flagsgoing;
            }
        }
    }
}

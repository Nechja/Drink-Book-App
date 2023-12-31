﻿using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace AFKDataLoader
{
    public class DrinkBuilder
    {
        List<Drink> drinks = new List<Drink>();
        List<DrinkDataModel> drinkDataModels = new List<DrinkDataModel>();
        public DrinkBuilder(List<Drink> drinks) 
        {
            this.drinks = drinks;
        }

        public void build()
        {
            DrinkDBContext context = new DrinkDBContext();
            List<FlagDataModel> flagModels = new List<FlagDataModel>();
            flagModels = context.Flags.ToList();
            List<Drink> nomods = new List<Drink>();
            List<Drink> mods = new List<Drink>();

            foreach (var drink in drinks)
            {
                if (string.IsNullOrEmpty(drink.Mod))
                {
                    nomods.Add(drink);
                }
                else
                {
                    mods.Add(drink);
                }
            }
            
            drinkDataModels = DrinkParse(nomods);

            var moddeddrinks = DrinkParse(mods);

            drinkDataModels.AddRange(moddeddrinks);

            foreach(var drink in drinkDataModels)
            {
                Console.WriteLine(drink.Name);
                string tags = string.Empty;
                foreach(var tag in drink.Tags)
                {
                    tags += tag.Value;
                    tags += " ";
                }

                foreach (var garnish in drink.Garnishes)
                {
                    Console.WriteLine(garnish.Value);
                }
                Console.WriteLine(tags);
                if(drink.Ice != null)
                {
                    Console.WriteLine(drink.Ice.Value);
                }
                if (drink.Rim != null)
                {
                    Console.WriteLine(drink.Rim.Value);
                }

                if (drink.Mod != null)
                {

                    Console.WriteLine($"----- MOD! {drink.Mod.Name}");
                }

                foreach (var i in drink.Instructions)
                {
                    if(i.Flag != null)
                    {
                        Console.WriteLine($"- {i.Oz}{i.Special} {i.Ingredient.Name}");
                        Console.WriteLine($"       Flag: {i.Flag.Name}");
                    }
                    else 
                    {
                        Console.WriteLine($"- {i.Oz}{i.Special} {i.Ingredient.Name}");
                    }
                    
                }
                Console.WriteLine("--------------------");
            }
        }
        public List<DrinkDataModel> DrinkParse(List<Drink> drinksin)
        {
            DrinkDBContext context = new DrinkDBContext();
            List < DrinkDataModel > drinkslist = new List<DrinkDataModel>();
            foreach (Drink drink in drinksin)
            {
                if (drinkDataModels.FirstOrDefault(d => d.Name.ToLower() == drink.Name.ToLower()) != null) continue;

                //basic info

                DrinkDataModel model = new DrinkDataModel();
                model.Name = drink.Name;
                try
                {
                    model.Image = new Uri(drink.Imgs);
                }
                catch(Exception e) { }
                
                if (!String.IsNullOrEmpty(drink.Link))
                {
                    try
                    {
                        model.Link = new Uri(drink.Link);
                    }
                    catch (Exception ex)
                    {
                        model.Notes += drink.Link;
                    }
                }

                //tag loader
                foreach (string tag in drink.Tags.TagList)
                {
                    var workingtag = tag.ToLower();
                    workingtag = workingtag.Trim();
                    model.Tags.Add(context.DrinkTags.Include(e => e.TagType).SingleOrDefault(e => e.Value.ToLower() == workingtag));

                }

                //ice logic
                if (!String.IsNullOrEmpty(drink.Ice))
                {
                    var ice = drink.Ice.ToLower();
                    ice = ice.Trim();
                    switch (ice)
                    {
                        case "yes":
                        case "yes ":
                        case "lots":
                        case string a when drink.Ice.Contains("yes"):
                            ice = "/w ice";
                            break;
                        case "light":
                        case "light ice":
                        case "little bit of ice":
                        case "half ice":
                            ice = "/w light ice";
                            break;

                        default:
                            break;
                    }
                    if (ice.Contains("no")) { }
                    else if (ice.Contains("yes")) { }
                    else if (ice.Contains(".5")) { }
                    else if (ice.Contains("rim")) { }
                    else if (string.IsNullOrEmpty(ice)) { }
                    else
                    {
                        var icemodel = context.IceTypes.SingleOrDefault(e => e.Value == ice);
                        if (icemodel != null)
                        {
                            model.Ice = icemodel;
                        }
                    }


                }
                //rim logic
                List<GarnishDataModel> garnishes = new List<GarnishDataModel>();
                if (!string.IsNullOrEmpty(drink.Garnish))
                {
                    if (drink.Garnish.Contains("rim"))
                    {

                        var rim = drink.Garnish.ToLower();
                        switch (rim)
                        {
                            case "salt/pepper rim and pickled jalapeno":
                                rim = "salt & pepper rim";
                                garnishes.Add(new GarnishDataModel() { Value = "pickled jalapeno" });
                                break;
                            case "smoked salt rim, lemon & lime wedge, pepperoncini & green olive skewer":
                                rim = "smoked salt rim";
                                garnishes.Add(new GarnishDataModel() { Value = "lemon wedge" });
                                garnishes.Add(new GarnishDataModel() { Value = "lime wedge" });
                                garnishes.Add(new GarnishDataModel() { Value = "pepperoncini" });
                                garnishes.Add(new GarnishDataModel() { Value = "green olive" });
                                break;
                            case "":
                                rim = "derpinteno";
                                break;

                        }



                        model.Rim = context.RimTypes.SingleOrDefault(e => e.Value.ToLower() == rim);

                    }
                }
                //garnish
                if (!string.IsNullOrEmpty(drink.Garnish))
                {

                    if (!drink.Garnish.Contains("rim"))
                    {
                        List<String> toadd = new List<String>();



                        var garnish = drink.Garnish.ToLower();
                        garnish = garnish.Trim();

                        switch (garnish)
                        {
                            


                            case "orange peel, sugar cube":
                                garnish = "orange peel";
                                toadd.Add("sugar cube");
                                break;
                            case "2-4 gummy worms":
                                garnish = "gummy worms";
                                break;
                            case "zested lemon and lime":
                                garnish = "lemon zest";
                                toadd.Add("lime");
                                break;
                            case "orange peel & sage leaf":
                                garnish = "orange peel";
                                toadd.Add("sage leaf");
                                break;
                            case "whipped cream, cherry":
                                garnish = "whipped cream";
                                toadd.Add("cherry");
                                break;
                            case "two cherries, two sugar cubes":
                                garnish = "cherry";
                                toadd.Add("sugar cube");
                                break;
                            case "gummy worms, 3 cherries":
                                garnish = "gummy worms";
                                toadd.Add("cherry");
                                break;
                            case "green olive, lime wedge optional":
                                garnish = "green olive";
                                break;
                            case "lime and mint":
                                garnish = "lime";
                                toadd.Add("mint");
                                break;
                            case "sliced wheel of lemon & black olive":
                            case "wheel of lemon and slice of black olive":
                                garnish = "wheel of lemon";
                                toadd.Add("black olive");
                                break;
                            case "lemon wedge (2 cal)":
                            case "lemon wedge":
                                garnish = "lemon";
                                break;
                            case "lime wedge":
                                garnish = "lime";
                                break;
                            case "grapefruit wedge":
                                garnish = "grapefruite";
                                break;



                        }

                        GarnishDataModel gAdd = new GarnishDataModel();
                        if (!String.IsNullOrEmpty(garnish)) gAdd = context.GarnishTypes.SingleOrDefault(e => e.Value.ToLower() == garnish);
                        if (gAdd != null) model.Garnishes.Add(gAdd);

                        if (!String.IsNullOrEmpty(garnish)) model.Garnishes.Add(context.GarnishTypes.SingleOrDefault(e => e.Value.ToLower() == garnish));

                        foreach(string s in toadd)
                        {
                            GarnishDataModel gadd = new GarnishDataModel();
                            if(!String.IsNullOrEmpty(s)) gadd = context.GarnishTypes.SingleOrDefault(e => e.Value.ToLower() == s);
                            if (gadd != null) model.Garnishes.Add(gadd);
                        }

                        foreach(var g in garnishes)
                        {
                            if(g != null) model.Garnishes.Add(context.GarnishTypes.SingleOrDefault(e => e.Value.ToLower() == g.Value));

                        }
                    }
                }

                //mod
                if (!string.IsNullOrEmpty(drink.Mod))
                {
                    var mod = drink.Mod.Trim();
                    mod = mod.ToLower();
                    model.Mod = drinkDataModels.FirstOrDefault(e => e.Name.ToLower() == mod);
                }

                if (!string.IsNullOrEmpty(drink.Glassware))
                {
                    var glass = drink.Glassware.ToLower();
                    int size; // = Int32.TryParse(Regex.Match(glass, @"\d+").Value);
                    glass = Regex.Replace(glass, @"[\d-]", string.Empty);
                    glass = glass.Replace("oz", string.Empty);

                    glass = glass.Trim();

                    if (String.IsNullOrEmpty(glass)) glass = "pounder";

                    switch (glass)
                    {
                        case "sealable container large enough to fit more than g of water":
                        case "sealable container large enough to fit more than ml":
                            glass = "Sealable Container";
                            break;
                        case "heat proof container":
                        case "heat proof sealable container":
                            glass = "Heat Proof Container";
                            break;
                        case "tall":
                        case "preferred size":
                        case "jar or pint glass":
                        case "pint glass":
                        case "dropshot":
                        case "drop shot":
                        case "glass":
                            glass = "pounder";
                            break;
                        case "collns":
                        case "rocks or collins":
                            glass = "collins";
                            break;
                        case "layered shot":
                            glass = "shot";
                            break;
                        case "bucket or lowball":
                        case "bucket or globe":
                        case "dirty drop shot, bucket":
                        case "globe/small wine/whiskey or bucket":
                        case "lowball":
                            glass = "bucket";
                            break;
                        case "globe or small whiskey glass":
                        case "stemless wine glass/globe":
                        case "wine glass or globe":
                        case "wine or globe":
                        case "globe or bucket":
                        case "small wine":
                        case "globe, stemless wine":
                            glass = "globe";
                            break;
                        case "hurricane/  glass":
                            glass = "hurricane";
                            break;
                        case "l glass":
                            glass = "One Liter Mug";
                            break;
                        case "small wineglass or martini":
                        case "small wine or martini":
                        case "martini":
                        case "coup or martini":
                        case "martini or globe":
                        case "globe or martini":
                        case "chilled martini":
                            glass = "martini shell";
                            break;
                        case "large mason jar or pitcher":
                            glass = "large mason jar";
                            break;
                        case "rocks/whiskey glass":
                            glass = "rocks";
                            break;
                        case "large wine/goblet":
                            glass = "goblet";
                            break;
                        case "double shot or rocks":
                        case "double shot or  rocks":
                            glass = "rocks";
                            break;
                        case "jar":
                            glass = "mason jar";
                            break;
                        case "large globe":
                        case "large wine":
                            glass = "large wine glass";
                            break;
                        case "stem wine or bourbon":
                            glass = "brandy snifter";
                            break;
                        default:
                            break;
                    }
                    glass = glass.ToLower();
                    glass = glass.Trim();
                    model.Glass = context.Glasses.FirstOrDefault(e => e.Name.ToLower() == glass);
                }
                else
                {
                    model.Glass = context.Glasses.FirstOrDefault(e => e.Name.ToLower() == "pounder");
                }

                //
                //start
                //the
                //instruction builder
                //
                foreach (var i in drink.Ingredients.IngredientList)
                {
                    InstructionDataModel instruction = new InstructionDataModel();
                    instruction.Ingredient = context.Ingredients.Include(e => e.IngredientType).SingleOrDefault(c => c.Name.ToLower() == i.Name.ToLower());
                    float oz = (float)double.Parse(i.Oz);
                    if (oz != 0)
                    {
                        instruction.Oz = oz;
                    }
                    if (!string.IsNullOrEmpty(i.Special))
                    {
                        instruction.Special = i.Special;
                    }
                    foreach (var t in i.Tags.TagList)
                    {
                        var tag = t.Trim();
                        tag = tag.ToLower();
                        switch (tag)
                        {
                            case "into glass":
                            case "glass":
                                tag = "in pounder";
                                break;
                            case "shot":
                                tag = "in shot";
                                break;
                            case "mix with spoon":
                            case "stried in":
                            case "stir":
                                tag = "mix";
                                break;
                            case "stir and strain":
                            case "stir untill chilled":
                                tag = "stir in shaker";
                                break;
                            case "shake and strain with ice":
                                tag = "shake and pour";
                                break;
                            case "shaken with ice":
                            case "shake & strain":
                            case "in shaker":
                                tag = "shake and strain";
                                break;

                        }

                        var flag = context.Flags.SingleOrDefault(e => e.Name.ToLower() == tag.ToLower());
                        if (flag != null)
                        {
                            instruction.Flag = flag;
                        }

                    }
                    model.Instructions.Add(instruction);
                    continue;
                    //end the builder
                }
                drinkslist.Add(model);
            }
            return drinkslist;
        }

        public void load()
        {
            DrinkDBContext context = new DrinkDBContext();
            foreach(DrinkDataModel drink in drinkDataModels)
            {
                if (context.Drinks.FirstOrDefault(i => i.Name == drink.Name) != null) continue;
                if(drink.Mod != null)
                {
                    drink.Mod = context.Drinks.FirstOrDefault(e => e.Name.ToLower() == drink.Mod.Name.ToLower());
                }
                try
                {
                    var glass = context.Glasses.FirstOrDefault(g => g.Id == drink.Glass.Id!);
                    drink.Glass = glass;
                }
                catch(Exception ex)
                {
                    var glass = context.Glasses.FirstOrDefault(g => g.Name.ToLower() == "pounder");
                    drink.Glass = glass;
                }
                

                //tags
                var drinktags = new List<DrinkTagDataModel>();
                foreach (var tag in drink.Tags)
                {
                    var existingtag = context.DrinkTags.Include(e => e.TagType).SingleOrDefault(i => i.Value == tag.Value);
                    if (existingtag != null)
                    {
                        drinktags.Add(existingtag);
                    }
                }
                drink.Tags.Clear();
                drink.Tags = drinktags;

                //ice
                if (drink.Ice != null)
                {
                    var ice = context.IceTypes.FirstOrDefault(i => i.Id == drink.Ice.Id!);
                    drink.Ice = ice;
                }

                //
                if (drink.Rim != null)
                {
                    var rim = context.RimTypes.FirstOrDefault(r => r.Id == drink.Rim.Id!);
                    drink.Rim = rim;
                }


                //Garnishes
                var garnishtags = new List<GarnishDataModel>();
                foreach (var tag in drink.Garnishes)
                {
                    var garnish = context.GarnishTypes.SingleOrDefault(i => i.Value == tag.Value);
                    if (garnish != null)
                    {
                        garnishtags.Add(garnish);
                    }
                }
                drink.Garnishes.Clear();
                drink.Garnishes = garnishtags;


                //unwinding and repacking instructions
                var ins = new List<InstructionDataModel>();
                foreach (InstructionDataModel instruction in drink.Instructions)
                {
                    if (instruction.Flag != null)
                    {
                        var flag = context.Flags.FirstOrDefault(f => f.id == instruction.Flag.id);
                        instruction.Flag = flag;
                    }
                    else { instruction.Flag = null; }

                    var ingredient = context.Ingredients.FirstOrDefault(i => i.Id == instruction.Ingredient.Id);
                    instruction.Ingredient = ingredient;

                    ins.Add(instruction);
                }
                drink.Instructions.Clear();
                drink.Instructions = ins;

                context.Drinks.Add(drink);
                context.SaveChanges();
            }
        }


    }
}

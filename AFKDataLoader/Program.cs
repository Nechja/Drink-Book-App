using AFKDataLoader;
using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("ArrayOfDrink")]
public class DrinkList
{
    [XmlElement("Drink")]
    public List<Drink> Drinks { get; set; }
}

public class Drink
{
    public string Name { get; set; }
    public string Mod { get; set; }
    public string Glassware { get; set; }
    public string Ice { get; set; }
    public string Garnish { get; set; }
    public string Link { get; set; }
    public string Imgs { get; set; }

    [XmlElement("Tags")]
    public Tags Tags { get; set; } = new Tags();

    [XmlElement("Ingredients")]
    public Ingredients Ingredients { get; set; }
}

public class Tags
{
    [XmlElement("string")]
    public List<string> TagList { get; set; } = new List<string>();
}

public class Ingredients
{
    [XmlElement("Ingredient")]
    public List<Ingredient> IngredientList { get; set; }
}

public class Ingredient
{
    public string Name { get; set; }
    public string Oz { get; set; }
    public string Special { get; set; }
    public string Type { get; set; }

    [XmlElement("Tags")]
    public Tags Tags { get; set; } = new Tags();
}




class Program
{

    static void Main()
    {


        string filePath = "merged.xml"; 

        // Read the XML content from the file
        string xmlData = File.ReadAllText(filePath);
        List<Drink> drinks = new List<Drink>();
        List<Ingredient> ingredients = new List<Ingredient>();

        XmlSerializer serializer = new XmlSerializer(typeof(DrinkList));
        using (TextReader reader = new StringReader(xmlData))
        {
            DrinkList drinkList = (DrinkList)serializer.Deserialize(reader);

            // Access the objects
            foreach (Drink drink in drinkList.Drinks)
            {
                //Console.WriteLine($"Drink Name: {drink.Name}");
                //Console.WriteLine($"Glassware: {drink.Glassware}");
                //Console.WriteLine($"Link: {drink.Link}");
                drinks.Add(drink);

                foreach (Ingredient ingredient in drink.Ingredients.IngredientList)
                {
                    ingredients.Add(ingredient);
                    //Console.WriteLine($"Ingredient Name: {ingredient.Name}");
                    //Console.WriteLine($"Oz: {ingredient.Oz}");
                    //Console.WriteLine($"Special: {ingredient.Special}");
                    //Console.WriteLine($"Type: {ingredient.Type}");
                    //Console.WriteLine();
                }

                //Console.WriteLine("----------");
            }
        }
        //var Types = new IngredientTypeBuilder(ingredients);
        //Load all the types in
        //Types.build();
        //Types.load(); //Load into database

        //Load all the Ingredients in
        //var ingredientBuilder = new IngredientBuilder(ingredients);
        //
        //
        //ingredientBuilder.build();

        //ingredientBuilder.load();

        //var rimbuilder = new RimBuilder(drinks);
        //rimbuilder.build();
        //rimbuilder.load();

        //var garnishbuilder = new GarnishBuilder(drinks);
        //garnishbuilder.build();
        //garnishbuilder.load();

        //var icebuilder = new IceBuilder(drinks);
        //icebuilder.build();
        //icebuilder.load();

        //var glassbuilder = new GlassBuilder(drinks);
        //glassbuilder.build();
        //glassbuilder.load();

        //var drinktagbuilder = new DrinkTagBuilder(drinks);
        //drinktagbuilder.build();
        //drinktagbuilder.load();

        var drinkbuilder = new DrinkBuilder(drinks);
        drinkbuilder.build();
        drinkbuilder.load();

    }
}
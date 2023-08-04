using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Tools
{
    public static class RandomName
    {
        static readonly List<string> _adjectives = new List<string> {
            "Happy", "Funny", "Clever", "Silly", "Brave", "Swift", "Kind", "Calm", "Gentle", "Wise",
            "Joyful", "Energetic", "Honest", "Loyal", "Creative", "Charming", "Generous", "Patient", "Sincere", "Adventurous",
            "Caring", "Cheerful", "Curious", "Grateful", "Humble", "Optimistic", "Playful", "Resourceful", "Thoughtful", "Vibrant",
            "Witty", "Ambitious", "Confident", "Enthusiastic", "Friendly", "Polite", "Reliable", "Supportive", "Talented", "Unique"
        };

        static readonly List<string> _nouns = new List<string> {
            "Unicorn", "Dragon", "Wizard", "Penguin", "Dolphin", "Tiger", "Lion", "Elephant", "Octopus", "Butterfly",
            "Star", "Moon", "Sun", "Cloud", "Mountain", "River", "Ocean", "Forest", "Meadow", "Desert",
            "Island", "Castle", "Book", "Music", "Painting", "Rainbow", "Diamond", "Galaxy", "Comet", "Telescope",
            "Ship", "Lantern", "Key", "Clock", "Treasure", "Crown", "Sword", "Shield", "Potion", "Crown"
        };

        static readonly List<string>  _colors = new List<string> {
            "AliceBlue", "AntiqueWhite", "Aqua", "Aquamarine", "Azure", "Beige", "Bisque", "Black", "BlanchedAlmond", "Blue",
            "BlueViolet", "Brown", "CadetBlue", "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Crimson",
            "Cyan", "DarkBlue", "DarkCyan", "DarkGoldenrod", "DarkGray", "DarkGreen", "DarkKhaki", "DarkMagenta", "DarkOliveGreen", "DarkOrange",
            "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSeaGreen", "DarkSlateBlue", "DarkSlateGray", "DarkTurquoise", "DarkViolet", "DeepPink", "DeepSkyBlue",
            "DimGray", "DodgerBlue", "FireBrick", "FloralWhite", "ForestGreen", "Fuchsia", "Gold", "Goldenrod",
            "Gray", "Green", "GreenYellow", "Honeydew", "HotPink", "Indigo", "Ivory", "Khaki", "Lavender",
             "LemonChiffon", "LightBlue", "LightCoral", "LightCyan", "LightGoldenrodYellow", "LightGray", "LightGreen", "LightPink",
            "LightSalmon", "LightSeaGreen", "LightSkyBlue", "LightSlateGray", "LightSteelBlue", "LightYellow", "Lime", "LimeGreen", "Linen", "Magenta",
            "Maroon", "MediumAquamarine", "MediumBlue", "MediumOrchid", "MediumPurple", "MediumSeaGreen", "MediumSlateBlue", "MediumSpringGreen", "MediumTurquoise", "MediumVioletRed",
            "MidnightBlue", "MintCream", "MistyRose", "Navy", "OldLace", "Olive", "OliveDrab", "Orange",
            "OrangeRed", "Orchid", "PaleGoldenrod", "PaleGreen", "PaleTurquoise", "PaleVioletRed", "PeachPuff", "Pink",
            "Plum", "PowderBlue", "Purple", "Red", "RosyBrown", "RoyalBlue", "SaddleBrown", "Salmon", "SandyBrown", "SeaGreen",
            "Seashell", "Sienna", "Silver", "SkyBlue", "SlateBlue", "SlateGray", "Snow", "SpringGreen", "SteelBlue",
            "Teal", "Thistle", "Tomato", "Transparent", "Turquoise", "Violet", "Wheat", "White", "WhiteSmoke", "Yellow",
            "YellowGreen"
        };

        public static string Name
        {
            get
            {
                Random rnd = new Random();
                return _colors[rnd.Next(_colors.Count())] + _adjectives[rnd.Next(_adjectives.Count())] + _nouns[rnd.Next(_nouns.Count())];
            }
        }

        public static string NameWithNumbers
        {
            get
            {
                Random rnd = new Random();
                return _colors[rnd.Next(_colors.Count())] + _adjectives[rnd.Next(_adjectives.Count())] + _nouns[rnd.Next(_nouns.Count())] + rnd.Next(400).ToString();
            }
        }
    }
}

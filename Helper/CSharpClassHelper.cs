using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SQLDataViewer.Helper.RichTextBoxHelper;

namespace SQLDataViewer.Helper
{
    public static class CSharpClassHelper
    {

        public static List<ColouredText> HighlightText(string[]? others = null)
        {
            var highlightText = new List<ColouredText>()
            {
                new ColouredText($"namespace", Color.Blue),
                new ColouredText($"using ", Color.Blue),
                new ColouredText($"public ", Color.Blue),
                new ColouredText($"partial ", Color.Blue),
                new ColouredText($"class ", Color.Blue),
                new ColouredText($"Key", Color.SkyBlue),
                new ColouredText($"Required", Color.SkyBlue),
                new ColouredText($"Column", Color.SkyBlue),
                new ColouredText($"StringLength", Color.SkyBlue),
                new ColouredText($"DatabaseGenerated", Color.SkyBlue),
                new ColouredText($"DatabaseGeneratedOption", Color.DarkSeaGreen),
                new ColouredText($"Identity", Color.SkyBlue),
                new ColouredText($" DateTime", Color.Green),
                new ColouredText($" int", Color.Blue),
                new ColouredText($" long", Color.Blue),
                new ColouredText($" string", Color.Blue),
                new ColouredText($" bool", Color.Blue)
            };

            if(others!= null)
            {
                foreach (var item in others)
                {
                    highlightText.Add(new ColouredText(item, Color.SkyBlue));
                }
            }

            return highlightText;
        }

    }
}

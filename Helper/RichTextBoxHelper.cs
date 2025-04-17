using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataViewer.Helper
{
    public static class RichTextBoxHelper
    {
        public static void ApplyTextHighlighting(RichTextBox richTextBox, List<ColouredText> highlights)
        {
            foreach (var highlight in highlights)
            {
                int startIndex = 0;
                while ((startIndex = richTextBox.Text.IndexOf(highlight.Text, startIndex, StringComparison.OrdinalIgnoreCase)) != -1)
                {
                    richTextBox.Select(startIndex, highlight.Text.Length);
                    if (highlight.Foreground != Color.Transparent)
                        richTextBox.SelectionColor = highlight.Foreground;
                    if (highlight.Background != Color.Transparent)
                        richTextBox.SelectionBackColor = highlight.Background;
                    startIndex += highlight.Text.Length;
                }
            }
            richTextBox.DeselectAll();
        }

        public class ColouredText
        {
            public string Text;
            public Color Foreground;
            public Color Background;

            public ColouredText(string text, Color foreground, Color background)
            {
                Text = text;
                Foreground = foreground;
                Background = background;
            }

            public ColouredText(string text, Color foreground) : this(text, foreground, Color.Transparent) { }

            public ColouredText(string text) : this(text, Color.Transparent, Color.Transparent) { }
        }

    }
}

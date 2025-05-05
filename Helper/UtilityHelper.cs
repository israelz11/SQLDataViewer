using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SQLDataViewer.Helper
{
    public static class UtilityHelper
    {
        public static string ToTitle(string Title)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            Title = textInfo.ToTitleCase(Title.ToLower());

            return Title.Replace(" ", "").Replace("-", "").Replace("_", "");
        }

        public static string ToNormalize(string Text)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(Text);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        public static string ToCamelCase(string Text)
        {
            if (string.IsNullOrEmpty(Text))
                return string.Empty;
            Text = Text.ToLower();
            Text = Text.Replace(" ", "");
            Text = Text.Replace("-", "");
            Text = Text.Replace("_", "");
            return char.ToLower(Text[0]) + Text.Substring(1);
        }

        public static string ToLowerCase(string Text)
        {
            if (string.IsNullOrEmpty(Text))
                return string.Empty;
            Text = Text.ToLower();
            Text = Text.Replace(" ", "");
            Text = Text.Replace("-", "");
            Text = Text.Replace("_", "");
            return Text;
        }

        public static string ToPascalCase(string Text)
        {
            if (string.IsNullOrEmpty(Text))
                return string.Empty;
            string originalText = Text.Replace(" ", "").Replace("-", "").Replace("_", "");
            Text = Text.ToLower();
            Text = Text.Replace(" ", "");
            Text = Text.Replace("-", "");
            Text = Text.Replace("_", "");
            return char.ToUpper(Text[0]) + originalText.Substring(1);
        }

    }
}

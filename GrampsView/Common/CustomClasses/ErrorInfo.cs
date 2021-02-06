using System.Collections.Generic;

namespace GrampsView.Common
{
    public class ErrorInfo : Dictionary<string, string>
    {
        public ErrorInfo(string argName, string argText)
        {
            Name = argName;
            Text = argText;
        }

        public ErrorInfo()
        {
        }

        public ErrorInfo(string argText)
        {
            Text = argText;
        }

        public string DialogBoxTitle
        {
            get; set;
        } = string.Empty;

        public string Name
        {
            get; set;
        }
                         = string.Empty;

        public string Text
        {
            get; set;
        }
                    = string.Empty;

        public override string ToString()
        {
            string outString = "Name: " + Name;

            outString = outString + ", Text: " + Text;

            foreach (var item in this)
            {
                outString = outString + ", " + item.Key + " = " + item.Value;
            }

            return outString;
        }
    }
}
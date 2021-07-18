using GrampsView.Data.Model;

namespace GrampsView.Common.CustomClasses
{
    public class ErrorInfo : CardListLineCollection
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

            foreach (CardListLine item in this)
            {
                outString = outString + ", " + item.Label + " = " + item.Value;
            }

            return outString;
        }
    }
}
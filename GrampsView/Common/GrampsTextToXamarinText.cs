namespace GrampsView.Common
{
    using GrampsView.Data.Model;

    using System.Collections.Generic;

    using Xamarin.Forms;

    public static class GrampsTextToXamarinText
    {
        public static List<Span> GetFormattedString(StyledTextModel argTextModel, double argFontSize)
        {
            List<Span> returnString = new List<Span>
            {
                // Add default font
                new Span { FontSize = argFontSize }
            };

            //Setup spans
            for (int i = 0; i < argTextModel.GText.Length; i++)
            {
                returnString.Add(new Span { Text = argTextModel.GText.Substring(i, 1) });
            }

            // Setup Styles
            foreach (GrampsStyle item in argTextModel.Styles)
            {
                foreach (GrampsStyleRangeModel item1 in item.GRange)
                {
                    for (int i = item1.Start; i < item1.End; i++)
                    {
                        switch (item.GStyle)
                        {
                            // TODO Handle multiple styles

                            case CommonEnums.TextStyle.bold:
                                returnString[i].FontAttributes = FontAttributes.Bold;
                                break;

                            case CommonEnums.TextStyle.fontcolor:
                                break;

                            case CommonEnums.TextStyle.fontface:
                                break;

                            case CommonEnums.TextStyle.fontsize:
                                break;

                            case CommonEnums.TextStyle.highlight:
                                break;

                            case CommonEnums.TextStyle.italic:
                                returnString[i].FontAttributes = FontAttributes.Italic;
                                break;

                            case CommonEnums.TextStyle.link:
                                break;

                            case CommonEnums.TextStyle.superscript:
                                break;

                            case CommonEnums.TextStyle.underline:
                                break;

                            case CommonEnums.TextStyle.unknown:
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            return returnString;
        }
    }
}
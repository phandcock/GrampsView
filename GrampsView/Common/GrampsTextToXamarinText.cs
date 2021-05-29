namespace GrampsView.Common
{
    using GrampsView.Data.Model;

    using Xamarin.Forms;

    public static class GrampsTextToXamarinText
    {
        public static FormattedString GetFormattedString(StyledTextModel argTextModel, double argFontSize)
        {
            FormattedString returnString = new FormattedString();

            // Add default font
            returnString.Spans.Add(new Span { FontSize = argFontSize });

            // Handle the normal case with no formatting
            if (argTextModel.Styles.Count == 0)
            {
                returnString.Spans.Add(new Span { Text = argTextModel.GText, FontSize = Common.CommonFontSize.FontSmall });

                return returnString;
            }

            //Setup spans
            for (int i = 0; i < argTextModel.GText.Length; i++)
            {
                returnString.Spans.Add(new Span { Text = argTextModel.GText.Substring(i, 1) });
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
                                returnString.Spans[i].FontAttributes = FontAttributes.Bold;
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
                                returnString.Spans[i].FontAttributes = FontAttributes.Italic;
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
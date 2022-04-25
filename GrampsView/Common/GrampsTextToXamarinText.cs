namespace GrampsView.Common
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;

    using System.Collections.Generic;

    using Xamarin.Forms;

    public static class GrampsTextToXamarinText
    {
        // TODO Cleanup up the code
        public static FormattedString GetFormattedString(StyledTextModel argTextModel, double argFontSize)
        {
            FormattedString returnString = new FormattedString();

            List<FormattedChar> workingString = new List<FormattedChar>();

            // Handle the normal case with no formatting
            if ((argTextModel.GText.Length == 0) || (argTextModel.Styles.Count == 0))
            {
                returnString.Spans.Add(new Span { Text = argTextModel.GText, FontSize = SharedSharp.CommonRoutines.FontSizes.FontSmall });

                return returnString;
            }

            // Setup working list
            for (int i = 0; i < argTextModel.GText.Length; i++)
            {
                workingString.Add(new FormattedChar { Character = argTextModel.GText[i] });
            }

            // Initialise Working String
            FormattedChar t;

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
                                {
                                    t = workingString[i];
                                    t.StyleBold = true;
                                    workingString[i] = t;
                                    break;
                                }

                            case CommonEnums.TextStyle.fontcolor:
                                break;

                            case CommonEnums.TextStyle.fontface:
                                t = workingString[i];

                                break;

                            case CommonEnums.TextStyle.fontsize:
                                break;

                            case CommonEnums.TextStyle.highlight:
                                break;

                            case CommonEnums.TextStyle.italic:
                                {
                                    t = workingString[i];
                                    t.StyleItalics = true;
                                    workingString[i] = t;
                                    break;
                                }

                            case CommonEnums.TextStyle.link:
                                break;

                            case CommonEnums.TextStyle.superscript:
                                {
                                    t = workingString[i];
                                    t.StyleSuperscript = true;
                                    workingString[i] = t;
                                    break;
                                }

                            case CommonEnums.TextStyle.underline:
                                {
                                    t = workingString[i];
                                    t.StyleUnderline = true;
                                    workingString[i] = t;
                                    break;
                                }

                            case CommonEnums.TextStyle.unknown:
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            // Setup Styles
            FormattedChar currentStyle;
            string outString = string.Empty;

            // Add default font
            returnString.Spans.Add(new Span { FontSize = argFontSize });

            currentStyle = workingString[0];

            foreach (FormattedChar item in workingString)
            {
                if (currentStyle == item)
                {
                    outString += item.Character;
                }
                else
                {
                    returnString.Spans.Add(MakeSpan(outString, currentStyle));

                    // Reset
                    outString = string.Empty;
                    outString += item.Character;
                    currentStyle = item;
                }
            }

            // Add the last one
            returnString.Spans.Add(MakeSpan(outString, currentStyle));

            return returnString;
        }

        private static Span MakeSpan(string argOutString, FormattedChar argStyle)
        {
            Span OutSpan = new Span
            {
                // Output the string
                Text = argOutString
            };

            if (argStyle.StyleBold)
            {
                OutSpan.FontAttributes = FontAttributes.Bold;
            }

            if (argStyle.StyleItalics)
            {
                OutSpan.FontAttributes = OutSpan.FontAttributes | FontAttributes.Italic;
            }

            if (argStyle.StyleSuperscript)
            {
                // TODO WOrk out how to do this. Two spans with different fontsizes?
            }

            if (argStyle.StyleUnderline)
            {
                OutSpan.TextDecorations = TextDecorations.Underline;
            }

            //// Default font
            //OutSpan.FontFamily = "Gill Sans MT";

            return OutSpan;
        }
    }
}
using System.Collections.ObjectModel;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Interfaces for the StyledText model
    /// </summary>
    /// 
    public interface IStyledTextModel
    {
        string GText { get; set; }
        ObservableCollection<GrampsStyle> Styles { get; }

        FormattedString TextFormatted
        {
            get;
        }

        /// <summary>
        /// Gets the shortened form of the text. Maximum length is 100.
        /// </summary>
        /// <value>
        /// The text short.
        /// </value>
        string TextShort
        {
            get;
        }
    }
}
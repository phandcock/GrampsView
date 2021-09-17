// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Model
{
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// Styled Text model collection.
    /// </summary>

    [KnownType(typeof(StyledTextModel))]
    public class StyledTextModel : ObservableObject, IStyledTextModel
    {
        private readonly ObservableCollection<GrampsStyle> _Styles

            = new ObservableCollection<GrampsStyle>();

        private string _GText = string.Empty;

        public StyledTextModel()
        {
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>

        public string GText
        {
            get => _GText;

            set => SetProperty(ref _GText, value);
        }

        public ObservableCollection<GrampsStyle> Styles
        {
            get
            {
                return _Styles;
            }
        }
    }
}
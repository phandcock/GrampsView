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

    [DataContract]
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
        [DataMember]
        public string GText
        {
            get
            {
                return _GText;
            }

            set
            {
                SetProperty(ref _GText, value);
            }
        }

        [DataMember]
        public ObservableCollection<GrampsStyle> Styles
        {
            get
            {
                return _Styles;
            }
        }
    }
}
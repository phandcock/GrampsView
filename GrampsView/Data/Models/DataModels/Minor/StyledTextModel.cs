// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Styled Text model collection.
    /// </summary>

    [DataContract]
    [KnownType(typeof(StyledTextModel))]
    public class StyledTextModel : CommonBindableBase, IStyledTextModel
    {
        private string _GText = string.Empty;

        private ObservableCollection<IGrampsStyle> _Styles

            = new ObservableCollection<IGrampsStyle>();

        public StyledTextModel()
        {
        }

        [DataMember]
        public ObservableCollection<IGrampsStyle> Styles
        {
            get
            {
                return _Styles;
            }
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
    }
}
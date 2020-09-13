/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Styled Text model collection.
    /// </summary>
    /// <seealso cref="System.Collections.ObjectViewModel.ObservableCollection{GrampsView.Data.ViewModel.AttributeModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<StyledTextModel>))]
    public class StyledTextModelCollection : ObservableCollection<StyledTextModel>
    {
        public StyledTextModelCollection()
        {
        }
    }
}
namespace GrampsView.Data.Model
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interfaces for the StyledText model
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase"/>
    public interface IStyledTextModel
    {
        string GText { get; set; }
        ObservableCollection<GrampsStyle> Styles { get; }
    }
}
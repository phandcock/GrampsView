namespace GrampsView.Data.Model
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase"/>
    public interface IStyledTextModel
    {
        string GText { get; set; }
        ObservableCollection<GrampsStyle> Styles { get; }
    }
}
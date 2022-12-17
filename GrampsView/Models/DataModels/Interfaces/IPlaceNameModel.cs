using GrampsView.Models.DataModels.Date;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Interface for the PlaceName model
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase"/>
    public interface IPlaceNameModel : IModelBase
    {
        DateObjectModel GDate { get; set; }

        string GLang { get; set; }

        string GValue { get; set; }
    }
}
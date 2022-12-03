using GrampsView.Models.HLinks.Models;


using static GrampsView.Common.CommonEnums;

namespace GrampsView.Data.Model
{
    /// <summary>
    ///   <br />
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase" />
    public interface IMapModel : IModelBase
    {
        string Description
        {
            get;
            set;
        }

        HLinkMapModel HLink
        {
            get;
        }

        MapType MapType
        {
            get;
            set;
        }

        Location MyLocation
        {
            get;
            set;
        }

        Placemark MyPlaceMark
        {
            get;
            set;
        }

        IAsyncRelayCommand OpenMapCommand
        {
            get;
        }

        Task OpenMap();
    }
}
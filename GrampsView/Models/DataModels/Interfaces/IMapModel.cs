using GrampsView.Data.Model;
using GrampsView.Models.HLinks.Models;


using static GrampsView.Common.CommonEnums;

namespace GrampsView.Models.DataModels.Interfaces
{
    /// <summary>
    ///   <br />
    /// </summary>
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
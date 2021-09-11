using System.Threading.Tasks;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase"/>
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

        IAsyncCommand OpenMapCommand
        {
            get;
        }

        Task OpenMap();
    }
}
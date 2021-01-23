// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkPrimaryObjectModel : HLinkBase, IHLinkPrimaryObjectModel
    {
        public IAsyncCommand<HLinkPrimaryObjectModel> UCNavigateCommand { get; set; }

        }
}
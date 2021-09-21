// TODO Needs XML 1.71 check

namespace GrampsView.Data.Collections
{
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// URL model collection.
    /// </summary>

    [KnownType(typeof(ObservableCollection<HLinkPlaceNameModel>))]
    public class HLinkPlaceNameModelCollection : HLinkBaseCollection<HLinkPlaceNameModel>
    {
        public HLinkPlaceNameModelCollection()
        {
            Title = "Place Name Collection";
        }

        public override string ToString()
        {
            // TODO Why first?

            if (this.Count > 0)
            {
                return this[0].DeRef.ToString();
            }

            return "Unknown Place Name";
        }
    }
}
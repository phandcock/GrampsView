namespace GrampsView.Data.Model
{
    using GrampsView.Common.CustomClasses;

    public interface IHLinkMediaModel : IHLinkBase
    {
        IMediaModel DeRef
        {
            get;
        }

        int GCorner1X
        {
            get; set;
        }

        int GCorner1Y
        {
            get; set;
        }

        int GCorner2X
        {
            get; set;
        }

        int GCorner2Y
        {
            get; set;
        }

        HLinkKey OriginalMediaHLink { get; set; }
    }
}
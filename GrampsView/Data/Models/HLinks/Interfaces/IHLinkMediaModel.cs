namespace GrampsView.Data.Model
{
    public interface IHLinkMediaModel : IHLinkBase
    {
        IMediaModel DeRef
        {
            get;
        }
    }
}
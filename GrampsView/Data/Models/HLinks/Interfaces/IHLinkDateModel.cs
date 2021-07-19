namespace GrampsView.Data.Model
{
    public interface IHLinkDateModel : IHLinkBase
    {
        DateObjectModel DeRef
        {
            get; set;
        }

        string Title
        {
            get; set;
        }
    }
}